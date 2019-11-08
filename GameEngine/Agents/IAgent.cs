namespace GameEngine.Players
{
    public interface IAgent
    {
        Play FormulatePlay(GameState state);
    }
}
