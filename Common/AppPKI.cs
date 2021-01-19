﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;


namespace Business
{
    public class AppPKI
    {
        #region 基础方法

        #region 签名
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="plainText">待签名的数据明文</param>
        /// <param name="cert">带私钥信息的证书</param>
        /// <returns>签名数据</returns>
        public static byte[] SignPKCS1(String plainText, X509Certificate2 cert)
        {
            //取出私钥
            RSACryptoServiceProvider rsa = cert.PrivateKey as RSACryptoServiceProvider;

            //将要签名的要素转化为byte[]
            byte[] data = Encoding.UTF8.GetBytes(plainText);

            return rsa.SignData(data, "SHA1");
        }

        #endregion

        #region 验签
        /// <summary>
        /// 验签
        /// </summary>
        /// <param name="plainText">数据明文</param>
        /// <param name="signB">签名数据</param>
        /// <param name="cert">带公钥信息的证书</param>
        /// <returns></returns>
        public static Boolean VerifyPKCS1(String plainText, byte[] signB, X509Certificate2 cert)
        {
            //取出公钥
            RSACryptoServiceProvider rsa = cert.PublicKey.Key as RSACryptoServiceProvider;

            //转成UTF-8
            byte[] data = Encoding.UTF8.GetBytes(plainText);

            return rsa.VerifyData(data, "SHA1", signB);
        }
        #endregion

        #region Base64解编码
        /// <summary>
        /// Base64解编码
        /// </summary>
        /// <param name="src">base64字符串</param>
        /// <returns>源字符数组</returns>
        public static byte[] base64Decode(String src)
        {
            return System.Convert.FromBase64String(src);
        }
        #endregion

        #region Base64解编码
        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="src">源字符数组</param>
        /// <returns>base64字符串</returns>
        public static String base64Encode(byte[] src)
        {
            return System.Convert.ToBase64String(src);
        }
        #endregion

        #region MD5 Hash
        public static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object. 
            MD5 md5Hasher = MD5.Create();
            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes        
            // and create a string.        
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data         
            // and format each one as a hexadecimal string.        
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        #endregion

        #region 验证MD5
        // Verify a hash against a string.
        public static bool verifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = getMd5Hash(input);
            // Create a StringComparer an comare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        #region AES加解密
        #region AES加密
        public static string AESEncrypt(string input, string key) {
            if (string.IsNullOrEmpty(input)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(input);
            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = System.Security.Cryptography.CipherMode.ECB,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };
            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        #endregion
        #region AES解密
        public static string AESDecrypt(string input, string key)
        {
            if (string.IsNullOrEmpty(input)) return null;
            Byte[] toEncryptArray = Convert.FromBase64String(input);

            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = System.Security.Cryptography.CipherMode.ECB,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };

            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }
        #endregion
        #endregion
        #endregion


    }
}