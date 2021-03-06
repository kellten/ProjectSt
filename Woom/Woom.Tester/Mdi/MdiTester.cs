﻿using System;
using System.Windows.Forms;

namespace Woom.Tester.Mdi
{
    public partial class MdiTester : Form
    {
        private int childFormNumber = 0;

        public MdiTester()
        {
            InitializeComponent();
        }

        private string _openType = "1";

        public void ShowChildForm(Form childForm)
        {
            Boolean isAlreadyContained = false;
            FormCollection fc = Application.OpenForms;
            try
            {
                foreach (Form frm in fc)
                {
                    if (frm == childForm)
                    {
                        isAlreadyContained = true;
                        frm.Activate();
                    }
                }

                if (isAlreadyContained == false)
                {
                    if (_openType == "1")
                    {
                        childForm.Show();
                    }
                    else
                    {
                        childForm.MdiParent = this;
                        childForm.Show();
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { }
        }

        private void tsbConnectionInfo_Click(object sender, EventArgs e)
        {
            Form oform = new Woom.DataAccess.Forms.FrmConnectionInfo();
            ShowChildForm(oform);
        }

        private void mnuItem10059_Click(object sender, EventArgs e)
        {
            Form oform = new Woom.Tester.Forms.FrmOptCallerTest();
            ShowChildForm(oform);
        }

        private void opt10069ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form oform = new Woom.Tester.Forms.FrmOpt10060Caller();
            ShowChildForm(oform);
        }

        private void opt10081ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form oform = new Woom.Tester.Forms.FrmOpt10081Caller();
            ShowChildForm(oform);
        }
    }
}