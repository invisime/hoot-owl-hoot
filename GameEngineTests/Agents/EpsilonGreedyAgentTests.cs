using GameEngine;
using GameEngine.Agents;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests.Agents
{
    [TestClass]
    public class EpsilonGreedyAgentTests
    {
        [TestMethod]
        public void ShouldPlayCardOnOnlyOwlThatGoesFurthestWhenUsingGreedyStrategy()
        {
            var player = new EpsilonGreedyAgent(0);
            var greedyPlayer = new GreedyAgent();

            var state = TestUtilities.GenerateTestState(2, 1);

            var play = player.FormulatePlay(state);
            var greedyPlay = greedyPlayer.FormulatePlay(state);

            Assert.AreEqual(play.Card, greedyPlay.Card);
            Assert.AreEqual(play.Position, greedyPlay.Position);
        }

        [TestMethod]
        public void ShouldPlayCardOnOwlThatGoesFurthestWithHootingIntoNestWhenUsingGreedyStrategy()
        {
            var player = new EpsilonGreedyAgent(0);
            var greedyPlayer = new GreedyAgent();

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
            var player = new EpsilonGreedyAgent(1);
            var state = TestUtilities.GenerateTestState(2, 1);

            var play = player.FormulatePlay(state);

            CollectionAssert.Contains(CardTypeExtensions.OneCardOfEachColor, play.Card);
            Assert.AreEqual(0, play.Position);
        }
    }
}
