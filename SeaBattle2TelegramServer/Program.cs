using System;
using System.Threading.Tasks;

namespace SeaBattle2TelegramServer
{
    class Program
    {
        static void Main(string[] args)
        {
            new TelegramNegotiator().StartConversation();
            Task.Delay(int.MaxValue).Wait();
        }
    }
}