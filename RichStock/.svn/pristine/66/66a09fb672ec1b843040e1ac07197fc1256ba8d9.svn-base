using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaikRichStock.Common;

namespace StockDayDataSaver
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //frmTest frm1 = new frmTest();
            //Application.Run(frm1);
            //return;

            frmSaver frm = new frmSaver();
            frmSaverNew frm1 = new frmSaverNew();

            if (args.Length > 0 && args[0] == "evening")
            {
                frm1 = new frmSaverNew("evening");
                Application.Run(frm1);
            }
            else if (args.Length > 0 && args[0] == "evening1")
            {
                frm1 = new frmSaverNew("evening1");
                Application.Run(frm1);
            }
            else if (args.Length > 0 && args[0] == "evening2")
            {
                frm1 = new frmSaverNew("evening2");
                Application.Run(frm1);
            }
            else if (args.Length > 0 && args[0] == "evening3")
            {
                frm1 = new frmSaverNew("evening3");
                Application.Run(frm1);
            }
            else if (args.Length > 0 && args[0] == "morning")
            {
                frm1 = new frmSaverNew("morning");
                Application.Run(frm1);
            }
            else if (args.Length > 0 && args[0] == "gong")
            {
                frm1 = new frmSaverNew("gong");
                Application.Run(frm1);
            }
            else if (args.Length > 0 && args[0] == "relate")
            {
                frm1 = new frmSaverNew("relate");
                Application.Run(frm1);
            }
            else if (args.Length > 0 && args[0] == "company")
            {
                frm1 = new frmSaverNew("company");
                Application.Run(frm1);
            }
            else
            {
                Application.Run(frm1);
            }
        }
    }
}
