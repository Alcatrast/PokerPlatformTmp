using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
using Poker.PokerGameMC;
using Poker.RoomsMC;

namespace Poker
{
    public class Program
    {
        static void Main(string[] args)
        {

            
            //BaseRooms.Initializate();

            PokerController pc = new PokerController(4, 200, 52, new List<List<int>>());
            pc.StartRound(4);
            GameState gameState = pc.State;
            StreamWriter sw = new StreamWriter(@"C:\temp\1.txt");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameState));
            xmlSerializer.Serialize(sw, gameState);
            sw.Close();
            Console.WriteLine(gameState.roundStage);
            int i = 1;
            while (true)
            {
                i++;
                string[] s = Console.ReadLine().Split();
                if (s[0] == "S") { pc.StartRound(pc.State.dealer); }
                else if (s[0] == "M")
                {
                    pc.Move(pc.State.nextMovePlayerId, int.Parse(s[1]));
                }
                else if (s[0] == "E")
                {
                    pc.EndBreak(pc.State.dealer);
                }
                gameState = pc.State;
                sw = new StreamWriter($@"C:\temp\{i}.txt");
                xmlSerializer = new XmlSerializer(typeof(GameState));
                xmlSerializer.Serialize(sw, gameState);
                sw.Close();
                Console.WriteLine(gameState.roundStage);

            }
        }
    }
}