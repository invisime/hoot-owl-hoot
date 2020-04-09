using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    public class DeterministicDeck : Deck
    {
        private List<CardType> cards;
        public override List<CardType> Cards => cards;
        public override int Count => Cards.Count;

        private DeterministicDeck() { }

        public DeterministicDeck(int gameSizeMultiplier, int? numberOfSunCards = null)
        {
            BuildDeck(gameSizeMultiplier, StartingSunCount(gameSizeMultiplier, numberOfSunCards));
            cards = Shuffle(Cards);
        }

        public override CardType[] Draw(int numberDesired)
        {
            var numberToDraw = Math.Min(numberDesired, Count);
            var cards = Cards.GetRange(0, numberToDraw);
            Cards.RemoveRange(0, numberToDraw);
            return cards.ToArray();
        }

        public override CardType[] Sample(int numberDesired)
        {
            var numberToDraw = Math.Min(numberDesired, Count);
            return Cards.GetRange(0, numberToDraw).ToArray();
        }

        public override Tuple<CardType, double> DrawForcedCard(CardType card)
        {
            if (cards.Contains(card))
            {
                var probs = Probabilities();
                cards.Remove(card);
                return Tuple.Create(card, probs[card]);
            }
            else
            {
                return Tuple.Create(card, 0d);
            }
        }

        private void BuildDeck(int gameSizeMultiplier, int sunCards)
        {
            cards = new List<CardType>(Enumerable.Repeat(0, gameSizeMultiplier)
                .SelectMany(cards => CardTypeExtensions.OneCardOfEachColor)
                .Concat(Enumerable.Repeat(CardType.Sun, sunCards)));
        }

        private List<CardType> Shuffle(List<CardType> cards)
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
