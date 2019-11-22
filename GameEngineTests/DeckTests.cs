using System.Collections.Generic;
using System.Linq;
using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests
{
    public abstract class DeckTests<T> where T: Deck
    {
        protected abstract T InitializeDeck(int gameSizeMultiplier, int? numberOfSunCards = null);

        [TestMethod]
        public void ShouldEqualItsClone()
        {
            var deck = InitializeDeck(2);
            var clonedDeck = deck.Clone();

            Assert.AreNotSame(deck, clonedDeck);
            Assert.AreNotSame(deck.Cards, clonedDeck.SampleAll());
            CollectionAssert.AreEquivalent(deck.Cards, clonedDeck.SampleAll());
            Assert.AreEqual(deck, clonedDeck);
        }

        [TestMethod]
        public void ShouldNotBeEqualAfterDraw()
        {
            var deck = InitializeDeck(2);
            var clonedDeck = deck.Clone();

            clonedDeck.Draw(1);

            CollectionAssert.AreNotEquivalent(deck.Cards, clonedDeck.SampleAll());
            Assert.AreNotEqual(deck, clonedDeck);
        }

        [TestMethod]
        public void ShouldRemoveDrawnCardsFromTheDeck()
        {
            var deck = InitializeDeck(2);
            var deckSizeBefore = deck.Cards.Count;

            deck.Draw(3);

            var deckSizeAfter = deck.Cards.Count;
            Assert.AreEqual(deckSizeBefore - 3, deckSizeAfter);
        }

        [TestMethod]
        public void ShouldDrawFewerWhenDrawingTooMany()
        {
            var deck = InitializeDeck(2);
            deck.Draw(deck.Cards.Count - 1);
            var theRestOfTheDeck = new List<CardType>(deck.Cards);

            var actualCardsDrawn = deck.Draw(1000);

            CollectionAssert.AreEqual(theRestOfTheDeck, actualCardsDrawn);
        }

        [TestMethod]
        public void ShouldDefaultTo28PercentSunCards()
        {
            var cards = InitializeDeck(1).Cards;
            Assert.AreEqual(8, cards.Count);
            Assert.AreEqual(2, cards.Count(card => card == CardType.Sun));

            cards = InitializeDeck(6).Cards;
            Assert.AreEqual(50, cards.Count);
            Assert.AreEqual(14, cards.Count(card => card == CardType.Sun));

            cards = InitializeDeck(300).Cards;
            Assert.AreEqual(2500, cards.Count);
            Assert.AreEqual(700, cards.Count(card => card == CardType.Sun));
        }

        [TestMethod]
        public virtual void ShouldAcceptExplicitNumberOfSunCards()
        {
            var cards = InitializeDeck(1, 0).Cards;
            Assert.AreEqual(6, cards.Count);
            CollectionAssert.DoesNotContain(cards.ToList(), CardType.Sun);

            cards = InitializeDeck(6, 1).Cards;
            Assert.AreEqual(37, cards.Count);
            Assert.AreEqual(1, cards.Count(card => card == CardType.Sun));

            cards = InitializeDeck(300, 800).Cards;
            Assert.AreEqual(2600, cards.Count);
            Assert.AreEqual(800, cards.Count(card => card == CardType.Sun));
        }
    }
}
