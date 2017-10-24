using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace TankBattle {
    class SmallTank : TankType
    {

        private string[] tankWeapons = { "Standard Shell", "Large Shell" };

        //do we overrite inherited fields here? to test
        private new static int WIDTH = 4;
        private new static int HEIGHT = 3;
        private int smallArmour = ARMOUR;

        public SmallTank()
        {

        }

        public override int[,] DisplayTank(float angle)
        {
            //Draw our tank
            int[,] graphic = {  { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
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
                DrawLine(graphic, 7, 6, 7, 1);
            }
            if (angle > -67.5 && angle <= -45) { //bit left
                DrawLine(graphic, 7, 6, 3, 1);
            }
            if (angle > -45 && angle <= -10) { // towards the left
                DrawLine(graphic, 7, 6, 1, 4);
            }
            if (angle < 10 && angle > -10) { // towards the middle
                DrawLine(graphic, 1, 7, 1, 7);
            }

            if (angle >= 10 && angle <= 45) { //right
            DrawLine(graphic, 7, 6, 1, 3);
            }
            if (angle > 45) { //farther right
                DrawLine(graphic, 7, 6, 6, 5);
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

            // find middle of the tank
            var xtemp = playerTank.XPos();
            var ytemp = playerTank.Y();
            xtemp = xtemp + (WIDTH / 2);
            ytemp = ytemp + (HEIGHT / 2);

            float xPos = (float) xtemp;
            float yPos = (float) ytemp;
            
            // generic player associated with tank
            var playerNum = playerTank.GetPlayerNumber();

            //init of bullet and blast variables for different weapons
            Blast weaponBlast;
            Bullet weaponBullet;
            float gravity = 0.01f;

            // standard shell
            if (weapon == 0)
            {
                weaponBlast = new Blast(80, 4, 4);
                weaponBullet = new Bullet(xPos, yPos, playerTank.GetTankAngle(), playerTank.GetPower(), gravity, weaponBlast, playerNum);

            } 
            // large shell
            else
            {
                weaponBlast = new Blast(120,8,8);
                weaponBullet = new Bullet(xPos, yPos, playerTank.GetTankAngle(), playerTank.GetPower(), gravity, weaponBlast, playerNum);

            }
            // add the final effect
            currentGame.AddEffect(weaponBullet);
        }

    }
}
