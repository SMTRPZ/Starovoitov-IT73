using System;
using System.Collections.Generic;
using System.IO.Pipes;
using SeaBattle2Lib.Exceptions;
using SeaBattle2Lib.Shooting;

namespace SeaBattle2Lib.GameLogic
{
    public struct Game
    {
        public Map Player1Map => _player1Map;
        public Map Player2Map => _player2Map;
        
        private Map _player1Map;
        private Map _player2Map;
        
        public bool GameIsOn { get; private set; }
        private bool _firstPlayerHasToShoot;
        

        public Game(int mapWidth, int mapHeight)
        {
            GameIsOn = true;
            _firstPlayerHasToShoot = true;
            _player1Map = Mapholder.GenerateFilledMap(mapWidth, mapHeight);
            _player2Map = Mapholder.GenerateFilledMap(mapWidth, mapHeight);
        }


        public bool Player1Shot(Coordinates coordinates)
        {
            if (_firstPlayerHasToShoot)
            {
                bool isWin = PlayerShot(1, coordinates);
                _firstPlayerHasToShoot = !_firstPlayerHasToShoot;
                return isWin;
            }
            else
                throw new OtherPlayerMustShootException("Player1Shot");
        }

        public bool Player2Shot(Coordinates coordinates)
        {
            if (!_firstPlayerHasToShoot)
            {
                bool isWin = PlayerShot(2, coordinates);
                _firstPlayerHasToShoot = !_firstPlayerHasToShoot;
                return isWin;
            }
            else
                throw new OtherPlayerMustShootException("Player2Shot");
        }

        private bool PlayerShot(int playerNumber, Coordinates coordinates)
        {
            if (coordinates.X < 0 || Player2Map.Width <= coordinates.X)
                throw new ArgumentOutOfRangeException(nameof(coordinates));

            if (coordinates.Y < 0 || Player2Map.Height <= coordinates.Y)
                throw new ArgumentOutOfRangeException(nameof(coordinates));

            switch (playerNumber)
            {
                case 1:
                    ChangeCell(ref _player2Map, coordinates);
                    //Перекрасить карту
                    RecolorMap(ref _player2Map);
                    break;
                case 2 :
                    ChangeCell(ref _player1Map, coordinates);
                    //Перекрасить карту
                    RecolorMap(ref _player1Map);
                    break;
                default:
                    throw new Exception("недопустимый номер игрока");
            }

            

            //TODO проверка на победу
            return IsWin(playerNumber);
        }

        private void RecolorMap(ref Map map)
        {
            List<Coordinates> damagedParts = new List<Coordinates>();

            for(int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    if (map.CellsStatuses[x, y] == CellStatus.DamagedPartOfShip)
                        damagedParts.Add(new Coordinates(x, y));
                }
            }

            //Закрасить все подбитые части в убитые корабли

            //Пройтись по всем "целым" и "подбитым" частям несколько раз, закрашивая части "уничтоженные" части рядом в "подтибые" части

            //Пройтись по карте столько раз, чтобы она перестала меняться

            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    
                    


                }
            }

        }

        private bool IsWin(int playerNumber)
        {
            //Просмотреть карту, по которой стреляли
            //Если всё убито, то этот игрок убит
            switch(playerNumber)
            {
                case 1:
                    return AllShipsAreKilled(ref _player2Map);
                case 2:
                    return AllShipsAreKilled(ref _player1Map);
                default:
                    throw new Exception("Недопустимый номер игрока");
            }
        }

        private bool AllShipsAreKilled(ref Map map)
        {
            for (int x = 0; x < map.Width; x++)
            {
                for(int y = 0; y < map.Height; y++)
                {
                    if(map.CellsStatuses[x,y]==CellStatus.DamagedPartOfShip || map.CellsStatuses[x, y] == CellStatus.PartOfShip)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void ChangeCell(ref Map map, Coordinates coordinates)
        {
            switch (map.CellsStatuses[coordinates.X, coordinates.Y])
            {
                case CellStatus.Water:
                    map.CellsStatuses[coordinates.X, coordinates.Y] = CellStatus.HittedWater;
                    break;
                case CellStatus.DamagedPartOfShip:
                    //не нужно менять
                    break;
                case CellStatus.PartOfShip:
                    map.CellsStatuses[coordinates.X, coordinates.Y] = CellStatus.DamagedPartOfShip;
                    break;
                case CellStatus.HittedWater:
                    //не нужно менять
                    break;
                case CellStatus.DestroyedShip:
                    //не нужно менять
                    break;
            }

        }
        
        public Coordinates Player1AutoShot()
        {
            var coordinates = Ai.MakeShot(ref _player2Map);
            Player1Shot(coordinates);
            return coordinates;
        }
        
        public bool Player2AutoShot()
        {
            var coordinates = Ai.MakeShot(ref _player1Map);
            return Player2Shot(coordinates);
        }

        public void EndGame()
        {
            if (GameIsOn)
                GameIsOn = false;
            else
                throw new Exception("Игра уже остановлена");
        }
    }
}