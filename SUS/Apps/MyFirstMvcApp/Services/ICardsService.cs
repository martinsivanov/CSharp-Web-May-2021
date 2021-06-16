using BattleCards.ViewModel.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCards.Services
{
    public interface ICardsService
    {
        int AddCard(AddCardInputModel input);

        IEnumerable<CardViewModel> GetAll();
        IEnumerable<CardViewModel> GetByUserId(string userId);
        void AddCardToCollection(string userId, int cardId);
        void RemoveCardFromCollection(string userId, int cardId);
    }
}
