using System;
using System.Linq;

namespace GameEngine
{
    public enum BoardPositionType
    {
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        Purple,
        Nest
    }

    public static class BoardPositionTypeExtensions
    {
        public static BoardPositionType[] OneOfEachColor
        {
            get
            {
                return Enum.GetValues(typeof(BoardPositionType))
                    .Cast<BoardPositionType>()
                    .Where(position => position != BoardPositionType.Nest)
                    .ToArray();
            }
        }
    }
}
