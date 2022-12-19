using Poker.AccountsMC;
using Poker.PokerGameMC;

namespace Poker.RoomsMC
{

    internal class Room
    {
        public string RoomId { get; private set; }
        private List<string> tmpIds;
        private int countAccounts;
        public int StartBank { get; private set; }
        /// <summary>
        /// 0 - closed.
        /// 1 - preparation.
        /// 2 - ingame.
        /// </summary>
        public int RoomState { get; private set; }
        private PokerController pokerController;
        public Room(string roomId, int countAccounts, int startBank)
        {
            tmpIds = new List<string>();
            RoomId = roomId;
            RoomState = 1;
            this.countAccounts = countAccounts;
            StartBank = startBank;
        }
        public bool Join(string accountId, string accountPassword)
        {
            if (RoomState == 1 && tmpIds.Count < countAccounts)
            {
                if (BaseAccounts.IsPasswordRight(accountId, accountPassword) && BaseAccounts.GetCurrentRoom(accountId) == string.Empty)
                {
                    if (BaseAccounts.WithdrawMoney(accountId, StartBank, accountPassword))
                    {
                        BaseAccounts.SetCurrentRoom(accountId, RoomId);
                        tmpIds.Add(accountId);
                        if (tmpIds.Count == countAccounts) { RoomState = 2; ApproveRoom(); }
                        return true;
                    }
                }
            }
            return false;
        }
        public bool Leave(string accountId, string accountPassword)
        {
            if (RoomState == 1)
            {
                if (BaseAccounts.IsPasswordRight(accountId, accountPassword) && BaseAccounts.GetCurrentRoom(accountId) == RoomId)
                {
                    if (BaseAccounts.TopUpBalance(accountId, StartBank))
                    {
                        BaseAccounts.SetCurrentRoom(accountId, string.Empty);
                        tmpIds.Remove(accountId);
                        if (tmpIds.Count == 0) { RoomState = 0; }
                        return true;
                    }
                }
            }
            return false;
        }
        private List<string> accounts;
        public bool ApproveRoom()
        {
            if (RoomState == 1 && countAccounts == tmpIds.Count)
            {
                accounts = new List<string>();
                for (int i = 1; i < tmpIds.Count; i++) { accounts.Add(tmpIds[i]); }
                accounts.Add(tmpIds[0]);
                pokerController = new PokerController(countAccounts, StartBank, 52, new List<List<int>>());
                return true;
            }
            else { return false; }
        }
        public bool Update(string accountId, string accountPassword, string function)
        {
            bool bb = false;
            if (function != null)
            {
                if (RoomState == 2 && BaseAccounts.GetCurrentRoom(accountId) == RoomId && BaseAccounts.IsPasswordRight(accountId, accountPassword) && function.Length > 0)
                {
                    string[] comm = function.Split(Literal.Split.Level3);
                    if (comm.Length > 0)
                    {
                        if (comm[0] == Literal.Command.Round.Start)
                        {
                            bb = StartRound(accountId);
                        }
                        else if (comm[0] == Literal.Command.Round.Finish)
                        {
                            bb = FinishRound(accountId);
                        }
                        else if (comm[0] == Literal.Command.Round.Close)
                        {
                            CloseRoom(accountId);
                        }
                        else if (comm[0] == Literal.Command.Round.Move && comm.Length > 1)
                        {
                            int sb = 0;
                            int.TryParse(comm[1], out sb);
                            Move(accountId, sb);
                        }
                    }
                }
            }
            return bb;
        }
        private int GetIdInGame(string accountId) { return accounts.IndexOf(accountId) + 1; }
        private bool StartRound(string accountId)
        {
            int id = GetIdInGame(accountId);
            GameState previousGameState = pokerController.State;
            pokerController.StartRound(id);
            if (previousGameState == pokerController.State) { return false; } else { return true; }
        }
        private bool FinishRound(string accountId)
        {
            int id = GetIdInGame(accountId);
            GameState previousGameState = pokerController.State;
            pokerController.StartRound(id);
            if (previousGameState == pokerController.State) { return false; } else { return true; }
        }
        private bool CloseRoom(string accountId)
        {
            int id = GetIdInGame(accountId);
            if (id == pokerController.State.nextMovePlayerId && RoomState == 1)
            {
                for (int i = 0; i < countAccounts; i++)
                {
                    BaseAccounts.TopUpBalance(accounts[i], pokerController.State.playersFreeMoney[i]);
                    BaseAccounts.SetCurrentRoom(accounts[i], string.Empty);
                }
                RoomState = 0;
                return true;
            }
            return false;
        }
        private bool Move(string accountId, int smartBid)
        {
            int id = GetIdInGame(accountId);
            GameState previousGameState = pokerController.State;
            pokerController.Move(id, smartBid);
            if (previousGameState == pokerController.State) { return false; } else { return true; }
        }
        public RoomResponse Get(string accountId, string accountPassword)
        {
            RoomResponse response = new RoomResponse();
            if (BaseAccounts.GetCurrentRoom(accountId) == RoomId && BaseAccounts.IsPasswordRight(accountId, accountPassword))
            {
                response.RoomId= RoomId;
                response.RoomState = RoomState;
                if (RoomState == 1)
                {
                    response.SelfId = tmpIds.IndexOf(accountId) + 1;

                    for (int i = 0; i < tmpIds.Count; i++)
                    {
                        response.Players.Add(new PlayerResponse());
                        response.Players[response.Players.Count - 1].Name = BaseAccounts.GetName(tmpIds[i]);
                        response.Players[response.Players.Count - 1].Avatar = BaseAccounts.GetCurrentAvatar(tmpIds[i]);
                        response.Players[response.Players.Count - 1].Money = StartBank;
                    }
                }
                else if (RoomState == 2)
                {
                    response.RoomState = RoomState;
                    response.SelfId = GetIdInGame(accountId);
                    response.Table.Cards = pokerController.State.tableCards;
                    response.Table.DealerId = pokerController.State.dealer;
                    response.Table.RoundStage = pokerController.State.roundStage;
                    response.Table.RoundNumber = pokerController.State.round;
                    response.Table.Skinset = BaseAccounts.GetCurrentRoomSkinSet(accounts[pokerController.State.dealer - 1]);
                    response.Table.TotalBank = pokerController.State.bank;

                    if (pokerController.State.roundStage == 5)
                    {
                        response.Table.WinnersId = pokerController.State.winnersId;
                    }

                    for (int i = 0; i < accounts.Count; i++)
                    {
                        response.Players.Add(new PlayerResponse());
                        response.Players[response.Players.Count - 1].Name = BaseAccounts.GetName(accounts[i]);
                        response.Players[response.Players.Count - 1].Avatar = BaseAccounts.GetCurrentAvatar(accounts[i]);
                        response.Players[response.Players.Count - 1].Money = pokerController.State.playersFreeMoney[i];
                        response.Players[response.Players.Count - 1].Bid = pokerController.State.playersBid[i];
                        response.Players[response.Players.Count - 1].State = pokerController.State.playersStateInGame[i];
                        if (pokerController.State.roundStage == 5)
                        {
                            response.Players[response.Players.Count - 1].Cards = pokerController.State.playersCards[i];
                            response.Players[response.Players.Count - 1].CombinationCard = pokerController.State.playersCardsCombination[i];
                            response.Players[response.Players.Count - 1].CombinationId = pokerController.State.playersCombinationId[i];
                        }
                    }
                    response.Players[response.SelfId - 1].Cards = pokerController.State.playersCards[response.SelfId - 1];
                }
            }
            return response;
        }
    }
}
