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

        private static float barrelAngle = 0;
        private static int tankPower = 25;
        private static int currentWeapon = 0;

        private static int tankDurability;
        private static Bitmap lastTank; 

        public ControlledTank(GenericPlayer player, int tankX, int tankY, Battle game)
        {
            this.player = player;
            this.tankX = tankX;
            this.tankY = tankY;
            this.game = game;

            playaTank = player.CreateTank();
            tankDurability = playaTank.GetTankArmour();
            lastTank = playaTank.CreateBMP(player.PlayerColour(), barrelAngle);
         
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            this.CreateTank();
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
            throw new NotImplementedException();
        }
    }
}
