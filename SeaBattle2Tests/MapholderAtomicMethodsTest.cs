using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaBattle2Lib;

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
            bool isAreaLimitIsNotExceeded = Mapholder.IsAreaLimitIsNotExceeded(2, 500, 0.2);

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

    }
}