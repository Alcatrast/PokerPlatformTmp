using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.CosmeticsMC
{
    internal class Cosmetics
    {
        public List<int> Avatars { get; private set; }
        public int CurrentAvatar { get; private set; }
        public List<int> CardBackSkins { get; private set; }
        public int CurrentCardBackSkin { get; private set; }
        public List<int> CardFrontSkins { get; private set; }
        public int CurrentCardFrontSkin { get; private set; }
        public List<int> TableSkins { get; private set; }
        public int CurrentTableSkin { get; private set; }
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
