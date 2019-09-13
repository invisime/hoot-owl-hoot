
namespace GameEngine
{
    public class GameState
    {
        public GameBoard Board { get; private set; }
        public Deck Deck { get; private set; }
        public RandomPlayer Player { get; private set; }

        public GameState()
        {
            Board = new GameBoard();
            Deck = new Deck();
            var hand = Deck.Draw(3);
            Player = new RandomPlayer(hand);
        }

        public void TakeTurn()
        {
            var cardToPlay = Player.Play(Board);
            Board.Move(cardToPlay);
            Player.Discard(cardToPlay);
            var newCards = Deck.Draw(1);
            Player.AddCardsToHand(newCards);
        }
    }
}
