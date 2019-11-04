using System;

namespace SeaBattle2Lib
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

        public bool TryToCross(ref Map map)
        {
            if (map.Height != Height || map.Width != Width)
                throw new TryingToCrossMapsOfDifferentSizesException();

            for (int loopWidth = 0; loopWidth < Width; loopWidth++)
            {
                for (int loopHeight = 0; loopHeight < Height; loopHeight++)
                {
                    //Если корабль накладывается на другой корабль, то пересечь не удалось
                    if (map.CellsStatuses[loopWidth, loopHeight] == CellStatus.PartOfShip 
                        && CellsStatuses[loopWidth, loopHeight] == CellStatus.PartOfShip)
                        return false;
                    
                    CellsStatuses[loopWidth, loopHeight] = map.CellsStatuses[loopWidth,loopHeight];
                }
            }


            return true;
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