
using GameEngine.Players;

namespace GameEngine
{
    public class GameState
    {
        public GameBoard Board { get; private set; }
        public Deck Deck { get; private set; }
        public IPlayer Player { get; private set; }

        public GameState(IPlayer player)
        {
            Board = new GameBoard();
            Deck = new Deck();
            var hand = Deck.Draw(3);
            Player = player;
            Player.AddCardsToHand(hand);
        }

        public void TakeTurn()
        {
            var cardToPlay = Player.SelectCardToPlay(Board);
            Board.Move(cardToPlay);
            Player.Discard(cardToPlay);
            var newCards = Deck.Draw(1);
            Player.AddCardsToHand(newCards);
        }
    }
}
