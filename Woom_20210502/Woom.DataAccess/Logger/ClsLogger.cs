using System;

namespace Woom.DataAccess.Logger
{
    internal class ClsLogger
    {
        public enum LoggerType
        { Connect, RealData, ScreeNo, Order, NotRealData, OnMsg }

        private bool _LoggerStartOption;
        public bool LoggerStartOption { get { return _LoggerStartOption; } set { _LoggerStartOption = value; } }

        public string Logger(LoggerType lt, string message, string messageName, string stockCode)
        {
            string sLog = "";
            string logDateTime;
            logDateTime = string.Format("{0:dd.MM.yyyy HH:mm:ss}: ", DateTime.Now);

            switch (lt)
            {
                case LoggerType.Connect:
                    sLog = logDateTime + "[접속정보]" + messageName + " " + message;
                    break;

                case LoggerType.RealData:
                    sLog = logDateTime + "[실시간정보]" + " " + stockCode + " : " + messageName + " " + message;
                    break;

                case LoggerType.ScreeNo:
                    sLog = logDateTime + "[화면번호]" + " " + stockCode + " : " + messageName + " " + message;
                    break;

                case LoggerType.Order:
                    sLog = logDateTime + "[주문]" + " " + stockCode + " : " + messageName + " " + message;
                    break;

                case LoggerType.NotRealData:
                    sLog = logDateTime + "[일반]" + " " + stockCode + " : " + messageName + " " + message;
                    break;

                case LoggerType.OnMsg:
                    sLog = logDateTime + "[OnMsg]" + " " + stockCode + " : " + messageName + " " + message;
                    break;

                default:
                    break;
            }

            if (_LoggerStartOption == true)
            {
            }

            return sLog;
        }
    }
}