using System.Collections.Generic;

namespace GameEngine.Players
{
    public interface IPlayer
    {
        PlayerHand Hand { get; }

        Play FormulatePlay(GameBoard board);
    }
}