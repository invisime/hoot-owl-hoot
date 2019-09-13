using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests
{
    [TestClass]
    public class GameStateTests
    {
        [TestMethod]
        public void ShouldStartPlayerWithThreeCards()
        {
            var state = new GameState();

            Assert.AreEqual(3, state.Player.Hand.Count);
        }

        [TestMethod]
        public void ShouldTakeOneTurn()
        {
            var state = new GameState();
            CardType[] startingHand = new CardType[3];
            state.Player.Hand.CopyTo(startingHand);
            var startingOwlPosition = state.Board.OwlPosition;
            var startingDeckSize = state.Deck.Cards.Count;

            state.TakeTurn();

            var endingHand = state.Player.Hand;
            CollectionAssert.AreNotEqual(startingHand, endingHand);
            Assert.AreEqual(3, endingHand.Count);

            var endingOwlPosition = state.Board.OwlPosition;
            Assert.AreNotEqual(startingOwlPosition, endingOwlPosition);

            var endingDeckSize = state.Deck.Cards.Count;
            Assert.AreEqual(startingDeckSize - 1, endingDeckSize);
        }
    }
}
