using System.Collections.Generic;
using System.Linq;
using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTests
{
    public abstract class DeckTests<T> where T: Deck
    {
        protected abstract T InitializeDeck(int gameSizeMultiplier, int? numberOfSunCards = null);

        [TestMethod]
        public void ShouldEqualItsClone()
        {
            var deck = InitializeDeck(2);
            var clonedDeck = deck.Clone();

            Assert.AreNotSame(deck, clonedDeck);
            Assert.AreNotSame(deck.Cards, clonedDeck.SampleAll());
            CollectionAssert.AreEquivalent(deck.Cards, clonedDeck.SampleAll());
            Assert.AreEqual(deck, clonedDeck);
        }

        [TestMethod]
        public void ShouldNotBeEqualAfterDraw()
        {
            var deck = InitializeDeck(2);
            var clonedDeck = deck.Clone();

            clonedDeck.Draw(1);

            CollectionAssert.AreNotEquivalent(deck.Cards, clonedDeck.SampleAll());
            Assert.AreNotEqual(deck, clonedDeck);
        }

        [TestMethod]
        public void ShouldRemoveDrawnCardsFromTheDeck()
        {
            var deck = InitializeDeck(2);
            var deckSizeBefore = deck.Count;

            deck.Draw(3);

            var deckSizeAfter = deck.Count;
            Assert.AreEqual(deckSizeBefore - 3, deckSizeAfter);
        }

        [TestMethod]
        public void ShouldDrawFewerWhenDrawingTooMany()
        {
            var deck = InitializeDeck(2);
            deck.Draw(deck.Count - 1);
            var theRestOfTheDeck = new List<CardType>(deck.Cards);

            var actualCardsDrawn = deck.Draw(1000);

            CollectionAssert.AreEqual(theRestOfTheDeck, actualCardsDrawn);
        }

        [TestMethod]
        public void ShouldDefaultTo28PercentSunCards()
        {
            var cards = InitializeDeck(1).Cards;
            Assert.AreEqual(8, cards.Count);
            Assert.AreEqual(2, cards.Count(card => card == CardType.Sun));

            cards = InitializeDeck(6).Cards;
            Assert.AreEqual(50, cards.Count);
            Assert.AreEqual(14, cards.Count(card => card == CardType.Sun));

            cards = InitializeDeck(300).Cards;
            Assert.AreEqual(2500, cards.Count);
            Assert.AreEqual(700, cards.Count(card => card == CardType.Sun));
        }

        [TestMethod]
        public virtual void ShouldAcceptExplicitNumberOfSunCards()
        {
            var cards = InitializeDeck(1, 0).Cards;
            Assert.AreEqual(6, cards.Count);
            CollectionAssert.DoesNotContain(cards.ToList(), CardType.Sun);

            cards = InitializeDeck(6, 1).Cards;
            Assert.AreEqual(37, cards.Count);
            Assert.AreEqual(1, cards.Count(card => card == CardType.Sun));

            cards = InitializeDeck(300, 800).Cards;
            Assert.AreEqual(2600, cards.Count);
            Assert.AreEqual(800, cards.Count(card => card == CardType.Sun));
        }

        [TestMethod]
        public void ShouldShowCorrectProbabilities()
        {
            var deck = InitializeDeck(1, 1);
            var probs = deck.Probabilities();

            Assert.AreEqual(0.1428, probs[CardType.Blue], 0.0001);
            Assert.AreEqual(0.1428, probs[CardType.Green], 0.0001);
            Assert.AreEqual(0.1428, probs[CardType.Orange], 0.0001);
            Assert.AreEqual(0.1428, probs[CardType.Purple], 0.0001);
            Assert.AreEqual(0.1428, probs[CardType.Red], 0.0001);
            Assert.AreEqual(0.1428, probs[CardType.Yellow], 0.0001);
            Assert.AreEqual(0.1428, probs[CardType.Sun], 0.0001);

            var card = deck.Draw(1)[0];
            var newProbs = deck.Probabilities();

            foreach(var cardType in new []
                {
                    CardType.Blue,
                    CardType.Green,
                    CardType.Orange,
                    CardType.Purple,
                    CardType.Red,
                    CardType.Yellow,
                    CardType.Sun,
                })
            {
                if (cardType == card)
                {
                    Assert.AreEqual(0d, newProbs[cardType], 0.0001);
                }
                else
                {
                    Assert.AreEqual(0.1666, newProbs[cardType], 0.0001);
                }
            }
        }

        [TestMethod]
        public void ShouldDrawForcedCardAndReflectChangesInDeck()
        {
            var deck = InitializeDeck(1, 1);

            var probs = deck.Probabilities();

            Assert.AreEqual(0.1428, probs[CardType.Blue], 0.0001);
            Assert.AreEqual(0.1428, probs[CardType.Green], 0.0001);
            Assert.AreEqual(0.1428, probs[CardType.Orange], 0.0001);
            Assert.AreEqual(0.1428, probs[CardType.Purple], 0.0001);
            Assert.AreEqual(0.1428, probs[CardType.Red], 0.0001);
            Assert.AreEqual(0.1428, probs[CardType.Yellow], 0.0001);
            Assert.AreEqual(0.1428, probs[CardType.Sun], 0.0001);

            var cardProb = deck.DrawForcedCard(CardType.Blue);
            Assert.AreEqual(CardType.Blue, cardProb.Item1);
            Assert.AreEqual(0.1428, cardProb.Item2, 0.0001);

            var probsAfterForce = deck.Probabilities();

            Assert.AreEqual(0d, probsAfterForce[CardType.Blue], 0.0001);
            Assert.AreEqual(0.1666, probsAfterForce[CardType.Green], 0.0001);
            Assert.AreEqual(0.1666, probsAfterForce[CardType.Orange], 0.0001);
            Assert.AreEqual(0.1666, probsAfterForce[CardType.Purple], 0.0001);
            Assert.AreEqual(0.1666, probsAfterForce[CardType.Red], 0.0001);
            Assert.AreEqual(0.1666, probsAfterForce[CardType.Yellow], 0.0001);
            Assert.AreEqual(0.1666, probsAfterForce[CardType.Sun], 0.0001);
        }
    }
}
