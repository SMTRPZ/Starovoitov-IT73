using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaBattle2Lib;
using SeaBattle2Lib.GameLogic;

namespace SeaBattle2Tests
{
    [TestClass]
    public class MapValidationTests
    {

        [TestMethod]
        public void MapValidation_EmptyMap_1()
        {
            //Arrange
            Map map = new Map();
            //Act
            bool mapIsValid = map.IsValid();
            //Assert
            Assert.IsFalse(mapIsValid);
        }
        
        [TestMethod]
        public void MapValidation_EmptyMap_2()
        {
            //Arrange
            Map map = new Map(10,10);
            //Act
            bool mapIsValid = map.IsValid();
            //Assert
            Assert.IsTrue(mapIsValid);
        }
        
        [TestMethod]
        public void MapValidation_OneShip()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[3, 4] = CellStatus.PartOfShip;
            //Act
            bool mapIsValid = map.IsValid();
            //Assert
            Assert.IsTrue(mapIsValid);
        }
        
        [TestMethod]
        public void MapValidation_TwoParts_OneLine()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[3, 4] = CellStatus.PartOfShip;
            map.CellsStatuses[3, 5] = CellStatus.PartOfShip;
            //Act
            bool mapIsValid = map.IsValid();
            //Assert
            Assert.IsTrue(mapIsValid);
        }
        
        [TestMethod]
        public void MapValidation_TwoParts_Diagonal_1()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[3, 4] = CellStatus.PartOfShip;
            map.CellsStatuses[4, 5] = CellStatus.PartOfShip;
            //Act
            bool mapIsValid = map.IsValid();
            //Assert
            Assert.IsFalse(mapIsValid);
        }
        [TestMethod]
        public void MapValidation_TwoParts_Diagonal_2()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[0, 0] = CellStatus.PartOfShip;
            map.CellsStatuses[1, 1] = CellStatus.PartOfShip;
            //Act
            bool mapIsValid = map.IsValid();
            //Assert
            Assert.IsFalse(mapIsValid);
        }
        
        [TestMethod]
        public void MapValidation_TwoParts_Diagonal_3()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[0, 9] = CellStatus.PartOfShip;
            map.CellsStatuses[1, 8] = CellStatus.PartOfShip;
            //Act
            bool mapIsValid = map.IsValid();
            //Assert
            Assert.IsFalse(mapIsValid);
        }
        
        [TestMethod]
        public void MapValidation_TwoParts_Diagonal_4()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[5, 6] = CellStatus.PartOfShip;
            map.CellsStatuses[6, 5] = CellStatus.PartOfShip;
            //Act
            bool mapIsValid = map.IsValid();
            //Assert
            Assert.IsFalse(mapIsValid);
        }
        
        [TestMethod]
        public void MapValidation_ThreeParts_Diagonal_1()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[3, 4] = CellStatus.PartOfShip;
            map.CellsStatuses[4, 5] = CellStatus.PartOfShip;
            map.CellsStatuses[5, 6] = CellStatus.PartOfShip;
            //Act
            bool mapIsValid = map.IsValid();
            //Assert
            Assert.IsFalse(mapIsValid);
        }
        
        [TestMethod]
        public void MapValidation_ThreeParts_Diagonal_2()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[3, 4] = CellStatus.PartOfShip;
            map.CellsStatuses[4, 5] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[5, 6] = CellStatus.PartOfShip;
            //Act
            bool mapIsValid = map.IsValid();
            //Assert
            Assert.IsFalse(mapIsValid);
        }
        
        [TestMethod]
        public void MapValidation_ThreeParts_Diagonal_3()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[4, 6] = CellStatus.PartOfShip;
            map.CellsStatuses[5, 5] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[6, 4] = CellStatus.PartOfShip;
            //Act
            bool mapIsValid = map.IsValid();
            //Assert
            Assert.IsFalse(mapIsValid);
        }
        
        [TestMethod]
        public void MapValidation_ThreeParts_Diagonal_4()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[4, 6] = CellStatus.PartOfShip;
            map.CellsStatuses[5, 5] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[2, 2] = CellStatus.PartOfShip;
            //Act
            bool mapIsValid = map.IsValid();
            //Assert
            Assert.IsFalse(mapIsValid);
        }

        
    }
}