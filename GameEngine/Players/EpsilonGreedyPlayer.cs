using System;

namespace GameEngine.Players
{
    public class EpsilonGreedyPlayer: GreedyPlayer
    {
        private static readonly Random Random = new Random();
        private double Epsilon { get; }

        public EpsilonGreedyPlayer(double epsilon) :
            base()
        {
            Epsilon = epsilon;
        }

        public new Play FormulatePlay(GameState state)
        {
            double randomSampling = Random.NextDouble();

            if (randomSampling > Epsilon)
            {
                return base.FormulatePlay(state);
            }

            return new Play(
                state.Hand.RandomCard,
                state.Board.Owls.TrailingOwl
            );
        }
    }
}
