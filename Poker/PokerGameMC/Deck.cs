
using System.Collections.Generic;

namespace Poker.PokerGameMC
{
    internal class Deck
    {


        public List<Card> deck;
        private int size;
        //
        public Deck(int size) { deck = new List<Card>(); this.size = size; Reset(); }
        public Deck(Deck other)
        {
            deck = new List<Card>(other.deck);
            size = other.size;
        }

        public void Reset()
        {
            deck.Clear();
            int minv = 14 - size / 4;
            for (int i = 14; i > minv; i--)
            {
                deck.Add(new Card('S', i));
                deck.Add(new Card('H', i));
                deck.Add(new Card('D', i));
                deck.Add(new Card('C', i));
            }

        }

        public void Shuffle()
        {
            Random random = new Random();
            List<Card> shuffledeck = new List<Card>();
            int min = 0, max = deck.Count - 1, r;
            while (deck.Count > 0)
            {
                r = random.Next(min, max);
                shuffledeck.Add(deck[r]);
                deck.RemoveAt(r);
                max--;
            }
            deck = shuffledeck;


        }

        public Card TakeCard()
        {
            Card card = deck[deck.Count - 1];
            deck.RemoveAt(deck.Count - 1);
            return card;
        }
    }
}
