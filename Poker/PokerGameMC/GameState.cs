using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Poker.PokerGameMC
{
    [Serializable]
    public class GameState
    {
        public int countPlayers;
        public int nextMovePlayerId;
        /// <summary>
        /// 0 - unstarted.
        /// 1 - preflop.
        /// 2 - flop.
        /// 3 - turn.
        /// 4 - river.
        /// 5 - results.
        /// </summary>
        public int roundStage;
        public int round;
        public List<List<string>> playersCards;
        public List<string> tableCards;
        public int dealer;
        public List<int> playersBid;
        public List<int> playersFreeMoney;
        public int bank;
        /// <summary>
        /// 0 - expectation.
        /// 1 - fold.
        /// 2 - call.
        /// 3 - allin.
        /// </summary>
        public List<int> playersStateInGame;
        public List<List<string>> playersCardsCombination;
        public List<int> winnersId;
        public List<int> playersCombinationId;
        public GameState() { }
        public GameState(int countPlayers, int playerFreeMoney)
        {
            bank = 0;
            playersCards = new List<List<string>>();
            tableCards = new List<string>();
            playersBid = new List<int>();
            playersFreeMoney = new List<int>();
            playersStateInGame = new List<int>();
            playersCardsCombination = new List<List<string>>();
            winnersId = new List<int>();
            playersCombinationId = new List<int>();

            round = 0;
            this.countPlayers = countPlayers;
            roundStage = 0;
            dealer = 0;
            nextMovePlayerId = 0;
            for (int i = 0; i < this.countPlayers; i++)
            {
                playersStateInGame.Add(0);
                playersFreeMoney.Add(playerFreeMoney);
                playersBid.Add(0);
                playersCardsCombination.Add(new List<string>());
                playersCards.Add(new List<string>());
                for (int j = 0; j < 2; j++)
                {
                    playersCards[i].Add("000");
                }
            }
            for (int j = 0; j < 5; j++)
            {
                tableCards.Add("000");
            }
        }
        public GameState(GameState other)
        {
            bank = other.bank;
            countPlayers = other.countPlayers;
            roundStage = other.roundStage;
            round = other.round;
            dealer = other.dealer;
            playersCards = new List<List<string>>(other.playersCards);
            tableCards = new List<string>(other.tableCards);
            playersBid = new List<int>(other.playersBid);
            playersFreeMoney = new List<int>(other.playersFreeMoney);
            playersStateInGame = new List<int>(other.playersStateInGame);
            playersCardsCombination = new List<List<string>>(other.playersCardsCombination);
            winnersId = new List<int>(other.winnersId);
            nextMovePlayerId = other.nextMovePlayerId;
            playersCombinationId = new List<int>(other.playersCombinationId);
        }
    }
}

