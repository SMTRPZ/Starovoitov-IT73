using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaBattle2Lib;
using SeaBattle2Lib.Exceptions;
using SeaBattle2Lib.GameLogic;
using SeaBattle2Lib.Shooting;

namespace SeaBattle2Tests
{
    [TestClass]
    public class NotAiRandomShooting
    {
        [TestMethod]
        public void RandomShooting_1()
        {
            //Arrange
            Map map = new Map(10,10);
            Random random = new Random(314159);
            
            //Act
            Coordinates shotCoordinates = NotAi.MakeShot(ref map, random);
            Console.WriteLine(shotCoordinates);
            
            //Assert
            Assert.AreEqual(new Coordinates(3,4), shotCoordinates);
        }
        
        [TestMethod]
        public void RandomShooting_2()
        {
            //Arrange
            Map map = new Map(10,10);
            Random random = new Random(314159265);
            
            //Act
            Coordinates shotCoordinates = NotAi.MakeShot(ref map, random);
            Console.WriteLine(shotCoordinates);
            
            //Assert
            Assert.AreEqual(new Coordinates(4,4), shotCoordinates );
        }
        
        [TestMethod]
        [ExpectedException(typeof(OtherPlayerMustShootException))]
        public void Game_1()
        {
            //Arrange
            Game game = new Game(10, 10);
            
            //Act
            game.PlayerAutoShot(Player.First);
            game.PlayerAutoShot(Player.First);

        }
        
        
        
        
    }
}