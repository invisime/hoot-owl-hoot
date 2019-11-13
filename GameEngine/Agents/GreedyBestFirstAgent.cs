using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Agents
{
    public class GreedyBestFirstAgent : IAgent
    {
        public Queue<SearchNode> solution;

        public Play FormulatePlay(GameState state)
        {
            solution = solution ?? GreedyBestFirstSearch(state);
            var nextNode = solution.Dequeue();
            if (nextNode.Parent.State != state)
            {
                throw new StateMismatchException();
            }
            return nextNode.Action;
        }

        private Queue<SearchNode> GreedyBestFirstSearch(GameState state)
        {
            var frontierNodes = new List<SearchNode> { new RootNode(state) };
            while (true)
            {
                if (frontierNodes.Count == 0)
                {
                    throw new NoSolutionFoundException();
                }
                var node = frontierNodes.RemoveMin(H);
                if ( H(node) == 0 )
                {
                    return new Queue<SearchNode>(node.Solution());
                }
                frontierNodes.AddRange(node.Expand());
            }
        }

        private int H(SearchNode node)
        {
            return H(node.State);
        }

        public static int H(GameState state)
        {
            return (state.Board.Owls.Count - state.Board.Owls.InTheNest)
                * state.Board.NestPosition
                - state.Board.Owls.ListOfPositions.Sum();
        }
    }
}
