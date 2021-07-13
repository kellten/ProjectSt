using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Woom.Tester.Class
{
    public class ClsTesterUtil
    {
        public void ShowChildForm(Form childForm, bool openType, Form sender)
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
                    if (openType == true)
                    {
                        childForm.Show();
                    }
                    else
                    {
                        childForm.MdiParent = sender.MdiParent;
                        childForm.Show();
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { }
        }
    }
}
