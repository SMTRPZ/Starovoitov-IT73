using SeaBattle2Lib.GameLogic;

namespace SeaBattle2Lib.Mapholder
{
    public static class Mapholder
    {
        public static Map GenerateFilledMap(int width, int height)
        {
            var map = new Map(width, height);
            map.CellsStatuses[0, 0] = CellStatus.PartOfShip;
            map.CellsStatuses[5, 0] = CellStatus.PartOfShip;
            map.CellsStatuses[7, 0] = CellStatus.PartOfShip;
            map.CellsStatuses[7, 1] = CellStatus.PartOfShip;
            return map;
        }
    }
}