using System;

namespace SeaBattle2Lib.Exceptions
{
    public class TooFewDamagedPartsException : Exception
    {
        public TooFewDamagedPartsException() : base("Видно слишком мало подбитых частей для выстрела вдоль(нужно хотя бы два)")
        {
        }
    }
}
