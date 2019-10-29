namespace GameEngine
{
    public enum CardType
    {
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        Purple,
        Sun
    }

    static class CardTypeExtensions
    {
        public static BoardPositionType AsBoardPositionType(this CardType cardType)
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
            throw new InvalidMoveException("Sun is not a valid movement type");
        }
    }
}
