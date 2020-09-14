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

			sb.Append("타   입 : " + type + "\n");
			sb.Append("이   름 : " + name + "\n");
			sb.Append("용   도 : " + use + "\n");
			sb.Append("제조사 : " + manufacture +"\n");
			sb.Append("가   격 : " + price +"\n");
			sb.Append("구입일 : " + dateOfPurchase +"\n");
			sb.Append("갯   수 : " + number + "\n");
			sb.Append("규   격 : ");

			foreach (string s in Standard)
			{
				sb.Append("\t" + s + "\n");
			}

			InquiryEqipment ie = new InquiryEqipment();

			return sb.ToString();
		}

		public virtual string nothing()
		{
			return "찾을 수 없습니다.";
		}
	}
}
