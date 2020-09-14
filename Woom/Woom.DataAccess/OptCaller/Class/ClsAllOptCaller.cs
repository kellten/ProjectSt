using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woom.DataAccess.OptCaller.InterFace;
using Woom.DataDefine.OptData;

namespace Woom.DataAccess.OptCaller.Class
{
    class ClsAllOptCaller : IDisposable
    {

        private DataTable _dt = new DataTable();

        private enum Column10081Index
        {
            종목코드, 현재가, 거래량, 거래대금, 일자, 시가, 고가, 저가,
            수정주가구분, 수정비율, 대업종구분, 소업종구분, 종목정보,
            수정주가이벤트
            //, 전일종가
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
        #endregion IDispose 구현
    }
}
