using GameEngine.Players;

namespace GameEngine
{
    public static class Game
    {
        public static GameState Start(int multiplier)
        {
            return Start(GameOptions.FromMultiplier(multiplier));
        }

        public static GameState Start(GameOptions options)
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

        public static bool IsOver(GameState state)
        {
            return IsWon(state) || IsLost(state);
        }

        public static bool IsWon(GameState state)
        {
            return state.Board.Owls.AreAllNested;
        }

        public static bool IsLost(GameState state)
        {
            return state.SunCounter == state.SunSpaces;
        }

        public static GameState TakeTurn(GameState state, IPlayer player)
        {
            var play = state.Hand.ContainsSun
                ? Play.Sun
                : player.FormulatePlay(state);
            var newState = Successor(state, play);
            return newState;
        }

        public static GameState Successor(GameState state, Play play)
        {
            var newBoard = state.Board.Clone();
            var newDeck = state.Deck.Clone();
            var newHand = state.Hand.Clone();
            var newSunCounter = state.SunCounter;

            if (play.Card == CardType.Sun)
            {
                newSunCounter++;
            } else
            {
                newBoard.Move(play);
            }

            newHand.Discard(play.Card);
            newHand.Add(newDeck.Draw(1));

            return new GameState
            {
                Board = newBoard,
                Deck = newDeck,
                Hand = newHand,
                SunCounter = newSunCounter,
                SunSpaces = state.SunSpaces
            };
        }
    }
}
