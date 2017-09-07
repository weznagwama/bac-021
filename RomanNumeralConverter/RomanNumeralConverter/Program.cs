using System;

namespace RomanNumerals
{
    class RomanNumeralConverter
    {
        public static void Main()
        {
            // Write your program to convert numbers into Roman numerals here
            int input;
            Console.Write("Please enter a number (1-10): ");
            input = int.Parse(Console.ReadLine());
            if (input == 1)
            {
                Console.WriteLine("{0} when written in Roman numerals is I", input);
            }
            else if (input == 2)
            {
                Console.WriteLine("{0} when written in Roman numerals is II",input);
            }
            else if (input == 3)
            {
                Console.WriteLine("{0} when written in Roman numerals is III", input);
            }
            else if (input == 4)
            {
                Console.WriteLine("{0} when written in Roman numerals is IV", input);
            }
            else if (input == 5)
            {
                Console.WriteLine("{0} when written in Roman numerals is V", input);
            }
            else if (input == 6)
            {
                Console.WriteLine("{0} when written in Roman numerals is VI", input);
            }
            else if (input == 7)
            {
                Console.WriteLine("{0} when written in Roman numerals is VII", input);
            }
            else if (input == 8)
            {
                Console.WriteLine("{0} when written in Roman numerals is VIII", input);
            }
            else if (input == 9)
            {
                Console.WriteLine("{0} when written in Roman numerals is IX", input);
            }
            else if (input == 10)
            {
                Console.WriteLine("{0} when written in Roman numerals is X", input);
            }
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    }
}