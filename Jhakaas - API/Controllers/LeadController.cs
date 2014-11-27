using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;
using Jhakaas___API.Models;
using Jhakaas___API.Models.Contexts;

namespace Jhakaas___API.Controllers
{
    public class LeadController : ApiController
    {
        private readonly LeadContext _leaddb = new LeadContext();

        private const string sSource = "Jhakaas Notification API";
        private const string sLog = "Jhakaas Application";

        [ActionName("GetLeadsByUserId")]
        public List<Lead> GetLeadsByUserId(int userId)
        {
            if (!EventLog.SourceExists(sSource))
                EventLog.CreateEventSource(sSource, sLog);
            List<Lead> results = null;
            EventLog.WriteEntry(sSource, string.Format("Started GetLeadsByUserId with parameter userId: {0}", userId), EventLogEntryType.Information, 234);
            try
            {
                results = _leaddb.GetLeadsByUserId(userId);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(sSource, string.Format("Exception inside GetLeadsByUserId.\nMessage: {0}\nStack Trace: {1}\nException: {de 2}", ex.Message, ex.StackTrace, ex), EventLogEntryType.Error, 234);
            }
            EventLog.WriteEntry(sSource, string.Format("Finished GetLeadsByUserId execution. Result: {0}", results.Count));

            return results;
        }
    }
}
