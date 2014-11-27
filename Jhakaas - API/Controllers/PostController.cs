using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Jhakaas___API.Helpers;
using Jhakaas___API.Models;
using Jhakaas___API.Models.Contexts;

namespace Jhakaas___API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PostController : ApiController
    {
        private readonly PostContext _postdb = new PostContext();
        private readonly GcmNotifier _gcmNotifier = new GcmNotifier();
        private readonly UserContext _userContext = new UserContext();
        private readonly LeadContext _leadContext = new LeadContext();

        [ActionName("GetNewPostsCount")]
        [HttpGet]
        public int GetNewPostsCount(DateTime lastPostCreatedOn)
        {
            return _postdb.GetNewPostsCount(lastPostCreatedOn);
        }

        [ActionName("GetNewPosts")]
        [HttpGet]
        public IEnumerable<Post> GetNewPosts(DateTime? lastPostCreatedOn)
        {
            return _postdb.GetNewPosts(lastPostCreatedOn);
        }

        [ActionName("AddNewPost")]
        [HttpGet]
        public int AddNewPost(int userId, int leadId, string activityCode, string refreshId, string channelId)
        {
            var result = _postdb.AddNewPost(userId, leadId, activityCode);

            if (result > 0)
            {
                var user = _userContext.GetUserById(userId);
                var lead = _leadContext.GetLeadById(leadId);
                var deviceIds = _userContext.GetDeviceIdsOfAllUsers();
                deviceIds.Select(deviceId => _gcmNotifier.SendGcmNotification(deviceId.Key, user.FirstName, ConfigurationManager.AppSettings["AppId"], deviceId.Value, activityCode, lead.FirstName)).ToList();

                _gcmNotifier.SendGcmNotificationToChrome(user.FirstName, activityCode, lead.FirstName, refreshId, channelId);
            }

            return result;
        }

        [ActionName("UpdatePostsStatus")]
        [HttpGet]
        public int UpdatePostsStatus(string postIds)
        {
            var ids = postIds.Replace("\"", "");
            return _postdb.UpdatePostsStatus(ids);
        }


    }
}