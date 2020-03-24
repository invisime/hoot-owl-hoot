﻿using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests
{
    [TestClass]
    public class GameStateTests
    {
        [TestMethod]
        public void ShouldEqualItsClone()
        {
            var initialState = TestUtilities.GenerateTestState();
            var clonedState = initialState.Clone();

            Assert.AreNotSame(initialState, clonedState);
            Assert.AreEqual(initialState.Board, clonedState.Board);
            Assert.AreEqual(initialState.Deck, clonedState.Deck);
            CollectionAssert.AreEqual(initialState.Hands, clonedState.Hands);
            Assert.AreEqual(initialState.SunSpaces, clonedState.SunSpaces);
            Assert.AreEqual(initialState.SunCounter, clonedState.SunCounter);
            Assert.AreEqual(initialState, clonedState);
        }

        [TestMethod]
        public void ShouldNotBeEqualIfAnOwlHasMoved()
        {
            var initialState = TestUtilities.GenerateTestState();
            var nextState = initialState.Successor(new Play(CardType.Red, 0));

            Assert.AreNotEqual(initialState.Board, nextState.Board);
            Assert.AreNotEqual(initialState, nextState);
        }

        [TestMethod]
        public void ShouldNotBeEqualIfACardHasBeenDrawnFromTheDeck()
        {
            var initialState = TestUtilities.GenerateTestState();
            var nextState = initialState.Clone();

            nextState.Deck.Draw(1);

            Assert.AreNotEqual(initialState.Deck, nextState.Deck);
            Assert.AreNotEqual(initialState, nextState);
        }

        [TestMethod]
        public void ShouldNotBeEqualIfACardHasBeenDiscardedFromTheHand()
        {
            var initialState = TestUtilities.GenerateTestState();
            var nextState = initialState.Clone();

            nextState.CurrentPlayerHand.Discard(initialState.CurrentPlayerHand.Cards[0]);

            Assert.AreNotEqual(initialState.Hands, nextState.Hands);
            Assert.AreNotEqual(initialState, nextState);
        }

        [TestMethod]
        public void ShouldNotBeEqualIfTheSunSpacesDiffer()
        {
            var initialState = TestUtilities.GenerateTestState();
            var nextState = initialState.Clone();

            nextState.SunSpaces = 0;

            Assert.AreNotEqual(initialState.SunSpaces, nextState.SunSpaces);
            Assert.AreNotEqual(initialState, nextState);
        }

        [TestMethod]
        public void ShouldNotBeEqualIfTheSunCounterHasMoved()
        {
            var initialState = TestUtilities.GenerateTestState();
            var nextState = initialState.Successor(Play.Sun);

            Assert.AreNotEqual(initialState.SunCounter, nextState.SunCounter);
            Assert.AreNotEqual(initialState, nextState);
        }
    }
}
