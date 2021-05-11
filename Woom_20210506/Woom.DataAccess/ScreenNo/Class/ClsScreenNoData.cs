using System.Data;

namespace Woom.DataAccess.ScreenNo.Class
{
    public static class ClsScreenNoData
    {
        private static DataTable _dtScreeNo;
        public static DataTable DtScreenNo { get { return _dtScreeNo; } }
    }
}