using System.Collections.Generic;

namespace GameEngine.Agents
{
    public abstract class TreeSearchAgent : IAgent
    {
        private Queue<SearchNode> solution;

        public Play FormulatePlay(GameState state)
        {
            solution = solution ?? TreeSearch(state);
            var nextNode = solution.Dequeue();
            if (nextNode.Parent.State != state)
            {
                throw new StateMismatchException();
            }
            return nextNode.Action;
        }

        private Queue<SearchNode> TreeSearch(GameState initialState)
        {
            var frontierNodes = new List<SearchNode> { new RootNode(initialState) };
            while (true)
            {
                if (frontierNodes.Count == 0)
                {
                    throw new NoSolutionFoundException();
                }
                var node = RemoveStrategicNode(frontierNodes);
                if (node.State.IsWin)
                {
                    return new Queue<SearchNode>(node.Solution());
                }
                frontierNodes.AddRange(node.Expand());
            }
        }

        protected abstract SearchNode RemoveStrategicNode(IList<SearchNode> nodes);
    }
}