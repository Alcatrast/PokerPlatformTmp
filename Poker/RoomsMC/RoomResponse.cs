using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Poker.RoomsMC
{
    [Serializable]
    public class RoomResponse
    {
        public int SelfId;
        public int RoomState;
        public List<AccountResponse> Players;
        public TableResponse Table;
        public RoomResponse()
        {
            SelfId = 0;
            RoomState = 0;
            Players = new List<AccountResponse>();
            Table = new TableResponse();
        }
    }
    [Serializable]

    public class AccountResponse
    {
        public string Name;
        public string Avatar;
        public int Bid;
        public int Money;
        public List<string> Cards;
        public List<string> CombinationCard;
        public int CombinationId;
        public int State;
        public AccountResponse()
        {
            Name = string.Empty;
            Avatar = string.Empty;
            Bid = 0;
            Money = 0;
            Cards = new List<string>();
            CombinationCard = new List<string>();
            State = 0;

        }
    }
    [Serializable]
    public class TableResponse
    {
        public int TotalBank;
        public List<string> Cards;
        public RoomCosmeticResponse Skinset;
        public int RoundNumber;
        public int RoundStage;
        public int DealerId;
        public List<int> WinnersId;
        public TableResponse()
        {
            TotalBank = 0;
            Cards = new List<string>();
            Skinset = new RoomCosmeticResponse();
            RoundNumber = 0;
            RoundStage = 0;
            DealerId = 0;
            WinnersId = new List<int>();
        }
    }
    [Serializable]
    public class RoomCosmeticResponse
    {
        public string CardBackSkin;
        public string CardFrontSkin;
        public string TableSkin;
        public RoomCosmeticResponse()
        {
            CardBackSkin = string.Empty;
            CardFrontSkin = string.Empty;
            TableSkin = string.Empty;
        }
    }
}
