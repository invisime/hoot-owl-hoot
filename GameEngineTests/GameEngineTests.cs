using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests
{
    [TestClass]
    public class GameEngineTests
    {
        [TestMethod]
        public void ShouldCompleteGame()
        {
            var engine = new GameEngine.GameEngine();
            engine.RunGame();
        }
    }
}
