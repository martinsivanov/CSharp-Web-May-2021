using BattleCards.ViewModel;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Linq;
using System.Text;

namespace BattleCards.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Cards/All");
            }
            return this.View();
        }

        public HttpResponse About()
        {
            this.SignIn("niki");
            return this.View();
        }
    }
}
