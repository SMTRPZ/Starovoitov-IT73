using System;
using System.Collections.Generic;
using SeaBattle2Lib.GameLogic;

namespace SeaBattle2Lib.Shooting
{
    public abstract class ShootingMethod
    {
        public virtual bool ConditionsAreMet(ref Map map)
        {
            throw new NotImplementedException();
        }
        protected virtual Coordinates Shot(ref Map map, Random random=null)
        {
            throw new NotImplementedException();
        }

        public bool TryToShot(ref Map map, out Coordinates coordinates, Random random=null)
        {
            coordinates = new Coordinates();
            if (!ConditionsAreMet(ref map)) return false;
            try
            {
                coordinates = Shot(ref map, random);
            }
            catch
            {
                return false;
            }
            return true;
        }

      
    }
}