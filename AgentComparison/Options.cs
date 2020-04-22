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

    public class BestCaseNumberOfPlaysToGoHeuristicOption : IHeuristicOption
    {
        public string Description() => "Usage: AgentComparison --agent=<agent-name> --heuristic=BestNumberOfPlays";
        public IHeuristic CreateHeuristic(IReadOnlyDictionary<string, string> parameters) => new BestCaseNumberOfPlaysToGo();
    }

    public class Options
    {
        public IReadOnlyDictionary<string, IAgentOption> Agents { get; private set; } 

        public IReadOnlyDictionary<string, IHeuristicOption> Heuristics { get; private set; }

        public Options() 
        {
            Agents = new Dictionary<string, IAgentOption>
            {
                { "BeamStack", new BeamStackAgentOption() }
            };

            Heuristics = new Dictionary<string, IHeuristicOption>
            {
                { "BestNumberOfPlays", new BestCaseNumberOfPlaysToGoHeuristicOption() }
            };
        }

    }
}
