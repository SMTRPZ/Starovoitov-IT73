using Telegram.Bot;
using Telegram.Bot.Types;

namespace SeaBattle2TelegramServer.MessageHandlers
{
    internal class MessageСonveyor:MessageHandler
    {
        private MessageHandler _firstHandler;

        public void AddHandler(MessageHandler messageHandler)
        {
            if (_firstHandler == null)
            {
                _firstHandler = messageHandler;
                return;
            }
            var currentHandler = _firstHandler;
            while (currentHandler.Successor!=null)
            {
                currentHandler = currentHandler.Successor;
            }
            currentHandler.Successor = messageHandler;
        }

        public override void HandleMessage(Message message, TelegramSession session, TelegramBotClient bot)
        {
            _firstHandler.HandleMessage(message,session,bot);
        }
    }
}