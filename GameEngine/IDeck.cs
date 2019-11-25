namespace GameEngine
{
    public interface IDeck
    {
        CardType[] SampleAll();
        CardType[] Sample(int numberDesired);
        CardType[] Draw(int numberDesired);
        IDeck Clone();
        bool Equals(object o);
        int GetHashCode();
    }
}
