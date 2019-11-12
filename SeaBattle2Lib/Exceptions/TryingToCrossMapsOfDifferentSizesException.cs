using System;

namespace SeaBattle2Lib.Exceptions
{
    public class TryingToCrossMapsOfDifferentSizesException:Exception
    {
        public TryingToCrossMapsOfDifferentSizesException() : base("Попытка пересечь карты разного размера.")
        {
        }
    }
}