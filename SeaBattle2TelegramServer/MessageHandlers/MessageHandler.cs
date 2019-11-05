using Telegram.Bot;
using Telegram.Bot.Types;

namespace SeaBattle2TelegramServer.MessageHandlers
{
    public abstract class MessageHandler
    {
        public MessageHandler Successor;
        public abstract void HandleMessage(Message message, TelegramSession session, TelegramBotClient bot);
    }
}