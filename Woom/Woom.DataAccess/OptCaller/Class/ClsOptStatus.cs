using System.Windows.Forms;

namespace Woom.DataAccess.OptCaller.Class
{
    internal class ClsOptStatus
    {
        private static ClsOptStatus _instance = null;
        private static readonly object padlock = new object();

        private string _OptCalling;

        public string OptCalling { get { return _OptCalling; } set { _OptCalling = value; } }

        protected ClsOptStatus()
        {
        }

        public static ClsOptStatus Instance()
        {
            // 다중 쓰레드 환경일 경우 Lock 필요

            if (_instance == null)
            {
                lock (padlock)
                {
                    _instance = new ClsOptStatus();
                    _instance._OptCalling = "";
                }
            }

            return _instance;
        }

        public bool OptCallingStatus(bool CallMessage)
        {
            if (_OptCalling != "")
            {
                if (CallMessage == true)
                {
                    MessageBox.Show(_OptCalling + "조회 중 입니다.");
                    if (MessageBox.Show("조회 중인 내용을 푸시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        _OptCalling = "";
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        public void InitOptCallingStatus()
        {
            _OptCalling = "";
        }
    }
}