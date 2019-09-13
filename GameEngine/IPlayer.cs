using System.Collections.Generic;

namespace GameEngine
{
    public interface IPlayer
    {
        List<CardType> Hand { get; }

        void AddCardsToHand(List<CardType> cards);
        void Discard(CardType card);
        CardType Play(GameBoard board);
    }
}