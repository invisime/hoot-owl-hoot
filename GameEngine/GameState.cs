
using GameEngine.Players;
using System;

namespace GameEngine
{
    public class GameState
    {
        public GameBoard Board { get; }
        public Deck Deck { get; }
        public IPlayer Player { get; }
        public int SunSpaces { get; }
        public int SunCounter { get; private set; }

        public GameState(int multiplier, IPlayer player)
            : this(GameOptions.FromMultiplier(multiplier), player) { }

        public GameState(GameOptions options, IPlayer player)
        {
            Board = new GameBoard(options.ColoredSpacesPerColor, options.Owls);
            Deck = new Deck(options.ColoredCardsPerColor, options.SunCards);
            Player = player;
            SunSpaces = options.SunSpaces;
            SunCounter = 0;
        }

        public void StartGame()
        {
            var hand = Deck.Draw(3);
            Player.AddCardsToHand(hand);
        }

        public void TakeTurn()
        {
            Console.WriteLine("Taking a turn");
            if (Player.HandContainsSun())
            {
                Console.WriteLine("Player forced to play a Sun card");
                SunCounter++;
                Player.Discard(CardType.Sun);
            }
            else
            {
                var play = Player.FormulatePlay(Board);
                Console.WriteLine("Player chose to play {0}", play);
                Board.Move(play);
                Player.Discard(play.Card);
            }
            var newCards = Deck.Draw(1);
            Player.AddCardsToHand(newCards);
            if (newCards.Length > 0)
            {
                Console.WriteLine("Player drew a {0} card", newCards[0]);
            }
            else
            {
                Console.WriteLine("Player drew no cards since the deck was empty");
            }
        }

        public bool GameIsWon()
        {
            return Board.Owls.AreAllNested;
        }

        public bool GameIsLost()
        {
            return SunCounter == SunSpaces;
        }
    }
}
