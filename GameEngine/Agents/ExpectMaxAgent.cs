using System;
using GameEngine.Heuristics;

namespace GameEngine.Agents
{
    public class ExpectMaxAgent : IAgent
    {
        private readonly int _depth;
        private readonly IHeuristic _heuristic;
        public ExpectMaxAgent(int depth, IHeuristic heuristic)
        {
            _depth = depth;
            _heuristic = heuristic;
        }

        public Play FormulatePlay(GameState state)
        {
            var root = new RootNode(state);
            var max = int.MinValue;
            Play choice = null;
            foreach( var node in root.Expand() )
            {
                var currentValue = ExpectMax(node, _depth);
                if ( max < currentValue )
                {
                    max = currentValue;
                    choice = node.Action;
                }
            }
            
            if ( choice == null )
            {
                throw new NoMoveFoundException("Could not determine any play");
            }

            return choice;
        }

        private int ExpectMax(SearchNode node, int depth) 
        {
            if( depth == 0 || node.State.IsOver )
            {
                return -1 * _heuristic.Evaluate(node.State);
            }

            switch(node)
            {
                case ChanceNode cn:
                    var chance = 0d;
                    foreach( var child in cn.ExpandDraw() )
                    {
                        if(child.Item2 != 0d)
                        {
                            chance = chance + (child.Item2 * ExpectMax(child.Item1, depth - 1));
                        }
                    }
                    return (int)chance;
                case SearchNode sn:
                    var max = int.MinValue;
                    foreach( var child in sn.Expand() )
                    {
                        max = Math.Max(max, ExpectMax(child, depth - 1));
                    }
                    return max;
                default:
                    throw new Exception("TODO");
            }

        }
    }
}
