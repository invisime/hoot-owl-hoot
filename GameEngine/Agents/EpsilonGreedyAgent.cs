using System;

namespace GameEngine.Agents
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
                state.CurrentPlayerHand.RandomCard,
                state.Board.Owls.TrailingOwl
            );
        }
    }
}
