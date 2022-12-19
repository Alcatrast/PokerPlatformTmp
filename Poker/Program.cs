using Poker.AccountsMC;
using Poker.CosmeticsMC;
using Poker.RoomsMC;
using System.Net;
using System.Text;

namespace Poker
{
    public class Program
    {
        const string accountDir = "accs";
        const string url="http://localhost:8080/";
        static async Task Main(string[] args)
        {
            
            if (!Directory.Exists(accountDir))
            {
                Directory.CreateDirectory(accountDir);
            }
            BaseAccounts.Initialaize(accountDir);
            BaseAccounts.BuildFromFiles();
            BaseRooms.Initialaize();
            BaseCosmetics.Initializate();

            HttpListener server = new HttpListener();
            server.Prefixes.Add(url);
            server.Start();
            Console.WriteLine("server started");

            while (true)
            {
                var context = await server.GetContextAsync();
                StreamReader sr = new StreamReader(context.Request.InputStream);
                string request = sr.ReadToEnd();  
                string response = MainController.ProcessRequest(request);
                byte[] buffer= Encoding.UTF8.GetBytes(response);
                context.Response.ContentLength64= buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                sr.Close();
            }
            
        }
    }
}