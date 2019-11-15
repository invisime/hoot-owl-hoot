using System.Collections.Generic;

namespace GameEngine
{
    public class Play
    {
        public static Play Sun = new Play(CardType.Sun, -1);

        public CardType Card { get; }
        public int Position { get; }
        public int StepCost { get { return 1; } }

        public Play(CardType card, int position)
        {
            Card = card;
            Position = position;
        }

        public override string ToString()
        {
            return Card == CardType.Sun
                ? "a Sun card"
                : string.Format("a {0} card on the owl at position {1}", Card, Position);
        }

        public override bool Equals(object o)
        {
            var other = o as Play;
            return other != null
                && Card == other.Card
                && Position == other.Position;
        }

        public override int GetHashCode()
        {
            var hashCode = 554855297;
            unchecked
            {
                hashCode = hashCode * -1521134295 + Card.GetHashCode();
                hashCode = hashCode * -1521134295 + Position.GetHashCode();
            }
            return hashCode;
        }

        public static bool operator ==(Play left, Play right)
        {
            return EqualityComparer<Play>.Default.Equals(left, right);
        }

        public static bool operator !=(Play left, Play right)
        {
            return !(left == right);
        }
    }
}
