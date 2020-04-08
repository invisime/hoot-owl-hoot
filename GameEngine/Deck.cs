using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    public abstract class Deck : IDeck
    {
        public abstract List<CardType> Cards { get; }
        public abstract int Count { get; }
        public abstract IDeck Clone();
        public abstract CardType[] Draw(int numberDesired);
        public abstract CardType[] Sample(int numberDesired);
        public virtual CardType[] SampleAll() => Sample(Count);
        public override abstract bool Equals(object o);
        public override abstract int GetHashCode();

        public virtual IDictionary<CardType, double> Probabilities()
        {
            var cards = SampleAll();
            var counts = new Dictionary<CardType, int> 
            {
                { CardType.Blue, 0 },
                { CardType.Green, 0 },
                { CardType.Orange, 0 },
                { CardType.Purple, 0 },
                { CardType.Red, 0 },
                { CardType.Yellow, 0 },
                { CardType.Sun, 0 },
            };

            foreach(var card in cards)
            {
                counts[card]++;
            }

            return counts.Select(kvp => new { cardType = kvp.Key, prob = 1.0 * kvp.Value / Count })
                .ToDictionary(count => count.cardType, count => count.prob);
                    
        }
        
        protected int StartingCount(int multiplier, int? requestedSunCards = null)
        {
            return multiplier * CardTypeExtensions.OneCardOfEachColor.Length
                + StartingSunCount(multiplier, requestedSunCards);
        }

        protected int StartingSunCount(int multiplier, int? requestedSunCards = null)
        {
            return requestedSunCards ?? 7 * multiplier / 3;
        }
    }
}
