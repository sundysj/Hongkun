using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Xml;
using System.Net.Mail;
using Business;
using MobileSoft.Common;


using System.Linq;
using System.Web;
using Newtonsoft.Json;
using log4net;
using Libraries;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Data.SqlClient;
using MobileSoft.DBUtility;
using Dapper;
using System.Collections.Specialized;
using Aliyun.OpenServices.OpenStorageService;
using BGYPlatformV1.Domain.Common;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.tabControl1.SelectedTab = this.tabPage7;
        }

        #region CheckValidationResult
        private bool CheckValidationResult(
            Object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors
        )
        {
            if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNotAvailable) != SslPolicyErrors.RemoteCertificateNotAvailable)
                return true;
            throw new Exception("SSL验证失败");
        }
        #endregion

        #region 发送http
        public string SendHttp(string Url, string Contents)
        {
            #region 发送
            HttpWebRequest request = null;
            byte[] postData;
            Uri uri = new Uri(Url);
            if (uri.Scheme == "https")
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.CheckValidationResult);
            }
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate);
            HttpWebRequest.DefaultCachePolicy = policy;

            request = (HttpWebRequest)WebRequest.Create(uri);
            request.AllowAutoRedirect = false;
            request.AllowWriteStreamBuffering = false;
            request.Method = "POST";

            postData = Encoding.GetEncoding("utf-8").GetBytes(Contents);

            request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentType = "text/plain;charset = utf-8"; //request.ContentType = "text/plain";
            request.ContentLength = postData.Length;
            request.KeepAlive = false;

            Stream reqStream = request.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();
            #endregion

            #region 响应
            string respText = "";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();

            MemoryStream ms = new MemoryStream();
            byte[] buf = new byte[4096];
            int count;
            while ((count = resStream.Read(buf, 0, buf.Length)) > 0)
            {
                ms.Write(buf, 0, count);
            }
            resStream.Close();
            respText = Encoding.GetEncoding("utf-8").GetString(ms.ToArray());
            #endregion

            return respText;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Result.Text = "http://localhost:80/TWInterface/Service/Service.ashx";
            string repsTxt = "";
            try
            {
                string strDoUrl = Result.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160321LOGIN";
                // string Attribute = "<attributes><Net>2</Net><Account>LFUser</Account><LoginPwd>LF123SPoss</LoginPwd></attributes>";
                string Attribute = "<attributes><Net>1</Net><Account>test12345</Account><LoginPwd>test12345</LoginPwd></attributes>";
                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=Login&Command=1&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        public string CreMAC(string Attribute, string Date, string Token)
        {
            string Ret = AppPKI.getMd5Hash(Attribute + Date + Token);
            return Ret;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160321LOGIN";
                string Attribute = "<attributes><Net>1</Net><Account>1973-test12345</Account><LoginPwd>test12345</LoginPwd></attributes>";
                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=Login&Command=1&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            //string strCorpName = "成都西南物业管理有限公司（演示）";
            //string strCustID = "10001300000002";

            TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";
            string strCorpName = "大连亿达物业";//"大连亿达物业";      //
            string strCustID = "18130100000001";//"";  //

            //TbURL.Text = "http://125.64.9.118:8888/Service.ashx";
            //string strCorpName = "成都家园经营管理有限公司";//"联创未来";      //
            //string strCustID = "13360300000001";//"10000100000001";  //

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160324QualityManage";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>1</Net>");
                Attribute.Append("<CorpName>" + strCorpName + "</CorpName>");
                Attribute.Append("<CustID>" + strCustID + "</CustID>");
                Attribute.Append("<RoomID>0</RoomID>");
                Attribute.Append("<BeginDate></BeginDate>");
                Attribute.Append("<EndDate></EndDate>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=FeesPeriodSearch&Command=FeesList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            string strCorpName = "成都西南物业管理有限公司（演示）";
            string strCustID = "10001300000002";

            //TbURL.Text = "http://125.64.9.118:8888/Service.ashx";
            //string strCorpName = "联创未来";
            //string strCustID = "10000100000001";

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160324QualityManage";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>1</Net>");
                Attribute.Append("<CorpName>" + strCorpName + "</CorpName>");
                Attribute.Append("<CustID>" + strCustID + "</CustID>");
                Attribute.Append("<RoomID></RoomID>");
                Attribute.Append("<BeginDate></BeginDate>");
                Attribute.Append("<EndDate></EndDate>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=FeesPaidSearch&Command=FeesList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            string strCorpName = "成都西南物业管理有限公司（演示）";
            string strCustID = "10001300000002";

            //TbURL.Text = "http://125.64.9.118:8888/Service.ashx";
            //string strCorpName = "联创未来";
            //string strCustID = "10000100000001";

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160324QualityManage";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>1</Net>");
                Attribute.Append("<CorpName>" + strCorpName + "</CorpName>");
                Attribute.Append("<CustID>" + strCustID + "</CustID>");
                Attribute.Append("<RoomID>0</RoomID>");
                Attribute.Append("<BeginDate></BeginDate>");
                Attribute.Append("<EndDate></EndDate>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=PrecFeeSearch&Command=FeesList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Result.Text = "";
            string repsTxt = "";
            string strUrls = "http://localhost:80/TWInterface/Service/Service.ashx";
            try
            {

                string strDoUrl = strUrls;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160324QualityManage";
                int bools = 0;
                string Attribute = "<attributes><Net>2</Net><Account>1000-az</Account><LoginPwd>1</LoginPwd><data>{\"TaskId\":\"fdc53dd2-f162-402c-8467-e5a0b5be5d12\",";
                Attribute += "\"Points\":[{\"PointId\":\"97b0b8a0-ffd9-4af0-8e42-7a7acfee134a\",\"AddTime\":\"2016-9-8 15:01:01\",\"AddPId\":\"000081\"},";
                Attribute += "\"Record\":[{\"id\":0,\"TaskId\":\"fdc53dd2-f162-402c-8467-e5a0b5be5d12\",\"CheckNote\":\"检查1..电梯\",\"CheckRemark\":\"电梯里边有垃圾\",\"CheckResult\":0,\"ProblemType\":\"整改类型\",\"RectificationNote\":\"测试\",\"AbarPIdName\":\"艾中\",\"RectificationPeriod\":\"测试2\",\"ReducePIdName\":\"艾中\",\"PointIds\":\"\"},{\"id\":0,\"TaskId\":\"fdc53dd2-f162-402c-8467-e5a0b5be5d12\",\"CheckNote\":\"检查1..电梯\",\"CheckRemark\":\"电梯里边有垃圾\",\"CheckResult\":0,\"ProblemType\":\"整改类型\",\"RectificationNote\":\"测试\",\"AbarPIdName\":\"艾中\",\"RectificationPeriod\":\"测试2\",\"ReducePIdName\":\"艾中\",\"PointIds\":\"\"}]}</data></attributes>";
                if (TbURL.Text.Trim() == "1")
                {
                    Attribute = DesUrl.Text;
                    bools = 1;
                }
                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=QualityManage&Command=GetQualityInspectSave&Attribute=" + Attribute + "&Mac=" + Mac;

                string strNewUrl = strDoUrl + "?" + strContent;

                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void DesUrl_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            // TbURL.Text = "http://localhost:80/TWInterface/Service/Service.ashx";
            // Result.Text = "";
            string repsTxt = "";
            //try
            //{
            //    string strDoUrl = TbURL.Text;
            //    string strDate = DateTime.Now.ToString("yyyyMMdd");
            //    string strToken = "20160324QualityManage";
            //    string Attribute = "<attributes><Net>2</Net><Account>1000-az</Account><LoginPwd>1</LoginPwd>";
            //    Attribute += "<ItemCode>100013</ItemCode><UserCode>000081</UserCode>";
            //    Attribute += "<Timer>2016-12-19 00:00:00.000</Timer></attributes>";
            //    string Mac = CreMAC(Attribute, strDate, strToken);
            //    string strContent = "Class=QualityManage&Command=GetQualityInspectList&Attribute=" + Attribute + "&Mac=" + Mac;
            //    string strNewUrl = strDoUrl + "?" + strContent;
            //    if (DesUrl.Text != "")
            //    {
            //        strNewUrl = DesUrl.Text;
            //    }
            //    DesUrl.Text = strNewUrl;
            //    repsTxt = SendHttp(strNewUrl, "");
            //}
            try
            {
                string strDoUrl = "http://localhost:80/TWInterface/Service/Service.ashx";
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160324QualityManage";
                string Attribute = "<attributes><Net>1</Net><Account>1331-lixianrong</Account><LoginPwd>lxr721650</LoginPwd>";
                Attribute += "<ItemCode>133140</ItemCode><UserCode>000584</UserCode>";
                Attribute += "</attributes>";
                if (TbURL.Text.Trim() == "1")
                {
                    Attribute = DesUrl.Text;
                }
                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=QualityManage&Command=GetQualityInspectListNew&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;

                repsTxt = SendHttp(strNewUrl, "");
                ////////////

            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160324QualityManage";
                string Attribute = "<attributes><Net>2</Net><Account>1000-az</Account><LoginPwd>1</LoginPwd>";
                Attribute += "<CommID>100013</CommID></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=QualityManage&Command=GetUserByCommId&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160324QualityManage";
                string Attribute = "<attributes><Net>2</Net><Account>1000-az</Account><LoginPwd>1</LoginPwd></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=QualityManage&Command=GetProblem&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }
        private void button77_Click(object sender, EventArgs e)
        {
            UnionPay_Txt_1.Text = "http://localhost:8909/Service/Service.ashx";
            UnionPay_Txt_3.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = UnionPay_Txt_1.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804AlipayPrec";


                string Attribute = "<attributes><CommunityId>" + UnionPay_CommunityId.Text.ToString() + "</CommunityId><OrderId>" + UnionPay_OrderId.Text.ToString() + "</OrderId><respCode>" + UnionPay_respCode.Text.ToString() + "</respCode><respMsg>" + UnionPay_respMsg.Text.ToString() + "</respMsg><CostID>" + CostID.Text.ToString() + "</CostID><RoomID>" + RoomID.Text.ToString() + "</RoomID><Amount>1000</Amount></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=AlipayPrec&Command=ReceProperyOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                UnionPay_Txt_2.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            UnionPay_Txt_3.Text = Result.Text + "返回：" + repsTxt;
        }
        private void button78_Click(object sender, EventArgs e)
        {
            UnionPay_Txt_1.Text = "http://localhost:8909/Service/Service.ashx";
            UnionPay_Txt_3.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = UnionPay_Txt_1.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804UnionPayPrec";


                string Attribute = "<attributes><CommunityId>" + UnionPay_CommunityId.Text.ToString() + "</CommunityId><OrderId>" + UnionPay_OrderId.Text.ToString() + "</OrderId><respCode>" + UnionPay_respCode.Text.ToString() + "</respCode><respMsg>" + UnionPay_respMsg.Text.ToString() + "</respMsg><CostID>" + CostID.Text.ToString() + "</CostID><RoomID>" + RoomID.Text.ToString() + "</RoomID><Amount>1000</Amount></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=UnionPayPrec&Command=ReceProperyOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                UnionPay_Txt_2.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            UnionPay_Txt_3.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //TbURL.Text =
            //Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = "http://localhost:80/TWInterface/Service/Service.ashx";
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160324QualityManage";

                string Attribute = "<attributes><Net>2</Net><Account>1000-az</Account><LoginPwd>1</LoginPwd>";
                Attribute += "<ItemCode>100013</ItemCode><UserCode>000081</UserCode>";
                Attribute += "<Timer>2016-12-19 00:00:00.000</Timer><AbarPId>000062</AbarPId></attributes>";
                //string Attribute = "<attributes><Net>2</Net><Account>1000-az</Account><LoginPwd>1</LoginPwd><AbarPId>000062</AbarPId></attributes>";
                if (TbURL.Text.Trim() == "1")
                {
                    Attribute = DesUrl.Text;
                }
                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=QualityManage&Command=GetTaskAbarbeitungList&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";
            string strMobilePhone = "13998639512";     //通过SQI_bs查询的手机号返回CommID  12上:13568955613


            //TbURL.Text = "http://125.64.9.118:8888/Service.ashx";
            //string strCorpName = "成都家园经营管理有限公司";//"联创未来";      //
            //string strCustID = "13360300000001";//"10000100000001";  //

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160412XPManage";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>1</Net>");
                Attribute.Append("<MobilePhone>" + strMobilePhone + "</MobilePhone>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=MobilePhoneVerification&Command=OwnerMobilePhoneList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";

            string strUnCustID = "210200019900";//业主账号 亿达服务器 1813
            string strCorpName = "大连亿达物业";//公司名称


            //TbURL.Text = "http://125.64.9.118:8888/Service.ashx";
            //string strCorpName = "成都家园经营管理有限公司";//"联创未来";      //
            //string strCustID = "13360300000001";//"10000100000001";  //

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160503XPManage";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>1</Net>");
                Attribute.Append("<UnCustID>" + strUnCustID + "</UnCustID>");
                Attribute.Append("<CorpName>" + strCorpName + "</CorpName>");

                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=RoomInformation&Command=RoomInformationList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";

            string strCorpName = "大连亿达物业";//公司名称

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160507CityInformation";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CityInformation&Command=CityInformationList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";

            string strCorpID = "1813";//公司ID
            string strProvince = "辽宁省";
            string strCity = "大连市";

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160507CommunityInformation";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>1</Net>");
                Attribute.Append("<CorpID>" + strCorpID + "</CorpID>");
                Attribute.Append("<Province>" + strProvince + "</Province>");
                Attribute.Append("<City>" + strCity + "</City>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CommunityInformation&Command=CommunityInformationList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            //TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";
            //string strCommID = "181301";//公司ID

            TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";
            string strCommID = "126703";//公司ID 177305 

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160507BuildingInformationXP";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<Net>1</Net>");
                Attribute.Append("<CommID>" + strCommID + "</CommID>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=BuildingInformation&Command=BuildingInformationList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://192.168.0.78/TWInterface/Service/Service.ashx";//鑫苑
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            //TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";
            TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";
            string strCommID = "181302";//公司ID
            string strBuildSNum = "81";


            //string strCommID = "100001";//公司ID
            ////string strBuildSNum = "1";
            //string strBuildSNum = "1";//可空参数

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160511AllHousingInformationXP";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>1</Net>");
                Attribute.Append("<CommID>" + strCommID + "</CommID>");
                Attribute.Append("<BuildSNum>" + strBuildSNum + "</BuildSNum>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=AllHousingInformation&Command=AllHousingInformationList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";

            string strRoomID = "18130200000983";//亿达业主电话

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160519MatchingMobilePhoneXP";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>1</Net>");
                Attribute.Append("<RoomID>" + strRoomID + "</RoomID>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=MatchingMobilePhone&Command=MatchingMobilePhoneList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "";
            //Result.Text = "";
            string strData = "{\"AbarCheck\":\"\",\"AbarCheckPId\":\"\",\"AbarCheckTime\":\"\",\"AbarPId\":\"000081\",\"AbarPIdName\":\"王朝兴\",\"AbarbeitungId\":\"cf2fda1f-7f7d-43b8-a6fb-786d01160e72\",\"AddPId\":\"\",\"AddTime\":\"2016/5/18 15:06:26\",\"CheckNote\":\"来来来\",\"CheckRemark\":\"来来来\",\"CheckResult\":\"整改\",\"CheckStatus\":\"\",\"CheckTime\":\"\",\"Coordinate\":\"\",\"Files\":\"\",\"IsInTime\":\"\",\"IsOk\":\"1\",\"Major\":\"\",\"Pictures\":\"\",\"PointIds\":\"5f71b4f7-af50-43e5-8fdf-674fb8392540\",\"PointIdsName\":\"[A座大厅] \",\"ProblemType\":\"消防问题\",\"RectificationNote\":\"来来来\",\"RectificationPeriod\":\"5\",\"ReduceCheckResult\":\"哈哈\",\"ReduceCheckTime\":\"2016-05-24 19:23:41\",\"ReducePId\":\"\",\"ReducePIdName\":\"\",\"ReducePoint\":\"2.00\",\"ReduceResult\":\"测\",\"ReduceTime\":\"2016 / 5 / 23 14:48:09\",\"TaskId\":\"f1d22809-a80c-4739-866e-73e203ee8488\",\"id\":43}";
            string repsTxt = "";
            try
            {
                string strDoUrl = "http://localhost:80//TWInterface/Service/Service.ashx";
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160324QualityManage";

                string Attribute = "<attributes><Net>2</Net><Account>1000-az</Account><LoginPwd>1</LoginPwd><data>";
                Attribute += strData;
                Attribute += "</data></attributes>";
                if (TbURL.Text.Trim() == "1")
                {
                    Attribute = DesUrl.Text;
                }
                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=QualityManage&Command=GetQualityInspectSaveNew&Attribute=" + Attribute + "&Mac=" + Mac;

                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:8008/Service.ashx";

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160604WaitWorkCount";

                StringBuilder Attribute = new StringBuilder("");
                Attribute.Append("<attributes>");
                Attribute.Append("<Net>2</Net>");
                Attribute.Append("<Account>1000-az</Account>");
                Attribute.Append("<LoginPwd>1</LoginPwd>");
                Attribute.Append("<UserName>艾中</UserName>");
                Attribute.Append("<CommID>100013</CommID>");
                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=WaitWorkCount&Command=IncidentCount&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:8008/Service.ashx";

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160604WaitWorkCount";

                StringBuilder Attribute = new StringBuilder("");
                Attribute.Append("<attributes>");
                Attribute.Append("<Net>2</Net>");
                Attribute.Append("<Account>1000-az</Account>");
                Attribute.Append("<LoginPwd>1</LoginPwd>");
                Attribute.Append("<UserCode>000081</UserCode>");
                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=WaitWorkCount&Command=BusinessCheckCount&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:8008/Service.ashx";

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160604WaitWorkCount";

                StringBuilder Attribute = new StringBuilder("");
                Attribute.Append("<attributes>");
                Attribute.Append("<Net>2</Net>");
                Attribute.Append("<Account>1000-az</Account>");
                Attribute.Append("<LoginPwd>1</LoginPwd>");
                Attribute.Append("<UserCode>000097</UserCode>");
                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=WaitWorkCount&Command=OACheckCount&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service.ashx";

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160606SwitchSystem";

                StringBuilder Attribute = new StringBuilder("");
                Attribute.Append("<attributes>");
                Attribute.Append("<Net>2</Net>");
                Attribute.Append("<Account>1000-az</Account>");
                Attribute.Append("<LoginPwd>1</LoginPwd>");
                Attribute.Append("<UserCode>000081</UserCode>");
                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=SwitchSystem&Command=OrganList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";

            string strCommID = "181339";//公司ID

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160601CommunityNotificationXP";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>1</Net>");
                Attribute.Append("<CommID>" + strCommID + "</CommID>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CommunityNotification&Command=CommunityNotificationList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";

            string strCommID = "181339";//公司ID  100013
            string strInfoID = "1813390000000003";            //1000130000000002

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160602CommunityNotificationDetailXP";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>1</Net>");
                Attribute.Append("<CommID>" + strCommID + "</CommID>");
                Attribute.Append("<InfoID>" + strInfoID + "</InfoID>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CommunityNotificationDetail&Command=CommunityNotificationDetailList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";

            string strCommID = "181307";//公司ID  181339   100013

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160603CommunityNotificationHisXP";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>1</Net>");
                Attribute.Append("<CommID>" + strCommID + "</CommID>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CommunityNotificationHis&Command=CommunityNotificationHisList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";

            string strCommID = "181339";//公司ID  181339   100013

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160604CallHouseKeeperXP";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>1</Net>");
                Attribute.Append("<CommID>" + strCommID + "</CommID>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CallHouseKeeper&Command=CallHouseKeeperList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://113.247.231.148:8093/TWInterface/Service/Service.ashx";//北方
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";  //本地
            TbURL.Text = "http://192.168.0.78/TWInterface/Service/Service.ashx";//鑫苑
            string strCommID = "100058";//测试项目
            string strCustName = "顾志明";
            string strRoomID = "10005800000002";
            string strRoomSign = "01-0102";

            //TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";
            //string strCommID = "100001";
            //string strCustName = "秦雯";
            //string strRoomID = "10000100000819";

            //TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";
            //string strCommID = "181307";
            //string strCustName = "陈湘月";
            //string strRoomID = "18130700000001"; //报事ID：181328000000000010
            //string strRoomSign = "4-1109";

            string strIncidentDate = "2017-04-01";
            string strIncidentMan = "Leon";
            string strIncidentContent = "报事测试";
            string strReserveDate = "2017-04-01";
            string strPhone = "123456789321";
            string strPic = "http://pic7.nipic.com/20100609/5136651_124423001651_2.jpg";
            string strTradeDate = DateTime.Now.ToString("yyyy-MM-dd");
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160605IncidentOnlineXP";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<Net>2</Net>");
                Attribute.Append("<CommID>" + strCommID + "</CommID>");
                //Attribute.Append("<CustName>" + strCustName + "</CustName>");
                Attribute.Append("<RoomID>" + strRoomID + "</RoomID>");
                //Attribute.Append("<RoomSign>" + strRoomSign + "</RoomSign>");
                Attribute.Append("<IncidentDate>" + strIncidentDate + "</IncidentDate>");
                Attribute.Append("<IncidentMan>" + strIncidentMan + "</IncidentMan>");
                Attribute.Append("<IncidentContent>" + strIncidentContent + "</IncidentContent>");
                Attribute.Append("<ReserveDate>" + strReserveDate + "</ReserveDate>");
                Attribute.Append("<Phone>" + strPhone + "</Phone>");
                Attribute.Append("<Pic>" + strPic + "</Pic>");


                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=IncidentOnline&Command=IncidentOnlineList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";


            string strRoomID = "18130100001196";//亿达18130100001196   18133900000001  18132800000084  10001300000006
            string strTradeDate = DateTime.Now.ToString("yyyy-MM-dd");

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160606IncidentSearchXP";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>1</Net>");

                Attribute.Append("<RoomID>" + strRoomID + "</RoomID>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=IncidentSearch&Command=IncidentSearchList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }


        private void button29_Click(object sender, EventArgs e)
        {

            TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160615AllCompanyInformation";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<Net>1</Net>");

                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=AllCompanyInformation&Command=AllCompanyInformationList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            TbURL.Text = "http://1.192.88.243:16666/XYTWInterface/Service/Service.ashx";//升龙调用鑫苑E


            string strCorpID = "1824";//公司ID 1000

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160615CommunityInformationQD";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>3</Net>");
                Attribute.Append("<CorpID>" + strCorpID + "</CorpID>");

                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CommunityInformationQD&Command=CommunityInformationQDList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button31_Click(object sender, EventArgs e)
        {

            TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160615AllCompanyInformation";

                //string strCorpID = "1000";//1241
                string strIndentifys = "MD01;MD04";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<CorpID>" + strCorpID + "</CorpID>");
                Attribute.Append("<fdIndentifys>" + strIndentifys + "</fdIndentifys>");
                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=AllCompanyInformation&Command=AllCompanyInformationList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button32_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";


            string strCorpID = "1000";//公司ID

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160615CommunityInformationQD";

                //string strCorpID = "1000";//1241
                string strFdID = "12313131";
                string strAuditDate = "2016-6-1";
                string strIsAudit = "1";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<fdID>" + strFdID + "</fdID>");
                Attribute.Append("<AuditDate>" + strAuditDate + "</AuditDate>");
                Attribute.Append("<IsAudit>" + strIsAudit + "</IsAudit>");
                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CommunityInformationQD&Command=CommunityInformationQDList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void GetData_MD01(Newtonsoft.Json.Linq.JToken JData)
        {
            Result.Text = Result.Text + "员工信息：" + "\r\n";

            int i = 0;
            foreach (Newtonsoft.Json.Linq.JToken DItem in JData)
            {
                i++;
                string strfdNo = DItem["fdNo"].ToString();
                string strfdName = DItem["fdName"].ToString();

                Result.Text = Result.Text + "第" + i.ToString() + "条" + "\r\n";
                Result.Text = Result.Text + "fdNo:" + strfdNo + "\r\n";
                Result.Text = Result.Text + "fdName:" + strfdName + "\r\n";
            }
        }

        private void GetData_MD04(Newtonsoft.Json.Linq.JToken JData)
        {
            Result.Text = Result.Text + "租户信息：" + "\r\n";

            int i = 0;
            foreach (Newtonsoft.Json.Linq.JToken DItem in JData)
            {
                i++;
                string strfdNo = DItem["fdNo"].ToString();
                string strfdName = DItem["fdName"].ToString();

                Result.Text = Result.Text + "第" + i.ToString() + "条" + "\r\n";
                Result.Text = Result.Text + "fdNo:" + strfdNo + "\r\n";
                Result.Text = Result.Text + "fdName:" + strfdName + "\r\n";
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            Result.Text = "";
            string jsonText = @"{'T':[{'fdIndentify':'MD01','fdTime':'2016-06-16 13:36'

,'data':[{'fdNo':'E001','fdName':'罗昭全','fdIsValid':true,'fdAddress':'成都三环路','fdIsPayable':true,'fdIsReceivable':true,'fdIsNetting':true,'fdIdentification':'522133198110119898','fdSex':'1','fdSexName':'Male'

,'fdCompanyData':[{'fdNo':'01240','fdName':'新希望置业有限公司'}],

'fdLinkmanData':[{'fdAddress':'成都三环路','fdEmail':'610000@qq.com','fdFax':'','fdId':'155531c0398396a1d0f2d8c4598bae11','fdIsValid':false,'fdLinkmanName':'罗昭全','fdMobile':'18983330993','fdModelId':'15552220813a2886f0ae48845a1a803c'
,'fdModelName':'com.landray.kmss.master.provider.model.MasterProviderMain'

,'fdPhone':'83747484','fdPostcodes':'610000','fdReserve1':'','fdReserve2':'','fdReserve3':'','fdReserve4':'','fdReserve5':'','fdType':'BX','fdTypeName':'备选联系人'
,'formClass':'com.landray.kmss.master.provider.forms.MasterProviderLinkmanForm'}]

,'fdBankData':[{'fdAccount':'罗昭全','fdBank':'成都银行','fdBankAccount':'644581470321740','fdId':'155531c03a771d1a22634204ee49d6e6','fdModelId':'15552220813a2886f0ae48845a1a803c'
,'fdModelName':'com.landray.kmss.master.provider.model.MasterProviderMain'

,'fdReserve1':'','fdReserve2':'','fdReserve3':'','fdReserve4':'','fdReserve5':''
,'formClass':'com.landray.kmss.master.provider.forms.MasterProviderBankForm'}]}]}

,{'fdIndentify':'MD04','fdTime':'2016-06-16 13:36'

,'data':[{'fdNo':'T02','fdName':'一个租户','fdBusinessScope':'01','fdBusinessClass':'02','fdBusinessClassName':'餐饮','fdBusinessSmallClass':'03','fdBusinessSmallClassName':'快餐','fdCustomerSource':'04','fdCustomerSourceName':'招商','fdLeasingExecutive':'张三','fdRegisteredFunds':'1000000','fdRanking':'05','fdRankingName':'一级','fdCustomerServer':'李四','fdIdentification':'4325432654326542643','fdIntentionalRent':'06','fdIntentionalRentName':'10000','fdIntentionalArea':'07','fdIntentionalAreaName':'200','fdEmployeesNum':100,'fdShopNum':100,'fdSex':'男','fdNationality':'中国','fdBirthday':'2016-06-01','fdWorkingUnit':'新希望','fdProfession':'工程师','fdHobbies':'无','fdIsValid':true,'fdIsPayable':true,'fdIsReceivable':true

,'fdBrandData':[{'fdNo':'B001','fdName':'华润'}]
,'fdHouseData':[]

,'fdLinkmanData':[{'fdAddress':'四川成都','fdEmail':'fdsafew@qq.com','fdFax':'352345','fdId':'15556d6a184e47de824f0744fab84af6','fdIsValid':false,'fdLinkmanName':'王五','fdMobile':'53453465436','fdModelId':'15556d33aa8c623e96b97cb49678e8ac','fdModelName':'com.landray.kmss.master.provider.model.MasterProviderTenant','fdPhone':'4356765','fdPostcodes':'4235','fdReserve1':'','fdReserve2':'','fdReserve3':'','fdReserve4':'','fdReserve5':'','fdType':'FR','fdTypeName':'法人联系人'
,'formClass':'com.landray.kmss.master.provider.forms.MasterProviderLinkmanForm'}

,{'fdAddress':'四川成都','fdEmail':'235432@QQ.COM','fdFax':'54235426','fdId':'15556d6a1846b870244a1ea4fbdbe858','fdIsValid':false,'fdLinkmanName':'赵敏','fdMobile':'1432543564','fdModelId':'15556d33aa8c623e96b97cb49678e8ac'
,'fdModelName':'com.landray.kmss.master.provider.model.MasterProviderTenant'

,'fdPhone':'23435346','fdPostcodes':'123545','fdReserve1':'','fdReserve2':'','fdReserve3':'','fdReserve4':'','fdReserve5':'','fdType':'ZC','fdTypeName':'驻场联系人'
,'formClass':'com.landray.kmss.master.provider.forms.MasterProviderLinkmanForm'}]

,'fdBankData':[{'fdAccount':'zhang','fdBank':'中国银行','fdBankAccount':'5345346534','fdId':'15556d6a1841be8c09f03144daf9dc09','fdModelId':'15556d33aa8c623e96b97cb49678e8ac'
,'fdModelName':'com.landray.kmss.master.provider.model.MasterProviderTenant','fdReserve1':'','fdReserve2':'','fdReserve3':'','fdReserve4':'','fdReserve5':''

,'formClass':'com.landray.kmss.master.provider.forms.MasterProviderBankForm'}

,{'fdAccount':'fggfdg','fdBank':'招商银行','fdBankAccount':'562346543765','fdId':'15556d6a1840df5c3de9fb54cd9987b7','fdModelId':'15556d33aa8c623e96b97cb49678e8ac'

,'fdModelName':'com.landray.kmss.master.provider.model.MasterProviderTenant'
,'fdReserve1':'','fdReserve2':'','fdReserve3':'','fdReserve4':'','fdReserve5':''
,'formClass':'com.landray.kmss.master.provider.forms.MasterProviderBankForm'}]}]}]}";

            string jsonTextErr = "{'F': '输入的标识位为空。'}";

            Newtonsoft.Json.Linq.JObject jo = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonText);

            if (jo["T"] != null)
            {
                foreach (Newtonsoft.Json.Linq.JToken DType in jo["T"])
                {
                    string strIndentify = DType["fdIndentify"].ToString();
                    string strTime = DType["fdTime"].ToString();
                    Newtonsoft.Json.Linq.JToken JData = DType["data"];

                    switch (strIndentify)
                    {

                        case "MD01":
                            //员工信息-MD01
                            GetData_MD01(JData);
                            break;
                        case "MD04":
                            //租户信息-MD04
                            GetData_MD04(JData);
                            break;

                    }

                }
            }

            Result.Text = Result.Text + "\r\n";

            Newtonsoft.Json.Linq.JObject joErr = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonTextErr);

            if (joErr["F"] != null)
            {
                Result.Text = Result.Text + "错误：" + joErr["F"].ToString();
            }

            Result.Text = Result.Text + "\r\n";

        }

        private string GetWaivForm(string CommName, string CustName, string RoomSign, string CostName
            , string WaivType, string WaivMonthAmount, string WaivRates, string WaivAmount, string WaivStartDuring, string WaivEndDuring, string WaivReason)
        {

            StringBuilder sListContent = new StringBuilder("");

            sListContent.Append("{");
            sListContent.Append("\"" + ReviewWaivField.CommName + "\"").Append(":").Append("\"" + CommName + "\"").Append(",");
            sListContent.Append("\"" + ReviewWaivField.CustName + "\"").Append(":").Append("\"" + CustName + "\"").Append(",");
            sListContent.Append("\"" + ReviewWaivField.RoomSign + "\"").Append(":").Append("\"" + RoomSign + "\"").Append(",");
            sListContent.Append("\"" + ReviewWaivField.CostName + "\"").Append(":").Append("\"" + CostName + "\"").Append(",");
            sListContent.Append("\"" + ReviewWaivField.WaivType + "\"").Append(":").Append("\"" + WaivType + "\"").Append(",");
            sListContent.Append("\"" + ReviewWaivField.WaivMonthAmount + "\"").Append(":").Append("\"" + WaivMonthAmount + "\"").Append(",");
            sListContent.Append("\"" + ReviewWaivField.WaivRates + "\"").Append(":").Append("\"" + WaivRates + "\"").Append(",");
            sListContent.Append("\"" + ReviewWaivField.WaivAmount + "\"").Append(":").Append("\"" + WaivAmount + "\"").Append(",");
            sListContent.Append("\"" + ReviewWaivField.WaivStartDuring + "\"").Append(":").Append("\"" + WaivStartDuring + "\"").Append(",");
            sListContent.Append("\"" + ReviewWaivField.WaivEndDuring + "\"").Append(":").Append("\"" + WaivEndDuring + "\"").Append(",");
            sListContent.Append("\"" + ReviewWaivField.WaivReason + "\"").Append(":").Append("\"" + WaivReason + "\"");
            sListContent.Append("}");

            string strJSON = sListContent.ToString();

            return strJSON;
        }

        private void button34_Click(object sender, EventArgs e)
        {
            Result.Text = "";

            try
            {
                KmReviewService.kmReviewParamterForm paramForm = new KmReviewService.kmReviewParamterForm();

                paramForm.docSubject = "减免_测试_1";//文档标题
                paramForm.fdTemplateId = ReviewTemplate.WaivTemplateId;//文档模板id
                paramForm.docContent = ""; //文档的富文本内容

                string strJSONForm = GetWaivForm("测试小区", "张飞", "A01-01-01", "物管费", "现金", "100", "0", "300", "2016-6-1", "2016-8-31", "减免原因");
                Result.Text = Result.Text + strJSONForm + "\r\n";
                //业务表单数据
                paramForm.formValues = strJSONForm;//流程表单数据(Json) 
                                                   //其中明细表明细表是按列来设置  格式为"明细表id.列id":["列值1","列值2","列值3"...]，每列单独组合成一个集合。

                paramForm.docStatus = "10";//文档状态 草稿"10" 待审"20"
                paramForm.docCreator = "{\"LoginName\": \"liwf\"}";//流程发起人(Json) 单值
                paramForm.fdKeyword = "[\"物管费\", \"减免\"]";//文档关键字(Json) 格式为["",""]
                paramForm.docProperty = "";//辅类别(Json) 格式为["",""]

                paramForm.flowParam = null;//流程参数(Json) auditNode=审批意见 
                                           //futureNodeId =流向下一节点的ID，需要人工决策时设置此参数 
                                           //changeNodeHandlers = 节点的处理人，格式为["节点名1：处理人ID1; 处理人ID2...","节点名2：处理人ID1; 处理人ID2..."...]，需要修改处理人时设置此参数
                                           //paramForm.attachmentForms = "";//附件列表 fdKey=fdAttachment,fdFileName=附件文件名,fdAttachment=附件内容，格式为字节编码byte[]

                KmReviewService.KmReviewWebserviceServiceClient client = new KmReviewService.KmReviewWebserviceServiceClient();

                string DocId = client.addReview(paramForm);

                Result.Text = Result.Text + DocId + "\r\n";
            }
            catch (Exception ex)
            {
                Result.Text = Result.Text + ex.Message.ToString() + "\r\n";
            }


        }

        private void button35_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";


            string strCommID = "191901";//公司ID

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160628GroupInformation";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<Net>5</Net>");
                Attribute.Append("<CommID>" + strCommID + "</CommID>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=GroupInformation&Command=GroupInformationList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button36_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            //TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";
            //TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";

            string strIncidentID = "100013000000000013"; //10000100000001 18130700000037
            string strTradeDate = DateTime.Now.ToString("yyyy-MM-dd");

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160701IncidentSearchXP";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>2</Net>");

                Attribute.Append("<IncidentID>" + strIncidentID + "</IncidentID>");
                //Attribute.Append("<CurrPage>1</CurrPage>");
                //Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=IncidentSearchQD&Command=IncidentSearchQDList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button37_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://125.64.9.118:8888/Service.ashx";//联创
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            //string strIncidentID = "100013000000000086";
            //string strAppraiseContent = "满意";
            //string strCustComments = "服务满意，继续加油";

            //TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";


            TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";
            string strIncidentID = "181307000000000245";
            string strAppraiseContent = "满意";
            string strCustComments = "业主APP回访评价测试";

            string strTradeDate = DateTime.Now.ToString("yyyy-MM-dd");
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160608IncidentCustAppraiseXP";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>1</Net>");
                Attribute.Append("<IncidentID>" + strIncidentID + "</IncidentID>");
                Attribute.Append("<AppraiseContent>" + strAppraiseContent + "</AppraiseContent>");
                Attribute.Append("<CustComments>" + strCustComments + "</CustComments>");

                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=IncidentCustAppraiseYD&Command=IncidentCustAppraiseYDList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button38_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://tw.cqhyrc.com.cn/TWInterface/Service/Service.ashx";
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";


            string strCommID = "152801";//公司ID  
            //string strCustID = "15280100000001";//可空参数 
            string strCustID = "";//可空参数 

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160712CustomerLiveQD";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes><Type>1</Type><Telphone>13193020363</Telphone><PaperCode></PaperCode></attributes>");
               
                 
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CustomerLiveQD&Command=CustomerLiveHYListNew&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button39_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";


            string strCommID = "100013";//公司ID 
            //string strCustID = "15280100000001";
            string strCustID = "";//可空参数

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "OwnerInformationQD";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<Net>2</Net>");
                Attribute.Append("<CommID>" + strCommID + "</CommID>");
                Attribute.Append("<CustID>" + strCustID + "</CustID>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=OwnerInformation&Command=OwnerInformationList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button40_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            //string strCorpName = "成都西南物业管理有限公司（演示）";
            //string strRoomID = "10001300000001";


            TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";
            //string strCorpName = "大连亿达物业";//"大连亿达物业";      //
            string strRoomID = "18130700000697";//"";  //
            //string strQuery = "0";
            string strQuery = "1";

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160715QualityManageYD";


                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>1</Net>");
                Attribute.Append("<RoomID>" + strRoomID + "</RoomID>");
                Attribute.Append("<Query>" + strQuery + "</Query>");
                Attribute.Append("<BeginDate></BeginDate>");
                Attribute.Append("<EndDate></EndDate>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=FeesPeriodSearchYD&Command=FeesYDList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button41_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            ////string strCorpName = "成都诚悦时代物业服务有限公司";
            //string strRoomID = "12100100000189";

            TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";
            //string strCorpName = "大连亿达物业";//"大连亿达物业";      //
            string strRoomID = "18133900000185";//"";  //

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160716PreBalanceSummarySerchYD";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<Net>1</Net>");
                Attribute.Append("<RoomID>" + strRoomID + "</RoomID>");
                Attribute.Append("<BeginDate></BeginDate>");
                Attribute.Append("<EndDate></EndDate>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=PreBalanceSummarySerchYD&Command=PreBalanceSummarySerchYDList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button42_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            ////string strCorpName = "成都西南物业服务有限公司";
            //string strRoomID = "13313700000063";//"";  //
            //string strFeesIDs = "133137000000028231,133137000000028232";
            //string strTotalAmount = "7";

            TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";
            //string strCorpName = "大连亿达物业";//"大连亿达物业";      //
            string strRoomID = "18130700000028";//"";  //
            string strFeesIDs = "181307000000002820,181307000000002821";
            string strTotalAmount = "120.94";

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160717PayBillFeesYD";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<Net>1</Net>");
                Attribute.Append("<RoomID>" + strRoomID + "</RoomID>");
                Attribute.Append("<FeesIDs>" + strFeesIDs + "</FeesIDs>");
                Attribute.Append("<TotalAmount>" + strTotalAmount + "</TotalAmount>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=PayBillFeesYD&Command=PrepareFeesYDList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button43_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8081/Service/Service.ashx";
            TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";
            string strCorpID = "1528";
            string strRoomID = "15280200001201";
            string strQuery = "";


            //TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";
            ////string strCorpName = "大连亿达物业";//"大连亿达物业";      //
            //string strRoomID = "18130700000639";//"";  //
            ////string strQuery = "0";
            //string strQuery = "1";

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160722QualityManageQD";


                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<Net>2</Net>");
                Attribute.Append("<CorpID>" + strCorpID + "</CorpID>");
                Attribute.Append("<RoomID>" + strRoomID + "</RoomID>");
                Attribute.Append("<Query>" + strQuery + "</Query>");
                Attribute.Append("<BeginDate>2018-07-08</BeginDate>");
                Attribute.Append("<EndDate>2018-07-08</EndDate>");
                //Attribute.Append("<CurrPage>1</CurrPage>");
                //Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=FeesPeriodSearchQD&Command=FeesQDList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button44_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";
            string strCorpName = "融信（福建）物业管理有限公司";//"四川水韵五兴物业管理有限公司";
            string strRoomID = "15280600000021";
            string strFeesIDs = "152806000000056401,152806000000053787,152806000000051135,152806000000046770";//"133137000000028231,133137000000028232";
            string strTotalAmount = "43.00";

            //TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";
            ////string strCorpName = "大连亿达物业";//"大连亿达物业";      //
            //string strRoomID = "18130700000028";//"";  //
            //string strFeesIDs = "181307000000002820,181307000000002821";
            //string strTotalAmount = "120.94";

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160717PayBillFeesQD";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<Net>2</Net>");//
                Attribute.Append("<CorpName>" + strCorpName + "</CorpName>");
                Attribute.Append("<RoomID>" + strRoomID + "</RoomID>");
                Attribute.Append("<FeesIDs>" + strFeesIDs + "</FeesIDs>");
                Attribute.Append("<TotalAmount>" + strTotalAmount + "</TotalAmount>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=PayBillFeesQD&Command=PrepareFeesQDList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button45_Click(object sender, EventArgs e)
        {
            UnionPay_Txt_3.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = UnionPay_Txt_1.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160803Location";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=Location&Command=GetCityList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                UnionPay_Txt_2.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button46_Click(object sender, EventArgs e)
        {
            UnionPay_Txt_3.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = UnionPay_Txt_1.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160803Location";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();
                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=Location&Command=GetCommList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                UnionPay_Txt_2.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            UnionPay_Txt_3.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button47_Click(object sender, EventArgs e)
        {
            string repsTxt = "";
            try
            {
                string strDoUrl = UnionPay_Txt_1.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160803Sms";
                string Attribute = "<attributes><Mobile>18980508320</Mobile><Content>你的验证码为：1898</Content></attributes>";
                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=Sms&Command=Send&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                UnionPay_Txt_2.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            UnionPay_Txt_3.Text = UnionPay_Txt_3.Text + "返回：" + repsTxt;
        }

        private void button48_Click(object sender, EventArgs e)
        {

        }

        private void button49_Click(object sender, EventArgs e)
        {
            UnionPay_Txt_1.Text = "http://localhost:8909/Service/Service.ashx";
            UnionPay_Txt_3.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = UnionPay_Txt_1.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804UnionPay";
                string Attribute = "<attributes><CommunityId>1000_100013</CommunityId><FeesIds>100013000000001575</FeesIds><CustID>10001300000001</CustID><txnTime>" + DateTime.Now.ToString("yyyyMMddHHmmss") + "</txnTime></attributes>";
                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=UnionPay&Command=GenerateOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                UnionPay_Txt_2.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            UnionPay_Txt_3.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button50_Click(object sender, EventArgs e)
        {
            //UnionPay_Txt_1.Text = "http://localhost:8909/Service/Service.ashx";
            UnionPay_Txt_3.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = UnionPay_Txt_1.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804UnionPay";


                string Attribute = "<attributes><CommunityId>" + UnionPay_CommunityId.Text.ToString() + "</CommunityId><OrderId>" + UnionPay_OrderId.Text.ToString() + "</OrderId><respCode>" + UnionPay_respCode.Text.ToString() + "</respCode><respMsg>" + UnionPay_respMsg.Text.ToString() + "</respMsg></attributes>";


                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=UnionPay&Command=UpdateProperyOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                UnionPay_Txt_2.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            UnionPay_Txt_3.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button51_Click(object sender, EventArgs e)
        {
            //UnionPay_Txt_1.Text = "http://localhost:8909/Service/Service.ashx";
            UnionPay_Txt_3.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = UnionPay_Txt_1.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804UnionPay";


                string Attribute = "<attributes><CommunityId>" + UnionPay_CommunityId.Text.ToString() + "</CommunityId><OrderId>" + UnionPay_OrderId.Text.ToString() + "</OrderId><respCode>" + UnionPay_respCode.Text.ToString() + "</respCode><respMsg>" + UnionPay_respMsg.Text.ToString() + "</respMsg></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=UnionPay&Command=ReceProperyOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                UnionPay_Txt_2.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            UnionPay_Txt_3.Text = Result.Text + "返回：" + repsTxt;
        }

        private void UnionPayCreate_Click(object sender, EventArgs e)
        {
            UnionPay_Txt_1.Text = "http://localhost:8909/Service/Service.ashx";
            UnionPay_Txt_3.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = UnionPay_Txt_1.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804UnionPay";


                string Attribute = "<attributes><CommunityId>" + UnionPay_CommunityId.Text.ToString() + "</CommunityId><FeesIds>" + UnionPay_FeesId.Text.ToString() + "</FeesIds><txnTime>" + DateTime.Now.ToString("yyyyMMddhhmmss") + "</txnTime><CustID>" + UnionPay_CustID.Text.ToString() + "</CustID></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=UnionPay&Command=GenerateOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                UnionPay_Txt_2.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            UnionPay_Txt_3.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button49_Click_1(object sender, EventArgs e)
        {
            // encodeResult.Text = RSAHelper.getInstance().Encrypt(tbContent.Text);
        }

        private void button52_Click(object sender, EventArgs e)
        {
            //decodeResult.Text = RSAHelper.getInstance().Decrypt(tbContent.Text);
        }

        private void button53_Click(object sender, EventArgs e)
        {
            UnionPay_Txt_1.Text = "http://localhost:8909/Service/Service.ashx";
            UnionPay_Txt_3.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = UnionPay_Txt_1.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804UnionPay";


                string Attribute = "<attributes><CommunityId>" + UnionPay_CommunityId.Text.ToString() + "</CommunityId><OrderId>" + UnionPay_OrderId.Text.ToString() + "</OrderId><respCode>" + UnionPay_respCode.Text.ToString() + "</respCode><respMsg>" + UnionPay_respMsg.Text.ToString() + "</respMsg></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=UnionPay&Command=SearchBankOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                UnionPay_Txt_2.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            UnionPay_Txt_3.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button54_Click(object sender, EventArgs e)
        {
            UnionPay_Txt_1.Text = "http://localhost:8909/Service/Service.ashx";
            UnionPay_Txt_3.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = UnionPay_Txt_1.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804UnionPay";


                string Attribute = "<attributes><CommunityId>" + UnionPay_CommunityId.Text.ToString() + "</CommunityId><OrderId>" + UnionPay_OrderId.Text.ToString() + "</OrderId><respCode>" + UnionPay_respCode.Text.ToString() + "</respCode><respMsg>" + UnionPay_respMsg.Text.ToString() + "</respMsg></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=UnionPay&Command=CancelOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                UnionPay_Txt_2.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            UnionPay_Txt_3.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button55_Click(object sender, EventArgs e)
        {

        }

        private void button55_Click_1(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            TbURL.Text = "http://125.64.9.118:8888/Service.ashx";
            //TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";

            string strIncidentID = "147206000000000003";//100018000000000018  147206000000000003
            string strCommID = "147206";//100018   147206
            string strInfoContent = "许鹏本地最后测试！";


            string strTradeDate = DateTime.Now.ToString("yyyy-MM-dd");
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160810IncidentRemindersXP";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>1</Net>");
                Attribute.Append("<IncidentID>" + strIncidentID + "</IncidentID>");
                Attribute.Append("<CommID>" + strCommID + "</CommID>");
                Attribute.Append("<InfoContent>" + strInfoContent + "</InfoContent>");
                Attribute.Append("<FileType>JSON</FileType>");
                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=IncidentRemindersLC&Command=IncidentRemindersList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button59_Click(object sender, EventArgs e)
        {
            textBox7.Text = "http://localhost:8909/Service/Service.ashx";
            WeiXin_Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = textBox7.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804WeiXinPay";

                string Attribute = "<attributes><CommunityId>" + WeiXinPay_CommunityId.Text.ToString() + "</CommunityId><FeesIds>" + WeiXinPay_FeesId.Text.ToString() + "</FeesIds><CustID>" + WeiXinPay_CustId.Text.ToString() + "</CustID></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=WeiXinPay&Command=GenerateOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                textBox8.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            WeiXin_Result.Text = "返回：" + repsTxt;
        }

        private void button65_Click(object sender, EventArgs e)
        {
            //textBox7.Text = "http://localhost:8909/Service/Service.ashx";
            WeiXin_Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = textBox7.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804WeiXinPay";

                string Attribute = "<attributes><CommunityId>" + WeiXinPay_CommunityId.Text.ToString() + "</CommunityId><OrderId>" + WeiXin_OrderId.Text.ToString() + "</OrderId><respCode>" + WeiXinPay_respCode.Text.ToString() + "</respCode><respMsg>" + WeiXinPay_respMsg.Text.ToString() + "</respMsg></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=WeiXinPay&Command=UpdateProperyOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                textBox8.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            WeiXin_Result.Text = "返回：" + repsTxt;
        }

        private void button61_Click(object sender, EventArgs e)
        {
            //textBox7.Text = "http://localhost:8909/Service/Service.ashx";
            WeiXin_Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = textBox7.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804WeiXinPay";

                string Attribute = "<attributes><CommunityId>" + WeiXinPay_CommunityId.Text.ToString() + "</CommunityId><OrderId>" + WeiXin_OrderId.Text.ToString() + "</OrderId><respCode>" + WeiXinPay_respCode.Text.ToString() + "</respCode><respMsg>" + WeiXinPay_respMsg.Text.ToString() + "</respMsg></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=WeiXinPay&Command=ReceProperyOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                textBox8.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            WeiXin_Result.Text = "返回：" + repsTxt;
        }

        private void button58_Click(object sender, EventArgs e)
        {
            //textBox7.Text = "http://localhost:8909/Service/Service.ashx";
            WeiXin_Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = textBox7.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804WeiXinPay";

                string Attribute = "<attributes><CommunityId>" + WeiXinPay_CommunityId.Text.ToString() + "</CommunityId><OrderId>" + WeiXin_OrderId.Text.ToString() + "</OrderId><respCode>" + WeiXinPay_respCode.Text.ToString() + "</respCode><respMsg>" + WeiXinPay_respMsg.Text.ToString() + "</respMsg></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=WeiXinPay&Command=SearchBankOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                textBox8.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            WeiXin_Result.Text = "返回：" + repsTxt;
        }

        private void button57_Click(object sender, EventArgs e)
        {
            //textBox7.Text = "http://localhost:8909/Service/Service.ashx";
            WeiXin_Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = textBox7.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804WeiXinPay";

                string Attribute = "<attributes><CommunityId>" + WeiXinPay_CommunityId.Text.ToString() + "</CommunityId><OrderId>" + WeiXin_OrderId.Text.ToString() + "</OrderId><respCode>" + WeiXinPay_respCode.Text.ToString() + "</respCode><respMsg>" + WeiXinPay_respMsg.Text.ToString() + "</respMsg></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=WeiXinPay&Command=CancelOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                textBox8.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            WeiXin_Result.Text = "返回：" + repsTxt;
        }

        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        private void button56_Click_1(object sender, EventArgs e)
        {
            string url = "http://localhost:8909/Service/WeiXinPayCallBack/WeiXinPay.ashx";
            Encoding encoding = Encoding.GetEncoding("utf-8");
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("result_code", "");
            parameters.Add("return_msg", "");
            parameters.Add("out_trade_no", "6A2319374857439C8C7F116525A2A669");
            parameters.Add("attach", "1000_100013");
            parameters.Add("sign", "");
            string Result = HttpPost.CreatePostHttpResponse(url, parameters, encoding);
            WeiXin_Result.Text = "返回：" + Result;
        }

        private void button60_Click(object sender, EventArgs e)
        {

        }

        private void button62_Click(object sender, EventArgs e)
        {
            //Alipay_Url.Text = "http://localhost:8909/Service/Service.ashx";
            Alipay_Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = Alipay_Url.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804Alipay";

                string Attribute = "<attributes><CommunityId>" + Alipay_CommunityId.Text.ToString() + "</CommunityId><FeesIds>" + Alipay_FeesIds.Text.ToString() + "</FeesIds><CustID>" + Alipay_CustId.Text.ToString() + "</CustID></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=Alipay&Command=GenerateOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                Alipay_ToUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Alipay_Result.Text = "返回：" + repsTxt;
        }

        private void button66_Click(object sender, EventArgs e)
        {
            Alipay_Url.Text = "http://localhost:8909/Service/Service.ashx";
            Alipay_Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = Alipay_Url.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804Alipay";

                string Attribute = "<attributes><CommunityId>" + Alipay_CommunityId.Text.ToString() + "</CommunityId><OrderId>" + Alipay_OrderId.Text.ToString() + "</OrderId><CustID>" + Alipay_CustId.Text.ToString() + "</CustID><respCode>" + Alipay_Trade_Status.Text.ToString() + "</respCode><respMsg>" + Alipay_Trade_Msg.Text.ToString() + "</respMsg></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=Alipay&Command=UpdateProperyOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                Alipay_ToUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Alipay_Result.Text = "返回：" + repsTxt;
        }

        private void button64_Click(object sender, EventArgs e)
        {
            Alipay_Url.Text = "http://localhost:8909/Service/Service.ashx";
            Alipay_Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = Alipay_Url.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804AlipayPrec";

                string Attribute = "<attributes><CommunityId>" + Alipay_CommunityId.Text.ToString() + "</CommunityId><OrderId>" + Alipay_OrderId.Text.ToString() + "</OrderId><CustID>" + Alipay_CustId.Text.ToString() + "</CustID></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=AlipayPrec&Command=ReceProperyOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                Alipay_ToUrl.Text = strNewUrl;
                //repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Alipay_Result.Text = "返回：" + repsTxt;
        }

        private void button63_Click(object sender, EventArgs e)
        {
            Alipay_Url.Text = "http://localhost:8909/Service/Service.ashx";
            Alipay_Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = Alipay_Url.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804Alipay";

                string Attribute = "<attributes><CommunityId>" + Alipay_CommunityId.Text.ToString() + "</CommunityId><OrderId>" + Alipay_OrderId.Text.ToString() + "</OrderId><CustID>" + Alipay_CustId.Text.ToString() + "</CustID></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=Alipay&Command=CancelOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                Alipay_ToUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Alipay_Result.Text = "返回：" + repsTxt;
        }

        private void button67_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            //TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";
            //TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";
            TbURL.Text = "http://landwuye.cofco.com/TWInterface/Service/Service.ashx";//中粮

            string strCommID = "142923";//公司ID  181339   100013

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160831CommunityNotificationHisQD";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<Net>2</Net>");
                Attribute.Append("<CommID>" + strCommID + "</CommID>");
                //Attribute.Append("<CurrPage>1</CurrPage>");
                //Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CommunityNotificationHisQD&Command=CommunityNotificationHisQDList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        /// <summary>
        /// 短信测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button68_Click(object sender, EventArgs e)
        {
            Alipay_Url.Text = "http://localhost:8008/Service/Service.ashx";
            Alipay_Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = Alipay_Url.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160803Sms";

                string Attribute = "<attributes><Mobile>13699014325</Mobile><Content>你的验证码为：3219</Content></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=Sms&Command=Send&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                Alipay_ToUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Alipay_Result.Text = "返回：" + repsTxt;
        }

        private void button69_Click(object sender, EventArgs e)
        {
            PushUrl.Text = "http://localhost:8909/Service/Service.ashx";
            PushUrlSure.Text = PushUrl.Text;
            string repsTxt = "";
            try
            {
                string strDoUrl = PushUrlSure.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160907AppPush";

                string Attribute = "<attributes><Package>" + Package.Text.ToString() + "</Package><Tag>" + Tag.Text.ToString() + "</Tag><Title>" + TITLE.Text.ToString() + "</Title><MsgContent>" + MSG_CONTENT.Text.ToString() + "</MsgContent></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=AppPush&Command=PushAll&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                PushUrlSure.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            PushRichTextBox.Text = "返回：" + repsTxt;
        }

        private void button70_Click(object sender, EventArgs e)
        {
            PushUrl.Text = "http://localhost:8909/Service/Service.ashx";
            PushUrlSure.Text = PushUrl.Text;
            string repsTxt = "";
            try
            {
                string strDoUrl = PushUrlSure.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160907AppPush";

                string Attribute = "<attributes><Package>" + Package.Text.ToString() + "</Package><Tag>" + Tag.Text.ToString() + "</Tag><Title>" + TITLE.Text.ToString() + "</Title><MsgContent>" + MSG_CONTENT.Text.ToString() + "</MsgContent></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=AppPush&Command=PushAlias&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                PushUrlSure.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            PushRichTextBox.Text = "返回：" + repsTxt;
        }

        private void button71_Click(object sender, EventArgs e)
        {
            // PushUrl.Text = "http://localhost:8909/Service/Service.ashx";
            PushUrlSure.Text = PushUrl.Text;
            string repsTxt = "";
            try
            {
                string strDoUrl = PushUrlSure.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160907AppPush";

                string Attribute = "<attributes><Package>" + Package.Text.ToString() + "</Package><Tag>" + Tag.Text.ToString() + "</Tag><Title>" + TITLE.Text.ToString() + "</Title><MsgContent>" + MSG_CONTENT.Text.ToString() + "</MsgContent></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=AppPush&Command=PushTag&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                PushUrlSure.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            PushRichTextBox.Text = "返回：" + repsTxt;
        }

        private void button72_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8909/Service/Service.ashx";
            DesUrl.Text = TbURL.Text;
            string repsTxt = "";
            try
            {
                string strDoUrl = DesUrl.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160817OwnerBangDing";

                string Attribute = "<attributes><RelationId>f7904e2c-fba9-4b4d-9b94-b74b495591a4</RelationId></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=OwnerBangDing&Command=UnBindCustomer&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = "返回：" + repsTxt;
        }

        private void button73_Click(object sender, EventArgs e)
        {

        }

        private void button74_Click(object sender, EventArgs e)
        {
            DesUrl.Text = TbURL.Text;
            string repsTxt = "";
            try
            {
                string strDoUrl = DesUrl.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160908NoticeInfo";

                string Attribute = "<attributes><Id>0752d1a9-ae6d-4436-9103-cd68ab90f8e9</Id></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=NoticeInfo&Command=GetNoticeTypeDetail&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = "返回：" + repsTxt;
        }

        private void Prec74_Click(object sender, EventArgs e)
        {
            DesUrl.Text = TbURL.Text;
            string repsTxt = "";
            try
            {
                string strDoUrl = DesUrl.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160908NoticeInfo";

                string Attribute = "<attributes><Id>0752d1a9-ae6d-4436-9103-cd68ab90f8e9</Id></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=NoticeInfo&Command=GetNoticeTypeDetail&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = "返回：" + repsTxt;
        }

        private void button75_Click(object sender, EventArgs e)
        {
            DesUrl.Text = TbURL.Text;
            string repsTxt = "";
            try
            {
                string strDoUrl = DesUrl.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160908NoticeInfo";

                string Attribute = "<attributes><TopNum>10</TopNum><CommunityId>1000_100013</CommunityId></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=NoticeInfo&Command=NoticeDeskTopInfo&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = "返回：" + repsTxt;
        }

        //private void button74_Click(object sender, EventArgs e)
        //{
        //    UnionPay_Txt_1.Text = "http://localhost:8909/Service/Service.ashx";
        //    UnionPay_Txt_3.Text = "";
        //    string repsTxt = "";
        //    try
        //    {
        //        string strDoUrl = UnionPay_Txt_1.Text;
        //        string strDate = DateTime.Now.ToString("yyyyMMdd");
        //        string strToken = "20160804UnionPayPrec";


        //        string Attribute = "<attributes><CommunityId>" + UnionPay_CommunityId.Text.ToString() + "</CommunityId><OrderId>" + UnionPay_OrderId.Text.ToString() + "</OrderId><respCode>" + UnionPay_respCode.Text.ToString() + "</respCode><respMsg>" + UnionPay_respMsg.Text.ToString() + "</respMsg><CostID>" + CostID.Text.ToString() + "</CostID><RoomID>" + RoomID.Text.ToString() + "</RoomID><Amount>1000</Amount></attributes>";

        //        string Mac = CreMAC(Attribute, strDate, strToken);
        //        string strContent = "Class=UnionPayPrec&Command=ReceProperyOrder&Attribute=" + Attribute + "&Mac=" + Mac;
        //        string strNewUrl = strDoUrl + "?" + strContent;
        //        UnionPay_Txt_2.Text = strNewUrl;
        //        repsTxt = SendHttp(strNewUrl, "");
        //    }
        //    catch (Exception ex)
        //    {
        //        repsTxt = ex.Message.ToString();
        //    }
        //    UnionPay_Txt_3.Text = Result.Text + "返回：" + repsTxt;
        //}

        private void button76_Click(object sender, EventArgs e)
        {
            UnionPay_Txt_1.Text = "http://localhost:8909/Service/Service.ashx";
            UnionPay_Txt_3.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = UnionPay_Txt_1.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804AlipayPrec";


                string Attribute = "<attributes><CommunityId>" + UnionPay_CommunityId.Text.ToString() + "</CommunityId><FeesIds>" + UnionPay_FeesId.Text.ToString() + "</FeesIds><txnTime>" + DateTime.Now.ToString("yyyyMMddhhmmss") + "</txnTime><CustID>" + UnionPay_CustID.Text.ToString() + "</CustID><CostID>" + CostID.Text.ToString() + "</CostID><RoomID>" + RoomID.Text.ToString() + "</RoomID><Amount>1000</Amount></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=AlipayPrec&Command=GenerateOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                UnionPay_Txt_2.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            UnionPay_Txt_3.Text = Result.Text + "返回：" + repsTxt;
        }

        //private void button75_Click(object sender, EventArgs e)
        //{
        //    UnionPay_Txt_1.Text = "http://localhost:8909/Service/Service.ashx";
        //    UnionPay_Txt_3.Text = "";
        //    string repsTxt = "";
        //    try
        //    {
        //        string strDoUrl = UnionPay_Txt_1.Text;
        //        string strDate = DateTime.Now.ToString("yyyyMMdd");
        //        string strToken = "20160804AlipayPrec";


        //        string Attribute = "<attributes><CommunityId>" + UnionPay_CommunityId.Text.ToString() + "</CommunityId><OrderId>" + UnionPay_OrderId.Text.ToString() + "</OrderId><respCode>" + UnionPay_respCode.Text.ToString() + "</respCode><respMsg>" + UnionPay_respMsg.Text.ToString() + "</respMsg><CostID>" + CostID.Text.ToString() + "</CostID><RoomID>" + RoomID.Text.ToString() + "</RoomID><Amount>1000</Amount></attributes>";

        //        string Mac = CreMAC(Attribute, strDate, strToken);
        //        string strContent = "Class=AlipayPrec&Command=ReceProperyOrder&Attribute=" + Attribute + "&Mac=" + Mac;
        //        string strNewUrl = strDoUrl + "?" + strContent;
        //        UnionPay_Txt_2.Text = strNewUrl;
        //        repsTxt = SendHttp(strNewUrl, "");
        //    }
        //    catch (Exception ex)
        //    {
        //        repsTxt = ex.Message.ToString();
        //    }
        //    UnionPay_Txt_3.Text = Result.Text + "返回：" + repsTxt;
        //}

        private void button80_Click(object sender, EventArgs e)
        {
            UnionPay_Txt_1.Text = "http://localhost:8909/Service/Service.ashx";
            UnionPay_Txt_3.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = UnionPay_Txt_1.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804WeiXinPayPrec";


                string Attribute = "<attributes><CommunityId>" + UnionPay_CommunityId.Text.ToString() + "</CommunityId><FeesIds>" + UnionPay_FeesId.Text.ToString() + "</FeesIds><txnTime>" + DateTime.Now.ToString("yyyyMMddhhmmss") + "</txnTime><CustID>" + UnionPay_CustID.Text.ToString() + "</CustID><CostID>" + CostID.Text.ToString() + "</CostID><RoomID>" + RoomID.Text.ToString() + "</RoomID><Amount>1000</Amount></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=WeiXinPayPrec&Command=GenerateOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                UnionPay_Txt_2.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            UnionPay_Txt_3.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button79_Click(object sender, EventArgs e)
        {
            UnionPay_Txt_1.Text = "http://localhost:8909/Service/Service.ashx";
            UnionPay_Txt_3.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = UnionPay_Txt_1.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804WeiXinPayPrec";


                string Attribute = "<attributes><CommunityId>" + UnionPay_CommunityId.Text.ToString() + "</CommunityId><OrderId>" + UnionPay_OrderId.Text.ToString() + "</OrderId><respCode>" + UnionPay_respCode.Text.ToString() + "</respCode><respMsg>" + UnionPay_respMsg.Text.ToString() + "</respMsg><CostID>" + CostID.Text.ToString() + "</CostID><RoomID>" + RoomID.Text.ToString() + "</RoomID><Amount>1000</Amount></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=WeiXinPayPrec&Command=ReceProperyOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                UnionPay_Txt_2.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            UnionPay_Txt_3.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button81_Click(object sender, EventArgs e)
        {
            //DesUrl.Text = "http://localhost:8909/Service/Service.ashx";
            DesUrl.Text = "http://localhost:63305/Service.ashx";

            //DesUrl.Text = TbURL.Text.ToString();
            string repsTxt = "";
            try
            {
                string strDoUrl = DesUrl.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160826OCostInfo";

                string Attribute = "<attributes><CommunityId>E4918A7C-DBE3-4D19-9117-FC7CEAC983EE</CommunityId><CustID>19730300000006</CustID><RoomID>19730300000015</RoomID></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=CostInfo&Command=GetPrecCost&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = "返回：" + repsTxt;
        }

        private void button84_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            //TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";
            //TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";

            string strCommunityId = "1000_100013";//公司ID  181339   100013

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160913CommunityNotificationTW";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<CommunityId>" + strCommunityId + "</CommunityId>");
                //Attribute.Append("<CurrPage>1</CurrPage>");
                //Attribute.Append("<PageSize>1</PageSize>");
                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CommunityNotificationTW&Command=ServiceInformationList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        //公区报事
        private void button85_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:8088/Service.ashx";
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20170411CostInfo_BGY";
                StringBuilder Attribute = new StringBuilder("");
                Attribute.Append("<attributes>");
                Attribute.Append("<CommID>133137</CommID>");
                Attribute.Append("<CustID>13313700000001</CustID>");
                Attribute.Append("<RoomID>13313700000001</RoomID>");

                Attribute.Append("<StateDate>20170101</StateDate>");
                Attribute.Append("<EndDate>20170701</EndDate>");
                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();
                //MD5加密
                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CostInfo_BGY&Command=GetHistoricalPaymentList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button82_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8909/Service/Service.ashx";
            DesUrl.Text = TbURL.Text;
            string repsTxt = "";
            try
            {
                string strDoUrl = DesUrl.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160803Rental";

                string Attribute = "<attributes><CommunityId>1000_100013</CommunityId><City></City><HouseType></HouseType><StartAmount>0</StartAmount><EndAmount>500</EndAmount><Page>1</Page><PageSize>20</PageSize><BussType>租售</BussType><ProcessState></ProcessState></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=Rental&Command=RentalList&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = "返回：" + repsTxt;
        }

        private void button83_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:8909/Service/Service.ashx";
            DesUrl.Text = TbURL.Text;
            string repsTxt = "";
            try
            {
                string strDoUrl = DesUrl.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160803Rental";

                string Attribute = "<attributes><CommunityId>1000_100013</CommunityId><BussType>转让</BussType><LinkMan>艾中</LinkMan><Sex>男</Sex><Province>四川省</Province><City>成都市</City><Area>市辖区</Area><Address>锦绣路1号</Address><Mobile>18980508320</Mobile><Img>http://125.64.16.10:1890/Administration/Web/Upload/201610/4197f91199bf44d48814c17cb4090dbb.png</Img></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=Rental&Command=RentalNew&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = "返回：" + repsTxt;
        }

        private void button87_Click(object sender, EventArgs e)
        {

            TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            DesUrl.Text = TbURL.Text;
            string repsTxt = "";
            try
            {
                string strDoUrl = DesUrl.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160822OUserProposal";

                string Attribute = "<attributes><CommunityId>1509-150901</CommunityId><UserId>8a688cb8-f741-463f-b24b-f5b22cc8fa14</UserId></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=UserProposal&Command=GetProposalInfoList&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = "返回：" + repsTxt;
        }

        private void button88_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost/TWInterface/Service/Service.ashx";
            TbURL.Text = "http://218.13.58.36:8888/Service/Service.ashx";
            DesUrl.Text = TbURL.Text;
            string repsTxt = "";
            try
            {
                string strDoUrl = DesUrl.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20170411CostInfo_BGY";

                string Attribute = "<attributes><CustID>18538200001363</CustID><CommID>185382</CommID ><RoomID>18538200001456</RoomID></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=CostInfo_BGY&Command=GetNowRoomFeesList&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                //  repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = "返回：" + DesUrl;
        }


        private void button129_Click_1(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost/TWInterface/Service/Service.ashx";
            TbURL.Text = "http://218.13.58.36:8888/Service/Service.ashx";
            DesUrl.Text = TbURL.Text;
            string repsTxt = "";
            try
            {
                string strDoUrl = DesUrl.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20170411CostInfo_BGY";

                string Attribute = "<attributes><CustID>18530100000041</CustID><CommID>185301</CommID ><RoomID>18530100001007</RoomID></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=CostInfo_BGY&Command=GetArrearsList&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                //  repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = "返回：" + DesUrl;
        }

        private void button130_Click_1(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost/TWInterface/Service/Service.ashx";
            TbURL.Text = "http://218.13.58.36:8888/Service/Service.ashx";
            DesUrl.Text = TbURL.Text;
            string repsTxt = "";
            try
            {
                string strDoUrl = DesUrl.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20170411CostInfo_BGY";


                string Attribute = "<attributes><CustID>18530400000630</CustID><CommID>185304</CommID><RoomID>18530400004501</RoomID><CostId>18530400000001</CostId><PrecAmount>499.99</PrecAmount><source>碧有信物业宝</source></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=CostInfo_BGY&Command=PayFeesPrec_fhh&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                //  repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = "返回：" + DesUrl;
        }

        //户内报事
        private void button89_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://218.13.58.36:8888/Service/Service.ashx";

            TbURL.Text = "http://localhost:8088/Service.ashx";



            //TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";




            string strTradeDate = DateTime.Now.ToString("yyyy-MM-dd");
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                // string strToken = "20170515CostInfo_HJ";
                string strToken = "20180506CostInfoExtendHousehold";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<RoomID>18549700000196</RoomID>");
                Attribute.Append("<MemberName>黄楚云</MemberName>");
                Attribute.Append("<PaperCode>440623197110292325</PaperCode>");
                Attribute.Append("<IsEntrust>1</IsEntrust>");
                //Attribute.Append("<PrecAmount>999.0000</PrecAmount>");
                //Attribute.Append("<source>凤凰会APP代金券</source>");
                //Attribute.Append("<Memo>8b8b8c78-3053-4853-9309-98f0cac5eaa6</Memo>");

                //Attribute.Append("<incidentName>沈维春</incidentName>");
                //Attribute.Append("<incidentTel>13903010618</incidentTel>");
                //Attribute.Append("<maintainDetail>家里筒灯需要更换，需带材料，毛巾架稳固，有偿服务，请安排谢谢</maintainDetail>");

                //Attribute.Append("<amount>15.0</amount>");
                //Attribute.Append("<chargeTime>2018-02-14 13:44:47.0</chargeTime>");
                //Attribute.Append("<chargeState>4</chargeState>");

                //Attribute.Append("<incidentid>a0i6F00000I1esPQAR</incidentid>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();



                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CostInfoExtendHousehold&Command=UpdateIsEntrust&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;

                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");

                //string Mac = CreMAC(strAttribute, strDate, strToken);
                //string strContent = "Class=CostInfo_HJ&Command=CalcAdvancePrice&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                //string strNewUrl = strDoUrl + "?" + strContent;
                //DesUrl.Text = strNewUrl;
                //repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button91_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://125.64.9.118/TWInterface/Service/Service.ashx";
            string strIncidentID = "100003000000000018";
            string strCommID = "100003";
            string strInfoContent = "报事催办测试!";

            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            //string strIncidentID = "100018000000000018";
            //string strCommID = "100018";
            //string strInfoContent = "报事催办测试!";


            //TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";
            //string strCommID = "100001";
            //string strCustName = "秦雯";
            //string strRoomID = "10000100000819";



            string strTradeDate = DateTime.Now.ToString("yyyy-MM-dd");
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160810IncidentRemindersXP";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>1</Net>");
                Attribute.Append("<IncidentID>" + strIncidentID + "</IncidentID>");
                Attribute.Append("<CommID>" + strCommID + "</CommID>");
                Attribute.Append("<InfoContent>" + strInfoContent + "</InfoContent>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=IncidentRemindersLC&Command=IncidentRemindersList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button92_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://125.64.9.118/TWInterface/Service/Service.ashx";//联创
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            //TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";
            string strCorpName = "成都家园经营管理有限公司";
            string strIncidentID = "133603000000000001";
            string strAppraiseContent = "一般";
            string strCustComments = "业主APP回访评价测试";

            string strTradeDate = DateTime.Now.ToString("yyyy-MM-dd");
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20161103IncidentCustAppraiseXP";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>1</Net>");
                Attribute.Append("<CorpName>" + strCorpName + "</CorpName>");
                Attribute.Append("<IncidentID>" + strIncidentID + "</IncidentID>");
                Attribute.Append("<AppraiseContent>" + strAppraiseContent + "</AppraiseContent>");
                Attribute.Append("<CustComments>" + strCustComments + "</CustComments>");

                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=IncidentCustAppraiseLC&Command=IncidentCustAppraiseLCList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button93_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";
            string strCorpName = "成都西南物业服务有限公司";//公司名称
            string strMobilePhone = "13718188965";//业主电话

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20161107XPManage";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<Net>2</Net>");
                Attribute.Append("<CorpName>" + strCorpName + "</CorpName>");
                Attribute.Append("<MobilePhone>" + strMobilePhone + "</MobilePhone>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");
                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=RoomInformationJB&Command=RoomInformationJBList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button94_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";
            string strRoomID = "13293000000008";//房号

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20161107XPManage";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<Net>2</Net>");
                Attribute.Append("<RoomID>" + strRoomID + "</RoomID>");
                Attribute.Append("<FileType>JSON</FileType>");
                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=RoomInformationJB&Command=RoomInformationJHList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button95_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";


            string strCommID = "100013";//公司ID  
            string strRoomID = "10001300001105";//公司ID
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "OwnerInformationXYe";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<Net>2</Net>");
                Attribute.Append("<CommID>" + strCommID + "</CommID>");
                Attribute.Append("<RoomID>" + strRoomID + "</RoomID>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=OwnerInformationXY&Command=OwnerInformationXYList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button96_Click(object sender, EventArgs e)
        {
            Alipay_Url.Text = "http://localhost:8909/Service/Service.ashx";
            Alipay_Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = Alipay_Url.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804Alipay";

                string Attribute = "<attributes><CommunityId>" + Alipay_CommunityId.Text.ToString() + "</CommunityId><OrderId>" + Alipay_OrderId.Text.ToString() + "</OrderId><CustID>" + Alipay_CustId.Text.ToString() + "</CustID></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=Alipay&Command=ReceProperyOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                Alipay_ToUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Alipay_Result.Text = "返回：" + repsTxt;
        }

        private void button97_Click(object sender, EventArgs e)
        {
            Alipay_Url.Text = "http://localhost:63305/Service.ashx";
            Alipay_Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = Alipay_Url.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804AlipayBusinessOrder";

                //string Attribute = "<attributes><ProductList><Product><Id>c4b8174e-5428-4534-a6a4-79dffbaf2e03</Id><Quantity>1</Quantity><ShoppingId>388c794c-2020-41e4-8c8c-b60b7d413c98</ShoppingId><RpdMemo></RpdMemo></Product></ProductList><UserId>4197401e-3170-468a-9532-ead02b512b98</UserId><BussId>100055</BussId><Name>小猫</Name><Mobile>18980508320</Mobile><DeliverAddress>成都市金牛区13-1号</DeliverAddress><ReceiptMemo></ReceiptMemo></attributes>";

                string Attribute = "<attributes><ProductList><Product><Id>4a5abb08-1290-40de-8390-09b3eaaa6b54</Id><Quantity>1</Quantity><ShoppingId>5a0c4589-bf60-4669-8bdb-df03cb18afc4</ShoppingId><RpdMemo></RpdMemo></Product></ProductList><UserId>06663d34-3d6c-40ca-907a-e3941f131bf3</UserId><BussId>1000032</BussId><Name>测试用户</Name><Mobile>13699014325</Mobile><DeliverAddress>四川省成都市武侯区棕南正街啊啊啊</DeliverAddress><ReceiptMemo>00</ReceiptMemo></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=AlipayBusinessOrder&Command=GenerateOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                Alipay_ToUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Alipay_Result.Text = "返回：" + repsTxt;
        }

        private void button77_Click_1(object sender, EventArgs e)
        {

        }

        private void button86_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            //TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";
            //TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";

            string strCommunityId = "1000_100013";//公司ID  181339   100013

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160826OCostInfo";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<RoomID>0</RoomID>");
                Attribute.Append("<CommID>270cd8d0-fb8b-4392-8eb9-397f34630f41</CommID>");
                Attribute.Append("<CustID>13360300000551</CustID>");
                //Attribute.Append("<FeesStarteDate>2016-01-01</FeesStarteDate>");
                //Attribute.Append("<FeesEndDate>2016-12-01</FeesEndDate>");
                //Attribute.Append("<FeesStarteDate>2016-01-01</FeesStarteDate>");
                //Attribute.Append("<FeesEndDate>2016-12-01</FeesEndDate>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CostInfo&Command=GetOffsetPreDetail&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button98_Click(object sender, EventArgs e)
        {
            Alipay_Url.Text = "http://localhost:63305/Service.ashx";
            Alipay_Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = Alipay_Url.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804AlipayBusinessOrder";

                string Attribute = "<attributes><OrderId>" + Alipay_OrderId.Text.ToString() + "</OrderId></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=AlipayBusinessOrder&Command=ReceBusinessOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                Alipay_ToUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Alipay_Result.Text = "返回：" + repsTxt;
        }

        private void button90_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            //TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";
            //TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";

            string strCommunityId = "1000_100013";//公司ID  181339   100013

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20161103ResourcesDetails";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<RoomID>0</RoomID>");
                Attribute.Append("<BussId>100066</BussId>");
                //Attribute.Append("<CustID>13360300000551</CustID>");
                //Attribute.Append("<FeesStarteDate>2016-01-01</FeesStarteDate>");
                //Attribute.Append("<FeesEndDate>2016-12-01</FeesEndDate>");
                //Attribute.Append("<FeesStarteDate>2016-01-01</FeesStarteDate>");
                //Attribute.Append("<FeesEndDate>2016-12-01</FeesEndDate>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=ShoppingCar&Command=UpdateShoppingCarDetailedNum&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button90_Click_1(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:8077/Service/Service.ashx";
            //TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";
            //TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";

            string strCommunityId = "1000_100013";//公司ID  181339   100013

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160803Rental";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<Empty></Empty>");
                Attribute.Append("<ProcessState>受理</ProcessState>");
                Attribute.Append("<BussType>租售</BussType>");
                Attribute.Append("<CommunityId></CommunityId>");
                Attribute.Append("<City></City>");
                Attribute.Append("<HouseType></HouseType>");
                Attribute.Append("<StartAmount>0</StartAmount>");
                Attribute.Append("<EndAmount>999999</EndAmount>");

                Attribute.Append("<Page>1</Page>");
                Attribute.Append("<PageSize>10</PageSize>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=Rental&Command=RentalList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button77_Click_2(object sender, EventArgs e)
        {
            //Alipay_Url.Text = "http://localhost:8909/Service/Service.ashx";
            Alipay_Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = Alipay_Url.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804AlipayBusinessOrder";

                //string Attribute = "<attributes><OrderId>" + Alipay_OrderId.Text.ToString() + "</OrderId></attributes>";

                string Attribute = "<attributes><Empty></Empty><OrderId>7a6b1dca-db72-4a32-bf4a-f236a4245c6a</OrderId></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=AlipayBusinessOrder&Command=GoOnGenerateOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                Alipay_ToUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Alipay_Result.Text = "返回：" + repsTxt;
        }

        private void button78_Click_1(object sender, EventArgs e)
        {

        }

        private void button99_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:80/TWInterface/Service/Service.ashx";
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160324QualityManage";//AbarPId
                string Attribute = "<attributes><Net>1</Net><Account>1331-tw</Account><LoginPwd>004519</LoginPwd>";
                Attribute += "<ItemCode>133108</ItemCode><UserCode>000001</UserCode><AbarPId>000001</AbarPId>";
                Attribute += "<Timer>2017-1-20 00:00:00.000</Timer><CommID>133140</CommID></attributes>";
                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=QualityManage&Command=GetQualityInspectListCount&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                if (DesUrl.Text != "")
                {
                    strNewUrl = DesUrl.Text;
                }
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button100_Click(object sender, EventArgs e)
        {
            //textBox7.Text = "http://localhost:8909/Service/Service.ashx";
            WeiXin_Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = textBox7.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804WeiXinPayPrec";

                string Attribute = "<attributes><CommunityId>" + WeiXinPay_CommunityId.Text.ToString() + "</CommunityId><OrderId>" + WeiXin_OrderId.Text.ToString() + "</OrderId><respCode>" + WeiXinPay_respCode.Text.ToString() + "</respCode><respMsg>" + WeiXinPay_respMsg.Text.ToString() + "</respMsg></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=WeiXinPayPrec&Command=ReceProperyOrder&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                textBox8.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            WeiXin_Result.Text = "返回：" + repsTxt;
        }

        private void button86_Click_1(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:8088/Service.ashx";
            //TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";
            //TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";

            string strCommunityId = "1000_100013";//公司ID  181339   100013

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20180307CostInfoExtendFHH";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>2</Net>");
                Attribute.Append("<CommID>185301</CommID>");
                Attribute.Append("<Roomid>18530100001074</Roomid>");
                //Attribute.Append("<FeesIDs>100013000000048586</FeesIDs>");
                //Attribute.Append("<TotalAmount>0.01</TotalAmount>");

                //Attribute.Append("<FileType>JSON</FileType>");
                //Attribute.Append("<TypeID>水管漏水</TypeID>");
                //Attribute.Append("<DealLimit>30</DealLimit>");
                //Attribute.Append("<IncidentDate></IncidentDate>");
                //Attribute.Append("<images></images>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CostInfoExtendFHH&Command=GetWaiversFeeList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }
        /// <summary>
        /// 物管报事
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button101_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "1";
            //DesUrl.Text = "2";
            //string strCommunityId = "1000_100013";//公司ID  181339   100013

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = "http://218.13.58.36:7000/Service/Service.ashx";
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20170411CostInfo_BGY";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");

                Attribute.Append("<CustID>1</CustID>");
                Attribute.Append("<CommID>风信路</CommID>");
                Attribute.Append("<orderId>15</orderId>");
                Attribute.Append("<incidentType>1</incidentType>");

                Attribute.Append("<maintainType>1</maintainType>");
                Attribute.Append("<incidentAddr>1</incidentAddr>");

                Attribute.Append("<incidentName>王志坚</incidentName>");
                Attribute.Append("<incidentTel>13002081925</incidentTel>");
                Attribute.Append("<maintainDetail>ceshi0818004</maintainDetail>");
                Attribute.Append("<amount>30.0</amount>");
                Attribute.Append("<chargeTime>2017-08-18 17:33:13.0</chargeTime>");
                Attribute.Append("<chargeState>4</chargeState>");
                Attribute.Append("<incidentid></incidentid>");
                Attribute.Append("<RoomID></RoomID>");



                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CostInfo_BGY&Command=SetFees&Attribute=" + strAttribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button102_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";
            //TbURL.Text = "http://192.168.0.78/TWInterface/Service/Service.ashx";//鑫苑
            //TbURL.Text = "http://1.192.88.243:16666/XYTWInterface/Service/Service.ashx";//升龙
            string strCorpID = "1000";
            string strRoomID = "10001300001106";
            string strQuery = "1";
            string strSort = "1";
            //string strCostID = "10002000000006";//可空参数
            string strCostID = "10001300000014";

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20161225QualityManageXYe";


                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<Net>2</Net>");
                Attribute.Append("<CorpID>" + strCorpID + "</CorpID>");
                Attribute.Append("<RoomID>" + strRoomID + "</RoomID>");
                Attribute.Append("<Query>" + strQuery + "</Query>");
                Attribute.Append("<Sort>" + strSort + "</Sort>");
                Attribute.Append("<CostID>" + strCostID + "</CostID>");
                Attribute.Append("<BeginDate></BeginDate>");
                Attribute.Append("<EndDate></EndDate>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=FeesPeriodSearchXYe&Command=FeesXYList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button103_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            //TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";
            string strCorpID = "1209";
            string strRoomID = "12090100000891";


            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20170222CombRowFees";


                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<Net>2</Net>");
                Attribute.Append("<CorpID>" + strCorpID + "</CorpID>");
                Attribute.Append("<RoomID>" + strRoomID + "</RoomID>");
                Attribute.Append("<BeginDate></BeginDate>");
                Attribute.Append("<EndDate></EndDate>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CombRowFeesSearch&Command=CombRowFees&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button104_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";
            string strCommID = "181339";//公司ID

            //TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";
            //string strCommID = "126703";//公司ID 177305 

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20170328BuildingHousekeeper";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>1</Net>");
                Attribute.Append("<CommID>" + strCommID + "</CommID>");
                //Attribute.Append("<CurrPage>1</CurrPage>");
                //Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=BuildingHousekeeper&Command=BuildingHousekeeperList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button105_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";

            string strCommID = "152801";//公司ID
            string strBuildSNum = "1";//可空参数

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20170328AllHousingInformationRX";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<Net>2</Net>");
                Attribute.Append("<CommID>" + strCommID + "</CommID>");
                Attribute.Append("<BuildSNum>" + strBuildSNum + "</BuildSNum>");
                Attribute.Append("<CurrPage>1</CurrPage>");
                Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=AllHousingInformationRX&Command=AllHousingInformationRXList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button87_Click_1(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8008/Service/Service.ashx";
            TbURL.Text = "http://113.247.231.148:8093/TWInterface/Service/Service.ashx";
            string strCorpID = "1000";
            string strRoomID = "10010400000001";
            string strFeesIDs = "100104000000000008";
            string strTotalAmount = "0.01";

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20161020PayBillFeesJB";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<Net>2</Net>");
                Attribute.Append("<CorpID>" + strCorpID + "</CorpID>");
                Attribute.Append("<RoomID>" + strRoomID + "</RoomID>");
                Attribute.Append("<FeesIDs>" + strFeesIDs + "</FeesIDs>");
                Attribute.Append("<TotalAmount>" + strTotalAmount + "</TotalAmount>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=PayBillFeesJB&Command=PrepareFeesJBList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button106_Click(object sender, EventArgs e)
        {
            Result.Text = "";
            string repsTxt = "";
            string strUrls = "http://localhost:80/TWInterface/Service/Service.ashx";
            try
            {
                if (TbURL.Text.Trim() != "1")
                {
                    strUrls = TbURL.Text;
                }
                string strDoUrl = strUrls;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160503Equipment";
                int bools = 0;
                string Attribute = "<attributes><Net>2</Net><Account>1000-az</Account><LoginPwd>1</LoginPwd><data>{\"TaskId\":\"fdc53dd2-f162-402c-8467-e5a0b5be5d12\",";
                Attribute += "\"Points\":[{\"PointId\":\"97b0b8a0-ffd9-4af0-8e42-7a7acfee134a\",\"AddTime\":\"2016-9-8 15:01:01\",\"AddPId\":\"000081\"},";
                Attribute += "\"Record\":[{\"id\":0,\"TaskId\":\"fdc53dd2-f162-402c-8467-e5a0b5be5d12\",\"CheckNote\":\"检查1..电梯\",\"CheckRemark\":\"电梯里边有垃圾\",\"CheckResult\":0,\"ProblemType\":\"整改类型\",\"RectificationNote\":\"测试\",\"AbarPIdName\":\"艾中\",\"RectificationPeriod\":\"测试2\",\"ReducePIdName\":\"艾中\",\"PointIds\":\"\"},{\"id\":0,\"TaskId\":\"fdc53dd2-f162-402c-8467-e5a0b5be5d12\",\"CheckNote\":\"检查1..电梯\",\"CheckRemark\":\"电梯里边有垃圾\",\"CheckResult\":0,\"ProblemType\":\"整改类型\",\"RectificationNote\":\"测试\",\"AbarPIdName\":\"艾中\",\"RectificationPeriod\":\"测试2\",\"ReducePIdName\":\"艾中\",\"PointIds\":\"\"}]}</data></attributes>";
                if (TbURL.Text.Trim() == "1")
                {
                    Attribute = DesUrl.Text;
                    bools = 1;
                }
                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=EquipmentManage&Command=SubmitEquipmentTaskList&Attribute=" + Attribute + "&Mac=" + Mac;

                string strNewUrl = strDoUrl + "?" + strContent;
                if (DesUrl.Text != "" && bools == 0)
                {
                    strNewUrl = DesUrl.Text;
                }
                else
                {
                    DesUrl.Text = strNewUrl;

                }
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }


        /// <summary>
        /// 联创支付宝订单生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button107_Click(object sender, EventArgs e)
        {

            TbURL.Text = "http://wy.newhopefdc.cn:9102/Service/Service.ashx";

            string strCommID = "152801";//公司ID
            string strBuildSNum = "1";//可空参数

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804Alipay";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<CommunityId>765cf5b8-b63d-42ba-966a-6360fe189712</CommunityId>");
                Attribute.Append("<FeesIds>183001000000000031</FeesIds>");
                Attribute.Append("<CustID>18300100000001</CustID>");
                //Attribute.Append("<CurrPage>1</CurrPage>");
                //Attribute.Append("<PageSize>10</PageSize>");
                //Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                //string strContent = "Class=Alipay&Command=GenerateOrder&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strContent = "Class=Alipay&Command=GenerateOrder&Attribute=" + strAttribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button108_Click(object sender, EventArgs e)
        {
            Result.Text = "";
            string repsTxt = "";
            string strUrls = "http://localhost:80/TWInterface/Service/Service.ashx";
            try
            {
                if (TbURL.Text.Trim() != "1")
                {
                    strUrls = TbURL.Text;
                }
                string strDoUrl = strUrls;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160503Equipment";
                int bools = 0;
                string Attribute = "<attributes><Net>2</Net><Account>1000-az</Account><LoginPwd>1</LoginPwd><data>{\"TaskId\":\"fdc53dd2-f162-402c-8467-e5a0b5be5d12\",";
                Attribute += "\"Points\":[{\"PointId\":\"97b0b8a0-ffd9-4af0-8e42-7a7acfee134a\",\"AddTime\":\"2016-9-8 15:01:01\",\"AddPId\":\"000081\"},";
                Attribute += "\"Record\":[{\"id\":0,\"TaskId\":\"fdc53dd2-f162-402c-8467-e5a0b5be5d12\",\"CheckNote\":\"检查1..电梯\",\"CheckRemark\":\"电梯里边有垃圾\",\"CheckResult\":0,\"ProblemType\":\"整改类型\",\"RectificationNote\":\"测试\",\"AbarPIdName\":\"艾中\",\"RectificationPeriod\":\"测试2\",\"ReducePIdName\":\"艾中\",\"PointIds\":\"\"},{\"id\":0,\"TaskId\":\"fdc53dd2-f162-402c-8467-e5a0b5be5d12\",\"CheckNote\":\"检查1..电梯\",\"CheckRemark\":\"电梯里边有垃圾\",\"CheckResult\":0,\"ProblemType\":\"整改类型\",\"RectificationNote\":\"测试\",\"AbarPIdName\":\"艾中\",\"RectificationPeriod\":\"测试2\",\"ReducePIdName\":\"艾中\",\"PointIds\":\"\"}]}</data></attributes>";
                if (TbURL.Text.Trim() == "1")
                {
                    Attribute = DesUrl.Text;
                    bools = 1;
                }
                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=EquipmentManage&Command=GetEquipmentTaskList&Attribute=" + Attribute + "&Mac=" + Mac;

                string strNewUrl = strDoUrl + "?" + strContent;
                if (DesUrl.Text != "" && bools == 0)
                {
                    strNewUrl = DesUrl.Text;
                }
                else
                {
                    DesUrl.Text = strNewUrl;

                }
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button109_Click(object sender, EventArgs e)
        {
            Result.Text = "";
            string repsTxt = "";
            string strUrls = "http://localhost:80/TWInterface/Service/Service.ashx";
            try
            {
                if (TbURL.Text.Trim() != "1")
                {
                    strUrls = TbURL.Text;
                }
                string strDoUrl = strUrls;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160503Equipment";
                int bools = 0;
                string Attribute = "";
                if (TbURL.Text.Trim() == "1")
                {
                    Attribute = DesUrl.Text;
                    bools = 1;
                }
                else
                {
                    DesUrl.Text = "按规则操作";

                }
                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=EquipmentManage&Command=GetMaintenanceTaskList&Attribute=" + Attribute + "&Mac=" + Mac;

                string strNewUrl = strDoUrl + "?" + strContent;
                if (DesUrl.Text != "" && bools == 0)
                {
                    strNewUrl = DesUrl.Text;
                }
                else
                {
                    DesUrl.Text = strNewUrl;

                }
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button110_Click(object sender, EventArgs e)
        {
            //
            Result.Text = "";
            string repsTxt = "";
            string strUrls = "http://localhost:80/TWInterface/Service/Service.ashx";
            try
            {
                if (TbURL.Text.Trim() != "1")
                {
                    strUrls = TbURL.Text;
                }
                string strDoUrl = strUrls;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160503Equipment";
                int bools = 0;
                string Attribute = "";
                if (TbURL.Text.Trim() == "1")
                {
                    Attribute = DesUrl.Text;
                    bools = 1;
                }
                else
                {
                    DesUrl.Text = "按规则操作";

                }
                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=EquipmentManage&Command=SubmitMaintenanceTaskList&Attribute=" + Attribute + "&Mac=" + Mac;

                string strNewUrl = strDoUrl + "?" + strContent;
                if (DesUrl.Text != "" && bools == 0)
                {
                    strNewUrl = DesUrl.Text;
                }
                else
                {
                    DesUrl.Text = strNewUrl;

                }
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button111_Click(object sender, EventArgs e)
        {
            //
            Result.Text = "";
            string repsTxt = "";
            string strUrls = "http://localhost:80/TWInterface/Service/Service.ashx";
            try
            {
                if (TbURL.Text.Trim() != "1")
                {
                    strUrls = TbURL.Text;
                }
                string strDoUrl = strUrls;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160503Equipment";
                int bools = 0;
                string Attribute = "";
                if (TbURL.Text.Trim() == "1")
                {
                    Attribute = DesUrl.Text;
                    bools = 1;
                }
                else
                {
                    DesUrl.Text = "按规则操作";

                }
                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=EquipmentManage&Command=SubmitMaintenanceTaskAcceptance&Attribute=" + Attribute + "&Mac=" + Mac;

                string strNewUrl = strDoUrl + "?" + strContent;
                if (DesUrl.Text != "" && bools == 0)
                {
                    strNewUrl = DesUrl.Text;
                }
                else
                {
                    DesUrl.Text = strNewUrl;

                }
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button112_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://wy.newhopefdc.cn:9102/Service.ashx";

            TbURL.Text = "http://localhost:8081/Service/Service.ashx";

            string strCommID = "152801";//公司ID
            string strBuildSNum = "1";//可空参数

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20170406OnlinePayment";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<EndDate>2017-04-13</EndDate>");
                Attribute.Append("<CommID>100013</CommID>");
                Attribute.Append("<CustID>10001300000005</CustID>");
                Attribute.Append("<StateDate>2017-01-01</StateDate>");
                //Attribute.Append("<PageSize>10</PageSize>");
                //Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                //string strContent = "Class=Alipay&Command=GenerateOrder&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strContent = "Class=OnlinePayment&Command=GetHistoricalPaymentList&Attribute=" + strAttribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        /// <summary>
        /// 支付报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button113_Click(object sender, EventArgs e)
        {
            //TbURL.Text = "http://localhost:8081/Service/Service.ashx";

            //string strCommID = "152801";//公司ID
            //string strBuildSNum = "1";//可空参数

            //Result.Text = "";
            //string repsTxt = "";
            //try
            //{
            //    string strDoUrl = TbURL.Text;
            //    string strDate = DateTime.Now.ToString("yyyyMMdd");
            //    string strToken = "20170416Alipay_Xxw";

            //    StringBuilder Attribute = new StringBuilder("");

            //    Attribute.Append("<attributes>");
            //    Attribute.Append("<CommunityId>100013</CommunityId>");
            //    Attribute.Append("<FeesIds>100013000000000053</FeesIds>");
            //    //Attribute.Append("<CustID>18300100000001</CustID>");
            //    //Attribute.Append("<CostID>13360300000004</CostID>");
            //    //Attribute.Append("<RoomID>13360300000013</RoomID>");
            //    Attribute.Append("<CustID>10001300000002</CustID>");

            //    Attribute.Append("</attributes>");
            //    string strAttribute = Attribute.ToString();

            //    string Mac = CreMAC(strAttribute, strDate, strToken);
            //    //string strContent = "Class=Alipay&Command=GenerateOrder&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
            //    string strContent = "Class=Alipay_Xxw&Command=GenerateOrder&Attribute=" + strAttribute + "&Mac=" + Mac;
            //    string strNewUrl = strDoUrl + "?" + strContent;
            //    DesUrl.Text = strNewUrl;
            //    repsTxt = SendHttp(strNewUrl, "");
            //}
            //catch (Exception ex)
            //{
            //    repsTxt = ex.Message.ToString();
            //}
            //Result.Text = Result.Text + "返回：" + repsTxt;

            //获取本期欠费总额

            // DataSet ds = new DbHelperSQLP(ContionString).Query(sb.ToString());


            // Result.Text = sb.ToString();

        }

        private void button114_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:8002/Service/Service.ashx";

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160826OCostInfo";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<CommID>0780b7f8-ecff-4dda-b3c6-5c51f21d8550</CommID>");
                Attribute.Append("<CustID>18180300004105</CustID>");
                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                //string strContent = "Class=Alipay&Command=GenerateOrder&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strContent = "Class=CostInfo&Command=CheckArrears&Attribute=" + strAttribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;

            //DataTable dt = new DataTable();

            //dt.Columns.Add("CustID");

            //dt.Columns.Add("CommID");

            //DataRow row = dt.NewRow();

            //row[0] = "18180300004105";
            //row[1] = "0780b7f8-ecff-4dda-b3c6-5c51f21d8550";//181803

            //Business.CostInfo pay = new Business.CostInfo();

            //string result = pay.CheckArrears(row);

            var ob = new
            {
                result = true,
                data = new
                {
                    name = "",
                    password = ""
                }

            };
            string jsonresult = JsonConvert.SerializeObject(ob);
        }

        private void button114_Click_1(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:63305/HJCRM_Service.asmx";
            string strContent = "op=IList";
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;

                StringBuilder Attribute = new StringBuilder("");

                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button115_Click(object sender, EventArgs e)
        {

            TbURL.Text = "http://localhost:8077/Service/Service.ashx";
            //TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";
            //TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";

            string strCommunityId = "1000_100013";//公司ID  181339   100013

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20161103ResourcesDetails";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<CommunityId>1000_100013</CommunityId>");
                Attribute.Append("<Page>1</Page>");
                Attribute.Append("<PageSize>10</PageSize>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=ResourcesDetails&Command=GetResourcesType&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button116_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:8077/Service/Service.ashx";
            //TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";
            //TbURL.Text = "http://125.64.16.10:9999/TWInterface/Service/Service.ashx";

            string strCommunityId = "1000_100013";//公司ID  181339   100013

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20161103ResourcesDetails";

                StringBuilder Attribute = new StringBuilder("");


                Attribute.Append("<attributes>");
                Attribute.Append("<ResourcesTypeID>ff88a80a-3cea-4625-994b-496429ce1ccb</ResourcesTypeID>");
                Attribute.Append("<CommunityId>100013</CommunityId>");
                Attribute.Append("<CorpID>1000</CorpID>");

                Attribute.Append("<Page>1</Page>");
                Attribute.Append("<PageSize>5</PageSize>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=ResourcesDetails&Command=GetHomeCommodity&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }
        /// <summary>
        /// 推送消息额外信息类型
        /// </summary>
        public enum JPushMsgType
        {
            /// <summary>
            /// 默认为0
            /// </summary>
            DEFAULT = 0,
            /// <summary>
            /// 报事抢单
            /// </summary>
            REPORT_ROB_SINGLE = 11,

            /// <summary>
            /// 报事派单
            /// </summary>
            REPORT_DISPATCH = 12,

            /// <summary>
            /// 报事处理
            /// </summary>
            REPORT_DEAL = 13
        }
        private void button117_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            //dic.Add("key" + i, new ApiResult(i % 2 == 0 ? true : false, "测试" + i));
            List<ApiResult> list = new List<ApiResult>();
            list.Add(new ApiResult(true, "测试"));
            for (int i = 0; i < 3; i++)
            {
                dic.Add("key" + i, new ApiResult(i % 2 == 0 ? true : false, "测试" + i));
            }
            dic.Add("list", list);


            string resule = new ApiResult(false, dic).toJson();
            // PushUrl.Text = "http://localhost:8909/Service/Service.ashx";
            PushUrlSure.Text = PushUrl.Text;
            string repsTxt = "";
            try
            {
                string strDoUrl = PushUrlSure.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160907AppPush";
                string Attribute = "<attributes><Package>" + Package.Text.ToString() + "</Package><Tag>" + Tag.Text.ToString() + "</Tag><Title>" + TITLE.Text.ToString() + "</Title><MsgContent>" + MSG_CONTENT.Text.ToString() + "</MsgContent><JPushMsgType>" + JPushMsgType.REPORT_DISPATCH + "</JPushMsgType><Data>" + 12345 + "</Data></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=AppPush&Command=PushTag&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                PushUrlSure.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            PushRichTextBox.Text = "返回：" + repsTxt;
        }

        private void button118_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:63305/Service.ashx";

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20170515CostInfo_HJ";

                StringBuilder Attribute = new StringBuilder("");
                Attribute.Append("<attributes>");
                Attribute.Append("<CustID>15601300000006</CustID>");
                Attribute.Append("<Phone>13308461114</Phone>");
                Attribute.Append("<State>3</State>");
                Attribute.Append("<CommID>bc92c767-4b05-4379-9815-d04b306ac7e0</CommID>");
                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CostInfo_HJ&Command=GetIncidentInfoList_HJ&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button119_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:63305/Service.ashx";

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20170515CostInfo_HJ";

                StringBuilder Attribute = new StringBuilder("");
                Attribute.Append("<attributes>");
                Attribute.Append("<IncidentID>156013000000000045</IncidentID>");
                Attribute.Append("<CommID>bc92c767-4b05-4379-9815-d04b306ac7e0</CommID>");
                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CostInfo_HJ&Command=GetIncidentInfo_HJ&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button120_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:63305/Service.ashx";

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160803Location";

                StringBuilder Attribute = new StringBuilder("");
                Attribute.Append("<attributes>");
                Attribute.Append("<location>104.078995,30.630444</location>");
                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=Location&Command=GetCommListNearBy&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button121_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost/HM2017new/TWInterface/Service/Service.ashx";
            //TbURL.Text = "http://wuye.yidachina.com/TWInterface/Service/Service.ashx";
            string strRoomID = "15280100000001";//"";  //

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20170826PreBalanceSummarySerchRX";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<Net>2</Net>");
                Attribute.Append("<RoomID>" + strRoomID + "</RoomID>");
                //Attribute.Append("<BeginDate></BeginDate>");
                //Attribute.Append("<EndDate></EndDate>");
                //Attribute.Append("<CurrPage>1</CurrPage>");
                //Attribute.Append("<PageSize>10</PageSize>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=PreBalanceSummarySerchRX&Command=PreBalanceSummarySerchRXList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        /// <summary>
        /// 接口测试
        /// </summary>
        private void button122_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbClass.Text.Trim()) ||
                string.IsNullOrEmpty(this.tbCommand.Text.Trim()) ||
                string.IsNullOrEmpty(this.tbToken.Text.Trim()) ||
                string.IsNullOrEmpty(this.tbParameters.Text.Trim()))
            {
                return;
            }

            this.tbMAC.Text = CreMAC(this.tbParameters.Text, DateTime.Now.ToString("yyyyMMdd"), this.tbToken.Text);
            string url = string.Format("{0}?Class={1}&Command={2}&Attribute={3}&Mac={4}&Agreement={5}", this.tbInterfaceRemoteURL.Text,
                this.tbClass.Text, this.tbCommand.Text, this.tbParameters.Text, this.tbMAC.Text, txt_agreement.Text);
            this.tb_url.Text = url;
            this.tbInterfaceResult.Text = SendHttp(url, "");
        }

        private void button123_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbClass.Text.Trim()) ||
                string.IsNullOrEmpty(this.tbCommand.Text.Trim()) ||
                string.IsNullOrEmpty(this.tbToken.Text.Trim()) ||
                string.IsNullOrEmpty(this.tbParameters.Text.Trim()))
            {
                return;
            }
            this.tbMAC.Text = CreMAC(this.tbParameters.Text, DateTime.Now.ToString("yyyyMMdd"), this.tbToken.Text);
            string url = string.Format("{0}?Class={1}&Command={2}&Attribute={3}&Mac={4}&Agreement={5}", this.tbInterfaceLoalURL.Text,
                this.tbClass.Text, this.tbCommand.Text, this.tbParameters.Text, this.tbMAC.Text, txt_agreement.Text);

            this.tbInterfaceResult.Text = SendHttp(url, "");
            //TWTools.Logger.TWRollingFileLogger.Test("app", "tw.property.android");
        }

        private void tbToken_TextChanged(object sender, EventArgs e)
        {
            this.tbMAC.Text = CreMAC(this.tbParameters.Text, DateTime.Now.ToString("yyyyMMdd"), this.tbToken.Text);
        }

        private void tbParameters_TextChanged(object sender, EventArgs e)
        {
            this.tbMAC.Text = CreMAC(this.tbParameters.Text, DateTime.Now.ToString("yyyyMMdd"), this.tbToken.Text);
        }

        private void button124_Click(object sender, EventArgs e)
        {

            TbURL.Text = "http://123.207.146.68:16666/TWInterface/Service/Service.ashx";//升龙

            string strCommID = "182460";
            string strMemo = "xp测试临停收费";
            string strAmount = "1.00";
            string strFeesStartDate = "2017-11-02 18:03:16";
            string strFeesEndDate = "2017-11-02 18:06:18";
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20170804CarLock_SL";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<commID>" + strCommID + "</commID>");
                Attribute.Append("<Memo>" + strMemo + "</Memo>");
                Attribute.Append("<amount>" + strAmount + "</amount>");
                Attribute.Append("<FeesStartDate>" + strFeesStartDate + "</FeesStartDate>");
                Attribute.Append("<FeesEndDate>" + strFeesEndDate + "</FeesEndDate>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CarLock_SL&Command=createCarFees&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button125_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://113.247.231.148:8093/TWInterface/Service/Service.ashx";//北方

            string strCommID = "100104";
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20171107TypeIDSearch";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<CommID>" + strCommID + "</CommID>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=IncidentTypeIDSearchBF&Command=TypeIDSearchList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button126_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:63305/Service.ashx";

            //TbURL.Text = "http://125.64.16.10:9998/Service.ashx";

            string strTradeDate = DateTime.Now.ToString("yyyy-MM-dd");
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160914IncidentAccept";
                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<Commid>E4918A7C-DBE3-4D19-9117-FC7CEAC983EE</Commid>");
                Attribute.Append("<CustID>19730300000577</CustID>");
                Attribute.Append("<Content>1111111</Content>");
                Attribute.Append("<RegionalID>1973030000000001</RegionalID>");
                Attribute.Append("<Phone>18810812788</Phone>");
                Attribute.Append("<images></images>");
                Attribute.Append("<IncidentMan>吴立刚</IncidentMan>");
                //Attribute.Append("<IncidentContent>水电费</IncidentContent>");
                //Attribute.Append("<IncidentImgs>http://172.19.50.176:9999/19fc6c8c2b12430c84b007463cc07814.jpg,http://172.19.50.176:9999/26ce592c152041ab9371aac483d1f947.jpg,http://172.19.50.176:9999/eb6a9c40e3914ae6aacbc23f0793e59e.jpg</IncidentImgs>");
                Attribute.Append("</attributes>");

                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);

                string strContent = "Class=IncidentAccept&Command=SetIncidentAcceptPhoneInsertRegion&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;

                string strNewUrl = strDoUrl + "?" + strContent;

                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button127_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:8567/Service.ashx";

            string strTradeDate = DateTime.Now.ToString("yyyy-MM-dd");
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20170707CarLock_ZD";
                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<CarNo>川A88888</CarNo>");
                Attribute.Append("<lockFlag>0</lockFlag>");
                Attribute.Append("</attributes>");

                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);

                string strContent = "Class=CarLock_ZD&Command=QF_LockCar&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;

                string strNewUrl = strDoUrl + "?" + strContent;

                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button128_Click(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long t = (time.Ticks - startTime.Ticks) / 10000000;

            TbURL.Text = "https://landkefuqa.cofco.com/rest/api/thirdParty/upRepairResult";

            string code = geAuthCode(t);

            string param = "{\"dataMap\":{\"viewTime\":\"2017-11-16 08:47:23\",\"viewRecord\":\"测试\",\"contactTime\":\"2017-11-16 08:47:23\",\"result\":\"测试\",\"head\":\"测试\",\"headTelephone\":\"18380471771\",\"planEndTime\":\"2017-11-16 08:47:23\",\"handleTime\":\"2017-11-16 08:47:23\",\"endTime\":\"2017-11-16 08:47:23\",\"handleResult\":\"测试\",\"imgsId\":\"测试\"},\"jobId\":\"100013000000000618\",\"clientCode\":\"3c941fa2c0bc4aa2aefa6fbd39d396ab\",\"reqTime\": " + t + ",\"authCode\": \"" + code + "\"}";
            DesUrl.Text = TbURL.Text + param;
            string res = HttpHelper.Post(TbURL.Text, param);
            Result.Text = Result.Text + "返回：" + res + "\r\n" + param;
        }

        private string geAuthCode(long req_time)
        {

            string securityClientCode = "3c941fa2c0bc4aa2aefa6fbd39d396ab";
            string securityClientSecret = "698ad2f31e134c1eb38dd5366c4274af";

            //采用排序的Dictionary的好处是方便对数据包进行签名，不用再签名之前再做一次排序
            SortedDictionary<string, string> m_values = new SortedDictionary<string, string>();
            m_values["client_code"] = securityClientCode;
            m_values["client_secret"] = securityClientSecret;
            m_values["req_time"] = req_time + "";

            m_values["viewTime"] = "2017-11-16 08:47:23";
            m_values["viewRecord"] = "测试";
            m_values["contactTime"] = "2017-11-16 08:47:23";
            m_values["result"] = "测试";
            m_values["head"] = "测试";
            m_values["headTelephone"] = "18380471771";
            m_values["planEndTime"] = "2017-11-16 08:47:23";
            m_values["handleTime"] = "2017-11-16 08:47:23";
            m_values["endTime"] = "2017-11-16 08:47:23";
            m_values["handleResult"] = "测试";
            m_values["imgsId"] = "测试";

            string buff = "";
            foreach (KeyValuePair<string, string> pair in m_values)
            {
                if (pair.Value.ToString() != "")
                {
                    buff += pair.Value;
                }
            }
            string code = AppPKI.getMd5Hash(buff);
            return code;
        }

        private void button129_Click(object sender, EventArgs e)
        {

            //TbURL.Text = "http://localhost:8567/Service.ashx";

            TbURL.Text = "http://125.64.16.10:9998/Service.ashx";

            string strTradeDate = DateTime.Now.ToString("yyyy-MM-dd");
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20171113Incident_MJ";
                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<ReceptionNum>12121211</ReceptionNum>");
                Attribute.Append("<IncidentState>已完结</IncidentState>");
                Attribute.Append("<IncidentTime>" + strDate + "</IncidentTime>");
                Attribute.Append("</attributes>");

                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);

                string strContent = "Class=Incident_MJ&Command=UpdateIncidentState&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;

                string strNewUrl = strDoUrl + "?" + strContent;

                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button130_Click(object sender, EventArgs e)
        {
            //接待唯一标识
            string WyReceiveGUID = "123123123";
            //明源房号
            string RoomCode = "123123123"; ;
            //服务请求人
            string RequestMan = "张三"; ;
            //联系电话
            string MobileTel = "18380471771"; ;
            //是否投诉
            string isTouSu = "否";
            //是否托管钥匙
            string IsTrustKey = "否";
            //预约时间
            string CstTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); ;
            //接待时间
            string ReceiveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); ;
            //分派时间
            string AllotTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //接待人
            string ReceiveMan = "测试";
            //接待描述
            string Content = "测试";

            //TbURL.Text = "http://172.18.18.39:9001/ErpWebService/RcbxService.asmx?op=GetRcbxMessage&jsonString=";
            //string posturl = "http://172.18.18.39:9001/ErpWebService/RcbxService.asmx?op=GetRcbxMessage&jsonString=";
            string parma = "{\"WyReceiveGUID\":\"" + WyReceiveGUID + "\",\"RoomCode\":\"" + RoomCode + "\",\"RequestMan\":\"" + RequestMan + "\",\"MobileTel\":\"" + MobileTel + "\",\"isTouSu\":\"" + isTouSu + "\",\"IsTrustKey\":\"" + IsTrustKey + "\",\"ReceiveMan\":\"" + ReceiveMan + "\",\"CstTime\":\"" + CstTime + "\",\"ReceiveDate\":\"" + ReceiveDate + "\",\"AllotTime\":\"" + AllotTime + "\",\"Content\":\"" + Content + "\"}";


            //DesUrl.Text = TbURL.Text + parma;
            //string res = HttpHelper.Get(posturl + parma);

            //2017-11-23
            //string res = GetjieDai();

            //2017-11-24
            Hashtable ht = new Hashtable();
            ht.Add("jsonString", parma);
            try
            {
                XmlDocument xml = WebServiceHelper.QuerySoapWebService("http://172.18.18.39:9001/ErpWebService/RcbxService.asmx", "GetRcbxMessage", ht);
                SortedDictionary<string, object> s_value = WebServiceHelper.FromXmlTo(xml.ToString());
                Result.Text = Result.Text + "返回：" + JsonConvert.SerializeObject(s_value);
            }
            catch (Exception msg)
            {
                Result.Text = Result.Text + "返回：" + msg.Message;
            }

        }

        public string GetjieDai()
        {
            //接待唯一标识
            string WyReceiveGUID = "123123123";
            //明源房号
            string RoomCode = "123123123"; ;
            //服务请求人
            string RequestMan = "张三"; ;
            //联系电话
            string MobileTel = "18380471771"; ;
            //是否投诉
            string isTouSu = "否";
            //是否托管钥匙
            string IsTrustKey = "否";
            //预约时间
            string CstTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); ;
            //接待时间
            string ReceiveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); ;
            //分派时间
            string AllotTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //接待人
            string ReceiveMan = "测试";
            //接待描述
            string Content = "测试";
            string sPlan = "http://172.18.18.39:9001/ErpWebService/RcbxService.asmx";
            cParaInfo cinfo = new cParaInfo();
            cinfo.op = "GetRcbxMessage";
            string parma = "{\"WyReceiveGUID\":\"" + WyReceiveGUID + "\",\"RoomCode\":\"" + RoomCode + "\",\"RequestMan\":\"" + RequestMan + "\",\"MobileTel\":\"" + MobileTel + "\",\"isTouSu\":\"" + isTouSu + "\",\"IsTrustKey\":\"" + IsTrustKey + "\",\"ReceiveMan\":\"" + ReceiveMan + "\",\"CstTime\":\"" + CstTime + "\",\"ReceiveDate\":\"" + ReceiveDate + "\",\"AllotTime\":\"" + AllotTime + "\",\"Content\":\"" + Content + "\"}";
            cinfo.jsonString = parma;
            string strJson = JsonConvert.SerializeObject(cinfo);
            HttpWebRequest requests = (HttpWebRequest)WebRequest.Create(sPlan);
            requests.Method = "POST";
            requests.KeepAlive = false;
            requests.ContentType = "application/json;charset=UTF-8";
            byte[] payload = Encoding.Default.GetBytes(strJson);
            requests.ContentLength = payload.Length;
            requests.Timeout = 600000;
            Stream myStream;
            HttpWebResponse myResp;
            try
            {
                myStream = requests.GetRequestStream();
                myStream.Write(payload, 0, payload.Length);
                myStream.Close();

                myResp = (HttpWebResponse)(requests.GetResponse());//这里错了
                myStream = myResp.GetResponseStream();//取出返回值
                StreamReader sr = new StreamReader(myStream, Encoding.GetEncoding("UTF-8"));
                string strShwo = sr.ReadToEnd();
                JObject json2 = (JObject)JsonConvert.DeserializeObject(strShwo);
                if (json2["Result"].ToString() == "true")
                {
                    return "OK";
                }
                return json2["data"].ToString();

            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        private void button131_Click(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long t = (time.Ticks - startTime.Ticks) / 10000000;

            TbURL.Text = "https://landkefuqa.cofco.com/rest/api/thirdParty/returnRepairJob";

            string code = geAuthCode(t);
            string param = "{\"jobId\":\"100013000000000618\",\"client_code\":\"3c941fa2c0bc4aa2aefa6fbd39d396ab\",\"req_time\": " + t + ",\"authcode\": \"" + code + "\"}";
            DesUrl.Text = TbURL.Text + param;
            string res = HttpHelper.Post(TbURL.Text, param);
            Result.Text = Result.Text + "返回：" + res + "\r\n" + param;
        }

        private void button132_Click(object sender, EventArgs e)
        {

            //TbURL.Text = "http://localhost:8567/Service.ashx";
            TbURL.Text = "http://125.64.16.10:9998/Service.ashx";

            string strTradeDate = DateTime.Now.ToString("yyyy-MM-dd");
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160914IncidentAccept";
                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<City>市辖区</City>");
                Attribute.Append("</attributes>");

                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);

                string strContent = "Class=IncidentAccept&Command=GetCOmmunity&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;

                string strNewUrl = strDoUrl + "?" + strContent;

                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button131_Click_1(object sender, EventArgs e)
        {
            string connStr = "Pooling=false;Data Source=47.107.176.138;Initial Catalog=Erp_Wygl_6008;User ID=LFUser;Password=LF123SPoss";
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT RID AS RenovationID,ID AS OutEnterCertificateID,Name,Sex,Photo,Phone,IDCard,OutEnterCertificateNum,OutEnterCertificateName,HandleCertificateDate,CertificateStartDate,CertificateEndDate,CertificateState FROM (SELECT *, ROW_NUMBER() OVER(ORDER BY HandleCertificateDate) AS RowId FROM (SELECT * FROM View_Tb_Reno_HandleCertificate) AS a WHERE IsDelete = 0 AND RID = 'c38bc9a5-1c05-4353-982f-c694e99f3d4e') AS a WHERE RowId BETWEEN 1 AND 10";
                dynamic RenovationInfo = conn.QueryFirstOrDefault(sql, new { CommID = "601413", RoomID = "60141300000166" });
                tbContent.Text = new ApiResult(true, RenovationInfo).toJson() ;
            }
        }

        // 泰禾门禁获取钥匙
        private void button132_Click_1(object sender, EventArgs e)
        {

        }

        private void button133_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost:8099/Service.ashx";
            //TbURL.Text = "http://125.64.16.10:9998/Service.ashx";

            string strTradeDate = DateTime.Now.ToString("yyyy-MM-dd");
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20180607CallCenter_JF_IVR";
                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<callerno>13551486166</callerno>");
                Attribute.Append("<ordertype>1</ordertype>");
                Attribute.Append("</attributes>");

                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);

                string strContent = "Class=CallCenter_JF_IVR&Command=CallCenterIVR&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;

                string strNewUrl = strDoUrl + "?" + strContent;

                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button134_Click(object sender, EventArgs e)
        {
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "DAA0CEE92188";

                StringBuilder Attribute = new StringBuilder("");
                Attribute.Append("<attributes>");
                Attribute.Append("<Net>2</Net>");
                Attribute.Append("<Account>1000-az</Account>");
                Attribute.Append("<LoginPwd>1</LoginPwd>");
                Attribute.Append("<UserCode>000081</UserCode>");
                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=Video_HJ&Command=CommList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }




        private void button135_Click(object sender, EventArgs e)
        {

            //以JSON格式提交的参数数据
            string data = "{ \"WYRoomId\": \"1\",\"WYRoomUserId\": \"1\",\"WYRoomReceiveTime\": \"2016-03-03\",\"Remark\": \"1\"}";
            //通过DES3加密
            string package = EncryptDES(data, "841957F61CE743E890D52763", "B537CA85");
            //时间戳加密
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string UnixTimeStamp = EncryptDES(Convert.ToInt64(ts.TotalSeconds).ToString(), "841957F61CE743E890D52763", "B537CA85");
            string data1 = $"{{\"companyid\":\"B04AFAA8-24E6-451F-9EA1-EC785D3EDF66\",\"channelid\":\"TW\",\"time\":\"{UnixTimeStamp}\",\"package\":\"{package}\"}}";
            //textBox2.Text = SendHttp(url, "");
            textBox2.Text = DecryptDES(SendHttp("http://59.37.49.174:4000/fhh.api.open/BgyFhh/TW_UpdateRoomReceiveState", data1), "841957F61CE743E890D52763", "B537CA85");

        }

        private string PostWebRequest(string postUrl, string paramData)
        {
            string ret = string.Empty;

            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(paramData); //转化
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
            webReq.Method = "POST";
            webReq.ContentType = "application/json";
            webReq.Accept = "application/xml";

            webReq.ContentLength = byteArray.Length;
            Stream newStream = webReq.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);//写入参数
            newStream.Close();
            HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
            ret = sr.ReadToEnd();
            sr.Close();
            response.Close();
            newStream.Close();
            return ret;

        }

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

        private void button136_Click(object sender, EventArgs e)
        {
            string str = "杭州映月-一期-HZYY-一期-16幢-1-0101";
            MessageBox.Show(str.Split('-')[5]);

            MessageBox.Show(str.Substring(str.Length - 4, 2));


            MessageBox.Show(str.Substring(str.Length - 2));
        }

        private void button137_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://tw.cqhyrc.com.cn/TWInterface/Service/Service.ashx";
            //TbURL.Text = "http://localhost/TWInterface/Service/Service.ashx";


            string strCustID = "19402300000001";//业主ID  

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20180726CostStanSettingSearch";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                Attribute.Append("<CustID>" + strCustID + "</CustID>");
                Attribute.Append("<FileType>JSON</FileType>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CostStanSettingSearch&Command=CostStanList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button138_Click(object sender, EventArgs e)
        {
            //Alipay_Url.Text = "http://120.79.213.157:9999/TWInterface/Service/Service.ashx";
            Alipay_Url.Text = "http://localhost:8909/TWInterface/Service/Service.ashx";
            Alipay_Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = Alipay_Url.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20160804Test";
                string Attribute = "<attributes><ProductList><Product><Id>4a5abb08-1290-40de-8390-09b3eaaa6b54</Id><Quantity>1</Quantity><ShoppingId>5a0c4589-bf60-4669-8bdb-df03cb18afc4</ShoppingId><RpdMemo></RpdMemo></Product></ProductList><UserId>06663d34-3d6c-40ca-907a-e3941f131bf3</UserId><BussId>1000032</BussId><Name>测试用户</Name><Mobile>13699014325</Mobile><DeliverAddress>四川省成都市武侯区棕南正街啊啊啊</DeliverAddress><ReceiptMemo>00</ReceiptMemo></attributes>";
                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=Log4Test&Command=Test&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                Alipay_ToUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Alipay_Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button139_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] fileByte = new byte[] { };
                fileByte = System.IO.File.ReadAllBytes(Environment.CurrentDirectory + "\\" + textBox6.Text);
                string strFileByte = Convert.ToBase64String(fileByte);

                //ParamText entity = new ParamText();
                //entity.esealId = "1357902468qpzmg20180717113440191a25f380afc614cf68408319e509cdbe3";
                //entity.pdfBase64 = strFileByte;
                //entity.xIncrement = textBox9.Text.Trim();
                //entity.yIncrement = textBox10.Text.Trim();
                //entity.text = textBox11.Text.Trim();

                //string url = "http://10.10.166.145:8080" + "/sign/cachet/text";
                //string ParamStr = oo.GetEntityToParamStr();
                NameValueCollection entityNvc = new NameValueCollection();
                entityNvc.Add("esealId", "1357902468qpzmg20180717113440191a25f380afc614cf68408319e509cdbe3");
                entityNvc.Add("pdfBase64", strFileByte);
                entityNvc.Add("xIncrement", textBox9.Text.Trim());
                entityNvc.Add("yIncrement", textBox10.Text.Trim());
                entityNvc.Add("text", textBox11.Text.Trim());
                WebClient webClient = new WebClient();
                webClient.Encoding = System.Text.Encoding.UTF8;
                byte[] Formatring = webClient.UploadValues(textBox4.Text, "POST", entityNvc);
                string result = System.Text.Encoding.UTF8.GetString(Formatring);
                textBox2.Text = result;
                ResultMsg entityRm = JsonConvert.DeserializeObject<ResultMsg>(result);
                if (entityRm.code == 200)
                {
                    fileByte = Convert.FromBase64String(entityRm.msg);
                    //string path = Environment.CurrentDirectory + "\\"+Guid.NewGuid().ToString("N")+".pdf";
                    //System.IO.File.WriteAllBytes(path, fileByte);

                    string fullName = Guid.NewGuid().ToString("N") + ".pdf";
                    ObjectMetadata metadata = new ObjectMetadata();
                    // 可以设定自定义的metadata。
                    metadata.ContentType = "application/pdf";
                    metadata.UserMetadata.Add("uname", "airong");
                    metadata.UserMetadata.Add("fromfileName", fullName);
                    OssClient client = OssManager.GetInstance();
                    MemoryStream ms = new MemoryStream(fileByte, 0, fileByte.Length);
                    PutObjectResult ret = client.PutObject(OssManager.BucketName, fullName, ms, metadata);
                    textBox3.Text = ret.ETag;
                    Uri uri = client.GeneratePresignedUri(new GeneratePresignedUriRequest(OssManager.BucketName, fullName, SignHttpMethod.Get));
                    textBox1.Text = uri.AbsoluteUri;
                }



            }
            catch (Exception ex)
            {

                textBox2.Text = ex.ToString();
            }
        }

        private void button140_Click(object sender, EventArgs e)
        {
            textBox4.Text = "http://10.10.166.145:8080/sign/cachet/list";
            NameValueCollection entityNvc = new NameValueCollection();
            entityNvc.Add("projectCode", textBox12.Text.Trim());
            WebClient webClient = new WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;
            byte[] Formatring = webClient.UploadValues(textBox4.Text, "POST", entityNvc);
            string result = System.Text.Encoding.UTF8.GetString(Formatring);
            textBox2.Text = result;
        }

        private void button141_Click(object sender, EventArgs e)
        {
            //  Class = JUNFA_CCLS & Command = CallCenterCollectData & Attribute =< Attribute >< HRL > 0 </ HRL >< HCL > 0 </ HCL >< PJJTSJ > 0 </ PJJTSJ >< PJDDSC > 0 </ PJDDSC >< RJJTL > 0 </ RJJTL >< RGJTL > 0 </ RGJTL >< HWMYD > 100 </ HWMYD >< BRHRJTL > 0 </ BRHRJTL >< BRWHJTL > 0 </ BRWHJTL >< HRPJSC > 0 </ HRPJSC >< WHPJSC > 0 </ WHPJSC ></ Attribute > &Mac = 282FB7EE4A3404C0C5B7A0D9B365A766


            //Alipay_Url.Text = "http://120.79.213.157:9999/TWInterface/Service/Service.ashx";
            Alipay_Url.Text = "http://120.79.144.240/TWInterface/Service/Service.ashx";
            Alipay_Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = Alipay_Url.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20181103JF";
                string Attribute = "<Attribute><HRL>0</HRL><HCL>0</HCL><PJJTSJ>0</PJJTSJ><PJDDSC>0</PJDDSC><RJJTL>0</RJJTL><RGJTL>0</RGJTL><HWMYD>100</HWMYD><BRHRJTL>0</BRHRJTL><BRWHJTL>0</BRWHJTL><HRPJSC>0</HRPJSC><WHPJSC>0</WHPJSC></Attribute>";
                string Mac = CreMAC(Attribute, strDate, strToken);

                string HashString = Attribute + DateTime.Now.ToString("yyyyMMdd") + strToken;
                //new Logger().WriteLog("HashString", HashString);
                string Mac2 = AppPKI.getMd5Hash(HashString);

                string strContent = "Class=JUNFA_CCLS&Command=CallCenterCollectData&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                Alipay_ToUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Alipay_Result.Text = Result.Text + "返回：" + repsTxt;

        }

        private void button142_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://localhost/TWInterface/Service/Service.ashx";
            //TbURL.Text = "http://218.13.58.36:8888/Service/Service.ashx";
            DesUrl.Text = TbURL.Text;
            string repsTxt = "";
            try
            {
                string strDoUrl = DesUrl.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20181114THJCSJCX";


                string Attribute = "<attributes><PageIndex>1</PageIndex><PageSize>10</PageSize><UpdateTime>0x00000000016FB7F8</UpdateTime></attributes>";

                string Mac = CreMAC(Attribute, strDate, strToken);

                //DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                //long lTime;
                //if (timeStamp.Length.Equals(10))
                ////判断是10位 
                //{ lTime = long.Parse(timeStamp + "0000000"); }
                //else
                //{
                //    lTime = long.Parse(timeStamp + "0000");
                //    //13位 
                //}
                //TimeSpan toNow = new TimeSpan(lTime);
                //DateTime daTime = dtStart.Add(toNow);
                //string time = daTime.ToString("yyyyMMddHHmmss");//转为了string格式 return time;



                string strContent = "Class=CommRoomHouseholdCustomerTH&Command=GetFWXX&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = "返回：" + DesUrl;
        }

        private void button143_Click(object sender, EventArgs e)
        {
            //http://10.0.141.15:19999/TWInterface/Service/Service.ashx


            TbURL.Text = "http://127.0.0.1:9999/TWInterface/Service/Service.ashx";





            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20180829CostInfo_RS";
//                10.00,2.00,10.00,7.00,1.00,0.10,6.00,6.00
//203618000000005405,203618000000005407,203618000000005439,203618000000005440,203618000000005462,203618000000005620,203618000000005621,203618000000005628

                StringBuilder Attribute = new StringBuilder("");

 
          
                Attribute.Append("<attributes><CommName>廊坊-四季花语</CommName><ChargeMode>支付宝支付</ChargeMode>" +
                    "<CustGuidID>44310</CustGuidID><DueAmount>3057.00</DueAmount><RoomGuidID>44310</RoomGuidID>" +
                    "<FeesIDs>203671000000120362,203671000000120363,203671000000120364,203671000000120365,203671000000120366,203671000000120367" +
                    ",203671000000120368,203671000000120369,203671000000120370,203671000000120371,203671000000120372,203671000000120373</FeesIDs>" +
                    "</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CostInfo_RS&Command=PostFeesUnder&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void button147_Click(object sender, EventArgs e)
        {

            textBox22.Text = "http://39.108.152.154:8688/Erp/GetZoneList";

            var model = new
            {
                pageNo = 1,
                pageSize = 10
            };
            string key = "a0a71b2fb79f35a12a740ede18a47113";
            string strJson = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string cbc = strJson.EncodeCBC(key);
            string md5 = strJson.Md5();
            //以JSON格式提交的参数数据
            string data = $"{{ \"AppID\": \"\",\"WarID\": \"5aSp6Zeu\",\"ParaHashCBC\": \"{cbc}\",\"ParaHashMd5\": \"{md5}\"}}";

            textBox20.Text = data;
            textBox21.Text = SendHttp(textBox22.Text, data);

          
        }

        private void button148_Click(object sender, EventArgs e)
        {
            textBox22.Text = "http://39.108.152.154:8688/Erp/GetZoneList";
        }

        private void button149_Click(object sender, EventArgs e)
        {
            textBox22.Text = "http://39.108.152.154:8688/Erp/GetYardBillingCategoryData";
        }

        private void button150_Click(object sender, EventArgs e)
        {
            textBox22.Text = "http://39.108.152.154:8688/Erp/GetYardBillingCategoryData";
        }

        private void button151_Click(object sender, EventArgs e)
        {
            textBox22.Text = "http://39.108.152.154:8688/Erp/AudAuthorizationPlate";
        }

        private void button146_Click(object sender, EventArgs e)
        {

            var model = new
            {
                pageNo = 1,
                pageSize = 10
            };
            string strJson = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string cbc = EncryptionMode.EncodeCBC(strJson);
            string md5 = HashMd5.EncodeCBC(cbc);
            //以JSON格式提交的参数数据
            string data = $"{{ \"AppID\": \"\",\"WarID\": \"5aSp6Zeu\",\"ParaHashCBC\": \"{cbc}\",\"ParaHashMd5\": \"{md5}\"}}";
            SendHttp(textBox22.Text, data);
        }

        private void button152_Click(object sender, EventArgs e)
        {
            string date = "2018-7-18";
            for (int i = 0; i < 56; i++)
            {
                string  begindate2 = Convert.ToDateTime(date).AddDays(i*7).ToShortDateString();

                string begindate3 = Convert.ToDateTime(begindate2).AddDays(7).ToShortDateString();

                string strDoUrl = "http://localhost:9999/TWInterface/Service/Service.ashx";
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20180803GetLL_OA_NEW";


                StringBuilder Attribute = new StringBuilder("");
                Attribute.Append("<attributes>");
                Attribute.Append("<count>2000</count>");


                Attribute.Append("<begindate>"+ begindate2 + "</begindate>");
                Attribute.Append("<enddate>" + begindate3 + "</enddate>");


                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();
                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=GetLL_OA_NEW&Command=GetLLOA_personTenure&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                string Res = SendHttp(strNewUrl, "");
            }
           
        }

        private void button153_Click(object sender, EventArgs e)
        {
            string strDoUrl = "http://localhost:63305/Service.ashx";
            string strToken = "20191029WeiXinPay";
            string strDate = DateTime.Now.ToString("yyyyMMdd");

            StringBuilder Attribute = new StringBuilder("");
            Attribute.Append("<attributes>");
            Attribute.Append("<CommunityId>E4918A7C-DBE3-4D19-9117-FC7CEAC983EE</CommunityId>");
            Attribute.Append("<CustID>19730200001386</CustID>");
            Attribute.Append("<RoomID>19730200001501</RoomID>");            

            string data = "{\"Type\":1,\"Data\":[{\"FeesId\":\"197302000000260930\"},{\"FeesId\":\"197302000000260918\"},{\"FeesId\":\"197302000000260919\"},{\"FeesId\":\"197302000000260920\"},{\"FeesId\":\"197302000000260931\"},{\"FeesId\":\"197302000000260921\"},{\"FeesId\":\"197302000000260922\"},{\"FeesId\":\"197302000000260923\"},{\"FeesId\":\"197302000000260932\"},{\"FeesId\":\"197302000000260924\"},{\"FeesId\":\"197302000000260925\"},{\"FeesId\":\"197302000000427110\"},{\"FeesId\":\"197302000000260926\"},{\"FeesId\":\"197302000000260933\"},{\"FeesId\":\"197302000000222663\"},{\"FeesId\":\"197302000000260927\"},{\"FeesId\":\"197302000000222664\"},{\"FeesId\":\"197302000000260928\"},{\"FeesId\":\"197302000000222665\"},{\"FeesId\":\"197302000000260929\"}]}";
            Attribute.Append("<PayData>" + data + "</PayData>");
            Attribute.Append("<PayType>"+ data + "</PayType>");

            Attribute.Append("</attributes>");
            string strAttribute = Attribute.ToString();
            string Mac = CreMAC(strAttribute, strDate, strToken);

            string strContent = "Class=WeiXinPay_New&Command=GenerateOrder&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
            string strNewUrl = strDoUrl + "?" + strContent;
            string Res = SendHttp(strNewUrl, "");
        }

        private void button154_Click(object sender, EventArgs e)
        {
            string strDoUrl = "http://localhost:63305/Service.ashx";
            string strToken = "20200103JDPay";
            string strDate = DateTime.Now.ToString("yyyyMMdd");

            StringBuilder Attribute = new StringBuilder("");
            Attribute.Append("<attributes>");
            Attribute.Append("<CommunityId>4D992137-104F-47AF-99DB-40EE003A26B2</CommunityId>");
            Attribute.Append("<CustID>19730300000006</CustID>");
            Attribute.Append("<RoomID>19730300000015</RoomID>");

            //string data = "{\"Type\":2,\"Data\":{\"CostID\":\"19730300000008\",\"Amt\":240.2}}";
            string data = "{\"Type\":1,\"Data\":[{\"FeesId\":\"197303000000044150\"},{\"FeesId\":\"197303000000045729\"}]}";
            Attribute.Append("<PayData>" + data + "</PayData>");
            Attribute.Append("<PayType>0</PayType>");

            Attribute.Append("</attributes>");
            string strAttribute = Attribute.ToString();
            string Mac = CreMAC(strAttribute, strDate, strToken);

            string strContent = "Class=JDPay&Command=GenerateOrder&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
            string strNewUrl = strDoUrl + "?" + strContent;
            string Res = SendHttp(strNewUrl, "");
        }

        private void button155_Click(object sender, EventArgs e)
        {
            string strDoUrl = "http://localhost:63305/Service.ashx";
            string strToken = "20200110Client";
            string strDate = DateTime.Now.ToString("yyyyMMdd");

            StringBuilder Attribute = new StringBuilder("");
            Attribute.Append("<attributes>");
            string data = "mobile_model:iPhone6,2;OS_version:iOS12.4.4;bt_info:蓝牙信息;account:18301492854;timestamp:1580863036;app_version:1.1.8;room_info:19730200002957;platform:iPhone;";
            Attribute.Append("<RecordData>" + data + "</RecordData>");

            Attribute.Append("</attributes>");
            string strAttribute = Attribute.ToString();
            string Mac = CreMAC(strAttribute, strDate, strToken);

            string strContent = "Class=RecordClientInfo&Command=Record&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
            string strNewUrl = strDoUrl + "?" + strContent;
            string Res = SendHttp(strNewUrl, "");
            Result.Text = Res;
        }

        private void button156_Click(object sender, EventArgs e)
        {
            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20170123IncidentAcceptManage";
                string Attribute = "<attributes><Net>1</Net><Account>1973-liulei01</Account><LoginPwd>123456</LoginPwd><CommID>197303</CommID><DealMan>hk</DealMan><UserCodes>000002</UserCodes><IncidentID>197303000000005173</IncidentID><TypeID>,1973030000000062,</TypeID><DealLimit>240</DealLimit><DispType>1</DispType><IncidentContent>测试分配</IncidentContent><IsSnatch>0</IsSnatch><DutySupplierID></DutySupplierID></attributes>";
                string Mac = CreMAC(Attribute, strDate, strToken);
                string strContent = "Class=IncidentAcceptManage&Command=IncidentAssignment&Attribute=" + Attribute + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                DesUrl.Text = strNewUrl;
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            Result.Text = Result.Text + "返回：" + repsTxt;
        }

        private void btnRedWY_Click(object sender, EventArgs e)
        {
            string strDoUrl = "http://wuyth-test.hongkun.com.cn:16666/TWInterface/Service/Service.ashx";//"http://localhost:63305/Service.ashx";
            string strToken = "20160803Register";
            string strDate = DateTime.Now.ToString("yyyyMMdd");

            StringBuilder Attribute = new StringBuilder("");
            Attribute.Append("<attributes>");
            Attribute.Append("<CommunityId>E4918A7C-DBE3-4D19-9117-FC7CEAC983EE</CommunityId>");

            Attribute.Append("</attributes>");
            string strAttribute = Attribute.ToString();
            string Mac = CreMAC(strAttribute, strDate, strToken);

            string strContent = "Class=Register&Command=CheckRedWuYeInfo&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
            string strNewUrl = strDoUrl + "?" + strContent;
            string Res = SendHttp(strNewUrl, "");

            DesUrl.Text = Res;
        }

        private void btnGetRedWYList_Click(object sender, EventArgs e)
        {
            TbURL.Text = "http://wuyth-test.hongkun.com.cn:16666/TWInterface/Service/Service.ashx";//"http://localhost:63305/Service.ashx";

            Result.Text = "";
            string repsTxt = "";
            try
            {
                string strDoUrl = TbURL.Text;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20171028CommunityManager";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>1</Net>");
                Attribute.Append("<CommunityId>E4918A7C-DBE3-4D19-9117-FC7CEAC983EE</CommunityId>");
                Attribute.Append("<RedType></RedType>");
                Attribute.Append("<PageIndex>1</PageIndex>");
                Attribute.Append("<PageSize>10</PageSize>");

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac = CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CommunityManager_th&Command=GetCommunityRedWYList&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;               
                repsTxt = SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            DesUrl.Text = repsTxt;
        }
    }
}
public class cParaInfo
{
    public string op { get; set; }
    public string jsonString { get; set; }
}

public class ParamText
{
    public string esealId { get; set; }
    public string pdfBase64 { get; set; }
    public string op { get; set; }
    public string xIncrement { get; set; }
    public string yIncrement { get; set; }
    public string text { get; set; }
}

public class ResultMsg
{
    //code int	200: 正常 ; 500: 异常
    //msg string 中文描述
    public int code { get; set; }
    public string msg { get; set; }
}

public static class OssManager
{
    /// <summary>
    /// 烏托邦:
    /// 换个bucket:  bgy-erp-tianwen
    ///
    /// 烏托邦:
    /// EndPoint（地域节点）Bucket 域名HTTPS
    ///外网访问 oss-cn-shenzhen.aliyuncs.com bgy-erp-tianwen.oss-cn-shenzhen.aliyuncs.com
    /// </summary>
    private static string _accessId = "LTAI9W6ocfmcSGQy";
    private static string _accessKey = "uP9gT6L7f154Zn2E6mHeg6wR8UV1fd";

    public static string BucketName
    {
        //set
        //{
        //    HttpContext.Current.Application["bucketName"] = value;
        //}
        get
        {
            //if (HttpContext.Current.Application["bucketName"] == null)
            //{
            return "bgy-erp-tianwen";
            //}
            //return Convert.ToString(HttpContext.Current.Application["bucketName"]);
        }
    }

    //private static string _accessId = "1f1MW7xXB0mHi2RC";
    //private static string _accessKey = "KzuDnnKsHSKBfqQiZLs0RtDKLzgLQM";
    private static string _http = "http://oss-cn-shenzhen.aliyuncs.com";

    private static OssClient ossClient;

    public static OssClient GetInstance()
    {
        if (ossClient == null)
        {
            ossClient = new OssClient(_http, _accessId, _accessKey);
        }
        return ossClient;
    }
}
