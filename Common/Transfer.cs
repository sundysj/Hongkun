using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Transfer
    {
        private string _Result = "";
        private string _Error = "";
        private string _Attribute = "";
        private string _Command = "";
        private string _Class = "";
        private string _Mac = "";
        private string _DataVID = "";
        private string _QYID = "";
        private string _QYUnitType = "";
        private string _ClassLog = "";
        private string _CommandLog = "";
        /// <summary>
        /// 协议(碧桂园专用) 因为要传html代码 不能以xml形式传送会解析失败 所以加个这个参数
        /// </summary>
        public string Agreement
        {
            set;get;
        }
        public string Result
        {
            set { _Result = value;}
            get { return _Result; }
        }

        public string Error
        {
            set { _Error = value; }
            get { return _Error; }
        }

        public string Attribute
        {
            set { _Attribute = value; }
            get { return _Attribute; }
        }

        public string Class
        {
            set { _Class = value; }
            get { return _Class; }

        }
        public string Command
        {
            set { _Command = value; }
            get { return _Command; }
        }
        public string Mac
        {
            set { _Mac = value; }
            get { return _Mac; }
        }


        public string QYID
        {
            set { _QYID = value; }
            get { return _QYID; }
        }
        

      public string QYUnitType
        {
            set { _QYUnitType = value; }
            get { return _QYUnitType; }
        }
        public string Output()
        {
            if (_Error.ToString() != "")
                return _Error.ToString();
            else return _Result.ToString();
        }

        public string ClassLog
        {
            set { _ClassLog = value; }
            get { return _ClassLog; }
        }

        public string CommandLog
        {
            set { _CommandLog = value; }
            get { return _CommandLog; }
        }
    }
}
