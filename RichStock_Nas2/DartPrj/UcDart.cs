using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace DartPrj
{

    public partial class UcDart : UserControl
    {

        private dartValue _dartValue;
        public delegate void OnChangeDartValue(dartValue dartV);

        public event OnChangeDartValue OnChangeDartV;
                
        public dartValue propDartValue
        {
            get
            {
                return _dartValue;
            }
            set
            {
                _dartValue = value;
               
            }
        }

        public struct dartValue {
            public string creator, title, link, time;

            public dartValue(string p1, string p2, string p3, string p4)
            {
                creator = p1;
                title = p2;
                link = p3;
                time = p4;
            }

        }


        public UcDart()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetGong();
        }

        private void GetGong()
        {
            timer1.Enabled = false;

            //Form1 frm;

            WebClient wc = new WebClient();
            wc.Encoding = System.Text.UTF8Encoding.UTF8;

            String buffer = wc.DownloadString("http://dart.fss.or.kr/api/todayRSS.xml");

            wc.Dispose();
            StringReader sr = new StringReader(buffer);
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(sr);
            sr.Close();
            //XmlNodeList forecastNodes = doc.SelectNodes("xml_api_reply/news/news_entry");
            System.Xml.XmlNodeList forecastNodes = doc.SelectNodes("rss/channel/item");
            foreach (System.Xml.XmlNode node in forecastNodes)
            {
                if (node["title"].InnerText.IndexOf("(기타)") > -1 || node["title"].InnerText.IndexOf("(코넥스)") > -1) continue;

                if (datagridview2.Rows.Count > 1)
                {
                    Boolean blnTrue = false;

                    for (int i = 0; i < datagridview2.Rows.Count; i++)
                    {
                        if (i + 1 == datagridview2.Rows.Count)
                        {
                            break;
                        }

                        //if (datagridview2.Rows[i].Cells[1].Value.ToString() == _htmlSource2.Substring(idxNews + 11, idxLast - idxNews - 11))
                        if (datagridview2.Rows[i].Cells[2].Value.ToString() == node["title"].InnerText)
                        {
                            blnTrue = true;
                            break;
                        }
                    }
                    if (blnTrue == false)
                    {
                        datagridview2.Rows.Insert(0, 1);

                        datagridview2.Rows[0].Cells[0].Value = DateTime.Now;
                        //datagridview2.Rows[0].Cells[1].Value = _htmlSource2.Substring(idxName + 15, idxNameLast - idxName - 16);
                        datagridview2.Rows[0].Cells[1].Value = node["dc:creator"].InnerText;
                        //datagridview2.Rows[0].Cells[1].Value = _htmlSource2.Substring(idxNews + 11, idxLast - idxNews - 11);
                        datagridview2.Rows[0].Cells[2].Value = node["title"].InnerText;
                        datagridview2.Rows[0].Cells[3].Value = node["link"].InnerText;
                        
                        //if (node["title"].InnerText.IndexOf("유상증자") > 0)
                        //{


                        //    LoadUrl2(node["link"].InnerText);

                        //    try
                        //    {

                        //        System.Threading.Thread t1 = new System.Threading.Thread(new System.Threading.ThreadStart(Run));
                        //        t1.Start();

                        //        ////if (_htmlSource4.IndexOf("3자배정") > 0)
                        //        ////{
                        //        ////    datagridview2.Rows[0].Cells[2].Value = node["title"].InnerText + "(3자배정)";
                        //        ////}
                        //    }
                        //    catch
                        //    {
                        //    }
                        //}

                        //foreach (string str in dart)
                        //{
                        //    if (datagridview2.Rows[0].Cells[2].Value.ToString().IndexOf(str) > -1)
                        //    {
                        //        string oStr = "";
                        //        oStr = datagridview2.Rows[0].Cells[2].Value.ToString() + " " + datagridview2.Rows[0].Cells[3].Value.ToString();
                        //        frm = new Form1(oStr, datagridview2.Rows[0].Cells[1].Value.ToString());
                        //        frm.Show();
                        //        frm.TopMost = true;
                        //        System.Windows.Forms.Clipboard.SetText(datagridview2.Rows[0].Cells[1].Value.ToString().Trim());
                        //        //break;
                        //        break;
                        //    }
                        //}

                        _dartValue.creator = datagridview2.Rows[0].Cells[1].Value.ToString();
                        _dartValue.title = datagridview2.Rows[0].Cells[2].Value.ToString();
                        _dartValue.link = datagridview2.Rows[0].Cells[3].Value.ToString();
                        _dartValue.time = datagridview2.Rows[0].Cells[0].Value.ToString();

                        OnChangeDartV(_dartValue);
                    }
                }
                else
                {
                    datagridview2.Rows.Insert(0, 1);


                    datagridview2.Rows[0].Cells[0].Value = DateTime.Now;
                    //datagridview2.Rows[0].Cells[1].Value = _htmlSource2.Substring(idxName + 15, idxNameLast - idxName - 16);
                    datagridview2.Rows[0].Cells[1].Value = node["dc:creator"].InnerText;
                    //datagridview2.Rows[0].Cells[1].Value = _htmlSource2.Substring(idxNews + 11, idxLast - idxNews - 11);
                    datagridview2.Rows[0].Cells[2].Value = node["title"].InnerText;
                    datagridview2.Rows[0].Cells[3].Value = node["link"].InnerText;


                    //foreach (string str in dart)
                    //{
                    //    if (datagridview2.Rows[0].Cells[2].Value.ToString().IndexOf(str) > -1)
                    //    {
                    //        string oStr = "";
                    //        oStr = datagridview2.Rows[0].Cells[2].Value.ToString() + " " + datagridview2.Rows[0].Cells[3].Value.ToString();
                    //        frm = new Form1(oStr, datagridview2.Rows[0].Cells[1].Value.ToString());
                    //        frm.Show();
                    //        frm.TopMost = true;
                    //        System.Windows.Forms.Clipboard.SetText(datagridview2.Rows[0].Cells[1].Value.ToString().Trim());
                    //        //break;
                    //        break;
                    //    }
                    //}

                    _dartValue.creator = datagridview2.Rows[0].Cells[1].Value.ToString();
                    _dartValue.title = datagridview2.Rows[0].Cells[2].Value.ToString();
                    _dartValue.link = datagridview2.Rows[0].Cells[3].Value.ToString();

                    OnChangeDartV(_dartValue);
                }
            }

            timer1.Enabled = true;
        }

        private void UcDart_Load(object sender, EventArgs e)
        {
            if (this.DesignMode == true)
            {
                timer1.Enabled = false;
                return;
            }
            timer1.Enabled = true;
            
        }

        private void datagridview2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                System.Diagnostics.Process.Start(datagridview2.Rows[e.RowIndex].Cells[3].Value.ToString().Trim());
            }
        }

    }
}
