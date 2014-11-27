using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using Jhakaas___API.Models.Contexts;
using Newtonsoft.Json.Linq;

namespace Jhakaas___API.Helpers
{
    public class GcmNotifier
    {
        private const string PostData = "{{ \"registration_ids\": [ \"{0}\" ], " +
                                        "\"data\": {{\"tickerText\":\"{1}\"," +
                                        "\"contentTitle\":\"{2}\", " +
                                        "\"message\": \"{3}\"}}}}";

        private const string PostDataContentType = "application/json";

        private readonly PostContext _postdb = new PostContext();
        private UserContext _userdb = new UserContext();
        private LeadContext _leaddb = new LeadContext();

        private const string sSource = "Jhakaas Notification API";
        private const string sLog = "Jhakaas Application";

        internal string SendGcmNotification(string deviceUniqueId, string userName, string apiKey, string deviceId, string activityCode, string leadName)
        {
            var responseLine = string.Empty;
            var activityDetails = _postdb.GetActivityDetails(activityCode).Split('#');

            if (((activityDetails.Length != 3) || (!string.Equals(activityDetails[2], "true"))) && string.IsNullOrEmpty(deviceUniqueId) && string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(leadName)) return responseLine;

            if (string.IsNullOrEmpty(deviceId))
                return deviceUniqueId + " is not registered for mobile notifcations.";

            var tickerText = "Triggered activity " + activityDetails[0];
            var message = string.Format(activityDetails[1], userName, activityDetails[0], leadName);


            var postData = string.Format(PostData, deviceId, tickerText, tickerText, message);
            try
            {
                var byteArray = Encoding.UTF8.GetBytes(postData);

                var request = (HttpWebRequest)WebRequest.Create("https://android.googleapis.com/gcm/send");
                request.Method = "POST";
                request.KeepAlive = false;
                request.ContentType = PostDataContentType;
                request.Headers.Add(string.Format("Authorization: key={0}", apiKey));
                request.ContentLength = byteArray.Length;

                var dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                var response = request.GetResponse();
                var responseCode = ((HttpWebResponse)response).StatusCode;

                if (responseCode.Equals(HttpStatusCode.Unauthorized) ||
                    responseCode.Equals(HttpStatusCode.Forbidden))
                {
                    responseLine = "Unauthorized - need new token for UniqueId: " + deviceUniqueId;
                }
                else if (!responseCode.Equals(HttpStatusCode.OK))
                {
                    responseLine = "Response from web service isn't OK";
                }
                else
                {
                    var reader = new StreamReader(response.GetResponseStream());
                    responseLine = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                responseLine = "Notification failed for UniqueID: " + deviceUniqueId + "\n Exception: " + exception.Message;
            }
            return responseLine;
        }


        internal string SendGcmNotificationToChrome(string userName, string activityCode, string leadName, string refreshId, string channelId)
        {
            try
            {
                if (!EventLog.SourceExists(sSource))
                    EventLog.CreateEventSource(sSource, sLog);

                EventLog.WriteEntry(sSource, string.Format("Started Notification for Chrome with parameters refreshId: {0}, channelId: {1}", refreshId, channelId), EventLogEntryType.Information, 234);

                var responseLine = string.Empty;
                var activityDetails = _postdb.GetActivityDetails(activityCode).Split('#');

                if (((activityDetails.Length != 3) || (!string.Equals(activityDetails[2], "true"))) && string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(leadName)) return responseLine;

                var tickerText = "Triggered activity " + activityDetails[0];
                var message = string.Format(activityDetails[1], userName, activityDetails[0], leadName);


                NotifyChrome(message, refreshId, channelId);
                EventLog.WriteEntry(sSource, string.Format("Notification for Chrome Succeeded with parameters refreshId: {0}, channelId: {1}", refreshId, channelId), EventLogEntryType.Information, 234);

            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(sSource, string.Format("Notification for Chrome Failed with parameters refreshId: {0}, channelId: {1}.Exception Message:{2}", refreshId, channelId, ex.Message), EventLogEntryType.Information, 234);

            }


            return string.Empty;
        }

        private void NotifyChrome(string message, string refreshId, string channelId)
        {
            var postData = "client_id=528261533991.apps.googleusercontent.com&client_secret=7q9i-wTCFNeY3_Zhp0fueFbL&grant_type=refresh_token&refresh_token=" + refreshId;
            var byteArray = Encoding.UTF8.GetBytes(postData);
            var responseLine = string.Empty;

            var request = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
            request.Method = "POST";
            request.KeepAlive = false;
            request.ContentType = "application/x-www-form-urlencoded";

            var dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            var response = request.GetResponse();
            var responseCode = ((HttpWebResponse)response).StatusCode;

            if (responseCode.Equals(HttpStatusCode.Unauthorized) ||
                responseCode.Equals(HttpStatusCode.Forbidden))
            {
                responseLine = "Unauthorized - need new token for UniqueId: ";
            }
            else if (!responseCode.Equals(HttpStatusCode.OK))
            {
                responseLine = "Response from web service isn't OK";
            }
            else
            {
                ExtractAndPostMessage(message, response, channelId);
            }

        }

        private void ExtractAndPostMessage(string message, WebResponse response, string channelId)
        {
            var reader = new StreamReader(response.GetResponseStream());
            var responseLine = reader.ReadToEnd();

            reader.Close();

            var jsonResult = JObject.Parse(responseLine);
            var accessToken = jsonResult["access_token"].ToString();

            PostMessageToChrome(accessToken, message, channelId);

        }

        private void PostMessageToChrome(string accessToken, string message, string channelId)
        {

            //var postData = "{\n  \"channelId\" : \"{0}\",\n  \"subchannelId\" : \"0\",\n  \"payload\" : \"{1}\"\n}";
            //postData = string.Format(postData, channelId, message);

            var postData = "{\n  \"channelId\" : \"" + channelId + "\",\n  \"subchannelId\" : \"0\",\n  \"payload\" : \"" + message + "\"\n}";

            var byteArray = Encoding.UTF8.GetBytes(postData);
            var responseLine = string.Empty;

            var request = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/gcm_for_chrome/v1/messages");
            request.Method = "POST";
            request.KeepAlive = false;
            request.ContentType = "application/json";
            request.Headers.Add(string.Format("Authorization: Bearer {0}", accessToken));

            var dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = null;

            try
            {
                response = request.GetResponse();
                var responseCode = ((HttpWebResponse)response).StatusCode;

            }
            catch (Exception ex)
            {
                var responseError = ex.Message;
                var responseCode = ((HttpWebResponse)response).StatusCode;
            }




        }
    }

    class PostManResult
    {
        string access_token;
        string token_type;
        string expires_in;
    }
}