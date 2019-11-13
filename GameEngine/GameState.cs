using System;

namespace GameEngine
{
    public class GameState
    {
        public GameBoard Board;
        public Deck Deck;
        public PlayerHand Hand;
        public int SunSpaces;
        public int SunCounter;

        public override bool Equals(object o)
        {
            var other = o as GameState;
            return other != null
                && Board.Equals(other.Board)
                && Deck.Equals(other.Deck)
                && Hand.Equals(other.Hand)
                && SunSpaces == other.SunSpaces
                && SunCounter == other.SunCounter;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public GameState Clone()
        {
            return new GameState
            {
                Board = Board.Clone(),
                Deck = Deck.Clone(),
                Hand = Hand.Clone(),
                SunCounter = SunCounter,
                SunSpaces = SunSpaces
            };
        }

        public GameState Successor(Play play)
        {
            var state = Clone();

            if (play.Card == CardType.Sun)
            {
                state.SunCounter++;
            }
            else
            {
                state.Board.Move(play);
            }

            state.Hand.Discard(play.Card);
            state.Hand.Add(state.Deck.Draw(1));

            return state;
        }
    }
}
