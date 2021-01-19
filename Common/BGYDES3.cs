using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Common
{
    public class BGYDES3
    {
        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString, string encryptKey, string _rgbIV)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey);
                byte[] rgbIV = Encoding.UTF8.GetBytes(_rgbIV);
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                MemoryStream mStream = new MemoryStream();
                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.CBC;             //默认值
                tdsp.Padding = PaddingMode.PKCS7;       //默认值
                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(mStream,
                    tdsp.CreateEncryptor(rgbKey, rgbIV),
                    CryptoStreamMode.Write);
                // Write the byte array to the crypto stream and flush it.
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                // Get an array of bytes from the 
                // MemoryStream that holds the 
                // encrypted data.
                byte[] ret = mStream.ToArray();
                // Close the streams.
                cStream.Close();
                mStream.Close();
                // Return the encrypted buffer.
                return Convert.ToBase64String(ret);
            }
            catch
            {
                return encryptString;
            }
        }


        public static string EncryptDESTime(double time, string encryptKey, string _rgbIV)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey);
                byte[] rgbIV = Encoding.UTF8.GetBytes(_rgbIV);
                byte[] inputByteArray = BitConverter.GetBytes(time); 
                MemoryStream mStream = new MemoryStream();
                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.CBC;             //默认值
                tdsp.Padding = PaddingMode.PKCS7;       //默认值
                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(mStream,
                    tdsp.CreateEncryptor(rgbKey, rgbIV),
                    CryptoStreamMode.Write);
                // Write the byte array to the crypto stream and flush it.
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                // Get an array of bytes from the 
                // MemoryStream that holds the 
                // encrypted data.
                byte[] ret = mStream.ToArray();
                // Close the streams.
                cStream.Close();
                mStream.Close();
                // Return the encrypted buffer.
                return Convert.ToBase64String(ret);
            }
            catch
            {
                return time.ToString();
            }
        }
        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为64位,和加密密钥相同</param>
        /// <param name="decryptKey">密钥向量,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey, string _rgbIV)
        {
            try
            {

                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Encoding.UTF8.GetBytes(_rgbIV);
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                // Create a MemoryStream.
                MemoryStream msDecrypt = new MemoryStream(inputByteArray);
                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.CBC;
                tdsp.Padding = PaddingMode.PKCS7;
                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    tdsp.CreateDecryptor(rgbKey, rgbIV),
                    CryptoStreamMode.Read);
                // Create buffer to hold the decrypted data.
                byte[] fromEncrypt = new byte[inputByteArray.Length];
                // Read the decrypted data out of the crypto stream
                // and place it into the temporary buffer.

                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);
                //Convert the buffer into a string and return it.


                // Return the encrypted buffer.
                return Encoding.UTF8.GetString(fromEncrypt).TrimEnd('\0');
            }
            catch
            {
                return decryptString;
            }
        }

        public static string Encrypt3DES(string a_strString, string a_strKey)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            DES.Key = ASCIIEncoding.ASCII.GetBytes(a_strKey);
            DES.Mode = CipherMode.ECB;

            ICryptoTransform DESEncrypt = DES.CreateEncryptor();

            // byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(a_strString);
            byte[] Buffer = Encoding.Default.GetBytes(a_strString);
            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        public static string Decrypt3DES(string a_strString, string a_strKey)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            DES.Key = ASCIIEncoding.ASCII.GetBytes(a_strKey);
            DES.Mode = CipherMode.ECB;
            DES.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

            ICryptoTransform DESDecrypt = DES.CreateDecryptor();

            string result = "";
            try
            {
                byte[] Buffer = Convert.FromBase64String(a_strString);
                result = Encoding.Default.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
                //  result = ASCIIEncoding.ASCII.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (Exception e)
            {

            }

            return result;
        }

    }
}
