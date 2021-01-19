using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;
using log4net;
using Model.Pay;

namespace Common
{
    /// <summary>
    /// 验签和加签工具类
    /// </summary>
    public class SignatureAndVerification
    {
        private ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 读取cer并验证公钥签名
        /// </summary>
        /// <param name="requsetBody">json报文数据</param>
        /// <param name="signature">加签标识</param>
        /// <returns>成功：true，失败：false</returns>
        public bool read_cer_and_verify_sign(string requsetBody, string signature)
        {
            bool result = false;
            try
            {
                byte[] orgin = Encoding.UTF8.GetBytes((requsetBody));//json报文数据获得字节数据
                byte[] singedBase64 = Convert.FromBase64String((signature));//对加签部分进行base64解密操作
                //读取证书
                string path = @"D:\87654321.pfx";
                RSACryptoServiceProvider tMerchantKey = GetPublicKey(path);
                result = tMerchantKey.VerifyData(orgin, "SHA1", singedBase64);
                return result;
            }
            catch (Exception e)
            {
                logger.Info("验签失败！", e);
                return result;
            }
        }

        /// <summary>
        /// 加签名
        /// </summary>
        /// <param name="contentForSign">需加标签的字符串</param>
        /// <returns></returns>
        public string signWhithsha1withrsa(string contentForSign)
        {
            string result = "";
            try
            {
                //string filePath = rootPath + PFXPATH;
                string path = @"D:\87654321.pfx";
                //获取私钥
                RSACryptoServiceProvider tMerchantKey = GetPrivateKey(path);
                SHA1Managed tHash = new SHA1Managed();
                //将传递需要加签的字符串进行base64操作
                byte[] base64 = Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(contentForSign)));
                byte[] tHashedData = tHash.ComputeHash(base64);
                //对其进行加签名
                byte[] tSigned = tMerchantKey.SignHash(tHashedData, "SHA1");
                result = Convert.ToBase64String(tSigned);
                return result;
            }
            catch (Exception e)
            {
                logger.Info("加签失败！", e);
                return result;
            }
        }

        public string signWhithsha1withrsa(string contentForSign, AutoSign autoSign)
        {
            string result = "";
            try
            {
                //string filePath = rootPath + PFXPATH;
                string path = autoSign.Url;
                //获取私钥
                RSACryptoServiceProvider tMerchantKey = GetPrivateKey(autoSign.PassWord, path);
                SHA1Managed tHash = new SHA1Managed();
                //将传递需要加签的字符串进行base64操作
                byte[] base64 = Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(contentForSign)));
                byte[] tHashedData = tHash.ComputeHash(base64);
                //对其进行加签名
                byte[] tSigned = tMerchantKey.SignHash(tHashedData, "SHA1");
                result = Convert.ToBase64String(tSigned);
                return result;
            }
            catch (Exception e)
            {
                logger.Info("加签失败！", e);
                return result;
            }
        }

        /// <summary>
        /// 获取私钥
        /// </summary>
        /// <returns></returns>
        private static RSACryptoServiceProvider GetPrivateKey(string path = "")
        {
            try
            {
                /*
                 * var cer = new X509Certificate2(File.ReadAllBytes(path), Resource.keystore_password, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);
                 * 以读取路径的方式读取文件 path为存放证书路径
                 * 
                */
                //byte[] rawData = Resource._103881104410001;
                //byte[]  rawData = returnbyte("d://103881104410001.pfx");
                //string file = "d://103881104410001.pfx";
                //var cer = new X509Certificate2(rawData, Resource.keystore_password, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);
                var cer = new X509Certificate2(File.ReadAllBytes(path), "87654321");
                return (RSACryptoServiceProvider)cer.PrivateKey;
            }
            catch { throw; }
        }

        private static RSACryptoServiceProvider GetPrivateKey(string passWord, string path = "")
        {
            try
            {
                var cer = new X509Certificate2(File.ReadAllBytes(path), passWord);
                return (RSACryptoServiceProvider)cer.PrivateKey;
            }
            catch { throw; }
        }

        public static byte[] returnbyte(string filePath)
        {
            FileStream fsMyfile = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryReader brMyfile = new BinaryReader(fsMyfile);
            brMyfile.BaseStream.Seek(0, SeekOrigin.Begin);
            byte[] bytes = brMyfile.ReadBytes(Convert.ToInt32(fsMyfile.Length.ToString()));
            brMyfile.Close();

            return bytes;
        }


        /// <summary>
        /// 获取公钥
        /// </summary>
        /// <returns></returns>
        private static RSACryptoServiceProvider GetPublicKey(string path = "")
        {
            try
            {
                /*
                * var cer = new X509Certificate2(File.ReadAllBytes(path));
                * 以读取路径的方式读取文件 path为存放证书路径
                * 
               */
                //var cer = new X509Certificate2(Resource.TrustPayTest);
                var cer = new X509Certificate2(File.ReadAllBytes(path), "87654321");
                return (RSACryptoServiceProvider)cer.PublicKey.Key;
            }
            catch { throw; }
        }

        /// <summary>
        /// 接收报文返回requsetBody和使用base64解析后的requsetBody以及缴费中心传送的签名
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Dictionary<string, string> requestBodyOfBase64(string request)
        {
            Dictionary<string, string> requestMap = new Dictionary<string, string>();

            //使用base64解析完成后的requsetBody
            requestMap.Add("requsetBodyOfDecoded", "");
            //解析前的requsetBody
            requestMap.Add("requsetBody", "");
            //获取缴费中心传送过来的签名
            requestMap.Add("signatureString", "");

            if (logger.IsWarnEnabled)
            {
                logger.WarnFormat("收到的报文：{0}", request);
            }
            try
            {
                string signatureString = request.Substring(0, request.IndexOf("||"));
                logger.WarnFormat("截取报文的signatureString:", signatureString);
                string requsetBody = request.Substring(signatureString.Length + 2);
                logger.WarnFormat("截取报文的requsetBody:", requsetBody);
                string requsetBodyOfDecoded = Base64Util.DecodeData(requsetBody);
                //System.out.println("-----解析完成后的requsetBody-------" + requsetBodyOfDecoded);
                //使用base64解析完成后的requsetBody
                requestMap["requsetBodyOfDecoded"] = requsetBodyOfDecoded;
                //解析前的requsetBody
                requestMap["requsetBody"] = requsetBody;
                //获取缴费中心传送过来的签名
                requestMap["signatureString"] = signatureString;
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                logger.Info(e.StackTrace);
            }
            return requestMap;
        }
    }
}
