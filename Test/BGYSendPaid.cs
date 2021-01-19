
using Common;

using MobileSoft.DBUtility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class BGYSendPaid : Form
    {
        public BGYSendPaid()
        {
            InitializeComponent();
        }


        int s = 0;

        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            int a = 4, b = 3, c = 5;
            f(a, b);f(a, c);f(b, c);


            rtxbSurr.AppendText("a:"+a+",b:"+b+",c:"+c);


            //while (s==0)
            //{
            //        try
            //        {
            //        string InterfaceURL = txbURL.Text.Trim();
            //        string userName = txbUsername.Text.Trim();
            //        string userPass = txbpwd.Text.Trim();
            //            DbHelperSQLP help = new DbHelperSQLP(txbcon.Text);
            //            DataSet ds = help.Query("select i.OrderID, f.FeesChargeDate, ISNULL(PaidAmount,0) as PaidAmount from Tb_HSPR_NewIncidentInfo as i left join Tb_HSPR_Fees as f " +
            //                "on i.FeesID = f.FeesID where i.FeesID is not null and f.IsCharge = 1 and i.IsSendPaid = 0 ");

            //            if (ds.Tables.Count > 0)
            //            {
            //                foreach (DataRow row in ds.Tables[0].Rows)
            //                {
            //                    Dictionary<string, string> postData = new Dictionary<string, string>();
            //                    postData.Add("userName", userName);
            //                    postData.Add("userPass", userPass);
            //                    postData.Add("orderId", row["OrderID"].ToString());
            //                    postData.Add("chargeState", "1");
            //                    postData.Add("chargeTime", Convert.ToDateTime(row["FeesChargeDate"].ToString()).ToString("yyyy-MM-dd hh:mm:ss"));
            //                    postData.Add("chargeType", "1");
            //                    postData.Add("erpTotalPrice", row["PaidAmount"].ToString());
            //                    try
            //                    {
            //                        string result = HttpUtil.CreatePostHttpResponse(InterfaceURL, postData, Encoding.UTF8);

            //                        JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            //                        if (jo["result"].ToString() == "0")//实收发送成功
            //                        {
            //                            //更新ERP数据

            //                            int count = help.ExecuteSql("update Tb_HSPR_NewIncidentInfo set IsSendPaid=1 where OrderID='" + row["OrderID"] + "'");

            //                            rtxbSurr.AppendText("一碑实收推送:"+DateTime.Now.ToString() + " 一碑订单号：" + row["OrderID"].ToString() + "推送成功");

            //                        }
            //                        else
            //                        {
            //                            rtxbSurr.AppendText("一碑实收推送:"+DateTime.Now.ToString() + " 一碑订单号：" + row["OrderID"].ToString() + "推送失败:" + jo["result"].ToString() + "参数为" +
            //                               string.Join(",", postData.Values));

            //                        }
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        rtxbSurr.AppendText(ex.Message);
            //                    }
            //                }

            //            }


            //        }
            //        catch (Exception ex)
            //        {
            //            rtxbSurr.AppendText(ex.Message);
            //            //new Logger().WriteLog("一碑实收推送", DateTime.Now.ToString() + " " + e.Message);

            //        }                
            //    System.Threading.Thread.Sleep(30000);
            //}

        }

        /// <summary>
        /// 结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            s = 1;
        }



        void f(int x, int y)
        {
            int t;
            if (x<y)
            {
                t = x; x = y;y = t;
            }
        }

    }
}
