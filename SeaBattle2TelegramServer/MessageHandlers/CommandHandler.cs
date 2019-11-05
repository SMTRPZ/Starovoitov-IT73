using Telegram.Bot;
using Telegram.Bot.Types;

namespace SeaBattle2TelegramServer.MessageHandlers
{
    class CommandHandler:MessageHandler
    {
        public override void HandleMessage(Message message, TelegramSession session, TelegramBotClient bot)
        {
            string text = message?.Text;
            text = text?.Trim();

            switch (text)
            {
                case "/start":
                    bot.SendTextMessageAsync(message.From.Id, 
                        $"Привет! Давай сыграем в \"Морской Бой\". Для старта игры отправь мне команду  {StartNewGameCommand}");
                    return;
                case StartNewGameCommand:
                    
                    int width = 10;
                    int height = 10;
                    //TODO попытка прочитать размеры полей в параметрах
                    session.RecreateGame(width, height);
                    bot.SendTextMessageAsync(message.From.Id, "Игра началась! Для выстрела просто отправьте два числа(координаты поля противника)");

                    Stub.SendPlayground(message, session, bot);

                    return;
                case "/end_game":
                    
                    bot.SendTextMessageAsync(message.From.Id, "Игра не начиналась.");
                    
//                    if (session.TryEndGame())
//                        bot.SendTextMessageAsync(message.From.Id, "Игра закончена");
//                    else
//                        bot.SendTextMessageAsync(message.From.Id, "Игра не начиналась.");

                    return;
                case "/auto_shot":
//                    (int x, int y) = session.PlayerAutoShot();
//                    PlayerShot(x,y,session,message);
//                    bot.SendTextMessageAsync(message.From.Id, $"Автоматический выстрел за игрока по координатам x:{x}, y:{y}.");
//                    SendPlaygrounds(message,session);

                    bot.SendTextMessageAsync(message.From.Id, "Я пока так не умею");
                    return;
                case "/show_playground":
//                    SendPlaygrounds(message, session);
                    bot.SendTextMessageAsync(message.From.Id, "Я пока так не умею");
                    return;
            }

            Successor?.HandleMessage(message, session, bot);

        }
        
        
        private const string StartInteractingWithBotCommand = "/start";
        private const string StartNewGameCommand = "/start_new_game";
        private const string ShowPlaygroundCommand = "/show_playground";
     
          
    }
}