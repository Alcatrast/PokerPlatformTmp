

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.PokerGameMC
{
    internal class PokerTable
    {

        public List<Player> Players { get; private set; }//
        private List<Card> tableCards;
        private List<Player> MoreWinner(List<Player> pls)
        {
            List<Player> res = new List<Player>();
            Player bp;
            int ibp;

            int count_players = pls.Count;
            for (int j = 0; j < count_players; j++)
            {
                bp = pls[0];
                ibp = 0;

                for (int i = 0; i < pls.Count; i++)
                {
                    if (pls[i].MaxCombination > bp.MaxCombination)
                    {
                        bp = pls[i];
                        ibp = i;
                    }
                }
                res.Add(new Player(bp));
                pls.RemoveAt(ibp);
            }
            return res;
        }
        private List<int> DistributeId(int count, List<int> ids)
        {
            Random random = new Random();
            List<int> buf = new List<int>(), res = new List<int>();
            for (int i = 0; i < count; i++)
            {
                buf.Add(i + 1);
            }
            int min = 0, max = buf.Count - 1, r;
            while (buf.Count > 0)
            {
                r = random.Next(min, max);
                res.Add(buf[r]);
                buf.RemoveAt(r);
                max--;
            }
            int b;
            for (int i = 0; i < ids.Count; i++)
            {

                for (int j = 0; j < Players.Count; j++)
                {
                    if (ids[i] == res[j])
                    {
                        b = res[i];
                        res[i] = res[j];
                        res[j] = b;
                    }
                }

            }


            return res;
        }


        public PokerTable() { }
        public PokerTable(int countPlayers, int deckSize, List<int> pids)
        {
            Players = new List<Player>();
            tableCards = new List<Card>();
            List<Card> player_cards = new List<Card>();
            Deck deck = new Deck(deckSize);
            deck.Shuffle();
            for (int i = 0; i < 5; i++)
            {
                tableCards.Add(new Card(deck.TakeCard()));
            }

            for (int i = 0; i < countPlayers; i++)
            {
                player_cards.Clear();
                for (int j = 0; j < 2; j++)
                {
                    player_cards.Add(new Card(deck.TakeCard()));
                }
                Players.Add(new Player(tableCards, player_cards, deckSize));
            }
            Players = MoreWinner(new List<Player>(Players));
            List<int> ids = DistributeId(Players.Count, pids);
            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].Id = ids[i];
            }

        }
        public PokerTable(PokerTable other)
        {
            Players = new List<Player>(other.Players);
            tableCards = new List<Card>(other.tableCards);
        }
        public List<string> TableCards()
        {
            List<string> res = new List<string>();
            for (int i = 0; i < tableCards.Count; i++)
            {
                res.Add(tableCards[i].Front());
            }
            return res;
        }
    }
}
