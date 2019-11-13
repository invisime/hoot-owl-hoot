using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    public class PlayerHand
    {
        public List<CardType> Cards { get; }

        public PlayerHand() :
            this(new List<CardType>()) { }

        public PlayerHand(params CardType[] startingHand) :
            this(startingHand.ToList()) { }
        
        public PlayerHand(IEnumerable<CardType> startingHand)
        {
            Cards = startingHand.ToList();
        }

        public override bool Equals(object o)
        {
            var other = o as PlayerHand;
            return other != null
                && Cards.SequenceEqual(other.Cards);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public PlayerHand Clone()
        {
            return new PlayerHand(Cards);
        }

        public CardType RandomCard
        {
            get { return Cards[SeededRandom.Next(0, Cards.Count)]; }
        }

        public CardType OldestCard
        {
            get { return Cards[0]; }
        }

        public bool ContainsSun
        {
            get { return Cards.Contains(CardType.Sun); }
        }

        public void Discard(CardType card)
        {
            Cards.Remove(card);
        }

        public void Add(params CardType[] cards)
        {
            Cards.AddRange(cards);
        }
    }
}
