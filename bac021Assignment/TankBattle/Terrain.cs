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

        private static bool[,] map = new bool[HEIGHT,WIDTH]; //ive used width first, THEN height, which appears to work throughout
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
                int setIndexAmount = rng.Next(verticalAmount - 1, verticalAmount + 1);

                for (int i = setIndexAmount; i < HEIGHT; i++)
                {
                    //int vert = i - 1;
                    map[i, col] = true;
                }     
            }
        }

        public bool IsTileAt(int x, int y)
        {
            //Console.WriteLine("Checkin X (across): {0} - and Y(high): {1}", x, y);
            if (map[y, x] == false)
            {
                return false;
            }
            return true;
        }

        public bool CheckTankCollide(int x, int y)
        {
            var oneSixty = x;
            var oneTwenty = y;

            if (
                IsTileAt(oneSixty, oneTwenty) || IsTileAt(oneSixty + 1, oneTwenty) || IsTileAt(oneSixty + 2, oneTwenty) ||
                IsTileAt(oneSixty, oneTwenty + 1) || IsTileAt(oneSixty + 1, oneTwenty + 1) || IsTileAt(oneSixty + 2, oneTwenty + 1) ||
                IsTileAt(oneSixty, oneTwenty + 2) || IsTileAt(oneSixty + 1, oneTwenty + 2) || IsTileAt(oneSixty + 2, oneTwenty + 2) ||
                IsTileAt(oneSixty, oneTwenty + 3) || IsTileAt(oneSixty + 1, oneTwenty + 3) || IsTileAt(oneSixty + 2, oneTwenty + 3)) 
            {
                return true;
            }

            return false;
        }

        public int TankVerticalPosition(int x)
        {

            int lowestValidPoint = 0;
            for (int oneTwenty = 0; oneTwenty < Terrain.HEIGHT - 1; oneTwenty++)
            {
                //Console.WriteLine("Checking {0} across and {1} down, should hit",x,yPos);
                if (this.CheckTankCollide(x, oneTwenty)) //I've screwed up the axis I think?
                {
                    //Console.WriteLine("Hit detected at {0} across and {1} down", x,yPos);
                    lowestValidPoint = oneTwenty;
                    //Console.WriteLine("This means we had to decrement by 1, which is now {0}", lowestValidPoint);
                    //Console.WriteLine("The result of checking {0}X and {1} ypos, should be FALSE: {2}",x,lowestValidPoint,IsTileAt(x,yPos));
                    //Console.WriteLine("This in theory should be TRUE though, {0}X and {1} ypos: {2}",x,lowestValidPoint+1,IsTileAt(x,yPos));
                    //Console.WriteLine("X, or across, input was {0}", x);
                    //Console.WriteLine("Hit detected at {0}, so safe return will be {1}. This means our safe coords are {2},{3}."
                     //   ,yPos,lowestValidPoint,x,lowestValidPoint);
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
            bool movedRequired = false;
            int counter = 0;

            do {                                                                    // stuck in infiniteloop
                counter ++ ;
                for (int oneSixty = WIDTH - 1; oneSixty == 0; oneSixty--)            // start per WIDTH
                {
                    Console.WriteLine($"oneSixty is {oneSixty}");
                    for (int oneTwenty = HEIGHT - 1; oneTwenty == 0; oneTwenty--)    //inner per height
                    {
                        int currentVertCheck = oneTwenty;                           //used as temp below
                        counter++;
                        for (int vertCheck = currentVertCheck - 1; vertCheck == 0; vertCheck--) //Checking one tile above our curreont one
                        {
                            if (                                            // if the below 3 tiles look like this
                                this.IsTileAt(oneSixty, vertCheck - 1) &&   // X
                                (!this.IsTileAt(oneSixty, vertCheck)) &&    // .
                                this.IsTileAt(oneSixty, oneTwenty))         // X
                            {
                                map[vertCheck - 1, oneSixty] = false;
                                map[vertCheck, oneSixty] = true;
                                movedRequired = true;   //swap the top two match files, set movedRequired to true
                                wasMoved = true;        // I think this might be redundant
                            }

                        }
                    }
                }
                if (counter == 119)
                {
                    movedRequired = true;
                }

            } while (movedRequired == false);
            return wasMoved;
        }
    }
}
