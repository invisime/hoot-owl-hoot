namespace GameEngine.Players
{
    public class LeastRecentCardPlayer : Player
    {
        private CardType OldestCard
        {
            get { return Hand[0]; }
        }

        public override Play FormulatePlay(GameBoard board)
        {
            return new Play(
                OldestCard,
                PositionOfTryHardOwl(board)
            );
        }
    }
}
