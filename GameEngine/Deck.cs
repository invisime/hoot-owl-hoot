using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class Deck
    {
        private static readonly Random Random = new Random();
        public List<CardType> Cards { get; private set; }

        public Deck()
        {
            BuildDeck();
            Shuffle();
        }

        public CardType Draw()
        {
            var card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }

        private void BuildDeck()
        {
            Cards = new List<CardType>();
            for (int i = 0; i < 6; i++)
            {
                foreach (CardType type in Enum.GetValues(typeof(CardType)))
                {
                    Cards.Add(type);
                }
            }
        }

        private void Shuffle()
        {
            // http://en.wikipedia.org/wiki/Fisher-Yates_shuffle
            int n = Cards.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Next(n + 1);
                CardType value = Cards[k];
                Cards[k] = Cards[n];
                Cards[n] = value;
            }
        }
    }
}
