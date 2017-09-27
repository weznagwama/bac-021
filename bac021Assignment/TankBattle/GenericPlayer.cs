using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public abstract class GenericPlayer
    {
        private string genericName;
        private TankType genericTank;
        private Color genericColor;
        private int roundsWon = 0;

        public GenericPlayer(string name, TankType tank, Color colour)
        {
            genericName = name;
            genericTank = tank;
            genericColor = colour;
        }
        public TankType CreateTank()
        {
            return genericTank;
        }
        public string GetName()
        {
            return genericName;
        }
        public Color PlayerColour()
        {
            return genericColor;
        }
        public void AddPoint()
        {
            roundsWon++;
        }
        public int GetVictories()
        {
            return roundsWon;
        }

        public abstract void BeginRound();

        public abstract void CommenceTurn(GameplayForm gameplayForm, Battle currentGame);

        public abstract void ProjectileHitPos(float x, float y);
    }
}
