using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    public class GameState
    {
        public enum Phase
        {
            BeginningOfTurn,
            MadePlay,
            DrewCard,
        }

        public Phase TurnPhase = Phase.BeginningOfTurn;
        public GameBoard Board;
        public IDeck Deck;
        public IList<PlayerHand> Hands;
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
            return MakePlay(play)
                .DrawCard()
                .SwitchPlayers();
        }

        public GameState MakePlay(Play play)
        {
            if (TurnPhase != Phase.BeginningOfTurn)
            {
                throw new Exception("TODO new exception");
            }

            var clone = Clone();

            if (play.Card == CardType.Sun)
            {
                clone.SunCounter++;
            }
            else
            {
                clone.Board.Move(play);
            }

            clone.CurrentPlayerHand.Discard(play.Card);

            clone.TurnPhase = Phase.MadePlay;
            return clone;
        }

        public GameState DrawCard() 
        {
            if (TurnPhase != Phase.MadePlay)
            {
                throw new Exception("TODO new exception");
            }

            var clone = Clone();
            clone.CurrentPlayerHand.Add(clone.Deck.Draw(1));
            clone.TurnPhase = Phase.DrewCard;
            return clone;
        }

        public Tuple<GameState, double> DrawForcedCard(CardType card) 
        {
            if (TurnPhase != Phase.MadePlay)
            {
                throw new Exception("TODO new exception");
            }

            var clone = Clone();
            var cardProb = clone.Deck.DrawForcedCard(card);
            clone.CurrentPlayerHand.Add(cardProb.Item1);
            clone.TurnPhase = Phase.DrewCard;
            return Tuple.Create(clone, cardProb.Item2);
        }

        public GameState SwitchPlayers()
        {
            if (TurnPhase != Phase.DrewCard)
            {
                throw new Exception("TODO new exception");
            }

            var clone = Clone();
            clone.NextPlayer();
            clone.TurnPhase = Phase.BeginningOfTurn;
            return clone;
        }

        public override bool Equals(object o)
        {
            var other = o as GameState;
            return other != null
                && TurnPhase == other.TurnPhase
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
                hashCode = hashCode * -1521134295 + EqualityComparer<IList<PlayerHand>>.Default.GetHashCode(Hands);
                hashCode = hashCode * -1521134295 + SunSpaces.GetHashCode();
                hashCode = hashCode * -1521134295 + SunCounter.GetHashCode();
                hashCode = hashCode * -1521134295 + TurnPhase.GetHashCode();
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
                TurnPhase = TurnPhase,
            };
        }

        private void NextPlayer()
        {
            CurrentPlayer++;
            CurrentPlayer %= Hands.Count;
        }
    }
}
