using System;

namespace SeaBattle2Lib.GameLogic
{
    public struct Game
    {
        private Map Player1Map;
        private Map Player2Map;

        public Game(int mapWidth,int mapHeight)
        {
            Player1Map = Mapholder.GenerateFilledMap(mapWidth, mapHeight);
            Player2Map = Mapholder.GenerateFilledMap(mapWidth, mapHeight);
        }

    
        public void Player1Shot(Coordinates coordinates)
        {

        }
        public void Player2Shot(Coordinates coordinates)
        {

        }
        public void Player1AutoShot()
        {

        }
        public void Player2AutoShot()
        {

        }
    }
}