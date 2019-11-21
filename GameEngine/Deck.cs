using System;
using System.Collections.Generic;

namespace GameEngine
{
    public abstract class Deck : IDeck
    {
        public abstract List<CardType> Cards { get; }
        public abstract int Count { get; }
        public abstract IDeck Clone();
        public abstract CardType[] Draw(int numberDesired);
        public CardType[] Sample(int numberDesired)
        {
            var numberToDraw = Math.Min(numberDesired, Cards.Count);
            return Cards.GetRange(0, numberToDraw).ToArray();
        }
        public virtual CardType[] SampleAll() => Sample(Count);
        public override abstract bool Equals(object o);
        public override abstract int GetHashCode();

        protected List<CardType> Shuffle(List<CardType> cards)
        {
            // http://en.wikipedia.org/wiki/Fisher-Yates_shuffle
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = SeededRandom.Next(n + 1);
                CardType card = cards[k];
                cards[k] = cards[n];
                cards[n] = card;
            }
            return cards;
        }
    }
}
