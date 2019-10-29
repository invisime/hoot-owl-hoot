using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    // "Parliament" is the collective noun for a group of owls.
    public class Parliament
    {
        private HashSet<int> PositionsWithOwls { get; set; }

        public int Count { get; private set; }
        public int InTheNest { get; private set; }

        public IEnumerable<int> ListOfPositions { get { return PositionsWithOwls; } }
        public bool AreAllNested { get { return Count == InTheNest; } }
        public int LeadOwl { get { return PositionsWithOwls.Max(); } }
        public int TrailingOwl { get { return PositionsWithOwls.Min(); } }

        public Parliament(int numberOfOwls)
        {
            PositionsWithOwls = new HashSet<int>(
                Enumerable.Range(0, numberOfOwls)
            );
            Count = numberOfOwls;
            InTheNest = 0;
        }

        public bool Inhabit(int position)
        {
            return PositionsWithOwls.Contains(position);
        }

        public void Move(int from, int to)
        {
            TakeOff(from);
            PositionsWithOwls.Add(to);
        }

        public void Nest(int from)
        {
            TakeOff(from);
            InTheNest++;
        }

        private void TakeOff(int from)
        {
            bool owlWasPresent = PositionsWithOwls.Remove(from);
            if (!owlWasPresent)
            {
                throw new InvalidMoveException("There is no owl at position " + from);
            }
        }
    }
}
