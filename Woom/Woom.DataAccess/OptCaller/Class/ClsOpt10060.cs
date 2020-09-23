using System;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using Woom.DataAccess.OptCaller.InterFace;
using Woom.DataDefine.OptData;
using static Woom.DataAccess.OptCaller.Class.ClsOptCallerMain;
using static Woom.DataAccess.PlugIn.ClsAxKH;

namespace Woom.DataAccess.OptCaller.Class
{
    public class ClsOpt10060 : IDisposable, IOptCaller
    {
        #region IOptCaller 구현

        private const string ConScreenNoFooter = "60";
        public string ScreenNoFooter { get { return ConScreenNoFooter; } }
        private string _screenNo = "";



        public void SetInit(string FormId)
        {
            _screenNo = FormId + ConScreenNoFooter;
        }

        public void MakeDataTable()
        {
            if (_dt != null)
            {
                _dt = null;
                _dt = new DataTable();
            }

            using (ClsColumnSets oBasicDataType = new ClsColumnSets())
            {
                foreach (int i in Enum.GetValues(typeof(Column10060Index)))
                {
                    int j = 0;
                    j = (int)Enum.Parse(typeof(ClsColumnSets.ColumnNameIndex), Enum.GetName(typeof(Column10060Index), i));

                    if (_dt.Columns.Contains(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)j).ToString()) != true)
                    {
                        _dt.Columns.Add(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)j));
                    }
                }
            };
        }

        #endregion IOptCaller 구현

        #region Event

        public delegate void OnReceivedEventHandler(string stockCode, DataTable dt, int sPreNext);

        public event OnReceivedEventHandler Opt10060_OnReceived;

        #endregion Event

        #region Enum

        private enum Column10060Index
        {
            일자, 현재가, 전일대비, 누적거래대금, 개인투자자, 외국인투자자, 기관계, 금융투자, 보험, 투신, 기타금융, 은행,
            연기금등, 사모펀드, 국가, 기타법인, 내외국인
        }

        #endregion Enum

        #region Const

        private const string RqName = "종목별투자자기관별차트요청";
        private const string OptName = "Opt10060";

        #endregion Const

        #region 전역변수

        private DataTable _dt = new DataTable();

        private string _startDate = "";
        private string _stockCode = "";
        private string _stockName = "";
        private string _amountQtyGb = "";
        private string _maeMaeGb = "";
        private string _unitGb = "";

        //private object lockObject = new object();

        #endregion 전역변수

        /// <summary>
        /// SetValue
        /// </summary>
        /// <param name="StartDate">일자</param>
        /// <param name="StockCode">종목코드</param>
        /// <param name="StockName">종목명</param>
        /// <param name="AmountQtyGb">금액수량구분 = 1:금액, 2:수량</param>
        /// <param name="MaeMaeGb">매매구분 = 0:순매수, 1:매수, 2:매도</param>
        /// <param name="UnitGb">단위구분 = 1000:천주, 1:단주</param>
        public bool SetValue(string StartDate, string StockCode, string StockName, string AmountQtyGb, string MaeMaeGb, string UnitGb)
        {
            //if (_OptStatus.OptCallingStatus(true) == false)
            //{
            //    return false;
            //}

            //_OptStatus.OptCalling = OptName + "(" + RqName + ")";

            _startDate = StartDate;
            _stockCode = StockCode;
            _stockName = StockName;
            _amountQtyGb = AmountQtyGb;
            _maeMaeGb = MaeMaeGb;
            _unitGb = UnitGb;

            return true;
        }

        public async void Opt10060(bool nextCall = false)
        {
            ArrayList SetInputValue = new ArrayList();

            SetInputValue.Add(_startDate);
            SetInputValue.Add(_stockCode);
            SetInputValue.Add(_amountQtyGb);
            SetInputValue.Add(_maeMaeGb);
            SetInputValue.Add(_unitGb);

            if (nextCall == false)
            {
                await JustRequest(SetInputValue);
            }
            else
            {
                await ReJustRequest(SetInputValue);
            }
        }

        private async Task JustRequest(ArrayList arrayL)
        {
            //TaskCompletionSource<bool> tcs = null;
            //tcs = new TaskCompletionSource<bool>();

            //AxKH.OnReceiveTrData += (object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e) =>
            //{
            //    if (tcs == null || tcs.Task.IsCompleted)
            //    { return; }
            //    AxKH_OnReceiveTrData(sender, e);
            //    tcs.SetResult(true);
            //};

            //// AxKH.CommRqData(RqName, OptName, 0, _screenNo);
            await OptCommRqData( OptType.Opt10060, arrayL, RqName, OptName, 0, _screenNo);
            //await tcs.Task;
            //AxKH.OnReceiveTrData -= AxKH_OnReceiveTrData;

        }

        private async Task ReJustRequest(ArrayList arrayL)
        {
            // await Task.Delay(TimeSpan.FromSeconds(4));

            //TaskCompletionSource<bool> tcs = null;
            //tcs = new TaskCompletionSource<bool>();

            //AxKH.OnReceiveTrData += (object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e) =>
            //{
            //    if (tcs == null || tcs.Task.IsCompleted)
            //    { return; }
            //    AxKH_OnReceiveTrData(sender, e);
            //    tcs.SetResult(true);
            //};

            // AxKH.CommRqData(RqName, OptName, 2, _screenNo);
            await OptCommRqData( OptType.Opt10060, arrayL, RqName, OptName, 2, _screenNo);
            //await tcs.Task;
            //AxKH.OnReceiveTrData -= AxKH_OnReceiveTrData;
            //OptCommRqData(RqName, OptName, 2, _screenNo);
        }

        private void AxKH_OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            if (e.sScrNo != _screenNo || e.sRQName != RqName)
            {
                return;
            }

            //Task<string> t = Task.Run(() =>
            //{
            //    MakeDataTable();

            //    var handler = Opt10060_OnReceived;

            //    int nCnt = AxKH.GetRepeatCnt(e.sTrCode, e.sRQName);

            //    if (nCnt == 0)
            //    {
            //        if (handler != null)
            //        {
            //            // _OptStatus.InitOptCallingStatus();
            //            Opt10060_OnReceived(_stockCode, null, 0);
            //        }
            //    }

            //    for (int i = 0; i < nCnt; i++)
            //    {
            //        DataRow dr = _dt.NewRow();
            //        for (int intColumName = 0; intColumName < _dt.Columns.Count; intColumName++)
            //        {
            //            var type = _dt.Columns[intColumName].DataType;
            //            dr[_dt.Columns[intColumName].ColumnName.ToString()] = Convert.ChangeType(AxKH.GetCommData(e.sTrCode, e.sRQName, i, _dt.Columns[intColumName].ColumnName.ToString()).ToString().Trim(), type);
            //        }

            //        _dt.Rows.Add(dr);
            //    }

            //    if (handler != null)
            //    {
            //        if (Convert.ToInt32(e.sPrevNext) != 2)
            //        {
            //            //  _OptStatus.InitOptCallingStatus();
            //        }
            //        Opt10060_OnReceived(_stockCode, _dt, Convert.ToInt32(e.sPrevNext));
            //    }

            //    return e.sPrevNext;
            //});

            //Task cwt = t.ContinueWith(task =>
            //{
            //    if (t.Result != "2")
            //    {
            //        //_OptStatus.InitOptCallingStatus();
            //    }

            //    Opt10060_OnReceived(_stockCode, _dt, Convert.ToInt32(e.sPrevNext));
            //});

            MakeDataTable();

            var handler = Opt10060_OnReceived;

            int nCnt = AxKH.GetRepeatCnt(e.sTrCode, e.sRQName);

            if (nCnt == 0)
            {
                if (handler != null)
                {
                    //_OptStatus.InitOptCallingStatus();
                    Opt10060_OnReceived(_stockCode, null, 0);
                }
            }

            for (int i = 0; i < nCnt; i++)
            {
                DataRow dr = _dt.NewRow();
                for (int intColumName = 0; intColumName < _dt.Columns.Count; intColumName++)
                {
                    var type = _dt.Columns[intColumName].DataType;
                    dr[_dt.Columns[intColumName].ColumnName.ToString()] = Convert.ChangeType(AxKH.GetCommData(e.sTrCode, e.sRQName, i, _dt.Columns[intColumName].ColumnName.ToString()).ToString().Trim(), type);
                }

                _dt.Rows.Add(dr);
            }

            if (handler != null)
            {
                if (Convert.ToInt32(e.sPrevNext) != 2)
                {
                    //_OptStatus.InitOptCallingStatus();
                }
                Opt10060_OnReceived(_stockCode, _dt, Convert.ToInt32(e.sPrevNext));
            }
        }

        #region IDispose 구현

        private void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            //_OptStatus.InitOptCallingStatus();
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDispose 구현
    }
}