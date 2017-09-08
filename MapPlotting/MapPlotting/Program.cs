using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapPlotting {
    class Program {
        const int rows = 20;
        const int cols = 35;
        string[,] map = new string[rows, cols];

        public Program() {
            string[,] map = new string[rows, cols];
        }

        public void SetMap() {
            for (int row=0; row < rows; row++) {
                for (int col=0; col < cols; col++) {
                    map[row, col] = ".";
                }
            }
        }

        public int GetXCoord() {
            bool validInput = true;
            int xCoord;
            do {
                int input;
                Console.Write("Place a marker at which X coordinate? (0-34): ");
                validInput = int.TryParse(Console.ReadLine(), out input);
                if (input < 35) { 
                    if (!validInput) {
                        validInput = false;
                        Console.WriteLine("Invalid input");
                    }
                } else {
                    validInput = false;
                    Console.WriteLine("Out of range");
                }
                xCoord = input;
            } while (!validInput);
            return xCoord;
        }

        public int GetYCoord() {
            bool validInput = true;
            int yCoord;
            do {
                int input;
                Console.Write("Place a marker at which Y coordinate? (0-19): ");
                validInput = int.TryParse(Console.ReadLine(), out input);
                if (input < rows) {
                    if (!validInput) {
                        validInput = false;
                        Console.WriteLine("Invalid input");
                    }
                } else {
                    validInput = false;
                    Console.WriteLine("Out of range");
                }
                yCoord = input;
            } while (!validInput);
            return yCoord;
        }

        public bool continueMap() {
            bool validInput = false;
            do {
                string input;
                Console.Write("More? (y/n): ");
                input = Console.ReadLine();
                if (input == "n") {
                    return false;
                }
                if (input == "y") {
                        return true;
                    } else {
                        Console.WriteLine("Please answer with a y or a n.");
                        validInput = false;
                    }
            } while (!validInput);
            return false;
        }

        public void GetCoords() {
            bool cont = true;
            do {
                int finalx = GetXCoord();
                int finaly = GetYCoord();
                SetCoords(finalx, finaly);
                cont = continueMap();
            } while (cont);
        }

        public void SetCoords(int xcord, int ycord) {
            map[ycord, xcord] = "X";
        }

        public void PrintCoords() {
            for (int row = 0; row < rows; row++) {
                int counter = 0;
                for (int col = 0; col < cols; col++) {
                    if (counter == (cols-1)) {
                        Console.WriteLine(map[row,col]);
                        counter = 0;
                    } else {
                        Console.Write(map[row,col]);
                        counter++;
                    }
                    
                }
            }
        }


        public static void Main() {

            Program map = new Program();
            map.SetMap();
            map.GetCoords();
            map.PrintCoords();


            Console.WriteLine();
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    }
}