using System.Collections.Generic;
using System;

namespace GameEngine.Players
{
    public class RandomPlayer : Player
    {
        private static readonly Random Random = new Random();
        
        public override Play FormulatePlay(GameBoard board)
        {
            return new Play(
                Hand.RandomCard,
                board.Owls.TrailingOwl
            );
        }
    }
}
