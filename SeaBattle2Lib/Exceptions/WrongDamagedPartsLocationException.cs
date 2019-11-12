using System;

namespace SeaBattle2Lib.Exceptions
{
    public class WrongDamagedPartsLocationException : Exception
    {
        public WrongDamagedPartsLocationException() : base("неверное расположение подбитых частей для этого метода стрельбы")
        {
        }
    }
}
