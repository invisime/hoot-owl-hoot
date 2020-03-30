using GameEngine.Agents;
using System.Linq;

namespace GameEngine
{
    public class Game
    {
        public GameState State { get; private set; }
        public bool IsOver { get { return State.IsOver; } }
        public bool IsWon { get { return State.IsWin; } }
        public bool IsLost { get { return State.IsLoss; } }

        public Game(int multiplier, int playerCount = 1)
        {
            State = Start(GameOptions.FromMultiplier(multiplier), playerCount);
        }

        private GameState Start(GameOptions options, int playerCount)
        {
            var deck = new DeterministicDeck(options.ColoredCardsPerColor, options.SunCards);
            return new GameState
            {
                Board = new GameBoard(options.ColoredSpacesPerColor, options.Owls),
                Deck = deck,
                Hands = Enumerable.Repeat(0, playerCount).Select( _ => new PlayerHand(deck.Draw(3)) ).ToList(),
                SunSpaces = options.SunSpaces,
                SunCounter = 0
            };
        }

        public void TakeTurn(IAgent player)
        {
            var play = State.CurrentPlayerHand.ContainsSun
                ? Play.Sun
                : player.FormulatePlay(State);
            State = State.Successor(play);
        }
    }
}
