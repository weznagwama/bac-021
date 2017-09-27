using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converters {
    public abstract class Converter {
        public abstract string From();

        public abstract string To();

        public abstract float Convert(float value);
    }

    public class MetresToCentimetres : Converter {
        string fromLen;
        string toUnit;

        public MetresToCentimetres() {
            //constructors
        }

        public override string From () {
            fromLen = "Metres";
            return fromLen;
        }

        public override string To() {
            //more constructors shit huehue
            toUnit = "Centimetres";
            return toUnit;
        }

        public override float Convert(float value) {
            float conversion = 100 * value;
            return conversion;
        }
    }

    public class CelsiusToFahrenheit : Converter {
        string fromLen;
        string toUnit;

        public CelsiusToFahrenheit() {
            //constructors
        }

        public override string From() {
            fromLen = "Celsius";
            return fromLen;
        }

        public override string To() {
            //more constructors shit huehue
            toUnit = "Fahrenheit";
            return toUnit;
        }

        public override float Convert(float value) {
            float conversion = value * 9/5+32;
            return conversion;
        }
    }

    public class MilesToKilometres : Converter {
        string fromLen;
        string toUnit;

        public MilesToKilometres() {
            //constructors
        }

        public override string From() {
            fromLen = "Miles";
            return fromLen;
        }

        public override string To() {
            //more constructors shit huehue
            toUnit = "Kilometres";
            return toUnit;
        }

        public override float Convert(float value) {
            double conversion = (value * 1.609344);
            float conversion2 = (float)conversion;
            return conversion2;
        }
    }

    public class DegreesToRadians : Converter {
        string fromLen;
        string toUnit;

        public DegreesToRadians() {
            //constructors
        }

        public override string From() {
            fromLen = "Degrees";
            return fromLen;
        }

        public override string To() {
            //more constructors shit huehue
            toUnit = "Radians";
            return toUnit;
        }

        public override float Convert(float value) {
            double conversion = value * Math.PI /180;
            float conversion2 = (float)conversion;
            return conversion2;
        }
    }


    class Program {
        static void Main(string[] args) {
            bool running = true;
            bool inputValid;
            int menuOption;
            float fromValue;

            List<Converter> converters = new List<Converter>();

            // Uncomment these to test out new classes as you implement them
            converters.Add(new MetresToCentimetres());
            converters.Add(new CelsiusToFahrenheit());
            converters.Add(new MilesToKilometres());
            converters.Add(new DegreesToRadians());

            while (running) {
                Console.WriteLine("\n\n  Conversion Menu");
                Console.WriteLine("-------------------");
                Console.WriteLine();
                for (int i = 0; i < converters.Count; i++) {
                    Console.WriteLine("{0}. {1} to {2}", i + 1, converters[i].From(), converters[i].To());
                }

                Console.WriteLine();

                do {
                    Console.Write("Choose a converter (or type 0 to exit): ");
                    inputValid = int.TryParse(Console.ReadLine(), out menuOption);
                    if (!inputValid || menuOption < 0 || menuOption > converters.Count) {
                        inputValid = false;
                        Console.WriteLine("Invalid option.");
                    }
                } while (!inputValid);

                if (menuOption == 0) {
                    running = false;
                } else {
                    Converter currentConverter = converters[menuOption - 1];
                    Console.WriteLine("Converting from {0} to {1}\n", currentConverter.From(), currentConverter.To());
                    do {
                        Console.Write("Enter input ({0}): ", currentConverter.From());


                        inputValid = float.TryParse(Console.ReadLine(), out fromValue);
                        if (!inputValid) {
                            Console.WriteLine("Invalid value.");
                        }
                    } while (!inputValid);

                    float result = currentConverter.Convert(fromValue);
                    Console.WriteLine("Result ({0}): {1}", currentConverter.To(), result);
                }

            }
        }
    }
}
