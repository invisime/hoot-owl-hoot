namespace GameEngine
{
    public class Play
    {
        public CardType Card { get; }
        public int Position { get; }

        public Play(CardType card, int position)
        {
            Card = card;
            Position = position;
        }

        public override string ToString()
        {
            return string.Format("a {0} card on the owl at position {1}", Card, Position);
        }
    }
}
