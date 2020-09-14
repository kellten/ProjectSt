using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PropertyGroupingExample
{
    public partial class MyControl : Control
    {
        public MyControl()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;

            g.Clear(Color.OrangeRed);

            if (this.Option.UseText == true)
            {
                SolidBrush br = new SolidBrush(Color.FromArgb(this.Option.UseOpacity ? 100 : 255, Color.Gray));
                Font font = new Font("굴림체", 20, FontStyle.Bold);
                g.DrawString(this.Text, font, br, 0, 0);
            }

            base.OnPaint(pe);
        }

        private Options m_Option = new Options();
        //[TypeConverter(typeof(ExpandableObjectConverter))]
        public Options Option
        {
            get
            {
                return m_Option;
            }
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Options
    {
        public bool UseOpacity { get; set; }
        public bool UseText { get; set; }
    }
}