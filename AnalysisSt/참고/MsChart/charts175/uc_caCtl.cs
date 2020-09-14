using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace charts175
{
    public partial class uc_caCtl : UserControl
    {
        public uc_caCtl()
        {
            InitializeComponent();

            cb_refresh.Text = '\u21BB'.ToString();

        }



        public Chart chart = null;
        ChartArea CA = null;

        private void uc_caCtl_Load(object sender, EventArgs e)
        {
            cb_refresh_Click(null, null);



        }

        private void cb_refresh_Click(object sender, EventArgs e)
        {
            if (chart != null)
            {
                //ddl_CA.Items.Clear();
                //ddl_CA.Items.AddRange(chart.ChartAreas.ToArray());
                clb_cas.Items.Clear();
                clb_cas.Items.AddRange(chart.ChartAreas.ToArray());

            }
        }

        public void uc_caCtl_Layout(object sender, LayoutEventArgs e)
        {
            if (chart != null && clb_cas.Items.Count <= 0)
                cb_refresh_Click(null, null);

        }

        private void cbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbx = sender as CheckBox;
            cbx_X.Checked = cbx.Checked;
            cbx_W.Checked = cbx.Checked;

            if (cbx.Checked) CA.Position = new ElementPosition();
            else
            {

                //  if (sender == cbx_X)
                CA.Position.X = cbx_X.Checked ? float.NaN : tr_X.Value;
                //  else if (sender == cbx_W)
                CA.Position.Width = cbx_W.Checked ? float.NaN : tr_W.Value;

            }
        }

        private void tr_ValueChanged(object sender, EventArgs e)
        {
            //if (changing) return;
            if (sender == tr_X)
            {
                CA.Position.X = cbx_X.Checked ? float.NaN : tr_X.Value;
                lbl_X.Text = tr_X.Value+ " %";
            }
            else if (sender == tr_W)
            {
                CA.Position.Width = cbx_W.Checked ? float.NaN : tr_W.Value;
                lbl_W.Text = tr_W.Value+ " %";

            }


            if (CA == ((ChartArea)clb_cas.Items[0]))
            {
                List<ChartArea> seriesCAs = chart.ChartAreas.Select(x => x)
                   .Where<ChartArea>(x => !x.Name.Contains("CA_AxY_")).ToList();

                foreach (var ca in seriesCAs)
                    if (ca != CA) ca.Position = CA.Position;



            }


            chart.Refresh();
        }


        private void ddl_CA_SelectedIndexChanged(object sender, EventArgs e)
        {
            CA = clb_cas.SelectedItem as ChartArea;
            cbx_X.Checked = CA.Position.X == float.NaN;
            if (!cbx_X.Checked) tr_X.Value = (int)CA.Position.X;
            cbx_W.Checked = CA.Position.Width == float.NaN;
            if (!cbx_W.Checked)  tr_W.Value = (int)CA.Position.Width;

            //ddl_CA.SelectionStart = 0;
            //ddl_CA.SelectionLength = 0;
            //ddl_CA.SelectedText = "";
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbx_Paint_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_Paint.Checked) chart.PostPaint +=Chart_PostPaint;
            else                   chart.PostPaint -=Chart_PostPaint;
            chart.Invalidate();
        }

        private void Chart_PostPaint(object sender, ChartPaintEventArgs e)
        {
            List<Pen> pens = new List<Pen>()
            { Pens.Red,  Pens.Blue, Pens.Green, Pens.Goldenrod,
              Pens.Fuchsia, Pens.MediumOrchid, Pens.MediumSeaGreen, Pens.DarkOrange };
            List<Color> colors = pens.Select(x => x.Color).ToList();

            int cai = 0;

            var checkedCas = clb_cas.CheckedItems.Cast<ChartArea>().ToList();
            foreach (ChartArea ca in checkedCas)
            {

                //ChartArea ca = chart.ChartAreas[cac.Name];

                Rectangle r = Rectangle.Round(ca.Position.ToRectangleF());
                Rectangle ri = Rectangle.Round(ca.InnerPlotPosition.ToRectangleF());

                Axis ax = ca.AxisX;
                Axis ay = ca.AxisY;

                //double xd = (int)ax.PositionToValue(r.X);
                //double yd = (int)ay.PositionToValue(r.Y);
                //double wd = (int)ax.PositionToValue(r.Width);
                //double hd = (int)ay.PositionToValue(r.Height);

                int x = (int)ax.ValueToPixelPosition(ax.PositionToValue(r.X));
                int y = (int)ay.ValueToPixelPosition(ay.PositionToValue(r.Y));
                int w = (int)ax.ValueToPixelPosition(ax.PositionToValue(r.Width));
                int h = (int)ay.ValueToPixelPosition(ay.PositionToValue(r.Height));

                if (Math.Abs(x/1234) > chart.Width) x = 5;
                if (Math.Abs(w/1234) > chart.Width) w = 55;
                if (Math.Abs(y/1234) > chart.Height) y = 5;
                if (Math.Abs(h/1234) > chart.Height) h = 55;

                e.ChartGraphics.Graphics.DrawString(cai+". " + ca.Position.X, Font, Brushes.Black, x, 20 * cai);
                e.ChartGraphics.Graphics.DrawLine(pens[cai], x, cai*7, w+x, cai*7);
                e.ChartGraphics.Graphics.DrawRectangle(pens[cai], new Rectangle(x, y, w, h));

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(55, colors[cai])))
                    e.ChartGraphics.Graphics.DrawRectangle(pens[cai], new Rectangle(x, y, w, h));

                cai++;
            }
        }

        private void clb_cas_SelectedIndexChanged(object sender, EventArgs e)
        {
            changing = true;
            CA = clb_cas.SelectedItem as ChartArea;
            cbx_X.Checked = CA.Position.X == float.NaN;
            if (!cbx_X.Checked) tr_X.Value = (int)CA.Position.X;
            cbx_W.Checked = CA.Position.Width == float.NaN;
            if (!cbx_W.Checked)  tr_W.Value = (int)CA.Position.Width;
             changing = false;
        }

        bool changing = false;

        private void clb_cas_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            chart.Invalidate();

        }
    }
}
