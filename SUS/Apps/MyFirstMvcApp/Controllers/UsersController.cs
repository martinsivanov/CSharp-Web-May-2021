using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.IO;
using System.Text;

namespace BattleCards.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login()
        {
            return this.View();
        }
        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse DoLogin()
        {
            //TODO: Read Data
            //TODO: check user
            //TODO: log user
            //TODO: home page
            return this.Redirect("/");
        }
    }
}
