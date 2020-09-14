using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybosDa.Common.Class
{
    public class ClsDefineDataType
    {
        public class TradeGb
        {
            public enum TradeGbTypeIndex { 전체 =0, 개인 = 1, 외국인 = 2, 기관계 = 3, 금융투자 = 4, 보험 = 5, 투신 = 6, 은행 = 7, 기타금융 = 8,
                                           연기금 = 9, 국가지자체 = 10, 기타외인 = 11, 사모펀드 = 12, 기타법인 = 13}

            public void SetCpSvr7254SetColumn(ref DataTable dt)
            {
                CpSvr7254 oCpSvr7254 = new CpSvr7254();
                oCpSvr7254.SetCpSvr7254SetColumn(ref dt);
            }
            internal class CpSvr7254
            {
                internal void SetCpSvr7254SetColumn(ref DataTable dt)
                {
                    if (dt != null)
                    {
                        dt = null;
                        dt = new DataTable();
                    }

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
                    dt.Columns.Add("종가", typeof(long));
                    dt.Columns.Add("대비", typeof(long));
                    dt.Columns.Add("대비율", typeof(double));
                    dt.Columns.Add("거래량", typeof(long));
                    dt.Columns.Add("확정치여부", typeof(char));

                }
            }
        }
    }
}
