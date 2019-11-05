using System.Collections.Generic;

namespace GameEngine.Players
{
    public abstract class Player : IPlayer
    {
        public PlayerHand Hand { get; private set; }

        protected Player()
        {
            Hand = new PlayerHand();
        }

        public abstract Play FormulatePlay(GameBoard board);
    }
}
