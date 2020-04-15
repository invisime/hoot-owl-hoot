using System.Linq;
using GameEngine;
using GameEngine.Agents;
using GameEngine.Heuristics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests.Agents
{
    [TestClass]
    public class BeamAgentTests
    {
        [TestMethod]
        public void ShouldThrowExceptionIfCannotDeterminePlay()
        {
            var agent = new BeamAgent(1, new WorstCaseNumberOfPlaysToGo());
            var state = TestUtilities.GenerateTestState(0, 0, 0, 0);

            Assert.ThrowsException<NoMoveFoundException>(() => agent.FormulatePlay(state));
        }

        [TestMethod]
        public void ShouldReturnPlay()
        {
            var agent = new BeamAgent(1, new WorstCaseNumberOfPlaysToGo());
            var state = TestUtilities.GenerateTestState();
            var play = agent.FormulatePlay(state);

            Assert.IsNotNull(play);
        }
    }
}
