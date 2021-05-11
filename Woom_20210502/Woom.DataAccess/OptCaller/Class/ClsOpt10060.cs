using System;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using Woom.DataAccess.OptCaller.InterFace;
using Woom.DataDefine.OptData;
using Woom.DataAccess.PlugIn;
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
        /// 
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="StockCode"></param>
        /// <param name="StockName"></param>
        /// <param name="AmountQtyGb"></param>
        /// <param name="MaeMaeGb"></param>
        /// <param name="UnitGb"></param>
        public void JustRequest(string StartDate, string StockCode, string StockName, string AmountQtyGb, string MaeMaeGb, string UnitG,int nPrevNext)
        {

            ArrayList SetInputValue = new ArrayList();

            SetInputValue.Add(StartDate);
            SetInputValue.Add(StockCode);
            SetInputValue.Add(AmountQtyGb);
            SetInputValue.Add(MaeMaeGb);
            SetInputValue.Add(UnitG);

            SendCommRqData(PlugIn.ClsAxKH.OptType.Opt10060 , SetInputValue, RqName, OptName, nPrevNext, _screenNo);
            //SendCommRqData(PlugIn.ClsAxKH.OptType.Opt10060, SetInputValue, RqName, StockCode, nPrevNext, _screenNo);
        }

        private void AxKH_OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            if (e.sScrNo != _screenNo || e.sRQName != RqName)
            {
                return;
            }

            MakeDataTable();

            var handler = Opt10060_OnReceived;

            int nCnt = AxKH.GetRepeatCnt(e.sTrCode, e.sRQName);

            if (nCnt == 0)
            {
                if (handler != null)
                {
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