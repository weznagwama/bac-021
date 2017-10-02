using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace TankBattle
{
    public class Battle
    {
        private static Battle game;
        private static int numplayers;
        private static int numRounds;
        private static GenericPlayer[] playerArray;
        private static ControlledTank[] controlledTankArray;
        private static AttackEffect[] attackEffect;

        private static Terrain newTerrain;
        private static int windSpeed;

        private static int currentRound;
        private static int startingPlayer;
        private static int currentPlayer;

        static Random rng = new Random();

        public Battle(int numPlayers, int numRounds)
        {
            Battle.playerArray = new GenericPlayer[numPlayers];
            int[] playerArray = new int[numPlayers];
            AttackEffect[] attackEffect = new AttackEffect[100];

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
            return controlledTankArray[playerNum];
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

            //calculate position
            double tempResult = Terrain.WIDTH / (numPlayers + 1.0);
            var tempWidth = (int)Math.Round(tempResult,0);

            for (int i = 0; i < numPlayers; i++)
            {
                if (counter == 0)
                {
                    temp[i] = tempWidth;
                    counter = (int) (counter + tempWidth*2);
                }
                else
                {
                    temp[i] = counter;
                    counter = (int) (counter + tempWidth);
                }
            }
            return temp;
        }  

        public static void Shuffle(int[] array)
        {
            //from previous assessment
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
            startingPlayer = 0;
            CommenceRound();
        }

        public void CommenceRound()
        {
                
            //Initialising a private field of Battle representing the current player to the value of the starting GenericPlayer field(see NewGame).
            currentPlayer = startingPlayer;

            //Creating a new Terrain, which is also stored as a private field of Battle.
            Battle.newTerrain =  new Terrain();
            //Creating an array of GenericPlayer positions by calling CalculatePlayerPositions with the number of GenericPlayers playing the game(hint: get the length of the GenericPlayers array)
            int[] calcedArray = CalculatePlayerPositions(playerArray.Length);
            //Looping through each GenericPlayer and calling its BeginRound method.
           // foreach (var player in playerArray)
            //{
            //    player.BeginRound();
            //}
            //Shuffling that array of positions with the Shuffle method.

            Shuffle(calcedArray);
            //Creating an array of ControlledTank as a private field.There should be the same number of ControlledTanks as there are GenericPlayers in the GenericPlayer array.
            ControlledTank[] controlledTankArray  = new ControlledTank[playerArray.Length - 1];

            //Initialising the array of ControlledTank by: 
            //finding the horizontal position of the ControlledTank(by looking up the appropriate index of shuffled calcedArray
            for (int i = 0; i < controlledTankArray.Length-1; i++)
            {
                Console.WriteLine("creating controlled tank array");
                var tankHoriz = newTerrain.TankVerticalPosition(calcedArray[i]);
                controlledTankArray[i] = new ControlledTank(
                    playerArray[i],
                    calcedArray[i],
                    tankHoriz,
                    this);

            }      

            //Initialising the wind speed, another private field of Battle, to a random number between -100 and 100.
            windSpeed = rng.Next(-100, 101);

            //Creating a new GameplayForm and Show()ing it.
            GameplayForm gPlayForm = new GameplayForm(this);
            gPlayForm.Show();
        }

        public Terrain GetBattlefield()
        {
            return newTerrain;
        }

        public void DisplayPlayerTanks(Graphics graphics, Size displaySize)
        {
            //Loop over each ControlledTanks in the array.
            // this breaks Battle.NewGame();
 //           for (int i = 0; i < controlledTankArray.Length-1; i++)
  //          {
   //             if (controlledTankArray[i].IsAlive())
    //           {
     //               controlledTankArray[i].Display(graphics,displaySize);
      //          }
       //     }
        }

        public ControlledTank CurrentPlayerTank()
        {
            //needs NewGame() to be run.
            return controlledTankArray[currentPlayer];
        }

        public void AddEffect(AttackEffect weaponEffect)
        {
            for (int i = 0; i < attackEffect.Length - 1; i++)
            {
                if (attackEffect[i] == null)
                {
                    attackEffect[i] = weaponEffect;
                    break;
                }
                break;
            }
        }

        public bool WeaponEffectStep()
        {
            throw new NotImplementedException();
        }

        public void DrawWeaponEffects(Graphics graphics, Size displaySize)
        {
            for (int i = 0; i < attackEffect.Length - 1; i++) {
                if (attackEffect[i] == null)
                {
                    break;
                }
                else
                {
                    attackEffect[i].Display(graphics,displaySize);
                }
            }
        }

        public void EndEffect(AttackEffect weaponEffect)
        {
            for (int i = 0; i < attackEffect.Length - 1; i++) {
                if (attackEffect[i] == weaponEffect) {
                    attackEffect[i] = null;
                } 
            }
        }

        public bool CheckCollidedTank(float projectileX, float projectileY)
        {
           // If the coordinates given are outside the map boundaries(less than 0 or greater than Terrain.WIDTH or Terrain.HEIGHT respectively), return false.
            if (projectileX < 0 || projectileX > Terrain.WIDTH || projectileY < 0 || projectileY > Terrain.HEIGHT)
            {
                return false;
            }
            // If the Terrain contains something at that location(hint: use IsTileAt), return true.
            if (newTerrain.IsTileAt((int)projectileX, (int)projectileY)) //are these axis correct?
            {
                return true;
            }

            // If there is a ControlledTank at that location, return true.
            // To detect collisions against ControlledTanks, loop through the array of ControlledTanks and check if the point described by projectileX, projectileY 
            // is inside the rectangle occupied by ControlledTank. The position of the ControlledTank can be found using the XPos() and Y() methods, while 
            // the width and height are stored in TankType.WIDTH and TankType.HEIGHT.
            // Note that collisions can never occur against the current player's ControlledTank. Otherwise shots fired by a tank would instantly hit that same tank.

            foreach (var tank in controlledTankArray)
            {
                if (tank == controlledTankArray[currentPlayer])
                {
                    break; //current player
                }
                if (projectileX >= tank.XPos() && (projectileX) <= (tank.XPos() + 3))// need to verify this
                {
                    return true;
                }
                if (projectileY >= tank.Y() && (projectileY) <= (tank.Y() + 2)) { //need to verify this
                    return true;
                }
            }
            return false;

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
