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

            var state = TestUtilities.GenerateTestState(2, 1);

            var play = player.FormulatePlay(state);
            var greedyPlay = greedyPlayer.FormulatePlay(state);

            Assert.AreEqual(play.Card, greedyPlay.Card);
            Assert.AreEqual(play.Position, greedyPlay.Position);
        }

        [TestMethod]
        public void ShouldPlayCardOnOwlThatGoesFurthestWithHootingIntoNestWhenUsingGreedyStrategy()
        {
            var player = new EpsilonGreedyPlayer(0);
            var greedyPlayer = new GreedyPlayer();

            var state = TestUtilities.GenerateTestState(2, 1);
            state.Board.Owls.Move(0, 6);

            var play = player.FormulatePlay(state);
            var greedyPlay = greedyPlayer.FormulatePlay(state);

            Assert.AreEqual(play.Card, greedyPlay.Card);
            Assert.AreEqual(play.Position, greedyPlay.Position);
        }

        [TestMethod]
        public void ShouldPlayCardAtRandomFromHandWhenUsingRandomStrategy()
        {
            var player = new EpsilonGreedyPlayer(1);
            var state = TestUtilities.GenerateTestState(2, 1);

            var play = player.FormulatePlay(state);

            CollectionAssert.Contains(CardTypeExtensions.OneCardOfEachColor, play.Card);
            Assert.AreEqual(0, play.Position);
        }
    }
}
