using System;
using System.Collections.Generic;
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
            var expectedCardType = CardType.Blue;
            var hand = new List<CardType> { expectedCardType };
            var player = new DummyPlayer();
            player.AddCardsToHand(hand);

            player.Discard(expectedCardType);

            Assert.AreEqual(0, player.Hand.Count);
        }

        [TestMethod]
        public void ShouldAddCardsToHand()
        {
            var expectedHand = new List<CardType> { CardType.Blue };
            var player = new DummyPlayer();

            player.AddCardsToHand(expectedHand);

            CollectionAssert.AreEqual(expectedHand, player.Hand);
        }
    }

    class DummyPlayer : Player
    {
        public override CardType Play(GameBoard board)
        {
            throw new NotImplementedException();
        }
    }
}
