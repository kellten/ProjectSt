using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StudyProject.DesignPattern.GOF;

namespace StudyProject.DesignPattern.Forms
{
    public partial class FrmSingleton1 : Form
    {
        public FrmSingleton1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ClsSingleTon1 os1 = ClsSingleTon1.Instance();
            ClsSingleTon1 os2 = ClsSingleTon1.Instance();

            os1.Name = "test";
            os2.Name = "test2";

            rcText.Text = os1.Name;
            rcText.Text = rcText.Text + "\r\n" + os2.Name;
        }
    }
}
