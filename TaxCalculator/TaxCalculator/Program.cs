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
                int income;
                int kids;

                income = GetIncome();
                kids = GetKids();
                CalculateTax(kids, income);

                Console.WriteLine("\n\n Hit Enter to exit.");
                Console.ReadLine();
            }
            public static void CalculateTax(int kids, int income)
            {
                int tax = -10000;
                int childtax = kids * 2000;
                int amount = ((income + tax) - childtax);
                double payable = amount * 0.02;

                if (amount <= 0)
                    {
                        Console.WriteLine("You owe no tax.");
                    }
                else
                    {
                        Console.WriteLine("You owe a total of {0:c0} tax.", payable);
                    }
            }
 

            public static int GetIncome()
            {

                string input;
                bool validate;
                int moneys;

            do
            {
                Console.Write("What is your total income: ");
                input = Console.ReadLine();
                validate = int.TryParse(input, out moneys);

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

            public static int GetKids()
            {

                string input;
                bool validate;
                int kids;

            do
            {
                Console.Write("How many children do you have: ");
                input = Console.ReadLine();
                validate = int.TryParse(input, out kids);

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