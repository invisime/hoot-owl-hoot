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
        public void ShouldDiscard()
        {
            var player = new DummyPlayer();
            player.AddCardsToHand(CardType.Blue);

            player.Discard(CardType.Blue);

            Assert.AreEqual(0, player.Hand.Count);
        }

        [TestMethod]
        public void ShouldAddCardsToHand()
        {
            var expectedHand = new [] { CardType.Blue };
            var player = new DummyPlayer();

            player.AddCardsToHand(expectedHand);

            CollectionAssert.AreEqual(expectedHand, player.Hand);
        }

        [TestMethod]
        public void ShouldDetectIfHandContainsSun()
        {
            var player = new DummyPlayer();
            player.AddCardsToHand(CardType.Sun);
            Assert.IsTrue(player.HandContainsSun());

            player.Discard(CardType.Sun);
            Assert.IsFalse(player.HandContainsSun());
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
