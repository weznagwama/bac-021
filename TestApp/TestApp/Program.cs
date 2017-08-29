using System;

namespace RandomArray {
    public class RandomArrayNoDuplicates {
        static Random rng = new Random();
        /// <summary>
        /// Creates an array with each element a unique integer
        /// between 1 and 45 inclusively.
        /// </summary>
        /// <param name="size"> length of the returned array < 45
        /// </param>
        /// <returns>an array of length "size" and each element is
        /// a unique integer between 1 and 45 inclusive </returns>
        public static bool arrayContains(int[] array, int num) {
            foreach (var thing in array) {
                if (thing == num) {
                    return true;
                }
            }
            return false;
        }

        public static int getRandNotIn(int[] numarray) {
            int value = 0;
            do {
                value = rng.Next(1, 46);

            } while (arrayContains(numarray, value));
            return value;
        }

        public static bool sizeWrong(int size) {
            if (size <= 0 || size >= 46) {
                return true;
            }
            return false;
        }

        public static int[] InitializeArrayWithNoDuplicates(int size) {
            bool butwhowas;
            butwhowas = sizeWrong(size);
            while (butwhowas) {
                //Console.WriteLine("Size incorrect!");
                size = int.Parse(Console.ReadLine());
                butwhowas = sizeWrong(size);
            }
            int[] numarray = new int[size];
            for (int count = 0; count < size; count++) {
                var ourvalue = getRandNotIn(numarray);
                numarray[count] = ourvalue;
            }
            return numarray;
        }


        public static int DisplayArray(int[] numberArray) {
            int thingNumber = 0;
            foreach (int num in numberArray) {
                Console.WriteLine("{0}", numberArray[thingNumber]);
                thingNumber++;
            }
            return thingNumber;
        }


        static void Main(string[] args) {
            int size = 5;
            DisplayArray(InitializeArrayWithNoDuplicates(size));
            Console.ReadLine();
        }
    }
}