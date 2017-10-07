using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankBattle
{
    public class Bullet : AttackEffect
    {
        private float xFloat;
        private float yFloat;
        private static float gravity;
        private static Blast explosion;
        private static GenericPlayer player;

        private static float xVol;
        private static float yVol;

        public Bullet(float x, float y, float angle, float power, float gravity, Blast explosion, GenericPlayer player)
        {
            float angleRadians = (90 - angle) * (float)Math.PI / 180;
            float magnitude = power / 50;

            xVol = (float)Math.Cos(angleRadians) * magnitude;
            yVol = (float)Math.Sin(angleRadians) * -magnitude;
        }

        public override void ProcessTimeEvent()
        {
            //do this 10 times
            for (int i = 0; i < 9; i++)
            {
                int windSpeed = theGame.Wind();
                xFloat = xFloat + xVol;
                yFloat = yFloat + yVol;
                xFloat = xFloat + (windSpeed / 1000.0f);
                if (xFloat >= Terrain.WIDTH)
                {
                    theGame.EndEffect(this);
                    return;
                }
                if (yFloat >= Terrain.HEIGHT)
                {
                    theGame.EndEffect(this);
                    return;
                }

                if (theGame.CheckCollidedTank(xFloat, yFloat))
                {
                    player.ProjectileHitPos(xFloat,yFloat);
                    explosion.Activate(xFloat,yFloat);
                    theGame.AddEffect(explosion);
                    theGame.EndEffect(this);
                    return;
                }
                yVol = yVol + gravity;
            }
            
        }

        public override void Display(Graphics graphics, Size size)
        {
            //xfloat could be wrong here?
            xFloat = (float)this.xFloat * size.Width / Terrain.WIDTH;
            yFloat = (float)this.yFloat * size.Height / Terrain.HEIGHT;
            float s = size.Width / Terrain.WIDTH;

            RectangleF r = new RectangleF(xFloat - s / 2.0f, yFloat - s / 2.0f, s, s);
            Brush b = new SolidBrush(Color.WhiteSmoke);

            graphics.FillEllipse(b, r);
        }
    }
}
