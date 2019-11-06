using System;

namespace GameEngine.Players
{
    public class RandomPlayer : Player
    {
        private CardType RandomCard
        {
            get { return Hand[SeededRandom.Next(0, Hand.Count)]; }
        }

        public override Play FormulatePlay(GameBoard board)
        {
            return new Play(
                RandomCard,
                board.Owls.TrailingOwl
            );
        }
    }
}
