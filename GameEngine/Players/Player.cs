using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Players
{
    public abstract class Player : IPlayer
    {
        public List<CardType> Hand { get; private set; }

        public Player()
        {
            Hand = new List<CardType>();
        }

        public void AddCardsToHand(List<CardType> cards)
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

        protected int PositionOfStragglerOwl(GameBoard board)
        {
            return board.OwlPositions
                .Where(position => position < board.NestPosition)
                .Min();
        }
        protected int PositionOfTryHardOwl(GameBoard board)
        {
            return board.OwlPositions
                .Where(position => position < board.NestPosition)
                .Max();
        }
    }
}
