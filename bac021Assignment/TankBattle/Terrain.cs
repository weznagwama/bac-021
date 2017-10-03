using System;
using System.Collections.Generic;
using System.Linq;
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

            landHeight = rng.Next(5,HEIGHT);
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
                int setIndexAmount = rng.Next(verticalAmount - 2, verticalAmount + 2);

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
            else
            {
                return true;
            }
        }

        public bool CheckTankCollide(int x, int y)
        {

            //this needs work, beleive it's wrong
            //x x    -> Y axis
            //  x v  X AXIS
            //  x
            //xxx

            if (
                IsTileAt(x,y)       || IsTileAt(x + 1,y)        || IsTileAt(x + 2,y) ||
                IsTileAt(x, y + 1)  || IsTileAt(x + 1, y + 1)   || IsTileAt(x + 2, y + 1) ||
                IsTileAt(x, y + 2)  || IsTileAt(x + 1, y + 2)   || IsTileAt(x + 2, y + 2) ||
                IsTileAt(x, y + 3)  || IsTileAt(x + 1, y + 3)   || IsTileAt(x + 2, y + 3)
                ) //check bottom axis, 3 down incriment across, Y up/down X left/right
            { 
                return true;
            }

            else {
                return false;
            }
        }

        public int TankVerticalPosition(int x)
        {

            // checks for lowest vertical positon of tank, returning the Y coord
            // takes X input, which is across, makes sense(??)
            // starting at 0 of HEIGHT, enumerate through each index of column
            // returning CheckTankCollide(). Return number once true?
            // or return number-1 once true?

            int lowestValidPoint = 0;
            for (int yPos = 0; yPos < Terrain.HEIGHT - 1; yPos++)
            {
                Console.WriteLine("Checking {0} across and {1} down, should hit",x,yPos);
                if (this.CheckTankCollide(x, yPos)) //I've screwed up the axis I think?
                {
                    Console.WriteLine("Hit detected at {0} across and {1} down", x,yPos);
                    lowestValidPoint = yPos;
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
            throw new NotImplementedException();
        }

        public bool CalculateGravity()
        {
            throw new NotImplementedException();
        }
    }
}
