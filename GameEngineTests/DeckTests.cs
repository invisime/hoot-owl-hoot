using System;
using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests
{
    [TestClass]
    public class DeckTests
    {
        [TestMethod]
        public void ShouldDrawZerothCard()
        {
            var deck = new Deck();
            var expectedCard = deck.Cards[0];

            var actualCard = deck.Draw();

            Assert.AreEqual(expectedCard, actualCard);
        }

        [TestMethod]
        public void ShouldRemoveDrawnCardFromTheDeck()
        {
            var deck = new Deck();
            var deckSizeBefore = deck.Cards.Count;

            deck.Draw();

            var deckSizeAfter = deck.Cards.Count;
            Assert.AreEqual(deckSizeBefore - 1, deckSizeAfter);
        }
    }
}
