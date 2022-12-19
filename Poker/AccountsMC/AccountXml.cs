using Poker.CosmeticsMC;

namespace Poker.AccountsMC
{
    [Serializable]
    public class AccountXml
    {
        public string Id;
        public string Name;
        public string Password;
        public int Balance;
        public Cosmetics Skins;
    }
}
