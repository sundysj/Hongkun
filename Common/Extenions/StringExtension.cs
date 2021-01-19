using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extenions
{
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="str">当前字符串</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str?.Trim());
        }

        /// <summary>
        /// 判断字符串是否非空
        /// </summary>
        /// <param name="str">当前字符串</param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str?.Trim());
        }

        /// <summary>
        /// 字符串格式化
        /// </summary>
        /// <param name="str">当前字符串</param>
        /// <param name="arg0">参数0</param>
        /// <returns>格式化后的字符串</returns>
        public static string FormatWith(this string str, object arg0)
        {
            return string.Format(str, arg0);
        }

        /// <summary>
        /// 字符串格式化
        /// </summary>
        /// <param name="str">当前字符串</param>
        /// <param name="formatStr">格式</param>
        /// <returns>格式化后的字符串</returns>
        public static string FormatWith(this string str, string formatStr)
        {
            return string.Format(formatStr, str);
        }


        /// <summary>
        /// 字符串格式化
        /// </summary>
        /// <param name="str">当前字符串</param>
        /// <param name="arg0">参数0</param>
        /// <param name="arg1">参数1</param>
        /// <returns>格式化后的字符串</returns>
        public static string FormatWith(this string str, object arg0, object arg1)
        {
            return string.Format(str, arg0, arg1);
        }

        /// <summary>
        /// 字符串格式化
        /// </summary>
        /// <param name="str">当前字符串</param>
        /// <param name="args">参数集</param>
        /// <returns>格式化后的字符串</returns>
        public static string FormatWith(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        /// <summary>
        /// 转换为驼峰命名格式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            if (!char.IsUpper(s[0])) return s;

            var chars = s.ToCharArray();
            for (var i = 0; i < chars.Length; i++)
            {
                var hasNext = (i + 1 < chars.Length);
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1])) break;
                chars[i] = char.ToLower(chars[i], CultureInfo.InvariantCulture);
            }
            return new string(chars);
        }

        /// <summary>
        /// 转换为url命名格式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToUrlCase(this string s)
        {
            if (string.IsNullOrEmpty(s)) return s;

            var chars = s.ToCharArray();
            var str = new StringBuilder();
            for (var i = 0; i < chars.Length; i++)
            {
                if (char.IsUpper(chars[i]))
                {
                    if (i > 0 && !char.IsUpper(chars[i - 1])) str.Append("_");
                    str.Append(char.ToLower(chars[i], CultureInfo.InvariantCulture));
                }
                else
                {
                    str.Append(chars[i]);
                }
            }
            return str.ToString();
        }

        /// <summary>
        /// MD5方式加密字符串,全小写形式
        /// </summary>
        /// <param name="str">待加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string GetMd5Hash(this string str)
        {
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString().ToLower();
        }
    }
}
