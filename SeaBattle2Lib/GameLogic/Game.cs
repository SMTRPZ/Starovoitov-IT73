using System;

namespace SeaBattle2Lib.GameLogic
{
    public struct Game
    {
        public Map Player1Map { get; private set; }
        public Map Player2Map { get; private set; }

        public Game(int mapWidth, int mapHeight)
        {
            Player1Map = Mapholder.GenerateFilledMap(mapWidth, mapHeight);
            Player2Map = Mapholder.GenerateFilledMap(mapWidth, mapHeight);
        }


        public void Player1Shot(Coordinates coordinates)
        {
            PlayerShot(1, coordinates);
        }

        public void Player2Shot(Coordinates coordinates)
        {
            PlayerShot(2, coordinates);
        }

        private void PlayerShot(int playerNumber, Coordinates coordinates)
        {
            if (coordinates.X < 0 || Player2Map.Width <= coordinates.X)
                throw new ArgumentOutOfRangeException(nameof(coordinates));

            if (coordinates.Y < 0 || Player2Map.Height <= coordinates.Y)
                throw new ArgumentOutOfRangeException(nameof(coordinates));

            switch (Player2Map.CellsStatuses[coordinates.X, coordinates.Y])
            {
                case CellStatus.Water:
                    Player2Map.CellsStatuses[coordinates.X, coordinates.Y] = CellStatus.HittedWater;
                    break;
                case CellStatus.DamagedPartOfShip:
                    //не нужно менять
                    break;
                case CellStatus.PartOfShip:
                    Player2Map.CellsStatuses[coordinates.X, coordinates.Y] = CellStatus.DamagedPartOfShip;
                    break;
                case CellStatus.HittedWater:
                    //не нужно менять
                    break;
                case CellStatus.DestroyedShip:
                    //не нужно менять
                    break;
            }

            //TODO проверка на победу
        }
    
    

        public Coordinates Player1AutoShot()
        {
            //TODO вызвать AI
            var coordinates = new Coordinates(1, 5);
            Player1Shot(coordinates);
            return coordinates;
        }
        public Coordinates Player2AutoShot()
        {
            //TODO вызвать AI
            var coordinates = new Coordinates(1, 2);
            Player2Shot(coordinates);
            return coordinates;
        }
    }
}