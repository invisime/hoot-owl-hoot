using System.Linq;
using GameEngine;
using GameEngine.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests.Players
{
    [TestClass]
    public class EpsilonGreedyPlayerTests
    {
        [TestMethod]
        public void ShouldPlayCardOnOnlyOwlThatGoesFurthestWhenUsingGreedyStrategy()
        {
            var player = new EpsilonGreedyPlayer(0);
            var greedyPlayer = new GreedyPlayer();

            var board = new GameBoard(2, 1);

            player.Hand.Add(CardTypeExtensions.OneCardOfEachColor);
            greedyPlayer.Hand.Add(CardTypeExtensions.OneCardOfEachColor);

            var play = player.FormulatePlay(board);
            var greedyPlay = player.FormulatePlay(board);

            Assert.AreEqual(play.Card, greedyPlay.Card);
            Assert.AreEqual(play.Position, greedyPlay.Position);
        }

        [TestMethod]
        public void ShouldPlayCardOnOwlThatGoesFurthestWithHootingIntoNestWhenUsingGreedyStrategy()
        {
            var player = new EpsilonGreedyPlayer(0);
            var greedyPlayer = new GreedyPlayer();

            var board = new GameBoard(2);
            board.Owls.Move(0, 6);

            player.Hand.Add(CardTypeExtensions.OneCardOfEachColor);
            greedyPlayer.Hand.Add(CardTypeExtensions.OneCardOfEachColor);

            var play = player.FormulatePlay(board);
            var greedyPlay = player.FormulatePlay(board);

            Assert.AreEqual(play.Card, greedyPlay.Card);
            Assert.AreEqual(play.Position, greedyPlay.Position);
        }

        [TestMethod]
        public void ShouldPlayCardAtRandomFromHandWhenUsingRandomStrategy()
        {
            var player = new EpsilonGreedyPlayer(1);
            var board = new GameBoard(2, 1);
            player.Hand.Add(CardTypeExtensions.OneCardOfEachColor);

            var play = player.FormulatePlay(board);

            CollectionAssert.Contains(CardTypeExtensions.OneCardOfEachColor, play.Card);
            Assert.AreEqual(0, play.Position);
        }
    }
}
