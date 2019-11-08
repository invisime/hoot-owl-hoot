using GameEngine.Players;

namespace GameEngine
{
    public class Game
    {
        public GameState State { get; private set; }

        public Game(int multiplier)
        {
            State = Start(GameOptions.FromMultiplier(multiplier));
        }

        private GameState Start(GameOptions options)
        {
            var deck = new Deck(options.ColoredCardsPerColor, options.SunCards);
            return new GameState
            {
                Board = new GameBoard(options.ColoredSpacesPerColor, options.Owls),
                Deck = deck,
                Hand = new PlayerHand(deck.Draw(3)),
                SunSpaces = options.SunSpaces,
                SunCounter = 0
            };
        }

        public bool IsOver
        {
            get { return IsWon || IsLost; }
        }

        public bool IsWon
        {
            get { return State.Board.Owls.AreAllNested; }
        }

        public bool IsLost
        {
            get { return State.SunCounter == State.SunSpaces; }
        }

        public void TakeTurn(IPlayer player)
        {
            var play = State.Hand.ContainsSun
                ? Play.Sun
                : player.FormulatePlay(State);
            State = State.Successor(play);
        }
    }
}
