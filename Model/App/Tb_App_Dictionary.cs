using System;

namespace App.Model
{
    /// <summary>
    /// App字典表
    /// </summary>
    public class Tb_App_Dictionary
    {
        /// <summary>
        /// 字典key
        /// </summary>
        public String Key
        {
            get;
            set;
        }

        /// <summary>
        /// 字典value
        /// </summary>
        public String Value
        {
            get;
            set;
        }

        /// <summary>
        /// 备注信息
        /// </summary>
        public String Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 排序号
        /// </summary>
        public Int16 SortIndex
        {
            get;
            set;
        }
    }
}
