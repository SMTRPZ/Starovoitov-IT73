using Telegram.Bot;
using Telegram.Bot.Types;

namespace SeaBattle2TelegramServer.MessageHandlers
{
    public abstract class MessageHandler
    {
        protected readonly MessageHandler Successor;
        protected MessageHandler(MessageHandler successor)
        {
            Successor = successor;
        }
        public abstract void HandleMessage(Message message, TelegramSession session, TelegramBotClient bot);
    }
}