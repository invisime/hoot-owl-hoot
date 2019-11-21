using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    public class DeterministicDeck : Deck
    {
        public List<CardType> cards { get; private set; }
        public override List<CardType> Cards => cards;
        public override int Count => Cards.Count;

        private DeterministicDeck() { }

        public DeterministicDeck(int gameSizeMultiplier, int? numberOfSunCards = null)
        {
            var defaultSunCards = gameSizeMultiplier + 4 * gameSizeMultiplier / 3;
            BuildDeck(gameSizeMultiplier, numberOfSunCards ?? defaultSunCards);
            cards = Shuffle(Cards);
        }

        public override CardType[] Draw(int numberDesired)
        {
            var numberToDraw = Math.Min(numberDesired, Cards.Count);
            var cards = Cards.GetRange(0, numberToDraw);
            Cards.RemoveRange(0, numberToDraw);
            return cards.ToArray();
        }

        private void BuildDeck(int gameSizeMultiplier, int sunCards)
        {
            cards = new List<CardType>(Enumerable.Repeat(0, gameSizeMultiplier)
                .SelectMany(cards => CardTypeExtensions.OneCardOfEachColor)
                .Concat(Enumerable.Repeat(CardType.Sun, sunCards)));
        }

        public override bool Equals(object o)
        {
            var other = o as DeterministicDeck;
            return other != null
                && Cards.SequenceEqual(other.Cards);
        }

        public override int GetHashCode()
        {
            var hashCode = 1217012356;
            unchecked
            {
                hashCode += EqualityComparer<List<CardType>>.Default.GetHashCode(Cards);
            }
            return hashCode;
        }

        public static bool operator ==(DeterministicDeck left, DeterministicDeck right)
        {
            return EqualityComparer<DeterministicDeck>.Default.Equals(left, right);
        }

        public static bool operator !=(DeterministicDeck left, DeterministicDeck right)
        {
            return !(left == right);
        }

        public override IDeck Clone()
        {
            return new DeterministicDeck()
            {
                cards = new List<CardType>(Cards)
            };
        }
    }
}
