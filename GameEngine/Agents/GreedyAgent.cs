namespace GameEngine.Agents
{
    public class GreedyAgent : IAgent
    {
        public Play FormulatePlay(GameState state)
        {
            Play greatestSingleDistancePlay = null;
            int greatestDistance = 0;
            foreach (var owlPosition in state.Board.Owls.ListOfPositions) {
                foreach (var card in state.Hand.Cards)
                {
                    var play = new Play(card, owlPosition);
                    var newPosition = state.Board.FindDestinationPosition(play);
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
