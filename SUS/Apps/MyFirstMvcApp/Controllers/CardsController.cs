using BattleCards.Data;
using BattleCards.Services;
using BattleCards.ViewModel;
using BattleCards.ViewModel;
using BattleCards.ViewModel.Cards;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Linq;

namespace BattleCards.Controllers
{
    public class CardsController : Controller
    {
        private readonly CardsService cardsService;

        public CardsController(CardsService cardsService)
        {
            this.cardsService = cardsService;
        }
        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddCardInputModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(model.Name) || model.Name.Length < 5 || model.Name.Length > 15)
            {
                return this.Error("Name should be between 5 and 20 characters long!");
            }
            if (string.IsNullOrEmpty(model.Image))
            {
                return this.Error("Image Url is required!");
            }
            if (string.IsNullOrEmpty(model.Keyword))
            {
                return this.Error("Keyword is required!");
            }
            if (model.Attack < 0)
            {
                return this.Error("Attack cannot be negative and its required!");
            }
            if (model.Health < 0)
            {
                return this.Error("Health cannot be negative and its required!");
            }
            if (string.IsNullOrEmpty(model.Description) || model.Description.Length > 200)
            {
                return this.Error("Description cannot be more than 200 characters and its required!");
            }
            var cardId =this.cardsService.AddCard(model);
            var userId = this.GetUserId();

            this.cardsService.AddCardToCollection(userId,cardId);

            return this.Redirect("/Cards/All");
        }
        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var cardsViewModel = this.cardsService.GetAll();

            return this.View(cardsViewModel);
        }
        public HttpResponse Collection()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var userId = this.GetUserId();

            var cards = this.cardsService.GetByUserId(userId);
            return this.View(cards);
        }

        public HttpResponse AddToCollection(int cardId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var userId = this.GetUserId();
            this.cardsService.AddCardToCollection(userId, cardId);
            return this.Redirect("/Cards/All");
        }
        public HttpResponse RemoveFromCollection(int cardId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var userId = this.GetUserId();
            this.cardsService.RemoveCardFromCollection(userId,cardId);
            return this.Redirect("/Cards/Collection");
        }
    }
}
