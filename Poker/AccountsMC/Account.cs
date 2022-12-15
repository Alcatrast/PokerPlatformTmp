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
            Balance = 0;
            this.CurrentRoomId = string.Empty;
            Id = id;
            Name = name;
            this.password = password;
            Skins = new Cosmetics();
        }
        public Account(AccountXml xml)
        {
            this.Balance = xml.Balance;
            this.CurrentRoomId=string.Empty;
            this.Id = xml.Id;
            this.Name = xml.Name;
            this.password = xml.Password;
            this.Skins = xml.Skins;
            
        }
        public AccountXml ForSerilaizer() 
        {
            AccountXml res = new AccountXml();
            res.Balance=this.Balance;
            res.Id = this.Id;
            res.Name=this.Name;
            res.Password = this.password;
            res.Skins=this.Skins;
            return res;
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
        
        public void Update(string type, string newProperty)
        {
            if (type == "N") { UpdateName(newProperty); }
            else if (type == "A") { UpdateAvatar(newProperty); }
            else if (type == "CBS") { UpdateCardBackSkin(newProperty); }
            else if (type == "CFS") { UpdateCardFrontSkin(newProperty); }
            else if (type == "TS") { UpdateTableSkin(newProperty); }
            
        }
        private void UpdateName(string newName) { this.Name = newName; }
        private void UpdateAvatar(string newSkin)
        {
            int ncs = this.Skins.Avatars.IndexOf(BaseCosmetics.Avatars.IndexOf(newSkin));
            if (ncs < 0) { ncs = 0; }
            this.Skins.CurrentAvatar = ncs;
        }
        private void UpdateCardBackSkin(string newSkin)
        {
            int ncs = this.Skins.CardBackSkins.IndexOf(BaseCosmetics.CardBackSkins.IndexOf(newSkin));
            if (ncs < 0) { ncs = 0; }
            this.Skins.CurrentCardBackSkin = ncs;
        }
        private void UpdateCardFrontSkin(string newSkin)
        {
            int ncs = this.Skins.CardFrontSkins.IndexOf(BaseCosmetics.CardFrontSkins.IndexOf(newSkin));
            if (ncs < 0) { ncs = 0; }
            this.Skins.CurrentCardFrontSkin = ncs;
        }
        private void UpdateTableSkin(string newSkin)
        {
            int ncs = this.Skins.TableSkins.IndexOf(BaseCosmetics.TableSkins.IndexOf(newSkin));
            if (ncs < 0) { ncs = 0; }
            this.Skins.CurrentTableSkin = ncs;
        }
    }
}
