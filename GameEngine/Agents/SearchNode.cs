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

        public IEnumerable<SearchNode> Expand()
        {
            if (State.IsOver)
            {
                return new SearchNode[] { };
            }
            if (State.Hand.ContainsSun)
            {
                return new SearchNode[] { ChildNode(Play.Sun) };
            }
            return State.Hand.Cards
                    .SelectMany(card => State.Board.Owls.ListOfPositions
                        .Select(owl => new Play(card, owl)))
                    .Select(ChildNode);
        }

        public IEnumerable<SearchNode> Solution()
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
    }

    public class RootNode : SearchNode
    {
        public RootNode(GameState state) : base()
        {
            State = state;
            PathCost = 0;
        }
    }
}
