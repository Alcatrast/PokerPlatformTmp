using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Poker.AccountsMC;

namespace Poker.RoomsMC
{
    internal static class BaseRooms
    {
        private static List<Room> rooms;
        private static string currentRoomId;
        public static void Initializate()
        {
            rooms = new List<Room>();
            currentRoomId = "r0";
        }
        private static void ChangeCurrentRoomId()
        {
            string sid = string.Empty;
            for (int i = 1; i < currentRoomId.Length; i++)
            {
                sid += currentRoomId[i];
            }
            currentRoomId = "r" + Convert.ToString(int.Parse(sid) + 1);
        }

        public static string CreateRoom(string createrId, string createrPassword, int countAccounts, int startBank)
        {
            if (BaseAccounts.IsPasswordRight(createrId, createrPassword))
            {
                ChangeCurrentRoomId();
                rooms.Add(new Room(currentRoomId, countAccounts, startBank));
                if (rooms[rooms.Count - 1].Join(createrId, createrPassword)) { return rooms[rooms.Count - 1].RoomId; }
                else { rooms.RemoveAt(rooms.Count - 1); }
            }
            return string.Empty;
        }

    }
}
