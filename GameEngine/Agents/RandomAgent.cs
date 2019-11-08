namespace GameEngine.Players
{
    public class RandomAgent : IAgent
    {
        public Play FormulatePlay(GameState state)
        {
            return new Play(
                state.Hand.RandomCard,
                state.Board.Owls.TrailingOwl
            );
        }
    }
}
