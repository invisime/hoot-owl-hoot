namespace GameEngine
{
    public interface IDeck
    {
        CardType[] Draw(int numberDesired);
        IDeck Clone();
        bool Equals(object o);
        int GetHashCode();
    }
}
