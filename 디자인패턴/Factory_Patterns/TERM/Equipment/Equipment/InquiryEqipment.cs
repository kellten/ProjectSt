using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Equipment
{
	public partial class InquiryEqipment : Form
	{
		StorageBox pcbBox = new PCBStorageBox();
		StorageBox cableBox = new CableStorageBox();
		StorageBox boardBox = new BoardStorageBox();

		Equipment eq;

		public InquiryEqipment()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (textBox1.Text != "")
			{
				eq = pcbBox.inquiryEquipment(textBox1.Text);
				MessageBox.Show(eq.prepare());
			}

			else if (textBox2.Text != "")
			{
				eq = cableBox.inquiryEquipment(textBox2.Text);
				MessageBox.Show(eq.prepare());
			}

			else if (textBox3.Text != "")
			{
				eq = boardBox.inquiryEquipment(textBox3.Text);
				MessageBox.Show(eq.prepare());
			}

			else
			{
				MessageBox.Show("데이터를 입력하세요");
			}

			textBox1.Text = null;
			textBox2.Text = null;
			textBox3.Text = null;
		}
	}
}