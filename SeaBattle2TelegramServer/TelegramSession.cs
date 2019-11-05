using SeaBattle2Lib;
using SeaBattle2Lib.GameLogic;

namespace SeaBattle2TelegramServer
{
    public class TelegramSession
    {
        private Game game;

        public Game GetGameCopy()
        {
            return game;
        }

        public void RecreateGame(int width, int height)
        {
            game = new Game(width, height);
        }

        public void ShootingForThePlayer(Coordinates coordinates)
        {
            //Если игра начата, то сделать выстрел
            //Если игра не начата бросить ошибку
            game.Player1Shot(coordinates);
        }
    }
}