using Poker.CosmeticsMC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
