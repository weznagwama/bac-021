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

        private static int tankWidth;
        private static int tankHeight;
        private static int tankNo;
        private static TankType playerTank;

        public abstract int[,] DisplayTank(float angle);

        public static void DrawLine(int[,] graphic, int X1, int Y1, int X2, int Y2)
        {
            throw new NotImplementedException();
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
            tankWidth = WIDTH;
            tankHeight = HEIGHT;
            return new TankType();
        }
    }
}
