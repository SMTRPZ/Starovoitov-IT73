using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaBattle2Lib;

namespace SeaBattle2Tests
{
    [TestClass]
    public class MapholderTests
    {
        [DataTestMethod]
        [DataRow(1,1)]
        [DataRow(1,2)]
        [DataRow(1,3)]
        [DataRow(1,4)] 
        
        [DataRow(1,1)]
        [DataRow(2,1)]
        [DataRow(3,1)]
        [DataRow(4,1)]
        
        [DataRow(2,2)]
        public void FillOutATooSmallMap(int width, int height)
        {
            //Arrange
            Map map = new Map(width,height);
            
            //Act
            try
            {
                Mapholder.FillOutTheMap(ref map);
                Assert.Fail();
            }
            catch (MapSizeIsTooSmallException e1)
            {

            }
            catch (Exception e2)
            {
                Assert.Fail();
            }
        }

        [DataTestMethod]
        [DataRow(1,5)]
        public void TheShipsDoNotTouchEachOther(int width, int height)
        {
            //Arrange
            Map map = new Map(width, height);
            
            //Act
           Mapholder.FillOutTheMap(ref map);
           
           //Assert
           //TODO пройтись по карте и проверить, что в радиусе 1 нет кораблей
        }

        [TestMethod]
        public void Test1()
        {
            //Arrange
            int maxLenght = 1;
            //Act
            int cellCount = Mapholder.GetFilledAreaByMaxShipLength(maxLenght);
            //Assert
            Assert.AreEqual(1, cellCount);
        }
        
        [TestMethod]
        public void Test2()
        {
            //Arrange
            int maxLenght = 2;
            //Act
            int cellCount = Mapholder.GetFilledAreaByMaxShipLength(maxLenght);
            //Assert
            Assert.AreEqual(4, cellCount);
        }
        
        [TestMethod]
        public void Test3()
        {
            //Arrange
            int maxLenght = 3;
            //Act
            int cellCount = Mapholder.GetFilledAreaByMaxShipLength(maxLenght);
            //Assert
            Assert.AreEqual(10, cellCount);
        }
        
        [TestMethod]
        public void Test4()
        {
            //Arrange
            int maxLenght = 4;
            //Act
            int cellCount = Mapholder.GetFilledAreaByMaxShipLength(maxLenght);
            //Assert
            Assert.AreEqual(20, cellCount);
        }
        
        [TestMethod]
        public void Test5()
        {
            //Arrange
            int maxLenght = 5;
            //Act
            int cellCount = Mapholder.GetFilledAreaByMaxShipLength(maxLenght);
            //Assert
            Assert.AreEqual(35, cellCount);
        }
        
        [TestMethod]
        public void Test6()
        {
            //Arrange
            int maxLenght = 6;
            //Act
            int cellCount = Mapholder.GetFilledAreaByMaxShipLength(maxLenght);
            //Assert
            Assert.AreEqual(56, cellCount);
        }
        
        [TestMethod]
        public void TestMaxShipLength1()
        {
            //Arrange
            int width = 100;
            int height = 100;
            int area = width * height;
            double coverageArea = Mapholder.CoverageArea;
            
            //Act
            int maxShipLength = Mapholder.GetMaxShipLengthByArea(width, height, coverageArea);
            int filledArea = Mapholder.GetFilledAreaByMaxShipLength(maxShipLength);
            
            //Assert
            Assert.AreEqual(100, maxShipLength);
            Assert.IsTrue(filledArea<=coverageArea*area);
        }
        
        [TestMethod]
        public void TestMaxShipLength2()
        {
            //Arrange
            int width = 1;
            int height = 10_000;
            //Act
            int maxShipLength = Mapholder.GetMaxShipLengthByArea(width, height, Mapholder.CoverageArea);
            //Assert
            Assert.AreEqual(492, maxShipLength);
        }
        
        [TestMethod]
        public void TestMaxShipLength3()
        {
            //Arrange
            int width = 400;
            int height = 400;
            //Act
            int maxShipLength = Mapholder.GetMaxShipLengthByArea(width, height, Mapholder.CoverageArea);
            //Assert
            Assert.AreEqual(400, maxShipLength);
        }
    }
}