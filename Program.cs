using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Random r = new Random();
            var bmp = new Bitmap("map.bmp");
            int gridXCellSize = (int)Math.Round((double)bmp.Width / 1000);
            int gridYCellSize = (int)Math.Round((double)bmp.Height / 1000);
            int gridX = bmp.Width / gridXCellSize;
            int gridY = bmp.Height / gridYCellSize;

            List<Cell> Fire = new List<Cell>();

            Cell[,] grid = new Cell[gridX,gridY];

            //initialazing and setting bioms

            for (int j = 0; j < gridY;j++)
            {
                for (int i = 0; i < gridX; i++)
                {
                    if (i > 450 && i < 500 && j > 450 && j < 500)
                    {
                        grid[i, j] = new Cell(i, j, 1, true, 20, 7, 3);
                        if (i == 451 || i == 499 || j == 451 || j == 499)
                        {
                            Fire.Add(grid[i, j]);
                        }
                    }
                    //bioms
                    else if (i > 0 && i < 306 && j > 0 && j < 234)
                    {
                        grid[i, j] = new Cell(i, j, 1, false, 15, 8, 2f);
                        grid[i, j].CalcRate();
                    }
                    else if (i >= 306 && i < 612 && j > 0 && j < 234)
                    {
                        grid[i, j] = new Cell(i, j, 1, false, 15, 8, 1f);
                        grid[i, j].CalcRate();
                    }
                    else if (i >= 612 && i < gridX && j > 0 && j <= 234)
                    {
                        grid[i, j] = new Cell(i, j, 1, false, 15, 8, 1.6f);
                        grid[i, j].CalcRate();
                    }
                    //вторая строка
                    else if (i >= 0 && i < 306 && j >= 234 && j < 468)
                    {
                        grid[i, j] = new Cell(i, j, 1, false, 15, 8, 0.6f);
                        grid[i, j].CalcRate();
                    }
                    else if (i >= 306 && i < 612 && j >= 234 && j < 468)
                    {
                        grid[i, j] = new Cell(i, j, 1, false, 15, 8, 0.2f);
                        grid[i, j].CalcRate();
                    }
                    else if (i >= 612 && i < gridX && j >= 234 && j < 468)
                    {
                        grid[i, j] = new Cell(i, j, 1, false, 15, 8, 2f);
                        grid[i, j].CalcRate();
                    }
                    //третья строка
                    else if (i >= 0 && i < 306 && j >= 468 && j < 702)
                    {
                        grid[i, j] = new Cell(i, j, 1, false, 15, 8, 2f);
                        grid[i, j].CalcRate();
                    }
                    else if (i >= 306 && i < 612 && j >= 468 && j < 702)
                    {
                        grid[i, j] = new Cell(i, j, 1, false, 15, 8, 1f);
                        grid[i, j].CalcRate();
                    }
                    else if (i >= 612 && i < gridX && j >= 234 && j < 468)
                    {
                        grid[i, j] = new Cell(i, j, 1, false, 15, 8, 2f);
                        grid[i, j].CalcRate();
                    }
                    //последняя строка
                    else if (i >= 0 && i < 306 && j >= 702 && j < gridY)
                    {
                        grid[i, j] = new Cell(i, j, 1, false, 15, 8, 0.6f);
                        grid[i, j].CalcRate();
                    }
                    else if (i >= 306 && i < 612 && j >= 702 && j < gridY)
                    {
                        grid[i, j] = new Cell(i, j, 1, false, 15, 8, 0.2f);
                        grid[i, j].CalcRate();
                    }

                    else //(i >= 612 && i < gridX && j >= 702 && j < gridY)
                    {
                        grid[i, j] = new Cell(i, j, 1, false, 15, 8, 1f);
                        grid[i, j].CalcRate();
                    }
                    //город
                    if (i >= 326 && i < 357 && j >= 644 && j < 658)
                    {
                        grid[i, j] = new Cell(i, j, 1, false, 15, 8, 2f);
                        grid[i, j].CalcRate();
                    }

                }
            }


            if (Fire.Count>0)
            {
                foreach (Cell c in Fire)
                {
                    c.CalcWind(grid, gridX, gridY);
                }
            }

            foreach (Cell c in grid)
            {
               // c.Probability += 0;
                //c.Probability += (float)r.NextDouble();
                c.SetColor(bmp);   
            }
            bmp.Save("file.bmp");
            
            Console.WriteLine($"{bmp.Width} x {bmp.Height}\ngrid cell: {gridXCellSize} x {gridYCellSize} \ngrid size: {gridX} x {gridY}");

            Console.ReadLine();

            
        }
    }
}
