using Newtonsoft.Json.Serialization;
using SeaBattle2TelegramServer.MessageHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SeaBattle2TelegramServer
{
    internal class MessageСonveyor:MessageHandler
    {
        private MessageHandler firstHandler;

        public void AddHandler(MessageHandler messageHandler)
        {
            if (firstHandler == null)
            {
                firstHandler = messageHandler;
                return;
            }
            
            var currentHandler = firstHandler;
            while (currentHandler.Successor!=null)
            {
                currentHandler = currentHandler.Successor;
            }

            currentHandler.Successor = messageHandler;

        }

        public override void HandleMessage(Message message, TelegramSession session, TelegramBotClient bot)
        {
            firstHandler.HandleMessage(message,session,bot);
        }
    }
}