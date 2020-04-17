using System;
using System.Linq;
using System.Collections.Generic;
using GameEngine.Heuristics;

namespace GameEngine.Agents
{
    public class BeamStackAgent : IAgent
    {
        private readonly int _width;
        private readonly IHeuristic _heuristic;

        public BeamStackAgent(int width, IHeuristic heuristic)
        {
            _width = width;
            _heuristic = heuristic;
        }

        public Play FormulatePlay(GameState state)
        {
            var solution = BeamStack(new RootNode(state), int.MaxValue).ToList();

            if (solution.Count == 0)
            {
                throw new NoMoveFoundException();
            }

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

            if (optimalSolution == null)
            {
                throw new NoMoveFoundException();
            }

            return optimalSolution.Solution();
        }

        private SearchNode Search(Stack<MinMax> beamStack, SearchNode start, int upperBound)
        {
            var currentLayer = new List<SearchNode> { start };
            var nextLayer = new List<SearchNode>();

            SearchNode bestGoal = null;

            while(currentLayer.Count != 0 || nextLayer.Count != 0)
            {
                while(currentLayer.Count != 0)
                {
                    var node = FindCheapest(currentLayer);
                    currentLayer.Remove(node);
                    if (node.State.IsOver)
                    {
                        upperBound = _heuristic.Evaluate(node.State);
                        bestGoal = node;
                    }
                    else
                    {
                        nextLayer.AddRange(GetSuccessorsInRange(node, beamStack.Peek()));
                        if (nextLayer.Count > _width)
                        {
                            nextLayer = Prune(nextLayer, beamStack.Peek());
                        }
                    }
                }
                currentLayer = nextLayer;
                nextLayer = new List<SearchNode>();
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

