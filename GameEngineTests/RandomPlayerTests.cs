using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GameEngineTests
{
    [TestClass]
    public class RandomPlayerTests
    {
        [TestMethod]
        public void ShouldPlayCardFromHand()
        {
            var expectedCardType = CardType.Blue;
            var hand = new List<CardType> { expectedCardType };
            var player = new RandomPlayer(hand);
            var board = new GameBoard();

            var playedCard = player.Play(board);

            Assert.AreEqual(expectedCardType, playedCard);
        }

        [TestMethod]
        public void ShouldDiscard()
        {
            var expectedCardType = CardType.Blue;
            var hand = new List<CardType> { expectedCardType };
            var player = new RandomPlayer(hand);

            player.Discard(expectedCardType);

            Assert.AreEqual(0, player.Hand.Count);
        }

        [TestMethod]
        public void ShouldAddCardsToHand()
        {
            var expectedHand = new List<CardType> { CardType.Blue };
            var player = new RandomPlayer(new List<CardType>());

            player.AddCardsToHand(expectedHand);

            CollectionAssert.AreEqual(expectedHand, player.Hand);
        }
    }
}
