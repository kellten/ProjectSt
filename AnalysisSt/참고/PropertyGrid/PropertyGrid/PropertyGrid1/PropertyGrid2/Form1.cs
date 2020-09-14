using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PropertyGrid2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PropertyGridSimpleDemoClass pgdc = new PropertyGridSimpleDemoClass();
            prpG.SelectedObject = pgdc;
        }

        private void prpG_Click(object sender, EventArgs e)
        {

        }
    }
}