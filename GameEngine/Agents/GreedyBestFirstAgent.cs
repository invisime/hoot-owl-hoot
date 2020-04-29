using GameEngine.Heuristics;
using System.Collections.Generic;

namespace GameEngine.Agents
{
    public class GreedyBestFirstAgent : TreeSearchAgent
    {
        private IHeuristic Heuristic { get; }

        public GreedyBestFirstAgent(IHeuristic heuristic)
        {
            Heuristic = heuristic;
        }

        protected override SearchNode RemoveStrategicNode(IList<SearchNode> nodes)
        {
            SearchNode minValueNode = default;
            var minimumValue = int.MaxValue;
            foreach (var node in nodes)
            {
                var value = Heuristic.Evaluate(node.State);
                if (minimumValue > value)
                {
                    minimumValue = value;
                    minValueNode = node;
                }
            }
            nodes.Remove(minValueNode);
            return minValueNode;
        }
    }
}
