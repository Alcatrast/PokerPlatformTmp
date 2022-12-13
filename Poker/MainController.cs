using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Poker.AccountsMC;
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
                        else if (command[0] == "ACC" && command.Length >= 4) { RequestAccount(command); }
                    }
                }
            }
        }
        private static string SerializateToXml(object response) 
        {
            string res = "ERROR";
            if (response != null)
            {
                res = "UNCORRECT_TYPE";
                StringWriter sw = new StringWriter();
                XmlSerializer xs;
                if (response is AccountResponse ar)
                {
                    xs=new XmlSerializer(typeof(AccountResponse));
                    xs.Serialize(sw, ar);
                    res=sw.ToString();
                }else if(response is RoomResponse rr) {
                    xs = new XmlSerializer(typeof(RoomResponse));
                    xs.Serialize(sw, rr);
                    res = sw.ToString();
                }
                sw.Close();
            }
            return res;
        }
        private static void RequestShop(string[] command)
        { 
           throw new NotImplementedException();
        }
        private static AccountResponse RequestAccount(string[] command)
        {
            return BaseAccounts.ProcessingRequest(command[2], command[3], command[1]);
        }
        private static RoomResponse RequestRoom(string[] command)
        {
          return BaseRooms.ProcessingRequest(command[2], command[3], command[1]);
        }
    }
}
//
