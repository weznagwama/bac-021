using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGradeOrder {
    class Student : IComparable
    {
        private string firstName;
        private string lastName;
        private string degree;
        private int grade;
        public Student(string firstName, string lastName, string degree, int grade)
        {
            firstName = this.firstName;
            lastName = this.lastName;
            degree = this.degree;
            grade = this.grade;
        }

        public override string ToString()
        {
            return String.Format("{0}, {1} ({2}) Grade: {3}",lastName, firstName,degree,grade);
        }

        public int CompareTo(object obj)
        {
            Student objTemp = (Student) obj;

            if (this > objTemp)
            {
                return 1;
            }

            if (this == (Student) obj)
            {
                return 0;
            }

            if (this < (Student) obj)
            {
                return -1;
            }
            return 0;
        }

    }
    class Program {
        static void Main(string[] args) {
            Student[] students = new Student[]
            {
                new Student("Jane", "Smith", "Bachelor of Engineering", 6),
                new Student("John", "Smith", "Bachelor of Engineering", 7),
                new Student("John", "Smith", "Bachelor of IT", 7),
                new Student("John", "Smith", "Bachelor of IT", 6),
                new Student("Jane", "Smith", "Bachelor of IT", 6),
                new Student("John", "Bloggs", "Bachelor of IT", 6),
                new Student("John", "Bloggs", "Bachelor of Engineering", 6),
                new Student("John", "Bloggs", "Bachelor of IT", 7),
                new Student("John", "Smith", "Bachelor of Engineering", 6),
                new Student("Jane", "Smith", "Bachelor of Engineering", 7),
                new Student("Jane", "Bloggs", "Bachelor of IT", 6),
                new Student("Jane", "Bloggs", "Bachelor of Engineering", 6),
                new Student("Jane", "Bloggs", "Bachelor of Engineering", 7),
                new Student("Jane", "Smith", "Bachelor of IT", 7),
                new Student("John", "Bloggs", "Bachelor of Engineering", 7),
                new Student("Jane", "Bloggs", "Bachelor of IT", 7),
            };

            Array.Sort(students);
            foreach (Student student in students) {
                Console.WriteLine("{0}", student);
            }

            Console.WriteLine("\nPress enter to exit.");
            Console.ReadLine();
        }
    }
}
