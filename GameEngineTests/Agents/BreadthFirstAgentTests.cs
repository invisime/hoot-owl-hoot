using GameEngine;
using GameEngine.Agents;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests.Agents
{
    [TestClass]
    public class BreadthFirstAgentTests
    {
        public IAgent Agent;

        [TestInitialize]
        public void TestInitialize()
        {
            Agent = new BreadthFirstAgent();
        }

        [TestMethod]
        public void ShouldPlayNextCardFromSolution()
        {
            var state = TestUtilities.GenerateTestState(1, 1);

            var play = Agent.FormulatePlay(state);

            Assert.AreEqual(new Play(CardType.Red, 0), play);
        }

        [TestMethod]
        public void ShouldThrowAnExceptionIfNoSolutionExists()
        {
            var state = TestUtilities.GenerateTestState(1, 1);
            state.CurrentPlayerHand.Cards.Clear();
            state.CurrentPlayerHand.Add(CardType.Sun);

            // The only play is to play the sun card and lose.
            Assert.ThrowsException<NoSolutionFoundException>(() =>
                Agent.FormulatePlay(state)
            );
        }

        [TestMethod]
        public void ShouldThrowAnExceptionIfTheBoardStateDoesNotMatchPreviousPlay()
        {
            var state = TestUtilities.GenerateTestState(2, 1, 0);
            state.CurrentPlayerHand.Cards.Clear();
            state.CurrentPlayerHand.Cards.AddRange(CardTypeExtensions.OneCardOfEachColor);

            Agent.FormulatePlay(state);
            // Ignore what the agent said to play, and play something else instead.
            var mismatchedState = state.Successor(Play.Sun);

            Assert.ThrowsException<StateMismatchException>(() =>
                Agent.FormulatePlay(mismatchedState)
            );
        }
    }
}
