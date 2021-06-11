using Woom.DataAccess.ScreenNo.Interface;

namespace Woom.DataAccess.ScreenNo.Class
{
    public class ClsScreenNoManage : IScreenNo
    {
        private static ClsScreenNoManage _instance = null;
        private static readonly object padlock = new object();

        public static ClsScreenNoManage Instance()
        {
            //다중 쓰레드 환경일 경우 Lock 필요

            if (_instance == null)
            {
                lock (padlock)
                {
                    _instance = new ClsScreenNoManage();
                    _instance.MakeRealScreenNo();
                    _instance.MakeBaseScreenNo();
                }
            }

            return _instance;
        }

        private void MakeRealScreenNo()
        {
        }

        private void MakeBaseScreenNo()
        {
        }

        public string BasicGetScreenNo(string formId, string ScreenNoFooter)
        {
            return "";
        }

        public string RealGetScreenNo(string formId, string ScreenNoFooter)
        {
            return "";
        }
    }

    internal class ClsBaseScreenNoDataType
    {
        public string FormId = "";
    }

    internal class ClsRealScreenNoDataType
    {
        public string ScreenNo = "";
        public string StockCodes = "";
        public string FormId = "";
    }
}