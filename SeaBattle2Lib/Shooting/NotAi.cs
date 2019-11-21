using System;
using System.Collections.Generic;
using SeaBattle2Lib.Exceptions;
using SeaBattle2Lib.GameLogic;

namespace SeaBattle2Lib.Shooting
{
    public static class NotAi
    {
        private static readonly List<ShootingMethod> Methods=new List<ShootingMethod>
        {
            new RandomShooting(),
            new CrossfireShooting(),
            new ShotAlong(),
            new WtfShooting()
        };    
        
        public static Coordinates MakeShot(ref Map targetMap, Random random=null)
        {
            if(!targetMap.IsValid())
                throw new InvalidMapException();
            
            foreach (var method in Methods)
            {
                if (method.TryToShot(ref targetMap, out Coordinates coordinates, random))
                    return coordinates;
            }
            throw new FailedToMakeAShotException();
        }
    }
}