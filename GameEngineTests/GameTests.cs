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
        private int NumberOfOwls { get { return Multiplier; } }
        private Game Game;
        private GameBoard Board { get { return Game.State.Board; } }
        private DeterministicDeck Deck { get { return Game.State.Deck as DeterministicDeck; } }
        private PlayerHand Hand { get { return Game.State.Hand; } }
        private int SunCounter { get { return Game.State.SunCounter; } }

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

            Assert.AreEqual(0, Game.State.SunCounter);
            Assert.AreEqual(3, Hand.Cards.Count);
            Assert.AreEqual(startingDeckSize - 3, Deck.Cards.Count);
        }

        [TestMethod]
        public void ShouldTakeOneTurnWithoutSunCard()
        {
            Hand.Cards.Clear();
            Hand.Cards.AddRange(new List<CardType> {
                CardType.Red,
                CardType.Orange,
                CardType.Yellow,
            });
            Deck.Cards.Insert(0, CardType.Green);
            var previousCardCount = Deck.Cards.Count;

            Game.TakeTurn(Player);

            var expectedHand = new List<CardType> {
                CardType.Orange,
                CardType.Yellow,
                CardType.Green
            };
            CollectionAssert.AreEqual(expectedHand, Hand.Cards);
            Assert.IsTrue(Board.Owls.Inhabit(6));
            Assert.AreEqual(previousCardCount - 1, Deck.Cards.Count);
            Assert.AreEqual(0, SunCounter);
        }

        [TestMethod]
        public void ShouldPlaySunCardFirstWhenPossible()
        {
            Hand.Cards.Clear();
            Hand.Cards.AddRange(new List<CardType> {
                CardType.Orange,
                CardType.Yellow,
                CardType.Sun
            });
            Deck.Cards.Insert(0, CardType.Green);
            var previousCardCount = Deck.Cards.Count;

            Game.TakeTurn(Player);

            var expectedHand = new List<CardType> {
                CardType.Orange,
                CardType.Yellow,
                CardType.Green
            };
            var expectedOwlPositions = Enumerable.Range(0, 6).ToArray();
            CollectionAssert.AreEqual(expectedHand, Hand.Cards);
            Board.AssertOwlPositionsMatch(expectedOwlPositions);
            Assert.AreEqual(previousCardCount - 1, Deck.Cards.Count);
            Assert.AreEqual(1, SunCounter);
        }

        [TestMethod]
        public void ShouldWinGameWhenOwlsReachNest()
        {
            var redCards = Enumerable.Repeat(CardType.Red, Multiplier * NumberOfOwls + 3);
            Deck.Cards.InsertRange(0, redCards);
            Hand.Cards.Clear();
            Hand.Add(Deck.Draw(3));

            foreach (int expectedOwlsInTheNest in Enumerable.Range(0, NumberOfOwls))
            {
                var owlsInStartingPositionsOrNest = Enumerable.Range(0, NumberOfOwls - expectedOwlsInTheNest)
                    .Concat(Enumerable.Repeat(Board.NestPosition, Board.Owls.InTheNest))
                    .ToArray();

                Board.AssertOwlPositionsMatch(owlsInStartingPositionsOrNest);
                Assert.AreEqual(expectedOwlsInTheNest, Board.Owls.InTheNest);
                Assert.IsFalse(Game.IsWon);

                foreach (int playNumber in Enumerable.Range(1, Multiplier))
                {
                    Assert.AreEqual(CardType.Red, Hand.Cards[0]);

                    Game.TakeTurn(Player);

                    var expectedOwlsAtStart = Enumerable.Range(0, NumberOfOwls - expectedOwlsInTheNest - 1);
                    var expectedNewPosition = Multiplier * playNumber;
                    var expectedNestedOwls = Enumerable.Repeat(Board.NestPosition, expectedOwlsInTheNest);
                    var expectedPositions = expectedOwlsAtStart
                        .Append(expectedNewPosition)
                        .Concat(expectedNestedOwls)
                        .ToArray();
                    Board.AssertOwlPositionsMatch(expectedPositions);
                }
            }

            Assert.AreEqual(NumberOfOwls, Board.Owls.InTheNest);
            Assert.AreEqual(0, SunCounter);
            Assert.IsTrue(Game.IsWon);
        }

        [TestMethod]
        public void ShouldLoseGameWhenSunCounterReachesMaximum()
        {
            var sunCards = Enumerable.Repeat(CardType.Sun, Multiplier);
            Deck.Cards.InsertRange(0, sunCards);
            Hand.Cards.Clear();
            Hand.Add(Deck.Draw(3));

            Assert.IsFalse(Game.IsLost);

            var allOwlsAtStartPosition = Enumerable.Range(0, NumberOfOwls).ToArray();
            for (int i = 0; i < Multiplier; i++)
            {
                Assert.AreEqual(CardType.Sun, Hand.Cards[0]);

                Game.TakeTurn(Player);

                Board.AssertOwlPositionsMatch(allOwlsAtStartPosition);
            }

            Assert.AreEqual(Multiplier, SunCounter);
            Assert.IsTrue(Game.IsLost);
        }
    }
}
