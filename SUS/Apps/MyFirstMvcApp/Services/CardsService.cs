﻿using BattleCards.Data;
using BattleCards.ViewModel.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleCards.Services
{
    public class CardsService : ICardsService
    {
        private readonly ApplicationDbContext db;

        public CardsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public int AddCard(AddCardInputModel input)
        {
            var card = new Card
            {
                Attack = input.Attack,
                Health = input.Health,
                Description = input.Description,
                Name = input.Name,
                ImageUrl = input.Image,
                Keyword = input.Keyword
            };
            this.db.Cards.Add(card);
            this.db.SaveChanges();
            return card.Id;
        }
        public IEnumerable<CardViewModel> GetAll()
        {
            return this.db.Cards.Select(x => new CardViewModel
            {
                Name = x.Name,
                Attack = x.Attack,
                Health = x.Health,
                ImageUrl = x.ImageUrl,
                Type = x.Keyword,
                Description = x.Description,
                Id = x.Id
            }).ToList();
        }

        public IEnumerable<CardViewModel> GetByUserId(string userId)
        {
            return this.db.UserCards.Where(x => x.UserId == userId)
                   .Select(x => new CardViewModel
                   {
                       Name = x.Card.Name,
                       Attack = x.Card.Attack,
                       Description = x.Card.Description,
                       Health = x.Card.Health,
                       ImageUrl = x.Card.ImageUrl,
                       Type = x.Card.Keyword,
                       Id = x.CardId
                   }).ToList();
        }
        public void AddCardToCollection(string userId, int cardId)
        {
            if (this.db.UserCards.Any(x => x.UserId == userId && x.CardId == cardId))
            {
                return;
            }
            this.db.UserCards.Add(new UserCard
            {
                UserId = userId,
                CardId = cardId
            });
            this.db.SaveChanges();
        }
        public void RemoveCardFromCollection(string userId, int cardId)
        {
            var userCard = this.db.UserCards.FirstOrDefault(x => x.UserId == userId && x.CardId == cardId);
            if (userCard == null)
            {
                return;
            }
            this.db.UserCards.Remove(userCard);
            this.db.SaveChanges();
        }
    }
}
