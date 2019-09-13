using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTests
{
    [TestClass]
    public class RandomPlayerTests
    {
        [TestMethod]
        public void ShouldPlayCardFromHand()
        {
            var expectedCardType = CardType.Blue;
            var hand = new List<CardType> { expectedCardType };
            var player = new RandomPlayer(hand);
            var board = new GameBoard();

            var playedCard = player.Play(board);

            Assert.AreEqual(expectedCardType, playedCard);
        }
    }
}
