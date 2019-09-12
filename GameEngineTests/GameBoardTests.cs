using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GameEngineTests
{
    [TestClass]
    public class GameBoardTests
    {
        [TestMethod]
        public void ShouldStartOwlsAtFirstSpots()
        {
            var board = new GameBoard();
            var actualPosition = board.OwlPosition;
            var expectedPosition = 0;

            Assert.AreEqual(expectedPosition, actualPosition);
        }

        [TestMethod]
        //[DataRow(CardType.Red, 6)]
        [DataRow(CardType.Orange, 1)]
        [DataRow(CardType.Yellow, 2)]
        [DataRow(CardType.Green, 3)]
        [DataRow(CardType.Blue, 4)]
        [DataRow(CardType.Purple, 5)]
        public void ShouldMakeSimpleMove(CardType cardType, int newPosition)
        {
            var board = new GameBoard();
            board.Move(cardType);

            Assert.AreEqual(newPosition, board.OwlPosition);
        }
    }
}
