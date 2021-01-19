using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.WChat2020.Model
{
    public class PageInfoModel
    {

        private int _PageIndex = 1;
        private int _PageSize = 10;
        private String _Sort = "UpdateTopDate";
        private bool _Descending = true;
        public int PageIndex
        {
            get { return _PageIndex; }
            set
            {
                if (value >= 0)
                {
                    _PageIndex = value;
                }
            }
        }

        public int PageSize
        {
            get { return _PageSize; }
            set
            {
                if (value >= 0)
                { _PageSize = value; }
            }
        }

        /// <summary>
        /// 排序字段
        /// </summary>
        public String Sort
        {
            get { return _Sort; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                { _Sort = value; }
            }
        }

        /// <summary>
        /// 是否倒序
        /// </summary>
        public bool Descending
        {
            get { return _Descending; }
            set
            {
                _Descending = value;
            }
        }


        /// <summary>
        /// 返回排序字段
        /// </summary>
        /// <returns></returns>
        public String GetSqlOrderStr()
        {
            return String.Format("  ORDER BY  {0} {1}", Sort, Descending ? " DESC " : " ASC");
        }

        /// <summary>
        /// 开始页码
        /// </summary>
        /// <returns></returns>
        public int GetStart()
        {
            return (PageIndex - 1) * PageSize + 1;
        }

        /// <summary>
        /// 结束页码
        /// </summary>
        /// <returns></returns>
        public int GetSEnd()
        {
            return PageIndex * PageSize;
        }


        /// <summary>
        /// 结束页码
        /// </summary>
        /// <returns></returns>
        public int GetEnd()
        {
            return PageIndex * PageSize;
        }
    }
}
