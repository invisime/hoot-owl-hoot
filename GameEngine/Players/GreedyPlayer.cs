using System.Collections.Generic;

namespace GameEngine.Players
{
    public class GreedyPlayer : Player
    {
        public override Play FormulatePlay(GameBoard board)
        {
            Play greatestSingleDistancePlay = null;
            int greatestDistance = 0;
            foreach (var owlPosition in board.Owls.ListOfPositions) {
                foreach (var card in Hand.Cards)
                {
                    var play = new Play(card, owlPosition);
                    var newPosition = board.FindDestinationPosition(play);
                    var distance = newPosition - owlPosition;
                    if (distance > greatestDistance)
                    {
                        greatestDistance = distance;
                        greatestSingleDistancePlay = play;
                    }
                }
            }
            return greatestSingleDistancePlay;
        }
    }
}
