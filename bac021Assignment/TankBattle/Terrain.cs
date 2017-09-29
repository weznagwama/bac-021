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

         if (map[x, y] == false)
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
            if (map[x+3, y+2]|| map[x+3, y + 1]|| map[x+3, y]) { //check col/length
                return true;
            }
            if (map[x + 3, y+2]|| map[x+2, y=2] || map[x+1, y+2] || map[x, y+2]) //cehck row height, checking too far ahead compared to test?
            {
                return true;
            }
            else {
                return false;
            }
        }

        public int TankVerticalPosition(int x)
        {
            throw new NotImplementedException();
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
