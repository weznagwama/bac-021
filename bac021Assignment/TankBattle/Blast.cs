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
        private static int explosionDamage;
        private static int explosionRadius;
        private static int earthDestructionRadius;

        private float blastLifespan;
        private float xPos;
        private float yPos;

        public Blast(int explosionDamage, int explosionRadius, int earthDestructionRadius)
        {
            
        }

        public void Activate(float x, float y)
        {
            this.xPos = x;
            this.xPos = y;
            blastLifespan = 1.0f;
    }

        public override void ProcessTimeEvent()
        {
            theGame.DamageArmour(this.xPos,this.yPos,explosionDamage,explosionRadius);
            Terrain ourStuff = theGame.GetBattlefield();
            ourStuff.DestroyGround(this.xPos,this.yPos,earthDestructionRadius);
            theGame.EndEffect(this);
        }

        public override void Display(Graphics graphics, Size displaySize)
        {
            var xPos = (float)this.xPos * displaySize.Width / Terrain.WIDTH;
            var yPos = (float)this.yPos * displaySize.Height / Terrain.HEIGHT;
            float radius = displaySize.Width * (float)((1.0 - blastLifespan) * explosionRadius * 3.0 / 2.0) / Terrain.WIDTH;

            int alpha = 0, red = 0, green = 0, blue = 0;

            if (blastLifespan < 1.0 / 3.0) {
                red = 255;
                alpha = (int)(blastLifespan * 3.0 * 255);
            } else if (blastLifespan < 2.0 / 3.0) {
                red = 255;
                alpha = 255;
                green = (int)((blastLifespan * 3.0 - 1.0) * 255);
            } else {
                red = 255;
                alpha = 255;
                green = 255;
                blue = (int)((blastLifespan * 3.0 - 2.0) * 255);
            }

            RectangleF rect = new RectangleF(xPos - radius, yPos - radius, radius * 2, radius * 2);
            Brush b = new SolidBrush(Color.FromArgb(alpha, red, green, blue));

            graphics.FillEllipse(b, rect);
        }
    }
}
