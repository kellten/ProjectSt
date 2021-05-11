using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woom.DataAccess.OptCaller.InterFace;
using Woom.DataDefine.OptData;
using Woom.DataAccess.PlugIn;
using static Woom.DataAccess.PlugIn.ClsAxKH;

namespace Woom.DataAccess.OptCaller.Class
{
    public class ClsOpt10001 : IDisposable, IOptCaller
    {
        #region IOptCaller 구현
        private const string ConScreenNoFooter = "90";
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
                foreach (int i in Enum.GetValues(typeof(Column10001Index)))
                {
                    int j = 0;
                    j = (int)Enum.Parse(typeof(ClsColumnSets.ColumnNameIndex), Enum.GetName(typeof(Column10001Index), i));

                    if (_dt.Columns.Contains(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)j).ToString()) != true)
                    {
                        _dt.Columns.Add(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)j));
                    }
                }
            };
        }
        #endregion

        #region Event

        public delegate void OnReceivedEventHandler(string stockCode, DataTable dt, int sPreNext);

        public event OnReceivedEventHandler Opt10001_OnReceived;

        #endregion Event
        #region Enum

        private enum Column10001Index
        {
           종목코드, 종목명, 결산월, 액면가, 자본금, 상장주식, 신용비율, 연중최고, 연중최저, 시가총액, 시가총액비중, 외인소진률, 대용가, PER, EPS, ROE, PBR, EV, BPS, 매출액, 영업이익, 당기순이익, 최고250,
            최저250, 시가, 고가, 저가, 상한가, 하한가, 기준가, 예상체결가, 예상체결수량, 최고가일250, 최고가대비율250, 최저가일250, 최저가대비율250, 현재가, 대비기호, 전일대비, 등락율, 거래량, 거래대비, 액면가단위
        }
        #endregion Enum

        #region Const

        private const string RqName = "주식기본정보요청";
        private const string OptName = "OPT10001";

        #endregion Const

        #region 전역변수

        private DataTable _dt = new DataTable();

        private string _stockCode = "";
    
        //private object lockObject = new object();

        #endregion 전역변수

        public void JustRequest(string StockCode, string StockName,  int nPrevNext)
        {

            ArrayList SetInputValue = new ArrayList();

            SetInputValue.Add(StockCode);

            SendCommRqData(PlugIn.ClsAxKH.OptType.Opt10001, SetInputValue, RqName, OptName, nPrevNext, _screenNo);
        }

        private void AxKH_OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            if (e.sScrNo != _screenNo || e.sRQName != RqName)
            {
                return;
            }

            MakeDataTable();

            var handler = Opt10001_OnReceived;

            int nCnt = AxKH.GetRepeatCnt(e.sTrCode, e.sRQName);

            if (nCnt == 0)
            {
                if (handler != null)
                {
                    Opt10001_OnReceived(_stockCode, null, 0);
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
                Opt10001_OnReceived(_stockCode, _dt, Convert.ToInt32(e.sPrevNext));
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
