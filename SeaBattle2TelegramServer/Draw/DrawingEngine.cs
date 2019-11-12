using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using SeaBattle.Draw;
using SeaBattle2Lib;
using SeaBattle2Lib.GameLogic;

namespace SeaBattle2TelegramServer.Draw
{
    static class DrawingEngine
    {
        private static int _fileCounter = 1;
        public static FileStream GetPlaygrounds(TelegramSession session)
        {
            var game = session.Game;

            if (game.Player1Map.Width != 10 || game.Player1Map.Height != 10 || game.Player2Map.Width != 10 ||game.Player2Map.Height != 10)
                throw new NotImplementedException("Невозможно отрисовать карту, кроме размера 10 на 10");

            using (Bitmap bitmap = new Bitmap(500, 500 + 100 + 500))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    DrawGameMap(g, game);
                }
                string filePath = $"D:\\green{_fileCounter++}.jpg";
                bitmap.Save(filePath, ImageFormat.Jpeg);
                return new FileStream(filePath, FileMode.Open);
            }
        }

        private static void DrawGameMap(Graphics g, Game game)
        {
            
            DrawCommonBackground(g);

            DrawGamersPlaygrounds(g, game);

            DrawStrings(g, game);
            
        }

        private static void DrawStrings(Graphics graphics, Game game)
        {
            
            string drawString = "Ваше поле";
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            float x = 100;
            float y = 480;
            StringFormat drawFormat = new StringFormat();

            graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);

            drawString = "Поле противника";
            y += 70;

            graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
        }

        private static void DrawGamersPlaygrounds(Graphics graphics, Game game)
        {
            
            DrawPlayground(graphics, game.Player1Map, 50, 10, 40,2,
                PlaygroundType.VisiblePlayground);
            
            DrawPlayground(graphics, game.Player2Map, 50, 10+422+78+78, 40,2, 
                PlaygroundType.InvisiblePlayground);
                       
        }

        private static void DrawPlayground(Graphics graphics,
            Map map,
            int xCoordinate,
            int yCoordinate,
            int cellSize,
            int lineSize,
            PlaygroundType visiblePlayground)
        {

            DrawUserObjects(graphics,map, xCoordinate, yCoordinate, cellSize, lineSize,visiblePlayground);

            DrawNumbering(
                graphics,
                xCoordinate+cellSize/4,
                yCoordinate + (cellSize + lineSize) * 10+cellSize/4,
                cellSize,
                lineSize);
        }

        private static void DrawNumbering(
            Graphics graphics,
            int xCoordinate,
            int yCoordinate,
            int cellSize,
            int lineSize)
        {
            
            using (Font drawFont = new Font("Arial", 16))
            {
                using (SolidBrush drawBrush = new SolidBrush(Color.Black))
                {
                    StringFormat drawFormat = new StringFormat();

                    int index=0;
                    
                    for (int xCoord = xCoordinate; xCoord < xCoordinate + 10 * (cellSize+lineSize) ;xCoord+=cellSize+lineSize)
                    {
                        graphics.DrawString(index.ToString(), drawFont, drawBrush, xCoord, yCoordinate, drawFormat);
                        index++;
                    }

                    index = 10;
                    for (int yCoord = yCoordinate-10*(cellSize+lineSize); yCoord < yCoordinate; yCoord+=(cellSize+lineSize))
                    {
                        index--;
                        graphics.DrawString(index.ToString(), drawFont, drawBrush, xCoordinate-cellSize,yCoord, drawFormat);
                    }
                }
            }
            
            
            
            
            
        }

        private static void DrawUserObjects(
            Graphics graphics,
            Map map,
            int xCoordinateStart,
            int yCoordinateStart,
            int cellSize,
            int lineSize,
            PlaygroundType playgroundType)
        {
            
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    var cell = map.CellsStatuses[x, y];

                    float xCoordinate = xCoordinateStart + x * (cellSize+lineSize);
                    float yCoordinate = yCoordinateStart + 10*(cellSize+lineSize)+lineSize - (cellSize+lineSize) - y * (cellSize+lineSize);

                    Brush brush;

                    switch (playgroundType)
                    {
                        case PlaygroundType.VisiblePlayground:
                            switch (cell)
                            {
                                case CellStatus.Water:
                                    brush = new SolidBrush(Color.Aquamarine);
                                    break;
                                case CellStatus.HittedWater:
                                    brush = new SolidBrush(Color.DarkCyan);
                                    break;
                                case CellStatus.PartOfShip:
                                    brush = new SolidBrush(Color.LightSlateGray);
                                    break;
                                case CellStatus.DamagedPartOfShip:
                                    brush = new SolidBrush(Color.LightCoral);
                                    break;
                                case CellStatus.DestroyedShip:
                                    brush = new SolidBrush(Color.Red);
                                    break;
                                default:
                                    throw new Exception("Неожиданный тип клетки");
                            }
                            break;
                        case PlaygroundType.InvisiblePlayground:
                            switch (cell)
                            {
                                case CellStatus.Water:
                                    brush = new SolidBrush(Color.White);
                                    break;
                                case CellStatus.HittedWater:
                                    brush = new SolidBrush(Color.DimGray);
                                    break;
                                case CellStatus.PartOfShip:
                                    brush = new SolidBrush(Color.White);
                                    break;
                                case CellStatus.DamagedPartOfShip:
                                    brush = new SolidBrush(Color.LightCoral);
                                    break;
                                case CellStatus.DestroyedShip:
                                    brush = new SolidBrush(Color.Red);
                                    break;
                                default:
                                    throw new Exception("Неожиданный тип клетки");
                            }
                            break;
                        default:
                            throw new Exception("Неожиданный тип игрового поля");
                    }
                    
                    
                    graphics.FillRectangle(brush, xCoordinate, yCoordinate, cellSize, cellSize);
                }
            }
        }
        
        private static void DrawCommonBackground(Graphics graphics)
        {
            graphics.Clear(Color.Chartreuse);
        }
        
    }
}