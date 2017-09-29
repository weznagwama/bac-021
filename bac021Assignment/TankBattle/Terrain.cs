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

        private static string[,] map = new string[HEIGHT,WIDTH];
        private static int terrainWidth;
        private static int terrainHeight;
        private static int landHeight;

        Random rng = new Random();

        public Terrain()
        {
            Terrain.terrainWidth = WIDTH;
            Terrain.terrainHeight = HEIGHT;
            landHeight = rng.Next(0,HEIGHT);

            //first run
            for (int row = 0; row < HEIGHT; row++) {
                for (int col = 0; col < WIDTH; col++) {
                    map[row, col] = ".";
                }
            }

            //create the land height
            // get horizontal amount
            // if horizontal amount == 0, then set vertical amount of landheight, else
            // get random vertical amount +/1 1 of previous amount ,starting from landHeight
            // highPoint = height - landHeight
            //for (int i = highpoint;i<HEIGHT;i++){map[horizontal, i] = "x";}
            for (int row = 0; row < HEIGHT; row++) {
                for (int col = 0; col < WIDTH; col++) {
                    map[row, col] = ".";
                }
            }
        }

        public bool IsTileAt(int x, int y)
        {
            throw new NotImplementedException();
        }

        public bool CheckTankCollide(int x, int y)
        {
            throw new NotImplementedException();
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
