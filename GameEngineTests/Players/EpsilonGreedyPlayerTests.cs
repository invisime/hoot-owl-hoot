using System.Linq;
using GameEngine;
using GameEngine.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests.Players
{
    [TestClass]
    public class EpsilonGreedyPlayerTests
    {
        private CardType[] OneCardOfEachColor
        {
            get
            {
                return new Deck(1).Cards
                    .Where(card => card != CardType.Sun)
                    .ToArray();
            }
        }

        [TestMethod]
        public void ShouldPlayCardOnOnlyOwlThatGoesFurthestWhenUsingGreedyStrategy()
        {
            var player = new EpsilonGreedyPlayer(1);
            var board = new GameBoard(2, 1);
            player.Hand.Add(OneCardOfEachColor);

            var play = player.FormulatePlay(board);

            Assert.AreEqual(CardType.Red, play.Card);
            Assert.AreEqual(0, play.Position);
        }

        [TestMethod]
        public void ShouldPlayCardOnOwlThatGoesFurthestWithHootingIntoNestWhenUsingGreedyStrategy()
        {
            var player = new EpsilonGreedyPlayer(1);
            var board = new GameBoard(2);
            board.Owls.Move(0, 6);
            player.Hand.Add(OneCardOfEachColor);

            var play = player.FormulatePlay(board);

            Assert.AreEqual(CardType.Red, play.Card);
            Assert.AreEqual(1, play.Position);
        }

        [TestMethod]
        public void ShouldPlayCardAtRandomFromHandWhenUsingRandomStrategy()
        {
            var player = new EpsilonGreedyPlayer(0);
            var board = new GameBoard(2, 1);
            player.Hand.Add(OneCardOfEachColor);

            var play = player.FormulatePlay(board);

            CollectionAssert.Contains(OneCardOfEachColor, play.Card);
            Assert.AreEqual(0, play.Position);
        }
    }
}
