using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    abstract public class GenericPlayer
    {
        public GenericPlayer(string name, TankType tank, Color colour)
        {
            throw new NotImplementedException();
        }
        public TankType CreateTank()
        {
            throw new NotImplementedException();
        }
        public string GetName()
        {
            throw new NotImplementedException();
        }
        public Color PlayerColour()
        {
            throw new NotImplementedException();
        }
        public void AddPoint()
        {
            throw new NotImplementedException();
        }
        public int GetVictories()
        {
            throw new NotImplementedException();
        }

        public abstract void BeginRound();

        public abstract void CommenceTurn(GameplayForm gameplayForm, Battle currentGame);

        public abstract void ProjectileHitPos(float x, float y);
    }
}
