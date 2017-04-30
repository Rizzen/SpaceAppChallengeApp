using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Cell
    {
        Color colRed = Color.FromArgb(40, Color.Red);
        Color colYel = Color.FromArgb(40, Color.Yellow);
        Color colOran = Color.FromArgb(40, Color.Orange);
        Color colDarkOrange = Color.FromArgb(80, Color.DarkOrange);
        Color colBrown = Color.FromArgb(40, Color.Brown);
        Color colGreen = Color.FromArgb(40, Color.Green);
        Color colBlue = Color.FromArgb(40, Color.Blue);

        public int X;
        public int Y;// умножать на 2
        /// <summary>
        /// 1 - forest, 2 - river, 3 - city
        /// </summary>
        public int Type;
        public bool IsFire;

        public float Probability =0;

        public float Temperature = 27f;
        public float RH =0.3f; // влажность
        public int WindPower;
        /// <summary>
        /// 1 - upper left; 2 - up; 3 - upper right; 4 - right; 5 - down right; 6  - down; 7 - down left; 8 - left
        /// </summary>
        public int WindDir;
        /// <summary>
        /// from 1 to 5, 1 - highest
        /// in fact - 2, 1.6, 1, 0.6, 0.2
        /// </summary>
        public int CoverDangerClass;

        
        //итоговый коэффициент
        public float FireRate = 0;


        public Cell (int _x, int _y, int _type, bool _isFire, int _windPower, int _windDir, int _CoverDangerClass)
        {
            X = _x;
            Y = _y;
            Type = _type;
            IsFire = _isFire;
            WindPower = _windPower;
            WindDir = _windDir;
            CoverDangerClass = _CoverDangerClass;
            Console.WriteLine($"Created cell {X} - {Y}");
        }

        public void SetColor(Bitmap bmp)
        {
            SolidBrush sb = new SolidBrush(colYel);

            if (FireRate <= 300)
            {
                sb = new SolidBrush(colGreen);
            }
            else if (FireRate >300 && FireRate <= 1000)
            {
                 sb = new SolidBrush(colYel);
            }
            else if (FireRate > 1000 && FireRate <= 4000)
            {
                sb = new SolidBrush(colOran);
            }
            else if (FireRate > 4000 && FireRate <= 8000)
            {
                sb = new SolidBrush(colDarkOrange);
            }
            else
            {
                sb = new SolidBrush(colBrown);
            }

            if (IsFire)
            {
                sb = new SolidBrush(colRed);
            }

            Pen p = new Pen(sb);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawRectangle(p, X, Y * 2, 1, 2);
            sb?.Dispose();
            g?.Dispose();
            p?.Dispose();
        }

        public void CalcWind(Cell[,] grid, int gridX, int gridY)
        {
            switch (WindDir)
            {
                case 1:
                    int i = 0;
                    while (i < WindPower) { if (X - i >= 0 && Y - i >= 0) { grid[X - i, Y - i].FireRate += 3000f; i++; Console.WriteLine($"probability of {X-i} - {Y-i} is now  {grid[X - i, Y - i].FireRate}"); } }
                    break;
                case 2:
                    i = 0;
                    while (i < WindPower) { if (X - i >= 0 && Y - i >= 0) { grid[X, Y - i].FireRate += 3000f; i++; Console.WriteLine($"probability of {X} - {Y - i} is now  {grid[X, Y - i].FireRate}"); } }
                    break;
                case 3:
                    i = 0;
                    while (i < WindPower) { if (X + i <= gridX && Y - i >= 0) { grid[X + i, Y - i].FireRate += 3000f; i++; Console.WriteLine($"probability of {X + i} - {Y - i} is now  {grid[X + i, Y - i].FireRate}"); } }
                    break;
                case 4:
                    i = 0;
                    while (i < WindPower) { if (X + i <= gridX && Y - i >= 0) { grid[X + i, Y].FireRate += 3000f; i++; Console.WriteLine($"probability of {X + i} - {Y} is now  {grid[X + i, Y].FireRate}"); } }
                    break;
                case 5:
                    i = 0;
                    while (i < WindPower) { if (X + i <= gridX && Y + i <= gridY) { grid[X + i, Y + i].FireRate += 3000f; i++; Console.WriteLine($"probability of {X + i} - {Y + i} is now  {grid[X + i, Y + i].FireRate}"); } }
                    break;
                case 6:
                    i = 0;
                    while (i < WindPower) { if (X + i <= gridX && Y + i <= gridY) { grid[X, Y + i].FireRate += 3000f; i++; Console.WriteLine($"probability of {X} - {Y + i} is now  {grid[X, Y + i].FireRate}"); } }
                    break;
                case 7:
                    i = 0;
                    while (i < WindPower) { if (X - i >= 0 && Y + i <= gridY) { grid[X - i, Y + i].FireRate += 3000f; i++; Console.WriteLine($"probability of {X - i} - {Y + i} is now  {grid[X - i, Y + i].FireRate}"); } }
                    break;
                case 8:
                    i = 0;
                    while (i < WindPower) { if (X - i >= 0 && Y + i <= gridY) { grid[X - i, Y].FireRate += 3000f; i++; Console.WriteLine($"probability of {X - i} - {Y} is now  {grid[X - i, Y].FireRate}"); } }
                    break;
            }
        }

        public void CalcRate()
        {
            FireRate = CoverDangerClass * Temperature * (RH / 0.05f) * (0.8664f * WindPower) * RH;
            Console.WriteLine($"Cell {X} - {Y} fireRate is {FireRate}");
        }
 
    }
}
