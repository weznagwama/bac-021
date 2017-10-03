using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance_of_SuperHeroes {
    class Enhanced_Human : SuperHero
    {
        private int sumOfPowers = 0;
        private bool enhanced;
        private List<SuperPower> powerSet;

        public Enhanced_Human(string trueIdentity, string alterEgo, List<SuperPower> myPowers) : base(trueIdentity, alterEgo)
        {
            enhanced = false;
            foreach (var superPower in myPowers)
            {
                Console.WriteLine("thing in each superpowers is {0} and it is of type {1}",superPower,superPower.GetType());
                var thing = (int) superPower;
                sumOfPowers = sumOfPowers + thing;
            }
            enhanced = false;
            Console.WriteLine("As a result, sum of powers is {0}", sumOfPowers);
            powerSet = myPowers;

        }

        public override void SwitchIdentity()
        {
            if (enhanced == true)
            {
                enhanced = false;
            }
            else
            {
                enhanced = true;
            }
            base.SwitchIdentity();
        }

        public override bool HasPower(SuperPower whatPower)
        {
            if (enhanced)
            {
                return powerSet.Contains(whatPower);
            }
            return false;
        }

        public override int TotalPower()
        {
            if (enhanced)
            {
                return sumOfPowers;
            }
            else
            {
                return 0;
            }
        }
    }
}
