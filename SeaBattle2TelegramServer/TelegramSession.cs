using System;
using System.Threading;
using SeaBattle2Lib;
using SeaBattle2Lib.GameLogic;
using SeaBattle2TelegramServer.Draw;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Game = SeaBattle2Lib.GameLogic.Game;

namespace SeaBattle2TelegramServer
{
    public class TelegramSession
    {
        public Game Game { get; private set; }
        
        public void RecreateGame(int width, int height)
        {
            Game = new Game(width, height);
        }

        public void ShootingForThePlayer(Coordinates coordinates)
        {
            if (Game.GameIsOn)
                Game.Player1Shot(coordinates);
            else
                throw new Exception("Игра не начата. Куда ты стреляешь?");
        }

        public bool TryEndGame()
        {
            if (Game!=null && Game.GameIsOn)
            {
                Game.EndGame();
                return true;
            }
            return false;
        }

        public Coordinates PlayerAutoShot()
        {
            return Game.Player1AutoShot();
        }
        
        public void SendPlayground(Message message, TelegramBotClient bot)
        {
            if (Game!=null&&Game.GameIsOn)
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

        public void ComputerShot()
        {
            Game.Player2AutoShot();
        }
    }
}