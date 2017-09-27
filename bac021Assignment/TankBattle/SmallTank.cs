using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace TankBattle {
    class SmallTank : TankType
    {

        private static string[] tankWeapons = { "std", "big" };

        private static int ourWidth;
        private static int ourHeight;
        private static int ourNo;
        private static string[] ourWeapons;
        private int ourArmour;

        protected SmallTank() {
        }

        public SmallTank(int ourNumber)
        {
            ourWidth = WIDTH;
            ourHeight = HEIGHT;
            ourNo = ourNumber;
            ourWeapons = tankWeapons;
            ourArmour = ARMOUR;
        }

        public override int[,] DisplayTank(float angle)
        {
            throw new NotImplementedException();
        }

        public override int GetTankArmour()
        {
            return ourArmour;
        }

        public override string[] ListWeapons()
        {
            return tankWeapons;
        }

        public override void ActivateWeapon(int weapon, ControlledTank playerTank, Battle currentGame)
        {
            throw new NotImplementedException();
        }

    }
}
