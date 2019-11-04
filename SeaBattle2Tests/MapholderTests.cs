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

        [DataTestMethod]
        [DataRow(1,5)]
        public void TheShipsDoNotTouchEachOther(int width, int height)
        {
            //Arrange
            Map map = new Map(width, height);
            
            //Act
            Mapholder.CheckCompliance(ref map);
           
            //Assert
            //TODO пройтись по карте и проверить, что в радиусе 1 нет кораблей
            //Горизонтальный проход
            //Вертикальный проход
            //Проход по двум диагоналям
        }

      
        
   
    }
}