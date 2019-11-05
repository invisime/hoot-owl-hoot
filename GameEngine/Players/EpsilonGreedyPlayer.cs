using System;

namespace GameEngine.Players
{
    public class EpsilonGreedyPlayer: GreedyPlayer
    {
        private static readonly Random Random = new Random();
        private double Epsilon { get; set; }

        public EpsilonGreedyPlayer(double epsilon) :
            base()
        {
            Epsilon = epsilon;
        }

        public override Play FormulatePlay(GameBoard board)
        {
            double randomSampling = Random.NextDouble();

            if (randomSampling < Epsilon)
            {
                return base.FormulatePlay(board);
            }

            return new Play(
                Hand.RandomCard,
                board.Owls.TrailingOwl
            );
        }
    }
}
