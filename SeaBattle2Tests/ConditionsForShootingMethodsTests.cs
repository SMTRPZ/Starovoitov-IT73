using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaBattle2Lib;
using SeaBattle2Lib.GameLogic;
using SeaBattle2Lib.Shooting;

namespace SeaBattle2Tests
{
    [TestClass]
    public class ConditionsForShootingMethodsTests
    {
        
        [TestMethod]
        public void ConditionsForARandomShot_0DamagedCells()
        {
            //Arrange
            Map map = new Map(10,10);
            //Act
            bool conditionsAreMet = new RandomShooting().ConditionsAreMet(ref map);
            //Assert
            Assert.IsTrue(conditionsAreMet);
        }
        
        [TestMethod]
        public void ConditionsForARandomShot_1DamagedCells()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[0, 0] = CellStatus.DamagedPartOfShip;
            //Act
            bool conditionsAreMet = new RandomShooting().ConditionsAreMet(ref map);
            //Assert
            Assert.IsFalse(conditionsAreMet);
        }
        

      
        [TestMethod]
        public void ConditionsForARCrossfireShot_0DamagedCells()
        {
            //Arrange
            Map map = new Map(10,10);
            //Act
            bool conditionsAreMet = new CrossfireShooting().ConditionsAreMet(ref map);
            //Assert
            Assert.IsFalse(conditionsAreMet);
        }
        
        [TestMethod]
        public void ConditionsForARCrossfireShot_1DamagedCells()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[5, 5] = CellStatus.DamagedPartOfShip;
            //Act
            bool conditionsAreMet = new CrossfireShooting().ConditionsAreMet(ref map);
            //Assert
            Assert.IsTrue(conditionsAreMet);
        }
        
        [TestMethod]
        public void ConditionsForARCrossfireShot_2DamagedCells()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[5, 5] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[5, 6] = CellStatus.DamagedPartOfShip;
            //Act
            bool conditionsAreMet = new CrossfireShooting().ConditionsAreMet(ref map);
            //Assert
            Assert.IsFalse(conditionsAreMet);
        }
        
        [TestMethod]
        public void ConditionsForAlongShot_0DamagedCells()
        {
            //Arrange
            Map map = new Map(10,10);
            //Act
            bool conditionsAreMet = new ShotAlong().ConditionsAreMet(ref map);
            //Assert
            Assert.IsFalse(conditionsAreMet);
        }
        
        [TestMethod]
        public void ConditionsForAlongShot_1DamagedCells()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[3, 4] = CellStatus.DamagedPartOfShip;
            //Act
            bool conditionsAreMet = new ShotAlong().ConditionsAreMet(ref map);
            //Assert
            Assert.IsFalse(conditionsAreMet);
        }

        [TestMethod]
        public void ConditionsForAlongShot_2DamagedCells()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[3, 4] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[3, 5] = CellStatus.DamagedPartOfShip;
            //Act
            bool conditionsAreMet = new ShotAlong().ConditionsAreMet(ref map);
            //Assert
            Assert.IsTrue(conditionsAreMet);
        }
        
        [TestMethod]
        public void ConditionsForAlongShot_3DamagedCellsOnTheSameLine()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[3, 4] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[3, 5] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[3, 6] = CellStatus.DamagedPartOfShip;
            //Act
            bool conditionsAreMet = new ShotAlong().ConditionsAreMet(ref map);
            //Assert
            Assert.IsTrue(conditionsAreMet);
        }
        
        [TestMethod]
        public void ConditionsForAlongShot_3DamagedCellsNotOnTheSameLine()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[3, 4] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[3, 5] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[4, 6] = CellStatus.DamagedPartOfShip;
            //Act
            bool conditionsAreMet = new ShotAlong().ConditionsAreMet(ref map);
            //Assert
            Assert.IsFalse(conditionsAreMet);
        }

        
        
        
     
        
    }
}