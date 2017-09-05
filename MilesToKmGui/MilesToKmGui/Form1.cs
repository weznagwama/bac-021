using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MilesToKm {
    public partial class Form1 : Form {
        bool textEntered = false;
        bool textCheck;
        string choice;
        double distance;

        public Form1() {
            InitializeComponent();
            label1.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private void milesButton_checked(object sender, EventArgs e) {
            choice = "kilometres";
        }

        private void kmButton_checked(object sender, EventArgs e) {
            choice = "miles";
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            textCheck = double.TryParse(this.textBox1.Text, out double distanceTest);
            distance = distanceTest;
            textEntered = true;
        }

        private double distConvert(double distance) {
            if (choice == "kilometres") {
                distance = distance * 1.609344;
            } else {
                distance = distance / 1.609344;
            }
            return distance;
        }

        private void label1_Click(object sender, EventArgs e) {
            label1.Text = string.Format("Distance in miles is ___");
            }

        private void button1_Click(object sender, EventArgs e) {
            if (!textEntered||!textCheck) {
                label1.Text = string.Format("Invalid input");
                label1.Visible = true;
            } else {
                double finaldist = distConvert(distance);
                finaldist = Math.Round(finaldist, 2);
                label1.Text = string.Format("Distance in {0} is {1}", choice, finaldist);
                label1.Visible = true;
            }
        }
    }
}

