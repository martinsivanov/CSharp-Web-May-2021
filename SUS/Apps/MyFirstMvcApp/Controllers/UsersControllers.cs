﻿using SUS.HTTP;
using System.Text;

namespace MyFirstMvcApp.Controllers
{
    public class UsersControllers
    {
        public HttpResponse Login(HttpRequest request)
        {
            var responseHtml = "<h1>Login...</h1>";
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }
        public HttpResponse Register(HttpRequest request)
        {
            var responseHtml = "<h1>Register...</h1>";
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }
    }
}
