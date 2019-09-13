using System.Collections.Generic;
using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests
{
    [TestClass]
    public class LeastRecentCardPlayerTests
    {
        [TestMethod]
        public void ShouldPlayCardFromHand()
        {
            var expectedCardType = CardType.Blue;
            var unexpected = CardType.Red;
            var hand = new List<CardType> { expectedCardType, unexpected };
            var player = new LeastRecentCardPlayer();
            var board = new GameBoard();

            var playedCard = player.Play(board);

            Assert.AreEqual(expectedCardType, playedCard);
        }

        [TestMethod]
        public void ShouldDiscard()
        {
            var expectedCardType = CardType.Blue;
            var hand = new List<CardType> { expectedCardType };
            var player = new LeastRecentCardPlayer();

            player.Discard(expectedCardType);

            Assert.AreEqual(0, player.Hand.Count);
        }

        [TestMethod]
        public void ShouldAddCardsToHand()
        {
            var expectedHand = new List<CardType> { CardType.Blue };
            var player = new LeastRecentCardPlayer();

            player.AddCardsToHand(expectedHand);

            CollectionAssert.AreEqual(expectedHand, player.Hand);
        }
    }
}
