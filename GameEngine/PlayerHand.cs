using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    public class PlayerHand
    {
        public List<CardType> Cards { get; }

        public CardType RandomCard {
            get { return Cards[SeededRandom.Next(0, Cards.Count)]; }
        }

        public CardType OldestCard { get { return Cards[0]; } }

        public bool ContainsSun { get { return Cards.Contains(CardType.Sun); } }

        public PlayerHand() :
            this(new List<CardType>()) { }

        public PlayerHand(params CardType[] startingHand) :
            this(startingHand.ToList()) { }
        
        public PlayerHand(IEnumerable<CardType> startingHand)
        {
            Cards = startingHand.ToList();
        }

        public void Discard(CardType card)
        {
            Cards.Remove(card);
        }

        public void Add(params CardType[] cards)
        {
            Cards.AddRange(cards);
        }

        public override bool Equals(object o)
        {
            var other = o as PlayerHand;
            return other != null
                && Cards.SequenceEqual(other.Cards);
        }

        public override int GetHashCode()
        {
            var hashCode = -1423067844;
            unchecked
            {
                hashCode += EqualityComparer<List<CardType>>.Default.GetHashCode(Cards);
            }
            return hashCode;
        }

        public static bool operator ==(PlayerHand left, PlayerHand right)
        {
            return EqualityComparer<PlayerHand>.Default.Equals(left, right);
        }

        public static bool operator !=(PlayerHand left, PlayerHand right)
        {
            return !(left == right);
        }

        public PlayerHand Clone()
        {
            return new PlayerHand(Cards);
        }
    }
}
