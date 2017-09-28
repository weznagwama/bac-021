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
        private string name;
        private TankType tank;
        private Color colour;
        private int roundsWon = 0;

        public GenericPlayer(string name, TankType tank, Color colour)
        {
            this.name = name;
            this.tank = tank;
            this.colour = colour;
        }
        public TankType CreateTank()
        {
            return tank;
        }
        public string GetName()
        {
            return name;
        }
        public Color PlayerColour()
        {
            return colour;
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
