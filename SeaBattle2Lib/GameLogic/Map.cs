using System;
using System.Threading;
using SeaBattle2Lib.Exceptions;

namespace SeaBattle2Lib.GameLogic
{
    public struct Map
    {
        public readonly int Height;
        public readonly int Width;
        public readonly CellStatus[,] CellsStatuses;
        
        public Map(int width, int height)
        {
            if (width < 1 || height < 1)
                throw new InvalidMapSizeException();
            Width = width;
            Height = height;
            CellsStatuses = new CellStatus[width, height];
        }

        public bool CoordinatesAllowed(Coordinates coordinates)
        {
            int x = coordinates.X;
            int y = coordinates.Y;

            if (x < 0 || Width <= x)
                return false;
            
            if (y < 0 || Height <= y)
                return false;

            return true;
        }
        
        public bool IsValid()
        {
            if (Width == 0 || Height == 0)
                return false;

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (!CheckForShipsDiagonally(x, y, ref this))
                        return false;
                }
            }
            
            return true;

            bool CheckForShipsDiagonally(int x, int y, ref Map map)
            {
                bool currentCellIsShip = CellIsShip(map.CellsStatuses[x, y]) ;
                if (currentCellIsShip)
                {
                    for (int deltaX = -1; deltaX <= 1; deltaX+=2)
                    {
                        for (int deltaY = -1; deltaY <= 1; deltaY+=2)
                        {
                            int tmpX = x + deltaX;
                            int tmpY = y + deltaY;
                            //Проверка на выход за границы карты
                            if (0 <= tmpX && tmpX < map.Width && 0 <= tmpY && tmpY < map.Height)
                            {
                                bool cellIsShip = CellIsShip(map.CellsStatuses[tmpX, tmpY]);
                                if (cellIsShip)
                                    return false;
                            }
                        }
                    }
                }
                return true;
            }
        }

        private static bool CellIsShip(CellStatus cellStatus)
        {
            return cellStatus == CellStatus.PartOfShip ||
                   cellStatus == CellStatus.DamagedPartOfShip ||
                   cellStatus == CellStatus.DestroyedShip;
        }
        
    
        public override bool Equals(object obj)
        {
            if (!(obj is Map))
                return false;

            Map map = (Map)obj;

            if (map.Height != Height || map.Width != Width )
                return false;

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if(map.CellsStatuses[x, y] != CellsStatuses[x, y])
                        return false;                    
                }
            }

            return true;
        }    

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + Height.GetHashCode();
                hash = (hash * 7) + Width.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Map left, Map right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Map left, Map right)
        {
            return !(left == right);
        }
    }
}