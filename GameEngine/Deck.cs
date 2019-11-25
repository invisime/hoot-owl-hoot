using System.Collections.Generic;

namespace GameEngine
{
    public abstract class Deck : IDeck
    {
        public abstract List<CardType> Cards { get; }
        public abstract int Count { get; }
        public abstract IDeck Clone();
        public abstract CardType[] Draw(int numberDesired);
        public abstract CardType[] Sample(int numberDesired);
        public virtual CardType[] SampleAll() => Sample(Count);
        public override abstract bool Equals(object o);
        public override abstract int GetHashCode();

        protected int StartingCount(int multiplier, int? requestedSunCards = null)
        {
            return multiplier * CardTypeExtensions.OneCardOfEachColor.Length
                + StartingSunCount(multiplier, requestedSunCards);
        }

        protected int StartingSunCount(int multiplier, int? requestedSunCards = null)
        {
            return requestedSunCards ?? 7 * multiplier / 3;
        }
    }
}
