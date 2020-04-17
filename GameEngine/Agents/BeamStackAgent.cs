using System;
using System.Linq;
using System.Collections.Generic;
using GameEngine.Heuristics;

namespace GameEngine.Agents
{
    public class BeamStackAgent : IAgent
    {
        private readonly int _width;
        private readonly int _upperBound;
        private readonly IHeuristic _heuristic;

        public BeamStackAgent(int width, int upperBound, IHeuristic heuristic)
        {
            _width = width;
            _upperBound = upperBound;
            _heuristic = heuristic;
        }

        public Play FormulatePlay(GameState state)
        {
            var solution = BeamStack(new RootNode(state), _upperBound);
            return solution.First().Action;
        }

        private IEnumerable<SearchNode> BeamStack(SearchNode start, int upperBound)
        {
            var beamStack = new Stack<MinMax>();
            beamStack.Push(new MinMax { Min = 0, Max = upperBound });

            SearchNode optimalSolution = null;
            while(beamStack.Count() != 0)
            {
                var solution = Search(beamStack, start, upperBound);
                if (solution != null)
                {
                    optimalSolution = solution;
                    upperBound = _heuristic.Evaluate(solution.State);
                }
                while(beamStack.Count() != 0 && beamStack.Peek().Max >= upperBound)
                {
                    beamStack.Pop();
                }
                if(beamStack.Count() != 0)
                {
                    beamStack.Peek().Min = beamStack.Peek().Max;
                    beamStack.Peek().Max = upperBound;
                }
            }
            return optimalSolution.Solution();
        }

        private SearchNode Search(Stack<MinMax> beamStack, SearchNode start, int upperBound)
        {
            var open = new List<List<SearchNode>>();

            open.Add(new List<SearchNode> { start });
            open.Add(new List<SearchNode>());

            SearchNode bestGoal = null;

            var layer = 0;

            while(open[layer].Count != 0 || open[layer + 1].Count != 0)
            {
                while (open[layer].Count != 0)
                {
                    var node = FindCheapest(open[layer]);
                    open[layer].Remove(node);
                    if (node.State.IsOver)
                    {
                        upperBound = _heuristic.Evaluate(node.State);
                        bestGoal = node;
                    }
                    else
                    {
                        open[layer + 1].AddRange(GetSuccessorsInRange(node, beamStack.Peek()));
                        if (open[layer + 1].Count > _width)
                        {
                            open[layer + 1] = Prune(open[layer + 1], beamStack.Peek());
                        }
                    }
                }
                layer++;
                open.Add(new List<SearchNode>());
                beamStack.Push(new MinMax { Min = 0, Max = upperBound });
            }
            if(bestGoal != null)
            {
                return bestGoal;
            }
            else
            {
                return null;
            }
        }
        
        private List<SearchNode> Prune(List<SearchNode> layer, MinMax range)
        {
            var highestCost = int.MinValue;
            var nodes = new List<Tuple<SearchNode, int>>();
            var pruneCosts = new List<int>();

            foreach(var node in layer)
            {
                var value = _heuristic.Evaluate(node.State);
                if (nodes.Count < _width)
                {
                    nodes.Add(Tuple.Create(node, value));
                    if (value > highestCost)
                    {
                        highestCost = value;
                    }
                }
                else if (value < highestCost)
                {
                    var i = nodes.FindIndex(n => n.Item2 == highestCost);
                    pruneCosts.Add(nodes[i].Item2);
                    nodes.RemoveAt(i);
                    nodes.Add(Tuple.Create(node, value));
                    highestCost = nodes.Max(n => n.Item2);
                }
                else
                {
                    pruneCosts.Add(value);
                }
            }

            range.Max = pruneCosts.Min();
            return nodes.Select(node => node.Item1).ToList();
        }

        private IEnumerable<SearchNode> GetSuccessorsInRange(SearchNode node, MinMax range)
            => node.Expand()
                   .Select(n => new { Node = n, Cost = _heuristic.Evaluate(node.State) })
                   .Where(n => range.Min <= n.Cost && n.Cost < range.Max)
                   .Select(n => n.Node);

        private SearchNode FindCheapest(List<SearchNode> nodes)
            => nodes.Select(node => new { Node = node, Cost = _heuristic.Evaluate(node.State) })
                    .Aggregate((n1, n2) => n1.Cost < n2.Cost ? n1 : n2)
                    .Node;

        private class MinMax
        {
            public int Max;
            public int Min;
        }
    }
}

