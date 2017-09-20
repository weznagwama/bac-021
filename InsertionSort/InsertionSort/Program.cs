using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertionSort {
    class Program {
        public static void InsertionSort(int[] array) {
            int sortedLength = 1;
            while (sortedLength < array.Length) { 

                int currentPos = sortedLength;
                int newItem = array[sortedLength];

                while (currentPos > 0 && array[currentPos-1] > newItem) {
                    int newpos = currentPos + 1;
                    array[currentPos] = array[currentPos - 1];
                    currentPos--;
                }
                array[currentPos] = newItem;
                sortedLength++;
            }

        }
        static void Main(string[] args) {
            int[] array = { 5, 1, 2, 8, 6, 3, 4 };
            InsertionSort(array);
            for (int i = 0; i < array.Length; i++) {
                if (i > 0) {
                    Console.Write(", ");
                }
                Console.Write("{0}", array[i]);
            }
            Console.WriteLine("\n\nPress enter to exit.");
            Console.ReadLine();
        }
    }
}
