
namespace Poker.RoomsMC
{
    [Serializable]
    public class RoomResponse
    {
        public string RoomId;
        public int SelfId;
        public int RoomState;
        public List<PlayerResponse> Players;
        public TableResponse Table;
        public RoomResponse()
        {
            this.RoomId=string.Empty;
            this.SelfId = 0;
            this.RoomState = 0;
            this.Players = new List<PlayerResponse>();
            this.Table = new TableResponse();
        }
    }
    [Serializable]

    public class PlayerResponse
    {
        public string Name;
        public string Avatar;
        public int Bid;
        public int Money;
        public List<string> Cards;
        public List<string> CombinationCard;
        public int CombinationId;
        public int State;
        public PlayerResponse()
        {
            this.Name = string.Empty;
            this.Avatar = string.Empty;
            this.Bid = 0;
            this.Money = 0;
            this.Cards = new List<string>();
            this.CombinationCard = new List<string>();
            this.State = 0;

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
            this.TotalBank = 0;
            this.Cards = new List<string>();
            this.Skinset = new RoomCosmeticResponse();
            this.RoundNumber = 0;
            this.RoundStage = 0;
            this.DealerId = 0;
            this.WinnersId = new List<int>();
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
            this.CardBackSkin = string.Empty;
            this.CardFrontSkin = string.Empty;
            this.TableSkin = string.Empty;
        }
    }
}
