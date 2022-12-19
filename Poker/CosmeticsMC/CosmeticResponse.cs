
namespace Poker.CosmeticsMC { 

    [Serializable]
    public class CosmeticResponse
    {
        public List<string> Avatars;
        public List<string> CardBackSkins;
        public List<string> CardFrontSkins;
        public List<string> TableSkins;//
        public CosmeticResponse()
        {
            Avatars = new List<string>();
            CardBackSkins = new List<string>();
            CardFrontSkins = new List<string>();
            TableSkins = new List<string>();

        }
    }
}
