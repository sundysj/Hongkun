using System;
using MobileSoft.DBUtility;
using MobileSoft.Common;
using System.Data;
using System.Text;
using System.Xml;
using MobileSoft.BLL.Common;
using MobileSoft.Model.Common;
using MobileSoft.Model.Unified;

namespace Business
{
    /// <summary>
    /// 成飞短信接口，未纳入接口文档内
    /// </summary>
    public class Sms : PubInfo
    {
        //string SmsAccount = "AD00190";
        //string SmsPassword = "00190";
        public Sms() //获取小区、项目信息
        {
            base.Token = "20160803Sms";
        }
        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = "false:";

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "Send":
                    Trans.Result = Send(Row["Mobile"].ToString(), Row["Content"].ToString(), Trans.Mac);
                    break;
                default:
                    break;
            }
        }

        public string Send(string Mobile, string Content, string MacCode)
        {

            //MAC验证
            //DataSet ds = new Bll_Tb_SendMessageRecord().GetList(" MacCode='" + MacCode + "'  ");
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    return JSONHelper.FromString(false, "无效操作");
            //}

            Tb_SendMessageRecord m = new Tb_SendMessageRecord();
            try
            {
                //记录短信
                m = new Bll_Tb_SendMessageRecord().Add(Mobile, Content, MacCode, "成飞验证码", "");
            }
            catch (Exception ex)
            {
                return JSONHelper.FromString(false, "无效操作");
            }
            Tb_Sms_Account smsModel = SmsInfo.GetSms_Account();

            //int Result = Common.Sms.Send(smsModel.SmsAccount, smsModel.SmsPwd, Mobile, Content + ""+smsModel.Sign, "", "");

            int Result = Common.Sms.Send_v2(smsModel.SmsUserId, smsModel.SmsAccount, smsModel.SmsPwd, Mobile, Content + "" + smsModel.Sign, out string strErrMsg);
            string Resul = "";
            switch (Result)
            {
                case 0:
                    Resul = "发送成功";
                    break;
                case -4:
                    Resul = "手机号码格式不正确";
                    break;
                default:
                    Resul = "发送失败：" + Result;
                    break;
            }

            //修改状态
            m.SendState = Result.ToString();
            //重写短信记录状态
            new Bll_Tb_SendMessageRecord().Update(m);
            if (Result == 0)
            {
                return JSONHelper.FromString(true, Resul);
            }
            else
            {
                return JSONHelper.FromString(false, Resul);
            }

        }

    }
}