using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.PokerGameMC
{
    internal class Combination
    {

        public List<string> Cards { get; private set; }
        public int Id { get; private set; }
        private int minValC;
        private int strengthCInC;

        private List<Card> sort(List<Card> cards)
        {
            Card buf;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (cards[j].Strength > cards[j + 1].Strength)
                    {
                        buf = new Card(cards[j]);
                        cards[j] = new Card(cards[j + 1]);
                        cards[j + 1] = new Card(buf);
                    }
                }
            }
            return new List<Card>(cards);
        }

        private void DefineCombination(List<Card> cards)
        {
            if (!IsRoyarFlush(cards))
            {
                if (!IsStraightFlush(cards))
                {
                    if (!IsFourOfAKing(cards))
                    {
                        if (!IsFullHouse(cards))
                        {
                            if (!IsFlush(cards))
                            {
                                if (!IsStraight(cards))
                                {
                                    if (!IsthereOfAKing(cards))
                                    {
                                        if (!IsTwoPairs(cards))
                                        {
                                            if (!IsOnePair(cards))
                                            {
                                                strengthCInC = cards[cards.Count - 1].Strength;
                                                Id = 0;
                                                Cards.Add(new string(cards[cards.Count - 1].Front()));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }
        private bool IsOnePair(List<Card> cards)
        {
            for (int i = 0; i < 4; i++)
            {
                if (cards[i].Strength == cards[i + 1].Strength)
                {
                    strengthCInC = cards[i].Strength;
                    Id = 1;
                    Cards.Clear();
                    Cards.Add(new string(cards[i].Front()));
                    Cards.Add(new string(cards[i + 1].Front()));
                    return true;
                }
            }
            return false;
        }
        private bool IsTwoPairs(List<Card> cards)
        {

            for (int i = 0; i < 4; i++)
            {
                if (cards[i].Strength == cards[i + 1].Strength)
                {
                    for (int j = i + 2; j < 4; j++)
                    {
                        if (cards[j].Strength == cards[j + 1].Strength)
                        {
                            strengthCInC = cards[j].Strength;
                            Id = 2;
                            Cards.Clear();
                            Cards.Add(new string(cards[i].Front()));
                            Cards.Add(new string(cards[i + 1].Front()));
                            Cards.Add(new string(cards[j].Front()));
                            Cards.Add(new string(cards[j + 1].Front()));
                            return true;
                        }
                    }
                    return false;
                }
            }
            return false;
        }
        private bool IsthereOfAKing(List<Card> cards)
        {
            for (int i = 0; i < 3; i++)
            {
                if (cards[i].Strength == cards[i + 1].Strength && cards[i].Strength == cards[i + 2].Strength)
                {
                    strengthCInC = cards[i].Strength;
                    Id = 3;
                    Cards.Clear();
                    Cards.Add(new string(cards[i].Front()));
                    Cards.Add(new string(cards[i + 1].Front()));
                    Cards.Add(new string(cards[i + 2].Front()));
                    return true;
                }
            }
            return false;

        }
        private bool IsStraight(List<Card> cards)
        {

            if (cards[4].Strength == 14)
            {
                bool ms = true;
                for (int i = 0; i < 4; i++)
                {
                    if (cards[i].Strength != minValC + i)
                    {
                        ms = false;
                        break;
                    }
                }
                if (ms)
                {
                    strengthCInC = 5;
                    Id = 4;
                    Cards.Clear();
                    for (int i = 0; i < cards.Count(); i++)
                    {
                        Cards.Add(new string(cards[i].Front()));
                    }
                    return true;
                }
            }
            int local_min_val_c = cards[0].Strength;
            bool s = true;
            for (int i = 0; i < 5; i++)
            {
                if (cards[i].Strength != local_min_val_c + i)
                {
                    s = false;
                    break;
                }
            }
            if (s)
            {
                strengthCInC = cards[cards.Count - 1].Strength;
                Id = 4;
                Cards.Clear();
                for (int i = 0; i < cards.Count; i++)
                {
                    Cards.Add(new string(cards[i].Front()));
                }
                return true;
            }
            return false;
        }
        private bool IsFlush(List<Card> cards)
        {

            char os = cards[0].Suit;
            for (int i = 1; i < cards.Count; i++)
            {
                if (os != cards[i].Suit)
                {
                    return false;
                }
            }
            strengthCInC = cards[cards.Count - 1].Strength;
            Id = 5;
            Cards.Clear();
            for (int i = 0; i < cards.Count; i++)
            {
                Cards.Add(new string(cards[i].Front()));
            }

            return true;
        }
        private bool IsFullHouse(List<Card> cards)
        {
            bool sc = IsTwoPairs(cards) && IsthereOfAKing(cards);
            Cards.Clear();
            Id = 0;
            strengthCInC = 0;
            if (sc)
            {
                strengthCInC = cards[cards.Count - 1].Strength;
                Id = 6;
                for (int i = 0; i < cards.Count; i++)
                {
                    Cards.Add(new string(cards[i].Front()));
                }
            }
            return sc;
        }
        private bool IsFourOfAKing(List<Card> cards)
        {
            for (int i = 0; i < 2; i++)
            {
                if (cards[i].Strength == cards[i + 1].Strength && cards[i].Strength == cards[i + 2].Strength && cards[i].Strength == cards[i + 3].Strength)
                {
                    strengthCInC = cards[i].Strength;
                    Id = 7;
                    Cards.Clear();
                    Cards.Add(new string(cards[i].Front()));
                    Cards.Add(new string(cards[i + 1].Front()));
                    Cards.Add(new string(cards[i + 2].Front()));
                    Cards.Add(new string(cards[i + 3].Front()));
                    return true;
                }
            }
            return false;
        }
        private bool IsStraightFlush(List<Card> cards)
        {
            bool sc = IsFlush(cards) && IsStraight(cards);
            Cards.Clear();
            Id = 0;
            strengthCInC = 0;
            if (sc)
            {
                strengthCInC = cards[cards.Count - 1].Strength;
                Id = 8;
                for (int i = 0; i < cards.Count; i++)
                {
                    Cards.Add(new string(cards[i].Front()));
                }
            }
            return sc;
        }
        private bool IsRoyarFlush(List<Card> cards)
        {
            bool sc = IsStraightFlush(cards) && cards[3].Strength == 13;
            Cards.Clear();
            Id = 0;
            strengthCInC = 0;
            if (sc)
            {
                Id = 9;
                strengthCInC = 14;
                for (int i = 0; i < cards.Count; i++)
                {
                    Cards.Add(new string(cards[i].Front()));
                }
            }
            return sc;
        }



        public Combination() { }
        public Combination(List<Card> cards, int dimensionsDeck)
        {
            Cards = new List<string>();
            minValC = 15 - dimensionsDeck / 4;
            strengthCInC = 0;
            cards = sort(cards);
            DefineCombination(cards);
        }
        public Combination(Combination other)
        {
            strengthCInC = other.strengthCInC;
            minValC = other.minValC;
            Id = other.Id;
            Cards = new List<string>(other.Cards);
        }

        public static bool operator >(Combination t, Combination o)
        {
            if (t.Id > o.Id) { return true; }
            if (t.Id == o.Id) { if (t.strengthCInC > o.strengthCInC) { return true; } }
            return false;
        }
        public static bool operator <(Combination t, Combination o)
        {
            if (t.Id < o.Id) { return true; }
            if (t.Id == o.Id) { if (t.strengthCInC < o.strengthCInC) { return true; } }
            return false;
        }
        public static bool operator ==(Combination t, Combination o)
        {
            return t.Id == o.Id && t.strengthCInC == o.strengthCInC;
        }
        public static bool operator !=(Combination t, Combination o)
        {
            return !(t.Id == o.Id && t.strengthCInC == o.strengthCInC);
        }
    }
}

