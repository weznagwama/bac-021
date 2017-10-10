using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class Terrain
    {
        public const int HEIGHT = 120;
        public const int WIDTH = 160;

        private bool[,] map = new bool[HEIGHT,WIDTH]; //ive used width first, THEN height, which appears to work throughout
        private static int terrainWidth;
        private static int terrainHeight;
        private static int landHeight;

        Random rng = new Random();

        public Terrain()
        {
            Terrain.terrainWidth = WIDTH;
            Terrain.terrainHeight = HEIGHT;

            landHeight = rng.Next(10,HEIGHT);
            int verticalAmount = HEIGHT-landHeight;

            //first run, set landscape
            //names are wrong but im lazy
            for (int row = 0; row < HEIGHT; row++) {
                for (int col = 0; col < WIDTH; col++) {
                  map[row, col] = false;
                }
            }

            //create the land, set true for tile
            for (int col = 0; col < WIDTH; col++)
            {
                int setIndexAmount = rng.Next(verticalAmount - 1, verticalAmount + 2);
                //int setIndexAmount = verticalAmount;
                for (int i = setIndexAmount; i < HEIGHT; i++)
                {
                    //int vert = i - 1;
                    map[i, col] = true;
                }     
            }
        }

        public bool IsTileAt(int x, int y)
        {
            var oneSixty = x;
            var oneTwenty = y;
            
            if (map[oneTwenty, oneSixty] == false)
            {
                return false;
            }
            return true;
        }

        public bool CheckTankCollide(int x, int y)
        {
            bool collide = false;

            for (int iy = 0; iy < TankType.HEIGHT; iy++) {
                for (int ix = 0; ix < TankType.WIDTH; ix++) {

                    if (IsTileAt(x + ix, y + iy))
                    {
                        collide = true;
                    }
                }
            }

            return collide;
        }

        public int TankVerticalPosition(int x)
        {
            int lowestValidPoint = 0;
            for (int oneTwenty = 0; oneTwenty < Terrain.HEIGHT - 1; oneTwenty++)
            {
 
                if (this.CheckTankCollide(x, oneTwenty)) 
                {
 
                    lowestValidPoint = oneTwenty-1;
                    
                    return lowestValidPoint;
                }
            }
            return lowestValidPoint;
        }

        public void DestroyGround(float destroyX, float destroyY, float radius)
        {
            for (int oneTwenty = 0; oneTwenty < HEIGHT; oneTwenty++)
            {
                for (int oneSixty = 0; oneSixty < WIDTH; oneSixty++)
                {
                    double calculation = Math.Sqrt(Math.Pow(oneSixty - destroyX, 2) +
                                                   Math.Pow(oneTwenty - destroyY, 2));
                    float newCalc = (float)calculation;
                    if (newCalc < radius)
                    {
                        map[oneTwenty, oneSixty] = false;
                    }
                }
            }
        }

        public bool CalculateGravity()
        {
            bool wasMoved = false;
                for (int oneTwenty = HEIGHT - 1; oneTwenty > 0; oneTwenty--) // start per WIDTH
                {
                    for (int oneSixty = 0; oneSixty < WIDTH; oneSixty++)   //inner per height
                    {
                    if (!IsTileAt(oneSixty, oneTwenty) && IsTileAt(oneSixty, oneTwenty - 1))
                        {
                        map[oneTwenty, oneSixty] = true;
                        map[oneTwenty-1, oneSixty] = false;
                        wasMoved = true;

                        }
                    }
                }
            return wasMoved;
        }
    }
}
