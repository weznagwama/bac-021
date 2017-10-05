﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace TankBattle
{
    public class Battle
    {
        //private static Battle game;
        private static int numplayers;
        private static int numRounds;
        internal static GenericPlayer[] playerArray;
        internal ControlledTank[] controlledTankArray;
        internal AttackEffect[] attackEffect = new AttackEffect[100];
        internal GameplayForm gPlayForm;

        private Terrain newTerrain;
        private static int windSpeed;

        private static int currentRound;
        private static int startingPlayer;
        internal int currentPlayer;

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
            return currentRound;
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
            newTerrain =  new Terrain();
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
            controlledTankArray  = new ControlledTank[playerArray.Length];

            //Initialising the array of ControlledTank by: 
            //finding the horizontal position of the ControlledTank(by looking up the appropriate index of shuffled calcedArray
            for (int i = 0; i < controlledTankArray.Length-1; i++)
            {
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
            gPlayForm = new GameplayForm(this);
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
            for (int i = 0; i < controlledTankArray.Length-1; i++)
            {
                if (controlledTankArray[i].IsAlive())
              {
                    controlledTankArray[i].Display(graphics,displaySize);
                }
            }
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
                    continue;
                }
            }
        }

        public bool WeaponEffectStep()
        {
            bool anyWeapon = false;
            for (int i = 0; i < attackEffect.Length - 1; i++) {
                if (attackEffect[i] == null) {
                    continue;
                } else
                {
                    anyWeapon = true;
                    attackEffect[i].ProcessTimeEvent();
                }
            }
            return anyWeapon;
        }

        public void DrawWeaponEffects(Graphics graphics, Size displaySize)
        {
            //this is never getting initialised? and causing errors.
            for (int i = 0; i < attackEffect.Length - 1; i++) {
                if (attackEffect[i] == null)
                {
                    continue;
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
            for (int i = 0; i < controlledTankArray.Length - 1; i++)
            {
                if (controlledTankArray[i].IsAlive())
                {
                    float middlePos = controlledTankArray[i].XPos() + controlledTankArray[i].Y() +
                                      (TankType.WIDTH / 2) + (TankType.HEIGHT / 2);

                    double calculation = Math.Sqrt(Math.Pow(controlledTankArray[i].XPos() - damageX, 2) +
                                                 Math.Pow(controlledTankArray[i].Y() - damageY, 2));
                    float newCalc = (float) calculation;

                    if (newCalc > radius / 2 && newCalc < radius)
                    {
                        float damage = (int) explosionDamage * ((newCalc - radius) / radius); //need to check this newCalc-radius calculation
                        int newDamage = (int) damage;
                        controlledTankArray[i].DamageArmour(newDamage);
                    }

                    if (newCalc < radius/2)
                    {
                        //have to cast damage to int because ControlledTank is int
                        int damage = (int) explosionDamage;
                        controlledTankArray[i].DamageArmour(damage);
                    }

                }
            }
        }

        public bool CalculateGravity()
        {
            bool isCalled = false;
            if (newTerrain.CalculateGravity())
            {
                isCalled = true;
            }
            for (int i = 0; i < controlledTankArray.Length - 1; i++)
            {
                if (controlledTankArray[i].CalculateGravity())
                {
                    isCalled = true;
                    return isCalled;
                }
            }
            //If the bool keeping track of whether anything moved is set to true, return true. Otherwise return false.
            return isCalled;

        }

        public bool TurnOver()
        {
            int anyAlive = 0;
            for (int i = 0; i < controlledTankArray.Length - 1; i++) {
                if (controlledTankArray[i].IsAlive()) {
                    anyAlive++;
                }
            }
            if (anyAlive >= 2) {
                currentPlayer++;
                if (controlledTankArray[currentPlayer].IsAlive()) {
                    //this player is now the current player?
                }
                windSpeed = rng.Next(windSpeed - 10, windSpeed + 11);
                return true;
            } else {
                this.CheckWinner();
                return false;
            }
        }

        public void CheckWinner()
        {
            for (int i = 0; i < controlledTankArray.Length - 1; i++)
            {
                if (controlledTankArray[i].IsAlive())
                {
                    playerArray[i].AddPoint();
                }
            }
        }

        public void NextRound()
        {
            currentRound++;
            if (currentRound <= Battle.numRounds)
            {
                startingPlayer++;
                if (startingPlayer == Battle.numplayers)
                {
                    startingPlayer = 0;
                }
            }
            else
            {
                gPlayForm.Show();
            }
            

        }
        
        public int Wind()
        {
            return windSpeed;
        }
    }
}
