using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Service
{
    public partial class Update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string filePath = ConfigurationManager.AppSettings["UpgradeIniFilePath"].ToString();
            string custFilePath = ConfigurationManager.AppSettings["CustUpgradeIniFilePath"].ToString();
            bool isCust = false;
            if (!string.IsNullOrEmpty(Request["isCust"])) {
                isCust = true;
            }
            string VersionType = Request["VersionType"].ToString();
            // 也可以指定编码方式 
            string Download = IniReadValue(VersionType, "Download", isCust ? custFilePath : filePath);
            Response.Redirect(Download, false);
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        /// <summary>
        /// 读取配置ini文件
        /// </summary>
        /// <param name="Section">配置段</param>
        /// <param name="Key">键</param>
        /// <param name="innpath">存放物理路径</param>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key, string inipath)
        {
            StringBuilder temp = new StringBuilder(500);
            GetPrivateProfileString(Section, Key, "", temp, 500, inipath);
            return temp.ToString();
        }
    }
}