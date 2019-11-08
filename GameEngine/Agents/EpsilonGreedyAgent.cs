using System;

namespace GameEngine.Players
{
    public class EpsilonGreedyAgent: GreedyAgent
    {
        private static readonly Random Random = new Random();
        private double Epsilon { get; }

        public EpsilonGreedyAgent(double epsilon) :
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
