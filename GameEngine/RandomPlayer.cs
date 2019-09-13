using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class RandomPlayer
    {
        private static readonly Random Random = new Random();
        public List<CardType> Hand { get; private set; }

        public RandomPlayer(List<CardType> hand)
        {
            Hand = hand;
        }

        public CardType Play(GameBoard board)
        {
            int index = Random.Next(0, Hand.Count);
            return Hand[index];
        }
    }
}
