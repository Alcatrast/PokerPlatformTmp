
namespace Poker.CosmeticsMC
{
    internal static class BaseCosmetics
    {
        public static List<string> Avatars { get; private set; }
        public static List<string> CardBackSkins { get; private set; }
        public static List<string> CardFrontSkins { get; private set; }
        public static List<string> TableSkins { get; private set; }
        public static void Initializate()
        {
            Avatars = new List<string> { "Adefault" };
            CardBackSkins = new List<string> { "CBdefault" };
            CardFrontSkins = new List<string> { "CFdefault" };
            TableSkins = new List<string> { "Tdefault" };
        }//
    }
}
