using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    public class GameBoard
    { 
        public List<BoardPositionType> Board { get; private set; }
        public List<int> OwlPositions { get; set; }
        
        public int NestPosition
        {
            get { return Board.Count - 1; }
        }
        
        public GameBoard(int gameSizeMultiplier, int numberOfOwls = 6)
        {
            BuildBoard(gameSizeMultiplier);
            OwlPositions = Enumerable.Repeat(0, numberOfOwls).ToList();
        }

        public void Move(Play play)
        {
            if (play.Card == CardType.Sun)
            {
                throw new InvalidMoveException("Sun is not a valid movement type");
            }

            var owlToMove = FindOwl(play.Position);
            var newPosition = FindDestinationPosition(play);

            OwlPositions[owlToMove] = newPosition;
        }

        private BoardPositionType Convert(CardType cardType)
        {
            switch (cardType)
            {
                case CardType.Red:
                    return BoardPositionType.Red;
                case CardType.Orange:
                    return BoardPositionType.Orange;
                case CardType.Yellow:
                    return BoardPositionType.Yellow;
                case CardType.Green:
                    return BoardPositionType.Green;
                case CardType.Blue:
                    return BoardPositionType.Blue;
                case CardType.Purple:
                    return BoardPositionType.Purple;
            }
            return BoardPositionType.Nest;
        }

        private int FindOwl(int position)
        {
            var owlIndex = OwlPositions.IndexOf(position);
            if (owlIndex == -1) {
                throw new InvalidMoveException("There is no owl at position " + position);
            }
            return owlIndex;
        }

        private int FindDestinationPosition(Play play)
        {
            var desiredBoardColor = Convert(play.Card);
            int newPosition = play.Position;
            Predicate<int> isEmptySpaceOfCorrectColor = (position) =>
                desiredBoardColor == Board[position]
                    && !OwlPositions.Contains(position);
            do
            {
                newPosition += 1;
            } while (newPosition < NestPosition && !isEmptySpaceOfCorrectColor(newPosition));
            return newPosition;
        }

        private void BuildBoard(int gameSizeMultiplier)
        {
            var nonNestTypes = Enum.GetValues(typeof(BoardPositionType))
                .Cast<BoardPositionType>()
                .Where(b => b != BoardPositionType.Nest);

            Board = new List<BoardPositionType>();
            for (int i = 0; i < gameSizeMultiplier; i++)
            {
                Board.AddRange(nonNestTypes);
            }
            Board.Add(BoardPositionType.Nest);
        }
    }
}
