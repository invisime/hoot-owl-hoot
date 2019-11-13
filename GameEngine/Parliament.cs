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

        public override bool Equals(object o)
        {
            var other = o as Parliament;
            return other != null
                && InTheNest == other.InTheNest
                && PositionsWithOwls.SetEquals(other.PositionsWithOwls);
        }

        public override int GetHashCode()
        {
            var hashCode = -1545310984;
            unchecked
            {
                hashCode = hashCode * -1521134295 + EqualityComparer<HashSet<int>>.Default.GetHashCode(PositionsWithOwls);
                hashCode = hashCode * -1521134295 + Count.GetHashCode();
            }
            return hashCode;
        }

        public static bool operator ==(Parliament left, Parliament right)
        {
            return EqualityComparer<Parliament>.Default.Equals(left, right);
        }

        public static bool operator !=(Parliament left, Parliament right)
        {
            return !(left == right);
        }

        public Parliament Clone()
        {
            return new Parliament()
            {
                PositionsWithOwls = new HashSet<int>(PositionsWithOwls),
                Count = Count
            };
        }
    }
}
