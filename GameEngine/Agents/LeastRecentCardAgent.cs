namespace GameEngine.Agents
{
    public class LeastRecentCardAgent : IAgent
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
