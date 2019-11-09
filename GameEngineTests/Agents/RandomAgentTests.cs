using GameEngine;
using GameEngine.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests.Agents
{
    [TestClass]
    public class RandomAgentTests
    {
        [TestMethod]
        public void ShouldPlayRandomCardFromHand()
        {
            var state = TestUtilities.GenerateTestState(2);
            state.Hand.Cards.Clear();
            state.Hand.Add(CardType.Blue);
            var player = new RandomAgent();

            var play = player.FormulatePlay(state);

            Assert.AreEqual(CardType.Blue, play.Card);
            Assert.AreEqual(0, play.Position);
        }
    }
}
