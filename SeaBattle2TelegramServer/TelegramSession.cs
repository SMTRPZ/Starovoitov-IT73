using SeaBattle2Lib;
using SeaBattle2Lib.GameLogic;

namespace SeaBattle2TelegramServer
{
    public class TelegramSession
    {
        private Game game;

        public void RecreateGame(int width, int height)
        {
            game = new Game(width, height);
        }

        public void ShootingForThePlayer(Coordinates coordinates)
        {
            throw new System.NotImplementedException();
        }
    }
}