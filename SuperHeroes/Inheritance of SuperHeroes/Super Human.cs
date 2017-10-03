using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance_of_SuperHeroes {
    class Super_Human : SuperHero
    {

        private int sumOfPowers;
        //private bool enhanced = true;
        private List<SuperPower> powerSet;

        public Super_Human(string trueIdentity, string alterEgo, List<SuperPower> myPowers) : base(trueIdentity, alterEgo)
        {
            foreach (var superPower in myPowers) {
                Console.WriteLine("thing in each superpowers is {0} and it is of type {1}", superPower, superPower.GetType());
                var thing = (int)superPower;
                sumOfPowers = sumOfPowers + thing;
            }
          //  enhanced = false;
            Console.WriteLine("As a result, sum of powers is {0}", sumOfPowers);
            powerSet = myPowers;
        }

        public void AddSuperPower(SuperPower power)
        {
            if (!this.HasPower(power))
            {
                powerSet.Add(power);
                sumOfPowers = 0;
                foreach (var superPower in powerSet) {
                    var thing = (int)superPower;
                    sumOfPowers = sumOfPowers + thing;
                }
            }
            
        }

        public void LoseAllSuperPowers()
        {
            powerSet.Clear();
            sumOfPowers = 0;
            foreach (var superPower in powerSet) {
                var thing = (int)superPower;
                sumOfPowers = sumOfPowers + thing;
            }
        }

        public void LoseSinglePower(SuperPower power)
        {
            powerSet.Remove(power);
            sumOfPowers = 0;
            foreach (var superPower in powerSet) {
                var thing = (int)superPower;
                sumOfPowers = sumOfPowers + thing;
            }
        }

        public override bool HasPower(SuperPower whatPower)
        {
            return powerSet.Contains(whatPower);
        }

        public override int TotalPower()
        {
            //if (enhanced)
           // {
                return sumOfPowers;
            //}
           // else
            //{
          //      return 0;
            //}
        }
    }
}
