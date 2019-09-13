using System.Collections.Generic;

namespace GameEngine
{
    public class LeastRecentCardPlayer : IPlayer
    {
        public List<CardType> Hand { get; private set; }

        public LeastRecentCardPlayer()
        {
            Hand = new List<CardType>();
        }

        public CardType Play(GameBoard board)
        {
            return Hand[0];
        }

        public void Discard(CardType card)
        {
            Hand.Remove(card);
        }

        public void AddCardsToHand(List<CardType> cards)
        {
            Hand.AddRange(cards);
        }
    }
}
