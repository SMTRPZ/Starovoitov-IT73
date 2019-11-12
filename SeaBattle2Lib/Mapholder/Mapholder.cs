using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.SymbolStore;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using SeaBattle2Lib.Exceptions;
using SeaBattle2Lib.GameLogic;

namespace SeaBattle2Lib
{
  
    public static class Mapholder
    {
        public const double CoverageArea = 0.2;
        public const int NumberOfAttemptsToRecreateTheMap = 15;

        public static void StupidFillOutTheMap(ref Map map, Random random = null)
        {
            int area = map.Height * map.Width;
            if(random==null)
                random=new Random();
            
            //Меньше одной клетки для вставки корабля
            if (CoverageArea * area < 1)
                throw new MapSizeIsTooSmallException();

            int numberOfFailedAttempts = 0;
            
            int numberOfCellsLeftToFill = (int) Math.Floor(CoverageArea *area) ;
            while(numberOfCellsLeftToFill != 0){
                if( numberOfFailedAttempts >1000)
                    break;
                var coordinates = new Coordinates(random.Next(map.Width), random.Next(map.Height));
                if (!CanInsertAShip(ref map, coordinates))
                {
                    numberOfFailedAttempts++;
                    continue;
                }
                map.CellsStatuses[coordinates.X, coordinates.Y] = CellStatus.PartOfShip;
                numberOfCellsLeftToFill--;
            }

            if (numberOfCellsLeftToFill != 0)
                throw new Exception("Ошибка заполнения карты");
        }

        public static Map GenerateFilledMap(int width, int height)
        {
            var map = new Map(width, height);
//            StupidFillOutTheMap(ref map);
            map.CellsStatuses[0, 0] = CellStatus.PartOfShip;
            return map;
        }
        private static bool CanInsertAShip(ref Map map, Coordinates coordinates)
        {
            for (int xDelta = -1; xDelta <= 1; xDelta++)
            {
                for (int yDelta = -1; yDelta <= 1; yDelta++)
                {
                    int tmpX = coordinates.X + xDelta;
                    int tmpY = coordinates.Y + yDelta;
                    
                    if(tmpX<0||map.Width<=tmpX)
                        continue;
                    if(tmpY<0||map.Height<=tmpY)
                        continue;

                    var currentCell = map.CellsStatuses[tmpX, tmpY];
                    if (currentCell == CellStatus.PartOfShip)
                        return false;
                }
            }

            return true;
        }


        public static void FillOutTheMap(ref Map map)
        {
            int height = map.Height;
            int width = map.Width;
            int area = height * width;

            //Меньше одной клетки для вставки корабля
            if (CoverageArea * area < 1)
                throw new MapSizeIsTooSmallException();
            
            //Создание набора карт (слоёв)
            MapLayer[] mapLayers = CreateLayers(width, height);

            //Пересечение слоёв
            IntersectLayers(mapLayers, map, width, height);
            
            //Проверка соблюдения правил располажения кораблей

            CheckCompliance(ref map);

        }
        public static bool CheckCompliance(ref Map map)
        {
         
            //Пройтись по всем клеткам и для каждой проверить соседей по диагоналям
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    var currentCell = map.CellsStatuses[x, y];
                    //В клетке находится часть корабля
                    if (currentCell == CellStatus.PartOfShip)
                    {
                        bool canCheckUpLeft = true;
                        bool canCheckUpRight = true;
                        bool canCheckDownLeft = true;
                        bool canCheckDownRight = true;

                        if (x == 0)
                        {
                            canCheckUpLeft = false;
                            canCheckDownLeft = false;
                        
                        }else if (x == map.Width - 1)
                        {
                            canCheckDownRight = false;
                            canCheckUpRight = false;
                        }

                        if (y == 0)
                        {
                            canCheckDownLeft = false;
                            canCheckDownRight = false;
                        }else if (y == map.Height - 1)
                        {
                            canCheckUpLeft = false;
                            canCheckUpRight = false;
                        }
                        
                        
                        
                        if (canCheckUpRight && map.CellsStatuses[x + 1, y + 1] == CellStatus.PartOfShip)
                            return false;
                        if (canCheckDownRight && map.CellsStatuses[x + 1, y - 1] == CellStatus.PartOfShip)
                            return false;
                        if(canCheckDownLeft && map.CellsStatuses[x - 1, y - 1] == CellStatus.PartOfShip)
                            return false;
                        if (canCheckUpLeft && map.CellsStatuses[x - 1, y + 1] == CellStatus.PartOfShip)
                            return false;
                    }
                }
            }
            
            /*TODO Проверить количество кораблей конкретного размера*/
            

