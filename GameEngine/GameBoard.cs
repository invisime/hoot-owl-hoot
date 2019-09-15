using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class GameBoard
    { 
        public List<BoardPositionType> Board { get; private set; }
        public int OwlPosition { get; set; }


        public GameBoard(int gameSizeMultiplier)
        {
            BuildBoard(gameSizeMultiplier);
            OwlPosition = 0;
        }

        public void Move(CardType type)
        {
            if(OwlPosition == Board.Count - 1)
            {
                throw new InvalidMoveException("Owl is already in the Nest");
            }
            if(type == CardType.Sun)
            {
                throw new InvalidMoveException("Sun is not a valid movement type");
            }

            var targetBoardPosition = Convert(type);
            for (int i = OwlPosition + 1; i < Board.Count; i++)
            {
                if(Board[i] == targetBoardPosition || Board[i] == BoardPositionType.Nest)
                {
                    OwlPosition = i;
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
            Board = new List<BoardPositionType>();
            for (int i = 0; i < gameSizeMultiplier; i++)
            {
                foreach (BoardPositionType type in Enum.GetValues(typeof(BoardPositionType)))
                {
                    if (type == BoardPositionType.Nest)
                    {
                        continue;
                    }
                    Board.Add(type);
                }
            }
            Board.Add(BoardPositionType.Nest);
        }
    }
}
