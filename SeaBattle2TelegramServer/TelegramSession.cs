using System;
using SeaBattle2Lib.Experiments;
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
        public Game Game => _game;
        private Game _game;

        int gamerTelegramId;

        public TelegramSession(int gamerTelegramId)
        {
            _game = new Game();
            this.gamerTelegramId = gamerTelegramId;
        }

        public void RecreateGame(int width, int height)
        {
            _game = new Game(width, height);
        }

        public ShotResult ShootingForThePlayer(Coordinates coordinates)
        {
            if (_game.GameIsOn)
                return _game.Player1Shot(coordinates);
            else
                throw new Exception("Игра не начата. Куда ты стреляешь?");
        }
        public Coordinates PlayerAutoShot()
        {
            return _game.Player1AutoShot();
        }

        public ShotResult ComputerShot()
        {
            return _game.Player2AutoShot();
        }
        public bool TryEndGame()
        {
            if (_game.GameIsOn)
            {
                _game.EndGame();
                return true;
            }
            return false;
        }

      
        
        public void SendPlayground(Message message, TelegramBotClient bot)
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
            
            if (!_game.GameIsOn) bot.SendTextMessageAsync(message.From.Id, "Игровое поле для остановленой игры.");
        }

     
        public void SendWinMessage(TelegramBotClient bot)
        {
            bot.SendTextMessageAsync(gamerTelegramId, "Вы выиграли");
            bot.SendPhotoAsync(gamerTelegramId, "AgADAgADua0xG-oUoEr8BKx4UMI1K2h2wQ8ABAEAAwIAA3gAA1CVAAIWBA");
        }

        public void SendLoseMessage(TelegramBotClient bot)
        {
            bot.SendTextMessageAsync(gamerTelegramId, "Вы проиграли");
            bot.SendPhotoAsync(gamerTelegramId, "AgADAgADu60xG-oUoErMymSlk94eQdPwtw8ABAEAAwIAA3gAAzv6BgABFgQ");
        }

        
    }
}