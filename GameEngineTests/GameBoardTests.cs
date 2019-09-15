using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests
{
    [TestClass]
    public class GameBoardTests
    {
        [TestMethod]
        public void ShouldStartOwlsAtFirstSpots()
        {
            var board = new GameBoard(2);
            var actualPosition = board.OwlPosition;
            var expectedPosition = 0;

            Assert.AreEqual(expectedPosition, actualPosition);
        }

        [TestMethod]
        [DataRow(CardType.Red, 6)]
        [DataRow(CardType.Orange, 1)]
        [DataRow(CardType.Yellow, 2)]
        [DataRow(CardType.Green, 3)]
        [DataRow(CardType.Blue, 4)]
        [DataRow(CardType.Purple, 5)]
        public void ShouldMakeSimpleMoves(CardType cardType, int newPosition)
        {
            var board = new GameBoard(2);

            board.Move(cardType);

            Assert.AreEqual(newPosition, board.OwlPosition);
        }

        [TestMethod]
        public void ShouldMoveOwlIntoNest()
        {
            var board = new GameBoard(2);
            board.OwlPosition = board.Board.Count - 2;

            board.Move(CardType.Red);

            Assert.AreEqual(board.Board.Count - 1, board.OwlPosition);
            Assert.AreEqual(BoardPositionType.Nest, board.Board[board.OwlPosition]);
        }

        [TestMethod]
        public void ShouldFailToMoveOwlWhenItIsAlreadyInTheNest()
        {
            var board = new GameBoard(2);
            board.OwlPosition = board.Board.Count - 1;

            Assert.ThrowsException<InvalidMoveException>(() => board.Move(CardType.Red));
        }

        [TestMethod]
        public void ShouldFailToMoveWhenInvalidCardType()
        {
            var board = new GameBoard(2);
            board.OwlPosition = board.Board.Count - 1;

            Assert.ThrowsException<InvalidMoveException>(() => board.Move(CardType.Sun));
        }
    }
}
