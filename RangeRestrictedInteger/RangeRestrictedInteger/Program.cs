using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeRestrictedInteger {
    public class RangedInteger {
        private int theNum = 0;
        private int mini;
        private int maxi;
        Random rng = new Random();

        public RangedInteger(int min, int max) {
            mini = min;
            maxi = max;
            theNum = rng.Next(min, max + 1);
        }

        public int Value {
            get {
                return theNum;
            }
            set {
                theNum = value;
                if (theNum <= mini) {
                    theNum = mini;
                }
                else if (theNum >= maxi) {
                    theNum = maxi;
                } else {
                    theNum = value;
                }
               
            }
        }
    }
    class Program {
        static void Main(string[] args) {
            RangedInteger myInteger = new RangedInteger(0, 100);
            RangedInteger myInteger2 = new RangedInteger(-87, 1);
            RangedInteger myInteger3 = new RangedInteger(-46, 35);
            RangedInteger myInteger4 = new RangedInteger(-46, 35);

            myInteger.Value = 57;
            Console.WriteLine("{0}", myInteger.Value); // Should be 57
            myInteger.Value = 103;
            Console.WriteLine("{0}", myInteger.Value); // Should be 100
            myInteger.Value = -4;
            Console.WriteLine("{0}", myInteger.Value); // Should be 0

            Console.WriteLine("{0}", myInteger2.Value); // Should be -69?
            Console.WriteLine("{0}", myInteger3.Value); // Should be -46
            Console.WriteLine("{0}", myInteger4.Value); // Should be 35

            Console.WriteLine("\nPress enter to exit.");
            Console.ReadLine();
        }
    }
}
