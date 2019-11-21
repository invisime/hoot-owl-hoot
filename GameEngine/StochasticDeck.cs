using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    public class StochasticDeck : DeterministicDeck
    {
        public StochasticDeck(int gameSizeMultiplier, int? numberOfSunCards = null)
            : base(gameSizeMultiplier, numberOfSunCards) { }

        public Dictionary<CardType,int> CardWeights
        {
            get
            {
                return Cards.GroupBy(card => card)
                    .ToDictionary(group => group.Key, group => group.Count());
            }
        }

        public override CardType[] Draw(int numberDesired)
        {
            Shuffle();
            return base.Draw(numberDesired);
        }
    }
}
