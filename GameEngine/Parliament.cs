using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    // "Parliament" is the collective noun for a group of owls.
    public class Parliament
    {
        private HashSet<int> PositionsWithOwls { get; set; }

        public int Count { get; private set; }
        public int InTheNest { get { return Count - PositionsWithOwls.Count; } }

        public IEnumerable<int> ListOfPositions { get { return PositionsWithOwls; } }
        public bool AreAllNested { get { return Count == InTheNest; } }
        public int LeadOwl { get { return PositionsWithOwls.Max(); } }
        public int TrailingOwl { get { return PositionsWithOwls.Min(); } }

        private Parliament() { }

        public Parliament(int numberOfOwls)
        {
            PositionsWithOwls = new HashSet<int>(
                Enumerable.Range(0, numberOfOwls)
            );
            Count = numberOfOwls;
        }

        public override bool Equals(object o)
        {
            var other = o as Parliament;
            return other != null
                && InTheNest == other.InTheNest
                && PositionsWithOwls.SetEquals(other.PositionsWithOwls);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public Parliament Clone()
        {
            return new Parliament()
            {
                PositionsWithOwls = new HashSet<int>(PositionsWithOwls),
                Count = Count
            };
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
