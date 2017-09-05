using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PushCounter {
    public partial class Form1 : Form {
        int totalCount = 0;
        public Form1() {
            InitializeComponent();
            button1.Enabled = false;
        }

        private void convertButton_Click(object sender, EventArgs e) {

            int num = letsIncriment(totalCount);
            outputLabel.Visible = true;
            outputLabel.Text = string.Format("{0} times", num);
            totalCount = num;
            button1.Enabled = true;
            return;

        }//end Button_Click


        int letsIncriment(int counter) {
            return counter + 1;
        }

        private void button1_Click(object sender, EventArgs e) {
            totalCount = 0;
            outputLabel.Text = string.Format("0 times");
            button1.Enabled = false;
        }
    }


}

