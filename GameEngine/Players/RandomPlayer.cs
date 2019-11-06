namespace GameEngine.Players
{
    public class RandomPlayer : Player
    {
        public override Play FormulatePlay(GameBoard board)
        {
            return new Play(
                Hand.RandomCard,
                board.Owls.TrailingOwl
            );
        }
    }
}
