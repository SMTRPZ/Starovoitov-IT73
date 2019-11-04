namespace SeaBattle2Lib
{
    public struct MapLayer
    {
        public Map Map;
        public readonly int ShipLength;

        public MapLayer(Map map, int shipLength)
        {
            Map = map;
            ShipLength = shipLength;
        }
    }
}