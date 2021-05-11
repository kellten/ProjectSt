using System;
using System.Data;
using System.Threading.Tasks;
using Woom.DataAccess.OptCaller.InterFace;
using Woom.DataDefine.OptData;
using static Woom.DataAccess.PlugIn.ClsAxKH;
using static Woom.DataAccess.OptCaller.Class.ClsOptCallerMain;

namespace Woom.DataAccess.OptCaller.Class
{
    public class ClsOpt10059 : IDisposable, IOptCaller
    {
        #region IOptCaller 구현
        private const string ConScreenNoFooter = "59";
        public string ScreenNoFooter { get { return ConScreenNoFooter; } }
        private string _screenNo = "";

        public void SetInit(string FormId)
        {
            _screenNo = FormId + ConScreenNoFooter;
            //_OptStatus = ClsOptStatus.Instance();

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
                foreach (int i in Enum.GetValues(typeof(ClsColumnSets.Column10059Index)))
                {
                    _dt.Columns.Add(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)i));
                }
            };
        }
        #endregion

        #region Event
        public delegate void OnReceivedEventHandler(string stockCode, DataTable dt, int sPreNext);
        public event OnReceivedEventHandler Opt10059_OnReceived;
        #endregion

       

        #region Const
        private const string RqName = "종목별투자자기관별요청";
        private const string OptName = "Opt10059";
        #endregion

        #region 전역변수
        //private ClsOptStatus _OptStatus;
        private DataTable _dt = new DataTable();

        private string _startDate = "";
        private string _stockCode = "";
        private string _stockName = "";
        private string _amountQtyGb = "";
        private string _maeMaeGb = "";
        private string _unitGb = "";

        private object lockObject = new object();
        #endregion

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

        public async void Opt10059(bool nextCall = false)
        {

            lock (lockObject)
            {
                AxKH.SetInputValue("일자", _startDate);
                AxKH.SetInputValue("종목코드", _stockCode);
                AxKH.SetInputValue("금액수량구분", _amountQtyGb);
                AxKH.SetInputValue("매매구분", _maeMaeGb);
                AxKH.SetInputValue("단위구분", _unitGb);
            }

            if (nextCall == false)
            {
                await JustRequest();
            }
            else
            {
                await ReJustRequest();
            }

        }

        private async Task JustRequest()
        {

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            AxKH.OnReceiveTrData += (object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e) =>
            {
                if (tcs == null || tcs.Task.IsCompleted)
                { return; }
                AxKH_OnReceiveTrData(sender, e);
                tcs.SetResult(true);
            };

            //AxKH.CommRqData(RqName, OptName, 0, _screenNo);
           //  OptCommRqData(RqName, OptName, 0, _screenNo);

            await tcs.Task;

        }

        private async Task ReJustRequest()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            AxKH.OnReceiveTrData += (object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e) =>
            {
                if (tcs == null || tcs.Task.IsCompleted)
                { return; }
                AxKH_OnReceiveTrData(sender, e);
                tcs.SetResult(true);
            };

            //AxKH.CommRqData(RqName, OptName, 2, _screenNo);
         //    OptCommRqData(RqName, OptName, 2, _screenNo);

            await tcs.Task;

        }

        private void AxKH_OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            if (e.sScrNo != _screenNo || e.sRQName != RqName)
            {
                return;
            }

            MakeDataTable();

            var handler = Opt10059_OnReceived;

            int nCnt = AxKH.GetRepeatCnt(e.sTrCode, e.sRQName);

            if (nCnt == 0)
            {
                if (handler != null)
                {
                    //_OptStatus.InitOptCallingStatus();
                    Opt10059_OnReceived(_stockCode, null, 0);
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
                   // _OptStatus.InitOptCallingStatus();
                }
                Opt10059_OnReceived(_stockCode, _dt, Convert.ToInt32(e.sPrevNext));
            }
        }

        #region IDispose 구현
        private void Dispose(bool disposing)
        {

        }
        public void Dispose()
        {
         //   _OptStatus.InitOptCallingStatus();
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
