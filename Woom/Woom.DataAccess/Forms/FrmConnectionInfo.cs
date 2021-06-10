using System;
using System.Windows.Forms;
using static Woom.DataAccess.ErrorManage.Class.ClsWoomErrorCode;
using static Woom.DataAccess.PlugIn.ClsAxKH;

namespace Woom.DataAccess.Forms
{
    public partial class FrmConnectionInfo : Form, IDisposable
    {
        public FrmConnectionInfo()
        {
            InitializeComponent();
            LoadInit();
        }

        #region "전역변수"
        private bool _loginStatus = false;
        #endregion

        #region Connection

        #region Func
        private void LoadInit()
        {
            _loginStatus = false;
            btnDisconnect.Enabled = false;
            AxKH = ucMain0.AxKHApi;
            RegEventMethod();
            Connection();
            AddOnReceivedEventHandler();
            AddOnReceiveRealDataEventHandler();
            //SetTimer();
        }
        private void RegEventMethod()
        {
            AxKH.OnEventConnect += AxKH_OnEventConnection;
        }
        private void Connection()
        {
            AxKH.CommConnect();
            
        }
        private void DisConnection()
        {


        }
        #endregion

        #region AxKH Event
        private void AxKH_OnEventConnection(Object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            string str = AxKH.GetLoginInfo("ACCNO");

            if (e.nErrCode == 0)
            {
                _loginStatus = true;
                lblLoginStatus.Text = "접속 중....";
                lblMsgTitle.Text = "접속정보";
                lblMsg.Text = "로그인 성공";
                btnDisconnect.Enabled = true;
            }
            else
            {
                _loginStatus = false;
                lblLoginStatus.Text = "오프라인....";
                lblMsgTitle.Text = "접속정보";
                GetErrorMessage(e.nErrCode);
                lblMsg.Text = GetErrorMessage();
                btnDisconnect.Enabled = false;
            }

        }
        #endregion

        #endregion

        #region Control Event
        private void notifyIcon0_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }
        private void btnTray_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            Connection();
        }
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            DisConnection();
        }
        #endregion

    }
}
