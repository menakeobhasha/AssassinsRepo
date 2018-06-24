using SMS.Business;
using SMS.Model;
using System.Web.Http;

namespace StockMarketSimulation.Api.Controllers
{
    public class LoginController : ApiController
    {
        public LoginBL oLoginBL = new LoginBL();
        // GET: Login
        [HttpGet]
        public LoginDTO GetLoginData(string UserName)
        {
            return oLoginBL.GetUserNameByUserId(UserName);
        }

        [HttpGet]
        public string GetUserNameByUserIdFromAllUsers(string userId)
        {
            return oLoginBL.GetUserNameByUserIdFromAllUsers(userId);
        }

        //[HttpGet]
        //public string Get()
        //{
        //    return "hello";
        //}
    }
}