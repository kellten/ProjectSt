using System;
using System.Data;
using System.Threading.Tasks;
using Woom.DataAccess.OptCaller.InterFace;
using Woom.DataDefine.OptData;
using static Woom.DataAccess.PlugIn.ClsAxKH;
using static Woom.DataAccess.OptCaller.Class.ClsOptCallerMain;

namespace Woom.DataAccess.OptCaller.Class
{
    public class ClsOpt10081 : IDisposable, IOptCaller
    {
        #region IOptCaller 구현

        private const string ConScreenNoFooter = "81";
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
                foreach (int i in Enum.GetValues(typeof(Column10081Index)))
                {
                    int j = 0;
                    j = (int)Enum.Parse(typeof(ClsColumnSets.ColumnNameIndex), Enum.GetName(typeof(Column10081Index), i));
                    // _dt.Columns.Add(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)i));
                    _dt.Columns.Add(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)j));
                }
            };
        }

        #endregion IOptCaller 구현

        #region Enum

        private enum Column10081Index
        {
            종목코드, 현재가, 거래량, 거래대금, 일자, 시가, 고가, 저가,
            수정주가구분, 수정비율, 대업종구분, 소업종구분, 종목정보,
            수정주가이벤트
            //, 전일종가
        }

        #endregion Enum

        #region 전역변수

        private DataTable _dt = new DataTable();
        // private ClsOptStatus _OptStatus;
        private string _stdDate = "";
        private string _stockCode = "";
        private string _stockName = "";
        private string _modifyJugaGb = "";
        private object lockObject = new object();

        #endregion 전역변수

        #region Const

        private const string RqName = "주식일봉차트조회";
        private const string OptName = "OPT10081";

        #endregion Const

        #region Event

        public delegate void OnReceivedEventHandler(string stockCode, DataTable dt, int sPreNext);

        public event OnReceivedEventHandler Opt10081_OnReceived;

        #endregion Event

        /// <summary>
        /// SetValue
        /// </summary>
        /// <param name="StockCode">종목코드</param>
        /// <param name="StartDate">일자</param>
        /// <param name="ModifyJugaGb"> 수정주가구분 : 0 or 1, 수신데이터 1:유상증자, 2:무상증자, 4:배당락, 8:액면분할, 16:액면병합, 32:기업합병, 64:감자, 256:권리락</param>
        public bool SetValue(string StockCode, string StockName, string StdDate, string ModifyJugaGb)
        {
            //if (_OptStatus.OptCallingStatus(true) == false)
            //{
            //    return false;
            //}

            //_OptStatus.OptCalling = OptName + "(" + RqName + ")";

            _stdDate = StdDate;
            _stockCode = StockCode;
            _stockName = StockName;
            _modifyJugaGb = ModifyJugaGb;

            return true;
        }

        public async void Opt10081(bool nextCall = false)
        {
            lock (lockObject)
            {
                AxKH.SetInputValue("종목코드", _stockCode);
                AxKH.SetInputValue("기준일자", _stdDate);
                AxKH.SetInputValue("수정주가구분", _modifyJugaGb);
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

            await OptCommRqData(RqName, OptName, 0, _screenNo);
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

            await OptCommRqData(RqName, OptName, 2, _screenNo);
            await tcs.Task;
        }

        private void AxKH_OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            if (e.sScrNo != _screenNo || e.sRQName != RqName)
            {
                return;
            }

            MakeDataTable();

            var handler = Opt10081_OnReceived;

            int nCnt = AxKH.GetRepeatCnt(e.sTrCode, e.sRQName);

            if (nCnt == 0)
            {
                if (handler != null)
                {
                    //_OptStatus.InitOptCallingStatus();
                    Opt10081_OnReceived(_stockCode, null, 0);
                    return;
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
                Opt10081_OnReceived(_stockCode, _dt, Convert.ToInt32(e.sPrevNext));
                return;
            }
        }

        #region IDispose 구현

        private void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
          //  _OptStatus.InitOptCallingStatus();
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDispose 구현
    }
}