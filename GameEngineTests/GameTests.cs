using GameEngine;
using GameEngine.Agents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngineTests
{
    [TestClass]
    public class GameTests
    {
        private IAgent Player;
        private int Multiplier;
        private Game Game;
        private GameState State { get { return Game.State; } }
        private int NumberOfOwls { get { return Multiplier; } }

        [TestInitialize]
        public void TestInitialize()
        {
            Player = new LeastRecentCardAgent();
            Multiplier = 6;
            Game = new Game(Multiplier);
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

            Game.TakeTurn(Player);

            var expectedHand = new List<CardType> {
                CardType.Orange,
                CardType.Yellow,
                CardType.Green
            };
            CollectionAssert.AreEqual(expectedHand, State.Hand.Cards);
            Assert.IsTrue(State.Board.Owls.Inhabit(6));
            Assert.AreEqual(previousCardCount - 1, State.Deck.Cards.Count);
            Assert.AreEqual(0, State.SunCounter);
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

            Game.TakeTurn(Player);

            var expectedHand = new List<CardType> {
                CardType.Orange,
                CardType.Yellow,
                CardType.Green
            };
            var expectedOwlPositions = Enumerable.Range(0, 6).ToArray();
            CollectionAssert.AreEqual(expectedHand, State.Hand.Cards);
            State.Board.AssertOwlPositionsMatch(expectedOwlPositions);
            Assert.AreEqual(previousCardCount - 1, State.Deck.Cards.Count);
            Assert.AreEqual(1, State.SunCounter);
        }

        [TestMethod]
        public void ShouldWinGameWhenOwlsReachNest()
        {
            var redCards = Enumerable.Repeat(CardType.Red, Multiplier * NumberOfOwls + 3);
            State.Deck.Cards.InsertRange(0, redCards);
            State.Hand.Cards.Clear();
            State.Hand.Add(State.Deck.Draw(3));

            foreach (int expectedOwlsInTheNest in Enumerable.Range(0, NumberOfOwls))
            {
                var owlsInStartingPositionsOrNest = Enumerable.Range(0, NumberOfOwls - expectedOwlsInTheNest)
                    .Concat(Enumerable.Repeat(State.Board.NestPosition, State.Board.Owls.InTheNest))
                    .ToArray();

                State.Board.AssertOwlPositionsMatch(owlsInStartingPositionsOrNest);
                Assert.AreEqual(expectedOwlsInTheNest, State.Board.Owls.InTheNest);
                Assert.IsFalse(Game.IsWon);

                foreach (int playNumber in Enumerable.Range(1, Multiplier))
                {
                    Assert.AreEqual(CardType.Red, State.Hand.Cards[0]);

                    Game.TakeTurn(Player);

                    var expectedOwlsAtStart = Enumerable.Range(0, NumberOfOwls - expectedOwlsInTheNest - 1);
                    var expectedNewPosition = Multiplier * playNumber;
                    var expectedNestedOwls = Enumerable.Repeat(State.Board.NestPosition, expectedOwlsInTheNest);
                    var expectedPositions = expectedOwlsAtStart
                        .Append(expectedNewPosition)
                        .Concat(expectedNestedOwls)
                        .ToArray();
                    State.Board.AssertOwlPositionsMatch(expectedPositions);
                }
            }

            Assert.AreEqual(NumberOfOwls, State.Board.Owls.InTheNest);
            Assert.AreEqual(0, State.SunCounter);
            Assert.IsTrue(Game.IsWon);
        }

        [TestMethod]
        public void ShouldLoseGameWhenSunCounterReachesMaximum()
        {
            var sunCards = Enumerable.Repeat(CardType.Sun, Multiplier);
            State.Deck.Cards.InsertRange(0, sunCards);
            State.Hand.Cards.Clear();
            State.Hand.Add(State.Deck.Draw(3));

            Assert.IsFalse(Game.IsLost);

            var allOwlsAtStartPosition = Enumerable.Range(0, NumberOfOwls).ToArray();
            for (int i = 0; i < Multiplier; i++)
            {
                Assert.AreEqual(CardType.Sun, State.Hand.Cards[0]);

                Game.TakeTurn(Player);

                State.Board.AssertOwlPositionsMatch(allOwlsAtStartPosition);
            }

            Assert.AreEqual(Multiplier, State.SunCounter);
            Assert.IsTrue(Game.IsLost);
        }
    }
}
