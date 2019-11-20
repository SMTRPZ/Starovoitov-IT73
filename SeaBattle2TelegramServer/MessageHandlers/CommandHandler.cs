using Telegram.Bot;
using Telegram.Bot.Types;

namespace SeaBattle2TelegramServer.MessageHandlers
{
    class CommandHandler:MessageHandler
    {
        public override void HandleMessage(Message message, TelegramSession session, TelegramBotClient bot)
        {
            string text = message?.Text?.Trim();
            switch (text)
            {
                case BotCommands.StartInteractingWithBotCommand:
                    bot.SendTextMessageAsync(message.From.Id, 
                        $"Привет! Давай сыграем в \"Морской Бой\". Для старта игры отправь мне команду  {BotCommands.StartNewGameCommand}");
                    return;
                case BotCommands.StartNewGameCommand:
                    //TODO снять ограничение на размер карты
                    int width = 10;
                    int height = 10;
                    session.RecreateGame(width, height);
                    bot.SendTextMessageAsync(message.From.Id, "Игра началась! Для выстрела просто отправьте два числа (координаты поля противника)");
                    session.SendPlayground(message, bot);
                    return;
                case BotCommands.EndGameCommand:
                    if (session.TryEndGame())
                        bot.SendTextMessageAsync(message.From.Id, "Игра закончена");
                    else
                        bot.SendTextMessageAsync(message.From.Id, "Игра не начиналась.");
                    return;
                case BotCommands.GetGameStatusCommand:
                    if (session.Game.GameIsOn)
                        bot.SendTextMessageAsync(message.From.Id, "Игра идёт");
                    else
                        bot.SendTextMessageAsync(message.From.Id, "Игра не идёт");
                    return;
                case BotCommands.AutoShotCommand:
                    //TODO убрать реализацию отсюда
                    //TODO нужно вынести её в отдельный класс
                    if(!session.Game.GameIsOn){
                        bot.SendTextMessageAsync(message.From.Id, $"Игра ещё не началась. ");
                        return;
                    }
                    var coordinates = session.PlayerAutoShot().Coordinates;
                    session.ComputerShot();
                    bot.SendTextMessageAsync(message.From.Id, $"Автоматический выстрел за игрока по координатам x:{coordinates.X}, y:{coordinates.Y}.");
                    session.SendPlayground(message, bot);
                    return;
                case BotCommands.ShowPlaygroundCommand:
                    session.SendPlayground(message, bot);
                    return;
            }
            Successor?.HandleMessage(message, session, bot);
        }
    }
}