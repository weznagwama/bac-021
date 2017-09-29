using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public abstract class TankType
    {
        public const int WIDTH = 4;
        public const int HEIGHT = 3;
        public const int NUM_TANKS = 1;
        protected static int ARMOUR = 100;

        public abstract int[,] DisplayTank(float angle);

        public static void DrawLine(int[,] graphic, int X1, int Y1, int X2, int Y2)
        {
            bool steep = Math.Abs(Y2 - Y1) > Math.Abs(X2 - X1);
            if (steep)
            {
                int t;
                t = X1; // swap X1 and Y1
                X1 = Y1;
                Y1 = t;
                t = X2; // swap X2 and Y2
                X2 = Y2;
                Y2 = t;
            }
            if (X1 > X2)
            {
                int t;
                t = X1; // swap X1 and X2
                X1 = X2;
                X2 = t;
                t = Y1; // swap Y1 and Y2
                Y1 = Y2;
                Y2 = t;
            }
            int dx = X2 - X1;
            int dy = Math.Abs(Y2 - Y1);
            int error = dx / 2;
            int ystep = (Y1 < Y2) ? 1 : -1;
            int y = Y1;
            for (int x = X1; x <= X2; x++)
            {
                graphic[x,y] = 1;
                error = error - dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }

            }
        }

        public Bitmap CreateBMP(Color tankColour, float angle)
        {
            int[,] tankGraphic = DisplayTank(angle);
            int height = tankGraphic.GetLength(0);
            int width = tankGraphic.GetLength(1);

            Bitmap bmp = new Bitmap(width, height);
            Color transparent = Color.FromArgb(0, 0, 0, 0);
            Color tankOutline = Color.FromArgb(255, 0, 0, 0);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (tankGraphic[y, x] == 0)
                    {
                        bmp.SetPixel(x, y, transparent);
                    }
                    else
                    {
                        bmp.SetPixel(x, y, tankColour);
                    }
                }
            }

            // Outline each pixel
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    if (tankGraphic[y, x] != 0)
                    {
                        if (tankGraphic[y - 1, x] == 0)
                            bmp.SetPixel(x, y - 1, tankOutline);
                        if (tankGraphic[y + 1, x] == 0)
                            bmp.SetPixel(x, y + 1, tankOutline);
                        if (tankGraphic[y, x - 1] == 0)
                            bmp.SetPixel(x - 1, y, tankOutline);
                        if (tankGraphic[y, x + 1] == 0)
                            bmp.SetPixel(x + 1, y, tankOutline);
                    }
                }
            }

            return bmp;
        }

        public abstract int GetTankArmour();

        public abstract string[] ListWeapons();

        public abstract void ActivateWeapon(int weapon, ControlledTank playerTank, Battle currentGame);

        public static TankType CreateTank(int tankNumber)
        {
            //first class, do if or switch statement for other types?
            return new SmallTank();
        }
    }
}
