﻿using System;
using System.Collections.Generic;

namespace GameEngine
{
    public interface IDeck
    {
        CardType[] SampleAll();
        CardType[] Sample(int numberDesired);
        CardType[] Draw(int numberDesired);
        Tuple<CardType, double> DrawForcedCard(CardType cardType); 
        IDictionary<CardType, double> Probabilities();
        IDeck Clone();
        bool Equals(object o);
        int GetHashCode();
    }
}
