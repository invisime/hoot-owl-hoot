namespace GameEngine.Players
{
    public class LeastRecentCardPlayer : Player
    {
        public override CardType SelectCardToPlay(GameBoard board)
        {
            return Hand[0];
        }
    }
}
