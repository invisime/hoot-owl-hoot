using GameEngine.Players;
using System;

namespace GameEngine
{
    public class GameState
    {
        public GameBoard Board { get; private set; }
        public Deck Deck { get; private set; }
        public IPlayer Player { get; private set; }
        public int SunCounter { get; private set; }
        public int GameSizeMultiplier { get; }
        
        public GameState(IPlayer player, Deck deck, int gameSizeMultiplier)
        {
            Board = new GameBoard(gameSizeMultiplier);
            Deck = deck;
            Player = player;
            SunCounter = 0;
            GameSizeMultiplier = gameSizeMultiplier;
        }

        public void StartGame()
        {
            var cards = Deck.Draw(3);
            Player.Hand.Add(cards);
        }

        public void TakeTurn()
        {
            Console.WriteLine("Taking a turn");
            if (Player.Hand.ContainsSun)
            {
                Console.WriteLine("Player forced to play a Sun card");
                SunCounter++;
                Player.Hand.Discard(CardType.Sun);
            }
            else
            {
                var play = Player.FormulatePlay(Board);
                Console.WriteLine("Player chose to play {0}", play);
                Board.Move(play);
                Player.Hand.Discard(play.Card);
            }
            var newCards = Deck.Draw(1);
            Player.Hand.Add(newCards);
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
            return SunCounter == GameSizeMultiplier;
        }
    }
}
