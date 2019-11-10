using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaBattle2Lib;
/*
 * Проверка атомарной функции проверки замещённой площади
 * Проверка функции, которая определяет максимальную длинну корабля по площади
 * Проверка функции, которая возвращает замещённую площадь по длинне максимального корабля
 * Проверка совместной работы всех трёх функций
 * Получаем по размеру карты площадь, которая будет замещена
 * Генерация слоёв по разным размерам с кораблями с проверкой длины корабля
 */
namespace SeaBattle2Tests
{
    [TestClass]
    public class MapholderAtomicMethodsTest
    {
        [DataTestMethod]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(50)]
        [DataRow(100)]
        public void IsAreaLimitIsNotExceeded_1(int countOfFilledCells)
        {
            //Act
            bool isAreaLimitIsNotExceeded = Mapholder.IsAreaLimitIsNotExceeded(countOfFilledCells, 500, 0.2);

            //Assert
            Assert.IsTrue(isAreaLimitIsNotExceeded);
        }

        
        [DataTestMethod]
        [DataRow(1,1,1)]
        [DataRow(1,1,10)]
        [DataRow(1,10,1)]
        [DataRow(10,10,10)]
        [DataRow(10,15,5)]
        [DataRow(10,5,15)]
        public void Test111(int maxShipLength, int width, int height)
        {
            //Act
            bool isMaximumLengthOfTheShipIsNotExceeded =
                Mapholder.IsMaximumLengthOfTheShipIsNotExceeded(maxShipLength, width, height);

            //Assert
            Assert.IsTrue(isMaximumLengthOfTheShipIsNotExceeded);
        }

        
          [TestMethod]
        public void Mapholder_GetFilledAreaByMaxShipLength_1()
        {
            //Arrange
            int maxLenght = 1;
            //Act
            int cellCount = Mapholder.GetFilledAreaByMaxShipLength(maxLenght);
            //Assert
            Assert.AreEqual(1, cellCount);
        }
        
        [TestMethod]
        public void Mapholder_GetFilledAreaByMaxShipLength_2()
        {
            //Arrange
            int maxLenght = 2;
            //Act
            int cellCount = Mapholder.GetFilledAreaByMaxShipLength(maxLenght);
            //Assert
            Assert.AreEqual(4, cellCount);
        }
        
        [TestMethod]
        public void Mapholder_GetFilledAreaByMaxShipLength_3()
        {
            //Arrange
            int maxLenght = 3;
            //Act
            int cellCount = Mapholder.GetFilledAreaByMaxShipLength(maxLenght);
            //Assert
            Assert.AreEqual(10, cellCount);
        }
        
        [TestMethod]
        public void Mapholder_GetFilledAreaByMaxShipLength_4()
        {
            //Arrange
            int maxLenght = 4;
            //Act
            int cellCount = Mapholder.GetFilledAreaByMaxShipLength(maxLenght);
            //Assert
            Assert.AreEqual(20, cellCount);
        }
        
        [TestMethod]
        public void Mapholder_GetFilledAreaByMaxShipLength_5()
        {
            //Arrange
            int maxLenght = 5;
            //Act
            int cellCount = Mapholder.GetFilledAreaByMaxShipLength(maxLenght);
            //Assert
            Assert.AreEqual(35, cellCount);
        }
        
        [TestMethod]
        public void Mapholder_GetFilledAreaByMaxShipLength_6()
        {
            //Arrange
            int maxLenght = 6;
            //Act
            int cellCount = Mapholder.GetFilledAreaByMaxShipLength(maxLenght);
            //Assert
            Assert.AreEqual(56, cellCount);
        }
        
        [TestMethod]
        public void Mapholder_GetMaxShipLengthByArea_1()
        {
            //Arrange
            int width = 100;
            int height = 100;
            int area = width * height;
            double coverageArea = Mapholder.CoverageArea;
            
            //Act
            int maxShipLength = Mapholder.GetMaxShipLengthByArea(width, height, coverageArea);
            int filledAreaMax = Mapholder.GetFilledAreaByMaxShipLength(maxShipLength);
            int filledAreaTooBig = Mapholder.GetFilledAreaByMaxShipLength(maxShipLength+1);
            
            //Assert
            Assert.AreEqual(21, maxShipLength);
            Assert.IsTrue(Mapholder.IsAreaLimitIsNotExceeded(filledAreaMax,area,coverageArea));
            Assert.IsFalse(Mapholder.IsAreaLimitIsNotExceeded(filledAreaTooBig,area,coverageArea));
        }
        
        
        [TestMethod]
        public void Mapholder_GetMaxShipLengthByArea_Many()
        {
            double coverageArea = Mapholder.CoverageArea;
            
            for (int width = 1; width < 10_000; width += 490)
            {
                for (int height = 1; height < 10_000; height+=159)
                {
                    //Arrange
                    int area = width * height;
           
                    //Act
                    int maxShipLength = Mapholder.GetMaxShipLengthByArea(width, height, coverageArea);
            
                    int filledAreaMax = Mapholder.GetFilledAreaByMaxShipLength(maxShipLength);
                    int filledAreaTooBig = Mapholder.GetFilledAreaByMaxShipLength(maxShipLength+1);
            
                    //Assert
                    Assert.IsTrue(Mapholder.IsAreaLimitIsNotExceeded(filledAreaMax,area,coverageArea));
                    Assert.IsFalse(Mapholder.IsAreaLimitIsNotExceeded(filledAreaTooBig,area,coverageArea));
                }
            }
            
        }

        [DataTestMethod]
        [DataRow(10,10,5)]
        [DataRow(100,100,5)]
        [DataRow(7,7,3)]
        [DataRow(7,7,4)]
        [DataRow(7,7,5)]
        [DataRow(7,7,6)]
        [DataRow(7,7,7)]
        [DataRow(2,2,2)]
        public void RandomGenerateMapWithOneShip(int width, int height, int shipLength)
        {
            //Arrange
            Random random = new Random(3141559);
            
            //Act
            var map = Mapholder.RandomGenerateMapWithOneShip(width, height,shipLength,random);
            int countOfShipCells = 0;
          
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    var currentCell = map.CellsStatuses[x, y];
                    if (currentCell == CellStatus.PartOfShip)
                        countOfShipCells++;
                }
            }
                
            //Assert
            Assert.AreEqual(shipLength,countOfShipCells);
        }
    }
}