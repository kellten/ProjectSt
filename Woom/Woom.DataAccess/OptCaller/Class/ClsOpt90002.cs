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
    public class ClsOpt90002 : IDisposable, IOptCaller
    {
        #region IOptCaller 구현
        private const string ConScreenNoFooter = "02";
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
                foreach (int i in Enum.GetValues(typeof(ClsColumnSets.Column90002Index)))
                {
                    int j = 0;
                    j = (int)Enum.Parse(typeof(ClsColumnSets.ColumnNameIndex), Enum.GetName(typeof(ClsColumnSets.Column90002Index), i));

                    if (_dt.Columns.Contains(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)j).ToString()) != true)
                    {
                        _dt.Columns.Add(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)j));
                    }
                }
            };
        }
        #endregion


        #region Const

        private const string RqName = "테마그룹별요청";
        private const string OptName = "opt90002";

        #endregion Const

        #region 전역변수

        private DataTable _dt = new DataTable();

        private string _stockCode = "";

        //private object lockObject = new object();

        #endregion 전역변수

        public void JustRequest(string dateGb, string kthCode, int nPrevNext)
        {

            ArrayList SetInputValue = new ArrayList();

            SetInputValue.Add(dateGb);
            SetInputValue.Add(kthCode);

            SendCommRqData(PlugIn.ClsAxKH.OptType.Opt90002, SetInputValue, RqName, OptName, nPrevNext, _screenNo);
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
