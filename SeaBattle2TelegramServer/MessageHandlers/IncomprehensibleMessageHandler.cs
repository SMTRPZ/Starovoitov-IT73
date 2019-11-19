using Telegram.Bot;
using Telegram.Bot.Types;

namespace SeaBattle2TelegramServer.MessageHandlers
{
    public class IncomprehensibleMessageHandler:MessageHandler
    {
        public override void HandleMessage(Message message, TelegramSession session, TelegramBotClient bot)
        {
            bot.SendTextMessageAsync(message.From.Id, $"Отправьте {BotCommands.StartInteractingWithBotCommand}, чтобы понять как мной пользоваться");
        }
    }
}