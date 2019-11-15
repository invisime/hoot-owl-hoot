namespace GameEngine.Agents
{
    public interface IAgent
    {
        Play FormulatePlay(GameState state);
    }
}
