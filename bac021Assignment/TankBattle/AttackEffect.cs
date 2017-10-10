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

        protected Battle theGame;
        protected int gameWindSpeed;

        public void ConnectGame(Battle game)
        {
            theGame = game;
            gameWindSpeed = theGame.Wind();

        }

        public abstract void ProcessTimeEvent();
        public abstract void Display(Graphics graphics, Size displaySize);
    }
}
