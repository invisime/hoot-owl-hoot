using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace GameEngineTests
{
    [TestClass]
    public class ParliamentTests
    {
        [TestMethod]
        public void ShouldInitializeOwlsAtStartingPositions()
        {
            var owls = new Parliament(2);

            owls.AssertPositionsMatch(0, 1);
            Assert.AreEqual(0, owls.InTheNest);
        }

        [TestMethod]
        public void ShouldReportOccupancy()
        {
            var owls = new Parliament(2);

            Assert.IsTrue(owls.Inhabit(0));
            Assert.IsFalse(owls.Inhabit(2));
        }

        [TestMethod]
        public void ShouldPerformMoveAction()
        {
            var owls = new Parliament(2);

            owls.Move(0, 2);

            owls.AssertPositionsMatch(1, 2);
            Assert.AreEqual(0, owls.InTheNest);
        }

        [TestMethod]
        public void ShouldPerformNestAction()
        {
            var owls = new Parliament(2);

            owls.Nest(0);

            owls.AssertPositionsMatch(1);
            Assert.AreEqual(1, owls.InTheNest);
        }

        [TestMethod]
        public void ShouldThrowWhenReferencingNonowl()
        {
            var owls = new Parliament(2);

            Assert.ThrowsException<InvalidMoveException>(() => {
                owls.Move(2, 10);
            });
            Assert.ThrowsException<InvalidMoveException>(() => {
                owls.Nest(2);
            });
        }

        [TestMethod]
        public void ShouldEqualItsClone()
        {
            var initialOwls = new Parliament(2);
            var clonedOwls = initialOwls.Clone();

            Assert.AreNotSame(initialOwls, clonedOwls);
            clonedOwls.AssertPositionsMatch(initialOwls);
            Assert.AreEqual(initialOwls.Count, clonedOwls.Count);
            Assert.AreEqual(initialOwls, clonedOwls);
        }

        [TestMethod]
        public void ShouldBeEqualIfTheyRepresentTheSameState()
        {
            var someOwls = new Parliament(2);
            var identicalOwls = new Parliament(2);

            someOwls.Nest(someOwls.TrailingOwl);
            someOwls.Move(someOwls.LeadOwl, 20);
            
            // The same actions in the other order should be equivalent.
            identicalOwls.Move(identicalOwls.LeadOwl, 20);
            identicalOwls.Nest(identicalOwls.TrailingOwl);

            Assert.AreEqual(someOwls, identicalOwls);
        }

        [TestMethod]
        public void ShouldNotBeEqualIfTheyHaveDifferentOwlCounts()
        {
            var twoOwls = new Parliament(2);
            var threeOwls = new Parliament(3);

            threeOwls.Nest(threeOwls.LeadOwl);

            twoOwls.AssertPositionsMatch(threeOwls);
            Assert.AreEqual(0, twoOwls.InTheNest);
            Assert.AreEqual(1, threeOwls.InTheNest);
            Assert.AreNotEqual(twoOwls, threeOwls);
        }

        [TestMethod]
        public void ShouldNotBeEqualIfTheOwlsAreInDifferentPositions()
        {
            var lazyOwls = new Parliament(2);
            var activeOwls = lazyOwls.Clone();

            activeOwls.Move(0, 2);

            Assert.AreNotEqual(lazyOwls, activeOwls);
        }
    }
}
