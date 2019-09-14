namespace GameEngine.Players
{
    public class LeastRecentCardPlayer : Player
    {
        public override CardType Play(GameBoard board)
        {
            return Hand[0];
        }
    }
}
