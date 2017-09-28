using System;
using System.Collections.Generic;
using System.Drawing;

namespace TankBattle
{
    public class Battle
    {
        private static Battle game;
        private static int numplayers;
        private static int numRounds;
        private static GenericPlayer[] playerArray;
        private static Terrain newTerrain;
        private static ControlledTank[] controlledTankArray;
        private static int windSpeed;

        private static int currentRound;
        private static int currentPlayer;

        static Random rng = new Random();

        public Battle(int numPlayers, int numRounds)
        {
            Battle.playerArray = new GenericPlayer[numPlayers];
            int[] playerArray = new int[numPlayers];
            List<string> attackEffect = new List<string>();

            Battle.numRounds = numRounds;
            Battle.numplayers = numPlayers;
        }

        public int PlayerCount()
        {
            return numplayers;
        }

        public int GetRound()
        {
            //unsure yet
            return numRounds;
        }

        public int GetRounds()
        {
            return numRounds;
        }

        public void SetPlayer(int playerNum, GenericPlayer player)
        {
            playerArray[playerNum-1] = player;
        }

        public GenericPlayer GetPlayerNumber(int playerNum)
        {
            return playerArray[playerNum-1];
        }

        public ControlledTank GetPlayerTank(int playerNum)
        {
            throw new NotImplementedException();
        }

        public static Color PlayerColour(int playerNum)
        {
            Color c = Color.FromArgb(playerNum);
            return c;
        }

        public static int[] CalculatePlayerPositions(int numPlayers)
        {
            // need to verify this one
            int counter = 0;
            int[] temp = new int[numPlayers];
            int tempWidth = Terrain.WIDTH / numPlayers;
            for (int i = 0; i < numPlayers; i++)
            {
                if (counter == 0)
                {
                    temp[i] = 0;
                    counter = counter + tempWidth;
                }
                else
                {
                    temp[i] = counter;
                    counter = counter + tempWidth;
                }
            }
            return temp;
        }  

        public static void Shuffle(int[] array)
        {
            for (var i = array.Length - 1; i > 0; i--) {
                var temp = array[i];
                var index = rng.Next(0, i + 1);
                array[i] = array[index];
                array[index] = temp;
            }
        }

        public void NewGame()
        {
            currentRound = 1;
            currentPlayer = 0;
            CommenceRound();
        }

        public void CommenceRound()
        {
            //Initialising a private field of Battle representing the current player to the value of the starting GenericPlayer field(see NewGame).
            // ??? isn't this currentPlayer?

            //Creating a new Terrain, which is also stored as a private field of Battle.
            Battle.newTerrain =  new Terrain();
            //Creating an array of GenericPlayer positions by calling CalculatePlayerPositions with the number of GenericPlayers playing the game(hint: get the length of the GenericPlayers array)
            int[] calcedArray = CalculatePlayerPositions(playerArray.Length);
            //Looping through each GenericPlayer and calling its BeginRound method.
            foreach (var player in playerArray)
            {
                player.BeginRound();
            }
            //Shuffling that array of positions with the Shuffle method.
            Shuffle(calcedArray);
            //Creating an array of ControlledTank as a private field.There should be the same number of ControlledTanks as there are GenericPlayers in the GenericPlayer array.
            controlledTankArray  = new ControlledTank[playerArray.Length - 1];

            //Initialising the array of ControlledTank by finding the horizontal position of the ControlledTank(by looking up the appropriate index of the array returned 
            //by CalculatePlayerPositions and shuffled with the Shuffle method), the vertical position of the ControlledTank(by calling TankVerticalPosition() on the Terrain 
            //with the horizontal position as an argument), and then calling ControlledTank's constructor to create that ControlledTank (passing in the appropriate GenericPlayer, 
            //the horizontal position, the vertical position and a reference to this).

            //Battle.controlledTankArray[]

            //Initialising the wind speed, another private field of Battle, to a random number between -100 and 100.
            windSpeed = rng.Next(-101, 100);

            //Creating a new GameplayForm and Show()ing it.
            //GameplayForm gPlayForm = new GameplayForm();
            //gPlayForm.Show();
        }

        public Terrain GetBattlefield()
        {
            throw new NotImplementedException();
        }

        public void DisplayPlayerTanks(Graphics graphics, Size displaySize)
        {
            foreach (var player in playerArray)
            {
                //do stuff perhapsf
            }
        }

        public ControlledTank CurrentPlayerTank()
        {
            return controlledTankArray[currentPlayer];
        }

        public void AddEffect(AttackEffect weaponEffect)
        {
            throw new NotImplementedException();
        }

        public bool WeaponEffectStep()
        {
            throw new NotImplementedException();
        }

        public void DrawWeaponEffects(Graphics graphics, Size displaySize)
        {
            throw new NotImplementedException();
        }

        public void EndEffect(AttackEffect weaponEffect)
        {
            throw new NotImplementedException();
        }

        public bool CheckCollidedTank(float projectileX, float projectileY)
        {
            throw new NotImplementedException();
        }

        public void DamageArmour(float damageX, float damageY, float explosionDamage, float radius)
        {
            throw new NotImplementedException();
        }

        public bool CalculateGravity()
        {
            throw new NotImplementedException();
        }

        public bool TurnOver()
        {
            throw new NotImplementedException();
        }

        public void CheckWinner()
        {
            throw new NotImplementedException();
        }

        public void NextRound()
        {
            currentRound++;

        }
        
        public int Wind()
        {
            return windSpeed;
        }
    }
}
