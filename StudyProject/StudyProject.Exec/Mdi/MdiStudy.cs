using System;
using System.Windows.Forms;

namespace StudyProject.Exec.Mdi
{
    public partial class MdiStudy : Form
    {

        public MdiStudy()
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

        private void tsbMenuSingleTon1_Click(object sender, EventArgs e)
        {
            Form frm = new StudyProject.DesignPattern.Forms.FrmSingleton1();
            ShowChildForm(frm);
        }

        private void tsbFactoryMethod1_Click(object sender, EventArgs e)
        {
            Form frm = new StudyProject.DesignPattern.Forms.FrmFactoryMethod1();
            ShowChildForm(frm);
        }

        private void tsbAstractFM1_Click(object sender, EventArgs e)
        {
            Form frm = new StudyProject.DesignPattern.Forms.FrmAbstactFactoryMethod1();
            ShowChildForm(frm);
        }

        private void TsbbindingList_Click(object sender, EventArgs e)
        {
            Form frm = new StudyProject.CSharpControl.Forms.FrmBindingListTester();
            ShowChildForm(frm);
        }
    }
}
