using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TankBattle
{
    public class Battle
    {
        private static int playerCount;
        private static int roundCount;
        private static GenericPlayer[] genePlayerArray;

        static Random rng = new Random();

        public Battle(int numPlayers, int numRounds)
        {
            GenericPlayer[] genePlayerArray = new GenericPlayer[numRounds];
            int[] playerArray = new int[numPlayers];
            List<string> attackEffect = new List<string>();

            playerCount = numPlayers;
            roundCount = numRounds;


        }

        public int PlayerCount()
        {
            return playerCount;
        }

        public int GetRound()
        {
            //unsure yet
            return roundCount;
        }

        public int GetRounds()
        {
            return roundCount;
        }

        public void SetPlayer(int playerNum, GenericPlayer player)
        {
            genePlayerArray[playerNum-1] = player;
        }

        public GenericPlayer GetPlayerNumber(int playerNum)
        {
            return genePlayerArray[playerNum-1];
        }

        public ControlledTank GetPlayerTank(int playerNum)
        {
            throw new NotImplementedException();
        }

        public static Color PlayerColour(int playerNum)
        {
            var displayColour = genePlayerArray[playerNum - 1].PlayerColour();
            return displayColour;
        }

        public static int[] CalculatePlayerPositions(int numPlayers)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void CommenceRound()
        {
            throw new NotImplementedException();
        }

        public Terrain GetBattlefield()
        {
            throw new NotImplementedException();
        }

        public void DisplayPlayerTanks(Graphics graphics, Size displaySize)
        {
            foreach (var player in genePlayerArray)
            {
                //do stuff perhapsf
            }
        }

        public ControlledTank CurrentPlayerTank()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        
        public int Wind()
        {
            throw new NotImplementedException();
        }
    }
}
