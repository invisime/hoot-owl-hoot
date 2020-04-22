using System.Collections.Generic;

using GameEngine.Agents;
using GameEngine.Heuristics;

namespace AgentComparison
{
    public interface IAgentOption 
    {
        string Description(); 
        IAgent CreateAgent(IHeuristic heuristic);
    }

    public interface IHeuristicOption
    {
        string Description();
        IAgent CreateHeuristic();
    }

    public class BeamStackAgentOption : IAgentOption
    {
        public string Description() => "Beam Stack Agent";
        public IAgent CreateAgent(IHeuristic heuristic) => new BeamStackAgent(0, heuristic); // TODO extra params?
    }

    public class Options
    {
        private readonly IReadOnlyDictionary<string, IAgentOption> _agents;
        private readonly IReadOnlyDictionary<string, IHeuristicOption> _heuristics;
        public IReadOnlyDictionary<string, IAgentOption> Agents => _agents;

        public IReadOnlyDictionary<string, IHeuristicOption> Heuristics => _heuristics;

        public Options() 
        {
            _agents = new Dictionary<string, IAgentOption>
            {
                { "BeamStack", new BeamStackAgentOption() }
            };
        }

    }
}
