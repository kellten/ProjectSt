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
    public class ClsOpt20068 : IDisposable, IOptCaller
    {
        #region IOptCaller 구현
        private const string ConScreenNoFooter = "68";
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
                foreach (int i in Enum.GetValues(typeof(ClsColumnSets.Column10060Index)))
                {
                    int j = 0;
                    j = (int)Enum.Parse(typeof(ClsColumnSets.ColumnNameIndex), Enum.GetName(typeof(ClsColumnSets.Column10060Index), i));

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
        public event OnReceivedEventHandler Opt20068_OnReceived;

        #endregion Event

        #region Const

        private const string RqName = "대차거래추이요청(종목별)";
        private const string OptName = "Opt20068";

        #endregion Const

        #region 전역변수

        private DataTable _dt = new DataTable();

        private string _startDate = "";
        private string _stockCode = "";
        private string _stockName = "";

        //private object lockObject = new object();

        #endregion 전역변수


        public void JustRequest(string startDate, string endDate, string allGb, string stockCode, int nPrevNext)
        {

            ArrayList SetInputValue = new ArrayList();

            SetInputValue.Add(startDate);
            SetInputValue.Add(endDate);
            SetInputValue.Add(allGb);
            SetInputValue.Add(stockCode);

            SendCommRqData(PlugIn.ClsAxKH.OptType.Opt20068, SetInputValue, RqName, OptName, nPrevNext, _screenNo);
        }

        private void AxKH_OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            if (e.sScrNo != _screenNo || e.sRQName != RqName)
            {
                return;
            }

            MakeDataTable();

            var handler = Opt20068_OnReceived;

            int nCnt = AxKH.GetRepeatCnt(e.sTrCode, e.sRQName);

            if (nCnt == 0)
            {
                if (handler != null)
                {
                    Opt20068_OnReceived(_stockCode, null, 0);
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
                Opt20068_OnReceived(_stockCode, _dt, Convert.ToInt32(e.sPrevNext));
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
