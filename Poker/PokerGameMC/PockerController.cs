using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.PokerGameMC
{
    internal class PokerController
    {//

        public GameState State { get; private set; }
        private int round;
        private int countPlayers;
        private int dealer;
        private int deckSize;
        private bool checkIsPossible;
        private List<string> tableCards;
        private List<List<int>> orderWin;
        private List<int> playersCombinationId;
        private List<List<string>> playersCardsCombination;
        private List<int> winnersId;


        private int smallBlind;

        public PokerController() { }
        public PokerController(int countPlayers, int playerFreeMoney, int deckSize, List<List<int>> orderWin)// pfm min =200
        {
            playersCombinationId = new List<int>();
            winnersId = new List<int>();
            State = new GameState(countPlayers, playerFreeMoney);
            smallBlind = playerFreeMoney / 40;
            round = 0;
            dealer = countPlayers;
            this.deckSize = deckSize;
            this.countPlayers = countPlayers;
            this.orderWin = orderWin;
            State.dealer = dealer;
            checkIsPossible = false;

            State.tableCards = new List<string>();
            tableCards = new List<string>();

            State.playersCardsCombination = new List<List<string>>();
            playersCardsCombination = new List<List<string>>();
            playersCombinationId = new List<int>();
            State.playersCombinationId = new List<int>();
            State.playersCards = new List<List<string>>();
            for (int i = 0; i < countPlayers; i++)
            {
                playersCardsCombination.Add(new List<string>());
                playersCombinationId.Add(default);
                State.playersCards.Add(new List<string>());
            }
            winnersId = new List<int>();
            State.winnersId = new List<int>();

        }
        public void StartRound(int dealerId)
        {
            if (dealerId != State.dealer || State.roundStage != 0) { return; }
            State.roundStage = 1;
            round++;

            State.playersCardsCombination = new List<List<string>>();
            playersCardsCombination = new List<List<string>>();
            playersCombinationId = new List<int>();
            State.playersCombinationId = new List<int>();
            State.playersCards = new List<List<string>>();
            for (int i = 0; i < countPlayers; i++)
            {
                playersCardsCombination.Add(new List<string>());
                playersCombinationId.Add(default);
                State.playersCards.Add(new List<string>());
            }
            winnersId = new List<int>();
            State.winnersId = new List<int>();
            State.tableCards = new List<string>();
            tableCards = new List<string>();


            PokerTable pokerTable;
            if (orderWin.Count >= round)
            {
                pokerTable = new PokerTable(countPlayers, deckSize, orderWin[round - 1]);
            }
            else
            {
                pokerTable = new PokerTable(countPlayers, deckSize, new List<int>());
            }
            State.countPlayers = countPlayers;

            tableCards = pokerTable.TableCards();

            for (int i = 0; i < countPlayers; i++)
            {
                State.playersBid[i] = 0;
                for (int j = 0; j < pokerTable.Players.Count; j++)
                {
                    if (i == pokerTable.Players[j].Id - 1)
                    {
                        State.playersCards[i] = pokerTable.Players[j].Cards;
                        playersCardsCombination[i] = pokerTable.Players[j].MaxCombination.Cards;
                        playersCombinationId[i] = pokerTable.Players[j].MaxCombination.Id;
                        break;
                    }
                }
                State.playersStateInGame[i] = 0;
            }

            winnersId.Add(pokerTable.Players[0].Id);
            for (int i = 1; i < pokerTable.Players.Count; i++)
            {
                if (pokerTable.Players[0].MaxCombination == pokerTable.Players[i].MaxCombination)
                {
                    winnersId.Add(pokerTable.Players[i].Id);
                }
                else
                {
                    break;
                }
            }

            State.round = round;
            tableCards = pokerTable.TableCards();


            for (int i = 0; i < State.countPlayers; i++)
            {
                if (State.playersFreeMoney[i] <= 0)
                {
                    State.playersStateInGame[i] = 1;
                }
            }
            bool sbe = false, mbe = false; int sb = 0, mb = 0;
            for (int i = 1; i <= State.countPlayers; i++)
            {
                sb = (State.dealer - 1 + i) % State.countPlayers;
                if (State.playersStateInGame[sb] == 0)
                {
                    sbe = true;
                    ApproveBid(sb + 1, smallBlind);
                    break;
                }
            }
            if (sbe)
            {
                mb = (sb + 1) % State.countPlayers;
                while (State.dealer % State.countPlayers != mb)
                {
                    if (State.playersStateInGame[mb] == 0)
                    {
                        mbe = true;
                        ApproveBid(mb + 1, 2 * smallBlind);
                        break;
                    }
                    mb = (mb + 1) % State.countPlayers;
                }
            }

            if (State.playersBid[sb] == 0 && State.playersBid[mb] == 0)
            {
                checkIsPossible = true;
            }

            if (sbe)
            {
                if (mbe)
                {
                    State.nextMovePlayerId = GetNextBidMoveId(mb + 1, checkIsPossible);
                }
                else
                {
                    State.nextMovePlayerId = GetNextBidMoveId(sb + 1, checkIsPossible);
                }
            }
            else
            {
                State.nextMovePlayerId = 0;
            }
            if (State.nextMovePlayerId == 0)
            {
                ChangeStage();
            }
        }

        public void Move(int id, int smartRase)
        {
            //strange move
            if (id != State.nextMovePlayerId) { return; }
            if (smartRase < 0)
            {
                State.playersStateInGame[id - 1] = 1;
            }
            else
            {
                int expectedResBid = GetMaxBid() - State.playersBid[id - 1];

                if (smartRase > 0)
                {
                    checkIsPossible = false;
                    if (smartRase < 2 * smallBlind)
                    {
                        if (expectedResBid + smartRase >= State.playersFreeMoney[id - 1])
                        {
                            ApproveBid(id, expectedResBid + smartRase);
                        }
                        else
                        {
                            ApproveBid(id, expectedResBid);
                        }
                    }
                    else
                    {
                        ApproveBid(id, expectedResBid + smartRase);
                    }
                }
                else
                {
                    ApproveBid(id, expectedResBid);
                }
            }
            State.nextMovePlayerId = GetNextBidMoveId(id, checkIsPossible);
            if (State.nextMovePlayerId == 0)
            {
                ChangeStage();
            }
        }
        private void ChangeStage()
        {
            if (State.roundStage == 0 || State.roundStage == 5) { return; }
            BuildBank();
            State.roundStage = (State.roundStage + 1) % 6;
            checkIsPossible = true;
            State.nextMovePlayerId = GetNextBidMoveId(0, true);
            if (State.nextMovePlayerId == 0)
            {
                State.roundStage = 5;
            }
            if (State.roundStage == 5)
            {
                ApproveWinners();
                State.nextMovePlayerId = State.dealer;
            }
            UncoverTableCards(State.roundStage);

        }
        public void EndBreak(int id)
        {
            if (id == State.dealer && State.roundStage == 5)
            {
                //transfer button
                for (int i = 1; i <= State.countPlayers; i++)
                {
                    int sb = (State.dealer - 1 + i) % State.countPlayers;
                    if (State.playersFreeMoney[sb] > 0)
                    {
                        State.dealer = sb + 1; break;
                    }
                }
                State.nextMovePlayerId = State.dealer;
                //reset
                GameState gameState = new GameState(State.countPlayers, 1);
                gameState.dealer = State.dealer;
                gameState.nextMovePlayerId = State.nextMovePlayerId;
                gameState.playersFreeMoney = State.playersFreeMoney;
                gameState.round = State.round;

                State = gameState;

                dealer = State.dealer;
                playersCardsCombination = new List<List<string>>();
                playersCombinationId = new List<int>();
                UncoverTableCards(0);
                tableCards = new List<string>();

                State.playersCardsCombination = new List<List<string>>();
                playersCardsCombination = new List<List<string>>();
                playersCombinationId = new List<int>();
                State.playersCombinationId = new List<int>();
                State.playersCards = new List<List<string>>();
                for (int i = 0; i < countPlayers; i++)
                {
                    playersCardsCombination.Add(new List<string>());
                    playersCombinationId.Add(default);
                    State.playersCards.Add(new List<string>());
                }
                winnersId = new List<int>();
                State.winnersId = new List<int>();
                State.tableCards = new List<string>();
                tableCards = new List<string>();


            }
        }
        private int GetMaxBid()
        {
            int res = 0;
            foreach (int bid in State.playersBid)
            {
                if (res < bid) { res = bid; }
            }
            return res;
        }
        private int GetNextBidMoveId(int id, bool checkPossibale)
        {
            int res = 0;
            if (CoutnExpectationPlayers() <= 1) { return 0; }

            if (id == 0)
            {

                for (int i = 1; i < State.countPlayers; i++)
                {
                    int nin = (State.dealer - 1 + i) % State.countPlayers;
                    if (State.playersStateInGame[nin] == 0) { res = nin + 1; break; }
                }
            }
            else
            {

                if (checkPossibale)
                {
                    int nin = id % State.countPlayers; ;
                    while (nin != State.dealer % State.countPlayers)
                    {
                        if (State.playersStateInGame[nin] == 0) { res = nin + 1; break; }
                        nin = (nin + 1) % State.countPlayers;
                    }
                }
                else
                {
                    for (int i = 1; i < State.countPlayers; i++)
                    {
                        int nin = (id - 1 + i) % State.countPlayers;
                        if (State.playersStateInGame[nin] == 0 && State.playersBid[nin] < GetMaxBid()) { res = nin + 1; break; }
                    }
                }
            }
            return res;
        }
        private void ApproveBid(int id, int bid)
        {
            if (bid < State.playersFreeMoney[id - 1])
            {
                State.playersFreeMoney[id - 1] -= bid;
                State.playersBid[id - 1] += bid;
            }
            else
            {
                State.playersBid[id - 1] += State.playersFreeMoney[id - 1];
                State.playersFreeMoney[id - 1] = 0;
                State.playersStateInGame[id - 1] = 3;
            }
        }
        private void BuildBank()
        {
            for (int i = 0; i < State.countPlayers; i++)
            {
                State.bank += State.playersBid[i];
                State.playersBid[i] = 0;
            }
        }
        private void UncoverTableCards(int stage)
        {
            if (stage == 0)
            {
                State.tableCards = new List<string>();
            }
            else
            {
                int ct = 0;
                State.tableCards = new List<string>();
                for (int i = 0; i < 5; i++)
                {
                    State.tableCards.Add("000");
                }
                if (stage == 2) { ct = 3; } else if (stage == 3) { ct = 4; } else if (stage >= 4) { ct = 5; }
                for (int i = 0; i < ct; i++)
                {
                    State.tableCards[i] = tableCards[i];
                }
            }
        }
        private void ApproveWinners()
        {
            State.playersCardsCombination = playersCardsCombination;
            State.playersCombinationId = playersCombinationId;
            State.winnersId = winnersId;
            int individualWin = State.bank / State.winnersId.Count;
            State.bank = 0;
            for (int i = 0; i < State.winnersId.Count; i++)
            {
                State.playersFreeMoney[State.winnersId[i] - 1] += individualWin;
            }
        }

        private int CoutnExpectationPlayers()
        {
            int count0 = 0;
            foreach (int st in State.playersStateInGame)
            {
                if (st == 0) { count0++; }
            }
            return count0;
        }
    }
}

