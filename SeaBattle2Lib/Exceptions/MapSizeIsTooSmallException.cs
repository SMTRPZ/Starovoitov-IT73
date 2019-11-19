using System;

namespace SeaBattle2Lib.Exceptions
{
    public class MapSizeIsTooSmallException :Exception
    {
        public MapSizeIsTooSmallException() : base("Не удаётся заполнить карту. Её размер слишком мал.")
        {
        }
    }
}