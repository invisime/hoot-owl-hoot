using System.Collections.Generic;

using GameEngine.Agents;
using GameEngine.Heuristics;

namespace AgentComparison
{
    public interface IAgentOption 
    {
        string Description(); 
        IAgent CreateAgent(IHeuristic heuristic, IReadOnlyDictionary<string, string> parameters);
    }

    public interface IHeuristicOption
    {
        string Description();
        IHeuristic CreateHeuristic(IReadOnlyDictionary<string, string> parameters);
    }

    public class BeamStackAgentOption : IAgentOption
    {
        public string Description() => "Usage: AgentComparison --agent=BeamStack --width=<integer> --heuristic=<heuristic-name>";
        public IAgent CreateAgent(IHeuristic heuristic, IReadOnlyDictionary<string, string> parameters) 
            => new BeamStackAgent(int.Parse(parameters["width"]), heuristic); 
    }

    public class BeamAgentOption : IAgentOption
    {
        public string Description() => "Usage: AgentComparison --agent=Beam --width=<integer> --heuristic=<heuristic-name>";
        public IAgent CreateAgent(IHeuristic heuristic, IReadOnlyDictionary<string, string> parameters) 
            => new BeamAgent(int.Parse(parameters["width"]), heuristic); 
    }

    public class RandomAgentOption : IAgentOption
    {
        public string Description() => "Usage: AgentComparison --agent=Random";
        public IAgent CreateAgent(IHeuristic heuristic, IReadOnlyDictionary<string, string> parameters) 
            => new RandomAgent(); 
    }

    public class MaxMaxAgentOption : IAgentOption
    {
        public string Description() => "Usage: AgentComparison --agent=MaxMax --depth=<integer> --heuristic=<heuristic-name>";
        public IAgent CreateAgent(IHeuristic heuristic, IReadOnlyDictionary<string, string> parameters) 
            => new MaxMaxAgent(int.Parse(parameters["depth"]), heuristic); 
    }

    public class LeastRecentCardAgentOption : IAgentOption
    {
        public string Description() => "Usage: AgentComparison --agent=LeastRecentCard";
        public IAgent CreateAgent(IHeuristic heuristic, IReadOnlyDictionary<string, string> parameters) 
            => new LeastRecentCardAgent(); 
    }

    // TODO GreedyBestFirstAgent after refactor

    public class GreedyAgentOption : IAgentOption
    {
        public string Description() => "Usage: AgentComparison --agent=Greedy";
        public IAgent CreateAgent(IHeuristic heuristic, IReadOnlyDictionary<string, string> parameters) 
            => new GreedyAgent(); 
    }

    public class ExpectMaxAgentOption : IAgentOption
    {
        public string Description() => "Usage: AgentComparison --agent=ExpectMax --depth=<integer> --heuristic=<heuristic-name>";
        public IAgent CreateAgent(IHeuristic heuristic, IReadOnlyDictionary<string, string> parameters) 
            => new ExpectMaxAgent(int.Parse(parameters["depth"]), heuristic); 
    }

    public class EpsilonGreedyAgentOption : IAgentOption 
    { 
        public string Description() => "Usage: AgentComparison --agent=EpsilonGreedy --epsilon=<double>";
        public IAgent CreateAgent(IHeuristic heuristic, IReadOnlyDictionary<string, string> parameters) 
            => new EpsilonGreedyAgent(double.Parse(parameters["epsilon"])); 
    }

    public class BreadthFirstAgentOption : IAgentOption
    {
        public string Description() => "Usage: AgentComparison --agent=BreadthFirst";
        public IAgent CreateAgent(IHeuristic heuristic, IReadOnlyDictionary<string, string> parameters) 
            => new BreadthFirstAgent(); 
    }

    // TODO AStarAgent after refactor

    public class BestCaseNumberOfPlaysToGoHeuristicOption : IHeuristicOption
    {
        public string Description() => "Usage: AgentComparison --agent=<agent-name> --heuristic=BestNumberOfPlays";
        public IHeuristic CreateHeuristic(IReadOnlyDictionary<string, string> parameters) => new BestCaseNumberOfPlaysToGo();
    }
    public class WorstCaseNumberOfPlaysToGoHeuristicOption : IHeuristicOption
    {
        public string Description() => "Usage: AgentComparison --agent=<agent-name> --heuristic=WorstNumberOfPlays";
        public IHeuristic CreateHeuristic(IReadOnlyDictionary<string, string> parameters) => new WorstCaseNumberOfPlaysToGo();
    }

    public class Options
    {
        public IReadOnlyDictionary<string, IAgentOption> Agents { get; private set; } 

        public IReadOnlyDictionary<string, IHeuristicOption> Heuristics { get; private set; }

        public Options() 
        {
            Agents = new Dictionary<string, IAgentOption>
            {
                { "BeamStack", new BeamStackAgentOption() },
                { "Beam", new BeamAgentOption() },
                { "Random", new RandomAgentOption() },
                { "MaxMax", new MaxMaxAgentOption() },
                { "LeastRecentCard", new LeastRecentCardAgentOption() },
                { "Greedy", new GreedyAgentOption() },
                { "ExpectMax", new ExpectMaxAgentOption() },
                { "EpsilonGreedy", new EpsilonGreedyAgentOption() },
                { "BreadthFirst", new BreadthFirstAgentOption() },
            };

            Heuristics = new Dictionary<string, IHeuristicOption>
            {
                { "BestNumberOfPlays", new BestCaseNumberOfPlaysToGoHeuristicOption() },
                { "WorstNumberOfPlays", new WorstCaseNumberOfPlaysToGoHeuristicOption() }
            };
        }

    }
}
