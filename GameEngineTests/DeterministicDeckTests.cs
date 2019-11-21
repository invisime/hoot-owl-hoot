using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests
{
    [TestClass]
    public class DeterministicDeckTests : DeckTests
    {
        protected override IDeck InitializeDeck(int gameSizeMultiplier, int? numberOfSunCards = null)
        {
            return new DeterministicDeck(gameSizeMultiplier, numberOfSunCards);
        }

        [TestMethod]
        public void ShouldDrawFromTheTopOfTheDeck()
        {
            var deck = new DeterministicDeck(1000);
            var top100Cards = deck.Cards.GetRange(0, 100);

            var actualDraws = deck.Draw(100);

            CollectionAssert.AreEqual(top100Cards, actualDraws);
        }
    }
}
