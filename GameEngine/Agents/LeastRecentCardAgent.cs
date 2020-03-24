namespace GameEngine.Agents
{
    public class LeastRecentCardAgent : IAgent
    {
        public Play FormulatePlay(GameState state)
        {
            return new Play(
                state.CurrentPlayerHand.OldestCard,
                state.Board.Owls.LeadOwl
            );
        }
    }
}
