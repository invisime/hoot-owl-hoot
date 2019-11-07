namespace GameEngine
{
    public class Play
    {
        public CardType Card { get; }
        public int Position { get; }
        public static Play Sun = new Play(CardType.Sun, -1);

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
    }
}
