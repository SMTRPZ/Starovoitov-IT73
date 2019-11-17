using SeaBattle2Lib;
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
                case StartInteractingWithBotCommand:
                    bot.SendTextMessageAsync(message.From.Id, 
                        $"Привет! Давай сыграем в \"Морской Бой\". Для старта игры отправь мне команду  {StartNewGameCommand}");
                    return;
                case StartNewGameCommand:
                    int width = 10;
                    int height = 10;
                    session.RecreateGame(width, height);
                    bot.SendTextMessageAsync(message.From.Id, "Игра началась! Для выстрела просто отправьте два числа (координаты поля противника)");
                    session.SendPlayground(message, bot);
                    return;
                case "/end_game":
                    if (session.TryEndGame())
                        bot.SendTextMessageAsync(message.From.Id, "Игра закончена");
                    else
                        bot.SendTextMessageAsync(message.From.Id, "Игра не начиналась.");
                    return;
                case "/game_status":
                    if (session.Game.GameIsOn)
                        bot.SendTextMessageAsync(message.From.Id, "Игра идёт");
                    else
                        bot.SendTextMessageAsync(message.From.Id, "Игра не идёт");
                    return;
                case "/auto_shot":
                    if(!session.Game.GameIsOn){
                        bot.SendTextMessageAsync(message.From.Id, $"Игра ещё не началась. соре");
                        return;
                    }
                    var coordinates = session.PlayerAutoShot();
                    session.ComputerShot();
                    bot.SendTextMessageAsync(message.From.Id, $"Автоматический выстрел за игрока по координатам x:{coordinates.X}, y:{coordinates.Y}.");
                    session.SendPlayground(message, bot);
                    return;
                case ShowPlaygroundCommand:
                    session.SendPlayground(message, bot);
                    return;
            }

            Successor?.HandleMessage(message, session, bot);
        }
        
        private const string StartNewGameCommand = "/start_new_game";
        private const string ShowPlaygroundCommand = "/show_playground";
        private const string StartInteractingWithBotCommand = "/start";
    }
}