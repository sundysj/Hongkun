using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Business
{
    /// <summary>
    /// DataProtect 的摘要说明。
    /// </summary>
    public class DataProtect
    {
        #region 私有变量

        //对称加密算法(系统自带)
        private string[] sma = new string[4] { "DES", "TripleDES", "RC2", "Rijndael" };

        private string _key = "66852318";
        private string _iv = "51873131";

        #endregion

        #region 公共属性
        /// <summary>
        /// 密码
        /// </summary>
        public string Key
        {
            get { return _key; }
        }

        /// <summary>
        /// 向量
        /// </summary>
        public string Iv
        {
            get { return _iv; }
        }

        #endregion

        public DataProtect()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 字节转化为16进制字符串
        private string BytesToString(byte[] bytes)
        {
            StringBuilder strb = new StringBuilder("");
            for (int i = 0; i < bytes.Length; ++i)
            {
                strb.Append(bytes[i].ToString("x2"));
            }
            return strb.ToString();

        }
        #endregion

        #region 16进制字符串转化为字节
        private byte[] StringToBytes(string strb)
        {

            int len = strb.Length;
            int bcount = Convert.ToInt32(Math.Ceiling(len / 2.0));

            //**补齐位数
            strb.PadRight(bcount * 2, '0');

            byte[] bytes = new byte[bcount];

            //**按每2个字符转化为字节
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(strb.Substring(i * 2, 2), 16);
            }

            return bytes;

        }
        #endregion

        #region 字符串转化为字节
        private byte[] GetBytes(string str)
        {
            Encoding gb = System.Text.Encoding.GetEncoding("utf-8");
            byte[] result = gb.GetBytes(str);
            return result;
        }
        #endregion

        #region 产生随机密码

        #region 随机产生对称加密的钥匙KEY和向量IV
        //随机产生对称加密的钥匙KEY和向量IV
        private byte[] RandomKey(int choice)
        {
            SymmetricAlgorithm crypt = SymmetricAlgorithm.Create(sma[choice]);
            crypt.GenerateKey();
            return crypt.Key;
        }
        private byte[] RandomIV(int choice)
        {
            SymmetricAlgorithm crypt = SymmetricAlgorithm.Create(sma[choice]);
            crypt.GenerateIV();
            return crypt.IV;
        }
        #endregion

        #endregion

        #region 对称加密{"DES","TripleDES","RC2","Rijndael"};

        #region 加密函数(数组)

        private byte[] EncryptData(int choice, byte[] Key, byte[] IV, byte[] toEncryptData)
        {
            try
            {
                SymmetricAlgorithm crypt = SymmetricAlgorithm.Create(sma[choice]);
                ICryptoTransform transform = crypt.CreateEncryptor(Key, IV);//这里不一样	

                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, transform, CryptoStreamMode.Write);
                //Convert the data to a byte array.
                //Write all data to the crypto stream and flush it.
                cs.Write(toEncryptData, 0, toEncryptData.Length);
                cs.FlushFinalBlock();
                //Get encrypted array of bytes.
                return ms.ToArray();
            }
            catch
            {
                throw new CryptographicException("加密出现失误，请重新检查输入！");
            }
        }
        #endregion

        #region 解密数据(数组)
        //解密数据，根据思路，此时byte[]Key,byte[]IV这些参数不再是随机的数据而是固定的数据啦。
        private byte[] DecryptData(int choice, byte[] Key, byte[] IV, byte[] toDecryptedData)
        {
            try
            {
                SymmetricAlgorithm crypt = SymmetricAlgorithm.Create(sma[choice]);
                ICryptoTransform transform = crypt.CreateDecryptor(Key, IV);//这里不一样	
                MemoryStream ms = new MemoryStream();
                //重写这部分
                CryptoStream cs = new CryptoStream(ms, transform, CryptoStreamMode.Write);
                //Read the data out of the crypto stream.
                cs.Write(toDecryptedData, 0, toDecryptedData.Length);
                cs.FlushFinalBlock();
                return ms.ToArray();
            }
            catch
            {
                throw new CryptographicException("解密数据流时，出现错误，请检查您的输入！");
            }
        }
        #endregion

        #endregion

        #region 对称加解密文本
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="PlainTxt">明文</param>
        /// <returns></returns>
        public string Encrypt(int choice, string PlainTxt)
        {
            byte[] KEY = GetBytes(_key);
            byte[] IV = GetBytes(_iv);

            System.Text.Encoding gb = System.Text.Encoding.GetEncoding("utf-8");
            byte[] bytes = gb.GetBytes(PlainTxt);

            byte[] EncryptionDate = EncryptData(choice, KEY, IV, bytes);

            return BytesToString(EncryptionDate);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="CryptTxt">密文</param>
        /// <returns></returns>
        public string Decrypt(int choice, string CryptTxt)
        {
            byte[] KEY = GetBytes(_key);
            byte[] IV = GetBytes(_iv);

            System.Text.Encoding gb = System.Text.Encoding.GetEncoding("utf-8");

            byte[] bytecipher = StringToBytes(CryptTxt);

            byte[] bytes = DecryptData(choice, KEY, IV, bytecipher);

            return gb.GetString(bytes);
        }
        #endregion

        #region 对称加解密文本(带密码)
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="PlainTxt">明文</param>
        /// <returns></returns>
        public string Encrypt(int choice, string PlainTxt, string key, string iv)
        {
            byte[] KEY = GetBytes(key);
            byte[] IV = GetBytes(iv);

            System.Text.Encoding gb = System.Text.Encoding.GetEncoding("utf-8");
            byte[] bytes = gb.GetBytes(PlainTxt);

            byte[] EncryptionDate = EncryptData(choice, KEY, IV, bytes);

            return BytesToString(EncryptionDate);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="CryptTxt">密文</param>
        /// <returns></returns>
        public string Decrypt(int choice, string CryptTxt, string key, string iv)
        {
            byte[] KEY = GetBytes(key);
            byte[] IV = GetBytes(iv);

            System.Text.Encoding gb = System.Text.Encoding.GetEncoding("utf-8");

            byte[] bytecipher = StringToBytes(CryptTxt);

            byte[] bytes = DecryptData(choice, KEY, IV, bytecipher);

            return gb.GetString(bytes);
        }
        #endregion
    }
}
