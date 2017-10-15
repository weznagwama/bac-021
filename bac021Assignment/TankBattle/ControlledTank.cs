using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class ControlledTank
    {

        private GenericPlayer player;
        private static TankType playaTank;
        private int tankX;
        private int tankY;
        private Battle game;

        private float barrelAngle;
        private int tankPower;
        private static int currentWeapon;

        private int tankDurability;
        private static Bitmap lastTank; 

        public ControlledTank(GenericPlayer player, int tankX, int tankY, Battle game)
        {
            this.player = player;
            this.tankX = tankX;
            this.tankY = tankY;
            this.game = game;

            barrelAngle = 0; // this works, so it's the tank redraw which isn't working.
            tankPower = 25;
            currentWeapon = 0;

            playaTank = this.player.CreateTank();
            tankDurability = playaTank.GetTankArmour();
            lastTank = playaTank.CreateBMP(this.player.PlayerColour(), barrelAngle);
         
        }

        public GenericPlayer GetPlayerNumber()
        {
            return player;
        }
        public TankType CreateTank()
        {
            return player.CreateTank();
        }

        public float GetTankAngle()
        {
            return barrelAngle;
        }

        public void Aim(float angle)
        {
            barrelAngle = angle;
        }

        public int GetPower()
        {
            return tankPower;
        }

        public void SetPower(int power)
        {
            tankPower = power;
        }

        public int GetWeapon()
        {
            return currentWeapon;

        }
        public void SetWeaponIndex(int newWeapon)
        {
            currentWeapon = newWeapon;
        }

        public void Display(Graphics graphics, Size displaySize)
        {
            int drawX1 = displaySize.Width * tankX / Terrain.WIDTH;
            int drawY1 = displaySize.Height * tankY / Terrain.HEIGHT;
            int drawX2 = displaySize.Width * (tankX + TankType.WIDTH) / Terrain.WIDTH;
            int drawY2 = displaySize.Height * (tankY + TankType.HEIGHT) / Terrain.HEIGHT;
            graphics.DrawImage(lastTank, new Rectangle(drawX1, drawY1, drawX2 - drawX1, drawY2 - drawY1));

            int drawY3 = displaySize.Height * (tankY - TankType.HEIGHT) / Terrain.HEIGHT;
            Font font = new Font("Arial", 8);
            Brush brush = new SolidBrush(Color.White);

            int pct = tankDurability * 100 / 100;
            if (pct < 100) {
                graphics.DrawString(pct + "%", font, brush, new Point(drawX1, drawY3));
            }
        }

        public int XPos()
        {
            return tankX;
        }
        public int Y()
        {
            return tankY;
        }

        public void Launch()
        {
            //This causes the ControlledTank to fire its current weapon.This method should call its own CreateTank() method, 
            //then call ActivateWeapon() on that TankType, passing in the current weapon, the this reference and the private Battle field of ControlledTank.

            //var whatIsThis = CreateTank();
            //whatIsThis.ActivateWeapon(currentWeapon,this,game);

            playaTank.ActivateWeapon(currentWeapon,this,game);
        }

        public void DamageArmour(int damageAmount)
        {
            tankDurability = tankDurability - damageAmount;
        }

        public bool IsAlive()
        {
            return tankDurability != 0;
        }

        public bool CalculateGravity()
        {
            if (!IsAlive())
            {
                return false;
            }
            var tempTerrain = game.GetBattlefield();
            if (tempTerrain.CheckTankCollide(tankX, tankY + 1))
            {
                return false;
            }

            tankY++;
            tankDurability--; //falling damage

            if (tankY == Terrain.HEIGHT - TankType.HEIGHT)
            {
                tankDurability = 0;
            }
            return true;
        }
    }
}
