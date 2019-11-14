namespace GameEngine.Heuristics
{
    public class BestCaseNumberOfPlaysToGo : IHeuristic
    {
        public int Evaluate(GameState state)
        {
            return state.Board.Owls.Count;
        }
    }
}
