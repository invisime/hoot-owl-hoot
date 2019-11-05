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
            var player = new RandomPlayer();
            player.Hand.Add(CardType.Blue);
            var board = new GameBoard(2);

            var play = player.FormulatePlay(board);

            Assert.AreEqual(CardType.Blue, play.Card);
            Assert.AreEqual(0, play.Position);
        }
    }
}
