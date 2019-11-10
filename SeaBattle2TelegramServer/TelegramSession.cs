using System;
using System.Threading;
using SeaBattle2Lib;
using SeaBattle2TelegramServer.Draw;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Game = SeaBattle2Lib.GameLogic.Game;

namespace SeaBattle2TelegramServer
{
    public class TelegramSession
    {
        private Game game;
        private bool GameIsOn = false;

        public Game GetGameCopy()
        {
            return game;
        }

        public void RecreateGame(int width, int height)
        {
            game = new Game(width, height);
            GameIsOn = true;
        }

        public void ShootingForThePlayer(Coordinates coordinates)
        {
            //Если игра начата, то сделать выстрел
            //Если игра не начата бросить ошибку
            game.Player1Shot(coordinates);
        }

        public bool TryEndGame()
        {
            if (!GameIsOn)
            {
                GameIsOn = false;
                return true;
            }

            return false;
        }

        public Coordinates PlayerAutoShot()
        {
            return game.Player1AutoShot();
        }
        
        public void SendPlayground(Message message, TelegramBotClient bot)
        {
            if (GameIsOn)
            {
                try
                {
                    var fileStream = DrawingEngine.GetPlaygrounds(this);
                    InputOnlineFile dichFile = new InputOnlineFile(fileStream);
                    bot.SendPhotoAsync(message.From.Id, dichFile, caption: "Игровое поле.").Wait();
                }
                catch (Exception e)
                {
                    bot.SendTextMessageAsync(message.From.Id,
                        $"Что-то навернулось при отправке картинки игрового поля. {e.Message}");
                }
            }
            else
            {
                bot.SendTextMessageAsync(message.From.Id, "Игра ещё не началась");
            }
           
            
        }
    }
}