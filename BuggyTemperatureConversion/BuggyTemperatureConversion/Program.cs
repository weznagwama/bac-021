using System;

namespace TemperatureConversion
{
    class Program
    {
        const string FAHRENHEIT = "Fahrenheit";
        const string CELSIUS = "Celsius";
        const int EXIT = 0;
        const int FAHRENHEIT_TO_CELSIUS = 1;
        const int NUM_OPTIONS = 3;

        public static void Main()
        {
            int menuOption;
            WelcomeMessage();   
           do
            {
                DisplayMenu();
                menuOption = ReadOption();
                PerformConversion(menuOption);
            } while (menuOption != EXIT);

            ExitProgram();
        } // end Main

        static void WelcomeMessage()
        {
            Console.WriteLine("\n\n\ttemperature Conversion\n\n");
        } // end WelcomeMessage

        static void ExitProgram()
        {
            Console.Write("\n\nPress enter to exit.");
            Console.ReadLine();
        } // end ExitProgram


        static void DisplayMenu()
        {
            string menu = "\n1) Convert Fahrenheit to Celsius"
                        + "\n2) Convert celsius to fahrenheit"
                        + "\n\nEnter your option(1-2 or 0 to exit): ";

            Console.Write(menu);
        } // end DisplayMenu


        static int ReadOption()
        {
            string choice;
            int option;
            bool okayChoice;

            do
            {
                choice = Console.ReadLine();
                okayChoice = int.TryParse(choice, out option);

                if (!okayChoice || option < 0 || option >= NUM_OPTIONS)
                {
                    okayChoice = false;
                    Console.WriteLine("You did not enter a correct option.\n\n Please try again");
                    DisplayMenu();
                }
                //else if (option == 0)
                //{
                 //   ExitProgram();

               // }
            } while (!(okayChoice));

            return option;
        } // end ReadOption


        static void PerformConversion(int option)
        {
            double initialTemp = 0, convertedTemp = 0;

            if (option == EXIT)
                return;

            initialTemp = ReadTemperaturefromConsole(option);

            if (option == FAHRENHEIT_TO_CELSIUS)
                convertedTemp = ConvertFahrenheitToCelsius(initialTemp);
            else
                convertedTemp = ConvertCelsiusToFahrenheit(initialTemp);

            DisplayConvertedTemperature(option, initialTemp, convertedTemp);
        } // end PerformConversion


        static double ReadTemperaturefromConsole(int option)
        {
            double initialTemperature;
            string temperatureType;
            bool temperatureOk;

            if (option == FAHRENHEIT_TO_CELSIUS)
                temperatureType = FAHRENHEIT;
            else
                temperatureType = CELSIUS;

            do
            {
                Console.Write("\nPlease enter intial temperature in {0} : ", temperatureType);
                temperatureOk = double.TryParse(Console.ReadLine(), out initialTemperature);
                if (!(temperatureOk))
                    Console.WriteLine("Please enter a number.");
            } while (!(temperatureOk));

            return initialTemperature;
        } // end ReadTemperaturefromConsole


        static double ConvertFahrenheitToCelsius(double initialTemperature)
        {
            return (initialTemperature - 32.0)* 5/9;
        } // end ConvertFahrenheitToCelsius


        static double ConvertCelsiusToFahrenheit(double initialTemperature)
        {
            return ((9.0 / 5.0) * initialTemperature + 32.0);
        } // end ConvertCelsiusToFahrenheit


        static void DisplayConvertedTemperature(int option,
           double initialTemperature, double convertedTemperature)
        {
            string initialType = "", convertedType = "";

            if (option == FAHRENHEIT_TO_CELSIUS)
            {
                initialType = FAHRENHEIT;
                convertedType = CELSIUS;
            }
            else
            {
                initialType = CELSIUS;
                convertedType = FAHRENHEIT;
            }

            Console.WriteLine("\n\n\t{0:F3} degrees {1} = {2:F3} degrees {3}",
                initialTemperature, initialType, convertedTemperature, convertedType);

        } //end DisplayConvertedTemperature


    }//end class
}//end namespace