using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaBattle2Lib;
using System;

/*
Создать карту и проверить её размер +
Создать карту с неадекватными параментами должно упасть +
Карта поддерживает любой прямоугольный размер +
 */
namespace SeaBattle2Tests
{
    [TestClass]
    public class MapCreatingTests
    {

        [TestMethod]
        public void MapMayNotBeASquare()
        {
            try
            {
                Map map = new Map(15, 65);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void MapWidthDoesNotChangeAfterCreation()
        {
            //Arrange
            Map map = new Map(10, 5);

            //Act

            //Assert
            Assert.AreEqual(10, map.Width);            
        }
        [TestMethod]
        public void MapHeightDoesNotChangeAfterCreation()
        {
            //Arrange
            Map map = new Map(10, 5);

            //Act

            //Assert
            Assert.AreEqual(5, map.Height);
        }
        [TestMethod]
        public void MapCanOnlyBeCreatedWithAPositiveSize1()
        {
            try
            {
                Map map = new Map(-10, -5);
                Assert.Fail();
            }catch(InvalidMapSizeException e)
            {

            }catch(Exception e1)
            {
                Assert.Fail();
            }

        }
        [TestMethod]
        public void MapCanOnlyBeCreatedWithAPositiveSize2()
        {
            try
            {
                Map map = new Map(-10, 5);
                Assert.Fail();
            }
            catch (InvalidMapSizeException e)
            {

            }
            catch (Exception e1)
            {
                Assert.Fail();
            }

        }
        [TestMethod]
        public void MapCanOnlyBeCreatedWithAPositiveSize3()
        {
            try
            {
                Map map = new Map(10, -5);
                Assert.Fail();
            }
            catch (InvalidMapSizeException e)
            {

            }
            catch (Exception e1)
            {
                Assert.Fail();
            }

        }

     
    }
}