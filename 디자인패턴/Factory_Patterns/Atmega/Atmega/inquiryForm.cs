using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Atmega
{
	public partial class inquiryForm : Form
	{
		AtmegaBox Box1280 = new Atmega128Box();

		Atmega at;

		public inquiryForm()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (textBox1.Text != "")
			{
				at = Box1280.inquiryAtmega(textBox1.Text);
				MessageBox.Show(at.Prepare());
			}
		}
	}
}