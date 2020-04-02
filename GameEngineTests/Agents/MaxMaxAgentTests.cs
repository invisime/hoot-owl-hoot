using System;
using System.Linq;
using GameEngine;
using GameEngine.Agents;
using GameEngine.Heuristics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests.Agents
{
    [TestClass]
    public class MaxMaxAgentTests
    {
        [TestMethod]
        public void ShouldThrowExceptionIfCannotDetermineAPlay()
        {
            var agent = new MaxMaxAgent(1, new WorstCaseNumberOfPlaysToGo());
            var state = TestUtilities.GenerateTestState(0, 0, 0, 0);

            Assert.ThrowsException<Exception>(() => agent.FormulatePlay(state));
        }

        [TestMethod]
        public void ShouldReturnCheapestPlayWithDepthZero()
        {
            var heuristic = new WorstCaseNumberOfPlaysToGo();
            var agent = new MaxMaxAgent(0, heuristic);
            var state = TestUtilities.GenerateTestState();
            var root = new RootNode(state);
            var expectedCheapestCost = root.Expand().Select(child => heuristic.Evaluate(child.State)).Min();
            var play = agent.FormulatePlay(state);
            var cheapestState = state.Successor(play);
            var observedCheapestCost = heuristic.Evaluate(cheapestState);

            Assert.AreEqual(expectedCheapestCost, observedCheapestCost);
        }
    }
}
