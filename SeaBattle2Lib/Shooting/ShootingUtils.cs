using System.Collections.Generic;
using SeaBattle2Lib.GameLogic;

namespace SeaBattle2Lib.Shooting
{
    public static class ShootingUtils
    {
        public static bool HasAFreeCell(ref Map map)
        {
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    if (map.CellsStatuses[x, y] == CellStatus.Water||
                        map.CellsStatuses[x, y] == CellStatus.PartOfShip) 
                        return true;
                }
            }

            return false;
        }
        public static uint GetCountOfDamagedParts(ref Map map)
        {
            uint numberOfDamagedParts = 0;
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    if (map.CellsStatuses[x, y] == CellStatus.DamagedPartOfShip) numberOfDamagedParts++;
                }
            }

            return numberOfDamagedParts;
        }
        public static bool MapHasOnlyOneDamagedShip(ref Map map)
        {
            List<Coordinates> damagedParts = new List<Coordinates>();
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    if (map.CellsStatuses[x, y] == CellStatus.DamagedPartOfShip)
                        damagedParts.Add(new Coordinates(x, y));
                }
            }

            bool allShipsOnTheSameHorizontal = true;
            bool allShipsOnTheSameVertical = true;

            for (int i = 1; i < damagedParts.Count; i++)
            {
                if (damagedParts[i].X != damagedParts[i - 1].X)
                    allShipsOnTheSameVertical = false;
                if (damagedParts[i].Y != damagedParts[i - 1].Y)
                    allShipsOnTheSameHorizontal = false;
            }

            return allShipsOnTheSameHorizontal || allShipsOnTheSameVertical;
        }
    }
}