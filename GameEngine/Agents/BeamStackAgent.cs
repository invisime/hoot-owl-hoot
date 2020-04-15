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


/*Algorithm DCBSS(Node start, Node goal, Real U, Integer relay)
38 beam_stack ← ∅
39 beam_stack.push([0, U)) // initialize beam stack
40 π∗ ← nil // initialize optimal solution path
41 while beam_stack.top() != nil do
42     π ← search(start, goal, U, relay) // π = solution path 
43     if π != nil then
44         π∗ ← π
45         U ← π.getCost()
46     while beam_stack.top().fmax ≥ U do
47         beam_stack.pop()
48     end while
49     if beam_stack.isEmpty() then return π∗
50     beam_stack.top().fmin ← beam_stack.top().fmax
51     beam_stack.top().fmax ← U
52 end while
*/

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
                    open[layer + 1].AddRange(GetSuccessorsInRange(node, beamStack.Peek()));
                    if (open[layer + 1].Count > _width)
                    {
                        open[layer + 1] = Prune(open[layer + 1], beamStack.Peek());
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
        
/*
 Procedure pruneLayer (Integer l) // l is layer ?
1 Keep ← the best w nodes ∈ Open[l]
2 Prune ← {n | n ∈ Open[l] ∧ n ∈/ Keep}
3 beam_stack.top().fmax ← min {f(n) | n ∈ Prune}
4 for each n ∈ Prune do // inadmissible pruning 
5     Open[l] ← Open[l] \ {n}
6     delete n
7 end for
*/
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

/*
 Procedure pruneLayer (Integer l) // l is layer ?
1 Keep ← the best w nodes ∈ Open[l]
2 Prune ← {n | n ∈ Open[l] ∧ n ∈/ Keep}
3 beam_stack.top().fmax ← min {f(n) | n ∈ Prune}
4 for each n ∈ Prune do // inadmissible pruning 
5     Open[l] ← Open[l] \ {n}
6     delete n
7 end for

Function search (Node start, Node goal, Real U, Integer relay)
8  Open[0] ← {start}
9  Open[1] ← ∅
10 Closed[0] ← ∅
11 best_goal ← nil
12 l ← 0 // l = index of layer
13 while Open[l] != ∅ or Open[l +1] != ∅ do
14     while Open[l] != ∅ do
15         node ← arg minn { f(n) | n ∈ Open [l]}
16         Open[l] ← Open[l] \ {node}
17         Closed[l] ← Closed[l] ∪ {node}
18         if node is goal then
19             U ← g(node)
20             best_goal ← node
21         node.generateAdmittedSuccessors(beam_stack.top())
22         if layerSize(l + 1) > w then pruneLayer(l + 1)
23     end while
24     if 1 < l ≤ relay or l > relay + 1 then
25         for each n ∈ Closed[l − 1] do // delete previous layer
26             Closed[l − 1] ← Closed[l − 1] \ {n}
27             delete n
28         end for
29     l ← l + 1 // move on to next layer 
30     Open[l +1] ← ∅
31     Closed[l] ← ∅
32     beam_stack.push([0, U)) // new beam-stack item
33 end while
34 if best_goal != nil then // delayed solution reconstruction 
35     return solutionReconstruction(best_goal)
36 else
37     return nil

    // U = upper bound
Algorithm DCBSS(Node start, Node goal, Real U, Integer relay)
38 beam_stack ← ∅
39 beam_stack.push([0, U)) // initialize beam stack
40 π∗ ← nil // initialize optimal solution path
41 while beam_stack.top() != nil do
42     π ← search(start, goal, U, relay) // π = solution path 
43     if π != nil then
44         π∗ ← π
45         U ← π.getCost()
46     while beam_stack.top().fmax ≥ U do
47         beam_stack.pop()
48     end while
49     if beam_stack.isEmpty() then return π∗
50     beam_stack.top().fmin ← beam_stack.top().fmax
51     beam_stack.top().fmax ← U
52 end while
*/
