using AnSt.Util.Func;
using System;

namespace AnSt.Define
{
    public static class clsDicDefine
    {
        public const String GAIN_QTY = "GAIN_QTY";
        public const String FORE_QTY = "FORE_QTY";
        public const String GIGAN_QTY = "GIGAN_QTY";
        public const String GUMY_QTY = "GUMY_QTY";
        public const String BOHUM_QTY = "BOHUM_QTY";
        public const String TOSIN_QTY = "TOSIN_QTY";
        public const String GITA_QTY = "GITA_QTY";
        public const String BANK_QTY = "BANK_QTY";
        public const String YEONGI_QTY = "YEONGI_QTY";
        public const String SAMO_QTY = "SAMO_QTY";
        public const String NATION_QTY = "NATION_QTY";
        public const String BUBIN_QTY = "BUBIN_QTY";
        public const String IOFORE_QTY = "IOFORE_QTY";
        public const String GIGAN_SUM_QTY = "GIGAN_SUM_QTY";

        public const String GAIN_PRICE = "GAIN_PRICE";
        public const String FORE_PRICE = "FORE_PRICE";
        public const String GIGAN_PRICE = "GIGAN_PRICE";
        public const String GUMY_PRICE = "GUMY_PRICE";
        public const String BOHUM_PRICE = "BOHUM_PRICE";
        public const String TOSIN_PRICE = "TOSIN_PRICE";
        public const String GITA_PRICE = "GITA_PRICE";
        public const String BANK_PRICE = "BANK_PRICE";
        public const String YEONGI_PRICE = "YEONGI_PRICE";
        public const String SAMO_PRICE = "SAMO_PRICE";
        public const String NATION_PRICE = "NATION_PRICE";
        public const String BUBIN_PRICE = "BUBIN_PRICE";
        public const String IOFORE_PRICE = "IOFORE_PRICE";
        public const String GIGAN_SUM_PRICE = "GIGAN_SUM_PRICE";

        public const String GAIN = "개인";
        public const String FORE = "외인";
        public const String GIGAN = "기관";
        public const String GUMY = "금융";
        public const String BOHUM = "보험";
        public const String TOSIN = "투신";
        public const String GITA = "기타";
        public const String BANK = "은행";
        public const String YEONGI = "연기금";
        public const String SAMO = "사모";
        public const String NATION = "국가";
        public const String BUBIN = "기법";
        public const String IOFORE = "내외";
        public const String GIGAN_SUM = "기관합";

        public enum DicParamIndex
        {
            Gain = 0, Fore = 1, Gigan = 2, Gumy = 3, Bohum = 4, Tosin = 5,
            Gita = 6, Bank = 7, Yeongi = 8, Samo = 9, Nation = 10, Bubin = 12, IoFore = 13
        }

        /// <summary>
        /// 거래량 주체의 필드값의 명칭을 가져온다.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetVolumeJucheName(string value)
        {
            string reValue = "";
            switch (value)
            {
                case "GAIN_PRICE": return GAIN;
                case "GAIN_QTY": return GAIN;
                case "FORE_PRICE": return FORE;
                case "FORE_QTY": return FORE;
                case "GIGAN_PRICE": return GIGAN;
                case "GIGAN_QTY": return GIGAN;
                case "GUMY_PRICE": return GUMY;
                case "GUMY_QTY": return GUMY;
                case "BOHUM_PRICE": return BOHUM;
                case "BOHUM_QTY": return BOHUM;
                case "TOSIN_PRICE": return TOSIN;
                case "TOSIN_QTY": return TOSIN;
                case "GITA_PRICE": return GITA;
                case "GITA_QTY": return GITA;
                case "BANK_PRICE": return BANK;
                case "BANK_QTY": return BANK;
                case "YEONGI_PRICE": return YEONGI;
                case "YEONGI_QTY": return YEONGI;
                case "SAMO_PRICE": return SAMO;
                case "SAMO_QTY": return SAMO;
                case "NATION_PRICE": return NATION;
                case "NATION_QTY": return NATION;
                case "BUBIN_PRICE": return BUBIN;
                case "BUBIN_QTY": return BUBIN;
                case "IOFORE_PRICE": return IOFORE;
                case "IOFORE_QTY": return IOFORE;
                case "GIGAN_SUM_PRICE": return GIGAN_SUM;
                case "GIGAN_SUM_QTY": return GIGAN_SUM;

            }

            return reValue;
        }

        public static string GetJuchePriceName(DicParamIndex pi)
        {
            switch (pi)
            {
                case DicParamIndex.Gain: return GAIN;
                case DicParamIndex.Fore: return FORE;
                case DicParamIndex.Gigan: return GIGAN;
                case DicParamIndex.Gumy: return GUMY;
                case DicParamIndex.Bohum: return BOHUM;
                case DicParamIndex.Tosin: return TOSIN;
                case DicParamIndex.Gita: return GITA;
                case DicParamIndex.Bank: return BANK;
                case DicParamIndex.Yeongi: return YEONGI;
                case DicParamIndex.Samo: return SAMO;
                case DicParamIndex.Nation: return NATION;
                case DicParamIndex.Bubin: return BUBIN;
                case DicParamIndex.IoFore: return IOFORE;
            }

            return "";
        }

        public static string GetJucheName(DicParamIndex pi)
        {
            switch (pi)
            {
                case DicParamIndex.Gain: return GAIN_PRICE;
                case DicParamIndex.Fore: return GIGAN_PRICE;
                case DicParamIndex.Gigan: return GUMY_PRICE;
                case DicParamIndex.Gumy: return BOHUM_PRICE;
                case DicParamIndex.Bohum: return TOSIN_PRICE;
                case DicParamIndex.Tosin: return GITA_PRICE;
                case DicParamIndex.Gita: return BANK_PRICE;
                case DicParamIndex.Bank: return YEONGI_PRICE;
                case DicParamIndex.Yeongi: return SAMO_PRICE;
                case DicParamIndex.Samo: return NATION_PRICE;
                case DicParamIndex.Nation: return BUBIN_PRICE;
                case DicParamIndex.Bubin: return IOFORE_PRICE;
                case DicParamIndex.IoFore: return GIGAN_SUM_PRICE;
            }

            return "";
        }

        public static string GetVolumeData()
        {

            ClsUtilFunc _clsUtilFunc = new ClsUtilFunc();
            String stdDate = "";
            int i = Int32.Parse(System.DateTime.Now.ToString("HH") + System.DateTime.Now.ToString("ss"));

            if (i > 1600)
            { stdDate = _clsUtilFunc.DateToString(System.DateTime.Now.Date.ToString()); }
            else
            { stdDate = DateTime.Today.AddDays(-1).ToString("yyyyMMdd"); }

            return stdDate;
        }

        // GAIN 
        // FORE 
        // GIGAN
        // GUMY 
        // BOHUM
        // TOSIN
        // GITA 
        // BANK 
        // YEONGI
        // SAMO 
        // NATION
        // BUBIN 
        // IOFORE
        // GIGAN_SUM

        // 개인
        // 외국인
        // 기관
        // 금융
        // 보험
        // 투신
        // 기타금융
        // 은행
        // 연기금
        // 사모
        // 국가
        // 기법
        // 기외
        // 기관합
    }
}
