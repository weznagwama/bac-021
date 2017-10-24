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
            // setup
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
            // calculate each tick for bullet, detect collision
            for (int i = 0; i < 10; i++)
            {
                Battle bulletGame = theGame;
                int windSpeed = bulletGame.Wind();
                xFloat = xFloat + xVol;
                yFloat = yFloat + yVol;
                xFloat = xFloat + (windSpeed / 1000.0f);
                if (xFloat >= Terrain.WIDTH || xFloat < 0 || yFloat >= Terrain.HEIGHT)
                {
                    // bullet is off screen left, right or below. Top is OK.
                    bulletGame.EndEffect(this);
                }

               if (bulletGame.CheckCollidedTank(xFloat, yFloat))
                {
                    // hit
                    thePlayer.ProjectileHitPos(xFloat,yFloat);
                    bulletExplosion.Activate(xFloat,yFloat);
                    bulletGame.AddEffect(bulletExplosion);
                    bulletGame.EndEffect(this);
                }
               // no hit or off screen, add grav and continue
                yVol = yVol + bulletGravity;

            }
            
        }

        public override void Display(Graphics graphics, Size size)
        {
            float x = xFloat * size.Width / Terrain.WIDTH;
            float y = yFloat * size.Height / Terrain.HEIGHT;
            float s = size.Width / Terrain.WIDTH;

            RectangleF r = new RectangleF(x - s / 2.0f, y - s / 2.0f, s, s);
            Brush b = new SolidBrush(Color.WhiteSmoke);

            graphics.FillEllipse(b, r);
        }
    }
}
