using GameEngine.Heuristics;
using System.Collections.Generic;

namespace GameEngine.Agents
{
    public class AStarAgent<T> : TreeSearchAgent where T : IHeuristic, new()
    {
        private IHeuristic Heuristic { get; }

        public AStarAgent() : this(new T()) { }

        private AStarAgent(IHeuristic heuristic)
        {
            Heuristic = heuristic;
        }

        protected override SearchNode RemoveStrategicNode(IList<SearchNode> nodes)
        {
            SearchNode minValueNode = default;
            var minimumValue = int.MaxValue;
            foreach (var node in nodes)
            {
                var value = node.PathCost + Heuristic.Evaluate(node.State);
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
