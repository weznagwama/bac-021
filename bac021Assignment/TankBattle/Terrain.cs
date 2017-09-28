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

        public Terrain()
        {
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
