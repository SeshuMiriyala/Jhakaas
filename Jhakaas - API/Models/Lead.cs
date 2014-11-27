
namespace Jhakaas___API.Models
{
    public class Lead
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public long Phone { get; set; }

        public string Avatar { get; set; }
    }
}