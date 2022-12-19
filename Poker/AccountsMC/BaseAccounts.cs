using System.Text;
using System.Xml.Serialization;
using Poker.CosmeticsMC;
using Poker.RoomsMC;

namespace Poker.AccountsMC
{
    internal static class BaseAccounts
    {
        private static string currentId;
        private static List<Account> accounts;
        private static string absAccDir;
        public static void Initialaize(string _absAccDir)
        {
            absAccDir= _absAccDir;
            accounts = new List<Account>();
            currentId = (Literal.Type.IdPrefix.Account + "0");
            Account sysac= new Account(currentId,"Error","aboba");
            sysac.TopUpBalance(int.MaxValue - 7);
            accounts.Add(sysac);
        }
        public static void BuildFromFiles()
        {
            XmlSerializer serializer= new XmlSerializer(typeof(AccountXml));
            while (true)
            {
                ChangeCurrentId();
                if (File.Exists($@"{absAccDir}\{currentId}"))
                {
                    StreamReader sr = new StreamReader($@"{absAccDir}\{currentId}", Encoding.UTF8);
                    accounts.Add(new Account((AccountXml)serializer.Deserialize(sr)));
                    sr.Close();
                }
                else { break; }
            }

        }
        public static void Deconstructe(string id) 
        {
            StreamWriter sw = new StreamWriter($@"{absAccDir}\{id}");
            XmlSerializer serializer = new XmlSerializer(typeof(AccountXml));
            serializer.Serialize(sw, accounts[GetIndex(id)].ForSerilaizer());
            sw.Close();

        }
        public static string Add(string name, string password)
        {
            ChangeCurrentId();
            accounts.Add(new Account(currentId, name, password));
            Deconstructe(currentId);
            return currentId;
        }
        private static void ChangeCurrentId()
        {
            string sid = string.Empty;
            for (int i = Literal.Type.IdPrefix.Account.Length; i < currentId.Length; i++)
            {
                sid += currentId[i];
            }
            currentId = Literal.Type.IdPrefix.Account + Convert.ToString(int.Parse(sid) + 1);
        }
        private static int GetIndex(string id)
        {
            if (id != null)
            {
                if (id.Length > Literal.Type.IdPrefix.Account.Length)
                {
                    if (id.Substring(0, Literal.Type.IdPrefix.Account.Length) == Literal.Type.IdPrefix.Account)
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
        public static int GetBalance(string id)
        {
            return accounts[GetIndex(id)].Balance;
        }
        public static string GetCurrentAvatar(string id)
        {   
            return BaseCosmetics.Avatars[accounts[GetIndex(id)].Skins.CurrentAvatar % BaseCosmetics.Avatars.Count];
        }   
        private static List<string> GetTableSkins(string id)
        {
            List<string> res = new List<string>();
            List<int> indexes = accounts[GetIndex(id)].Skins.TableSkins;
            foreach(int ind in indexes)
            {
                if (ind < BaseCosmetics.TableSkins.Count)
                {
                    res.Add(BaseCosmetics.TableSkins[ind]);
                }
            }
            return res;
        }
        private static List<string> GetAvatars(string id)
        {
            List<string> res = new List<string>();
            List<int> indexes = accounts[GetIndex(id)].Skins.Avatars;
            foreach (int ind in indexes)
            {
                if (ind < BaseCosmetics.Avatars.Count)
                {
                    res.Add(BaseCosmetics.Avatars[ind]);
                }
            }
            return res;
        }
        private static List<string> GetCardBackSkins(string id)
        {
            List<string> res = new List<string>();
            List<int> indexes = accounts[GetIndex(id)].Skins.CardBackSkins;
            foreach (int ind in indexes)
            {
                if (ind < BaseCosmetics.CardBackSkins.Count)
                {
                    res.Add(BaseCosmetics.CardBackSkins[ind]);
                }
            }
            return res;
        }
        private static List<string> GetCardFrontSkins(string id)
        {
            List<string> res = new List<string>();
            List<int> indexes = accounts[GetIndex(id)].Skins.CardFrontSkins;
            foreach (int ind in indexes)
            {
                if (ind < BaseCosmetics.CardFrontSkins.Count)
                {
                    res.Add(BaseCosmetics.CardFrontSkins[ind]);
                }
            }
            return res;
        }
        public static RoomCosmeticResponse GetCurrentRoomSkinSet(string id)
        {
            RoomCosmeticResponse res = new RoomCosmeticResponse();
            res.CardBackSkin = BaseCosmetics.CardBackSkins[accounts[GetIndex(id)].Skins.CurrentCardBackSkin % BaseCosmetics.CardBackSkins.Count];
            res.CardFrontSkin = BaseCosmetics.CardFrontSkins[accounts[GetIndex(id)].Skins.CurrentCardFrontSkin % BaseCosmetics.CardFrontSkins.Count];
            res.TableSkin = BaseCosmetics.TableSkins[accounts[GetIndex(id)].Skins.CurrentTableSkin % BaseCosmetics.TableSkins.Count];
            return res;
        }
        public static CosmeticResponse GetCosmeticResponse(string id) 
        {
            CosmeticResponse res = new CosmeticResponse();
            res.Avatars=GetAvatars(id);
            res.CardBackSkins=GetCardBackSkins(id);
            res.CardFrontSkins=GetCardFrontSkins(id);
            res.TableSkins=GetTableSkins(id);
            return res;
        }
        public static AccountResponse GetResponse(string id,string password) 
        { 
            AccountResponse res=new AccountResponse();
            if(IsPasswordRight(id, password))
            {
                res.Balance=GetBalance(id);
                res.Avatar=GetCurrentAvatar(id);
                res.CurrentRoomId = GetCurrentRoom(id);
                res.Id = id;
                res.Name=GetName(id);
                res.Skins=GetCosmeticResponse(id);
            }
                return res;
        } 
        public static AccountResponse ProcessingRequest(string accountIdName, string accountPassword, string function) 
        {
            if (function != null)
            {
                if (function.Length > 3)
                {
                    string[] command = function.Split(Literal.Split.Level2);///////////////////
                    if (command.Length > 0)
                    {
                        if (command[0] == Literal.Command.Create) 
                        {
                            string id= Add(accountIdName, accountPassword);
                            return GetResponse(id, accountPassword);
                        }else if (command[0] == Literal.Command.Get)
                        {
                            return GetResponse(accountIdName, accountPassword);
                        } else if (command[0] == Literal.Command.Update)
                        {
                            accounts[GetIndex(accountIdName)].Update(command[1], command[2]);
                            Deconstructe(accountIdName);
                            return GetResponse(accountIdName, accountPassword);
                        }
                    }
                }
            }
            return new AccountResponse();
        }
    }
}
