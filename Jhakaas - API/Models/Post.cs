using System;

namespace Jhakaas___API.Models
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsPostRead { get; set; }

        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string ActivityImageUrl { get; set; }
        public bool IsNotificationEnabled { get; set; }

        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserMiddleName { get; set; }
        public string UserLastName { get; set; }
        public string UserRole { get; set; }
        public string UserAvatarUrl { get; set; }

        public int LeadId { get; set; }
        public string LeadFirstName { get; set; }
        public string LeadMiddleName { get; set; }
        public string LeadLastName { get; set; }
        public string LeadAvatarUrl { get; set; }
        public string LeadEmail { get; set; }
        public long LeadPhoneNumber { get; set; }
    }
}