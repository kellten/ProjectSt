using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaikRichStock.Common;
namespace Finance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            DataRow dr;
            if (e.KeyCode == Keys.Enter)
            {
                mySqlDbConn comm = new mySqlDbConn();
                DataSet ds = comm.GetDataTableCommndText("select * from stock_finance where stock_name = '" + textBox1.Text.Trim() + "'");
                comm.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    ucFinance1.StockCodeNotKw = dr["STOCK_CODE"].ToString();
                }
            }
        }
    }
}
