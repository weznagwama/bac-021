using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankBattle
{
    public class HumanOpponent : GenericPlayer
    {
        public HumanOpponent(string name, TankType tank, Color colour) : base(name, tank, colour)
        {
            throw new NotImplementedException();
        }

        public override void BeginRound()
        {
            throw new NotImplementedException();
        }

        public override void CommenceTurn(GameplayForm gameplayForm, Battle currentGame)
        {
            throw new NotImplementedException();
        }

        public override void ProjectileHitPos(float x, float y)
        {
            throw new NotImplementedException();
        }
    }
}
