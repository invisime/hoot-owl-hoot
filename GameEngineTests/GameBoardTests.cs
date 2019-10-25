using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests
{
    [TestClass]
    public class GameBoardTests
    {
        #region Single-owl tests

        [TestMethod]
        public void ShouldStartOwlAtFirstSpot()
        {
            var board = new GameBoard(2, 1);
            
            Assert.AreEqual(0, board.OwlPositions[0]);
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

            Assert.AreEqual(newPosition, board.OwlPositions[0]);
        }

        [TestMethod]
        public void ShouldMoveOwlIntoNest()
        {
            var board = new GameBoard(2, 1);
            var initialPosition = board.Board.Count - 2;
            board.OwlPositions[0] = initialPosition;
            var play = new Play(CardType.Red, initialPosition);

            board.Move(play);

            var newOwlPosition = board.OwlPositions[0];
            Assert.AreEqual(board.NestPosition, newOwlPosition,
                "Owl is not on last position.");
            Assert.AreEqual(BoardPositionType.Nest, board.Board[newOwlPosition],
                "Owl's position is not a nest.");
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
            var actualOwls = board.OwlPositions.Count;

            Assert.AreEqual(6, actualOwls);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(6)]
        [DataRow(100)]
        public void ShouldStartWithSpecificNumberOfOwls(int expectedNumberOfOwls)
        {
            var board = new GameBoard(2, expectedNumberOfOwls);
            var actualOwls = board.OwlPositions.Count;

            Assert.AreEqual(expectedNumberOfOwls, actualOwls);
        }

        [TestMethod]
        public void ShouldHootAnOwlToEmptyColoredSpace()
        {
            var board = new GameBoard(2, 2);

            board.Move(new Play(CardType.Orange, 0));

            CollectionAssert.AreEquivalent(new[] { 0, 1 }, board.OwlPositions);

            board.Move(new Play(CardType.Orange, 0));

            CollectionAssert.AreEquivalent(new[] { 1, 7 }, board.OwlPositions);
        }

        [TestMethod]
        public void ShouldHootAnOwlToNest()
        {
            var board = new GameBoard(2, 2);

            board.Move(new Play(CardType.Orange, 0));

            CollectionAssert.AreEquivalent(new[] { 0, 1 }, board.OwlPositions);

            board.Move(new Play(CardType.Orange, 0));

            CollectionAssert.AreEquivalent(new[] { 1, 7 }, board.OwlPositions);

            board.Move(new Play(CardType.Orange, 1));

            CollectionAssert.AreEquivalent(new[] { 7, board.NestPosition }, board.OwlPositions);
        }

        #endregion
    }
}
