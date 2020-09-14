using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisSt.Dasin.Define
{
    class clsDefineDataTable
    {
        public Boolean SetDtCpSvr7254(DataTable dt)
        {
            try
            {
                dt.Clear();
                dt.Columns.Clear();
                dt.Columns.Add("일자", typeof(long));
                dt.Columns.Add("개인", typeof(long));
                dt.Columns.Add("외국인", typeof(long));
                dt.Columns.Add("기관계", typeof(long));
                dt.Columns.Add("금융투자", typeof(long));
                dt.Columns.Add("보험", typeof(long));
                dt.Columns.Add("투신", typeof(long));
                dt.Columns.Add("은행", typeof(long));
                dt.Columns.Add("기타금융", typeof(long));
                dt.Columns.Add("연기금", typeof(long));
                dt.Columns.Add("기타법인", typeof(long));
                dt.Columns.Add("기타외인", typeof(long));
                dt.Columns.Add("사모펀드", typeof(long));
                dt.Columns.Add("국가지자체", typeof(long));
                
                return true;

            }
            catch (Exception)
            {
                   throw;
            }

            return false;
                                                               
         }

    }
}
