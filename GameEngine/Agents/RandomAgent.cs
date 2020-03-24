namespace GameEngine.Agents
{
    public class RandomAgent : IAgent
    {
        public Play FormulatePlay(GameState state)
        {
            return new Play(
                state.CurrentPlayerHand.RandomCard,
                state.Board.Owls.TrailingOwl
            );
        }
    }
}
