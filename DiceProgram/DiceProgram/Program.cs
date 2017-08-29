using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceRoller {
    /// <summary>
    /// Represents one die (singular of dice) with faces showing values between
    /// 1 and the number of faces on the die.
    /// 
    /// Modified from Java version in Lewis/Loftus "Java Software Solutions."
    /// by Mike Roggenkamp March 2009
    /// 
    /// Updated XML comments: MGR April 2016
    /// 
    /// </summary>
    public class Die {

        private const int SIX_SIDED = 6;
        private const int DEFAULT_FACE_VALUE = 1;
        private const int MIN_FACES = 3;
        /// <summary>
        /// 
        /// </summary>
        private int numFaces; //number of sides on die
        private int faceValue; // which side is showing
        private static Random randomNumber = new Random();

        public Die() {
            numFaces = SIX_SIDED;
            faceValue = DEFAULT_FACE_VALUE;
        }

        /// <summary>
        /// Allows user to specify the number of sides on a Die.
        /// If "faces" is less than 3, a six-sided die is instantiated. 
        /// </summary>
        /// <param name="faces"> the numberr of sides</param>
        public Die(int faces) {

            if (faces >= MIN_FACES) {
                numFaces = faces;
            } else {
                numFaces = SIX_SIDED;
            }

            RollDie();
        }

        /// <summary>
        /// Simulates the rolling of a Die.
        /// </summary>
        public void RollDie() {
            faceValue = randomNumber.Next(1, numFaces + 1);
        } // end RollDie

        /// <summary>
        ///  Die accessor
        /// </summary>
        /// <returns> The current face of the Die</returns>
        public int GetFaceValue() {
            return faceValue;
        } //end GetFaceValue


    }// end Class Die

    public class Dice {
        // Implement your 'Dice' class here
        public Dice(int dice) {
            int[] diearray = new int[dice];
            for (int z=0;z > temp;z++) {
                Console.WriteLine("hello123");
                return;
            }
        }
    }
}