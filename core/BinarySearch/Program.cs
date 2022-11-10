// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


internal class BinarySearch
{
    static void Main(string[] args)
    {
        int[] sortedPrimesArray =
        {
            3,
            5,
            7,
            11,
            13,
            17,
            19,
            23,
            29,
            31,
            37,
            41,
            43,
            47,
            53,
            59,
            61,
            67,
            71,
            73,
            79,
            83,
            89,
            97
        };
        Console.WriteLine("What prime number do you want to find?");
        string guess = Console.ReadLine();
        SearchArray(sortedPrimesArray, Int32.Parse(guess));
    }

    public static int SearchArray(int[] array, int guess)
    {
        // Round the index we want to search to
        int minGuess = 0;
        int maxGuess = array.Length - 1;
        int betweenMinMax;
        //Console.WriteLine($"Guess range start is {minGuess}, Guess range end is {maxGuess}");

        

        if (maxGuess < minGuess){
            Console.WriteLine("Number don't exist!");
            return -1;
        }

        while (minGuess <= maxGuess)
        {
            betweenMinMax = (maxGuess + minGuess) / 2;
            //Console.WriteLine($"guessIndex is {betweenMinMax}");

            if (array[betweenMinMax] == guess)
            {
                Console.WriteLine($"Guess found! {guess} is at index {betweenMinMax}");
                return guess;
            }
            //Halfway index number is lower than our guess, so recalculate max to guess - 1
            else if (array[betweenMinMax] < guess)
            {
                minGuess = betweenMinMax + 1;
            }
            // Halfway index number is higher than our guess, so shift  the min to guess + 1
            else if (array[betweenMinMax] > guess)
            {
                maxGuess = betweenMinMax - 1;
            }
            else {
                Console.WriteLine($"{guess} is it! times 2");
                return guess+guess;
            }
        }
        // Max is less than min, so exit
        Console.WriteLine($"we reached the enbd");
        return -1;
    }
}
