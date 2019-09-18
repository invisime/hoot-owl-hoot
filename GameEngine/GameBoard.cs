using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    public class GameBoard
    { 
        public List<BoardPositionType> Board { get; private set; }
        public List<int> OwlPositions { get; set; }


        public GameBoard(int gameSizeMultiplier)
        {
            BuildBoard(gameSizeMultiplier);
            OwlPositions = Enumerable.Repeat(0, gameSizeMultiplier).ToList();
        }

        public void Move(CardType type, int owlIndex)
        {
            if (OwlPositions[owlIndex] == Board.Count - 1)
            {
                throw new InvalidMoveException("Owl is already in the Nest");
            }
            if(type == CardType.Sun)
            {
                throw new InvalidMoveException("Sun is not a valid movement type");
            }

            var targetBoardPosition = Convert(type);
            for (int i = OwlPositions[owlIndex] + 1; i < Board.Count; i++)
            {
                if(Board[i] == targetBoardPosition || Board[i] == BoardPositionType.Nest)
                {
                    OwlPositions[owlIndex] = i;
                    break;
                }
            }
        }

        private BoardPositionType Convert(CardType cardType)
        {
            switch(cardType)
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
