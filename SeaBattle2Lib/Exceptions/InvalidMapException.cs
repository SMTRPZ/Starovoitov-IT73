using System;

namespace SeaBattle2Lib.Exceptions
{
    public class InvalidMapException : Exception
    {
        public InvalidMapException() : base("Недопустимое расположение кораблей")
        {
        }
    }
}
