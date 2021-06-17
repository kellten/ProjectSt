using System;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using Woom.DataAccess.OptCaller.InterFace;
using Woom.DataDefine.OptData;
using static Woom.DataAccess.OptCaller.Class.ClsOptCallerMain;
using static Woom.DataAccess.PlugIn.ClsAxKH;
using Woom.DataAccess;

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
 
        private object lockObject = new object();

        #endregion 전역변수

        #region Const

        private const string RqName = "주식일봉차트조회";
        private const string OptName = "OPT10081";

        #endregion Const

        
        /// <summary>
        /// SetValue
        /// </summary>
        /// <param name="StockCode">종목코드</param>
        /// <param name="StartDate">일자</param>
        /// <param name="ModifyJugaGb"> 수정주가구분 : 0 or 1, 수신데이터 1:유상증자, 2:무상증자, 4:배당락, 8:액면분할, 16:액면병합, 32:기업합병, 64:감자, 256:권리락</param>
        //public  void Opt10081(string StockCode, string StockName, string StdDate, string ModifyJugaGb, bool nextCall = false)
        //{
        //    ArrayList SetInputValue = new ArrayList();

        //    SetInputValue.Add(StockCode);
        //    SetInputValue.Add(StdDate);
        //    SetInputValue.Add(ModifyJugaGb);

        //    if (nextCall == false)
        //    {
        //         JustRequest(SetInputValue);
        //    }
        //    else
        //    {
        //         ReJustRequest(SetInputValue);
        //    }
        //}

        //private void JustRequest(ArrayList arrayL)
        //{
        //    SendCommRqData(OptType.Opt10081, arrayL, RqName, OptName, 0, _screenNo);
        //}

        //private void ReJustRequest(ArrayList arrayL)
        //{
        //    SendCommRqData(OptType.Opt10081, arrayL, RqName, OptName, 2, _screenNo);

        //}
        /// <summary>
        /// SetValue
        /// </summary>
        /// <param name="StockCode">종목코드</param>
        /// <param name="StartDate">일자</param>
        /// <param name="ModifyJugaGb"> 수정주가구분 : 0 or 1, 수신데이터 1:유상증자, 2:무상증자, 4:배당락, 8:액면분할, 16:액면병합, 32:기업합병, 64:감자, 256:권리락</param>
        /// <param name="nPrevNext">0 - 조회 2 - 연속조회</param>
        public void JustRequest(string StockCode, string StockName, string StdDate, string ModifyJugaGb, int nPrevNext)
        {

            ArrayList SetInputValue = new ArrayList();

            SetInputValue.Add(StockCode);
            SetInputValue.Add(StdDate);
            SetInputValue.Add(ModifyJugaGb);

            SendCommRqData(PlugIn.ClsAxKH.OptType.Opt10081, SetInputValue, RqName, OptName, nPrevNext, _screenNo);
        }

        //private void AxKH_OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        //{
        //    if (e.sScrNo != _screenNo || e.sRQName != RqName)
        //    {
        //        return;
        //    }

        //    MakeDataTable();

        //    var handler = Opt10081_OnReceived;

        //    int nCnt = AxKH.GetRepeatCnt(e.sTrCode, e.sRQName);

        //    if (nCnt == 0)
        //    {
        //        if (handler != null)
        //        {
        //            //_OptStatus.InitOptCallingStatus();
        //            Opt10081_OnReceived(_stockCode, null, 0);
        //            return;
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
        //            //_OptStatus.InitOptCallingStatus();
        //        }
        //        Opt10081_OnReceived(_stockCode, _dt, Convert.ToInt32(e.sPrevNext));
        //        return;
        //    }
        //}

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
