using System;

namespace SeaBattle2Lib.Exceptions
{
    public class TotalZrada : Exception
    {
        public TotalZrada() : base("Це тотальна зрада")
        {
        }
    }
}