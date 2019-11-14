using System.Linq;

namespace GameEngine.Heuristics
{
    public class WorstCaseNumberOfPlaysToGo : IHeuristic
    {
        public int Evaluate(GameState state)
        {
            return (state.Board.Owls.Count - state.Board.Owls.InTheNest)
                  * state.Board.NestPosition
                  - state.Board.Owls.ListOfPositions.Sum();
        }
    }
}
