using System;
using System.Linq;
using System.Text.RegularExpressions;
using SeaBattle2Lib.GameLogic;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SeaBattle2TelegramServer.MessageHandlers
{
    public class CoordinatesHandler:MessageHandler
    {
        public override void HandleMessage(Message message, TelegramSession session, TelegramBotClient bot)
        {
            if (message.Text == null)
            {
                bot.SendTextMessageAsync(message.From.Id, "Где текст, Лебовски?");
                Successor?.HandleMessage(message, session, bot);
                return;
            }

            string text = message.Text.Trim();
            int[] shotCoordinates = GetNumbersFromString(text);
            
            if (shotCoordinates.Length != 2)
            {
                bot.SendTextMessageAsync(message.From.Id, "Это не два числа");
                Successor?.HandleMessage(message, session, bot);
                return;
            }

            if (!session.Game.GameIsOn)
            {
                bot.SendTextMessageAsync(message.From.Id,
                    $"Игра ещё не началась. Не могу принять координаты. Отошлите мне {BotCommands.StartNewGameCommand}");
                return;
            }

            //TODO код плохо читается
            try
            {
                var playerShotCoordinates = new Coordinates(shotCoordinates[0], shotCoordinates[1]);
                var playerShotResult = session.ShootingForThePlayer(playerShotCoordinates);
                if (playerShotResult.Victory)
                {
                    SendPlayground(message, session, bot);
                    session.SendWinMessage(bot);
                    return;
                }


                var computerShotResult = session.ComputerShot();
                bot.SendTextMessageAsync(message.From.Id,
                    $"Компьютер сделал выстрел по координатам {computerShotResult.Coordinates}");
                if (computerShotResult.Victory)
                {
                    session.SendLoseMessage(bot);
                    SendPlayground(message, session, bot);
                    return;
                }
                SendPlayground(message, session, bot);
            }
            catch (ArgumentOutOfRangeException)
            {
                bot.SendTextMessageAsync(message.From.Id,
                    $"Неверные координаты. Вы можете стрелять только в диапазоне  (0-{session.Game.Player2Map.Width - 1}) (0-{session.Game.Player2Map.Height - 1})");
            }
            catch (Exception e)
            {
                bot.SendTextMessageAsync(message.From.Id, $"Не удалось сделать выстрел. Причина: {e.Message}");
            }
        }

        private static void SendPlayground(Message message, TelegramSession session, TelegramBotClient bot)
        {
            try
            {
                session.SendPlayground(message, bot);
            }
            catch (Exception e)
            {
                bot.SendTextMessageAsync(message.From.Id, $"Не удалось отправить картинку поля боя. {e.Message}");
            }
        }


        private int[] GetNumbersFromString(string text)
        {
            return Regex.Matches(text, "\\d+")
                    .Select(x => int.Parse(x.Value))
                    .ToArray();
        }
    }
}