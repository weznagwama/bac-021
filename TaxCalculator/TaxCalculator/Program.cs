using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IncomeTaxCalculator
{
    class IncomeTax
    {
        public static void Main()
        {
            double income;
            double kids;
            double taxowed;

            income = GetIncome();
            kids = GetKids();
            taxowed = CalculateTax(kids, income);

            Console.WriteLine("\n\n Hit Enter to exit.");
            Console.ReadLine();
        }
        public static double CalculateTax(double kids, double income)
        {
            double tax = -10000;
            double childtax = kids * 2000;
            double amount = ((income + tax) - childtax);
            double payable = amount * 0.02;

            if (amount < 0)
                Console.WriteLine("You owe no tax.");
            else
            {
                Console.WriteLine("You owe a total of {0:c0} tax.", payable);
            }

            return amount;
        }
        public static double GetIncome()
        {

        string input;
        bool validate;
        double negative;
        double moneys;

        Start:

        Console.Write("What is your total income: ");
        input = Console.ReadLine();
        validate = Double.TryParse(input, out moneys);
        if (validate == false)
        {
            Console.WriteLine("Enter your income as a whole-dollar numeric figure.");
            goto Start;
        }
        negative = Double.Parse(input);

        if (negative < 0)
        {
            Console.WriteLine("Your income cannot be negative.");
            goto Start;
        }

        return negative;   
        }

        public static double GetKids()
        {

            string input;
            bool validate;
            double negative;
            double kids;

        Start:

            Console.Write("How many children do you have: ");
            input = Console.ReadLine();
            validate = Double.TryParse(input, out kids);
            if (validate == false)
            {
                Console.WriteLine("You must enter a valid number.");
                goto Start;
            }
            negative = Double.Parse(input);

            if (negative < 0)
            {
                Console.WriteLine("You must enter a positive number.");
                goto Start;
            }

            return negative;
        }


    }
}