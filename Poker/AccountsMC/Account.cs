using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.CosmeticsMC;
//
namespace Poker.AccountsMC
{
    internal class Account
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        private string password;
        public int Balance { get; private set; }
        public Cosmetics Skins { get; private set; }

        public string CurrentRoomId { get; set; }
        public Account() { }
        public Account(string id, string name, string password)
        {
            Id = id;
            Name = name;
            this.password = password;
            Balance = 0;
            Skins = new Cosmetics();
        }
        public bool IsPasswordRight(string password)
        {
            return this.password == password;
        }
        public bool UpdateName(string newName, string password)
        {
            if (IsPasswordRight(password))
            {
                Name = newName;
                return true;
            }
            else { return false; }
        }
        public bool TopUpBalance(int money)
        {


            if (money > 0)
            {
                Balance += money;
                return true;
            }

            return false;
        }
        public bool WithdrawMoney(int money, string password)
        {
            if (IsPasswordRight(password))
            {
                if (money > 0)
                {
                    if (Balance >= money)
                    {
                        Balance -= money;
                        return true;
                    }
                }
            }
            return false;
        }
        
        private void Update(string type, string newProperty)
        {
            if (type == "N") { UpdateName(newProperty); }
            else if (type == "TS") { UpdateTableSkin(newProperty); } 
            else if (type == "CBS") { }//// 
            else if (type == "CFS") { }

        }
        private void UpdateName(string newName) { this.Name = newName; }
        private void UpdateTableSkin(string newSkin) { 
            int ncs = this.Skins.TableSkins.IndexOf(BaseCosmetics.TableSkins.IndexOf(newSkin));
            if (ncs < 0) { ncs = 0; }
            this.Skins.CurrentTableSkin = ncs;
        }
    }
}
