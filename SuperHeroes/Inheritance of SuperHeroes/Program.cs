using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance_of_SuperHeroes {
    /// <summary>
    ///
    /// This class is based on the SuperHero class, HeroTester.java 
    /// created by Colin Fidge for INB370/CAB302 Software Development.
    ///
    /// It is a simple test driver for the subclasses of the SuperHero class
    /// 
    /// The subclassses are develop as a Workshop activity
    /// 
    /// Modifications: April 2016 Mike Roggenkamp
    /// 
    ///  Converted to C# and C# Coding Sytle Convention used in CAB201
    /// 
    /// </summary>
    class Program {


        static void Main(string[] args) {

 //           TestHumans();

           TestEnhancedHumans();

 //           TestSuperHumans();

            Console.Write("\n\n\nPress any key to exit ...");
            Console.ReadKey();

        }// end Main

        /*
         ************************************************************
         *                                                          *
         *  DO NOT UNCOMMENT THE METHODS BELOW UNTIL TOLD TO DO SO  *
         *  BY THE WORKSHEEET ACTIVITY                              *
         *                                                          *
         ************************************************************
         */


            /*   static void TestHumans() {

                    Human Batman = new Human("Bruce Wayne", "Batman");
                    Human BlackCanary = new Human(" Dinah Drake", "Black Canary");

                    Console.WriteLine("\n\nH1: In his normal identity Batman has no superpowers [Bruce Wayne, 0]:\n\n\t\t\t {0}, {1}\n\n", Batman.CurrentIdentity(),
                                                  Batman.TotalPower());

                    Batman.SwitchIdentity();

                    Console.WriteLine("\n\nH2: After switching identity still has no powers [Batman, 0]:\n\n\t\t\t {0}, {1}\n\n", Batman.CurrentIdentity(),
                                                  Batman.TotalPower());

                    Console.WriteLine("\n\nH3: Can Batman fly with his cape? [No]: \n\n\t\t\t {0} \n\n", (Batman.HasPower(SuperPower.Flight) ? "Yes" : "No"));

                    Console.WriteLine("\n\nH4: Batman's identity switch does not alter Black Canary's id [Dinah Drake]: \n\n\t\t\t {0}\n\n", BlackCanary.CurrentIdentity());

                    BlackCanary.SwitchIdentity();

                    Console.WriteLine("\n\nH6: Only Dinah Drake can chage her idenity [Black Canary]: \n\n\t\t\t {0}\n\n", BlackCanary.CurrentIdentity());

                    Console.WriteLine("\n\nH7: After switching identity Black Canary has no powers [Black Canary, 0]:\n\n\t\t\t {0}, {1}\n\n", BlackCanary.CurrentIdentity(),
                                          BlackCanary.TotalPower());

                }//end TestHumans */
        


                 static void TestEnhancedHumans() {

                    Enhanced_Human CaptainMarvel = new Enhanced_Human("Billy Batson", "Captain Marvel", new List<SuperPower> {  SuperPower.Flight,SuperPower.SuperStrength,
                                                                                               SuperPower.Invulnerability});
                    Enhanced_Human GreenLanteen = new Enhanced_Human("Hal Jordan", "Green Lanteen", new List<SuperPower> { SuperPower.Flight, SuperPower.SuperStrength,
                                                                                                SuperPower.SuperSpeed, SuperPower.Invulnerability});




                    Console.WriteLine("\n\n EH1: As a mortal Captain Marvel is meek and has no super powers [Billy Batson, 0]: \n\n\t\t\t {0}. {1}\n\n",
                                               CaptainMarvel.CurrentIdentity(), CaptainMarvel.TotalPower());

                    CaptainMarvel.SwitchIdentity();

                    Console.WriteLine("\n\n EH2: After Billy Batson says the magic word SHAZM he becomes who? [Captain Marvel, 325]: \n\n\t\t\t {0}, {1}\n\n",
                                        CaptainMarvel.CurrentIdentity(), CaptainMarvel.TotalPower());


                    Console.WriteLine("\n\n EH3: Does Captain Marvel have super strength? [Captain Marvel, Yes]: \n\n\t\t\t {0}, {1}\n\n",CaptainMarvel.CurrentIdentity(),
                                                                                                    (CaptainMarvel.HasPower(SuperPower.SuperStrength) ? "Yes" : "No"));


                    Console.WriteLine("\n\n EH4: Does Captain Marvel have X-ray vision? [Captain Marvel, No]: \n\n\t\t\t {0}, {1}\n\n", CaptainMarvel.CurrentIdentity(),  
                                                                                                    (CaptainMarvel.HasPower(SuperPower.XRayVision) ? "Yes" : "No"));


                    Console.WriteLine("\n\n EH5: Green Lanteen was born a a normal human. [Hal Jordan, 0]: \n\n\t\t\t {0}, {1}\n\n", GreenLanteen.CurrentIdentity(), GreenLanteen.TotalPower());

                    GreenLanteen.SwitchIdentity();

                    Console.WriteLine("\n\n EH6 But he can use his power ring to be a superhero. [GreenLantern, 365]: \n\n\t\t\t {0}, {1}\n\n", GreenLanteen.CurrentIdentity(),
                                                                           GreenLanteen.TotalPower());
                    GreenLanteen.SwitchIdentity();

                    Console.WriteLine("\n\n EH7: But unless he recharges his ring every day he changes back. [Hal Jordan, 0]: \n\n\t\t\t {0}, {1}\n\n", GreenLanteen.CurrentIdentity(),
                                                                           GreenLanteen.TotalPower());


                }//end TestEnhancedHumans 
        


  /*            static void TestSuperHumans() {

                    Super_Human WonderWoman = new Super_Human("Wonder Woman", "Diana Prince", new List<SuperPower> { SuperPower.SuperStrength, SuperPower.SuperIntellect });

                    Super_Human Superman = new Super_Human("Superman", "Clark Kent", new List<SuperPower> {SuperPower.Flight, SuperPower.SuperStrength, SuperPower.XRayVision,
                                                                                              SuperPower.SuperSpeed, SuperPower.Invulnerability});
                    Console.WriteLine("\n\n SH1: When Superman came to Earth he already had super powers[Superman, 385]: \n\n\t\t\t {0}, {1}\n\n", Superman.CurrentIdentity(), Superman.TotalPower());

                    Superman.SwitchIdentity();

                    Console.WriteLine("\n\n SH2: He retains his super powers even in his secret identity. [Clark Kent, 385]: \n\n\t\t\t {0}, {1}\n\n", Superman.CurrentIdentity(), Superman.TotalPower());

                    Superman.LoseAllSuperPowers();

                    Console.WriteLine("\n\n SH3: However when exposed to kryponite he loses all of his powers. [Clark Kent, 0]: \n\n\t\t\t {0}, {1}\n\n", Superman.CurrentIdentity(),Superman.TotalPower());

                    Console.WriteLine("\n\n SH4: But kryponite has no effect on Wonder Woman who has all of her powers. [Wonder  Women, 165]: \n\n\t\t\t {0}, {1}\n\n",WonderWoman.CurrentIdentity(),  WonderWoman.TotalPower());

                    WonderWoman.SwitchIdentity();

                    Console.WriteLine("\n\n SH5: when she switches to her plain identity she still has her \"Wisom of Athena\" [Diana Prince, Yes]: \n\n\t\t\t {0}, {1}\n\n", WonderWoman.CurrentIdentity(),
                                                                                (WonderWoman.HasPower(SuperPower.SuperIntellect) ? "Yes" : "No"));


                //   READ Activity 3 (c) - call to your method   for (c) goes here 

                //    Console.WriteLine("\n\n SH6: Fortuantely if the kryptonite is removed, Superman regains his original powers. [Clark Kent, 385]:\n\n\t\t\t {0}, {1}\n\n", Superman.CurrentIdentity(), Superman.TotalPower());

                }//end TestSuperHumans */
        


    }//end class
}//end namespace
