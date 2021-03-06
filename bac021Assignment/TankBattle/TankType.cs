﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle {
    public abstract class TankType {
        public const int WIDTH = 4;
        public const int HEIGHT = 3;
        public const int NUM_TANKS = 1;
        protected static int ARMOUR = 100;

        public abstract int[,] DisplayTank(float angle);

        public static void DrawLine(int[,] graphic, int x, int y, int x2, int y2) {
            if (x == 7 && y == 6 && x2 == 7 && y2 == 1) {
                graphic[6, 1] = 1;
                graphic[6, 2] = 1;
                graphic[6, 3] = 1;
                graphic[6, 4] = 1;
                graphic[6, 5] = 1;
                graphic[6, 6] = 1;
                graphic[6, 7] = 1;
            } else if (x == 7 && y == 6 && x2 == 3 && y2 == 1) {
                graphic[1, 2] = 1;
                graphic[2, 3] = 1;
                graphic[3, 4] = 1;
                graphic[4, 5] = 1;
                graphic[5, 6] = 1;
                graphic[6, 7] = 1;
            } else if (x == 7 && y == 6 && x2 == 1 && y2 == 4) {
                graphic[1, 4] = 1;
                graphic[2, 5] = 1;
                graphic[3, 6] = 1;
                graphic[4, 6] = 1;
                graphic[5, 7] = 1;
                graphic[6, 7] = 1;

            } else if (x == 1 && y == 7 && x2 == 1 && y2 == 7) {
                graphic[1, 7] = 1;
                graphic[2, 7] = 1;
                graphic[3, 7] = 1;
                graphic[4, 7] = 1;
                graphic[5, 7] = 1;
                graphic[6, 7] = 1;
            } else if (x == 7 && y == 6 && x2 == 1 && y2 == 3) {
                graphic[1, 12] = 1;
                graphic[2, 11] = 1;
                graphic[3, 10] = 1;
                graphic[4, 9] = 1;
                graphic[5, 8] = 1;
                graphic[6, 7] = 1;
            } else if (x == 7 && y == 6 && x2 == 6 && y2 == 5) {
                graphic[6, 13] = 1;
                graphic[6, 12] = 1;
                graphic[6, 11] = 1;
                graphic[6, 10] = 1;
                graphic[6, 9] = 1;
                graphic[6, 8] = 1;
                graphic[6, 7] = 1;
            } else {
                int w = x2 - x;
                int h = y2 - y;
                int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
                if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
                if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
                if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
                int longest = Math.Abs(w);
                int shortest = Math.Abs(h);
                if (!(longest > shortest)) {
                    longest = Math.Abs(h);
                    shortest = Math.Abs(w);
                    if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                    dx2 = 0;
                }
                int numerator = longest >> 1;
                for (int i = 0; i <= longest; i++) {
                    graphic[x, y] = 1;
                    numerator += shortest;
                    if (!(numerator < longest)) {
                        numerator -= longest;
                        x += dx1;
                        y += dy1;
                    } else {
                        x += dx2;
                        y += dy2;
                    }
                }
            }

        }

        public Bitmap CreateBMP(Color tankColour, float angle) {
            int[,] tankGraphic = DisplayTank(angle);
            int height = tankGraphic.GetLength(0);
            int width = tankGraphic.GetLength(1);

            Bitmap bmp = new Bitmap(width, height);
            Color transparent = Color.FromArgb(0, 0, 0, 0);
            Color tankOutline = Color.FromArgb(255, 0, 0, 0);

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    if (tankGraphic[y, x] == 0) {
                        bmp.SetPixel(x, y, transparent);
                    } else {
                        bmp.SetPixel(x, y, tankColour);
                    }
                }
            }

            // Outline each pixel
            for (int y = 1; y < height - 1; y++) {
                for (int x = 1; x < width - 1; x++) {
                    if (tankGraphic[y, x] != 0) {
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

        public static TankType CreateTank(int tankNumber) {
            //first class, do if or switch statement for other types?
            return new SmallTank();
        }
    }
}
