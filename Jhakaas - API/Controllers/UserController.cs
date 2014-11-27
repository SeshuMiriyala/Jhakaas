using System.Web.Http;
using System.Web.Http.Cors;
using Jhakaas___API.Models;
using Jhakaas___API.Models.Contexts;

namespace Jhakaas___API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private readonly UserContext _userdb = new UserContext();

        [ActionName("GetUserById")]
        public User GetUserById(int userId)
        {
            return _userdb.GetUserById(userId);
        }

        [ActionName("GetUserByFirstName")]
        public User GetUserById(string firstName)
        {
            return _userdb.GetUserByFirstName(firstName);
        }
    }
}
