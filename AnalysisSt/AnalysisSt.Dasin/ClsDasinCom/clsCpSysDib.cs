using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalysisSt.Dasin.InterFaceICpSysDib;
using AnalysisSt.Dasin.ModModCpSysDib;
using CPSYSDIBLib;
using AnalysisSt.Dasin.Define;

namespace AnalysisSt.Dasin.ClsDasinCom
{
    public class clsCpSysDib : ICpSysDib
    {

        private DataTable _dt = new DataTable();
        private clsDefineDataTable _clsDefineDataTable = new clsDefineDataTable();
        private Boolean _regEvent = false;

        public void RegEvent()
        {
            if (_regEvent == false)
            {
                ModModCpSysDib.ModCpSysDib.ModCpSvr7254.Received += CpSvr7254_OnReceived;
                _regEvent = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stockCode">[0] - 종목코드</param>
        /// <param name="GiganSunTakGb">[1] 0:사용자지정 1:1개,월, 2:2개월 3:3개월 4:6개월,5:최근5일 6:일별</param>
        /// <param name="fromDate">[2] 시작일자: 기간선택구분을 0이 아닐경우 생략</param>
        /// <param name="toDate">[3]  끝일자: 기간선택구분을 0이 아닐경우 생략 </param>
        /// <param name="trader">[5] (short)  투자자 </param>
        public void CpSvr7254_SetInputValue(String stockCode, Int16 GiganSunTakGb, long fromDate, long toDate, Int16 trader)
        {
            RegEvent();
            ModModCpSysDib.ModCpSysDib.ModCpSvr7254.SetInputValue(0, stockCode);
            ModModCpSysDib.ModCpSysDib.ModCpSvr7254.SetInputValue(1, GiganSunTakGb);
            ModModCpSysDib.ModCpSysDib.ModCpSvr7254.SetInputValue(2, fromDate);
            ModModCpSysDib.ModCpSysDib.ModCpSvr7254.SetInputValue(3, toDate);
            ModModCpSysDib.ModCpSysDib.ModCpSvr7254.SetInputValue(4, '1');
            ModModCpSysDib.ModCpSysDib.ModCpSvr7254.SetInputValue(5, trader);
            ModModCpSysDib.ModCpSysDib.ModCpSvr7254.Request();
        }
        /// <summary>
        /// 
        /// </summary>
        public void CpSvr7254_OnReceived()
        {
            _clsDefineDataTable.SetDtCpSvr7254(_dt);
            DataRow dr;
            int cnt;
            string stockCode;
            stockCode =  ModModCpSysDib.ModCpSysDib.ModCpSvr7254.GetHeaderValue(0).ToString();
            cnt = (int)ModModCpSysDib.ModCpSysDib.ModCpSvr7254.GetHeaderValue(1);

            for (int i = 1; i < cnt; i++)
            {
                dr = _dt.NewRow();
                dr["일자"] = ModCpSysDib.ModCpSvr7254.GetDataValue((int)clsGetValueDataType.indexGetCpSvr7254.Date, i);
                dr["개인"] = ModCpSysDib.ModCpSvr7254.GetDataValue((int)clsGetValueDataType.indexGetCpSvr7254.Gain, i);
                dr["외국인"] = ModCpSysDib.ModCpSvr7254.GetDataValue((int)clsGetValueDataType.indexGetCpSvr7254.Fore, i);
                dr["기관계"] = ModCpSysDib.ModCpSvr7254.GetDataValue((int)clsGetValueDataType.indexGetCpSvr7254.Gigan, i);
                dr["금융투자"] = ModCpSysDib.ModCpSvr7254.GetDataValue((int)clsGetValueDataType.indexGetCpSvr7254.Gumy, i);
                dr["보험"] = ModCpSysDib.ModCpSvr7254.GetDataValue((int)clsGetValueDataType.indexGetCpSvr7254.Bohum, i);
                dr["투신"] = ModCpSysDib.ModCpSvr7254.GetDataValue((int)clsGetValueDataType.indexGetCpSvr7254.Tosin, i);
                dr["은행"] = ModCpSysDib.ModCpSvr7254.GetDataValue((int)clsGetValueDataType.indexGetCpSvr7254.Bank, i);
                dr["기타금융"] = ModCpSysDib.ModCpSvr7254.GetDataValue((int)clsGetValueDataType.indexGetCpSvr7254.Gita, i);
                dr["연기금"] = ModCpSysDib.ModCpSvr7254.GetDataValue((int)clsGetValueDataType.indexGetCpSvr7254.Yeongi, i);
                dr["기타법인"] = ModCpSysDib.ModCpSvr7254.GetDataValue((int)clsGetValueDataType.indexGetCpSvr7254.Bubin, i);
                dr["기타외인"] = ModCpSysDib.ModCpSvr7254.GetDataValue((int)clsGetValueDataType.indexGetCpSvr7254.IoFore, i);
                dr["사모펀드"] = ModCpSysDib.ModCpSvr7254.GetDataValue((int)clsGetValueDataType.indexGetCpSvr7254.Samo, i);
                dr["국가지자체"] = ModCpSysDib.ModCpSvr7254.GetDataValue((int)clsGetValueDataType.indexGetCpSvr7254.Nation, i);

                _dt.Rows.Add(dr);

            }
                        
        }

        public DataSet CpSvr7254_DsOnReceived()
        {
            DataSet ds = new DataSet();
            return ds;
        }

        
    }
}
