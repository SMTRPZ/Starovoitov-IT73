using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaBattle2Lib;
using SeaBattle2Lib.GameLogic;
using SeaBattle2Lib.Shooting;

namespace SeaBattle2Tests
{
    [TestClass]
    public class AiCrossfireShooting
    {
        [TestMethod]
        public void CrossfireShooting_Right()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[2, 4] = CellStatus.DamagedPartOfShip;
            Random random = new Random(314159);
            
            //Act
            Coordinates shotCoordinates = Ai.MakeShot(ref map, random);
            Console.WriteLine(shotCoordinates);
            
            //Assert
            Assert.AreEqual(new Coordinates(3,4), shotCoordinates);
        }
        
        [TestMethod]
        public void CrossfireShooting_Left()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[2, 4] = CellStatus.DamagedPartOfShip;
            Random random = new Random(316159);
            
            //Act
            Coordinates shotCoordinates = Ai.MakeShot(ref map, random);
            Console.WriteLine(shotCoordinates);
            
            //Assert
            Assert.AreEqual(new Coordinates(1,4), shotCoordinates);
        }
        
      
        [TestMethod]
        public void CrossfireShooting_Down()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[2, 4] = CellStatus.DamagedPartOfShip;
            Random random = new Random(31639);
            
            //Act
            Coordinates shotCoordinates = Ai.MakeShot(ref map, random);
            Console.WriteLine(shotCoordinates);
            
            //Assert
            Assert.AreEqual(new Coordinates(2,3), shotCoordinates);
        }
        
        [TestMethod]
        public void CrossfireShooting_Up()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[2, 4] = CellStatus.DamagedPartOfShip;
            Random random = new Random(3163999);
            
            //Act
            Coordinates shotCoordinates = Ai.MakeShot(ref map, random);
            Console.WriteLine(shotCoordinates);
            
            //Assert
            Assert.AreEqual(new Coordinates(2,5), shotCoordinates);
        }
        
            
        [TestMethod]
        public void CrossfireShooting_LowerLeftCorner_1()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[0, 0] = CellStatus.DamagedPartOfShip;
            Random random = new Random(3163999);
            
            //Act
            Coordinates shotCoordinates = Ai.MakeShot(ref map, random);
            Console.WriteLine(shotCoordinates);
            
            //Assert
            Assert.AreEqual(new Coordinates(0,1), shotCoordinates);
        }
        
        [TestMethod]
        public void CrossfireShooting_LowerLeftCorner_2()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[0, 0] = CellStatus.DamagedPartOfShip;
            Random random = new Random(3169);
            
            //Act
            Coordinates shotCoordinates = Ai.MakeShot(ref map, random);
            Console.WriteLine(shotCoordinates);
            
            //Assert
            Assert.AreEqual(new Coordinates(1,0), shotCoordinates);
        }
        
        [TestMethod]
        public void CrossfireShooting_BadCheckOnTheCorrectnessOfTheMap()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[5, 5] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[5, 6] = CellStatus.DestroyedShip;
            Random random = new Random(3169);
            //Act
            bool canShot = new CrossfireShooting().TryToShot(ref map, out Coordinates coordinates, random);
            //Assert
            Assert.IsFalse(canShot);
        }
    }
}