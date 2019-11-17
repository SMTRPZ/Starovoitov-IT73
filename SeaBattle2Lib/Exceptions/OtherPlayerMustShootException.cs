using System;

namespace SeaBattle2Lib.Exceptions
{
    public class OtherPlayerMustShootException:Exception
    {
        public OtherPlayerMustShootException(string message):base(message){}
    }
}