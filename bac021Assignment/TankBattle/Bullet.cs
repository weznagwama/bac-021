using System;
using System.Drawing;

namespace TankBattle
{
    public class Bullet : AttackEffect
    {
        private float xFloat;
        private float yFloat;
        private float bulletGravity;
        private Blast bulletExplosion;
        private GenericPlayer thePlayer;


        private static float xVol;
        private static float yVol;

        public Bullet(float x, float y, float angle, float power, float gravity, Blast explosion, GenericPlayer player)
        {
            xFloat = x;
            yFloat = y;
            bulletGravity = gravity;
            bulletExplosion = explosion;
            thePlayer = player;


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


                int windSpeed = gameWindSpeed;
                xFloat = xFloat + xVol;
                yFloat = yFloat + yVol;
                xFloat = xFloat + (windSpeed / 1000.0f);
                if (xFloat >= Terrain.WIDTH)
                {
                    theGame.EndEffect(this);
                }

                if (yFloat >= Terrain.HEIGHT)
                {
                    theGame.EndEffect(this);
                }

               if (theGame.CheckCollidedTank((float)Math.Round(xFloat,0), (float)Math.Round(yFloat,0)))
                {
                    thePlayer.ProjectileHitPos(xFloat,yFloat);
                    bulletExplosion.Activate(xFloat,yFloat);
                    theGame.AddEffect(bulletExplosion);
                    theGame.EndEffect(this);
                }
                yVol = yVol + bulletGravity;
            }
            
        }

        public override void Display(Graphics graphics, Size size)
        {
            xFloat = xFloat * size.Width / Terrain.WIDTH;
            yFloat = yFloat * size.Height / Terrain.HEIGHT;
            float s = size.Width / Terrain.WIDTH;

            RectangleF r = new RectangleF(xFloat - s / 2.0f, yFloat - s / 2.0f, s, s);
            Brush b = new SolidBrush(Color.WhiteSmoke);

            graphics.FillEllipse(b, r);
        }
    }
}
