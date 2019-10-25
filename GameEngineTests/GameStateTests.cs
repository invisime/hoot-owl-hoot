using GameEngine;
using GameEngine.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngineTests
{
    [TestClass]
    public class GameStateTests
    {
        private IPlayer Player;
        private Deck Deck;
        private GameState State;
        private int Multiplier;
        private int NumberOfOwls;

        [TestInitialize]
        public void TestInitialize()
        {
            Player = new LeastRecentCardPlayer();
            Multiplier = 6;
            NumberOfOwls = 6;
            Deck = new Deck(Multiplier);
            State = new GameState(Player, Deck, Multiplier);
        }

        [TestMethod]
        public void ShouldStartGame()
        {
            var startingDeckSize = State.Deck.Cards.Count;

            State.StartGame();

            Assert.AreEqual(0, State.SunCounter);
            Assert.AreEqual(3, State.Player.Hand.Count);
            Assert.AreEqual(startingDeckSize - 3, State.Deck.Cards.Count);
        }

        [TestMethod]
        public void ShouldTakeOneTurnWithoutSunCard()
        {
            Deck.Cards.InsertRange(0, new List<CardType> {
                CardType.Red,
                CardType.Orange,
                CardType.Yellow,
                CardType.Green
            });
            State.StartGame();

            State.TakeTurn();

            var expectedHand = new List<CardType> {
                CardType.Orange,
                CardType.Yellow,
                CardType.Green
            };
            CollectionAssert.AreEqual(expectedHand, State.Player.Hand);
            CollectionAssert.Contains(State.Board.OwlPositions, 6);
            Assert.AreEqual(42, State.Deck.Cards.Count);
            Assert.AreEqual(0, State.SunCounter);
        }

        [TestMethod]
        public void ShouldPlaySunCardFirstWhenPossible()
        {
            Deck.Cards.InsertRange(0, new List<CardType> {
                CardType.Orange,
                CardType.Yellow,
                CardType.Sun,
                CardType.Green
            });
            State.StartGame();

            State.TakeTurn();

            var expectedHand = new List<CardType> {
                CardType.Orange,
                CardType.Yellow,
                CardType.Green
            };
            var expectedOwlPositions = Enumerable.Repeat(0, 6).ToList();
            CollectionAssert.AreEqual(expectedHand, State.Player.Hand);
            CollectionAssert.AreEqual(expectedOwlPositions, State.Board.OwlPositions);
            Assert.AreEqual(42, State.Deck.Cards.Count);
            Assert.AreEqual(1, State.SunCounter);
        }

        [TestMethod]
        public void ShouldWinGameWhenOwlsReachNest()
        {
            var redCards = Enumerable.Repeat(CardType.Red, Multiplier * NumberOfOwls);
            Deck.Cards.InsertRange(0, redCards);
            State.StartGame();

            Assert.IsFalse(State.GameIsWon());

            Func<int, int> getOwlsAtPosition = (queryPosition) => State.Board.OwlPositions
                .Count(position => queryPosition == position);

            foreach (int expectedNestedOwls in Enumerable.Range(0, NumberOfOwls))
            {
                Assert.AreEqual(expectedNestedOwls, getOwlsAtPosition(State.Board.NestPosition));
                Assert.AreEqual(NumberOfOwls - expectedNestedOwls, getOwlsAtPosition(0));
                foreach (int playsForThisOwl in Enumerable.Range(0, Multiplier))
                {
                    Assert.AreEqual(CardType.Red, State.Player.Hand[0]);

                    State.TakeTurn();

                    var expectedNewPosition = Multiplier * (playsForThisOwl + 1);
                    var expectedOwlsAtNewPosition = 1;
                    if (expectedNewPosition == State.Board.NestPosition)
                    {
                        expectedOwlsAtNewPosition += expectedNestedOwls;
                    }
                    Assert.AreEqual(expectedOwlsAtNewPosition, getOwlsAtPosition(expectedNewPosition));

                    int expectedOwlsAtStart = NumberOfOwls - expectedNestedOwls - 1;
                    Assert.AreEqual(expectedOwlsAtStart, getOwlsAtPosition(0));
                }
            }

            Assert.AreEqual(NumberOfOwls, getOwlsAtPosition(State.Board.NestPosition));
            Assert.AreEqual(0, State.SunCounter);
            Assert.IsTrue(State.GameIsWon());
        }

        [TestMethod]
        public void ShouldLoseGameWhenSunCounterReachesMaximum()
        {
            var cards = Enumerable.Repeat(CardType.Sun, Multiplier);
            Deck.Cards.InsertRange(0, cards);
            State.StartGame();

            Assert.IsFalse(State.GameIsLost());

            var allOwlsAtStartPosition = Enumerable.Repeat(0, NumberOfOwls).ToList();
            for (int i = 0; i < Multiplier; i++)
            {
                Assert.AreEqual(CardType.Sun, State.Player.Hand[0]);
                State.TakeTurn();
                CollectionAssert.AreEqual(allOwlsAtStartPosition, State.Board.OwlPositions);
            }

            Assert.AreEqual(Multiplier, State.SunCounter);
            Assert.IsTrue(State.GameIsLost());
        }
    }
}
