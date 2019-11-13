using GameEngine;
using GameEngine.Agents;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests.Agents
{
    [TestClass]
    public class GreedyAgentTests
    {
        [TestMethod]
        public void ShouldPlayCardOnOnlyOwlThatGoesFurthest()
        {
            var player = new GreedyAgent();
            var state = TestUtilities.GenerateTestState(2, 1);

            var play = player.FormulatePlay(state);

            Assert.AreEqual(CardType.Red, play.Card);
            Assert.AreEqual(0, play.Position);
        }

        [TestMethod]
        public void ShouldPlayCardOnOwlThatGoesFurthestWithHootingIntoNest()
        {
            var player = new GreedyAgent();
            var state = TestUtilities.GenerateTestState(2);
            state.Board.Owls.Move(0, 6);

            var play = player.FormulatePlay(state);

            Assert.AreEqual(CardType.Red, play.Card);
            Assert.AreEqual(1, play.Position);
        }
    }
}
