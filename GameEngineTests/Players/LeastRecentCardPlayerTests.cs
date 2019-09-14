using System.Collections.Generic;
using GameEngine;
using GameEngine.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests.Players
{
    [TestClass]
    public class LeastRecentCardPlayerTests
    {
        [TestMethod]
        public void ShouldPlayOldestCardFromHand()
        {
            var firstCard = CardType.Red;
            var secondCard = CardType.Orange;
            var hand = new List<CardType> { firstCard, secondCard };
            var player = new LeastRecentCardPlayer();
            player.AddCardsToHand(hand);
            var board = new GameBoard();

            var playedCard = player.Play(board);
            Assert.AreEqual(firstCard, playedCard);

            player.Discard(playedCard);
            playedCard = player.Play(board);
            Assert.AreEqual(secondCard, playedCard);
        }
    }
}
