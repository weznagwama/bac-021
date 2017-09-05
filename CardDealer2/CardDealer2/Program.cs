using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDealer {
    class Deck {

        // think a list can be used here instead
        private static string[] suits = new string[] { "Hearts", "Diamonds", "Clubs", "Spades" };
        private static string[] faces = new string[] { "Ace", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King" };

        // our 3 decks, main deck, a tempdeck and a 'pristine' deck
        private string[][] shuffledDeck = new string[52][];
        private string[][] tempDeck = new string[52][];
        private string[][] pristineDeck = new string[52][];

        // how many cards we have, starting from 0
        private const int cardNo = 51;

        // keeps track of random numbers we use, for shuffling
        private static int[] shuffNums = new int[52];

        // keeps track of how many cards we have dealt
        private int dealt = 0;

        static Random rng = new Random();

        public Deck() {
            for (int i = 0; i < shuffledDeck.Length; i++) {

                // our 3 decks
                shuffledDeck[i] = new string[2];
                tempDeck[i] = new string[2];
                pristineDeck[i] = new string[2];
            }
            int counter = 0;
            for (int z = 0; z <= suits.Length-1; z++) {
                for (int y = 0; y <= (faces.Length-1); y++) {

                    // does all our work
                    shuffledDeck[counter][0] = suits[z];
                    shuffledDeck[counter][1] = faces[y];

                    // users for reference
                    pristineDeck[counter][0] = suits[z];
                    pristineDeck[counter][1] = faces[y];

                    // keeps track across the jagged arrays
                    counter++;
                    
                }
            }
            counter = 0;
        }

        public static bool arrayContains(int[] shuffnums, int value) {
            foreach (var thing in shuffNums) {
                if (thing == value) {
                    return true;
                }
            }
            return false;
        }

        public void Shuffle() {
            dealt = 0;

            // resets the deck, having issues without this and I'm not understanding variable assignment for jagged arrays
            for (int i = 0;i < shuffledDeck.Length; i++) {
                shuffledDeck[i] = pristineDeck[i];
            }

            for (var i = shuffledDeck.Length-1; i > 0; i--) {
                var temp = shuffledDeck[i];
                var index = rng.Next(0, i + 1);
                shuffledDeck[i] = shuffledDeck[index];
                shuffledDeck[index] = temp;
            }
        }

        public string Deal() {
            if (dealt > 51) {
                string sorry = "Out of cards";
                return sorry;
            }
            string outpet = shuffledDeck[dealt][1] + " of " + shuffledDeck[dealt][0];
            dealt++;
            return outpet;
        }

        public void show() {
            for (int p = 0; p < shuffledDeck.Length; p++) {
                Console.WriteLine("cardno {0}, suit was {1} and face was {2}", p, shuffledDeck[p][0], shuffledDeck[p][1]);
            }
        }


        static void Main(string[] args) {
            Deck myDeck = new Deck();
            Console.WriteLine("Dealing all the card");
            for (int v = 0; v < 80; v++) {
                Console.WriteLine(myDeck.Deal());
            }
            Console.ReadLine();

        }

    }

}
