using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace TankBattle {
    class SmallTank : TankType
    {

        private static string[] tankWeapons = { "Standard Shell", "Large Shell" };

        //do we overrite inherited fields here? to test
        private new static int WIDTH = 4;
        private new static int HEIGHT = 3;
        private int smallArmour = ARMOUR;

        public SmallTank()
        {

        }

        public override int[,] DisplayTank(float angle)
        {
            //static for now, create algorithm later
            int[,] graphic = { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0 },
                { 0, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 0 },
                { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };

            if (angle < -67.5) //far left
            {
                SmallTank.DrawLine(graphic, 7,6,2,6);
            }
            if (angle > -67.5 && angle <= -45) { //bit left
                SmallTank.DrawLine(graphic, 7, 6, 3, 2);
            }
            if (angle > -45 && angle <= 0) { // towards the middle
                SmallTank.DrawLine(graphic, 7, 6, 7, 1);
            }
            if (angle > 0 && angle <= 45) { //right
                SmallTank.DrawLine(graphic, 7, 6, 1, 2);
            }
            if (angle > 45) { //farther right
                SmallTank.DrawLine(graphic, 7, 6, 1, 2);
            }
            return graphic;

        }

        public override int GetTankArmour()
        {
            return smallArmour;
        }

        public override string[] ListWeapons()
        {
            return tankWeapons;
        }

        public override void ActivateWeapon(int weapon, ControlledTank playerTank, Battle currentGame)
        {
            //int weapon is based on strings from listWeapons, input can only be between lenght of array
            string weaponChoice = tankWeapons[weapon];
            //Use XPos() and Y() on the ControlledTank to get the tank's coordinates.
            //Convert the coordinates into floats and add on half the values of TankType.HEIGHT and TankType.WIDTH respectively to them, to get the position at the centre of the tank.
            var xtemp = playerTank.XPos();
            var ytemp = playerTank.Y();
            xtemp = xtemp + (WIDTH / 2);
            ytemp = ytemp + (HEIGHT / 2);

            float xPos = (float) xtemp;
            float yPos = (float) xtemp;
            
            //Get the GenericPlayer associated with the ControlledTank passed to ActivateWeapon by using GetPlayerNumber.
            var playerNum = playerTank.GetPlayerNumber();

            //Create a new Blast to reflect the payload of the weapon.Reasonable values to pass in are 100(for damage), 4(for explosion radius) and 4(for earth destruction radius).
            Blast weaponBlast = new Blast(100,4,4);
            //Create a new Bullet for the projectile itself.Pass in the X and Y coordinates of the centre of the tank, the angle and power
            //(from ControlledTank's GetTankAngle() and GetPower() respectively), a reasonable value for gravity (e.g. 0.01f), then the Blast 
            //and GenericPlayer references we just got.
            float gravity = 0.01f;
            Bullet weaponBullet = new Bullet(xPos,yPos,playerTank.GetTankAngle(),playerTank.GetPower(),gravity,weaponBlast,playerNum);

            //Call Battle's AddEffect(), passing in the newly-created Bullet.
            currentGame.AddEffect(weaponBullet);
        }

    }
}
