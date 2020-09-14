using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudyProject.DesignPattern.Util
{
    public static class ClsFunc
    {
        public static void WriteRcText(RichTextBox rc, string str)
        {
            rc.Text = rc.Text + str + "\r\n";
        }
    }
}
