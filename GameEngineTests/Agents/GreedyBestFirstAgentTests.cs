using GameEngine;
using GameEngine.Agents;
using GameEngine.Heuristics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests.Agents
{
    [TestClass]
    public class GreedyBestFirstAgentTests
    {
        public IAgent Agent;

        [TestInitialize]
        public void TestInitialize()
        {
            Agent = new GreedyBestFirstAgent<WorstCaseNumberOfPlaysToGo>();
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
            state.Hand.Cards.Clear();
            state.Hand.Add(CardType.Sun);

            // The only play is to play the sun card and lose.
            Assert.ThrowsException<NoSolutionFoundException>(() =>
                Agent.FormulatePlay(state)
            );
        }

        [TestMethod]
        public void ShouldThrowAnExceptionIfTheBoardStateDoesNotMatchPreviousPlay()
        {
            var state = TestUtilities.GenerateTestState(2, 1);
            state.Hand.Cards.Clear();
            state.Hand.Cards.AddRange(CardTypeExtensions.OneCardOfEachColor);
            state.RemoveAllSunCardsFromDeck();

            Agent.FormulatePlay(state);
            // Ignore what the agent said to play, and play something else instead.
            var mismatchedState = state.Successor(Play.Sun);

            Assert.ThrowsException<StateMismatchException>(() =>
                Agent.FormulatePlay(mismatchedState)
            );
        }
    }
}
