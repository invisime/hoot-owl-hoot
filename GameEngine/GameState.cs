
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
                Console.WriteLine("Player chose to play a {0} card on an owl at position {1}", play.Card, play.Position);
                Board.Move(play);
                Player.Discard(play.Card);
            }
            var newCards = Deck.Draw(1);
            Console.WriteLine("Player drew a {0} card", newCards[0]);
            Player.AddCardsToHand(newCards);
        }

        public bool GameIsWon()
        {
            return Board.OwlPositions
                .TrueForAll(position => position == Board.NestPosition);
        }

        public bool GameIsLost()
        {
            return SunCounter == GameSizeMultiplier;
        }
    }
}
