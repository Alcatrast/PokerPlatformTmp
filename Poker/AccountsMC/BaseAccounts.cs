using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Poker.CosmeticsMC;
using Poker.RoomsMC;

namespace Poker.AccountsMC
{
    internal static class BaseAccounts
    {
        private static string currentId;
        private static List<Account> accounts;
        public static void Initializate()
        {
            accounts = new List<Account>();
            currentId = "ac0";
        }
        public static string Add(string name, string password)
        {
            ChangeCurrentId();
            accounts.Add(new Account(currentId, name, password));
            return currentId;
        }
        private static void ChangeCurrentId()
        {
            string sid = string.Empty;
            for (int i = 2; i < currentId.Length; i++)
            {
                sid += currentId[i];
            }
            currentId = "ac" + Convert.ToString(int.Parse(sid) + 1);
        }
        private static int GetIndex(string id)
        {
            if (id != null)
            {
                if (id.Length > 2)
                {
                    if (id[0] == 'a' && id[1]=='c')
                    {
                        string sid = string.Empty;
                        for (int i = 2; i < id.Length; i++)
                        {
                            sid += id[i];
                        }
                        int index = 0;
                        int.TryParse(sid, out index);
                        index--;
                        if (index < 0 || index >= accounts.Count) { return 0; }
                        return index;
                    }
                }
            }
            return 0;
        }
        public static bool TopUpBalance(string id, int money)
        {
            int index = GetIndex(id);
            return accounts[index].TopUpBalance(money);
        }
        public static bool WithdrawMoney(string id, int money, string password)
        {
            int index = GetIndex(id);
            return accounts[index].WithdrawMoney(money, password);
        }
        public static void SetCurrentRoom(string acid, string newRoomId)
        {
            accounts[GetIndex(acid)].CurrentRoomId = newRoomId;
        }
        public static string GetCurrentRoom(string acid)
        {
            return accounts[GetIndex(acid)].CurrentRoomId;
        }
        public static bool IsPasswordRight(string id, string password)
        {
            return accounts[GetIndex(id)].IsPasswordRight(password);
        }
        public static string GetName(string id)
        {
            return accounts[GetIndex(id)].Name;
        }
        public static string GetCurrentAvatar(string id)
        {
            return BaseCosmetics.Avatars[accounts[GetIndex(id)].Skins.CurrentAvatar % BaseCosmetics.Avatars.Count];
        }

        internal static RoomCosmeticResponse GetCurrentRoomSkinSet(string id)
        {
            RoomCosmeticResponse res = new RoomCosmeticResponse();
            res.CardBackSkin = BaseCosmetics.CardBackSkins[accounts[GetIndex(id)].Skins.CurrentCardBackSkin % BaseCosmetics.CardBackSkins.Count];
            res.CardFrontSkin = BaseCosmetics.CardFrontSkins[accounts[GetIndex(id)].Skins.CurrentCardFrontSkin % BaseCosmetics.CardFrontSkins.Count];
            res.TableSkin = BaseCosmetics.TableSkins[accounts[GetIndex(id)].Skins.CurrentTableSkin % BaseCosmetics.TableSkins.Count];
            return res;
        }
    }
}
