using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enrolment {
    public class EnrolmentManager {
        private List<string>[] classes;
        private int maxClassSize;

        public EnrolmentManager(int numClasses, int placesPerClass) {
            if (numClasses <= 0 || placesPerClass <= 0) {
                throw new System.ArgumentOutOfRangeException();
            }
            classes = new List<string>[numClasses];

            for (int classNum = 0; classNum < numClasses; classNum++) {
                classes[classNum] = new List<string>(placesPerClass);
            }
            maxClassSize = placesPerClass;
        }

        public void EnrolStudent(string studentName, int classNum) {


            if (classNum > classes.Length-1 || classNum < 0) {
                Console.WriteLine("invalid class");
                throw new System.ArgumentOutOfRangeException();
            }

            foreach (var thing in classes)
            {
                if (thing.Count==maxClassSize)
                {
                    //Console.WriteLine("Current count of class is {0}", classes[classNum]);
                    throw new System.ArgumentException();    
                }

            }

            for (int i = 0; i == maxClassSize - 1; i++)
            {
                Console.WriteLine("Sub list class no {0} is {1}",i,classes[i]);
            }
            //Console.Write("fin enrol, we are going to add {0} to class {1}",studentName,classes[classNum]);
            classes[classNum].Add(studentName);
           // Console.WriteLine("What is this {0}",classes[classNum][0]);
        }

        public int GetNumClasses() {
            return classes.Length;
        }

        public int GetClassSize() {
            return maxClassSize;
        }

        public int GetNumEnrolments(int classNum) {
            if (classNum > classes.Length-1 || classNum < 0) {
                throw new System.ArgumentOutOfRangeException();
            }
            return classes[classNum].Count;
        }

        public string GetStudent(int classNum, int studentNum) {
            if (classNum > classes.Length-1 || classNum < 0) {
                throw new System.ArgumentOutOfRangeException();
            }

            if (studentNum < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            if (classes[classNum].Count < studentNum)
            {
                throw new System.ArgumentException();
            }
            return classes[classNum][studentNum];
        }

        public void ListStudents(int classNum) {
            if (classNum > classes.Length-1 || classNum < 0) {
                throw new System.ArgumentOutOfRangeException();
            }
            Console.WriteLine("Students in class {0}:", classNum);
            foreach (string studentName in classes[classNum]) {
                Console.WriteLine("- {0}", studentName);
            }
            Console.WriteLine("Total of {0} students.",
                classes[classNum].Count);
        }

        public int GetFreeClass() {
            for (int classNum = 0; classNum < classes.Length; classNum++) {
                if (classes[classNum].Count < maxClassSize) {
                    return classNum;
                }
            }
            return -1;
        }
        static void Main(string[] args) {
            EnrolmentManager test = new EnrolmentManager(3,2);
            Console.WriteLine("Enrolling student");
            test.EnrolStudent("Tristan",1);
            test.EnrolStudent("Tristan2", 1);
            test.EnrolStudent("Tristan3", 1);
            test.EnrolStudent("Tristan4", 1);
            test.GetNumEnrolments(0);
            Console.WriteLine("Hit enter to exit");
            Console.ReadLine();
        }
    }
}
