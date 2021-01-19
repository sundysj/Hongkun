using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Text;

namespace Common.Extenions
{
    /// <summary>
    /// 对象扩展类
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 对象转换为泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">当前对象</param>
        /// <returns></returns>
        public static T CastTo<T>(this object obj)
        {
            return obj.CastTo(default(T));
        }

        /// <summary>
        /// 对象转换为泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="def">默认值</param>
        /// <returns></returns>
        public static T CastTo<T>(this object obj, T def)
        {
            var value = obj.CastTo(typeof(T));
            return value == null ? def : (T)value;
        }

        /// <summary>
        /// 对象转换为泛型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object CastTo(this object value, Type type)
        {
            if (value == null) return null;
            if (type.IsNullableType()) type = type.GetUnNullableType();
            try
            {
                if (type.IsEnum) return System.Enum.Parse(type, value.ToString());
                if (type == typeof(Guid)) return Guid.Parse(value.ToString());
                return Convert.ChangeType(value, type);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 将对象（主要是匿名对象）转换为dynamic
        /// </summary>
        public static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();
            var type = value.GetType();
            var properties = TypeDescriptor.GetProperties(type);
            foreach (PropertyDescriptor property in properties)
            {
                var val = property.GetValue(value);
                if (property.PropertyType.FullName.StartsWith("<>f__AnonymousType"))
                {
                    dynamic dval = val.ToDynamic();
                    expando.Add(property.Name, dval);
                }
                else
                {
                    expando.Add(property.Name, val);
                }
            }
            return (ExpandoObject)expando;
        }

        /// <summary>
        /// 异常信息格式化
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="isHideStackTrace"></param>
        /// <returns></returns>
        public static string Format(this Exception ex, bool isHideStackTrace = false)
        {
            var sb = new StringBuilder();
            var count = 0;
            var appString = string.Empty;
            while (ex != null)
            {
                if (count > 0)
                {
                    appString += "  ";
                }
                sb.AppendLine(string.Format("{0}异常消息：{1}", appString, ex.Message));
                sb.AppendLine(string.Format("{0}异常类型：{1}", appString, ex.GetType().FullName));
                sb.AppendLine(string.Format("{0}异常方法：{1}", appString, ex.TargetSite?.Name));
                sb.AppendLine(string.Format("{0}异常源：{1}", appString, ex.Source));
                if (!isHideStackTrace && ex.StackTrace != null)
                {
                    sb.AppendLine(string.Format("{0}异常堆栈：{1}", appString, ex.StackTrace));
                }
                if (ex.InnerException != null)
                {
                    sb.AppendLine(string.Format("{0}内部异常：", appString));
                    count++;
                }
                ex = ex.InnerException;
            }
            return sb.ToString();
        }

        public static System.Data.DataTable GetPagedTable(this System.Data.DataTable dt, int PageIndex, int PageSize)//PageIndex表示第几页，PageSize表示每页的记录数
        {
            if (PageIndex == 0)
                return dt;//0页代表每页数据，直接返回

            System.Data.DataTable newdt = dt.Copy();
            newdt.Clear();//copy dt的框架

            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
                return newdt;//源数据记录数小于等于要显示的记录，直接返回dt

            if (rowend > dt.Rows.Count)
                rowend = dt.Rows.Count;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                System.Data.DataRow newdr = newdt.NewRow();
                System.Data.DataRow dr = dt.Rows[i];
                foreach (System.Data.DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }
            return newdt;
        }
    }
}
