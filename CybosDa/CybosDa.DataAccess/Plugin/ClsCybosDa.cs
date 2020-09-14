using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybosDa.DataAccess.Plugin
{
    public static class ClsCybosDa
    {
        public static CPUTILLib.CpStockCode S_CpStockCode = new CPUTILLib.CpStockCode();
        public static CPUTILLib.CpCodeMgr S_CpCodeMgr = new CPUTILLib.CpCodeMgr();
        public static CPTRADELib.CpTdUtil S_CpTdUtil = new CPTRADELib.CpTdUtil();
        public static CPUTILLib.CpCybos S_CpCybos = new CPUTILLib.CpCybos();
        public static DSCBO1Lib.CpConclusion S_CpConclusion = new DSCBO1Lib.CpConclusion();
    }
}
