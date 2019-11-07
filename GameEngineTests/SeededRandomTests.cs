using System;
using System.Collections.Generic;
using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests
{
    [TestClass]
    public class SeededRandomTests
    {
        private int theSeed = 4;

        [TestMethod]
        public void ShouldUseRandomSeed()
        {
            var seededSample = new Random(theSeed).Next();
            var actualSample = SeededRandom.Next();

            Assert.AreNotEqual(seededSample, actualSample);
        }

        [TestMethod]
        public void ShouldUseGivenSeed()
        {
            var seededSample = new Random(theSeed).Next();
            SeededRandom.Seed = theSeed;
            var actualSample = SeededRandom.Next();

            Assert.AreEqual(seededSample, actualSample);
        }
    }
}
