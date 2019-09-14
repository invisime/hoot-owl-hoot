using GameEngine;
using GameEngine.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GameEngineTests
{
    [TestClass]
    public class GameStateTests
    {
        [TestMethod]
        public void ShouldStartGame()
        {
            var player = new LeastRecentCardPlayer();
            var deck = new Deck();
            var state = new GameState(player, deck);
            var startingDeckSize = state.Deck.Cards.Count;

            state.StartGame();

            Assert.AreEqual(0, state.SunCounter);
            Assert.AreEqual(3, state.Player.Hand.Count);
            Assert.AreEqual(startingDeckSize - 3, state.Deck.Cards.Count);
        }

        [TestMethod]
        public void ShouldTakeOneTurnWithoutSunCard()
        {
            var player = new LeastRecentCardPlayer();
            var deck = new Deck();
            deck.Cards.InsertRange(0, new List<CardType> {
                CardType.Red,
                CardType.Orange,
                CardType.Yellow,
                CardType.Green
            });
            var state = new GameState(player, deck);
            state.StartGame();

            state.TakeTurn();

            var expectedHand = new List<CardType> {
                CardType.Orange,
                CardType.Yellow,
                CardType.Green
            };
            CollectionAssert.AreEqual(expectedHand, state.Player.Hand);
            Assert.AreEqual(6, state.Board.OwlPosition);
            Assert.AreEqual(42, state.Deck.Cards.Count);
            Assert.AreEqual(0, state.SunCounter);
        }

        [TestMethod]
        public void ShouldTakeOneTurnWithSunCard()
        {
            var player = new LeastRecentCardPlayer();
            var deck = new Deck();
            deck.Cards.InsertRange(0, new List<CardType> {
                CardType.Sun,
                CardType.Orange,
                CardType.Yellow,
                CardType.Green
            });
            var state = new GameState(player, deck);
            state.StartGame();

            state.TakeTurn();

            var expectedHand = new List<CardType> {
                CardType.Orange,
                CardType.Yellow,
                CardType.Green
            };
            CollectionAssert.AreEqual(expectedHand, state.Player.Hand);
            Assert.AreEqual(0, state.Board.OwlPosition);
            Assert.AreEqual(42, state.Deck.Cards.Count);
            Assert.AreEqual(1, state.SunCounter);
        }
    }
}
