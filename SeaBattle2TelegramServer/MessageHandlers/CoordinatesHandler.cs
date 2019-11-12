﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using SeaBattle2Lib;
using SeaBattle2Lib.GameLogic;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SeaBattle2TelegramServer.MessageHandlers
{
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
                    Coordinates coordinates = new Coordinates(shotCoordinates[0], shotCoordinates[1]);
                    try
                    {
                        session.ShootingForThePlayer(coordinates);
                        try
                        {
                            session.SendPlayground(message, bot);
                        }
                        catch (Exception e)
                        {
                            bot.SendTextMessageAsync(message.From.Id,
                                $"Не удалось отправить акртинку поля боя. {e.Message}");
                        }
                    }
                    catch (ArgumentOutOfRangeException argumentOutOfRangeException)
                    {
                        bot.SendTextMessageAsync(message.From.Id, $"Неверные координаты. Вы можете стрелять только в диапазоне  (0-{session.Game.Player2Map.Width-1}) (0-{session.Game.Player2Map.Height-1})");
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