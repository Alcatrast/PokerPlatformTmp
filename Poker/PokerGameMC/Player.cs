using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.PokerGameMC
{
    internal class Player
    {//

        public int Id { get; set; }
        public Combination MaxCombination { get; private set; }
        public List<string> Cards { get; private set; }
        private List<Card> cards;

        private Combination FindMaxCombination(int deckSize)
        {
            Combination combin, combuf;
            combin = new Combination(PrepareCardsForComb(0, 1), deckSize);

            for (int i = 0; i < 5; i++)
            {
                for (int j = i + 1; j < 7; j++)
                {
                    combuf = new Combination(PrepareCardsForComb(i, j), deckSize);
                    if (combuf > combin)
                    {
                        combin = combuf;
                    }
                }
            }
            return combin;
        }
        private List<Card> PrepareCardsForComb(int i1, int i2)
        {
            List<Card> res = new List<Card>();
            for (int i = 0; i < cards.Count; i++)
            {
                if (i != i1 && i != i2) { res.Add(new Card(cards[i])); }
            }
            return res;
        }



        public Player() { }
        public Player(List<Card> tableCards, List<Card> individualCards, int deckSize)
        {
            Id = 0;
            cards = new List<Card>(tableCards);
            Cards = new List<string>();
            for (int i = 0; i < individualCards.Count; i++)
            {
                cards.Add(new Card(individualCards[i]));
            }
            for (int i = 0; i < individualCards.Count; i++)
            {
                Cards.Add(new string(individualCards[i].Front()));
            }
            MaxCombination = FindMaxCombination(deckSize);
        }
        public Player(Player other)
        {
            Id = other.Id;
            cards = new List<Card>(other.cards);
            Cards = new List<string>(other.Cards);
            MaxCombination = new Combination(other.MaxCombination);
        }

    }
}
