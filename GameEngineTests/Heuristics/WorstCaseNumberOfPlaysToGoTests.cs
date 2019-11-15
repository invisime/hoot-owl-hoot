using GameEngine;
using GameEngine.Heuristics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests.Agents
{
    [TestClass]
    public class WorstCaseNumberOfPlaysToGoTests
    {
        public IHeuristic heuristic = new WorstCaseNumberOfPlaysToGo();

        [TestMethod]
        public void ShouldEvaluateInitialState()
        {
            var state = TestUtilities.GenerateTestState(1, 2);

            Assert.AreEqual(11, heuristic.Evaluate(state));
        }

        [TestMethod]
        public void ShouldEvaluateIntermediateState()
        {
            var state = TestUtilities.GenerateTestState(1, 2)
                .Successor(new Play(CardType.Red, 0));

            Assert.AreEqual(1, state.Board.Owls.InTheNest);
            state.Board.Owls.AssertPositionsMatch(1);

            Assert.AreEqual(5, heuristic.Evaluate(state));
        }

        [TestMethod]
        public void ShouldEvaluateWinningState()
        {
            var state = TestUtilities.GenerateTestState(1, 2)
                .Successor(new Play(CardType.Red, 0))
                .Successor(new Play(CardType.Red, 1));

            Assert.IsTrue(state.IsWin);

            Assert.AreEqual(0, heuristic.Evaluate(state));
        }
    }
}
