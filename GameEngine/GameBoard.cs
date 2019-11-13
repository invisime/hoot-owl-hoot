using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    public class GameBoard
    {
        public List<BoardPositionType> Board { get; private set; }
        public Parliament Owls { get; private set; }
        
        public int NestPosition
        {
            get { return Board.Count - 1; }
        }

        private GameBoard() { }
        
        public GameBoard(int gameSizeMultiplier, int numberOfOwls = 6)
        {
            BuildBoard(gameSizeMultiplier);
            Owls = new Parliament(numberOfOwls);
        }

        public void Move(Play play)
        {
            var newPosition = FindDestinationPosition(play);
            if(newPosition >= NestPosition)
            {
                Owls.Nest(play.Position);
            } else
            {
                Owls.Move(play.Position, newPosition);
            }
        }

        public int FindDestinationPosition(Play play)
        {
            var desiredColor = play.Card.AsBoardPositionType();
            int newPosition = play.Position + 1;

            while (!OwlShouldStopHere(newPosition, desiredColor))
            {
                newPosition++;
            }
            return newPosition;
        }

        private bool OwlShouldStopHere(int position, BoardPositionType desiredColor)
        {
            return position >= NestPosition
                || (desiredColor == Board[position] && !Owls.Inhabit(position));
        }

        private void BuildBoard(int gameSizeMultiplier)
        {
            var nonNestTypes = BoardPositionTypeExtensions.OneOfEachColor;

            Board = new List<BoardPositionType>();
            for (int i = 0; i < gameSizeMultiplier; i++)
            {
                Board.AddRange(nonNestTypes);
            }
            Board.Add(BoardPositionType.Nest);
        }

        public override bool Equals(object o)
        {
            var other = o as GameBoard;
            return other != null
                && Board.SequenceEqual(other.Board)
                && Owls.Equals(other.Owls);
        }

        public override int GetHashCode()
        {
            var hashCode = 137991109;
            unchecked
            {
                hashCode = hashCode * -1521134295 + EqualityComparer<List<BoardPositionType>>.Default.GetHashCode(Board);
                hashCode = hashCode * -1521134295 + EqualityComparer<Parliament>.Default.GetHashCode(Owls);
            }
            return hashCode;
        }

        public static bool operator ==(GameBoard left, GameBoard right)
        {
            return EqualityComparer<GameBoard>.Default.Equals(left, right);
        }

        public static bool operator !=(GameBoard left, GameBoard right)
        {
            return !(left == right);
        }

        public GameBoard Clone()
        {
            return new GameBoard
            {
                Board = new List<BoardPositionType>(Board),
                Owls = Owls.Clone()
            };
        }
    }
}
