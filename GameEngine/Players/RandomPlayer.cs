namespace GameEngine.Players
{
    public class RandomPlayer : IPlayer
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
