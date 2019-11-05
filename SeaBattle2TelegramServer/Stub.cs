using System;
using SeaBattle2TelegramServer.Draw;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace SeaBattle2TelegramServer
{
    public static class Stub
    {
        public static void SendPlayground(Message message, TelegramSession session, TelegramBotClient bot)
        {
            try
            {
                var fileStream = DrawingEngine.GetPlaygrounds(session);
                InputOnlineFile dichFile = new InputOnlineFile(fileStream);
                bot.SendPhotoAsync(message.From.Id, dichFile, caption: "Игровое поле.").Wait();
            }
            catch (Exception e)
            {
                bot.SendTextMessageAsync(message.From.Id,
                    $"Что-то навернулось при отправке картинки игрового поля. {e.Message}");
            }
            
        }
    }
}