﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace TankBattle {
    class SmallTank : TankType
    {

        private static string[] tankWeapons = { "std", "big" };

        //do we overrite inherited fields here? to test
        private static int ourWidth;
        private static int ourHeight;
        private static int ourNo;
        private static string[] ourWeapons;
        private int ourArmour;

        protected SmallTank() {
        }

        public SmallTank(int ourNumber)
        {
            //is this shit redundant? to test
            ourWidth = WIDTH;
            ourHeight = HEIGHT;
            ourNo = ourNumber;
            ourWeapons = tankWeapons;
            ourArmour = ARMOUR;
        }

        public override int[,] DisplayTank(float angle)
        {
            //return at int[12,16] array, with 1's for the tank
            throw new NotImplementedException();
        }

        public override int GetTankArmour()
        {
            return ourArmour;
        }

        public override string[] ListWeapons()
        {
            return ourWeapons;
        }

        public override void ActivateWeapon(int weapon, ControlledTank playerTank, Battle currentGame)
        {
            //int weapon is based on strings from listWeapons, input can only be between lenght of array

            //Use XPos() and Y() on the ControlledTank to get the tank's coordinates.
            //Convert the coordinates into floats and add on half the values of TankType.WIDTH and TankType.HEIGHT respectively to them, to get the position at the centre of the tank.
            //Get the GenericPlayer associated with the ControlledTank passed to ActivateWeapon by using GetPlayerNumber.
            //Create a new Blast to reflect the payload of the weapon.Reasonable values to pass in are 100(for damage), 4(for explosion radius) and 4(for earth destruction radius).
            //Create a new Bullet for the projectile itself.Pass in the X and Y coordinates of the centre of the tank, the angle and power(from ControlledTank's GetTankAngle() and GetPower() respectively), a reasonable value for gravity (e.g. 0.01f), then the Blast and GenericPlayer references we just got.
            //Call Battle's AddEffect(), passing in the newly-created Bullet.

            throw new NotImplementedException();
        }

    }
}
