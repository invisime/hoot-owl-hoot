using System;
using GameEngine;
using GameEngine.Agents;
using GameEngine.Heuristics;

namespace AgentComparison
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var player = new LeastRecentCardAgent();
            int numberOfTurns = 0;
            var game = new Game(10);
            while(!game.IsOver)
            {
                game.TakeTurn(player);
                numberOfTurns++;
            }
            Console.WriteLine("Game is {0}!", game.IsLost ? "lost" : "won");
            Console.WriteLine("Game took {0} turns!", numberOfTurns);
        }
    }
}
