using System;
using System.Collections.Generic;
using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests
{
    [TestClass]
    public class PlayerHandTests
    {
        [TestMethod]
        public void ShouldDiscard()
        {
            var hand = new PlayerHand(new List<CardType> { CardType.Blue });

            hand.Discard(CardType.Blue);

            Assert.AreEqual(0, hand.Cards.Count);
        }

        [TestMethod]
        public void ShouldAddCardsToHand()
        {
            var expectedHand = new[] { CardType.Blue };
            var hand = new PlayerHand();

            hand.Add(expectedHand);

            CollectionAssert.AreEqual(expectedHand, hand.Cards);
        }


        [TestMethod]
        public void ShouldDetectIfHandContainsSun()
        {
            var hand = new PlayerHand(new List<CardType> { CardType.Sun });

            Assert.IsTrue(hand.ContainsSun);

            hand.Discard(CardType.Sun);
            Assert.IsFalse(hand.ContainsSun);
        }
    }
}
