using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        else if (command[0]=="ROOM"){ RequestRoom(command); }
                    }
                }
            }
        }
        private static void GetEmptyResponse() { }
        private static void RequestShop(string[] command)
        {
            throw new NotImplementedException();
        }
        private static void RequestRoom(string[] command)
        {
            throw new NotImplementedException();
        }
    }
}
