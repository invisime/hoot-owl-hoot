namespace GameEngine.Players
{
    public class LeastRecentCardPlayer : Player
    {
        private CardType OldestCard
        {
            get { return Hand.Cards[0]; }
        }

        public override Play FormulatePlay(GameBoard board)
        {
            return new Play(
                OldestCard,
                board.Owls.LeadOwl
            );
        }
    }
}
