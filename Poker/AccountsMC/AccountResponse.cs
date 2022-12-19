using Poker.CosmeticsMC;

namespace Poker.AccountsMC
{
    [Serializable]
    public class AccountResponse
    {
        public string Id;
        public string Name;
        public string Avatar;
        public int Balance;
        public CosmeticResponse Skins;
        public string CurrentRoomId;
        public AccountResponse()
        {
            Id = string.Empty;
            Name = string.Empty;
            Avatar = string.Empty;
            Balance = 0;
            Skins = new CosmeticResponse();
            CurrentRoomId = string.Empty;
        }
    }
}
