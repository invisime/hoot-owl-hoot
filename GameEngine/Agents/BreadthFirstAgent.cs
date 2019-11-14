using System.Collections.Generic;

namespace GameEngine.Agents
{
    public class BreadthFirstAgent : TreeSearchAgent
    {
        protected override SearchNode RemoveStrategicNode(IList<SearchNode> nodes)
        {
            // Since new nodes are added to the end of the node list,
            // the first node will always be the lowest depth node.
            var node = nodes[0];
            nodes.Remove(node);
            return node;
        }
    }
}
