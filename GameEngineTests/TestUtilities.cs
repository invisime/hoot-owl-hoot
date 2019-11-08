using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests
{
    public static class TestUtilities
    {
        public static GameState GenerateTestState(int multiplier, int? numberOfOwls = null)
        {
            var board = numberOfOwls.HasValue
                ? new GameBoard(multiplier, numberOfOwls.Value)
                : new GameBoard(multiplier);
            return new GameState
            {
                Board = board,
                Hand = new PlayerHand(CardTypeExtensions.OneCardOfEachColor)
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
    }
}
