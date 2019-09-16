using GameEngine.Players;
using System;

namespace GameEngine
{
    public class GameEngine
    {
        public void RunGame()
        {
            var player = new LeastRecentCardPlayer();
            var deck = new Deck(10);
            var state = new GameState(player, deck, 10);
            state.StartGame();
            int numberOfTurns = 0;
            for (; ; numberOfTurns++)
            {
                if (state.GameIsLost())
                {
                    Console.WriteLine("Game is lost!");
                    break;
                }

                if (state.GameIsWon())
                {
                    Console.WriteLine("Game is won!");
                    break;
                }

                state.TakeTurn();
            }
            Console.WriteLine("Game took {0} turns!", numberOfTurns);
        }
    }
}
