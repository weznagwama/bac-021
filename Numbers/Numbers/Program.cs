using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numbers {
    public class NumberList {
        List<float> ourList = new List<float>();
        float mini = 100;
        float maxi = 0;
        float totes;
        float temptotes;
        int listNum;
        float avg = 0;
        int countBeloh;
        int countAbuv;

        public void Add(float number) {
            ourList.Add(number);
        }

        public List<float> Numbers() {
            return ourList;
        }

        public float Minimum() {
            foreach (var thing in ourList) {
                if (thing < mini){
                    mini = thing;
                }
            }
            return mini;
        }
        public float Maximum() {
            foreach (var thing in ourList) {
                if (thing > maxi) {
                    maxi = thing;
                }
            }
            return maxi;
        }
        public float Sum() {
            foreach (var thing in ourList) {
                totes += thing;
            }
            return totes;
        }
        public float Average() {
            listNum = ourList.Count();
            foreach (var thing in ourList) {
                temptotes += thing;
            }
            avg = temptotes / listNum;
            return avg;
        }
        public int Count() {
            listNum = ourList.Count();
            return listNum;
        }
        public void DeleteBelow(float threshold) {
            for (int i = ourList.Count-1;i>=0;i--) {
                if (ourList[i] < threshold) {
                    ourList.RemoveAt(i);
                }
            }
        }
        public void DeleteAbove(float threshold) {
            for (int i = ourList.Count - 1; i >= 0; i--) {
                if (ourList[i] > threshold) {
                    ourList.RemoveAt(i);
                }
            }
        }
        public int CountBelow(float threshold) {
            foreach (var thing in ourList) {
                if (thing < threshold) {
                    countBeloh++;
                }
            }
            return countBeloh;
        }
        public int CountAbove(float threshold) {
            foreach (var thing in ourList) {
                if (thing > threshold) {
                    countAbuv++;
                }
            }
            return countAbuv;
        }
    }
    public class Program {
        private static string ListToString(List<float> numbers) {
            string text = "";
            for (int i = 0; i < numbers.Count; i++) {
                if (i > 0) text += ", ";
                text += String.Format("{0}", numbers[i]);
            }
            text += "";
            return text;
        }
        static void Main(string[] args) {
            NumberList myList = new NumberList();
            myList.Add(1);
            myList.Add(2);
            myList.Add(3);
            myList.Add(4);
            myList.Add(5);

            //Console.WriteLine("{0} should be 1", myList.Minimum()); //work
            //Console.WriteLine("{0} should be 5", myList.Maximum()); //work
            //Console.WriteLine("{0} should be 15", myList.Sum()); //work
            //Console.WriteLine("{0} should be 3", myList.Average()); //work
            Console.WriteLine("{0} should be 5", myList.Count()); // work
            //Console.WriteLine("{0} should be 2", myList.CountBelow(2.5f)); //work
            //Console.WriteLine("{0} should be 2", myList.CountAbove(3.5f)); //work

            //Console.WriteLine("{0} should be 1, 2, 3, 4, 5", ListToString(myList.Numbers())); //work, go the lot
            //myList.DeleteBelow(2.5f);
            //Console.WriteLine("{0} should be 3, 4, 5", ListToString(myList.Numbers())); //fail, expected

            myList.Add(6);
            myList.Add(7);

            Console.WriteLine("{0} should be 7", myList.Count()); // work
            //Console.WriteLine("{0} should be 3, 4", ListToString(myList.Numbers())); //fail, expected

            Console.WriteLine("\nPress enter to exit.");
            Console.ReadLine();
        }
    }
}
