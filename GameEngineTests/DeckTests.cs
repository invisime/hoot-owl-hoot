using System;
using System.Collections.Generic;
using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests
{
    [TestClass]
    public class DeckTests
    {
        [TestMethod]
        public void ShouldDrawFromBeginningOfDeck()
        {
            var deck = new Deck(2);
            var expectedCards = deck.Cards.GetRange(0, 2);

            var actualCards = deck.Draw(2);

            CollectionAssert.AreEqual(expectedCards, actualCards);
        }

        [TestMethod]
        public void ShouldRemoveDrawnCardsFromTheDeck()
        {
            var deck = new Deck(2);
            var deckSizeBefore = deck.Cards.Count;

            deck.Draw(3);

            var deckSizeAfter = deck.Cards.Count;
            Assert.AreEqual(deckSizeBefore - 3, deckSizeAfter);
        }

        [TestMethod]
        public void ShouldDrawFewerWhenDrawingTooMany()
        {
            var deck = new Deck(2);
            deck.Draw(deck.Cards.Count - 1);
            var theRestOfTheDeck = new List<CardType>(deck.Cards);

            var actualCardsDrawn = deck.Draw(1000);

            CollectionAssert.AreEqual(theRestOfTheDeck, actualCardsDrawn);
        }
    }
}
