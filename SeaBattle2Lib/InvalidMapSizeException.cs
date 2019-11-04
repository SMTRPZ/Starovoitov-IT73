using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle2Lib
{
    public class InvalidMapSizeException : Exception
    {
        public InvalidMapSizeException() : base("Недопустимый размер карты")
        {
        }
    }
}
