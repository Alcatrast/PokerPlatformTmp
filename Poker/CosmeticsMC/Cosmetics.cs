
namespace Poker.CosmeticsMC
{
    [Serializable]
    public class Cosmetics
    {
        public List<int> Avatars;
        public int CurrentAvatar;
        public List<int> CardBackSkins;
        public int CurrentCardBackSkin;
        public List<int> CardFrontSkins;
        public int CurrentCardFrontSkin;
        public List<int> TableSkins;
        public int CurrentTableSkin;
        public Cosmetics()
        {
            Avatars = new List<int> { 0 };
            CurrentAvatar = 0;
            CardBackSkins = new List<int> { 0 };
            CurrentCardBackSkin = 0;
            CardFrontSkins = new List<int> { 0 };
            CurrentCardFrontSkin = 0;
            TableSkins = new List<int> { 0 };
            CurrentTableSkin = 0;
        }
    }
}
