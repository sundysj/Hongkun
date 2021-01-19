using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MobileSoft.DBUtility;
using MobileSoft.Common;
using MobileSoft.BLL.Sys;
using System.Data;
using MobileSoft;
using System.Web.Configuration;

namespace Business
{
    public class getCommunity : PubInfo
    {
        public getCommunity()
        {
            base.Token = "20160323GETCOMMOMID";
        }
        private void Logining(string NetType, string Account)
        {

        }
        public override void Operate(ref Common.Transfer Trans)
        {
            DataSet ds = new DataSet();
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            string NetType = Row["Net"].ToString();
            string Account = Row["Account"].ToString();
            string LoginPwd = Row["LoginPwd"].ToString();

            Global_Var.SystemType = "property";
            //获得所在服务器的tw2_bs数据库连接字符串
            if (NetType == "1")
            {
                PubConstant.tw2bsConnectionString = Global_Fun.GetConnectionString("10Connection");
                Global_Var.CommImageUrl = WebConfigurationManager.AppSettings["10ImageUrl"].ToString();
                Global_Var.ServerIp = WebConfigurationManager.AppSettings["10IP"].ToString();
            }
            if (NetType == "2")
            {
                PubConstant.tw2bsConnectionString = Global_Fun.GetConnectionString("14Connection");
                //Global_Var.CommImageUrl = WebConfigurationManager.AppSettings["14ImageUrl"].ToString();
                //Global_Var.ServerIp = WebConfigurationManager.AppSettings["14IP"].ToString();
            }
            if (NetType == "3")
            {
                PubConstant.tw2bsConnectionString = Global_Fun.GetConnectionString("37Connection");
                Global_Var.CommImageUrl = WebConfigurationManager.AppSettings["37ImageUrl"].ToString();
                Global_Var.ServerIp = WebConfigurationManager.AppSettings["37IP"].ToString();
            }

            string[] arrUser = Account.Split('-');

            if (arrUser.Length > 1)
            {
                Global_Var.CorpId = arrUser[0].ToString();
                Global_Var.LoginCode = arrUser[1].ToString();
            }
            else
            {
                Trans.Result = JSONHelper.FromString(false,"身份认证失败,请重新登录!"); 
                return;
            }
            AppGlobal.GetHmWyglConnection();

            Bll_Tb_Sys_User Bll = new Bll_Tb_Sys_User();
            DataTable dTable = Bll.GetList("LoginCode='" + Global_Var.LoginCode.ToString() + "' AND PassWord='" + LoginPwd + "' AND ISNULL(IsDelete,0)=0").Tables[0];

            if (dTable.Rows.Count > 0)
            {
                DataRow DRow = dTable.Rows[0];

                if (DRow["IsMobile"].ToString() != "1")
                {
                    Trans.Result = JSONHelper.FromString(false,"身份认证失败,请重新登录!");
                    return;
                }
                else
                {

                    //存在此用户,登陆成功
                    Global_Var.UserCode = DRow["UserCode"].ToString();
                    Global_Var.LoginUserCode = DRow["UserCode"].ToString();
                    Global_Var.UserName = DRow["UserName"].ToString();
                    Global_Var.LoginUserName = DRow["UserName"].ToString();
                    Global_Var.LoginDepCode = DRow["DepCode"].ToString();
                    Global_Var.LoginMobile = DRow["MobileTel"].ToString();

                    string strSQLComm = " IsDelete = 0 and CorpID = " + Global_Var.CorpId.ToString();
                    MobileSoft.BLL.HSPR.Bll_Tb_HSPR_Community B = new MobileSoft.BLL.HSPR.Bll_Tb_HSPR_Community();
                    DataTableCollection dTablecommCollection = B.GetList(strSQLComm).Tables;
                    for (int i = 0; i < dTablecommCollection.Count; i++)
                    {
                        DataTable dtTable = dTablecommCollection[i].Copy();
                        dtTable.TableName = "data";
                        ds.Tables.Add(dtTable);
                    }
                    Trans.Result = JSONHelper.FromString(ds,false,true);
                    ds.Dispose();
                }
            }
            else
            {
                Trans.Result = JSONHelper.FromString(false,"身份认证失败,请重新登录!"); 
                return;
            }
        }
    }
}