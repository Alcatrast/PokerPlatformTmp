using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.RoomsMC;
namespace Poker
{
    internal static class MainController
    {
        public static void ProcessRequest(string request)
        {
            if (request != null)
            {
                if (request.Length > 3)
                {
                    string[] command = request.Split('/');
                    if(command.Length > 0)
                    {
                        if (command[0] == "SHOP")
                        {
                            RequestShop(command);
                        }
                        else if (command[0]=="ROOM" && command.Length>=4){ RequestRoom(command); }
                    }
                }
            }
        }
        private static void GetEmptyResponse() { }
        private static void RequestShop(string[] command)
        { 
           throw new NotImplementedException();
        }
        private static void RequestAccount(string[] command)
        {

        }
        private static RoomResponse RequestRoom(string[] command)
        {
          return BaseRooms.ProcessingRequest(command[2], command[3], command[1]);
        }
    }
}
