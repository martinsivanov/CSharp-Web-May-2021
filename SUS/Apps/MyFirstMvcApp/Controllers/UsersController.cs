using BattleCards.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace BattleCards.Controllers
{
    public class UsersController : Controller
    {
        public readonly UsersService usersService;

        public UsersController()
        {
            this.usersService = new UsersService();
        }
        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost("/Users/Login")]
        public HttpResponse DoLogin()
        {
            var username = this.Request.FormData["username"];
            var password = this.Request.FormData["password"];
            var userId = this.usersService.GetUserId(username, password);
            if (userId == null)
            {
                return this.Error("Invalid username or password!");
            }

            this.SignIn(userId);
            return this.Redirect("/Cards/All");
        }

        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost("/Users/Register")]
        public HttpResponse DoRegister()
        {
            var username = this.Request.FormData["username"];
            var email = this.Request.FormData["email"];
            var password = this.Request.FormData["password"];
            var confirmPassword = this.Request.FormData["confirmPassword"];

            if (password != confirmPassword)
            {
                return this.Error("Passwords should be the same!");
            }
            if (!this.usersService.IsUsernameAvailable(username))
            {
                return this.Error("Username already taken!");
            }
            if (!Regex.IsMatch(username, @"^[A-Za-z0-9\.]+$"))
            {
                return this.Error("Invalid username!");
            }
            if (!this.usersService.IsEmailAvailable(email))
            {
                return this.Error("Email already taken!");
            }
            if (string.IsNullOrEmpty(username) || username.Length < 5 || username.Length > 20)
            {
                return this.Error("Invalid username. Then username should be betweet 5 and 20 characters!");
            }
            if (!new EmailAddressAttribute().IsValid(email) || string.IsNullOrEmpty(email))
            {
                return this.Error("Invalid email!");
            }
            if (password == null || password.Length < 6 || password.Length > 20)
            {
                return this.Error("Invalid password. The password should be between 6 and 20 characters!");
            }

            this.usersService.CreateUser(username, email, password);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("Only logged-in users can logout!");
            }
            this.SignOut();
            return this.Redirect("/");
        }
    }
}
