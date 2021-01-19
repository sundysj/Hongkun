using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using MobileSoft.Common;
using System.Data;
using MobileSoft.DBUtility;
using System.IO;
using System.Configuration;
using System.Runtime.InteropServices;

namespace Business
{
    class VersionManage : PubInfo
    {
        public VersionManage()
        {
            base.Token = "20160516VersionManage";
        }
        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            //项目，责任人，时间
            switch (Trans.Command)
            {
                case "GetUpdateInfo"://获取更新信息
                    Trans.Result = GetUpdateInfo(Row);
                    break;
                case "GetCustUpdateInfo"://获取更新信息
                    Trans.Result = GetCustUpdateInfo(Row);
                    break;
                case "GetCustUpdatePatchInfo"://获取补丁更新信息
                    Trans.Result = GetCustUpdatePatchInfo(Row);
                    break;
            }
        }

        /// <summary>
        /// 获取更新补丁信息
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetCustUpdatePatchInfo(DataRow Row)
        {
            string result = JSONHelper.FromString(false, "未知错误!");
            try
            {
                string filePath = ConfigurationManager.AppSettings["CustUpgradeIniFilePath"].ToString();
                string VersionType = Row["VersionType"].ToString();
                DataTable dt = new DataTable("UpdateIfo");
                dt.Columns.Add("PatchVersion", typeof(System.Int32));
                dt.Columns.Add("PatchMD5", typeof(System.String));
                dt.Columns.Add("PatchDownload", typeof(System.String));
                int PatchVersion = Convert.ToInt32(IniReadValue(VersionType, "PatchVersion", filePath));
                string PatchMD5 = IniReadValue(VersionType, "PatchMD5", filePath);
                string PatchDownload = IniReadValue(VersionType, "PatchDownload", filePath);
                DataRow dr = dt.NewRow();
                dr["PatchVersion"] = PatchVersion;
                dr["PatchMD5"] = PatchMD5;
                dr["PatchDownload"] = PatchDownload;
                dt.Rows.Add(dr);
                result = JSONHelper.FromString(dt);
            }
            catch (Exception e)
            {
                result = JSONHelper.FromString(false, e.Message);
            }
            return result;
        }
        /// <summary>
        /// 获取更新信息
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetCustUpdateInfo(DataRow Row)
        {
            string result = JSONHelper.FromString(false, "未知错误!");
            try
            {
                string filePath = ConfigurationManager.AppSettings["CustUpgradeIniFilePath"].ToString();
                string VersionType = Row["VersionType"].ToString();
                DataTable dt = new DataTable("UpdateIfo");
                dt.Columns.Add("Version", typeof(System.Int32));
                dt.Columns.Add("Name", typeof(System.String));
                dt.Columns.Add("Download", typeof(System.String));
                dt.Columns.Add("Content", typeof(System.String));
                dt.Columns.Add("isMustUpdate", typeof(System.Boolean));
                int Version = Convert.ToInt32(IniReadValue(VersionType, "Version", filePath));
                string Name = IniReadValue(VersionType, "Name", filePath);
                string Download = IniReadValue(VersionType, "Download", filePath);
                string Content = IniReadValue(VersionType, "Content", filePath);
                bool isMustUpdate = Convert.ToBoolean(IniReadValue(VersionType, "isMustUpdate", filePath));
                DataRow dr = dt.NewRow();
                dr["Version"] = Version;
                dr["Name"] = Name;
                dr["Download"] = Download;
                dr["Content"] = Content;
                dr["isMustUpdate"] = isMustUpdate;
                dt.Rows.Add(dr);
                result = JSONHelper.FromString(dt);
            }
            catch (Exception e)
            {
                result = JSONHelper.FromString(false, e.Message);
            }
            return result;
        }

        private string GetUpdateInfo(DataRow Row)
        {
            string result = JSONHelper.FromString(false, "未知错误!");
            try
            {
                string filePath = ConfigurationManager.AppSettings["UpgradeIniFilePath"].ToString();
                string VersionType = Row["VersionType"].ToString();
                DataTable dt = new DataTable("UpdateIfo");
                dt.Columns.Add("Version", typeof(System.Int32));
                dt.Columns.Add("Name", typeof(System.String));
                dt.Columns.Add("Download", typeof(System.String));
                dt.Columns.Add("Content", typeof(System.String));
                dt.Columns.Add("isMustUpdate", typeof(System.String));
                int Version = Convert.ToInt32(IniReadValue(VersionType, "Version", filePath));
                string Name = IniReadValue(VersionType, "Name", filePath);
                string Download = IniReadValue(VersionType, "Download", filePath);
                string Content = IniReadValue(VersionType, "Content", filePath);
                //bool isMustUpdate = Convert.ToBoolean(IniReadValue(VersionType, "isMustUpdate", filePath));
                DataRow dr = dt.NewRow();
                dr["Version"] = Version;
                dr["Name"] = Name;
                dr["Download"] = Download;
                dr["Content"] = Content;
                dr["isMustUpdate"] = IniReadValue(VersionType, "isMustUpdate", filePath);
                dt.Rows.Add(dr);
                result = JSONHelper.FromString(dt);
            }
            catch (Exception e) {
                result = JSONHelper.FromString(false, e.Message);
            }
            return result;
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
