using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    public class StochasticDeck : Deck
    {
        private Dictionary<CardType, int> CardsByCount;

        public Dictionary<CardType, int> CardWeights
        {
            get { return new Dictionary<CardType, int>(CardsByCount); }
        }

        public override List<CardType> Cards { get => SampleAll().ToList(); }

        private int RunningCount { get; set; }
        public override int Count => RunningCount;

        private StochasticDeck() { }

        public StochasticDeck(int gameSizeMultiplier, int? numberOfSunCards = null)
        {
            RunningCount = StartingCount(gameSizeMultiplier, numberOfSunCards);
            CardsByCount = CardTypeExtensions.OneCardOfEachColor
                .ToDictionary(cardType => cardType, _ => gameSizeMultiplier);
            CardsByCount[CardType.Sun] = StartingSunCount(gameSizeMultiplier, numberOfSunCards);
        }

        public override IDeck Clone()
        {
            return new StochasticDeck
            {
                RunningCount = RunningCount,
                CardsByCount = new Dictionary<CardType, int>(CardsByCount)
            };
        }

        public override CardType[] Draw(int numberDesired)
        {
            var numberToDraw = Math.Min(numberDesired, Count);
            var cardsDrawn = new CardType[numberToDraw]
                .Select(_ =>
                {
                    var card = SampleOne();
                    --CardsByCount[card];
                    --RunningCount;
                    return card;
                }).ToArray();
            return cardsDrawn;
        }

        public override CardType[] Sample(int numberDesired)
        {
            return Clone().Draw(numberDesired);
        }

        private CardType SampleOne()
        {
            var pick = SeededRandom.Next(0, Count - 1);
            var cardsSkipped = 0;
            foreach (var kvp in CardsByCount)
            {
                cardsSkipped += kvp.Value;
                if (cardsSkipped > pick)
                {
                    return kvp.Key;
                }
            }
            throw new ArgumentOutOfRangeException(String.Format(
                "Of {0} cards, pick was {1}, but we skipped {2} cards and ran out.",
                Count, pick, cardsSkipped
            ));
        }

        public override bool Equals(object o)
        {
            var other = o as StochasticDeck;
            return other != null
                && CardsByCount.SequenceEqual(other.CardsByCount);
        }

        public override int GetHashCode()
        {
            var hashCode = 1217012356;
            unchecked
            {
                hashCode += EqualityComparer<Dictionary<CardType,int>>.Default.GetHashCode(CardsByCount);
            }
            return hashCode;
        }
    }
}
