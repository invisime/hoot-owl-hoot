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

        public override int Count => CardsByCount.Values.Sum();

        private StochasticDeck() { }

        public StochasticDeck(int gameSizeMultiplier, int? numberOfSunCards = null)
        {
            var defaultSunCards = gameSizeMultiplier + 4 * gameSizeMultiplier / 3;
            CardsByCount = CardTypeExtensions.OneCardOfEachColor
                .ToDictionary(cardType => cardType, _ => gameSizeMultiplier);
            CardsByCount[CardType.Sun] = numberOfSunCards ?? defaultSunCards;
        }

        public override IDeck Clone()
        {
            return new StochasticDeck
            {
                CardsByCount = new Dictionary<CardType, int>(CardsByCount)
            };
        }

        public override CardType[] Draw(int numberDesired)
        {
            var cardsDrawn = Sample(numberDesired);
            cardsDrawn.ToList().ForEach(card => --CardsByCount[card]);
            return cardsDrawn;
        }

        public override CardType[] SampleAll()
        {
            return Shuffle(CardsByCount
                .SelectMany(kvp => Enumerable.Repeat(kvp.Key, kvp.Value))
                .ToList()).ToArray();
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
