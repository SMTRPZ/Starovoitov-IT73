using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle2Lib
{
    public abstract class ShootingMethod
    {
        public virtual Coordinates Shot(Map map)
        {
            return new Coordinates(0,0);
        }

    }
}