            return true;
        }
        private static void IntersectLayers(MapLayer[] mapLayers, Map map, int width, int height)
        {
            //Пересечение набора карт (слоёв) с результирующей картой
            for (int i = 0; i < mapLayers.Length; i++)
            {
                bool failedToPerformTheOperationForNAttempts = true;
                //Попытки пересечь слои с картой
                for (int attemptNumber = 0; attemptNumber < NumberOfAttemptsToRecreateTheMap; attemptNumber++)
                {
                    //Если не удалось пересечь, то нужно пересоздать слой
                    if (!map.TryToCross(ref mapLayers[i].Map))
                        mapLayers[i].Map = RandomGenerateMapWithOneShip(width, height, mapLayers[i].ShipLength);
                    else
                    {
                        failedToPerformTheOperationForNAttempts = false; 
                        break;
                    }
                }
                if (failedToPerformTheOperationForNAttempts)
                    throw new TotalZrada();
            }
        }
        private static MapLayer[] CreateLayers(int width, int height)
        {
            int maxShipLength = GetMaxShipLengthByArea(width, height, CoverageArea);
            MapLayer[] mapLayers = new MapLayer[maxShipLength];
                        
            int arrayIndex = 0;
            for (int shipsCount = 1; shipsCount < maxShipLength; shipsCount++)
            {
                int shipLength = maxShipLength + 1 - shipsCount;
                Map oneShipMap = RandomGenerateMapWithOneShip(width, height, shipLength);
                
                mapLayers[arrayIndex] = new MapLayer(oneShipMap, shipLength);
                arrayIndex++;
            }

            return mapLayers;
        }
        public static int GetMaxShipLengthByArea(int width,int height, double coverageArea)
        {
            int totalArea = width * height;
            int countOfFilledCells = 0;
            int maxShipLength = 0;

            bool areaLimitIsNotExceeded = IsAreaLimitIsNotExceeded(countOfFilledCells, totalArea,coverageArea);
            bool maximumLengthOfTheShipIsNotExceeded = IsMaximumLengthOfTheShipIsNotExceeded(maxShipLength, width, height);
            
            while (areaLimitIsNotExceeded && maximumLengthOfTheShipIsNotExceeded)
            {
                maxShipLength++;
                countOfFilledCells = GetFilledAreaByMaxShipLength(maxShipLength);
                
                //Обновление условий
                areaLimitIsNotExceeded = IsAreaLimitIsNotExceeded(countOfFilledCells, totalArea, coverageArea);
                maximumLengthOfTheShipIsNotExceeded = IsMaximumLengthOfTheShipIsNotExceeded(maxShipLength, width, height);
            }

            maxShipLength--;
            return maxShipLength;
        }
        public static bool IsAreaLimitIsNotExceeded(int countOfFilledCells, int totalArea, double coverageArea)
        {
            if (countOfFilledCells < 0)
                throw new ArgumentOutOfRangeException(nameof(countOfFilledCells));
            if (totalArea < 0)
                throw new ArgumentOutOfRangeException(nameof(totalArea));
            if (coverageArea < 0 || coverageArea > 1)
                throw new ArgumentOutOfRangeException(nameof(coverageArea));
            
            return countOfFilledCells <= totalArea * coverageArea;
        }
        public  static bool IsMaximumLengthOfTheShipIsNotExceeded(int maxShipLength, int width, int height)
        {
            return maxShipLength <= width || maxShipLength <= height;
        }
        public static int GetFilledAreaByMaxShipLength(int maxShipLength)
        {
            int totalCellCount = 0;
            for (int length = 1; length <= maxShipLength; length++)
            {
                int countOfShips = maxShipLength - length + 1;
                totalCellCount += countOfShips * length;
            }
            return totalCellCount;
        }
        public static Map RandomGenerateMapWithOneShip(int mapWidth, int mapHeight, int shipLength, Random random = null)
        {
            if (shipLength > mapHeight && shipLength > mapWidth)
                throw new ArgumentException("Длинна корабля не может быть одновременно больше и длинны и ширины");

            var map = new Map(mapWidth, mapHeight);
            
            if(random==null)
                random = new Random();
            
            var startCoordinates = new Coordinates(random.Next(mapWidth), random.Next(mapHeight));
            var endCoordinates = GetAllPossibilities(startCoordinates, shipLength, mapWidth, mapHeight);

            //TODO STUB
            if (endCoordinates.Count == 0)
            {
                startCoordinates = random.Next()%2==0 ? new Coordinates(0, random.Next(mapHeight)) : new Coordinates(random.Next(mapWidth), 0);
                endCoordinates = GetAllPossibilities(startCoordinates, shipLength, mapWidth, mapHeight);
            }
               
            int index = random.Next(endCoordinates.Count);
            
            //Важно: сортировка
            int leftX = Math.Min(endCoordinates[index].X, startCoordinates.X);
            int rightX = Math.Max(endCoordinates[index].X, startCoordinates.X);
            
            int downY = Math.Min(endCoordinates[index].Y, startCoordinates.Y);
            int upY = Math.Max(endCoordinates[index].Y, startCoordinates.Y);
            
            for (int x = leftX; x <= rightX; x++)
            {
                for (int y = downY; y <= upY; y++)
                {
                    map.CellsStatuses[x, y] = CellStatus.PartOfShip;
                }
            }
            
            return map;
        }
        private static List<Coordinates> GetAllPossibilities(Coordinates startCoordinates, int shipLength, int mapWidth, int mapHeight)
        {
            //поправка на то, что одна клетка уже занята
            shipLength--;
            
            List<Coordinates> coordinates = new List<Coordinates>();
            //Вверх
            var coordinateUp = new Coordinates(startCoordinates.X, startCoordinates.Y + shipLength);
            if(coordinateUp.Y<mapHeight)
                coordinates.Add(coordinateUp);
            
            //Вниз
            var coordinateDown = new Coordinates(startCoordinates.X, startCoordinates.Y - shipLength);
            if(0<=coordinateDown.Y)
                coordinates.Add(coordinateDown);
            
            //Вправо
            var coordinateRight = new Coordinates(startCoordinates.X + shipLength, startCoordinates.Y);
            if(coordinateRight.X<mapWidth)
                coordinates.Add(coordinateRight);
            
            //Влево
            var coordinateLeft = new Coordinates(startCoordinates.X - shipLength, startCoordinates.Y);
            if(0<=coordinateLeft.X)
                coordinates.Add(coordinateLeft);
            
            return coordinates;
        }
    }
}