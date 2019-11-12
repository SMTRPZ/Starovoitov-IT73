using System;
using System.Collections.Generic;
using System.Threading;
using SeaBattle2Lib.Exceptions;
using SeaBattle2Lib.GameLogic;

namespace SeaBattle2Lib.Shooting
{
    public static class Ai
    {
        private static readonly List<ShootingMethod> Methods=new List<ShootingMethod>
        {
            new RandomShooting(),
            new CrossfireShooting(),
            new ShotAlong(),
            new WtfShooting()
        };
        
        
        public static Coordinates MakeShot(ref Map map, Random random)
        {
            if(!map.IsValid())
                throw new InvalidMapException();
            
            foreach (var method in Methods)
            {
                if (method.TryToShot(ref map, out Coordinates coordinates, random))
                    return coordinates;
            }
            throw new FailedToMakeAShotException();
        }
    }
}