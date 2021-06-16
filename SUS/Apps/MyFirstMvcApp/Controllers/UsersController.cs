using BattleCards.Services;
using BattleCards.ViewModel.Users;
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
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username,string password)
        {
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

        [HttpPost]
        public HttpResponse Register(RegisterInputModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return this.Error("Passwords should be the same!");
            }
            if (!this.usersService.IsUsernameAvailable(model.Username))
            {
                return this.Error("Username already taken!");
            }
            if (!Regex.IsMatch(model.Username, @"^[A-Za-z0-9\.]+$"))
            {
                return this.Error("Invalid username!");
            }
            if (!this.usersService.IsEmailAvailable(model.Email))
            {
                return this.Error("Email already taken!");
            }
            if (string.IsNullOrEmpty(model.Username) || model.Username.Length < 5 || model.Username.Length > 20)
            {
                return this.Error("Invalid username. Then username should be betweet 5 and 20 characters!");
            }
            if (!new EmailAddressAttribute().IsValid(model.Email) || string.IsNullOrEmpty(model.Email))
            {
                return this.Error("Invalid email!");
            }
            if (model.Password == null || model.Password.Length < 6 || model.Password.Length > 20)
            {
                return this.Error("Invalid password. The password should be between 6 and 20 characters!");
            }

            this.usersService.CreateUser(model.Username, model.Email, model.Password);

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
