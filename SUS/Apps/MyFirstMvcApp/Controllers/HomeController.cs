using MyFirstMvcApp.ViewModel;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Linq;
using System.Text;

namespace MyFirstMvcApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index(HttpRequest request)
        {
            var viewModel = new IndexViewModel();
            viewModel.Year = DateTime.UtcNow.Year;
            viewModel.Message = "Welcome to Battle Cards";
            return this.View(viewModel);
        }

        public HttpResponse About(HttpRequest request)
        {
            return this.View();
        }
    }
}
