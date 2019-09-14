using System;

namespace GameEngine.Players
{
    public class RandomPlayer : Player
    {
        private static readonly Random Random = new Random();

        public override CardType Play(GameBoard board)
        {
            int index = Random.Next(0, Hand.Count);
            return Hand[index];
        }
    }
}
