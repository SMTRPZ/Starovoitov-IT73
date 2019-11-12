namespace SeaBattle2Lib.GameLogic
{
    public struct Coordinates
    {
        public readonly int X;
        public readonly int Y;

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Coordinates))
                return false;

            Coordinates coordinates = (Coordinates)obj;

            if (coordinates.X == X && coordinates.Y == Y)            
                return true;            
            else            
                return false;            
        }

        public override int GetHashCode()
        {
            unchecked 
            { 
                int hash = 13;
                hash = (hash * 7) + X.GetHashCode();
                hash = (hash * 7) + Y.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Coordinates left, Coordinates right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Coordinates left, Coordinates right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"x = {X}, y = {Y}";
        }
    }
}