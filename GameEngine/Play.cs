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
    }
}
