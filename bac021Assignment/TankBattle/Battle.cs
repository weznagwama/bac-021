using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


// BUGS:
//
// damagearmour doesnt appear work
// ground goes below screen
// player tank colour only works for second player, which applies to both players
// the 'current player' (which I think is the last tank variable), gets draws for both players :S

namespace TankBattle {
    public class Battle {
        //private static Battle game;
        private int numplayers;
        private int numRounds;
        internal static GenericPlayer[] playerArray;
        internal ControlledTank[] controlledTankArray;
        internal AttackEffect[] attackEffect;
        internal GameplayForm gPlayForm;
        internal static Color[] playerColours = {Color.AliceBlue, Color.Aqua, Color.Blue, Color.Brown, Color.Beige, Color.Chartreuse, Color.Crimson, Color.DarkMagenta, Color.DarkOrchid, Color.Coral};


        private Terrain newTerrain;
        private static int windSpeed;

        private static int currentRound;
        private static int startingPlayer;
        internal int currentPlayer;

        static Random rng = new Random();

        public Battle(int numPlayers, int numRounds) {
            playerArray = new GenericPlayer[numPlayers];
            
            //int[] playerArray = new int[numPlayers];
            attackEffect = new AttackEffect[100];


            this.numRounds = numRounds;
            this.numplayers = numPlayers;
        }

        public int PlayerCount() {
            return numplayers;
        }

        public int GetRound() {
            return currentRound;
        }

        public int GetRounds() {
            return numRounds;
        }

        public void SetPlayer(int playerNum, GenericPlayer player) {
            playerArray[playerNum - 1] = player;
        }

        public GenericPlayer GetPlayerNumber(int playerNum) {
            return playerArray[playerNum - 1];
        }

        public ControlledTank GetPlayerTank(int playerNum) {
            return controlledTankArray[playerNum];
        }

        public static Color PlayerColour(int playerNum)
        {
            var tempNum = playerNum - 1;
            var c = playerColours[tempNum];
            return c;
        }

        public static int[] CalculatePlayerPositions(int numPlayers) {

            int counter = 0;
            int[] temp = new int[numPlayers];

            //calculate position
            double tempResult = Terrain.WIDTH / (numPlayers + 1.0);
            var tempWidth = (int)Math.Round(tempResult, 0);

            for (int i = 0; i < numPlayers; i++) {
                if (counter == 0) {
                    temp[i] = tempWidth;
                    counter = (int)(counter + tempWidth * 2);
                } else {
                    temp[i] = counter;
                    counter = (int)(counter + tempWidth);
                }
            }
            return temp;
        }

        public static void Shuffle(int[] array) {
            //from AMS assessment
            for (var i = array.Length - 1; i > 0; i--) {
                var temp = array[i];
                var index = rng.Next(0, i + 1);
                array[i] = array[index];
                array[index] = temp;
            }
        }

        public void NewGame() {
            currentRound = 1;
            startingPlayer = 0;
            CommenceRound();
        }

        public void CommenceRound() {
            currentPlayer = startingPlayer;
            newTerrain = new Terrain();
            int[] calcedArray = CalculatePlayerPositions(playerArray.Length);
           foreach (var player in playerArray) {
                player.BeginRound();
            }
            Shuffle(calcedArray);
            controlledTankArray = new ControlledTank[playerArray.Length];
            for (int i = 0; i < controlledTankArray.Length; i++) {
                var tankHoriz = newTerrain.TankVerticalPosition(calcedArray[i]);
                controlledTankArray[i] = new ControlledTank(
                    playerArray[i],
                    calcedArray[i],
                    tankHoriz,
                    this);

            }
            windSpeed = rng.Next(-100, 101);

            gPlayForm = new GameplayForm(this);
            gPlayForm.Show();
        }

        public Terrain GetBattlefield() {
            return newTerrain;
        }

        public void DisplayPlayerTanks(Graphics graphics, Size displaySize) {

            for (int i = 0; i < controlledTankArray.Length; i++) {
                if (controlledTankArray[i].IsAlive()) {
                    controlledTankArray[i].Display(graphics, displaySize);
                }
            }
        }

        public ControlledTank CurrentPlayerTank()
        {
            return controlledTankArray[currentPlayer];
        }

        public void AddEffect(AttackEffect weaponEffect)
        {
            int upTo = 0;
            for (int i = 0; i < attackEffect.Length - 1; i++) {
                if (attackEffect[i] == null) {
                    attackEffect[i] = weaponEffect;
                    upTo = i;
                    break;
                }
            }
            attackEffect[upTo].ConnectGame(this);
        }

