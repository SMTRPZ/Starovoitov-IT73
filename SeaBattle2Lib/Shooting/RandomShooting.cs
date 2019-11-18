using System;
using System.Collections.Generic;
using SeaBattle2Lib.GameLogic;

namespace SeaBattle2Lib.Shooting
{
    public class RandomShooting : ShootingMethod
    {
        public override bool ConditionsAreMet(ref Map map)
        {
            return GetCountOfDamagedParts(ref map) == 0 && HasAFreeCell(ref map);
        }
        protected override Coordinates Shot(ref Map map, Random random)
        {
            if (random == null) random = new Random();
            Console.WriteLine("RandomShooting");
            Coordinates[] unknownCells = GetUnknownCells(ref map);
            int index = random.Next(unknownCells.Length);
            var coordinates = unknownCells[index];
            return coordinates;
        }

        private Coordinates[] GetUnknownCells(ref Map map)
        {
            List<Coordinates> list = new List<Coordinates>();
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    if (map.CellsStatuses[x, y] == CellStatus.PartOfShip || map.CellsStatuses[x, y] == CellStatus.Water)
                    {
                        list.Add(new Coordinates(x,y));
                    }
                }
            }
            return list.ToArray();
        }
    }
}
