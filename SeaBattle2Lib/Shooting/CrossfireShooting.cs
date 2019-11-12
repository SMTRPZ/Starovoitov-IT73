using System;
using System.Collections.Generic;
using SeaBattle2Lib.GameLogic;

namespace SeaBattle2Lib.Shooting
{
    public class CrossfireShooting:ShootingMethod
    {
        protected override Coordinates Shot(ref Map map, Random random=null)
        {
            if (random == null) random = new Random();
            
            Coordinates damagedPartOfShip = GetSingleDamagedPartOfShipCoordinates(ref map);
            List<Coordinates> possibleHitCoordinates = new List<Coordinates>();
            
            for (int deltaX = -1; deltaX <= 1; deltaX+=2)
            {
                int tmpX = damagedPartOfShip.X + deltaX;
                if (0 <= tmpX && tmpX < map.Width )
                    possibleHitCoordinates.Add(new Coordinates(tmpX, damagedPartOfShip.Y));                
            }
            
            for (int deltaY = -1; deltaY <= 1; deltaY+=2)
            {
                int tmpY = damagedPartOfShip.Y + deltaY;
                if (0 <= tmpY && tmpY < map.Height )
                    possibleHitCoordinates.Add(new Coordinates(damagedPartOfShip.X, tmpY));  
            }  

            int index = random.Next(possibleHitCoordinates.Count);
            var answer = possibleHitCoordinates[index];

            return answer;
        }

        private Coordinates GetSingleDamagedPartOfShipCoordinates(ref Map map)
        {
            
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    if(map.CellsStatuses[x,y]==CellStatus.DamagedPartOfShip)
                        return new Coordinates(x,y);
                }
            }
            
            throw new Exception("Не найдено ни одного подбитого корабля");
        }
        public override bool ConditionsAreMet(ref Map map)
        {
            return GetCountOfDamagedParts(ref map) == 1;
        }
    }
}