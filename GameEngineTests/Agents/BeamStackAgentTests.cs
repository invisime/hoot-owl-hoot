using GameEngine;
using GameEngine.Agents;
using GameEngine.Heuristics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests.Agents
{
    [TestClass]
    public class BeamStackAgentTests
    {
        [TestMethod]
        public void ShouldThrowExceptionIfCannotDetermineAPlay()
        {
            var agent = new BeamStackAgent(5000, new WorstCaseNumberOfPlaysToGo());
            var state = TestUtilities.GenerateTestState(0, 0, 0, 0);

            Assert.ThrowsException<NoMoveFoundException>(() => agent.FormulatePlay(state));
        }
        
        [TestMethod]
        public void ShouldReturnPlay()
        {
            var agent = new BeamStackAgent(5000, new WorstCaseNumberOfPlaysToGo());
            var state = TestUtilities.GenerateTestState();
            var play = agent.FormulatePlay(state);

            Assert.IsNotNull(play);
        }
    }
}