        public bool WeaponEffectStep() {
            bool anyWeapon = false;
            for (int i = 0; i < attackEffect.Length; i++) {
                if (attackEffect[i] == null) continue;
                anyWeapon = true;
                attackEffect[i].ProcessTimeEvent();
            }
            return anyWeapon;
        }

        public void DrawWeaponEffects(Graphics graphics, Size displaySize) {
            for (int i = 0; i < attackEffect.Length - 1; i++) {
                if (attackEffect[i] == null) {
                } else {
                    attackEffect[i].Display(graphics, displaySize);
                }
            }
        }

        public void EndEffect(AttackEffect weaponEffect) {
            for (int i = 0; i < attackEffect.Length - 1; i++) {
                if (attackEffect[i] == weaponEffect) {
                    attackEffect[i] = null;
                }
            }
        }

        public bool CheckCollidedTank(float projectileX, float projectileY)
        {
            if (projectileX < 0 || projectileX > Terrain.WIDTH || projectileY < 0 || projectileY > Terrain.HEIGHT)
            {
                return false;
            }

            if (newTerrain.IsTileAt((int)projectileX,(int)projectileY)) {
                return true;
            }

            foreach (var tank in controlledTankArray)
            {
                // If there is a ControlledTank at that location and that ControlledTank returns true when IsAlive is called on it, return true.
                //if (newTerrain.IsTileAt()
                if (tank.GetPlayerNumber() == controlledTankArray[currentPlayer].GetPlayerNumber())
                {
                    continue;
                }
                for (int iy = (int)projectileY;
                    iy < (int) projectileY + TankType.HEIGHT;
                    iy++)
                {
                    for (int ix = (int) projectileX;
                        ix < (int) projectileX + TankType.WIDTH;
                        ix++)
                    {

                        if (newTerrain.IsTileAt(ix,iy))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void DamageArmour(float damageX, float damageY, float explosionDamage, float radius) {
            for (int i = 0; i < controlledTankArray.Length; i++) {
                if (controlledTankArray[i].IsAlive()) {
                    float middleX = controlledTankArray[i].XPos() + (TankType.WIDTH / 2); 
                    float middleY = controlledTankArray[i].Y() + (TankType.HEIGHT / 2);

                    double calculation = Math.Sqrt(Math.Pow(middleX - damageX, 2) +
                                                   Math.Pow(middleY - damageY, 2));

                    float newCalc = (float)calculation;

                    if (newCalc > radius)
                    {
                        controlledTankArray[i].DamageArmour(0);
                    }

                    if (newCalc > radius / 2 && newCalc < radius) {
                        float damage = (int)explosionDamage * ((newCalc - radius) / radius); 
                        int newDamage = (int)damage;
                        controlledTankArray[i].DamageArmour(newDamage);
                    }

                    if (newCalc < radius / 2) {
                        int damage = (int)explosionDamage;
                        controlledTankArray[i].DamageArmour(damage);
                    }

                }
            }
        }

        public bool CalculateGravity() {
            bool isCalled = newTerrain.CalculateGravity();
            for (int i = 0; i < controlledTankArray.Length; i++) {
                if (controlledTankArray[i].CalculateGravity()) {
                    isCalled = true;
                }
            }
            return isCalled;

        }

        public bool TurnOver() {
            int anyAlive = controlledTankArray.Count(t => t.IsAlive());
            if (anyAlive >= 2) {
                currentPlayer++;
                if (currentPlayer == numplayers) {
                    currentPlayer = 0;
                }
                if (controlledTankArray[currentPlayer].IsAlive())
                {

                }
                windSpeed = rng.Next(windSpeed - 10, windSpeed + 11);
                return true;
            } else {
                this.CheckWinner();
                return false;
            }
        }

        public void CheckWinner() {
            for (int i = 0; i < controlledTankArray.Length - 1; i++) {
                if (controlledTankArray[i].IsAlive()) {
                    playerArray[i].AddPoint();
                }
            }
        }

        public void NextRound() {
            currentRound++;
            if (currentRound <= numRounds) {
                startingPlayer++;
                if (startingPlayer == numplayers) {
                    startingPlayer = 0; 
                }
                CommenceRound();
            } else {
                var gPlayForm1 = new TitleForm();;
                gPlayForm1.Show();
            }

        }

        public int Wind() {
            return windSpeed;
        }
    }
}
