using System;
using System.Linq;
using System.Collections.Generic;
using GameEngine;
using GameEngine.Agents;
using GameEngine.Heuristics;

namespace AgentComparison
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var options = new Options();
            var parser = new ParamParser();
            var parameters = parser.Parse(string.Join(' ', args));

            if (!parameters.ContainsKey("agent"))
            {
                Console.WriteLine("Heuristics:");
                foreach( var heuristic in options.Heuristics.Select(h => $"{h.Key}: {h.Value.Description()}"))
                {
                    Console.WriteLine(heuristic);
                }
                Console.WriteLine("\nAgents:");
                foreach( var agent in options.Agents.Select(a => $"{a.Key}: {a.Value.Description()}"))
                {
                    Console.WriteLine(agent);
                }
                Console.WriteLine(@"

Optional Configurations: --deck=[deterministic|stochastic]
                         --players=<integer>
                         --boardSize=<integer>
                         --numberOfGames=<integer>");
                return;
            }

            var boardSize = 1;
            if (parameters.ContainsKey("boardSize"))
            {
                boardSize = int.Parse(parameters["boardSize"]);
            }

            var playerCount = 1;
            if (parameters.ContainsKey("players"))
            {
                playerCount = int.Parse(parameters["players"]);
            }

            var deckType = "deterministic";
            if (parameters.ContainsKey("deck") && parameters["deck"] == "stochastic")
            {
                deckType = "stochastic";
            }

            var numberOfGames = 1;
            var totalGames = 1;
            if(parameters.ContainsKey("numberOfGames"))
            {
                numberOfGames = int.Parse(parameters["numberOfGames"]);
                totalGames = numberOfGames;
            }

            var totalWinTime = 0d;
            var totalLoseTime = 0d;
            var totalCrashTime = 0d; 
            var win = 0;
            var lose = 0;
            var crash = 0;
            var totalNumberOfWinTurns = 0;
            var totalNumberOfLoseTurns = 0;
            var totalNumberOfCrashTurns = 0;
            while(numberOfGames > 0)
            {
                var start = DateTime.Now;

                var player = Agent(parameters, options);
                int numberOfTurns = 0;
                var game = new Game(boardSize, playerCount: playerCount, deckType: deckType);
                try
                {
                    while(!game.IsOver)
                    {
                        game.TakeTurn(player);
                        numberOfTurns++;
                    }
                    var time = DateTime.Now - start;
                    Console.WriteLine("Game is {0}!", game.IsLost ? "lost" : "won");
                    Console.WriteLine($"Game took {numberOfTurns} turns!");
                    Console.WriteLine($"Game took {time.TotalSeconds} seconds to run");
                    if (game.IsLost)
                    {
                        totalLoseTime += time.TotalSeconds;
                        lose++;
                        totalNumberOfLoseTurns += numberOfTurns;
                    }
                    else
                    {
                        totalWinTime += time.TotalSeconds; 
                        win++;
                        totalNumberOfWinTurns += numberOfTurns;
                    }
                }
                catch
                {
                    var time = DateTime.Now - start;
                    totalCrashTime += time.TotalSeconds;
                    totalNumberOfCrashTurns += numberOfTurns;
                    Console.WriteLine("The Game crashed!");
                    Console.WriteLine($"Game crashed after {numberOfTurns} turns!");
                    Console.WriteLine($"Game took {time.TotalSeconds} seconds to crash");
                    crash++;
                }
                Console.WriteLine();
                numberOfGames--;
            }
            Console.WriteLine("=========================================");

            Console.WriteLine($"Wins {win}/{totalGames}");
            if (win != 0)
            {
                Console.WriteLine($"Ave turn per win: {1.0 * totalNumberOfWinTurns / win}");
                Console.WriteLine($"Ave time per win: {totalWinTime / win} seconds");
            }
            Console.WriteLine();
            Console.WriteLine($"Lose {lose}/{totalGames}");
            if (lose != 0)
            {
                Console.WriteLine($"Ave turn per loss: {1.0 * totalNumberOfLoseTurns / lose}");
                Console.WriteLine($"Ave time per loss: {totalLoseTime / lose} seconds");
            }
            Console.WriteLine();
            Console.WriteLine($"Crash {crash}/{totalGames}");
            if (crash != 0)
            {
                Console.WriteLine($"Ave turn per crash {1.0 * totalNumberOfCrashTurns / crash}");
                Console.WriteLine($"Ave time per crash: {totalCrashTime / crash} seconds");
            }

        }

        private static IAgent Agent(IReadOnlyDictionary<string, string> parameters, Options options)
        {
            IHeuristic heuristic = new WorstCaseNumberOfPlaysToGo();
            if (parameters.ContainsKey("heuristic"))
            {
                heuristic = options.Heuristics[parameters["heuristic"]].CreateHeuristic(parameters);
            }
            return options.Agents[parameters["agent"]].CreateAgent(heuristic, parameters);
        }

    }
}
