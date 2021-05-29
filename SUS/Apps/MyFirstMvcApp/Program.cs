using MyFirstMvcApp.Controllers;
using SUS.HTTP;
using System.Threading.Tasks;

namespace MyFirstMvcApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IHttpServer server = new HttpServer();

            server.AddRoute("/", new HomeController().Index);
            server.AddRoute("/favicon.ico", new StaticFilesController().Favicon);
            server.AddRoute("/about", new HomeController().About);
            server.AddRoute("/register", new UsersControllers().Register);
            server.AddRoute("/users/login", new UsersControllers().Login);

            await server.StartAsync(80);
        }
    }
}
