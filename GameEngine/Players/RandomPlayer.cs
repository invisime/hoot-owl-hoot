using System;

namespace GameEngine.Players
{
    public class RandomPlayer : Player
    {
        private static readonly Random Random = new Random();

        private CardType RandomCard
        {
            get { return Hand[Random.Next(0, Hand.Count)]; }
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
