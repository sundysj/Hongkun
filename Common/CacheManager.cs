using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using System.Data;
using System.Web;
using System.Collections.Generic;

namespace MobileSoft.Common
{
    public partial class CacheManager 
    {
        public static T Get<T>(string Key)
        {
            return (T)System.Web.HttpRuntime.Cache[Key];
        }

        public static string ResultJson(string Key)
        {
            string Result="[]";
            if(System.Web.HttpRuntime.Cache[Key]!=null)
            {
                Result= Get<string>(Key);
            }
            return Result;
        }

        /// <summary>
        /// 缓存增加  分钟
        /// </summary>
        /// <param name="Type">1滑动过期 2 绝对过期</param>
        /// <param name="cacheTime">单位 分钟</param>
        public static void Insert(int Type, string Key, object Data, int CacheTime)
        {
            if(Type==1)
                HttpRuntime.Cache.Insert(Key, Data, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(CacheTime));
            if(Type==2)
                HttpRuntime.Cache.Insert(Key, Data, null, DateTime.Now.AddMinutes(CacheTime), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// 删除缓存项
        /// </summary>
        /// <param name="key">/key</param>
        public static void Remove(string Key)
        {
            System.Web.HttpRuntime.Cache.Remove(Key);
        }

    }
}
