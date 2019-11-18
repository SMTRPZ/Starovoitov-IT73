using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SeaBattle2Lib.Exceptions;
using SeaBattle2Lib.GameLogic;

namespace SeaBattle2Lib.Shooting
{
    public class ShotAlong : ShootingMethod
    {
        protected override Coordinates Shot(ref Map map, Random random=null)
        {
            Console.WriteLine("ShotAlong");
            if (random == null)
                random = new Random();
          
            List<Coordinates> damagedPartsOfShips = new List<Coordinates>();
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    if(map.CellsStatuses[x,y]==CellStatus.DamagedPartOfShip)
                        damagedPartsOfShips.Add(new Coordinates(x,y));
                }
            }

            if(damagedPartsOfShips.Count<=1)
                throw new TooFewDamagedPartsException();

            bool horizontal = true;
            bool vertical = true;

            for (int i = 1; i < damagedPartsOfShips.Count; i++)
            {
                if (damagedPartsOfShips[i - 1].X != damagedPartsOfShips[i].X)
                    vertical = false;

                if (damagedPartsOfShips[i - 1].Y != damagedPartsOfShips[i].Y)
                    horizontal = false;
            }

            if (!(horizontal || vertical))
                throw new WrongDamagedPartsLocationException();
            
            if(horizontal&&vertical)
                throw new WrongDamagedPartsLocationException();

            List<Coordinates> possibleShotCoordinates = new List<Coordinates>();
            
            #region Это дичь

            if (horizontal)
            {
                //найти максимальный и минимальный Х
                int maxX = damagedPartsOfShips.OrderByDescending(c => c.X).First().X;
                int maxXPlusOne = maxX + 1;
                //Проверить на выход за границы карты
                if (maxXPlusOne < map.Width)
                {
                    //Клетка не занята
                    if (map.CellsStatuses[maxXPlusOne, damagedPartsOfShips[0].Y] != CellStatus.DamagedPartOfShip &&
                        map.CellsStatuses[maxXPlusOne, damagedPartsOfShips[0].Y] != CellStatus.HittedWater)
                    {
                        possibleShotCoordinates.Add(new Coordinates(maxXPlusOne,damagedPartsOfShips[0].Y));
                    }
                }
                    
                int minX = damagedPartsOfShips.OrderBy(c => c.X).First().X; 
                int minXMinusOne = minX - 1;
                //Проверить на выход за границы карты
                if (minXMinusOne >= 0)
                {
                    //Клетка не занята
                    if (map.CellsStatuses[minXMinusOne, damagedPartsOfShips[0].Y] != CellStatus.DamagedPartOfShip &&
                        map.CellsStatuses[minXMinusOne, damagedPartsOfShips[0].Y] != CellStatus.HittedWater)
                    {
                        possibleShotCoordinates.Add(new Coordinates(minXMinusOne,damagedPartsOfShips[0].Y));
                    }
                }
            }
                
            if (vertical)
            {
                //найти максимальный и минимальный Х
                int maxY = damagedPartsOfShips.OrderByDescending(c => c.Y).First().Y;
                int maxYPlusOne = maxY + 1;
                //Проверить на выход за границы карты
                if (maxYPlusOne < map.Height)
                {
                    //Клетка не занята
                    if (map.CellsStatuses[damagedPartsOfShips[0].X,maxYPlusOne] != CellStatus.DamagedPartOfShip &&
                        map.CellsStatuses[damagedPartsOfShips[0].X,maxYPlusOne] != CellStatus.HittedWater)
                    {
                        possibleShotCoordinates.Add(new Coordinates(damagedPartsOfShips[0].X, maxYPlusOne));
                    }
                }
                    
                int minY = damagedPartsOfShips.OrderBy(c => c.Y).First().Y; 
                int minYMinusOne = minY - 1;
                //Проверить на выход за границы карты
                if (minYMinusOne >= 0)
                {
                    //Клетка не занята
                    if (map.CellsStatuses[damagedPartsOfShips[0].X, minYMinusOne] != CellStatus.DamagedPartOfShip &&
                        map.CellsStatuses[damagedPartsOfShips[0].X, minYMinusOne] != CellStatus.HittedWater)
                    {
                        possibleShotCoordinates.Add(new Coordinates(damagedPartsOfShips[0].X, minYMinusOne));
                    }
                }
            }
            

            #endregion

            if (possibleShotCoordinates.Count == 0)
                throw new ArgumentException("Этот метод стрельбы не подходит для этой карты");
            
            int index = random.Next(possibleShotCoordinates.Count);
            return possibleShotCoordinates[index];
        }

        public override bool ConditionsAreMet(ref Map map)
        {
            return GetCountOfDamagedParts(ref map) >= 2 && MapHasOnlyOneDamagedShip(ref map);
        }
    }
}
