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

                income = GetIncome();
                kids = GetKids();
                CalculateTax(kids, income);

                Console.WriteLine("\n\n Hit Enter to exit.");
                Console.ReadLine();
            }
            public static void CalculateTax(double kids, double income)
            {
                double tax = -10000;
                double childtax = kids * 2000;
                double amount = ((income + tax) - childtax);
                double payable = amount * 0.02;

                if (amount < 0)
                    {
                        Console.WriteLine("You owe no tax.");
                    }
                else
                    {
                        Console.WriteLine("You owe a total of {0:c0} tax.", payable);
                    }
            }
 

            public static double GetIncome()
            {

                string input;
                bool validate;
                double moneys;

            do
            {
                Console.Write("What is your total income: ");
                input = Console.ReadLine();
                validate = Double.TryParse(input, out moneys);

                    if (validate == false)
                    {
                        Console.WriteLine("Enter your income as a whole-dollar numeric figure.");
                    }
                    
                    if (moneys < 0)
                    {
                        Console.WriteLine("Your income cannot be negative.");
                        validate = false;
                    }
                } while (validate == false);
            return moneys;   
            }

            public static double GetKids()
            {

                string input;
                bool validate;
                double kids;

            do
            {
                Console.Write("How many children do you have: ");
                input = Console.ReadLine();
                validate = Double.TryParse(input, out kids);

                    if (validate == false)
                    {
                        Console.WriteLine("You must enter a valid number.");
                    }
                
                    if (kids < 0)
                    {
                        Console.WriteLine("You must enter a positive number.");
                        validate = false;
                    }
            } while (validate == false);
                return kids;
            }
        }
    }