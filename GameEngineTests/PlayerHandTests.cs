using System.Linq;
using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests
{
    [TestClass]
    public class PlayerHandTests
    {
        [TestMethod]
        public void ShouldEqualItsClone()
        {
            var initialHand = new PlayerHand();
            var clonedHand = initialHand.Clone();

            Assert.AreNotSame(initialHand, clonedHand);
            Assert.AreNotSame(initialHand.Cards, clonedHand.Cards);
            Assert.IsTrue(initialHand.Cards.SequenceEqual(clonedHand.Cards));
            Assert.AreEqual(initialHand, clonedHand);
        }

        [TestMethod]
        public void ShouldNotBeEqualAfterDiscard()
        {
            var initialHand = new PlayerHand(CardType.Blue);
            var clonedHand = initialHand.Clone();

            clonedHand.Discard(CardType.Blue);

            Assert.IsFalse(initialHand.Cards.SequenceEqual(clonedHand.Cards));
            Assert.AreNotEqual(initialHand, clonedHand);
        }

        [TestMethod]
        public void ShouldDiscard()
        {
            var hand = new PlayerHand(CardType.Blue);

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
            var hand = new PlayerHand(CardType.Sun);

            Assert.IsTrue(hand.ContainsSun);

            hand.Discard(CardType.Sun);
            Assert.IsFalse(hand.ContainsSun);
        }
    }
}
