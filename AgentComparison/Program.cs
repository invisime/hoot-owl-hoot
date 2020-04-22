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
                // TODO deck option
                // TODO number of players option
                // TODO board size option
                // TODO number of games option
                return;
            }

            if(parameters.ContainsKey("agent"))
            foreach( var (key, value) in parameters )
            {
                Console.WriteLine($"{key} : {value}");
            }

            var player = Agent(parameters, options);
            int numberOfTurns = 0;
            var game = new Game(10, playerCount: 1);
            while(!game.IsOver)
            {
                game.TakeTurn(player);
                numberOfTurns++;
            }
            Console.WriteLine("Game is {0}!", game.IsLost ? "lost" : "won");
            Console.WriteLine("Game took {0} turns!", numberOfTurns);
        }

        private static IAgent Agent(IReadOnlyDictionary<string, string> parameters, Options options)
        {
            // TODO check to see if called out heuristic and agent exists
            IHeuristic heuristic = new WorstCaseNumberOfPlaysToGo();
            if (parameters.ContainsKey("heuristic"))
            {
                heuristic = options.Heuristics[parameters["heuristic"]].CreateHeuristic(parameters);
            }
            return options.Agents[parameters["agent"]].CreateAgent(heuristic, parameters);
        }

    }
}
