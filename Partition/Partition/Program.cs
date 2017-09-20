using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partition {
    class Program {
        public static int Partition(int[] array, int first, int last) {
            {
                int left = first;
                int right = last;
                int pivot = array[left];

                while (true) {
                    while (array[left] < pivot)
                        left++;

                    while (array[right] > pivot)
                        right--;

                    if (left < right) {
                        int temp = array[right];
                        array[right] = array[left];
                        array[left] = temp;
                    } else {
                        return System.Array.IndexOf(array, pivot);
                    }
                }
            }
        }
        
        public static void PrintArray(int[] array) {
            for (int i = 0; i < array.Length; i++) {
                if (i > 0) {
                    Console.Write(", ");
                }
                Console.Write("{0}", array[i]);
            }
            Console.WriteLine();
        }

        public static void QuickSort(int[] array, int first, int last) {
            int pivot;

            if (first < last) {
                pivot = Partition(array, first, last);

                if (pivot > 1) { 
                    QuickSort(array, first, last - 1);
                }
                if (pivot + 1 < last) { 
                    QuickSort(array, pivot + 1, last);
                }
            }
        }
        static void Main(string[] args) {
            int[] array = new int[10];
            Random rng = new Random();
            for (int i = 0; i < array.Length; i++) {
                array[i] = rng.Next(0, 100);
            }
            Console.WriteLine("Pivot no is {0}", array[0]);
            Console.WriteLine();
            Console.WriteLine("Unsorted array:");
            PrintArray(array);

            Console.WriteLine("Partitioned array:");
            Partition(array, 0, array.Length - 1);
            PrintArray(array);

            Console.WriteLine("Sorted array:");
            QuickSort(array, 0, array.Length - 1);
            PrintArray(array);

            Console.WriteLine("\nPress enter to exit.");
            Console.ReadLine();
        }
    }
}

