using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance_of_SuperHeroes {

    /// <summary>
    /// 
    /// This enumeration lists various super powers commonly available to comic book superheroes
    /// and associates a (subjective) value with each superpower.
    /// This list of powers is by no means comprehensive.
    /// 
    /// Adapted from a Java enumeration, SuperPower.java created by Colin Fidge 
    /// for INB370/CAB302 Software Development.
    /// 
    /// Modified to C# April 2016. Mike Roggenkamp
    /// 
    /// </summary>
    public enum SuperPower {
        Flight = 100,
        SuperStrength = 75,
        XRayVision = 20,
        SuperSpeed = 40,
        Invulnerability =150,
        SuperIntellect = 90
    }

    /// <summary>
    /// A SuperHero has two alternative identities
    /// and may have various super powers or none at all.
    /// 
    /// This class is based on the SuperHero class, SuperHero.java written by Colin Fidge
    /// for INB370/CAB302 Software Development.
    /// 
    /// Modifications: April 2016 Mike Roggenkamp
    /// 
    ///  Converted to C# and C# Coding Sytle Convention used in CAB201
    /// 
    /// </summary>
    public abstract class SuperHero {

        private string currentIdentity;
        private string otherIdentity;
       

        public SuperHero(string trueIdentity, string alterEgo) {
            currentIdentity = trueIdentity;
            otherIdentity = alterEgo;
        }


        public string CurrentIdentity(){
            return currentIdentity;
        }//end CurrentIdentity


        public int GetPowerValue(SuperPower power) {
            return (int) power;
        }//end GetPowerValue

        public virtual void SwitchIdentity()
        {

            var tempIdentity = currentIdentity;
            currentIdentity = otherIdentity;
            otherIdentity = tempIdentity;

        }//end SwitchIdentity

        public abstract bool HasPower(SuperPower whatPower);

        public abstract int TotalPower();

        
    }//end class
}
