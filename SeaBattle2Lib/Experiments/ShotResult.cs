using SeaBattle2Lib.GameLogic;

namespace SeaBattle2Lib.Experiments
{
    public class ShotResult
    {
        public readonly bool Victory;
        public readonly Coordinates Coordinates;

        public ShotResult(bool victory, Coordinates coordinates)
        {
            Victory = victory;
            Coordinates = coordinates;
        }
    }
}