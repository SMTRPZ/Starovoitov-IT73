using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle2Lib
{
    public static class Mapholder
    {
        public const double CoverageArea = 0.2; 
        
        public static void FillOutTheMap(ref Map map)
        {
            int height = map.Height;
            int width = map.Width;
            int area = height * width;

            //Меньше одной клетки для вставки корабля
            if (CoverageArea * area < 1)
                throw new MapSizeIsTooSmallException();

            int maxShipLength = GetMaxShipLengthByArea(width, height, CoverageArea);





            //Можно сразу определить набор кораблей,
            //которые нужно разместить
            //Варианты:
            //1) решение в лоб
            //    создаём количество карт равное кол-ву кораблей
            //    случайно размещаем корабли
            //    пытаемся пересечь карты, контролируя, чтобы корабли не стояли близко друг к другу
            //    если не получилось значит пытаемся N раз перегенерировать тот слой
            //    если не получилось после N раз, то вызываем полную перегенерацию
            //    
            //    при этом контролируем  кол-во полных перегенераций
            //    если оно больше Z, то бросаем ошибку

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

      

      
        private static Map GenerateMapWithOneShip(int mapWidth, int mapHeight, int shipLength)
        {
            throw new NotImplementedException();
        }
    }
}