using System;
using System.Configuration;

namespace KernelDev
{
    /// <summary>
    /// AppKDev 的摘要说明。
    /// </summary>
    public sealed class AppKDev
    {
        //		private static CryptEngine mAppCryptEngine;
        //		private static SolutionInfo mAppSolutionInfo;
        //		private static ClientControl mClientControl;
        //		private static KernelDev.DataAccess.DataAccess mDataAccess;
        //		private static DataTableXML mDataTableXML;
        // 
        //		private static IniFile mIniFile;
        // 
        //		private static LoginTokens mLoginTokens;

        private static bool mServiceStart;

        //		private static string mstrCryptKey;

        private static bool mWebStart;


        public AppKDev()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        static AppKDev()
        {
            AppKDev.mServiceStart = false;
            AppKDev.mWebStart = false;
            //			AppKDev.mstrCryptKey = "TFWorkHome";
            //			AppKDev.mAppCryptEngine = new CryptEngine(CryptType.DES);
            //			AppKDev.mAppCryptEngine.Key = AppKDev.mstrCryptKey;
            //			AppKDev.mDataAccess = new KernelDev.DataAccess.DataAccess();
            //			AppKDev.mLoginTokens = new LoginTokens();
            //			AppKDev.mClientControl = new ClientControl();
            //			AppKDev.mIniFile = new IniFile();
            //			AppKDev.mDataTableXML = new DataTableXML();
            //			AppKDev.mAppSolutionInfo = new SolutionInfo();
        }

        //		public static ClientControl AppClientControl
        //		{
        //			get
        //			{
        //				return AppKDev.mClientControl;
        //			}
        //		}
        // 
        //
        //		public static CryptEngine AppCryptEngine
        //		{
        //			get
        //			{
        //				return AppKDev.mAppCryptEngine;
        //			}
        //		}
        // 
        //		public static DataTableXML AppDataTableXML
        //		{
        //			get
        //			{
        //				return AppKDev.mDataTableXML;
        //			}
        //		}
        // 
        //		public static IniFile AppIniFile
        //		{
        //			get
        //			{
        //				return AppKDev.mIniFile;
        //			}
        //		}
        // 
        //		public static LoginTokens AppLoginTokens
        //		{
        //			get
        //			{
        //				return AppKDev.mLoginTokens;
        //			}
        //		}
        // 
        //		public static SolutionInfo AppSolutionInfo
        //		{
        //			get
        //			{
        //				return AppKDev.mAppSolutionInfo;
        //			}
        //		}

        public static bool IsServiceStart
        {
            get
            {
                return AppKDev.mServiceStart;
            }
            set
            {
                AppKDev.mServiceStart = value;
            }
        }

        public static bool IsWebStart
        {
            get
            {
                return AppKDev.mWebStart;
            }
            set
            {
                AppKDev.mWebStart = value;
            }
        }


        public static string AppWebSettings(string Key)
        {
            AppSettingsReader reader1 = new AppSettingsReader();
            string text1 = "";
            return Convert.ToString(reader1.GetValue(Key, text1.GetType()));
        }


        public static string BulidErrorMessage(Exception ex)
        {
            return AppKDev.BulidErrorMessage(ex, "");
        }


        public static string BulidErrorMessage(Exception ex, string IP)
        {
            string text1 = "Web\u670d\u52a1\u5668\u672a\u542f\u52a8\u6216\u914d\u7f6e\u9519\u8bef,\u8bf7\u8054\u7cfb\u7ba1\u7406\u5458!";
            if (((IP == "127.0.0.1") || (IP == "")) && (ex.Source == "System.Web.Services"))
            {
                int num1 = ex.Message.IndexOf("<title>", 0);
                if (num1 > 0)
                {
                    int num2 = ex.Message.IndexOf("</title>", 0);
                    text1 = ex.Message.Substring(num1 + 7, (num2 - num1) - 7);
                }
            }
            if ((ex.Message.IndexOf("\u975e\u6cd5\u7528\u6237,\u4e0d\u5141\u8bb8\u8fdb\u5165\u7cfb\u7edf!") > 0) && ((IP == "127.0.0.1") || (IP == "")))
            {
                text1 = "IIS Web\u94fe\u63a5Service\u4e3a\u975e\u6cd5\u7528\u6237,\u4e0d\u5141\u8bb8\u8fdb\u5165\u7cfb\u7edf!";
            }
            if ((ex.Message.IndexOf("\u6743\u9650\u4e0d\u8db3\u4e0d\u80fd\u8fdb\u5165\u7cfb\u7edf!") > 0) && ((IP == "127.0.0.1") || (IP == "")))
            {
                text1 = "IIS Web\u94fe\u63a5Service\u7684\u7528\u6237\u4e0d\u5728Service\u5141\u8bb8\u8bbf\u95ee\u7684\u6743\u9650\u7ec4\u4e2d,\u4e0d\u80fd\u8fdb\u5165Service\u670d\u52a1!";
            }
            if ((ex.Message.IndexOf("Access Denied") > 0) && ((IP == "127.0.0.1") || (IP == "")))
            {
                text1 = "IIS Service\u670d\u52a1\u6ca1\u6709\u8bbe\u7f6eWindows\u9a8c\u8bc1\u65b9\u5f0f\u6216\u8fdb\u884c\u7528\u6237\u9a8c\u8bc1\u65f6\u672a\u901a\u8fc7!";
            }
            return text1.ToString();
        }


        public static string BulidTreeNumNext(string strNum, string strPNum)
        {
            if (strNum == "")
            {
                return (strPNum + "01");
            }
            string text1 = "";
            text1 = strNum.Substring(strNum.Length - 2, 2);
            int num1 = Convert.ToInt16(text1);
            num1++;
            text1 = num1.ToString();
            text1 = text1.PadLeft(2, '0');
            strNum = strPNum + text1;
            return strNum;
        }


        public static string URLParamsCheck(string mURLParams)
        {
            return AppKDev.URLParamsCheck(mURLParams, true);
        }


        public static string URLParamsCheck(string mURLParams, bool IsCutLength)
        {
            if ((mURLParams != null) && (mURLParams != ""))
            {
                string[] textArray1 = ">,;,','',=".Split(new char[] { Convert.ToChar(",") });
                foreach (string text2 in textArray1)
                {
                    mURLParams = mURLParams.Replace(text2, "");
                }
                if (IsCutLength && (mURLParams.Length > 0x2d))
                {
                    mURLParams = mURLParams.Substring(0, 0x2d);
                }
            }
            return mURLParams;
        }
    }
}
