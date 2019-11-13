using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests
{
    [TestClass]
    public class GameBoardTests
    {
        [TestMethod]
        public void ShouldEqualItsClone()
        {
            var initialBoard = new GameBoard(6);
            var clonedBoard = initialBoard.Clone();

            Assert.AreNotSame(initialBoard, clonedBoard);
            CollectionAssert.AreEqual(initialBoard.Board, clonedBoard.Board);
            Assert.AreEqual(initialBoard.Owls, clonedBoard.Owls);
            Assert.AreEqual(initialBoard, clonedBoard);
        }

        [TestMethod]
        public void ShouldNotBeEqualIfThePositionsAreNotEqual()
        {
            var initialBoard = new GameBoard(6);
            var clonedBoard = initialBoard.Clone();

            clonedBoard.Board.Remove(0);

            Assert.AreNotEqual(initialBoard, clonedBoard);
        }

        [TestMethod]
        public void ShouldNotBeEqualIfTheParliamentsAreNotEqual()
        {
            var initialBoard = new GameBoard(6);
            var clonedBoard = initialBoard.Clone();

            clonedBoard.Owls.Nest(0);

            Assert.AreNotEqual(initialBoard, clonedBoard);
        }

        #region Single-owl tests

        [TestMethod]
        public void ShouldStartOwlAtFirstSpot()
        {
            var board = new GameBoard(2, 1);
            
            Assert.IsTrue(board.Owls.Inhabit(0));
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
            var board = new GameBoard(2, 1);
            var play = new Play(cardType, 0);

            board.Move(play);

            Assert.IsFalse(board.Owls.Inhabit(0));
            Assert.IsTrue(board.Owls.Inhabit(newPosition));
        }

        [TestMethod]
        public void ShouldMoveOwlIntoNest()
        {
            var board = new GameBoard(2, 1);
            var penultimatePosition = board.NestPosition - 1;
            board.Owls.Move(0, penultimatePosition);
            var play = new Play(CardType.Red, penultimatePosition);

            Assert.IsFalse(board.Owls.AreAllNested);

            board.Move(play);

            Assert.IsFalse(board.Owls.Inhabit(penultimatePosition));
            Assert.IsTrue(board.Owls.AreAllNested);
        }

        [TestMethod]
        public void ShouldFailToMoveOwlWhenItIsAlreadyInTheNest()
        {
            var board = new GameBoard(2, 2);
            board.Owls.Nest(0);
            
            Assert.AreEqual(1, board.Owls.InTheNest);

            var play = new Play(CardType.Red, board.NestPosition);
            Assert.ThrowsException<InvalidMoveException>(() =>
                board.Move(play)
            );
        }

        [TestMethod]
        public void ShouldFailToMoveWhenThereAreNoOwlsAtThePosition()
        {
            var board = new GameBoard(2, 1);
            var positionWithNoOwls = 1;
            var play = new Play(CardType.Red, positionWithNoOwls);

            Assert.ThrowsException<InvalidMoveException>(() =>
                board.Move(play)
            );

        }

        [TestMethod]
        public void ShouldFailToMoveWhenInvalidCardType()
        {
            var board = new GameBoard(2, 1);
            var play = new Play(CardType.Sun, 0);

            Assert.ThrowsException<InvalidMoveException>(() =>
                board.Move(play)
            );
        }

        #endregion

        #region Multi-owl tests

        [TestMethod]
        public void ShouldStartWithSixOwlsByDefault()
        {
            var board = new GameBoard(2);

            Assert.AreEqual(6, board.Owls.Count);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(6)]
        [DataRow(100)]
        public void ShouldStartWithSpecificNumberOfOwls(int expectedNumberOfOwls)
        {
            var board = new GameBoard(2, expectedNumberOfOwls);

            Assert.AreEqual(expectedNumberOfOwls, board.Owls.Count);
        }

        [TestMethod]
        public void ShouldHootAnOwlToEmptyColoredSpace()
        {
            var board = new GameBoard(2, 2);

            board.Move(new Play(CardType.Yellow, 0));

            board.AssertOwlPositionsMatch(1, 2);

            board.Move(new Play(CardType.Yellow, 1));

            board.AssertOwlPositionsMatch(2, 8);
        }

        [TestMethod]
        public void ShouldHootAnOwlToNest()
        {
            var board = new GameBoard(2, 2);

            board.Move(new Play(CardType.Red, 0));

            board.AssertOwlPositionsMatch(1, 6);

            board.Move(new Play(CardType.Red, 1));

            board.AssertOwlPositionsMatch(6, board.NestPosition);
        }

        #endregion
    }
}
