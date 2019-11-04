using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle2Lib
{
    /*
     Этот класс должен уведомлять о окончании игры
         */
    public abstract class AbstractSession
    {
        private Game game;

        public void RecreateGame(int width, int height)
        {
            game = new Game();
        }
    }
}