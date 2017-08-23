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
        public static int[] InitializeArrayWithNoDuplicates(int size) {
            int[] numarray = new int[size];

            for (int count = 0; count < size; count++) {
                int value = rng.Next(1, 45);

                bool dup = false;
                for (int z = 1; z < count; z++) {
                    if (numarray[count] == numarray[z]) {
                        Console.WriteLine("Shits the same");
                        dup = true;
                        break;
                    }
                }

                if(dup == false) {
                Console.WriteLine("Adding random value {0} to numarray", value);
                numarray[count] = value;
                Console.WriteLine("array index {0} value is {1}", count, numarray[count]);
                Console.WriteLine();
                 
                }
            }
            return numarray;
        }

        public void DisplayArray(int[] numbers) {
          int dayNumber = 0;
          foreach (int num in numbers) {
              Console.WriteLine("number is {0}", numbers[num]);
              dayNumber++;
         }
          return;
        }


        static void Main(string[] args) {
            int size = 5;
            InitializeArrayWithNoDuplicates(size);
           // DisplayArray(InitializeArrayWithNoDuplicates);
            Console.ReadLine();
        }
    }
}