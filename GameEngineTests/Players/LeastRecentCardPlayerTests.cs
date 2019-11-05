using System.Collections.Generic;
using GameEngine;
using GameEngine.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests.Players
{
    [TestClass]
    public class LeastRecentCardPlayerTests
    {
        [TestMethod]
        public void ShouldPlayOldestCardFromHand()
        {
            var firstCard = CardType.Red;
            var secondCard = CardType.Orange;
            var player = new LeastRecentCardPlayer();
            player.Hand.Add(firstCard, secondCard);
            var board = new GameBoard(2);

            var play = player.FormulatePlay(board);
            Assert.AreEqual(firstCard, play.Card);

            player.Hand.Discard(play.Card);
            play = player.FormulatePlay(board);
            Assert.AreEqual(secondCard, play.Card);
        }
    }
}
