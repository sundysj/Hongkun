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

namespace Test
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string URLText = "http://125.64.16.10:9998/Service.ashx";          


            string strTradeDate = DateTime.Now.ToString("yyyy-MM-dd");
            
            string repsTxt = "";
            try
            {
                string strDoUrl = URLText;
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string strToken = "20170707CarLock_ZD";

                StringBuilder Attribute = new StringBuilder("");

                Attribute.Append("<attributes>");
                //Attribute.Append("<Net>1</Net>");
                //Attribute.Append("<Empty></Empty>");
                //Attribute.Append("<PageIndex>1</PageIndex>");
                //Attribute.Append("<PageSize>10</PageSize>");
                //Attribute.Append("<CorpID>1000</CorpID>");

                //Attribute.Append("<CommunityId>1000_100013</CommunityId>");
                //Attribute.Append("<CommodityType>101</CommodityType>");
                //Attribute.Append("<DataSort></DataSort>");
                Attribute.Append("<CarNo>贵-H123456</CarNo>");
                Attribute.Append("<lockFlag>0</lockFlag>");                

                Attribute.Append("</attributes>");
                string strAttribute = Attribute.ToString();

                string Mac =new Form1().CreMAC(strAttribute, strDate, strToken);
                string strContent = "Class=CarLock_ZD&Command=LockCar&Attribute=" + System.Net.WebUtility.UrlEncode(strAttribute) + "&Mac=" + Mac;
                string strNewUrl = strDoUrl + "?" + strContent;
                label2.Text = strNewUrl;
                textBox1.Text = strNewUrl;
                repsTxt = new Form1().SendHttp(strNewUrl, "");
            }
            catch (Exception ex)
            {
                repsTxt = ex.Message.ToString();
            }
            label1.Text = "返回：" + repsTxt;


        }      
    }
}
