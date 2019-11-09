using GameEngine;
using GameEngine.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests.Agents
{
    [TestClass]
    public class LeastRecentCardAgentTests
    {
        [TestMethod]
        public void ShouldPlayOldestCardFromHand()
        {
            var state = TestUtilities.GenerateTestState(2);
            var firstCard = CardType.Red;
            var secondCard = CardType.Orange;
            state.Hand.Cards.Clear();
            state.Hand.Add(firstCard, secondCard);
            var player = new LeastRecentCardAgent();

            var play = player.FormulatePlay(state);
            Assert.AreEqual(firstCard, play.Card);

            state.Hand.Discard(play.Card);
            play = player.FormulatePlay(state);
            Assert.AreEqual(secondCard, play.Card);
        }
    }
}
