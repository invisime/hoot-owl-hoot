using System;
using GameEngine;
using GameEngine.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests.Players
{
    [TestClass]
    public class PlayerTests
    {

        [TestMethod]
        public void ShouldHaveAHand()
        {
            var player = new DummyPlayer();

            Assert.AreEqual(0, player.Hand.Cards.Count);
        }
    }

    class DummyPlayer : Player
    {
        public override Play FormulatePlay(GameBoard board)
        {
            throw new NotImplementedException();
        }
    }
}
