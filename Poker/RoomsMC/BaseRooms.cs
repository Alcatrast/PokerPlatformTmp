﻿using Poker.AccountsMC;

namespace Poker.RoomsMC
{
    internal static class BaseRooms
    {
        private static List<Room> rooms;
        private static string currentRoomId;
        public static void Initialaize()
        {
            rooms = new List<Room>();
            currentRoomId = Literal.Type.IdPrefix.Room + "0";

        }
        private static void ChangeCurrentRoomId()
        {
            string sid = string.Empty;
            for (int i = Literal.Type.IdPrefix.Room.Length; i < currentRoomId.Length; i++)
            {
                sid += currentRoomId[i];
            }
            currentRoomId = Literal.Type.IdPrefix.Room + Convert.ToString(int.Parse(sid) + 1);
        }
        private static int GetIndex(string id)
        {
            if (id != null)
            {
                if (id.Length > 1)
                {
                    if (id.Substring(0, Literal.Type.IdPrefix.Room.Length) == Literal.Type.IdPrefix.Room)
                    {
                        string sid = string.Empty;
                        for (int i = Literal.Type.IdPrefix.Room.Length; i < id.Length; i++)
                        {
                            sid += id[i];
                        }
                        int index = 0;
                        int.TryParse(sid, out index);
                        index--;
                        if (index < 0 || index >= rooms.Count) { return 0; }
                        return index;
                    }
                }
            }
            return 0;
        }

        public static string CreateRoom(string createrId, string createrPassword, int countAccounts, int startBank)
        {
            if (BaseAccounts.IsPasswordRight(createrId, createrPassword) && startBank >= 200 && countAccounts > 1)
            {
                ChangeCurrentRoomId();
                rooms.Add(new Room(currentRoomId, countAccounts, startBank));
                if (rooms[rooms.Count - 1].Join(createrId, createrPassword)) { return rooms[rooms.Count - 1].RoomId; }
                else { rooms.RemoveAt(rooms.Count - 1); }
            }
            return string.Empty;
        }

        public static bool Join(string roomId, string accountId, string accountPassword)
        {
            return rooms[GetIndex(roomId)].Join(accountId, accountPassword);

        }
        public static bool Leave(string accountId, string accountPassword)
        {
            return rooms[GetIndex(BaseAccounts.GetCurrentRoom(accountId))].Leave(accountId, accountPassword);
        }
        public static bool Update(string accountId, string accountPassword, string function)
        {
            return rooms[GetIndex(BaseAccounts.GetCurrentRoom(accountId))].Update(accountId, accountPassword, function);
        }
        public static RoomResponse Get(string accountId, string accountPassword)
        {
            return rooms[GetIndex(BaseAccounts.GetCurrentRoom(accountId))].Get(accountId, accountPassword);
        }

        public static RoomResponse ProcessingRequest(string accountId, string accountPassword, string function)
        {
            if (function != null)
            {
                if (function.Length > 3)
                {
                    string[] command = function.Split(Literal.Split.Level2);
                    if (command.Length > 0)
                    {
                        if (command[0] == Literal.Command.Create)
                        {
                            if (command.Length >= 3)
                            {
                                int p1 = 0, p2 = 0;
                                int.TryParse(command[1], out p1);
                                int.TryParse(command[2], out p2);
                                if (CreateRoom(accountId, accountPassword, p1, p2) != string.Empty)
                                {
                                    return Get(accountId, accountPassword);
                                }
                                else
                                {
                                    return new RoomResponse();
                                }
                            }
                            else if (command[0] == Literal.Command.Join)
                            {
                                if (command.Length >= 2)
                                {
                                    if (Join(command[1], accountId, accountPassword))
                                    {
                                        return Get(accountId, accountPassword);
                                    }
                                    else
                                    {
                                        return new RoomResponse();
                                    }
                                }
                            }
                            else if (command[0] == Literal.Command.Leave)
                            {
                                if (Leave(accountId, accountPassword))
                                {
                                    return new RoomResponse();
                                }
                                else
                                {
                                    return Get(accountId, accountPassword);
                                }
                            }
                            else if (command[0] == Literal.Command.Update)
                            {
                                if (command.Length >= 2)
                                {
                                    if (Update(accountId, accountPassword, command[1])) { return Get(accountId, accountPassword); }
                                    else { return Get(accountId, accountPassword); }
                                }
                            }
                            else if (command[0] == Literal.Command.Get)
                            {
                                return Get(accountId, accountPassword);
                            }
                        }
                    }
                }
            }
            return new RoomResponse();
        }
    }
}
