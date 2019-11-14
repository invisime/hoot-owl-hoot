namespace GameEngine.Heuristics
{
    public interface IHeuristic
    {
        int Evaluate(GameState state);
    }
}