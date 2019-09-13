using System;
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
            var deck = new Deck();
            var expectedCards = deck.Cards.GetRange(0, 2);

            var actualCards = deck.Draw(2);

            CollectionAssert.AreEqual(expectedCards, actualCards);
        }

        [TestMethod]
        public void ShouldRemoveDrawnCardsFromTheDeck()
        {
            var deck = new Deck();
            var deckSizeBefore = deck.Cards.Count;

            deck.Draw(3);

            var deckSizeAfter = deck.Cards.Count;
            Assert.AreEqual(deckSizeBefore - 3, deckSizeAfter);
        }
    }
}
