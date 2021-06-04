using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woom.DataDefine.Attribute
{
    public class ClsVolumeAttribute
    {
        public enum VolumePrice
        {
            GAIN_PRICE, FORE_PRICE, GIGAN_PRICE, GUMY_PRICE, BOHUM_PRICE, TOSIN_PRICE, GITA_PRICE
       , BANK_PRICE, YEONGI_PRICE, SAMO_PRICE, NATION_PRICE, BUBIN_PRICE, IOFORE_PRICE, GIGAN_SUM_PRICE
        }

        public enum VolumeQty
        {
            GAIN_QTY, FORE_QTY, GIGAN_QTY, GUMY_QTY, BOHUM_QTY, TOSIN_QTY, GITA_QTY
           , BANK_QTY, YEONGI_QTY, SAMO_QTY, NATION_QTY, BUBIN_QTY, IOFORE_QTY, GIGAN_SUM_QTY
        }

        public string VolumePriceName(VolumePrice volumePrice)
        {
            string priceName = "";

            switch (volumePrice)
            {
                case VolumePrice.GAIN_PRICE:
                    priceName = "개인";
                    break;
                case VolumePrice.FORE_PRICE:
                    priceName = "외인";
                    break;
                case VolumePrice.GIGAN_PRICE:
                    priceName = "기관";
                    break;
                case VolumePrice.GUMY_PRICE:
                    priceName = "금융";
                    break;
                case VolumePrice.BOHUM_PRICE:
                    priceName = "보험";
                    break;
                case VolumePrice.TOSIN_PRICE:
                    priceName = "투신";
                    break;
                case VolumePrice.GITA_PRICE:
                    priceName = "기금";
                    break;
                case VolumePrice.BANK_PRICE:
                    priceName = "은행";
                    break;
                case VolumePrice.YEONGI_PRICE:
                    priceName = "연기금";
                    break;
                case VolumePrice.SAMO_PRICE:
                    priceName = "사모";
                    break;
                case VolumePrice.NATION_PRICE:
                    priceName = "국가";
                    break;
                case VolumePrice.BUBIN_PRICE:
                    priceName = "법인";
                    break;
                case VolumePrice.IOFORE_PRICE:
                    priceName = "내외";
                    break;
                case VolumePrice.GIGAN_SUM_PRICE:
                    priceName = "기관합";
                    break;
                default:
                    break;
            }

            return priceName;

        }
        public string VolumeQtyName(VolumeQty volumeQty)
        {
            string priceName = "";

            switch (volumeQty)
            {
                case VolumeQty.GAIN_QTY:
                    priceName = "개인";
                    break;
                case VolumeQty.FORE_QTY:
                    priceName = "외인";
                    break;
                case VolumeQty.GIGAN_QTY:
                    priceName = "기관";
                    break;
                case VolumeQty.GUMY_QTY:
                    priceName = "금융";
                    break;
                case VolumeQty.BOHUM_QTY:
                    priceName = "보험";
                    break;
                case VolumeQty.TOSIN_QTY:
                    priceName = "투신";
                    break;
                case VolumeQty.GITA_QTY:
                    priceName = "기금";
                    break;
                case VolumeQty.BANK_QTY:
                    priceName = "은행";
                    break;
                case VolumeQty.YEONGI_QTY:
                    priceName = "연기금";
                    break;
                case VolumeQty.SAMO_QTY:
                    priceName = "사모";
                    break;
                case VolumeQty.NATION_QTY:
                    priceName = "국가";
                    break;
                case VolumeQty.BUBIN_QTY:
                    priceName = "법인";
                    break;
                case VolumeQty.IOFORE_QTY:
                    priceName = "내외";
                    break;
                case VolumeQty.GIGAN_SUM_QTY:
                    priceName = "기관합";
                    break;
                default:
                    break;
            }

            return priceName;

        }


    }
}
