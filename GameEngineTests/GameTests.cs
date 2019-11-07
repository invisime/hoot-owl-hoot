using GameEngine;
using GameEngine.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngineTests
{
    [TestClass]
    public class GameTests
    {
        private IPlayer Player;
        private int Multiplier;
        private GameState State;
        private int NumberOfOwls { get { return Multiplier; } }

        [TestInitialize]
        public void TestInitialize()
        {
            Player = new LeastRecentCardPlayer();
            Multiplier = 6;
            State = Game.Start(Multiplier);
        }

        [TestMethod]
        public void ShouldStartGame()
        {
            var startingDeckSize = Multiplier * Enum.GetValues(typeof(CardType)).Length;

            Assert.AreEqual(0, State.SunCounter);
            Assert.AreEqual(3, State.Hand.Cards.Count);
            Assert.AreEqual(startingDeckSize - 3, State.Deck.Cards.Count);
        }

        [TestMethod]
        public void ShouldTakeOneTurnWithoutSunCard()
        {
            State.Hand.Cards.Clear();
            State.Hand.Cards.AddRange(new List<CardType> {
                CardType.Red,
                CardType.Orange,
                CardType.Yellow,
            });
            State.Deck.Cards.Insert(0, CardType.Green);
            var previousCardCount = State.Deck.Cards.Count;

            var nextState = Game.TakeTurn(State, Player);

            var expectedHand = new List<CardType> {
                CardType.Orange,
                CardType.Yellow,
                CardType.Green
            };
            CollectionAssert.AreEqual(expectedHand, nextState.Hand.Cards);
            Assert.IsTrue(nextState.Board.Owls.Inhabit(6));
            Assert.AreEqual(previousCardCount - 1, nextState.Deck.Cards.Count);
            Assert.AreEqual(0, nextState.SunCounter);
        }

        [TestMethod]
        public void ShouldPlaySunCardFirstWhenPossible()
        {
            State.Hand.Cards.Clear();
            State.Hand.Cards.AddRange(new List<CardType> {
                CardType.Orange,
                CardType.Yellow,
                CardType.Sun
            });
            State.Deck.Cards.Insert(0, CardType.Green);
            var previousCardCount = State.Deck.Cards.Count;

            var nextState = Game.TakeTurn(State, Player);

            var expectedHand = new List<CardType> {
                CardType.Orange,
                CardType.Yellow,
                CardType.Green
            };
            var expectedOwlPositions = Enumerable.Range(0, 6).ToArray();
            CollectionAssert.AreEqual(expectedHand, nextState.Hand.Cards);
            nextState.Board.AssertOwlPositionsMatch(expectedOwlPositions);
            Assert.AreEqual(previousCardCount - 1, nextState.Deck.Cards.Count);
            Assert.AreEqual(1, nextState.SunCounter);
        }

        [TestMethod]
        public void ShouldWinGameWhenOwlsReachNest()
        {
            var redCards = Enumerable.Repeat(CardType.Red, Multiplier * NumberOfOwls + 3);
            State.Deck.Cards.InsertRange(0, redCards);
            State.Hand.Cards.Clear();
            State.Hand.Add(State.Deck.Draw(3));
            var currentState = State;

            foreach (int expectedOwlsInTheNest in Enumerable.Range(0, NumberOfOwls))
            {
                var owlsInStartingPositionsOrNest = Enumerable.Range(0, NumberOfOwls - expectedOwlsInTheNest)
                    .Concat(Enumerable.Repeat(currentState.Board.NestPosition, currentState.Board.Owls.InTheNest))
                    .ToArray();

                currentState.Board.AssertOwlPositionsMatch(owlsInStartingPositionsOrNest);
                Assert.AreEqual(expectedOwlsInTheNest, currentState.Board.Owls.InTheNest);
                Assert.IsFalse(Game.IsWon(currentState));

                foreach (int playNumber in Enumerable.Range(1, Multiplier))
                {
                    Assert.AreEqual(CardType.Red, currentState.Hand.Cards[0]);

                    currentState = Game.TakeTurn(currentState, Player);

                    var expectedOwlsAtStart = Enumerable.Range(0, NumberOfOwls - expectedOwlsInTheNest - 1);
                    var expectedNewPosition = Multiplier * playNumber;
                    var expectedNestedOwls = Enumerable.Repeat(currentState.Board.NestPosition, expectedOwlsInTheNest);
                    var expectedPositions = expectedOwlsAtStart
                        .Append(expectedNewPosition)
                        .Concat(expectedNestedOwls)
                        .ToArray();
                    currentState.Board.AssertOwlPositionsMatch(expectedPositions);
                }
            }

            Assert.AreEqual(NumberOfOwls, currentState.Board.Owls.InTheNest);
            Assert.AreEqual(0, currentState.SunCounter);
            Assert.IsTrue(Game.IsWon(currentState));
        }

        [TestMethod]
        public void ShouldLoseGameWhenSunCounterReachesMaximum()
        {
            var sunCards = Enumerable.Repeat(CardType.Sun, Multiplier);
            State.Deck.Cards.InsertRange(0, sunCards);
            State.Hand.Cards.Clear();
            State.Hand.Add(State.Deck.Draw(3));
            var currentState = State;

            Assert.IsFalse(Game.IsLost(currentState));

            var allOwlsAtStartPosition = Enumerable.Range(0, NumberOfOwls).ToArray();
            for (int i = 0; i < Multiplier; i++)
            {
                Assert.AreEqual(CardType.Sun, currentState.Hand.Cards[0]);

                currentState = Game.TakeTurn(currentState, Player);

                currentState.Board.AssertOwlPositionsMatch(allOwlsAtStartPosition);
            }

            Assert.AreEqual(Multiplier, currentState.SunCounter);
            Assert.IsTrue(Game.IsLost(currentState));
        }
    }
}
