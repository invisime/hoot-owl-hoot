using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class PlayerHand
    {
        private static readonly Random Random = new Random();
        public List<CardType> Cards { get; }

        public PlayerHand()
        {
            Cards = new List<CardType>();
        }

        public PlayerHand(List<CardType> startingHand)
        {
            Cards = startingHand;
        }

        public CardType RandomCard
        {
            get { return Cards[Random.Next(0, Cards.Count)]; }
        }
        public bool ContainsSun
        {
            get { return Cards.Contains(CardType.Sun); }
        }

        public void Discard(CardType card)
        {
            Cards.Remove(card);
        }

        public void Add(params CardType[] cards)
        {
            Cards.AddRange(cards);
        }
    }
}
