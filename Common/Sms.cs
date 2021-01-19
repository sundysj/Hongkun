using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

namespace Common
{

    //http://www.ht3g.com/htWS/Send.aspx?CorpID=AD00190&Pwd=00190&Mobile=18980508320&Content=测试【天问互联】&Cell=&SendTime=
    public static class Sms
    {
        public static int Send(string CorpID,
                       string Pwd,
                       string Mobile,
                       string Content,
                       string Cell,
                       string SendTime)
        {
            int strReturn = 0;

            try
            {
                Common.ht3g.com.LinkWS xxMsg = new Common.ht3g.com.LinkWS();
                int strMsg = xxMsg.Send(CorpID, Pwd, Mobile, Content, Cell, SendTime);
                strReturn = strMsg;
            }
            catch
            {
                strReturn = -2;
            }

            return strReturn;
        }

        public static int Send_v2(int userid,
                       string account,
                       string password,
                       string mobile,
                       string content, out string strErrMsg)
        {
            strErrMsg = "";
            //设置必要参数
            var url = "http://sms.ht3g.com/sms.aspx";

            //设置HttpClientHandler的AutomaticDecompression
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };

            //创建HttpClient（注意传入HttpClientHandler）
            using (var http = new HttpClient(handler))
            {
                //使用FormUrlEncodedContent做HttpContent
                var data = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    {"userid", userid.ToString()},//企业ID
                    {"account", account},//用户帐号，由系统管理员
                    {"password", password},//用户账号对应的密码
                    {"mobile", mobile},//发信发送的目的号码.多个号码之间用半角逗号隔开
                    {"content", content},//短信的内容，内容需要UTF-8编码
                    {"sendTime", ""},//为空表示立即发送，定时发送格式2010-10-24 09:08:10
                    {"action", "send"},//设置为固定的:send
                    {"extno", ""}//请先询问配置的通道是否支持扩展子号，如果不支持，请填空。子号只能为数字，且最多10位数。
                });

                //await异步等待回应
                HttpResponseMessage response = http.PostAsync(url, data).Result;

                string Results = response.Content.ReadAsStringAsync().Result;

                // await异步读取最后的JSON（注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression = DecompressionMethods.GZip）
                // Console.WriteLine(await response.Content.ReadAsStringAsync());

                if (Results != "")
                {
                    System.Xml.XmlDocument xmlReturn = new System.Xml.XmlDocument();
                    try
                    {
                        #region returnsms
                        string returnstatus = "";
                        string message = "";
                        string remainpoint = "";
                        string taskID = "";
                        string successCounts = "";

                        xmlReturn.LoadXml(Results);

                        if (xmlReturn.ChildNodes[1].SelectSingleNode("returnstatus") != null)
                        {
                            returnstatus = xmlReturn.ChildNodes[1].SelectSingleNode("returnstatus").InnerXml;
                        }
                        if (xmlReturn.ChildNodes[1].SelectSingleNode("message") != null)
                        {
                            message = xmlReturn.ChildNodes[1].SelectSingleNode("message").InnerXml;
                        }
                        if (xmlReturn.ChildNodes[1].SelectSingleNode("remainpoint") != null)
                        {
                            remainpoint = xmlReturn.ChildNodes[1].SelectSingleNode("remainpoint").InnerXml;
                        }
                        if (xmlReturn.ChildNodes[1].SelectSingleNode("taskID") != null)
                        {
                            taskID = xmlReturn.ChildNodes[1].SelectSingleNode("taskID").InnerXml;
                        }
                        if (xmlReturn.ChildNodes[1].SelectSingleNode("successCounts") != null)
                        {
                            successCounts = xmlReturn.ChildNodes[1].SelectSingleNode("successCounts").InnerXml;
                        }
                        #endregion

                        if (returnstatus == "Success")
                        {
                            strErrMsg = "成功";
                            return 0;
                        }
                        if (returnstatus == "Faild")
                        {
                            strErrMsg = "失败" + Results;
                            return -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        strErrMsg = ex.Message + ex.StackTrace;
                        return -1;
                    }
                }

                return 0;
            }
        }
    }
}
