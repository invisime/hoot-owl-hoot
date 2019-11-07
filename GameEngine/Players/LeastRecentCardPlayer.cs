namespace GameEngine.Players
{
    public class LeastRecentCardPlayer : IPlayer
    {
        public Play FormulatePlay(GameState state)
        {
            return new Play(
                state.Hand.OldestCard,
                state.Board.Owls.LeadOwl
            );
        }
    }
}
