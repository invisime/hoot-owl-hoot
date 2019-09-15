using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class Deck
    {
        private static readonly Random Random = new Random();
        public List<CardType> Cards { get; private set; }

        public Deck(int gameSizeMultiplier)
        {
            BuildDeck(gameSizeMultiplier);
            Shuffle();
        }

        public List<CardType> Draw(int numberOfDraws)
        {
            var cards = Cards.GetRange(0, numberOfDraws);
            Cards.RemoveRange(0, numberOfDraws);
            return cards;
        }

        private void BuildDeck(int gameSizeMultiplier)
        {
            Cards = new List<CardType>();
            for (int i = 0; i < gameSizeMultiplier; i++)
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
