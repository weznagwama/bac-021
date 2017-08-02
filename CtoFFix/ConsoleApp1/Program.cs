using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SimpleTempConversion
{
    /* Class to convert Celsius temperature to Fahrenheit
     * Author ....
     * Date: July 2015
     */
    class CelsiusToFahrenheit
    {
        public static void Main()
        {
            double celsius;
            double f;

            //input the temperature in degrees Celsius
            Console.Write("Enter degrees Celsius: ");
            celsius = Double.Parse(Console.ReadLine());
            f = ((celsius * 1.8) + 32);

            // Calculate degrees Fahrenheit and output the result
            Console.WriteLine("\n\nThe equivalent in Fahrenheit is " + f);

            Console.WriteLine("\n\n Hit Enter to exit.");
            Console.ReadLine();
        }
    }
}