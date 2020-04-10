using System;
using System.Linq;
using System.Collections.Generic;

namespace GameEngine.Agents
{
    public class SearchNode
    {
        public GameState State { get; protected set; }
        public SearchNode Parent { get; protected set; }
        public Play Action { get; protected set; }
        public int PathCost { get; protected set; }

        protected SearchNode() { }

        public SearchNode(GameState state, SearchNode parent, Play action, int pathCost)
        {
            State = state;
            Parent = parent;
            Action = action;
            PathCost = pathCost;
        }

        public virtual IEnumerable<SearchNode> Expand()
        {
            if (State.IsOver)
            {
                return new SearchNode[] { };
            }
            if (State.CurrentPlayerHand.ContainsSun)
            {
                return new SearchNode[] { ChildNode(Play.Sun) };
            }
            return State.CurrentPlayerHand.Cards
                    .SelectMany(card => State.Board.Owls.ListOfPositions
                        .Select(owl => new Play(card, owl)))
                    .Select(ChanceNode);
        }

        public virtual IEnumerable<SearchNode> Solution()
        {
            return Action == null
                ? new List<SearchNode>()
                : Parent.Solution()
                    .Concat(new[] { this });
        }

        private SearchNode ChildNode(Play play)
        {
            return new SearchNode
            {
                State = State.Successor(play), 
                Parent = this,
                Action = play,
                PathCost = PathCost + play.StepCost
            };
        }

        private SearchNode ChanceNode(Play play)
        {
            return new ChanceNode 
            {
                State = State.MakePlay(play),
                Parent = this,
                Action = play,
                PathCost = PathCost + play.StepCost
            };
        }
    }

    public class RootNode : SearchNode
    {
        public RootNode(GameState state) : base()
        {
            State = state;
            PathCost = 0;
        }
    }

    public class ChanceNode : SearchNode
    {
        public override IEnumerable<SearchNode> Expand()
        {
            var node = new SearchNode(
                State.DrawCard().SwitchPlayers(),
                Parent,
                Action,
                PathCost);

            return node.Expand();
        }

        public override IEnumerable<SearchNode> Solution()
        {
            var node = new SearchNode(
                State.DrawCard().SwitchPlayers(),
                Parent,
                Action,
                PathCost);
            return Parent.Solution().Concat(new[] { node });
        }

        public IEnumerable<Tuple<SearchNode, double>> ExpandDraw()
        {
            foreach( var card in new[] { 
                CardType.Blue, 
                CardType.Green, 
                CardType.Orange, 
                CardType.Purple, 
                CardType.Red, 
                CardType.Yellow, 
                CardType.Sun })
            {
                var newStateProb = State.DrawForcedCard(card);
                var newNode = new SearchNode(
                    newStateProb.Item1.SwitchPlayers(),
                    Parent,
                    Action,
                    PathCost);
                yield return Tuple.Create(newNode, newStateProb.Item2);
            }
        }
    }
}
