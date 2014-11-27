using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

using Jhakaas___API.Helpers;
using Jhakaas___API.Models.Contexts;

namespace Jhakaas___API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NotificationController : ApiController
    {
        private readonly UserContext _userContext = new UserContext();
        private readonly GcmNotifier _gcmNotifier = new GcmNotifier();

        private const string sSource = "Jhakaas Notification API";
        private const string sLog = "Jhakaas Application";

        [ActionName("NotifyAllUsers")]
        [HttpGet]
        public IEnumerable<string> NotifyAllUsers(string userName, string leadName, string activityCode)
        {
            if (!EventLog.SourceExists(sSource))
                EventLog.CreateEventSource(sSource, sLog);

            EventLog.WriteEntry(sSource, string.Format("Started NotifyAllUsers with parameters UserName: {0}, leadName: {1}, activityCode: {2}", userName, leadName, activityCode), EventLogEntryType.Information, 234);
            IEnumerable<string> result = null;
            try
            {
                var deviceIds = _userContext.GetDeviceIdsOfAllUsers();
                result = deviceIds.Select(deviceId => _gcmNotifier.SendGcmNotification(deviceId.Key, userName, ConfigurationManager.AppSettings["AppId"], deviceId.Value, activityCode, leadName)).ToList();

            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(sSource, string.Format("Exception inside NotifyAllUsers.\nMessage: {0}\nStack Trace: {1}\nException: {de 2}", ex.Message, ex.StackTrace, ex), EventLogEntryType.Error, 234);
            }
            EventLog.WriteEntry(sSource, string.Format("Finished NotifyAllUsers execution. Result: {0}", result));
            return result;
        }

        [ActionName("UpdateDeviceIdForUser")]
        [HttpGet]
        public int UpdateDeviceIdForUser(string uniquePhoneId, string deviceId)
        {
            if (!EventLog.SourceExists(sSource))
                EventLog.CreateEventSource(sSource, sLog);

            EventLog.WriteEntry(sSource, string.Format("Started UpdateDeviceIdForUser with parameters uniquePhoneId: {0}, deviceId: {1}", uniquePhoneId, deviceId), EventLogEntryType.Information, 234);

            int result = 0;
            try
            {
                result = _userContext.UpdateDeviceIdForUser(uniquePhoneId, deviceId);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(sSource, string.Format("Exception inside UpdateDeviceIdForUser.\nMessage: {0}\nStack Trace: {1}\nException: {2}", ex.Message, ex.StackTrace, ex), EventLogEntryType.Error, 234);
            }
            EventLog.WriteEntry(sSource, string.Format("Finished UpdateDeviceIdForUser execution. Result: {0}", result));
            return result;
        }


        [ActionName("RegisterForChromeNotifications")]
        [HttpGet]
        public int RegisterForChromeNotifications(string refreshId, string channelId)
        {
            if (!EventLog.SourceExists(sSource))
                EventLog.CreateEventSource(sSource, sLog);

            EventLog.WriteEntry(sSource, string.Format("Started chrome registration with parameters refreshId: {0}, channelId: {1}", refreshId, channelId), EventLogEntryType.Information, 234);

            int result = 0;
            try
            {
                System.Web.HttpContext.Current.Session["chromeCredentials"] = new { refreshId = refreshId, channelId = channelId };

            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(sSource, string.Format("Exception inside registration.\nMessage: {0}\nStack Trace: {1}\nException: {2}", ex.Message, ex.StackTrace, ex), EventLogEntryType.Error, 234);
            }
            EventLog.WriteEntry(sSource, string.Format("Finished registration execution. Result: {0}", result));
            return result;
        }



    }
}