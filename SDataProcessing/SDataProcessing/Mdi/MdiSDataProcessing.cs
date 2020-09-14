using CybosDa.DataAccess.Connection;
using SDataAccess;
using System;
using System.Data;
using System.Windows.Forms;

namespace SDataProcessing.Mdi
{
    public partial class MdiSDataProcessing : Form
    {
        private int _childFormNumber = 0;
        private DataTable _dtCybos;
        private clsCybosConnection _cc = new clsCybosConnection();
        public MdiSDataProcessing()
        {
            InitializeComponent();
            // GetAllSotckCode();
            CheckConnectionCybosDa();
        }

        private void CheckConnectionCybosDa()
        {

            if (_cc.CybosConnection() == true)
            {
                toolStripCybosStatus.Text = "Cybos 접속 성공"; //CybosLoadStockCode(); 
            }
            else
            { toolStripCybosStatus.Text = "Cybos 접속 실패"; }
        }
        private void CybosLoadStockCode()
        {
            if (_dtCybos != null)
            {
                _dtCybos = null;
            }
            _dtCybos = new DataTable();

            _dtCybos = _cc.LoadStockCode().Copy();
        }

        private void GetAllSotckCode()
        {
            DataSet ds = new DataSet();
            RichQuery richQuery = new RichQuery();

            ds = richQuery.p_ScodeQuery("1", "", "", false);

            if (ds.Tables[0].Rows.Count < 1)
            {
                ds.Reset();
            }
            else
            {
                toolStripDbStatus.Text = "DB 접속 성공";
            }
        }
        private string _openType = "1";
        public void ShowChildForm(Form childForm)
        {
            Boolean isAlreadyContained = false;
            FormCollection fc = Application.OpenForms;
            try
            {
                foreach (Form frm in fc)
                {
                    if (frm == childForm)
                    {
                        isAlreadyContained = true;
                        frm.Activate();
                    }
                }

                if (isAlreadyContained == false)
                {
                    if (_openType == "1")
                    {
                        childForm.Show();
                    }
                    else
                    {
                        childForm.MdiParent = this;
                        childForm.Show();
                    }

                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { }
        }

        private void MenuItemConnection_Click(object sender, EventArgs e)
        {
            _openType = "1";
            Form oform = new Woom.DataAccess.Forms.FrmConnectionInfo();
            ShowChildForm(oform);
        }

        private void menuItemVolumeCollection_Click(object sender, EventArgs e)
        {
            _openType = "0";
            Form oform = new SDataProcessing.Batch.Forms.FrmVolumeCollection();
            ShowChildForm(oform);
        }

        private void menuItemVolume10060Collection_Click(object sender, EventArgs e)
        {
            _openType = "0";
            Form oform = new SDataProcessing.Batch.Forms.FrmVolume10060Collection();
            ShowChildForm(oform);
        }

        private void newVolume10060CollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _openType = "0";
            Form oform = new SDataProcessing.Batch.Forms.frmOpt10060Caller();
            ShowChildForm(oform);
        }
    }
}
