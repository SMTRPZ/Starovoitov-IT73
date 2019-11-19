using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaBattle2Lib;
using SeaBattle2Lib.GameLogic;
using SeaBattle2Lib.Shooting;

namespace SeaBattle2Tests
{
    [TestClass]
    public class NotAiAlongShooting
    {
        [TestMethod]
        public void ShotAlong_1()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[0, 0] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[0, 1] = CellStatus.DamagedPartOfShip;
            Random random = new Random();
            //Act
            Coordinates shotCoordinates = NotAi.MakeShot(ref map, random);
            //Assert
            Assert.AreEqual(shotCoordinates, new Coordinates(0,2));
        }
        
        [TestMethod]
        public void ShotAlong_2()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[0, 0] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[0, 1] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[0, 2] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[0, 9] = CellStatus.DamagedPartOfShip;
            Random random = new Random(314431);
            //Act
            bool canShot = new ShotAlong().TryToShot(ref map, out Coordinates coordinates, random);
            //Assert
            Assert.IsFalse(canShot);
        }
        
        [TestMethod]
        public void ShotAlong_3()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[5, 2] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[5, 3] = CellStatus.DamagedPartOfShip;
            Random random = new Random(314431);
            //Act
            bool canShot = new ShotAlong().TryToShot(ref map, out Coordinates coordinates, random);
            //Assert
            Assert.IsTrue(canShot);
            Assert.AreEqual(new Coordinates(5,4), coordinates);
        }
        [TestMethod]
        public void ShotAlong_4()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[5, 2] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[5, 3] = CellStatus.DamagedPartOfShip;
            Random random = new Random(31541);
            //Act
            bool canShot = new ShotAlong().TryToShot(ref map, out Coordinates coordinates, random);
            //Assert
            Assert.IsTrue(canShot);
            Assert.AreEqual(new Coordinates(5,1), coordinates);
        }
        [TestMethod]
        public void ShotAlong_5()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[5, 2] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[5, 4] = CellStatus.DamagedPartOfShip;
            Random random = new Random(31541);
            //Act
            bool canShot = new ShotAlong().TryToShot(ref map, out Coordinates coordinates, random);
            //Assert
            Assert.IsTrue(canShot);
            Assert.AreEqual(new Coordinates(5,1), coordinates);
        }
        
        [TestMethod]
        public void ShotAlong_6()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[5, 2] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[5, 4] = CellStatus.DamagedPartOfShip;
            Random random = new Random(313541);
            //Act
            bool canShot = new ShotAlong().TryToShot(ref map, out Coordinates coordinates, random);
            //Assert
            Assert.IsTrue(canShot);
            Assert.AreEqual(new Coordinates(5,5), coordinates);
        }
        
        [TestMethod]
        public void ShotAlong_7()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[5, 4] = CellStatus.DestroyedShip;
            map.CellsStatuses[5, 3] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[5, 2] = CellStatus.DamagedPartOfShip;
            Random random = new Random(313541);
            //Act
            bool canShot = new ShotAlong().TryToShot(ref map, out Coordinates coordinates, random);
            //Assert
            Assert.IsFalse(canShot);
        }
        
        [TestMethod]
        public void ShotAlong_ToFewDamagedParts()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[0, 1] = CellStatus.DamagedPartOfShip;
            Random random = new Random(314159);
            //Act
            bool canShot = new ShotAlong().TryToShot(ref map, out Coordinates coordinates, random);
            //Assert
            Assert.IsFalse(canShot);
        }
        
        [TestMethod]
        public void ShotAlong_ToFewDamagedParts_Reflection_1()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[0, 1] = CellStatus.DamagedPartOfShip;
            Random random = new Random(314159);
            //Act
            var method = typeof(ShotAlong).GetMethod("Shot", BindingFlags.NonPublic | BindingFlags.Instance);
            //Assert
            Assert.ThrowsException<TargetInvocationException>(() =>
            {
                try
                {
                    if (method != null)
                    {
                        Coordinates coordinates =
                            (Coordinates) method.Invoke(new ShotAlong(), new object[] {map, null});
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException?.Message);
                    throw e;
                }
            });
        }
        
        [TestMethod]
        public void ShotAlong_ToFewDamagedParts_Reflection_2()
        {
            //Arrange
            Map map = new Map(10,10);
            map.CellsStatuses[0, 0] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[0, 1] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[0, 2] = CellStatus.DamagedPartOfShip;
            map.CellsStatuses[0, 9] = CellStatus.DamagedPartOfShip;
            Random random = new Random(314159);
            //Act
            var method = typeof(ShotAlong).GetMethod("Shot", BindingFlags.NonPublic | BindingFlags.Instance);
            //Assert
            Assert.ThrowsException<TargetInvocationException>(() =>
            {
                try
                {
                    if (method != null)
                    {
                        Coordinates coordinates =
                            (Coordinates) method.Invoke(new ShotAlong(), new object[] {map, null});
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException?.Message);
                    throw e;
                }
            });
        }
        
        
      
    }
}