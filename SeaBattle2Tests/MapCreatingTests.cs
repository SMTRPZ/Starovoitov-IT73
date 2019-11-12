using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaBattle2Lib;
using System;
using SeaBattle2Lib.Exceptions;
using SeaBattle2Lib.GameLogic;

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

            Assert.ThrowsException<InvalidMapSizeException>(() =>
            {
                Map map = new Map(-10, -5);
            });
            
        }
        [TestMethod]
        public void MapCanOnlyBeCreatedWithAPositiveSize2()
        {
            Assert.ThrowsException<InvalidMapSizeException>(() =>
            {
                Map map = new Map(-10, 5);
            });
         }
        [TestMethod]
        public void MapCanOnlyBeCreatedWithAPositiveSize3()
        {
            Assert.ThrowsException<InvalidMapSizeException>(() =>
            {
                Map map = new Map(10, -5);
            });
        }
    }
}