using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public partial class TitleForm : Form
    {
        public TitleForm()
        {
            InitializeComponent();
        }

        private void newGameButton_Click(object sender, EventArgs e) {
            Battle game = new Battle(2, 5);
            TankType tank = new SmallTank();
            GenericPlayer player1 = new HumanOpponent("Player 1", tank, Color.Brown);
            GenericPlayer player2 = new HumanOpponent("Player 2", tank, Color.Goldenrod);
            //GenericPlayer player3 = new HumanOpponent("Player 2", tank, Color.Aqua);
            game.SetPlayer(1, player1);
            game.SetPlayer(2, player2);
            //game.SetPlayer(3, player3);
            game.NewGame();
        }

        private void TitleForm_Load(object sender, EventArgs e) {
            this.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e) {

        }
    }
}
