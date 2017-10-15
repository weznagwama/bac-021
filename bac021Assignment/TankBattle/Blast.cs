using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class Blast : AttackEffect
    {
        private int boomDamage;
        private int boomRadius;
        private int globeDestructionRadius;

        private float blastLifeSpan;
        private float xPos;
        private float yPos;

        public Blast(int explosionDamage, int explosionRadius, int earthDestructionRadius)
        {
            boomDamage = explosionDamage;
            boomRadius = explosionRadius;
            globeDestructionRadius = earthDestructionRadius;
        }

        public void Activate(float x, float y)
        {
            xPos = x;
            yPos = y;
            blastLifeSpan = 1.0f;
    }

        public override void ProcessTimeEvent()
        {
            blastLifeSpan = (float) (blastLifeSpan - 0.05);
            if (blastLifeSpan <= 0)
            {
                theGame.DamageArmour(xPos, yPos, boomDamage, boomRadius);
                Terrain ourStuff = theGame.GetBattlefield();
                ourStuff.DestroyGround(xPos, yPos, globeDestructionRadius);
                theGame.EndEffect(this);
            }

        }

        public override void Display(Graphics graphics, Size displaySize)
        {

            float x = xPos * displaySize.Width / Terrain.WIDTH;
            float y = yPos * displaySize.Height / Terrain.HEIGHT;
            var radius = displaySize.Width * (float)((1.0 - blastLifeSpan) * boomRadius * 3.0 / 2.0) / Terrain.WIDTH;

            int alpha = 0, red = 0, green = 0, blue = 0;

            if (blastLifeSpan < 1.0 / 3.0) {
                red = 255;
                alpha = (int)(blastLifeSpan * 3.0 * 255);
            } else if (blastLifeSpan < 2.0 / 3.0) {
                red = 255;
                alpha = 255;
                green = (int)((blastLifeSpan * 3.0 - 1.0) * 255);
            } else {
                red = 255;
                alpha = 255;
                green = 255;
                blue = (int)((blastLifeSpan * 3.0 - 2.0) * 255);
            }
            RectangleF rect = new RectangleF(x - radius, y - radius, radius*2, radius * 2);
            Brush b = new SolidBrush(Color.FromArgb(alpha, red, green, blue));

            graphics.FillEllipse(b, rect);
        }
    }
}
