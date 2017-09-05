using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall {
    class MonthlyRainfall {
        public static void Main() {

            string[] MonthsOfYear = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
            double[] MonthlyAmount = new double[MonthsOfYear.Length];
            double[] results = new double[MonthsOfYear.Length];

            results = GetRainfall(MonthsOfYear, MonthlyAmount);

            HighestRainfall(MonthsOfYear, results);
            TotalRainFall(results);

            ExitProgram();
        }


        static double[] GetRainfall(string[] MonthList, double[]MonthlyAmount) {
            double[] AmountList = new double[MonthlyAmount.Length];
            for (int z = 0; z < (MonthList.Length); z++) {
                bool validInput;
                do {
                    double input;
                    Console.Write("What is the monthly rainfall amount for {0} (in millimetres): ", MonthList[z]);
                    validInput = double.TryParse(Console.ReadLine(), out input);
                    if (!validInput || input < 0) {
                        validInput = false;
                        Console.WriteLine("Rainfall amount must be a positive number");
                    }
                    AmountList[z] = input;
                } while (!validInput); 
            }
            AmountList = RoundArray(AmountList);
            return AmountList;
        }

        static double[] RoundArray(double[] MonthlyRain) {
            double[] RoundedAmount = new double[MonthlyRain.Length];
            for (int z = 0;z < (MonthlyRain.Length); z++) {
                RoundedAmount[z] = Math.Round(MonthlyRain[z], 2);
                
            }
            return RoundedAmount;
        }

        static double HighestRainfall(string[] MonthList, double[] MonthlyRain) {
            double highestAmount = 0;
            string highestMonth = "";
            for (int z = 0;z < (MonthlyRain.Length); z++) {
                if (MonthlyRain[z] > highestAmount) {
                    highestAmount = MonthlyRain[z];
                    highestMonth = MonthList[z];
                }
            }
            Console.WriteLine("The month with the greatest rainfall was {0} with {1}mm", highestMonth, highestAmount);
            return highestAmount;
        }// end DisplayDailyAverage


        static double TotalRainFall(double[] MonthlyRain) {
            double totes = 0;
            for (int z = 0; z < (MonthlyRain.Length); z++) {
                totes = totes + MonthlyRain[z]; 
            }
            Console.WriteLine("The total rainfall for the year was {0}mm", totes);
            return totes;
        }//end CalculateAverageByMeal

        static void DisplayArray(double[] arrayinput) {
            foreach (double number in arrayinput) {
                Console.WriteLine("The amount is {0}", number);
            }
        }

        static void ExitProgram() {
            Console.Write("Press enter to exit ...");
            Console.ReadLine();
        }//end ExitProgram



    }//end class
}