using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GameEngineTests
{
    [TestClass]
    public class StochasticDeckTests : DeckTests<StochasticDeck>
    {
        protected override StochasticDeck InitializeDeck(int gameSizeMultiplier, int? numberOfSunCards = null)
        {
            return new StochasticDeck(gameSizeMultiplier, numberOfSunCards);
        }

        [TestMethod]
        public void ShouldProvideAccurateCardCountAfterCardsAreDrawn()
        {
            var deck = new StochasticDeck(6);
            var expectedCardWeights = InitialCardWeights(6);
            
            CollectionAssert.AreEquivalent(expectedCardWeights, deck.CardWeights);
            
            var drawnCards = deck.Draw(10).ToList();
            drawnCards.ForEach(card => --expectedCardWeights[card]);

            CollectionAssert.AreEquivalent(expectedCardWeights, deck.CardWeights);
        }

        [TestMethod]
        public void ShouldNotJustDrawFromTheTopOfTheDeck()
        {
            var deck = new StochasticDeck(1000);
            var top100Cards = deck.Cards.GetRange(0, 100);

            var actualDraws = deck.Draw(100);

            // It's possible this fails, but it's very, very unlikely.
            CollectionAssert.AreNotEqual(top100Cards, actualDraws);
        }

        private Dictionary<CardType, int> InitialCardWeights(int multiplier)
        {
            var weights = CardTypeExtensions.OneCardOfEachColor
                .ToDictionary(cardType => cardType, _ => multiplier);
            weights[CardType.Sun] = multiplier + 4 * multiplier / 3;
            return weights;
        }
    }
}
