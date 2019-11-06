using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class Deck
    {
        public List<CardType> Cards { get; private set; }

        public Deck(int gameSizeMultiplier)
        {
            BuildDeck(gameSizeMultiplier);
            Shuffle();
        }

        public CardType[] Draw(int numberDesired)
        {
            var numberToDraw = Math.Min(numberDesired, Cards.Count);
            var cards = Cards.GetRange(0, numberToDraw);
            Cards.RemoveRange(0, numberToDraw);
            return cards.ToArray();
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
                int k = SeededRandom.Next(n + 1);
                CardType value = Cards[k];
                Cards[k] = Cards[n];
                Cards[n] = value;
            }
        }
    }
}
