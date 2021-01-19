using System.Web.Configuration;
using System.Configuration;

namespace com.unionpay.acp.sdk
{
    public class SDKConfig
    {
        private static Configuration config = WebConfigurationManager.OpenWebConfiguration("~");

        public  string signCertPath = "C:\\Verify\\700000000000001_acp.pfx";  //功能：读取配置文件获取签名证书路径
        public  string signCertPwd = "000000";//功能：读取配置文件获取签名证书密码
        public  string validateCertDir = "C:\\Verify\\";//功能：读取配置文件获取验签目录
        public  string encryptCert = "C:\\Verify\\acp_test_verify.cer";  //功能：加密公钥证书路径
        public  string merId = "";  //商户号
        public  string AccNo = "";  //银行卡号

        public  string frontUrl = "";//功能：读取配置文件获取前台通知地址
        public  string backUrl = "";//功能：读取配置文件获取前台通知地址

        public  string appRequestUrl = "";  //app交易地址 手机控件支付使用该地址;
        private  string cardRequestUrl = "";  //功能：有卡交易路径;
        public  string singleQueryUrl = ""; //功能：读取配置文件获取交易查询地址
        private  string fileTransUrl = "";  //功能：读取配置文件获取文件传输类交易地址
        private  string frontTransUrl = ""; //功能：读取配置文件获取前台交易地址
        private  string backTransUrl = "";//功能：读取配置文件获取后台交易地址
        private  string batTransUrl = "";//功能：读取配批量交易地址
        private  string jfAppRequestUrl = "";  //功能：缴费产品app交易路径;
        private  string jfSingleQueryUrl = ""; //功能：读取配置文件获取缴费产品交易查询地址
        private  string jfFrontTransUrl = ""; //功能：读取配置文件获取缴费产品前台交易地址
        private  string jfBackTransUrl = "";//功能：读取配置文件获取缴费产品后台交易地址

        private  string ifValidateRemoteCert = "false";//功能：是否验证后台https证书
        
        public  string CardRequestUrl
        {
            get { return cardRequestUrl; }
            set { cardRequestUrl = value; }
        }
        public  string AppRequestUrl
        {
            get { return appRequestUrl; }
            set { appRequestUrl = value; }
        }

        public  string FrontTransUrl
        {
            get { return frontTransUrl; }
            set { frontTransUrl = value; }
        }
        public  string EncryptCert
        {
            get { return encryptCert; }
            set { encryptCert = value; }
        }


        public  string BackTransUrl
        {
            get { return backTransUrl; }
            set { backTransUrl = value; }
        }

        public  string SingleQueryUrl
        {
            get { return singleQueryUrl; }
            set { singleQueryUrl = value; }
        }

        public  string FileTransUrl
        {
            get { return fileTransUrl; }
            set { fileTransUrl = value; }
        }

        public  string SignCertPath
        {
            get { return signCertPath; }
            set { signCertPath = value; }
        }

        public  string SignCertPwd
        {
            get { return signCertPwd; }
            set { signCertPwd = value; }
        }

        public  string ValidateCertDir
        {
            get { return validateCertDir; }
            set { validateCertDir = value; }
        }
        public  string BatTransUrl
        {
            get { return batTransUrl; }
            set { batTransUrl = value; }
        }
        public  string BackUrl
        {
            get { return backUrl; }
            set { backUrl = value; }
        }
        public  string FrontUrl
        {
            get { return frontUrl; }
            set { frontUrl = value; }
        }
        public  string JfCardRequestUrl
        {
            get { return cardRequestUrl; }
            set { cardRequestUrl = value; }
        }
        public  string JfAppRequestUrl
        {
            get { return jfAppRequestUrl; }
            set { jfAppRequestUrl = value; }
        }

        public  string JfFrontTransUrl
        {
            get { return jfFrontTransUrl; }
            set { jfFrontTransUrl = value; }
        }
        public  string JfBackTransUrl
        {
            get { return jfBackTransUrl; }
            set { jfBackTransUrl = value; }
        }
        public  string JfSingleQueryUrl
        {
            get { return jfSingleQueryUrl; }
            set { jfSingleQueryUrl = value; }
        }

        public  string IfValidateRemoteCert
        {
            get { return ifValidateRemoteCert; }
            set { ifValidateRemoteCert = value; }
        }
    }
}