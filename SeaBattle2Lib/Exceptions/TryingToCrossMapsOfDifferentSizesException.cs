using System;

namespace SeaBattle2Lib
{
    public class TryingToCrossMapsOfDifferentSizesException:Exception
    {
        public TryingToCrossMapsOfDifferentSizesException() : base("Попытка пересечь карты разного размера.")
        {
        }
    }
}