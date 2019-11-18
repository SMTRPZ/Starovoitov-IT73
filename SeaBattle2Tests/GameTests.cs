using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaBattle2Lib;
using SeaBattle2Lib.GameLogic;
using SeaBattle2Lib.Shooting;

namespace SeaBattle2Tests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void RandomShooting_1()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[5, 5] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[5, 6] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[5, 7] = CellStatus.PartOfShip;
            Random random = new Random(314159);
            
            //Act
            Coordinates shotCoordinates = NotAi.MakeShot(ref map, random);
            Console.WriteLine(shotCoordinates);

            //Assert
            Assert.AreEqual(new Coordinates(5,7), shotCoordinates);
        }
        
        [TestMethod]
        public void RandomShooting_2()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[5, 5] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[5, 6] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[5, 7] = CellStatus.PartOfShip;
            Random random = new Random(41459);
            
            //Act
            Coordinates shotCoordinates = NotAi.MakeShot(ref map, random);
            Console.WriteLine(shotCoordinates);

            //Assert
            Assert.AreEqual(new Coordinates(5,4), shotCoordinates);
        }
        
        [TestMethod]
        public void RandomShooting_3()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[5, 5] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[5, 6] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[5, 7] = CellStatus.PartOfShip;
            Random random = new Random();
            
            List<Coordinates> shotCoordinates = new List<Coordinates>();
            
            
            //Act
            for (int i = 0; i < 1000; i++)
            {
                Coordinates shotCoord = NotAi.MakeShot(ref map, random);
                shotCoordinates.Add(shotCoord);        
            }
            

            //Assert
            Coordinates coord1 = new Coordinates(5,4);
            Coordinates coord2 = new Coordinates(5,7);

            foreach (var coordinate in shotCoordinates)
            {
                Assert.IsTrue(coordinate == coord1 || coordinate == coord2);
            }
        
            
        }
    }
}