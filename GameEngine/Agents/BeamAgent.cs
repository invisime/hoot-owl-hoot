using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Heuristics;

namespace GameEngine.Agents
{
    public class BeamAgent : IAgent
    {
        private int _width;
        private IHeuristic _heuristic;

        public BeamAgent(int width, IHeuristic heuristic)
        {
            _width = width;
            _heuristic = heuristic; 
        }

        public Play FormulatePlay(GameState state)
        {
            var root = new RootNode(state);

            var rootBest = FindBest(root.Expand().ToList());

            var results = new List<Best> { rootBest };
            
            while(results.Count != 0)
            {
                var target = results[0];
                results.RemoveAt(0);

                foreach(var node in target.Nodes())
                {
                    if (node.State.IsOver)
                    {
                        return node.Solution().ToList()[0].Action;
                    }
                    var best = FindBest(node.Expand().ToList());
                    results.Add(best);
                }
            }
            throw new NoMoveFoundException();
        }

        private Best FindBest(List<SearchNode> nodes)
        {
            var best = new Best(_width);
            foreach(var node in nodes)
            {
                var value = _heuristic.Evaluate(node.State);
                best.Add(node, value);
            }
            return best;
        }
        
        private class Best
        {
            private List<Tuple<SearchNode, int>> _nodes;
            private int _maxCount;
            private int _worstCost;
            public Best(int maxCount)
            {
                _maxCount = maxCount;
                _nodes = new List<Tuple<SearchNode, int>>();
                _worstCost = int.MinValue;
            }

            public void Add(SearchNode node, int value)
            {
                if (_nodes.Count < _maxCount)
                {
                    _nodes.Add(Tuple.Create(node, value));
                    if (value > _worstCost)
                    {
                        _worstCost = value; 
                    }
                }
                else if (value < _worstCost)
                {
                    var i = _nodes.FindIndex(n => n.Item2 == _worstCost);
                    _nodes.RemoveAt(i);
                    _nodes.Add(Tuple.Create(node, value));
                    _worstCost = _nodes.Max(n => n.Item2);
                }
            }

            public List<SearchNode> Nodes() => _nodes.Select(n => n.Item1).ToList();
        }
    }
}
