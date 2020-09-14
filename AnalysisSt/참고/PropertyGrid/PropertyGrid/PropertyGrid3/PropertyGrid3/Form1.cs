using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PropertyGrid3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            PropertyGridSimpleDemoClass3 pgsd = new PropertyGridSimpleDemoClass3();
            prpG.SelectedObject = pgsd;            
        }
    }
}