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

        readonly int _gamerTelegramId;

        public TelegramSession(int gamerTelegramId)
        {
            _game = new Game();
            _gamerTelegramId = gamerTelegramId;
        }

        public void RecreateGame(int width, int height)
        {
            _game = new Game(width, height);
        }

        public ShotResult ShootingForThePlayer(Coordinates coordinates)
        {
            if (_game.GameIsOn)
                return _game.PlayerShot(Player.First, coordinates);
            
            throw new Exception("Игра не начата. Куда ты стреляешь?");
        }
        public ShotResult PlayerAutoShot()
        {
            return _game.PlayerAutoShot(Player.First);
        }

        public ShotResult ComputerShot()
        {
            return _game.PlayerAutoShot(Player.Second);
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
            bot.SendTextMessageAsync(_gamerTelegramId, "Вы выиграли");
            bot.SendPhotoAsync(_gamerTelegramId, "AgADAgADua0xG-oUoEr8BKx4UMI1K2h2wQ8ABAEAAwIAA3gAA1CVAAIWBA");
        }

        public void SendLoseMessage(TelegramBotClient bot)
        {
            bot.SendTextMessageAsync(_gamerTelegramId, "Вы проиграли");
            bot.SendPhotoAsync(_gamerTelegramId, "AgADAgADu60xG-oUoErMymSlk94eQdPwtw8ABAEAAwIAA3gAAzv6BgABFgQ");
        }

        
    }
}