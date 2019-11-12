using System;

namespace SeaBattle2Lib.Exceptions
{
    public class InvalidMapSizeException : Exception
    {
        public InvalidMapSizeException() : base("Недопустимый размер карты")
        {
        }
    }
}
