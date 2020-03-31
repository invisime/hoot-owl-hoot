using System;
using GameEngine.Heuristics;

namespace GameEngine.Agents
{
    public class MaxMaxAgent : IAgent
    {
        private readonly int _depth;
        private readonly IHeuristic _heuristic;
        public MaxMaxAgent(int depth, IHeuristic heuristic)
        {
            _depth = depth;
            _heuristic = heuristic;
        }

        public Play FormulatePlay(GameState state)
        {
            var root = new RootNode(state);
            var max = int.MinValue;
            Play choice = null;
            foreach( var node in root.Expand())
            {
                var currentValue = MaxMax(node, _depth);
                if ( max < currentValue )
                {
                    max = currentValue;
                    choice = node.Action;
                }
            }
            
            if ( choice == null )
            {
                throw new Exception("Could not determine any play");
            }

            return choice;
        }

        private int MaxMax(SearchNode node, int depth) 
        {
            if( depth == 0 || node.State.IsOver )
            {
                return _heuristic.Evaluate(node.State);
            }

            var max = int.MinValue;
            foreach( var child in node.Expand() )
            {
                max = Math.Max(max, MaxMax(child, depth - 1));
            }
            return max;
        }
    }
}
