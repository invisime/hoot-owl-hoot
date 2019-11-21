using System.Collections.Generic;
using System.Linq;
using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests
{
    public abstract class DeckTests
    {
        protected abstract IDeck InitializeDeck(int gameSizeMultiplier, int? numberOfSunCards = null);

        protected DeterministicDeck TestableDeck(int gameSizeMultiplier, int? numberOfSunCards = null)
        {
            return InitializeDeck(gameSizeMultiplier, numberOfSunCards) as DeterministicDeck;
        }

        [TestMethod]
        public void ShouldEqualItsClone()
        {
            var deck = TestableDeck(2) as DeterministicDeck;
            var clonedDeck = deck.Clone() as DeterministicDeck;

            Assert.AreNotSame(deck, clonedDeck);
            Assert.AreNotSame(deck.Cards, clonedDeck.Cards);
            Assert.IsTrue(deck.Cards.SequenceEqual(clonedDeck.Cards));
            Assert.AreEqual(deck, clonedDeck);
        }

        [TestMethod]
        public void ShouldNotBeEqualAfterDraw()
        {
            var deck = TestableDeck(2);
            var clonedDeck = deck.Clone() as DeterministicDeck;

            clonedDeck.Draw(1);

            Assert.IsFalse(deck.Cards.SequenceEqual(clonedDeck.Cards));
            Assert.AreNotEqual(deck, clonedDeck);
        }

        [TestMethod]
        public void ShouldRemoveDrawnCardsFromTheDeck()
        {
            var deck = TestableDeck(2);
            var deckSizeBefore = deck.Cards.Count;

            deck.Draw(3);

            var deckSizeAfter = deck.Cards.Count;
            Assert.AreEqual(deckSizeBefore - 3, deckSizeAfter);
        }

        [TestMethod]
        public void ShouldDrawFewerWhenDrawingTooMany()
        {
            var deck = TestableDeck(2);
            deck.Draw(deck.Cards.Count - 1);
            var theRestOfTheDeck = new List<CardType>(deck.Cards);

            var actualCardsDrawn = deck.Draw(1000);

            CollectionAssert.AreEqual(theRestOfTheDeck, actualCardsDrawn);
        }

        [TestMethod]
        public void ShouldDefaultTo28PercentSunCards()
        {
            var cards = TestableDeck(1).Cards;
            Assert.AreEqual(8, cards.Count);
            Assert.AreEqual(2, cards.Count(card => card == CardType.Sun));

            cards = TestableDeck(6).Cards;
            Assert.AreEqual(50, cards.Count);
            Assert.AreEqual(14, cards.Count(card => card == CardType.Sun));

            cards = TestableDeck(300).Cards;
            Assert.AreEqual(2500, cards.Count);
            Assert.AreEqual(700, cards.Count(card => card == CardType.Sun));
        }

        [TestMethod]

        public virtual void ShouldAcceptExplicitNumberOfSunCards()
        {
            var cards = TestableDeck(1, 0).Cards;
            Assert.AreEqual(6, cards.Count);
            CollectionAssert.DoesNotContain(cards.ToList(), CardType.Sun);

            cards = TestableDeck(6, 1).Cards;
            Assert.AreEqual(37, cards.Count);
            Assert.AreEqual(1, cards.Count(card => card == CardType.Sun));

            cards = TestableDeck(300, 800).Cards;
            Assert.AreEqual(2600, cards.Count);
            Assert.AreEqual(800, cards.Count(card => card == CardType.Sun));
        }
    }
}
