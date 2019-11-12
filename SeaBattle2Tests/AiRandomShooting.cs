using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaBattle2Lib;
using SeaBattle2Lib.GameLogic;
using SeaBattle2Lib.Shooting;

namespace SeaBattle2Tests
{
    [TestClass]
    public class AiRandomShooting
    {
        [TestMethod]
        public void RandomShooting_1()
        {
            //Arrange
            Map map = new Map(10,10);
            Random random = new Random(314159);
            
            //Act
            Coordinates shotCoordinates = Ai.MakeShot(ref map, random);
            Console.WriteLine(shotCoordinates);
            
            //Assert
            Assert.AreEqual(new Coordinates(3,9), shotCoordinates);
        }
        
        [TestMethod]
        public void RandomShooting_2()
        {
            //Arrange
            Map map = new Map(10,10);
            Random random = new Random(314159265);
            
            //Act
            Coordinates shotCoordinates = Ai.MakeShot(ref map, random);
            Console.WriteLine(shotCoordinates);
            
            //Assert
            Assert.AreEqual(new Coordinates(4,0), shotCoordinates );
        }
    }
}