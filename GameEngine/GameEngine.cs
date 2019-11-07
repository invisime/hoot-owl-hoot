using GameEngine.Players;
using System;

namespace GameEngine
{
    public class GameEngine
    {
        public void RunGame()
        {
            var player = new LeastRecentCardPlayer();
            int numberOfTurns = 0;
            var state = Game.Start(10);
            while(!Game.IsOver(state))
            {
                state = Game.TakeTurn(state, player);
                numberOfTurns++;
            }
            Console.WriteLine("Game is {0}!", Game.IsLost(state) ? "lost" : "won");
            Console.WriteLine("Game took {0} turns!", numberOfTurns);
        }
    }
}
