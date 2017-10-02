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
            // we only need to check 2 axis, because a TRUE cannot be only in the middle, above, left or right only.
            // index from top left

            // need to add constraints 

            if (map[x+3, y]|| map[x+3, y + 1]|| map[x+3, y+2]) //check bottom axis
            { 
                return true;
            }
            if (map[x, y+2]|| map[x+1, y+2] || map[x+2, y+2] || map[x+3, y+2]) // check right axis
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
            for (int yPos = 3; yPos < Terrain.HEIGHT - 1; yPos++)
            {
                if (this.CheckTankCollide(yPos, x)) //I've screwed up the axis I think?
                {
                    lowestValidPoint = yPos-1;
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
