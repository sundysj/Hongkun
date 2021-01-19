using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extenions
{
    public static class ListExtension
    {
        /// <summary>
        /// 判断集合是否为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || !source.Any();
        }

        /// <summary>
        /// 集合遍历
        /// </summary>
        public static void Foreach<T>(this IEnumerable<T> instance, Action<T> action)
        {
            foreach (T item in instance)
            {
                action(item);
            }
        }

        /// <summary>
        /// 字典ASCII升序
        /// </summary>
        /// <typeparam name="TValue">字典中的值的类型</typeparam>
        /// <param name="preSortDic">排序前的字典集合</param>
        /// <returns>排序后的字典集合</returns>
        public static Dictionary<string, TValue> ASCIISort<TValue>(this Dictionary<string, TValue> preSortDic)
        {
            Dictionary<string, TValue> sortedDic = new Dictionary<string, TValue>();
            string[] arrKeys = preSortDic.Keys.ToArray();
            Array.Sort(arrKeys, string.CompareOrdinal);
            arrKeys.Foreach(key =>
            {
                sortedDic.Add(key, preSortDic[key]);
            });
            return sortedDic;
        }
    }
}
