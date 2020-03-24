using System.Collections.Generic;
using GameEngine.Agents;

namespace GameEngine
{
    public class Game
    {
        public GameState State { get; private set; }
        public bool IsOver { get { return State.IsOver; } }
        public bool IsWon { get { return State.IsWin; } }
        public bool IsLost { get { return State.IsLoss; } }

        public Game(int multiplier)
        {
            State = Start(GameOptions.FromMultiplier(multiplier));
        }

        private GameState Start(GameOptions options)
        {
            var deck = new DeterministicDeck(options.ColoredCardsPerColor, options.SunCards);
            return new GameState
            {
                Board = new GameBoard(options.ColoredSpacesPerColor, options.Owls),
                Deck = deck,
                Hands = new List<PlayerHand> { new PlayerHand(deck.Draw(3)) } ,
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
