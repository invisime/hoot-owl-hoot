using GameEngine;
using GameEngine.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GameEngineTests.Players
{
    [TestClass]
    public class RandomPlayerTests
    {
        [TestMethod]
        public void ShouldPlayRandomCardFromHand()
        {
            var expectedCardType = CardType.Blue;
            var hand = new List<CardType> { expectedCardType };
            var player = new RandomPlayer();
            player.AddCardsToHand(hand);
            var board = new GameBoard();

            var playedCard = player.Play(board);

            Assert.AreEqual(expectedCardType, playedCard);
        }
    }
}
