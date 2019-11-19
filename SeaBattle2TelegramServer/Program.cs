using System.Threading.Tasks;

namespace SeaBattle2TelegramServer
{
    static class Program
    {
        static void Main()
        {
            string botToken = "909771267:AAHfdJ5NN2efYFRFEzAaQFar757qQwSwlrA";
            new TelegramNegotiator().StartConversation(botToken);
            Task.Delay(int.MaxValue).Wait();
        }
    }
}