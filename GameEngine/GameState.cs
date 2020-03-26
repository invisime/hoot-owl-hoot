using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    public class GameState
    {
        public GameBoard Board;
        public IDeck Deck;
        public List<PlayerHand> Hands;
        public int SunSpaces;
        public int SunCounter;
        public int CurrentPlayer = 0;

        public PlayerHand CurrentPlayerHand => Hands[CurrentPlayer];

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

            state.CurrentPlayerHand.Discard(play.Card);
            state.CurrentPlayerHand.Add(state.Deck.Draw(1));

            state.NextPlayer();

            return state;
        }

        public override bool Equals(object o)
        {
            var other = o as GameState;
            return other != null
                && SunSpaces == other.SunSpaces
                && SunCounter == other.SunCounter
                && Board.Equals(other.Board)
                && Deck.Equals(other.Deck)
                && CurrentPlayer == other.CurrentPlayer
                && Hands.Count == other.Hands.Count
                && Hands.Zip(other.Hands, (a, b) => a.Equals(b)).All(b => b);
        }

        public override int GetHashCode()
        {
            var hashCode = -722000984;
            unchecked
            {
                hashCode = hashCode * -1521134295 + EqualityComparer<GameBoard>.Default.GetHashCode(Board);
                hashCode = hashCode * -1521134295 + EqualityComparer<IDeck>.Default.GetHashCode(Deck);
                hashCode = hashCode * -1521134295 + EqualityComparer<List<PlayerHand>>.Default.GetHashCode(Hands);
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
                Hands = Hands.Select( hand => hand.Clone() ).ToList(),
                SunCounter = SunCounter,
                SunSpaces = SunSpaces,
                CurrentPlayer = CurrentPlayer,
            };
        }

        private void NextPlayer()
        {
            if (CurrentPlayer < Hands.Count - 1)
            {
                CurrentPlayer++;
            }
            else
            {
                CurrentPlayer = 0;
            }
        }
    }
}
