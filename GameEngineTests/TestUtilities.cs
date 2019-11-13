using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace GameEngineTests
{
    public static class TestUtilities
    {
        public static GameState GenerateTestState(int multiplier = 6, int? numberOfOwls = null)
        {
            var board = numberOfOwls.HasValue
                ? new GameBoard(multiplier, numberOfOwls.Value)
                : new GameBoard(multiplier);
            return new GameState
            {
                Board = board,
                Deck = new Deck(multiplier),
                Hand = new PlayerHand(CardTypeExtensions.OneCardOfEachColor),
                SunCounter = 0,
                SunSpaces = multiplier
            };
        }

        public static void AssertOwlPositionsMatch(this GameBoard board, params int[] expectedPositions)
        {
            Assert.AreEqual(expectedPositions.Length, board.Owls.Count);
            int expectedOwlsInNest = 0;
            foreach (var expectedPosition in expectedPositions)
            {
                if (expectedPosition == board.NestPosition)
                {
                    expectedOwlsInNest++;
                }
                else
                {
                    Assert.IsTrue(board.Owls.Inhabit(expectedPosition),
                        "No owl found at " + expectedPosition);
                }
            }
            Assert.AreEqual(expectedOwlsInNest, board.Owls.InTheNest);
        }

        public static void AssertPositionsMatch(this Parliament owls, Parliament otherOwls)
        {
            owls.AssertPositionsMatch(otherOwls.ListOfPositions.ToArray());
        }

        public static void AssertPositionsMatch(this Parliament owls, params int[] expectedPositions)
        {
            CollectionAssert.AreEquivalent(expectedPositions, owls.ListOfPositions.ToList());
        }
    }
}
