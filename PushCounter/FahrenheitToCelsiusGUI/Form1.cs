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
        bool boxChecked = false;
        public Form1() {
            InitializeComponent();
            button1.Enabled = false;
            checkBox1.Text = "Box is unchecked";
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            if (boxChecked) {
                checkBox1.Text = "Box is unchecked";
                boxChecked = false;
            } else {
                checkBox1.Text = "Box is checked";
                boxChecked = true;
            }
            }
        }
    }




