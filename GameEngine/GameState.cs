namespace GameEngine
{
    public struct GameState
    {
        public GameBoard Board;
        public Deck Deck;
        public PlayerHand Hand;
        public int SunSpaces;
        public int SunCounter;
    }

    public static class GameStateExtensions
    {
        public static GameState Successor(this GameState oldState, Play play)
        {
            var state = oldState.Clone();

            if (play.Card == CardType.Sun)
            {
                state.SunCounter++;
            }
            else
            {
                state.Board.Move(play);
            }

            state.Hand.Discard(play.Card);
            state.Hand.Add(state.Deck.Draw(1));

            return state;
        }

        public static GameState Clone(this GameState state)
        {
            return new GameState
            {
                Board = state.Board.Clone(),
                Deck = state.Deck.Clone(),
                Hand = state.Hand.Clone(),
                SunCounter = state.SunCounter,
                SunSpaces = state.SunSpaces
            };
        }
    }
}
