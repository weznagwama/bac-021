using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class Terrain
    {
        public const int WIDTH = 160;
        public const int HEIGHT = 120;

        private static bool[,] map = new bool[HEIGHT,WIDTH];
        private static int terrainWidth;
        private static int terrainHeight;
        private static int landHeight;

        Random rng = new Random();

        public Terrain()
        {
            Terrain.terrainWidth = WIDTH;
            Terrain.terrainHeight = HEIGHT;

            landHeight = rng.Next(0,HEIGHT-5);
            int verticalAmount = HEIGHT-landHeight;

            //first run, set landscape
            for (int row = 0; row < HEIGHT; row++) {
                for (int col = 0; col < WIDTH; col++) {
                    map[row, col] = false;
                }
            }

            //create the land, set true for tile
            for (int horiz = 0; horiz < WIDTH; horiz++)
            {
                int setIndexAmount = rng.Next(verticalAmount - 1, verticalAmount + 1);

                for (int i = setIndexAmount; i < HEIGHT; i++)
                {
                    //int vert = i - 1;
                    map[i, horiz] = true;
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
            if (map[x+4, y+3] == false) {
                return false;
            } else {
                return true;
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

        public bool[,] returnMap()
        {
            return map;
        }
    }
}
