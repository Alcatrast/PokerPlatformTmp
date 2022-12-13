using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Poker.PokerGameMC
{
    internal class Card
    {

        public int Strength { get; private set; }//
        public char Suit { get; private set; }

        public Card(char suit, int strength)
        {
            Strength = strength;
            Suit = suit;
        }
        public Card(Card card)
        {
            Strength = card.Strength;
            Suit = card.Suit;
        }

        public string Front()
        {
            string res = string.Empty;
            res += Convert.ToString(Suit);
            if (Strength < 10) { res += "0"; }
            return res + Convert.ToString(Strength);
        }
    }
}
