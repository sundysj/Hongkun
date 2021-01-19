using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianChengEntranceSyncService
{
    public class Config
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        public static string CorpName
        {
            get
            {
                try
                {
                    return System.Configuration.ConfigurationManager.AppSettings["MoreDian_CorpName"];
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// 系统编号
        /// </summary>
        public static string CorpID
        {
            get
            {
                try
                {
                    return System.Configuration.ConfigurationManager.AppSettings["MoreDian_CorpID"];
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 魔蓝开发者配置信息
        /// </summary>
        public class MoreDian
        {
            /// <summary>
            /// 魔蓝接口地址
            /// </summary>
            public static string APIURL
            {
                get
                {
                    try
                    {
                        return System.Configuration.ConfigurationManager.AppSettings["MoreDian_APIURL"];
                    }
                    catch (Exception)
                    {
                        return "";
                    }
                }
            }
            /// <summary>
            /// 魔蓝APPID
            /// </summary>
            public static string APPID {
                get
                {
                    try
                    {
                        return System.Configuration.ConfigurationManager.AppSettings["MoreDian_APPID"];
                    }
                    catch (Exception)
                    {
                        return "";
                    }
                }
            }
            /// <summary>
            /// 魔蓝SECRET
            /// </summary>
            public static string SECRET
            {
                get
                {
                    try
                    {
                        return System.Configuration.ConfigurationManager.AppSettings["MoreDian_SECRET"];
                    }
                    catch (Exception)
                    {
                        return "";
                    }
                }
            }
        }


        /// <summary>
        /// 门禁系统数据库配置（PMS_Base库或者ERP主库）
        /// </summary>
        public static string EntranceConnectionStr { get; set; }


        /// <summary>
        /// 微信信息配置数据库
        /// </summary>
        public static string WChat2020ConnectionStr { get; set; }
    }
}
