
namespace Poker
{
    public static class Literal
    {
        public static CommandWord Command = new CommandWord();
        public static SplitSymbol Split = new SplitSymbol();
        public static PointWord Point = new PointWord();
        public static TypeWord Type = new TypeWord();
    }
    public class SplitSymbol
    {
        public char Level1='/';
        public char Level2=',';
        public char Level3=' ';
    }
    public class CommandWord
    {
        public string Create="CREATE";
        public string Get = "GET";
        public string Update = "UPDATE";
        public string Leave = "LEAVE";
        public string Join = "JOIN";
        public RoomCommand Round= new RoomCommand();
    }
    public class RoomCommand 
    {
        public string Start = "START";
        public string Finish = "END";
        public string Move = "MOVE";
        public string Close = "CLOSE";
    }
    public class PointWord
    {
        public string Account="ACC";
        public string Room="ROOM";
        public string Shop="SHP";
    }
    public class TypeWord 
    {
        public string Name="N";
        public TypeOfSkinWord Skin= new TypeOfSkinWord();
        public TypeOfIdPrefix IdPrefix = new TypeOfIdPrefix();
    }
    public class TypeOfSkinWord 
    {
        public string Avatar="AS";
        public string CardBack="CBS";
        public string CardFront="CFS";
        public string Table="TS";
    }
    public class TypeOfIdPrefix 
    {
        public string Account="ac";
        public string Room="r";
    }
}
