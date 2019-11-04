using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaBattle2Lib;

namespace SeaBattle2Tests
{
    [TestClass]
    public class StupidMapholderTests
    {
        [TestMethod]
        public void StupidMapholder_ContainsShips()
        {
            //Arrange
            var map = new Map(10, 10);
            
            //Act
            Mapholder.StupidFillOutTheMap(ref map);
            bool containsShips=false;
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    if (map.CellsStatuses[x, y] == CellStatus.PartOfShip)
                    {
                        containsShips = true;
                        break;;
                    }
                }
            }
            //Assert
            Assert.IsTrue(containsShips);
        }
        
        //TODO Да ну его нахер
        [TestMethod]
        public void StupidMapholder_ExpectedNumberOfShips()
        {
            //Arrange
            var map = new Map(10, 10);
            
            //Act
            Mapholder.StupidFillOutTheMap(ref map);
            bool containsShips=false;
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    if (map.CellsStatuses[x, y] == CellStatus.PartOfShip)
                    {
                        containsShips = true;
                        break;;
                    }
                }
            }
            //Assert
            Assert.IsTrue(containsShips);
        }
    }
}