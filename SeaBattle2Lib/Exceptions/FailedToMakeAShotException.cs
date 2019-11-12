using System;

namespace SeaBattle2Lib.Exceptions
{
    public class FailedToMakeAShotException : Exception
    {
        public FailedToMakeAShotException() : base("Не удалось сделать выстрел")
        {
        }
    }
}
