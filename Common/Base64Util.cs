using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 将String进行base64编码解码，使用utf-8
    /// </summary>
    public class Base64Util
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 对给定的字符串进行base64解码操作
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static string DecodeData(string inputData)
        {
            try
            {
                if (null == inputData)
                {
                    return null;
                }
                return DecodeBase64(Encoding.UTF8, inputData);
            }
            catch (EncoderFallbackException e)
            {
                logger.Error(inputData, e);
            }

            return null;
        }

        private static string DecodeBase64(Encoding encode, string result)
        {
            string decode = string.Empty;
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encode.GetString(bytes);
            }
            catch { throw; }
            return decode;
        }

        /// <summary>
        /// 对给定的字符串进行base64加密操作
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static string EncodeData(string inputData)
        {
            try
            {
                if (null == inputData)
                {
                    return null;
                }
                return EncodeBase64(Encoding.UTF8, inputData);
            }
            catch (EncoderFallbackException e)
            {
                logger.Error(inputData, e);
            }
            return null;
        }

        private static string EncodeBase64(Encoding encode, string data)
        {
            string eEncodeE = string.Empty;
            byte[] bytes = encode.GetBytes(data);
            try
            {
                eEncodeE = Convert.ToBase64String(bytes);
            }
            catch { throw; }
            return eEncodeE;
        }

    }

}
