using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackbarBoxSorter {
    public partial class Form1 : Form {
        List<int> numList = new List<int>();
        int num;

        public Form1() {
            InitializeComponent();
            label1.Text = "0";
            this.Text = "hello123";
        }

        private void button1_Click(object sender, EventArgs e) {
            listBox1.BeginUpdate();
            listBox1.Items.Add(num);
            numList.Add(num);
            listBox1.EndUpdate();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void button2_Click(object sender, EventArgs e) {
            listBox1.BeginUpdate();
            listBox1.Items.Clear();
            numList.Sort();
            foreach (var thing in numList) {
                listBox1.Items.Add(thing);
            }
            listBox1.EndUpdate();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            num = trackBar1.Value;
            label1.Text = "" + trackBar1.Value;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
           
        }

        private void Form1_Load(object sender, EventArgs e) {

        }
    }
}
