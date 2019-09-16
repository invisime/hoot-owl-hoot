
using GameEngine.Players;

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
            if (Player.HandContainsSun())
            {
                SunCounter++;
                Player.Discard(CardType.Sun);
            }
            else
            {
                var cardToPlay = Player.SelectCardToPlay(Board);
                Board.Move(cardToPlay);
                Player.Discard(cardToPlay);
            }
            var newCards = Deck.Draw(1);
            Player.AddCardsToHand(newCards);
        }

        public bool GameIsWon()
        {
            return Board.OwlPosition == Board.Board.Count - 1;
        }

        public bool GameIsLost()
        {
            return SunCounter == GameSizeMultiplier;
        }
    }
}
