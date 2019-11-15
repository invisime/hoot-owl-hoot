using System.Collections.Generic;

namespace GameEngine
{
    public class GameState
    {
        public GameBoard Board;
        public Deck Deck;
        public PlayerHand Hand;
        public int SunSpaces;
        public int SunCounter;

        public bool IsOver
        {
            get { return IsWin || IsLoss; }
        }

        public bool IsWin
        {
            get { return Board.Owls.AreAllNested; }
        }

        public bool IsLoss
        {
            get { return SunCounter == SunSpaces; }
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
            var hashCode = -722000984;
            unchecked
            {
                hashCode = hashCode * -1521134295 + EqualityComparer<GameBoard>.Default.GetHashCode(Board);
                hashCode = hashCode * -1521134295 + EqualityComparer<Deck>.Default.GetHashCode(Deck);
                hashCode = hashCode * -1521134295 + EqualityComparer<PlayerHand>.Default.GetHashCode(Hand);
                hashCode = hashCode * -1521134295 + SunSpaces.GetHashCode();
                hashCode = hashCode * -1521134295 + SunCounter.GetHashCode();
            }
            return hashCode;
        }

        public static bool operator ==(GameState left, GameState right)
        {
            return EqualityComparer<GameState>.Default.Equals(left, right);
        }

        public static bool operator !=(GameState left, GameState right)
        {
            return !(left == right);
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
    }
}
