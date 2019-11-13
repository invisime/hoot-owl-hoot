﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    public class Deck
    {
        public List<CardType> Cards { get; private set; }

        private Deck() { }

        public Deck(int gameSizeMultiplier, int? numberOfSunCards = null)
        {
            var defaultSunCards = gameSizeMultiplier + 4 * gameSizeMultiplier / 3;
            BuildDeck(gameSizeMultiplier, numberOfSunCards ?? defaultSunCards);
            Shuffle();
        }

        public CardType[] Draw(int numberDesired)
        {
            var numberToDraw = Math.Min(numberDesired, Cards.Count);
            var cards = Cards.GetRange(0, numberToDraw);
            Cards.RemoveRange(0, numberToDraw);
            return cards.ToArray();
        }

        private void BuildDeck(int gameSizeMultiplier, int sunCards)
        {
            Cards = new List<CardType>(Enumerable.Repeat(0, gameSizeMultiplier)
                .SelectMany(cards => CardTypeExtensions.OneCardOfEachColor)
                .Concat(Enumerable.Repeat(CardType.Sun, sunCards)));
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

        public override bool Equals(object o)
        {
            var other = o as Deck;
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

        public static bool operator ==(Deck left, Deck right)
        {
            return EqualityComparer<Deck>.Default.Equals(left, right);
        }

        public static bool operator !=(Deck left, Deck right)
        {
            return !(left == right);
        }

        public Deck Clone()
        {
            return new Deck()
            {
                Cards = new List<CardType>(Cards)
            };
        }
    }
}
