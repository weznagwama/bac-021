using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankBattle
{
    public abstract class AttackEffect
    {
        public void ConnectGame(Battle game)
        {
            throw new NotImplementedException();
        }

        public abstract void ProcessTimeEvent();
        public abstract void Display(Graphics graphics, Size displaySize);
    }
}
