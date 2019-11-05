using System;
using System.Linq;
using System.Text.RegularExpressions;
using SeaBattle2Lib;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SeaBattle2TelegramServer.MessageHandlers
{
    /// <summary>
    /// Блокирует сообщение, если в нём есть текст
    /// </summary>
    public class CoordinatesHandler:MessageHandler
    {
        public override void HandleMessage(Message message, TelegramSession session, TelegramBotClient bot)
        {
            //попытаться распознать координаты и сделать выстрел
            if (message.Text != null)
            {
                string text = message.Text.Trim();
                int[] shotCoordinates = Regex.Matches(text, "\\d+")
                    .Select(x => int.Parse(x.Value))
                    .ToArray();

                 
                
                //два числа в строке
                if (shotCoordinates.Length == 2)
                {
                    
                    bot.SendTextMessageAsync(message.From.Id,
                        $"Сударь, похоже, что вы пытаетесь выстрелить по координатам {shotCoordinates[0]} {shotCoordinates[1]}. Но нет.");
                    bot.SendTextMessageAsync(message.From.Id, "Ну я попытаюсь.");
                
                    Coordinates coordinates = new Coordinates(shotCoordinates[0], shotCoordinates[1]);

                    try
                    {
                        session.ShootingForThePlayer(coordinates);
                    }
                    catch (Exception e)
                    {
                        bot.SendTextMessageAsync(message.From.Id, $"Не удалось сделать выстрел. Причина: {e.Message}");
                    }
                    
                }
                else
                {
                    //Какого хера тут не два числа?
                    bot.SendTextMessageAsync(message.From.Id, "Сударь, мне нужно два чила, чтобы сделать выстрел.");
                }    
                return;
            }
            Successor?.HandleMessage(message,session,bot);
            
            
        }
        
    }
}