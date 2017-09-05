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
    public class Dice {

        private const int SIX_SIDED = 6;
        private const int DEFAULT_FACE_VALUE = 1;
        private const int MIN_FACES = 3;
        
        /// <summary>
        /// 
        /// </summary>
        private int numFaces; //number of sides on die
        private int dieno;
        private int faceValue; // which side is showing
        private static Random randomNumber = new Random();


        public Dice(int dice) {
            numFaces = SIX_SIDED;
            faceValue = DEFAULT_FACE_VALUE;
        }

        /// <summary>
        /// Allows user to specify the number of sides on a Die.
        /// If "faces" is less than 3, a six-sided die is instantiated. 
        /// </summary>
        /// <param name="faces"> the numberr of sides</param>
        public Dice(int dice, int faces) {

            if (faces >= MIN_FACES) {
                numFaces = faces;
            } else {
                numFaces = SIX_SIDED;
            }
            dieno = dice;
           
        }

        /// <summary>
        /// Simulates the rolling of a Die.
        /// </summary>
        public void RollDice() {
            faceValue = randomNumber.Next(1, (numFaces*dieno) + 1);
        } // end RollDie

        /// <summary>
        ///  Die accessor
        /// </summary>
        /// <returns> The current face of the Die</returns>
        public int GetFaceValue() {
            Console.WriteLine("your face value is {0}", faceValue);
            return faceValue;
        } //end GetFaceValue

        static void Main(string[] args) {

            Dice mydice = new Dice(2,4);
            Console.WriteLine("dice number is {0} and sides is {1}",mydice.dieno, mydice.numFaces);
            mydice.RollDice();
            mydice.GetFaceValue();
            Console.ReadLine();
        }

        }// end Class Die

    
}
