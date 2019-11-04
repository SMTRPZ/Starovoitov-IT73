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