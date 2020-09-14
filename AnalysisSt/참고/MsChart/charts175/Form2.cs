using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace charts175
{
    public partial class Form2 : Form
    {
        public Form2(Chart c)
        {
            InitializeComponent();
            uc_caCtl CaCtl = new uc_caCtl();
            CaCtl.chart = c;
            Controls.Add(CaCtl);
            CaCtl.Dock = DockStyle.Fill;

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
