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

        private static GenericPlayer playa;
        private static TankType playaTank;
        private static int tankXpos;
        private static int tankYpos;
        private static Battle theGame;

        private static float barrelAngle = 0;
        private static int tankPower = 25;
        private static int currentWeapon = 0;

        private static int tankDurability;
        private static Bitmap lastTank; 

        public ControlledTank(GenericPlayer player, int tankX, int tankY, Battle game)
        {
            playa = player;
            tankXpos = tankX;
            tankYpos = tankY;
            theGame = game;

            playa.CreateTank();
            tankDurability = playaTank.GetTankArmour();
            lastTank = playaTank.CreateBMP(playa.PlayerColour(), barrelAngle);

        }

        public GenericPlayer GetPlayerNumber()
        {
            return playa;
        }
        public TankType CreateTank()
        {
            playaTank = playa.CreateTank();
            return playaTank;
        }

        public float GetTankAngle()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        public int Y()
        {
            throw new NotImplementedException();
        }

        public void Launch()
        {
            throw new NotImplementedException();
        }

        public void DamageArmour(int damageAmount)
        {
            throw new NotImplementedException();
        }

        public bool IsAlive()
        {
            throw new NotImplementedException();
        }

        public bool CalculateGravity()
        {
            throw new NotImplementedException();
        }
    }
}
