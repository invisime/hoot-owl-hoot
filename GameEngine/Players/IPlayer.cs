using System.Collections.Generic;

namespace GameEngine.Players
{
    public interface IPlayer
    {
        List<CardType> Hand { get; }

        void AddCardsToHand(List<CardType> cards);
        void Discard(CardType card);
        CardType SelectCardToPlay(GameBoard board);
        bool HandContainsSun();
    }
}