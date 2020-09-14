using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CybosDa.DataAccess.CpSvr
{
    public class clsCpSvr7254 : IDisposable
    {
        private CPSYSDIBLib.CpSvr7254 _cpSvr7254;
        private CybosDa.Common.Class.ClsDefineDataType.TradeGb _TradeGb = new CybosDa.Common.Class.ClsDefineDataType.TradeGb();
        private string _stockCode;
        private DataTable _dt = new DataTable();

        public enum ChoiceGiganTypeIndex
        { 사용자지정 = 0, 개월1 = 1, 개월2 = 2, 개월3 = 3, 개월6 = 4, 최근5일 = 5, 일별 = 6 }
        public enum ChoiceTradeTypeIndex
        { 순매수 = 0, 매매비중 = 1 }
        public enum ChoiceDataGbIndex
        { 순매수량 = 0, 추정금액백만원 = 1 }

        public delegate void OnReceivedEventHandler(string stockCode, DataTable dt, int NextCall);
        public event OnReceivedEventHandler CpSvr7254_OnReceived;

        public delegate void OnEndGetDataEventHandler();
        public event OnEndGetDataEventHandler CpSvr7254_OnEndGetData;

        private bool _Stop;
        public void UseCpSvr7254()
        {
            InitDataTable();
        }

        private void InitDataTable()
        {
            _TradeGb.SetCpSvr7254SetColumn(ref _dt);
        }

        public void StopGetData()
        {
            _Stop = true;
        }
        /// <summary>
        /// GetCpSvr7254 - 
        /// </summary>
        /// <param name="stockCode">(string) 종목코드</param>
        /// <param name="cgTypeIndex">(short) 기간선택구분 (0:사용자지정 1:1개월, 2:2개월,3:3개월, 4:6개월,5:최근5일,6:일별)
        ///* 0 ~ 5 : 누적 * 6 : 일자별</param>
        /// <param name="fromDate">(long)  시작일자: 기간선택구분을 0이 아닐경우 생략</param>
        /// <param name="toDate">(long)  끝일자: 기간선택구분을 0이 아닐경우 생략</param>
        /// <param name="ctTypeIndex">(char)  매매비중구분 '0' -순매수 '1' - 매매비중 </param>
        /// <param name="tgTypeIndex">(short)  투자자</param>
        /// <param name="cdGbIndex">(char)  데이터 구분</param>
        public async void GetCpSvr7254(string stockCode, ChoiceGiganTypeIndex cgTypeIndex, string fromDate, string toDate,
                                     ChoiceTradeTypeIndex ctTypeIndex, CybosDa.Common.Class.ClsDefineDataType.TradeGb.TradeGbTypeIndex tgTypeIndex,
                                     ChoiceDataGbIndex cdGbIndex)
        {   
            _cpSvr7254 = null;
            _cpSvr7254 = new CPSYSDIBLib.CpSvr7254();
            _stockCode = stockCode;
            _cpSvr7254.SetInputValue(0, stockCode);
            _cpSvr7254.SetInputValue(1, ChoiceGiganType(cgTypeIndex).ToString());
            _cpSvr7254.SetInputValue(2, Convert.ToInt64(fromDate));
            _cpSvr7254.SetInputValue(3, Convert.ToInt64(toDate));
            _cpSvr7254.SetInputValue(4, ChoiceTradeType(ctTypeIndex));
            _cpSvr7254.SetInputValue(5, TradeGbType(tgTypeIndex));
            _cpSvr7254.SetInputValue(6, ChoiceDataGb(cdGbIndex));
            
            await JustRequest(); 

        }

        public async void NextCaller()
        {
            await ReJustRequest();
        }
        
        #region DataConvet
        private short ChoiceGiganType(ChoiceGiganTypeIndex ix)
        {
            switch (ix)
            {
                case ChoiceGiganTypeIndex.사용자지정:
                    return 0;
                case ChoiceGiganTypeIndex.개월1:
                    return 1;
                case ChoiceGiganTypeIndex.개월2:
                    return 2;
                case ChoiceGiganTypeIndex.개월3:
                    return 3;
                case ChoiceGiganTypeIndex.개월6:
                    return 4;
                case ChoiceGiganTypeIndex.최근5일:
                    return 5;
                case ChoiceGiganTypeIndex.일별:
                    return 6;
                default:
                    return 0;
            }
        }

        private char ChoiceTradeType(ChoiceTradeTypeIndex ix)
        {
            switch (ix)
            {
                case ChoiceTradeTypeIndex.순매수:
                    return '0';
                case ChoiceTradeTypeIndex.매매비중:
                    return '1';
            }

            return '-';
        }

        private short TradeGbType(CybosDa.Common.Class.ClsDefineDataType.TradeGb.TradeGbTypeIndex ix)
        {
            switch (ix)
            {
                case Common.Class.ClsDefineDataType.TradeGb.TradeGbTypeIndex.전체:
                    return 0;
                case Common.Class.ClsDefineDataType.TradeGb.TradeGbTypeIndex.개인:
                    return 1;
                case Common.Class.ClsDefineDataType.TradeGb.TradeGbTypeIndex.외국인:
                    return 2;
                case Common.Class.ClsDefineDataType.TradeGb.TradeGbTypeIndex.기관계:
                    return 3;
                case Common.Class.ClsDefineDataType.TradeGb.TradeGbTypeIndex.금융투자:
                    return 4;
                case Common.Class.ClsDefineDataType.TradeGb.TradeGbTypeIndex.보험:
                    return 5;
                case Common.Class.ClsDefineDataType.TradeGb.TradeGbTypeIndex.투신:
                    return 6;
                case Common.Class.ClsDefineDataType.TradeGb.TradeGbTypeIndex.은행:
                    return 7;
                case Common.Class.ClsDefineDataType.TradeGb.TradeGbTypeIndex.기타금융:
                    return 8;
                case Common.Class.ClsDefineDataType.TradeGb.TradeGbTypeIndex.연기금:
                    return 9;
                case Common.Class.ClsDefineDataType.TradeGb.TradeGbTypeIndex.국가지자체:
                    return 10;
                case Common.Class.ClsDefineDataType.TradeGb.TradeGbTypeIndex.기타외인:
                    return 11;
                case Common.Class.ClsDefineDataType.TradeGb.TradeGbTypeIndex.사모펀드:
                    return 12;
                case Common.Class.ClsDefineDataType.TradeGb.TradeGbTypeIndex.기타법인:
                    return 13;
                default:
                    return 0;
            }
        }

        private char ChoiceDataGb(ChoiceDataGbIndex ix)
        {
            switch (ix)
            {
                case ChoiceDataGbIndex.순매수량:
                    return '1';
                case ChoiceDataGbIndex.추정금액백만원:
                    return '2';
                default:
                    return '1';
            }
        }
        #endregion

        private async Task JustRequest()
        {
            if (_cpSvr7254.GetDibStatus() == 1)
            { Trace.TraceInformation("DibRq 요청 수신대기 중 입니다. 수신이 완료된 후 다시 호출 하십시오.");
                return;
            }

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            _cpSvr7254.Received += () =>
            {
                if (tcs == null || tcs.Task.IsCompleted)
                { return; }
                OnReceived();
                tcs.SetResult(true);
            };
            CallBlockRequest();
            await tcs.Task;

            tcs.Task.Dispose();
            
        }

        private async Task ReJustRequest()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            _cpSvr7254.Received += () =>
            {
                if (tcs == null || tcs.Task.IsCompleted)
                { return; }
                OnReceived();
                tcs.SetResult(true);
            };
            CallBlockRequest();
            await tcs.Task;
            tcs.Task.Dispose();
        }

        private void CallBlockRequest()
        {
            int result = -1;
            result = _cpSvr7254.BlockRequest();
            
            if (result == 0)
            {

            }
        }

        private void OnReceived()
        {
            _dt.Rows.Clear();
            
            for (int i = 0; i <= _cpSvr7254.GetHeaderValue(1) - 1; i++)
            {
                dynamic fieldCount = _cpSvr7254.Data;
                int count = Convert.ToInt32(fieldCount.Count);

                DataRow dr;

                dr = _dt.NewRow();

                //for (int j = 0; j < count - 1; j++)
                for (int j = 0; j < count; j++)
                {
                    if (j == 15)
                    { break; }

                    if (j == 0)
                    { dr[j] = _cpSvr7254.GetDataValue(j, i); }
                    else
                    { dr[j] = _cpSvr7254.GetDataValue(j, i); }

                }

                _dt.Rows.Add(dr);
            }
                        
                var handler = CpSvr7254_OnReceived;
                if (handler != null)
                { CpSvr7254_OnReceived(_stockCode, _dt, _cpSvr7254.Continue); }
            
        }
        #region IDispose 구현
        private void Dispose(bool disposing)
        {

        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }

}