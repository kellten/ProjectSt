using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Equipment
{
	public abstract class Equipment
	{
		protected string type = string.Empty;
		protected string name = string.Empty;
		protected string use = string.Empty;
		protected string manufacture = string.Empty;
		protected string price = string.Empty;
		protected string dateOfPurchase = string.Empty;
		protected string number = string.Empty;

		protected ArrayList Standard = new ArrayList();

		public Equipment() { }

		public string prepare()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("Ÿ   �� : " + type + "\n");
			sb.Append("��   �� : " + name + "\n");
			sb.Append("��   �� : " + use + "\n");
			sb.Append("������ : " + manufacture +"\n");
			sb.Append("��   �� : " + price +"\n");
			sb.Append("������ : " + dateOfPurchase +"\n");
			sb.Append("��   �� : " + number + "\n");
			sb.Append("��   �� : ");

			foreach (string s in Standard)
			{
				sb.Append("\t" + s + "\n");
			}

			InquiryEqipment ie = new InquiryEqipment();

			return sb.ToString();
		}

		public virtual string nothing()
		{
			return "ã�� �� �����ϴ�.";
		}
	}
}
