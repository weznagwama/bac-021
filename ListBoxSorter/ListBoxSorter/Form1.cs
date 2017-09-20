using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListBoxSorter {
    public partial class Form1 : Form {
        List<int> numList = new List<int>();
        int num;

        public Form1() {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e) {
            listBox1.BeginUpdate();
            listBox1.Items.Add(num);
            numList.Add(num);
            listBox1.EndUpdate();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            num = (int)this.numericUpDown1.Value;
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
    }
}
