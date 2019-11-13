using GameEngine;
using GameEngine.Agents;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests.Agents
{
    [TestClass]
    public class GreedyBestFirstAgentTests
    {
        [TestMethod]
        public void ShouldEvaluateHForInitialState()
        {
            var state = TestUtilities.GenerateTestState(1, 2);

            Assert.AreEqual(11, GreedyBestFirstAgent.H(state));
        }

        [TestMethod]
        public void ShouldEvaluateHForIntermediateState()
        {
            var state = TestUtilities.GenerateTestState(1, 2)
                .Successor(new Play(CardType.Red, 0));

            Assert.AreEqual(1, state.Board.Owls.InTheNest);
            state.Board.Owls.AssertPositionsMatch(1);

            Assert.AreEqual(5, GreedyBestFirstAgent.H(state));
        }

        [TestMethod]
        public void ShouldEvaluateHForWinningState()
        {
            var state = TestUtilities.GenerateTestState(1, 2)
                .Successor(new Play(CardType.Red, 0))
                .Successor(new Play(CardType.Red, 1));

            Assert.IsTrue(state.IsWin);

            Assert.AreEqual(0, GreedyBestFirstAgent.H(state));
        }

        [TestMethod]
        public void ShouldPlayNextCardFromSolution()
        {
            var state = TestUtilities.GenerateTestState(1, 1);
            var player = new GreedyBestFirstAgent();

            var play = player.FormulatePlay(state);

            Assert.AreEqual(new Play(CardType.Red, 0), play);
        }

        [TestMethod]
        public void ShouldThrowAnExceptionIfNoSolutionExists()
        {
            var state = TestUtilities.GenerateTestState(1, 1);
            state.Hand.Cards.Clear();
            state.Hand.Add(CardType.Sun);
            var player = new GreedyBestFirstAgent();

            Assert.ThrowsException<NoSolutionFoundException>(() =>
                player.FormulatePlay(state)
            );
        }

        [TestMethod]
        public void ShouldThrowAnExceptionIfTheBoardStateDoesNotMatchPreviousPlay()
        {
            var state = TestUtilities.GenerateTestState(2, 1);
            state.Hand.Cards.RemoveAll(card => card == CardType.Sun);
            var player = new GreedyBestFirstAgent();

            var ignoredNonSunPlay = player.FormulatePlay(state);
            state = state.Successor(Play.Sun);

            Assert.ThrowsException<StateMismatchException>(() =>
                player.FormulatePlay(state)
            );
        }
    }
}
