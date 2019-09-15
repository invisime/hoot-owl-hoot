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

        [TestInitialize]
        public void TestInitialize()
        {
            Player = new LeastRecentCardPlayer();
            Multiplier = 6;
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
            Assert.AreEqual(6, State.Board.OwlPosition);
            Assert.AreEqual(42, State.Deck.Cards.Count);
            Assert.AreEqual(0, State.SunCounter);
        }

        [TestMethod]
        public void ShouldTakeOneTurnWithSunCard()
        {
            Deck.Cards.InsertRange(0, new List<CardType> {
                CardType.Sun,
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
            Assert.AreEqual(0, State.Board.OwlPosition);
            Assert.AreEqual(42, State.Deck.Cards.Count);
            Assert.AreEqual(1, State.SunCounter);
        }

        [TestMethod]
        public void ShouldEndGameWhenOwlReachesNest()
        {
            var cards = Enumerable.Repeat(CardType.Red, Multiplier);
            Deck.Cards.InsertRange(0, cards);
            State.StartGame();

            Assert.IsFalse(State.IsGameOver());

            for (int i = 0; i < Multiplier; i++)
            {
                Assert.AreEqual(CardType.Red, State.Player.Hand[0]);
                State.TakeTurn();
                Assert.AreEqual(Multiplier * (i + 1), State.Board.OwlPosition);
            }

            Assert.AreEqual(BoardPositionType.Nest, State.Board.Board[State.Board.OwlPosition]);
            Assert.AreEqual(0, State.SunCounter);
            Assert.IsTrue(State.IsGameOver());
        }

        [TestMethod]
        public void ShouldEndGameWhenSunCounterReachesMaximum()
        {
            var cards = Enumerable.Repeat(CardType.Sun, Multiplier);
            Deck.Cards.InsertRange(0, cards);
            State.StartGame();

            Assert.IsFalse(State.IsGameOver());

            for (int i = 0; i < Multiplier; i++)
            {
                Assert.AreEqual(CardType.Sun, State.Player.Hand[0]);
                State.TakeTurn();
                Assert.AreEqual(0, State.Board.OwlPosition);
            }

            Assert.AreNotEqual(BoardPositionType.Nest, State.Board.Board[State.Board.OwlPosition]);
            Assert.AreEqual(Multiplier, State.SunCounter);
            Assert.IsTrue(State.IsGameOver());
        }
    }
}
