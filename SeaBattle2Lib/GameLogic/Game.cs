using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using SeaBattle2Lib.Exceptions;
using SeaBattle2Lib.Experiments;
using SeaBattle2Lib.Shooting;

namespace SeaBattle2Lib.GameLogic
{
    public struct Game
    {
        public Map Player1Map => _player1Map;
        public Map Player2Map => _player2Map;
        
        private Map _player1Map;
        private Map _player2Map;
        private Player playerWhoseTurnToShoot;
        
        public bool GameIsOn { get; private set; }
        public Player? Winner { get; private set; }
        public Game(int mapWidth, int mapHeight)
        {
            GameIsOn = true;
            playerWhoseTurnToShoot = Player.First;
            _player1Map = Mapholder.Mapholder.GenerateFilledMap(mapWidth, mapHeight);
            _player2Map = Mapholder.Mapholder.GenerateFilledMap(mapWidth, mapHeight);
            Winner = null;
        }
       
        
        public ShotResult PlayerShot(Player player, Coordinates coordinates)
        {
            if (player != playerWhoseTurnToShoot)
                throw new OtherPlayerMustShootException(player.ToString());

            if (coordinates.X < 0 || Player2Map.Width <= coordinates.X)
                throw new ArgumentOutOfRangeException(nameof(coordinates));

            if (coordinates.Y < 0 || Player2Map.Height <= coordinates.Y)
                throw new ArgumentOutOfRangeException(nameof(coordinates));

            switch (player)
            {
                case Player.First:
                    ChangeCell(ref _player2Map, coordinates);
                    RecolorMap(ref _player2Map);
                    break;
                case Player.Second:
                    ChangeCell(ref _player1Map, coordinates);
                    RecolorMap(ref _player1Map);
                    break;
                default:
                    throw new Exception("недопустимый номер игрока");
            }
            if (player == Player.First)
                playerWhoseTurnToShoot = Player.Second;
            else
                playerWhoseTurnToShoot = Player.First;
            
            bool isWin = IsWin(player);
            if (isWin)
            {
                Winner = player;
                GameIsOn = false;
            }
            return new ShotResult(isWin, coordinates);
        }
        private void RecolorMap(ref Map map)
        {
            for(int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    if (map.CellsStatuses[x, y] == CellStatus.DamagedPartOfShip)
                        map.CellsStatuses[x, y] = CellStatus.DestroyedShip;
                }
            }


            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    if (map.CellsStatuses[x, y] == CellStatus.PartOfShip)
                    {
                        TryRedrawDestroyedCell(ref map, new Coordinates(x+1,y));
                        TryRedrawDestroyedCell(ref map, new Coordinates(x-1,y));
                        TryRedrawDestroyedCell(ref map, new Coordinates(x,y+1));
                        TryRedrawDestroyedCell(ref map, new Coordinates(x,y-1));
                    }
                }
            }
        }
        private void TryRedrawDestroyedCell(ref Map map, Coordinates coordinates)
        {
            if (!map.CoordinatesAllowed(coordinates))
                return;

            if (map.CellsStatuses[coordinates.X, coordinates.Y] == CellStatus.DestroyedShip)
                map.CellsStatuses[coordinates.X, coordinates.Y] = CellStatus.DamagedPartOfShip;
        }
        private bool IsWin(Player player)
        {
            //Просмотреть карту, по которой стреляли
            //Если всё убито, то этот игрок убит
            switch(player)
            {
                case Player.First:
                    return AllShipsAreKilled(ref _player2Map);
                case Player.Second:
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
        
        public ShotResult PlayerAutoShot(Player player)
        {
            Coordinates coordinates ;
            switch (player)
            {
                case Player.First:
                    coordinates = NotAi.MakeShot(ref _player2Map);
                    break;
                case Player.Second:
                    coordinates = NotAi.MakeShot(ref _player1Map);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(player), player, null);
            }
            var shotResult = PlayerShot(player, coordinates);
            return shotResult;
        }
        
        public void EndGame()
        {
            if (GameIsOn)
                GameIsOn = false;
            else
                throw new Exception("Игра уже остановлена");
        }
    }

    public enum Player
    {
        First,
        Second
    }
}