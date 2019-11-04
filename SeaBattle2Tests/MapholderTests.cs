using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaBattle2Lib;
/*
 *    Нельзя заполнить слишком маленькую карту
 *    Корабли после заполнения не касаются друг друга
 */
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
            catch (Exception e1)
            {
                Assert.IsTrue(e1 is MapSizeIsTooSmallException);
            }
           
        }
        [TestMethod]
        public void TheShipsDoNotTouchEachOther_1()
        {
            //Arrange
            Map map = new Map(5, 5);
            map.CellsStatuses[0, 0] = CellStatus.PartOfShip;
            map.CellsStatuses[1, 1] = CellStatus.PartOfShip;
            
            //Act
            bool mapIsOk = Mapholder.CheckCompliance(ref map);
           
            //Assert
            Assert.IsFalse(mapIsOk);
        }
        [TestMethod]
        public void TheShipsDoNotTouchEachOther_2()
        {
            //Arrange
            Map map = new Map(5, 5);
            map.CellsStatuses[0, 0] = CellStatus.PartOfShip;
            map.CellsStatuses[0, 1] = CellStatus.PartOfShip;
            
            //Act
            bool mapIsOk = Mapholder.CheckCompliance(ref map);
           
            //Assert
            Assert.IsTrue(mapIsOk);
        }
        [TestMethod]
        public void TheShipsDoNotTouchEachOther_3()
        {
            //Arrange
            Map map = new Map(5, 5);
            map.CellsStatuses[0, 0] = CellStatus.PartOfShip;
            map.CellsStatuses[0, 1] = CellStatus.PartOfShip;
            map.CellsStatuses[1, 1] = CellStatus.PartOfShip;
            
            //Act
            bool mapIsOk = Mapholder.CheckCompliance(ref map);
           
            //Assert
            Assert.IsFalse(mapIsOk);
        }
        [TestMethod]
        public void TheShipsDoNotTouchEachOther_4()
        {
            //Arrange
            Map map = new Map(5, 5);
            map.CellsStatuses[0, 0] = CellStatus.PartOfShip;
            map.CellsStatuses[2, 2] = CellStatus.PartOfShip;
            map.CellsStatuses[4, 4] = CellStatus.PartOfShip;
            
            //Act
            bool mapIsOk = Mapholder.CheckCompliance(ref map);
           
            //Assert
            Assert.IsTrue(mapIsOk);
        }
        [TestMethod]
        public void TheShipsDoNotTouchEachOther_5()
        {
            //Arrange
            Map map = new Map(5, 5);
            map.CellsStatuses[0, 1] = CellStatus.PartOfShip;
            map.CellsStatuses[0, 2] = CellStatus.PartOfShip;
            map.CellsStatuses[0, 3] = CellStatus.PartOfShip;
            
            map.CellsStatuses[1, 1] = CellStatus.PartOfShip;
            map.CellsStatuses[1, 2] = CellStatus.PartOfShip;
            map.CellsStatuses[1, 3] = CellStatus.PartOfShip;

            //Act
            bool mapIsOk = Mapholder.CheckCompliance(ref map);
           
            //Assert
            Assert.IsFalse(mapIsOk);
        }

//        [TestMethod]
//        public void MapFilling_1()
//        {
//            //Arrange
//            Map map = new Map(5,40);
//            
//            //Act
//            Mapholder.FillOutTheMap(ref map);
//            bool containsPartOfShip = false;
//            for (int x  = 0; x  < map.Width; x ++)
//            {
//                for (int y = 0; y <map.Height; y++)
//                {
//                    var currentCell = map.CellsStatuses[x, y];
//                    if (currentCell == CellStatus.PartOfShip)
//                    {
//                        containsPartOfShip = true;
//                        break;
//                    }
//                }
//            }
//            
//            //Assert
//            Assert.IsTrue(containsPartOfShip);
//        }
        
   
    }
}