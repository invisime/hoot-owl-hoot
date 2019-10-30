using System.Collections.Generic;

namespace GameEngine.Players
{
    public interface IPlayer
    {
        List<CardType> Hand { get; }

        void AddCardsToHand(params CardType[] cards);
        void Discard(CardType card);
        Play FormulatePlay(GameBoard board);
        bool HandContainsSun();
    }
}