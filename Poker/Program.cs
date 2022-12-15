using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
using Poker.AccountsMC;
using Poker.CosmeticsMC;
using Poker.PokerGameMC;
using Poker.RoomsMC;

namespace Poker
{
    public class Program
    {
        static void Main(string[] args)
        {

            BaseAccounts.Initialaize(@"C:\temp\acc");
            BaseRooms.Initialaize();
            BaseCosmetics.Initializate();
            while (true)
            {
                Console.WriteLine(MainController.ProcessRequest(Console.ReadLine()));
            }
            
        }
    }
}