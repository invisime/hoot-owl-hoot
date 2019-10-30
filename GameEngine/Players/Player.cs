using System.Collections.Generic;

namespace GameEngine.Players
{
    public abstract class Player : IPlayer
    {
        public List<CardType> Hand { get; private set; }

        public Player()
        {
            Hand = new List<CardType>();
        }

        public void AddCardsToHand(params CardType[] cards)
        {
            Hand.AddRange(cards);
        }

        public void Discard(CardType card)
        {
            Hand.Remove(card);
        }

        public bool HandContainsSun()
        {
            return Hand.Contains(CardType.Sun);
        }

        public abstract Play FormulatePlay(GameBoard board);
    }
}
