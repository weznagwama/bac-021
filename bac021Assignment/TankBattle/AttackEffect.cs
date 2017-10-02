using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TankBattle
{
    public abstract class AttackEffect
    {

        protected static Battle theGame;

        public void ConnectGame(Battle game)
        {
            theGame = game;
        }

        public abstract void ProcessTimeEvent();
        public abstract void Display(Graphics graphics, Size displaySize);
    }
}
