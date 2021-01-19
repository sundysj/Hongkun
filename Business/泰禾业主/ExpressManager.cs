using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Business
{
    public class ExpressManager : PubInfo
    {
        public ExpressManager()
        {
            base.Token = "20171130ExpressManager";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "GetExpressList":             // 获取快递列表
                    Trans.Result = GetExpressList(Row);
                    break;
                case "ReceiveExpress":
                    Trans.Result = ReceiveExpress(Row);
                    break;
                default:
                    break;
            }
        }

        // 获取快递列表
        public string GetExpressList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityID") || string.IsNullOrEmpty(row["CommunityID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编号不能为空");
            }

            int PageIndex = 1;
            int PageSize = 10;
            string CommunityID = row["CommunityID"].ToString();
            string UserID = row["UserID"].ToString();

            if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommunityID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            int pageCount;
            int Counts;
            

            // 获取该用户绑定的房号
            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                string sql = @"SELECT RoomId FROM Tb_User_Relation WHERE isnull(Locked,0)=0 AND UserId=@UserId AND CommunityId=@CommunityId";

                IEnumerable<string> roomResultSet = conn.Query<string>(sql, new { UserId = UserID, CommunityId = CommunityID });

                if (roomResultSet.Count() > 0)
                {
                    //构建链接字符串
                    this.GetConnectionString(Community);

                    sql = string.Format(@"SELECT EMSID,MailCompany,PmGetDate,CustIsGet,GetDate,EMSNumber FROM VIEW_HSPR_Express_Filter 
                                            WHERE CommID={0} AND RoomID IN({1})", Community.CommID, string.Join(",", roomResultSet));

                    DataSet ds = this.GetList(out pageCount, out Counts, sql, PageIndex, PageSize, "PmGetDate", 1, "EMSID", Global_Var.LoginSQLConnStr);

                    return JSONHelper.FromString(ds.Tables[0]);
                }

                return JSONHelper.FromString(false, "还没有绑定任何房屋");
            }
        }

        // 领取快递
        public string ReceiveExpress(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityID") || string.IsNullOrEmpty(row["CommunityID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            if (!row.Table.Columns.Contains("EMSID") || string.IsNullOrEmpty(row["EMSID"].ToString()))
            {
                return JSONHelper.FromString(false, "快递记录编号不能为空");
            }
            if (!row.Table.Columns.Contains("Mobile") || string.IsNullOrEmpty(row["Mobile"].ToString()))
            {
                return JSONHelper.FromString(false, "领取人不能为空");
            }


            string CommunityID = row["CommunityID"].ToString();
            string EMSID = row["EMSID"].ToString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommunityID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            //构建链接字符串
            this.GetConnectionString(Community);

            // 获取该用户绑定的房号
            using (IDbConnection conn = new SqlConnection(Global_Var.LoginSQLConnStr))
            {
                conn.Execute("UPDATE Tb_HSPR_Express SET CustIsGet=1,GetPerson=@Mobile,GetDate=getdate() WHERE EMSId=@EMSId", 
                    new { EMSId = EMSID, Mobile = row["Mobile"].ToString() });
                return JSONHelper.FromString(true, "签收成功");
            }
        }
    }
}
