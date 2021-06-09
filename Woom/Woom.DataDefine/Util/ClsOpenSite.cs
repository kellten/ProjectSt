using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woom.DataDefine.Util
{
    public class ClsOpenSite
    {
        public void OpenFnGuideSite(string stockCode)
        {
            System.Diagnostics.Process.Start("http://comp.fnguide.com/SVO2/ASP/SVD_Main.asp?pGB=1&gicode=" + "A" + stockCode + "&cID=&MenuYn=Y&ReportGB=D&NewMenuID=Y&stkGb=701");
        }

        public void OpenDartFss()
        {
            System.Diagnostics.Process.Start("http://newdart.fss.or.kr/");
        }
    }
}
