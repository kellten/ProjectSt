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
    public class ClsOpt10086 
    {
        #region IOptCaller 구현
        private const string ConScreenNoFooter = "86";
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
                foreach (int i in Enum.GetValues(typeof(ClsColumnSets.Column10086Index)))
                {
                    int j = 0;
                    j = (int)Enum.Parse(typeof(ClsColumnSets.ColumnNameIndex), Enum.GetName(typeof(ClsColumnSets.Column10086Index), i));

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

        public event OnReceivedEventHandler Opt10086_OnReceived;

        #endregion Event
    
        #region Const

        private const string RqName = "일별주가요청 ";
        private const string OptName = "opt10086";

        #endregion Const

        #region 전역변수

        private DataTable _dt = new DataTable();

        private string _stockCode = "";

        //private object lockObject = new object();

        #endregion 전역변수

        public void JustRequest(string StockCode, string StockName, int nPrevNext)
        {

            ArrayList SetInputValue = new ArrayList();

            SetInputValue.Add(StockCode);

            SendCommRqData(PlugIn.ClsAxKH.OptType.Opt10086, SetInputValue, RqName, OptName, nPrevNext, _screenNo);
        }

        private void AxKH_OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            if (e.sScrNo != _screenNo || e.sRQName != RqName)
            {
                return;
            }

            MakeDataTable();

            var handler = Opt10086_OnReceived;

            int nCnt = AxKH.GetRepeatCnt(e.sTrCode, e.sRQName);

            if (nCnt == 0)
            {
                if (handler != null)
                {
                    Opt10086_OnReceived(_stockCode, null, 0);
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
                Opt10086_OnReceived(_stockCode, _dt, Convert.ToInt32(e.sPrevNext));
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
