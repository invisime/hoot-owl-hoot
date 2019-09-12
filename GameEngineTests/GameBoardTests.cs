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
    }
}
