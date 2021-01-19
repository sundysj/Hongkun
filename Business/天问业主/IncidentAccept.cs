using Common;
using Dapper;
using MobileSoft.BLL.SQMSys;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.HSPR;
using MobileSoft.Model.SQMSys;
using MobileSoft.Model.Unified;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TWTools.Push;

namespace Business
{
    /// <summary>
    /// 报事
    /// </summary>
    public class IncidentAccept : PubInfo
    {
        public IncidentAccept()
        {
            base.Token = "20160914IncidentAccept";
        }

        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
              

                case "GetIncidentBigType":
                    Trans.Result = GetIncidentBigType(Row);//报事大类
                    break;
                case "GetIncidentSmallType":
                    Trans.Result = GetIncidentSmallType(Row);//报事小类
                    break;
                case "SetIncidentAcceptPhoneInsertRegion":
                    Trans.Result = SetIncidentAcceptPhoneInsertRegion(Row);//公区报事
                    break;
                case "SetIncidentAcceptPhoneInsertRegionFor118":
                    Trans.Result = SetIncidentAcceptPhoneInsertRegionFor118(Row);//联创公区报事
                    break;
                case "SetIncidentAcceptPhoneInsert"://户内报事
                    Trans.Result = SetIncidentAcceptPhoneInsert(Row);
                    break;
                case "SetIncidentAcceptPhoneInsertFor118":
                    Trans.Result = SetIncidentAcceptPhoneInsertFor118(Row);//联创户内报事
                    break;
                case "GetIncidentInfoListRegion"://公区报事历史
                    Trans.Result = GetIncidentInfoListRegion(Row);
                    break;
                case "GetIncidentInfoListRegional"://公区报事区域
                    Trans.Result = GetIncidentInfoListRegional(Row);
                    break;
                case "GetIncidentLocation"://公区报事位置
                    Trans.Result = GetIncidentLocation(Row);
                    break;
                case "GetIncidentObject"://公区报事对像
                    Trans.Result = GetIncidentObject(Row);
                    break;
                case "GetIncidentInfoList"://户内报事历史
                    Trans.Result = GetIncidentInfoList(Row);
                    break;
                case "GetIncidentInfoList_JH"://户内报事历史
                    Trans.Result = GetIncidentInfoList_JH(Row);
                    break;
                case "SubmitProposal":
                    Trans.Result = SubmitProposal(Row);//提交报事评价
                    break;
                case "NewIncidentSeachList":
                    Trans.Result = NewIncidentSeachList(Row);//北方报事接口
                    break;
                case "IncidentAcceptReminders"://报事催办
                    Trans.Result = IncidentAcceptReminders(Row);
                    break;
                case "SetIncidentAcceptPhoneInsertRegion_ZL"://中粮公区报事（提供给中粮呼叫中心）
                    Trans.Result = SetIncidentAcceptPhoneInsertRegion_ZL(Row);//公区报事
                    break;
                case "GetCOmmunity"://中粮公区报事，根据城市名称查询项目。
                    Trans.Result = GetCOmmunity(Row);
                    break;
                case "MLSIncidentTelComming"://美利山呼叫中心来电查询
                    Trans.Result = MLSIncidentTelComming(Row);
                    break;
                case "MLSIncidentAcceptOral"://美利山呼叫中心口派报事
                    Trans.Result = MLSIncidentAcceptOral(Row);
                    break;
                case "IsCanSubmitIncident":
                    Trans.Result = IsCanSubmitIncident(Row);
                    break;
                case "GetIncidentInfoListFromUpdateTime":
                    Trans.Result = GetIncidentInfoListFromUpdateTime(Row);
                    break;

                default:
                    break;
            }
        }

        #region 报事催办
        private string IncidentAcceptReminders(DataRow row)
        {

            string IncidentId = row["IncidentID"].ToString();
            string CommID = row["CommID"].ToString();
            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构建链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string connStr = new CostInfo().GetConnectionStringStr(Community);
            using (IDbConnection con = new SqlConnection(connStr))
            {
                string sql = "select count(*) from Tb_HSPR_IncidentRemindersInfo where IsDelete=0 and  IncidentID ='" + IncidentId + "'and convert(varchar(10), getdate(), 23)=convert(varchar(10), RemindersDate, 23)";
                int count = con.Execute(sql);
                if (count > 0)
                {
                    return JSONHelper.FromString(false, "今天已经催办过了");
                }
            }
            using (IDbConnection con = new SqlConnection(connStr))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("InfoID", 0);
                parameters.Add("IncidentID", IncidentId);
                parameters.Add("CommID", Community.CommID);
                parameters.Add("RemindersType", "");
                parameters.Add("RemindersDate", DateTime.Now);
                parameters.Add("UserID", "");
                parameters.Add("UserName", "");
                parameters.Add("InfoContent", "业主APP催办");
                parameters.Add("Remark", "业主APP催办");
                parameters.Add("IsSendMessage", false);
                parameters.Add("SendMessage", "");
                parameters.Add("IsDelete", 0);
                int count = con.Execute("Proc_HSPR_IncidentRemindersInfo_Insert", parameters, null, null, CommandType.StoredProcedure);
                if (count <= 0)
                {
                    return new ApiResult(false, "催办失败,请重试").toJson();
                }
            }
            return JSONHelper.FromString(true, "催办成功");
        }
        #endregion



        //公区报事对像
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// CommID      小区编号【必填】
        /// LocationID   位置编号【必填】
        /// 返回：
        ///     ObjectID   对像编号
        ///     ObjectName   对像名称
        /// <returns></returns>
        private string GetIncidentObject(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("LocationID") || string.IsNullOrEmpty(row["LocationID"].ToString()))
            {
                return JSONHelper.FromString(false, "位置编号不能为空");
            }

            string CommID = row["CommID"].ToString();
            string LocationID = row["LocationID"].ToString();
            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string ConStt = new CostInfo().GetConnectionStringStr(Community);
            IDbConnection con = new SqlConnection(ConStt);
            string str = "select ObjectID,ObjectName from (SELECT E.*, B.CommID, C.IsDelete AS RegionalIsDelete, C.RegionalName FROM view_HSPR_IncidentObject_Filter AS E  LEFT JOIN Tb_HSPR_IncidentObject AS B ON E.ObjectID = B.ObjectID  LEFT JOIN dbo.Tb_HSPR_IncidentRegional AS C ON E.RegionalID = C.RegionalID) AS D where Isnull(ObjectIsDelete,0) = 0 and Isnull(RegionalIsDelete,0) = 0 and Isnull(LocationIsDelete,0) = 0 And LocationID = '" + LocationID + "' AND CommID = " + CommID;
            DataSet ds = con.ExecuteReader(str, null, null, null, CommandType.Text).ToDataSet();
            return JSONHelper.FromDataTable(ds.Tables[0]);

        }

        //公区报事位置
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// CommID      小区编号【必填】
        /// RegionalID   区域编号【必填】
        /// 返回：
        ///     LocationID   位置编号
        ///     LocationName   位置名称
        /// <returns></returns>
        private string GetIncidentLocation(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("RegionalID") || string.IsNullOrEmpty(row["RegionalID"].ToString()))
            {
                return JSONHelper.FromString(false, "区域编号不能为空");
            }

            string CommID = row["CommID"].ToString();
            string RegionalID = row["RegionalID"].ToString();
            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string ConStt = new CostInfo().GetConnectionStringStr(Community);
            IDbConnection con = new SqlConnection(ConStt);
            string sqlStr = " select LocationID,LocationName from  Tb_HSPR_IncidentLocation where CommID=@CommID and RegionalID=@RegionalID and isnull(IsDelete,0)=0";
            List<Tb_HSPR_IncidentLocation> list = con.Query<Tb_HSPR_IncidentLocation>(sqlStr, new { CommID = Community.CommID, RegionalID = RegionalID }).ToList<Tb_HSPR_IncidentLocation>();
            return JSONHelper.FromString<Tb_HSPR_IncidentLocation>(list);
        }

        /// <summary>
        /// 获取公区报事区域
        /// </summary>
        /// <param name="row"></param>
        /// CommID      小区编号【必填】
        /// 返回：
        ///     RegionalID    区域编号
        ///     RegionalPlace  区域名称
        /// <returns></returns>
        private string GetIncidentInfoListRegional(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            string CommID = row["CommID"].ToString();
            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string ConStt = new CostInfo().GetConnectionStringStr(Community);
            IDbConnection con = new SqlConnection(ConStt);
            string sqlStr = " select RegionalID,RegionalPlace from  Tb_HSPR_IncidentRegional where isnull(IsDelete,0)=0 AND CommID=@CommID";
            List<Tb_HSPR_IncidentRegional> list = con.Query<Tb_HSPR_IncidentRegional>(sqlStr, new { CommID = Community.CommID }).ToList<Tb_HSPR_IncidentRegional>();
            return JSONHelper.FromString<Tb_HSPR_IncidentRegional>(list);
        }

        //提交报事评价
        /// <summary>
        /// SubmitProposal
        /// </summary>
        /// <param name="row"></param>
        /// CommID 小区编号 1000-100013必填
        /// IncidentID 报事编号 必填
        /// ServiceQuality 报事评价【非常满意、满意、一般、不满意、很不满意】 【默认非常满意】
        /// CustComments  评价内容
        /// <returns></returns>
        private string SubmitProposal(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编号不能为空");
            }
            if (!row.Table.Columns.Contains("ServiceQuality") || string.IsNullOrEmpty(row["ServiceQuality"].ToString()))
            {
                return JSONHelper.FromString(false, "服务质量不能为空");
            }

            string CommID = row["CommID"].ToString();
            string IncidentID = row["IncidentID"].ToString();
            string ServiceQuality = "非常满意";
            string CustComments = "";
            if (row.Table.Columns.Contains("ServiceQuality") && row["ServiceQuality"].ToString() != "")
            {
                ServiceQuality = row["ServiceQuality"].ToString();
            }
            if (row.Table.Columns.Contains("CustComments") && row["CustComments"].ToString() != "")
            {
                CustComments = row["CustComments"].ToString();
            }

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            string ConStt = new CostInfo().GetConnectionStringStr(Community);

            IDbConnection con = new SqlConnection(ConStt);
            string sqlStr = " select * from  Tb_HSPR_IncidentAccept where IncidentID=@IncidentID";
            Tb_HSPR_IncidentAccept list = con.Query<Tb_HSPR_IncidentAccept>(sqlStr, new { IncidentID = IncidentID }).SingleOrDefault<Tb_HSPR_IncidentAccept>();

            if (list == null)
            {
                return JSONHelper.FromString(false, "不存在此报事信息");
            }

            // 回访方式、回访类型
            string replyWay = "客户线上评价";


            // 业主自己从业主App评价，受访人默认为客户本人、没有回访人，此条件可以用于ERP端判断用
            // 金辉版本
            if (Community.CorpID == 1985)
            {
                con.Execute(string.Format(@"INSERT INTO Tb_HSPR_IncidentReply(CommID,IncidentID,ReplyType,
                                            ReplyDate,ReplyContent,ServiceQuality,ReplyWay,ReplyResult,IsDelete) 
                                            values({0},{1},'App业主自评',getdate(),'{2}','{3}','{4}',1, 0)",
                                            list.CommID, list.IncidentID, CustComments, ServiceQuality, replyWay));
            }
            // 海亮
            else if (Community.CorpID == 2021)
            {
                string replyResult = "1";
                if (ServiceQuality != "非常满意" && ServiceQuality != "满意")
                {
                    replyResult = "0";
                    replyWay = "继续回访";
                }

                con.Execute(string.Format(@"INSERT INTO Tb_HSPR_IncidentReply(CommID,IncidentID,ReplyType,
                            ReplyDate,ReplyContent,ServiceQuality,ReplyWay,ReplyResult,IsDelete) 
                            values({0},{1},'App业主自评',getdate(),'{2}','{3}','{4}',{5}, 0)",
                            list.CommID, list.IncidentID, CustComments, ServiceQuality, replyWay, replyResult));

                con.Execute(@"UPDATE Tb_HSPR_IncidentAccept SET ServiceQuality=@ServiceQuality,CustComments=@CustComments 
                              WHERE IncidentID=@IncidentID", new
                {
                    ServiceQuality = ServiceQuality,
                    CustComments = CustComments,
                    IncidentID = IncidentID
                });
            }
            else
            {
                con.Execute(string.Format(@"INSERT INTO Tb_HSPR_IncidentReply(CommID,IncidentID,ReplyType,
                                            ReplyDate,ReplyContent,ServiceQuality,ReplyWay,ReplyResult,IsDelete) 
                                            values({0},{1},'App业主自评',getdate(),'{2}','{3}','{4}',0, 0)",
                                            list.CommID, list.IncidentID, CustComments, ServiceQuality, replyWay));

                var sql = $"SELECT count(1) FROM syscolumns WHERE id=object_id('Tb_HSPR_IncidentReply') AND name='RespondentsMans'";
                if (con.Query<int>(sql).FirstOrDefault() > 0)
                {
                    con.Execute(@"UPDATE Tb_HSPR_IncidentReply SET RespondentsMan='客户本人' 
                                    WHERE IncidentID=@IncidentID AND ReplyType='App业主自评'",
                                    new { IncidentID = list.IncidentID });
                }
            }

            return JSONHelper.FromString(true, "报事评价成功!");
        }

        /// <summary>
        /// 公区报事历史查询
        /// </summary>
        /// <param name="row"></param>
        /// CommID 小区编号 1000-100013必填
        /// CustID 客户编号 必填
        /// Phone  联系方式  必填
        /// PageIndex  页码
        /// PageSize   条数
        /// 
        /// 返回信息：
        ///     IncidentID     报事编号
        ///     IncidentDate   报事日期
        ///     IncidentPlace  报事类型
        ///     IncidentContent 报事内容
        ///     State   报事状态
        ///     TypeName 报事项目
        ///     RegionalName 报事区域
        /// <returns></returns>
        private string GetIncidentInfoListRegion(DataRow row)
        {

            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            if (!row.Table.Columns.Contains("Phone") || string.IsNullOrEmpty(row["Phone"].ToString()))
            {
                return JSONHelper.FromString(false, "联系方式不能为空");
            }
            int PageIndex = 1;
            int PageSize = 5;
            string CommID = row["CommID"].ToString();
            string CustID = row["CustID"].ToString();
            string Phone = row["Phone"].ToString();
            if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            int pageCount;
            int Counts;
            //构建链接字符串
            new CostInfo().GetConnectionString(Community);

            string query = @"SELECT IncidentID,IncidentDate,'公区报事' as IncidentPlace,ISNULL(IncidentImgs,'') as IncidentImgs,DispTypeState,
                                IncidentContent,State,TypeName,RegionalName,ISNULL(IsDelete,0) as IsDelete ,
                                ISNULL(a.ServiceQuality, '') as ServiceQuality,
                                ISNULL(a.CustComments, '') as CustComments
                                FROM view_HSPR_IncidentSeach_Filter a WHERE ISNULL(IsDelete,0)=0 and  
                                CommID=" + Community.CommID + "  and IncidentPlace='公共区域' and  Phone='" + Phone + "' and " +
                                "IncidentMan =(select CustName from Tb_HSPR_Customer where CustID =" + CustID + ")";

            DataSet ds = new CostInfo().GetList(out pageCount, out Counts, query, PageIndex, PageSize, "IncidentDate", 1, "IncidentID");

            return JSONHelper.FromString(ds.Tables[0]);

        }


        /// <summary>
        /// 户内报事历史查询
        /// </summary>
        /// <param name="row"></param>
        /// CommID 小区编号 1000-100013必填
        /// CustID 客户编号 必填
        /// PageIndex  页码
        /// PageSize   条数
        /// 
        /// 返回信息：
        ///     IncidentID     报事编号
        ///     IncidentDate   报事日期
        ///     IncidentPlace  报事类型
        ///     IncidentContent 报事内容
        ///     State   报事状态
        ///     CommName  小区名称
        ///     BuildSNum  楼栋
        ///     RoomSign  房间号a
        ///     DueAmount  费用金额
        ///     ServiceQuality  满意度
        ///     CustComments   评价信息
        /// <returns></returns>
        private string GetIncidentInfoList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }

            int PageIndex = 1;
            int PageSize = 5;
            string CommID = row["CommID"].ToString();
            string CustID = row["CustID"].ToString();

            if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            int pageCount;
            int Counts;
            //构建链接字符串
            new CostInfo().GetConnectionString(Community);

            string query = @"select a.IncidentID,a.IncidentDate,'户内报事' as IncidentPlace,ISNULL(a.IncidentImgs,'') as IncidentImgs,
                              a.IncidentContent,a.DispTypeState,a.State,a.IncidentReply,a.CommName,ISNULL(a.BuildSNum, 0) as BuildSNum,
                              ISNULL(a.RoomSign, '') as RoomSign,ISNULL(a.DueAmount, 0) as DueAmount,ISNULL(a.ServiceQuality, '') as ServiceQuality,
                              ISNULL(a.CustComments, '') as CustComments,ISNULL(a.IsDelete, 0) as IsDelete,b.BuildName
                              from view_HSPR_IncidentSeach_Filter a 
                              left join Tb_HSPR_Building b 
                              on a.BuildSNum = b.BuildSNum AND a.CommID=b.CommID 
                              where ISNULL(a.IsDelete,0)=0 and a.IncidentPlace='业主权属' and a.CommID=" + Community.CommID + " and a.CustID=" + CustID;

            DataSet ds = new CostInfo().GetList(out pageCount, out Counts, query, PageIndex, PageSize, "IncidentDate", 1, "IncidentID");

            return JSONHelper.FromString(ds.Tables[0]);
        }

        private string GetIncidentInfoListFromUpdateTime(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            string CommID = row["CommID"].ToString();

            string UpdateStartTime = "";
            string UpdateEndedTime = "";
            if (row.Table.Columns.Contains("UpdateStartTime"))
            {
                UpdateStartTime = row["UpdateStartTime"].ToString();
            }
            if (row.Table.Columns.Contains("UpdateEndedTime"))
            {
                UpdateEndedTime = row["UpdateEndedTime"].ToString();
            }
            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            //构建链接字符串
            //new CostInfo().GetConnectionString(Community);

            string query = @"select a.IncidentID,a.IncidentDate,a.DispTypeState,a.State,a.IncidentReply,a.CommName,ISNULL(a.BuildSNum, 0) as BuildSNum,
                              ISNULL(a.RoomSign, '') as RoomSign,ISNULL(a.DueAmount, 0) as DueAmount,ISNULL(a.ServiceQuality, '') as ServiceQuality,
                              ISNULL(a.CustComments, '') as CustComments
                              from view_HSPR_IncidentSeach_Filter a 
                              left join Tb_HSPR_Building b 
                              on a.BuildSNum = b.BuildSNum AND a.CommID=b.CommID 
                              where ISNULL(a.IsDelete,0)=0 and a.CommID=" + Community.CommID + " and incidentDate >='" + UpdateStartTime + "' and incidentDate<='" + UpdateEndedTime + "'";

            string connStr = new CostInfo().GetConnectionStringStr(Community);
            using (IDbConnection con = new SqlConnection(connStr))
            {
                DataTable dt2 = con.ExecuteReader(query).ToDataSet().Tables[0];

                return JSONHelper.FromString(dt2);
            }
        }

        private string GetIncidentInfoList_JH(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }

            int PageIndex = 1;
            int PageSize = 5;
            string CommID = row["CommID"].ToString();
            string CustID = row["CustID"].ToString();

            if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            int pageCount;
            int Counts;
            //构建链接字符串
            new CostInfo().GetConnectionString(Community);

            string query = @"select a.IncidentID,a.IncidentDate,'户内报事' as IncidentPlace,ISNULL(a.IncidentImgs,'') as IncidentImgs,
                              a.IncidentContent,a.DispTypeState,a.State,a.IncidentReply,a.CommName,ISNULL(a.BuildSNum, 0) as BuildSNum,
                              ISNULL(a.RoomSign, '') as RoomSign,ISNULL(a.DueAmount, 0) as DueAmount,ISNULL(a.IncidentServiceQuality, '') as ServiceQuality,
                              ISNULL(a.CustComments, '') as CustComments,ISNULL(a.IsDelete, 0) as IsDelete,b.BuildName
                              from view_HSPR_IncidentSeach_Filter a left join Tb_HSPR_Building b 
                              on a.BuildSNum = b.BuildSNum AND a.CommID=b.CommID 
                              where ISNULL(a.IsDelete,0)=0 and a.IncidentPlace='业主权属' and a.CommID=" + Community.CommID + " and a.CustID=" + CustID;

            DataSet ds = new CostInfo().GetList(out pageCount, out Counts, query, PageIndex, PageSize, "IncidentDate", 1, "IncidentID");

            return JSONHelper.FromString(ds.Tables[0]);
        }

        /// <summary>
        /// 公区报事
        /// </summary>
        /// <param name="row"></param>
        /// CommID 小区编号【必填】
        /// CustID  客户编号 【必填】
        /// Content 报事内容【必填】
        /// RegionalID 报事区域ID【必填】
        /// TypeID    报事项目ID【必填】
        /// ObjectIDList  对像集【选填，格式：8888,88888,8888】
        /// Signatory    区域名称
        /// Phone  联系电话
        /// images 图片
        /// <returns></returns>
        private string SetIncidentAcceptPhoneInsertRegion(DataRow row)
        {

           
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            if (!row.Table.Columns.Contains("Content") || string.IsNullOrEmpty(row["Content"].ToString()))
            {
                return JSONHelper.FromString(false, "报事内容不能为空");
            }
            if (!row.Table.Columns.Contains("RegionalID") || string.IsNullOrEmpty(row["RegionalID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事区域不能为空");
            }

            DateTime dtNow = DateTime.Now;
            string strStart = Global_Fun.AppWebSettings("StartHour");
            DateTime startDate = Convert.ToDateTime(dtNow.ToShortDateString() + (string.IsNullOrEmpty(strStart)? " 14:00:00":strStart));
            int compNum = DateTime.Compare(dtNow, startDate);
            if (compNum >= 0)
            {
                return JSONHelper.FromString(false, "夜间报事请拨打全国服务热线:400-650-3500");
            }
            else
            {
                string strEnd = Global_Fun.AppWebSettings("EndHour");
                DateTime endDate = Convert.ToDateTime(dtNow.ToShortDateString() + (string.IsNullOrEmpty(strEnd) ? " 08:00:00" : strEnd));
                compNum = DateTime.Compare(dtNow, endDate);
                if (compNum <= 0)
                {
                    return JSONHelper.FromString(false, "夜间报事请拨打全国服务热线:400-650-3500");
                }
            }

            string Content = row["Content"].ToString();
            string CommID = row["CommID"].ToString();
            string CustID = row["CustID"].ToString();
            string RegionalID = row["RegionalID"].ToString();
            string IncidentMan = null;
            string TypeID = null;
            string Phone = "";
            string images = "";
            string ObjectIDList = "";
            string Signatory = "";
            if (row.Table.Columns.Contains("IncidentMan") && !string.IsNullOrEmpty(row["IncidentMan"].ToString()))
            {
                IncidentMan = row["IncidentMan"].ToString();
            }
            if (row.Table.Columns.Contains("TypeID") && !string.IsNullOrEmpty(row["TypeID"].ToString()))
            {
                TypeID = row["TypeID"].ToString();
            }
            if (row.Table.Columns.Contains("Phone") && !string.IsNullOrEmpty(row["Phone"].ToString()))
            {
                Phone = row["Phone"].ToString();
            }
            if (row.Table.Columns.Contains("images") && !string.IsNullOrEmpty(row["images"].ToString()))
            {
                images = row["images"].ToString();
            }
            if (row.Table.Columns.Contains("ObjectIDList") && !string.IsNullOrEmpty(row["ObjectIDList"].ToString()))
            {
                ObjectIDList = row["ObjectIDList"].ToString();
            }
            if (row.Table.Columns.Contains("Signatory") && !string.IsNullOrEmpty(row["Signatory"].ToString()))
            {
                Signatory = row["Signatory"].ToString();
            }

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            //构建链接字符串
            string constr = new CostInfo().GetConnectionStringStr(Community);
            PubConstant.hmWyglConnectionString = constr;
            Regex regex = new Regex(@"(initial catalog = [^;]+);");
            if (regex.IsMatch(constr))
            {
                PubConstant.tw2bsConnectionString = regex.Replace(constr, @"initial catalog = tw2_bs;");
            }
            IDbConnection con = new SqlConnection(constr);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CommID", Community.CommID);
            parameters.Add("@CustID", CustID);
            parameters.Add("@IncidentContent", Content);
            parameters.Add("@Phone", Phone);
            parameters.Add("@IncidentImgs", images);
            parameters.Add("@RegionalID", RegionalID);
            parameters.Add("@IncidentMan", IncidentMan);
            parameters.Add("@Signatory", Signatory);
            parameters.Add("@TypeID", TypeID==null?"":TypeID);

            con.ExecuteScalar("Proc_HSPR_IncidentAccept_PhoneInsert_Region", parameters, null, null, CommandType.StoredProcedure);

            con = new SqlConnection(constr);
            //获取当前报事
            string str = "select * from Tb_HSPR_IncidentAccept where CommID=@CommID  and Phone=@Phone  and IncidentImgs=@IncidentImgs and IncidentContent=@IncidentContent";
            Tb_HSPR_IncidentAccept model = con.Query<Tb_HSPR_IncidentAccept>(str, new { CommID = Community.CommID, Phone = Phone, IncidentImgs = images, IncidentContent = Content }).LastOrDefault();

            IncidentAcceptPush.SynchPushPublicIncident(model);
      
            if (ObjectIDList != "")
            {
                //con = new SqlConnection(constr);
                //获取当前报事
                //str = "select * from Tb_HSPR_IncidentAccept where CommID=@CommID  and CustID=@CustID  and Phone='@Phone'  and IncidentImgs='@IncidentImgs' and IncidentContent='@IncidentContent'";
                //Tb_HSPR_IncidentAccept model = con.Query<Tb_HSPR_IncidentAccept>(str, new { CommID = Community.CommID, CustID = CustID, Phone = Phone, IncidentImgs = images, IncidentContent = Content }).SingleOrDefault();
       
                if (model != null)
                {
                    con = new SqlConnection(constr);
                    //删除所有子表数据
                    string IncidentID = model.IncidentID.ToString();
                    SqlParameter[] dbParams = new SqlParameter[] {
                        new SqlParameter("@IncidentID",SqlDbType.BigInt)
                    };
                    dbParams[0].Value = IncidentID;
                    int rowNum = con.Execute("Proc_HSPR_IncidentAcceptObject_DeleteALL", dbParams, null, null, CommandType.StoredProcedure);

                    //插入子表数据
                    string[] idListPar = ObjectIDList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < idListPar.Length; i++)
                    {
                        con = new SqlConnection(constr);
                        SqlParameter[] sqlpar = new SqlParameter[] {
                        new SqlParameter("@IncidentAcceptObjectID",SqlDbType.BigInt),
                        new SqlParameter("@ObjectID",SqlDbType.BigInt),
                        new SqlParameter("@IncidentID",SqlDbType.BigInt),
                        new SqlParameter("@IsDelete",SqlDbType.Int),
                        new SqlParameter("@CommID",SqlDbType.Int)
                        };
                        sqlpar[0].Value = 0;
                        sqlpar[1].Value = AppGlobal.StrToLong(idListPar[i]);
                        sqlpar[2].Value = IncidentID;
                        sqlpar[3].Value = 0;
                        sqlpar[4].Value = Community.CommID;
                        dbParams[0].Value = IncidentID;
                        int rowNum_inser = con.Execute("Proc_HSPR_IncidentAcceptObject_Insert", sqlpar, null, null, CommandType.StoredProcedure);

                    }
                
                }
            }
            if (model != null)
            {
              
                //鸿坤实时推送报事
                RealTimeIncident_HK(model);
            }

            //SelectAppMsgSend(constr, "公共区域", Community.CommID, "", TypeID, null == model ? "" : model.IncidentID.ToString());
            return JSONHelper.FromStringZL(true, "报事成功!稍后会有人员与您联系!", model.IncidentID.ToString());  //JSONHelper.FromString(true, "报事成功!");
        }


        #region 联创 户内报事、公区报事


        /// <summary>
        /// 公区报事  联创
        /// </summary>
        /// <param name="row"></param>
        /// 2016-11-18，调整，公区报事 报事区域允许为空
        /// <returns></returns>
        private string SetIncidentAcceptPhoneInsertRegionFor118(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            if (!row.Table.Columns.Contains("Content") || string.IsNullOrEmpty(row["Content"].ToString()))
            {
                return JSONHelper.FromString(false, "报事内容不能为空");
            }


            string Content = row["Content"].ToString();
            string CommID = row["CommID"].ToString();
            string CustID = row["CustID"].ToString();
            string RegionalID = "";
            string TypeID = "其它";
            string Phone = "";
            string images = "";
            string ObjectIDList = "";
            string Signatory = "";
            if (row.Table.Columns.Contains("Phone") && !string.IsNullOrEmpty(row["Phone"].ToString()))
            {
                Phone = row["Phone"].ToString();
            }
            if (row.Table.Columns.Contains("images") && !string.IsNullOrEmpty(row["images"].ToString()))
            {
                images = row["images"].ToString();
            }
            if (row.Table.Columns.Contains("ObjectIDList") && !string.IsNullOrEmpty(row["ObjectIDList"].ToString()))
            {
                ObjectIDList = row["ObjectIDList"].ToString();
            }
            if (row.Table.Columns.Contains("Signatory") && !string.IsNullOrEmpty(row["Signatory"].ToString()))
            {
                Signatory = row["Signatory"].ToString();
            }
            if (row.Table.Columns.Contains("TypeID") && !string.IsNullOrEmpty(row["TypeID"].ToString()))
            {
                TypeID = row["TypeID"].ToString();
            }
            if (row.Table.Columns.Contains("RegionalID") && !string.IsNullOrEmpty(row["RegionalID"].ToString()))
            {
                RegionalID = row["RegionalID"].ToString();
            }



            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            //构建链接字符串
            string constr = new CostInfo().GetConnectionStringStr(Community);
            IDbConnection con = new SqlConnection(constr);

            //应联创要求，在此处新增报事类型的查询并判断【根据Type值来判断】            
            Tb_HSPR_IncidentType IncidentType = con.Query<Tb_HSPR_IncidentType>("select * from Tb_HSPR_IncidentType  where CommID=@CommID and TypeName=@TypeName", new { CommID = Community.CommID, TypeName = TypeID }).SingleOrDefault<Tb_HSPR_IncidentType>();
            //如果有传类别，且没有找到的情况下，默认取【其它】这个类别的编号
            if (IncidentType == null || IncidentType.TypeID.ToString() == "")
            {
                con = new SqlConnection(constr);
                IncidentType = con.Query<Tb_HSPR_IncidentType>("select * from Tb_HSPR_IncidentType  where CommID=@CommID and TypeName=@TypeName", new { CommID = Community.CommID, TypeName = "其它" }).SingleOrDefault<Tb_HSPR_IncidentType>();

                IncidentType.TypeID = AppGlobal.StrToLong(IncidentType.TypeID.ToString().Replace(",", ""));
            }
            TypeID = IncidentType.TypeID.ToString();


            //2016-11-28 增加报事返回
            //DataSet ds= DbHelperSQL.RunProcedure("Proc_HSPR_IncidentAccept_PhoneInsert_Region", parameters).ToDataSet();
            //DataSet ds =DbHelperSQL.Query(" exec Proc_HSPR_IncidentAccept_PhoneInsert_Region  '"+ Community.CommID+"','"+ CustID + "','"+ Content + "','"+ Phone +"','"+ images +"','"+ RegionalID +"','"+ Signatory +"','"+ TypeID +"'");
            con = new SqlConnection(constr);
            DataSet ds = con.ExecuteReader(" exec Proc_HSPR_IncidentAccept_PhoneInsert_Region  '" + Community.CommID + "','" + CustID + "','" + Content + "','" + Phone + "','" + images + "','" + RegionalID + "','" + Signatory + "','" + TypeID + "'", null, null, null, CommandType.Text).ToDataSet();
            string IncidentIDStr = "";
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                IncidentIDStr = ds.Tables[0].Rows[0][0].ToString();
            }
            con = new SqlConnection(constr);
            //获取当前报事
            string str = "select * from Tb_HSPR_IncidentAccept where CommID=@CommID  and Phone=@Phone  and IncidentImgs=@IncidentImgs and IncidentContent=@IncidentContent";
            Tb_HSPR_IncidentAccept model = con.Query<Tb_HSPR_IncidentAccept>(str, new { CommID = Community.CommID, Phone = Phone, IncidentImgs = images, IncidentContent = Content }).LastOrDefault();

            if (ObjectIDList != "")
            {
                if (model != null)
                {
                    con = new SqlConnection(constr);
                    //删除所有子表数据
                    string IncidentID = model.IncidentID.ToString();
                    SqlParameter[] dbParams = new SqlParameter[] {
                        new SqlParameter("@IncidentID",SqlDbType.BigInt)
                    };
                    dbParams[0].Value = IncidentID;
                    int rowNum = con.Execute("Proc_HSPR_IncidentAcceptObject_DeleteALL", dbParams, null, null, CommandType.StoredProcedure);

                    //插入子表数据
                    string[] idListPar = ObjectIDList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < idListPar.Length; i++)
                    {
                        SqlParameter[] sqlpar = new SqlParameter[] {
                        new SqlParameter("@IncidentAcceptObjectID",SqlDbType.BigInt),
                        new SqlParameter("@ObjectID",SqlDbType.BigInt),
                        new SqlParameter("@IncidentID",SqlDbType.BigInt),
                        new SqlParameter("@IsDelete",SqlDbType.Int),
                        new SqlParameter("@CommID",SqlDbType.Int)
                        };
                        sqlpar[0].Value = 0;
                        sqlpar[1].Value = AppGlobal.StrToLong(idListPar[i]);
                        sqlpar[2].Value = IncidentID;
                        sqlpar[3].Value = 0;
                        sqlpar[4].Value = Community.CommID;
                        dbParams[0].Value = IncidentID;
                        int rowNum_inser = con.Execute("Proc_HSPR_IncidentAcceptObject_Insert", sqlpar, null, null, CommandType.StoredProcedure);
                    }
                }
            }
            return JSONHelper.FromString(true, IncidentIDStr);
        }

        /// <summary>
        /// 户内报事  联创
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string SetIncidentAcceptPhoneInsertFor118(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            if (!row.Table.Columns.Contains("Content") || string.IsNullOrEmpty(row["Content"].ToString()))
            {
                return JSONHelper.FromString(false, "报事内容不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房间编号不能为空");
            }
            if (!row.Table.Columns.Contains("Phone") || string.IsNullOrEmpty(row["Phone"].ToString()))
            {
                return JSONHelper.FromString(false, "联系方式不能为空");
            }
            //if (!row.Table.Columns.Contains("TypeID") || string.IsNullOrEmpty(row["TypeID"].ToString()))
            //{
            //    return JSONHelper.FromString(false, "报事项目不能为空");
            //}
            if (!row.Table.Columns.Contains("DealLimit") || string.IsNullOrEmpty(row["DealLimit"].ToString()))
            {
                return JSONHelper.FromString(false, "处理时限不能为空");
            }


            string Content = row["Content"].ToString();
            string CommID = row["CommID"].ToString();
            string CustID = row["CustID"].ToString();
            string RoomID = row["RoomID"].ToString();
            string Phone = row["Phone"].ToString();
            string TypeID = "其它";
            string DealLimit = row["DealLimit"].ToString();
            string images = "";
            string IncidentDate = DateTime.Now.ToString();
            string IncidentMan = "";

            if (row.Table.Columns.Contains("TypeID") && !string.IsNullOrEmpty(row["TypeID"].ToString()))
            {
                TypeID = row["TypeID"].ToString();
            }
            if (row.Table.Columns.Contains("images") && !string.IsNullOrEmpty(row["images"].ToString()))
            {
                images = row["images"].ToString();
            }
            if (row.Table.Columns.Contains("IncidentDate") && !string.IsNullOrEmpty(row["IncidentDate"].ToString()))
            {
                IncidentDate = row["IncidentDate"].ToString();
            }

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            //构建链接字符串
            string constr = new CostInfo().GetConnectionStringStr(Community);
            IDbConnection con = new SqlConnection(constr);

            //应联创要求，在此处新增报事类型的查询并判断【根据Type值来判断】            
            Tb_HSPR_IncidentType IncidentType = con.Query<Tb_HSPR_IncidentType>("select * from Tb_HSPR_IncidentType  where CommID=@CommID and TypeName=@TypeName", new { CommID = Community.CommID, TypeName = TypeID }).SingleOrDefault<Tb_HSPR_IncidentType>();
            //如果有传类别，且没有找到的情况下，默认取【其它】这个类别的编号
            if (IncidentType == null || IncidentType.TypeID.ToString() == "")
            {
                con = new SqlConnection(constr);
                IncidentType = con.Query<Tb_HSPR_IncidentType>("select * from Tb_HSPR_IncidentType  where CommID=@CommID and TypeName=@TypeName", new { CommID = Community.CommID, TypeName = "其它" }).SingleOrDefault<Tb_HSPR_IncidentType>();
                IncidentType.TypeID = AppGlobal.StrToLong(IncidentType.TypeID.ToString().Replace(",", ""));
            }
            TypeID = IncidentType.TypeID.ToString();



            con = new SqlConnection(constr);
            //DataSet ds = DbHelperSQL.Query("select CustName from Tb_HSPR_Customer where CustID=" + CustID + "   and CommID=" + Community.CommID);
            DataSet ds = con.ExecuteReader("select CustName from Tb_HSPR_Customer where CustID=" + CustID + "   and CommID=" + Community.CommID, null, null, null, CommandType.Text).ToDataSet();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                IncidentMan = ds.Tables[0].Rows[0]["CustName"].ToString();
            }

            con = new SqlConnection(constr);
            DataSet dds = con.ExecuteReader(" exec Proc_HSPR_IncidentAccept_PhoneInsert  '" + Community.CommID + "','" + CustID + "','" + RoomID + "','" + IncidentDate + "','" + IncidentMan + "','" + Content + "','" + IncidentDate + "','" + Phone + "','" + images + "','" + TypeID + "'," + DealLimit + ",0", null, null, null, CommandType.Text).ToDataSet();

            string IncidentIDStr = "";
            if (dds != null && dds.Tables.Count > 0 && dds.Tables[0].Rows.Count > 0)
            {
                IncidentIDStr = dds.Tables[0].Rows[0][0].ToString();
            }
            return JSONHelper.FromString(true, IncidentIDStr);
        }

        #endregion


        /// <summary>
        /// 获取报事小类
        /// </summary>
        /// <param name="row"></param>
        /// CommID 小区编号【必填】
        /// TypeCode 大类编码【必填】
        /// 返回：
        ///     TypeID  类型编号
        ///     TypeName  类型名称
        ///     DealLimit  处理时限
        /// <returns></returns>
        private string GetIncidentSmallType(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("TypeCode") || string.IsNullOrEmpty(row["TypeCode"].ToString()))
            {
                return JSONHelper.FromString(false, "大类编号不能为空");
            }
            string TypeCode = row["TypeCode"].ToString();
            string CommID = row["CommID"].ToString();
            string IncidentPlace = null;//业主权属 公共区域

            if (row.Table.Columns.Contains("IncidentPlace") && !string.IsNullOrEmpty(row["IncidentPlace"].ToString()))
            {
                IncidentPlace = row["IncidentPlace"].ToString();
            }

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            //构建链接字符串
            string ContionString = new Business.CostInfo().GetConnectionStringStr(Community);
            IDbConnection con = new SqlConnection(ContionString);
            string str = "select  t.TypeID,i.TypeName,t.DealLimit from Tb_HSPR_IncidentType as t inner join Tb_HSPR_CorpIncidentType as i on i.CorpTypeID = t.CorpTypeID where t.IsTreeRoot = 0 and t.CommID = '" + Community.CommID + "' and i.IsDelete = 0 and isnull(i.IsEnabled,0)=0 and t.TypeCode like '" + TypeCode + "%'";

            if (IncidentPlace != null)
            {
                str += " and  t.IncidentPlace like '" + IncidentPlace + "%'";

            }


            DataSet ds = con.ExecuteReader(str, null, null, null, CommandType.Text).ToDataSet();


            return JSONHelper.FromString(ds.Tables[0]);
        }


        /// <summary>
        /// 获取大类
        /// </summary>
        /// <param name="row"></param>
        /// CommID  小区编号  必填
        /// 返回：
        ///      value 类型名称
        ///      key   类型编码
        /// <returns></returns>
        private string GetIncidentBigType(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            string CommID = row["CommID"].ToString();
            string IncidentPlace = null;//业主权属 公共区域

            if (row.Table.Columns.Contains("IncidentPlace") && !string.IsNullOrEmpty(row["IncidentPlace"].ToString()))
            {
                IncidentPlace = row["IncidentPlace"].ToString();
            }

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }


            //构建链接字符串
            string ContionString = new Business.CostInfo().GetConnectionStringStr(Community);
            IDbConnection con = new SqlConnection(ContionString);

            string str = @"select t.TypeCode as [key],t.TypeName as [value] from Tb_HSPR_IncidentType as t 
                            inner join Tb_HSPR_CorpIncidentType as i on i.CorpTypeID = t.CorpTypeID 
                            where t.IsTreeRoot = 2 and t.CommID =@CommID and i.IsDelete = 0 
                            and isnull(i.IsEnabled,0)=0";

            if (IncidentPlace != null)
            {
                str += $" AND t.incidentPlace LIKE '%{IncidentPlace}%' ";
            }



            // 敏捷
            if (Community.CorpID == 1971)
            {
                str += " AND len(t.TypeCode)=4";
            }

            DataSet ds = con.ExecuteReader(str, new { CommID = Community.CommID }, null, null, CommandType.Text).ToDataSet();

            return JSONHelper.FromString(ds.Tables[0]);
        }

        public string IsCanSubmitIncident(DataRow row)
        {
            DateTime dtNow = DateTime.Now;
            string strStart = Global_Fun.AppWebSettings("StartHour");
            DateTime startDate = Convert.ToDateTime(dtNow.ToShortDateString() + (string.IsNullOrEmpty(strStart) ? " 14:00:00" : strStart));
            int compNum = DateTime.Compare(dtNow, startDate);
            if (compNum >= 0)
            {
                return JSONHelper.FromString(false, "夜间报事请拨打全国服务热线:400-650-3500");  
            }
            else
            {
                string strEnd = Global_Fun.AppWebSettings("EndHour");
                DateTime endDate = Convert.ToDateTime(dtNow.ToShortDateString() + (string.IsNullOrEmpty(strEnd) ? " 08:00:00" : strEnd));
                compNum = DateTime.Compare(dtNow, endDate);
                if (compNum <= 0)
                {
                    return JSONHelper.FromString(false, "夜间报事请拨打全国服务热线:400-650-3500");
                }
            }

            return JSONHelper.FromString(true, "可以发起报事");
        }

        /// <summary>
        /// 户内报事
        /// </summary>
        /// <param name="row"></param>
        /// CommID   小区编号  必填
        /// CustID    客户编号  必填
        /// Content   报事内容   必填
        /// RoomID    房间编号  必填
        /// Phone     联系方式  必填
        /// TypeID    报事项目  必填
        /// DealLimit 处理时限   必填
        /// images    图片   
        /// IncidentDate  报事时间
        /// 
        /// <returns></returns>
        private string SetIncidentAcceptPhoneInsert(DataRow row)
        {

            try
            {
                if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
                {
                    return JSONHelper.FromString(false, "小区编号不能为空");
                }
                if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
                {
                    return JSONHelper.FromString(false, "客户编号不能为空");
                }
                if (!row.Table.Columns.Contains("Content") || string.IsNullOrEmpty(row["Content"].ToString()))
                {
                    return JSONHelper.FromString(false, "报事内容不能为空");
                }
                if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
                {
                    return JSONHelper.FromString(false, "房间编号不能为空");
                }
                if (!row.Table.Columns.Contains("Phone") || string.IsNullOrEmpty(row["Phone"].ToString()))
                {
                    return JSONHelper.FromString(false, "联系方式不能为空");
                }

                DateTime dtNow = DateTime.Now;
                string strStart = Global_Fun.AppWebSettings("StartHour");
                DateTime startDate = Convert.ToDateTime(dtNow.ToShortDateString() + (string.IsNullOrEmpty(strStart) ? " 14:00:00" : strStart));
                int compNum = DateTime.Compare(dtNow, startDate);
                if (compNum >= 0)
                {
                    return JSONHelper.FromString(false, "夜间报事请拨打全国服务热线:400-650-3500");
                }
                else
                {
                    string strEnd = Global_Fun.AppWebSettings("EndHour");
                    DateTime endDate = Convert.ToDateTime(dtNow.ToShortDateString() + (string.IsNullOrEmpty(strEnd) ? " 08:00:00" : strEnd));
                    compNum = DateTime.Compare(dtNow, endDate);
                    if (compNum <= 0)
                    {
                        return JSONHelper.FromString(false, "夜间报事请拨打全国服务热线:400-650-3500");
                    }
                }

                string Content = row["Content"].ToString();
                string CommID = row["CommID"].ToString();
                string CustID = row["CustID"].ToString();
                string RoomID = row["RoomID"].ToString();
                string Phone = row["Phone"].ToString();
                string images = "";
                string IncidentDate = DateTime.Now.ToString();
                string ReserveDate = null;
                string TypeID = null;
                string DealLimit = null;
                if (row.Table.Columns.Contains("images") && !string.IsNullOrEmpty(row["images"].ToString()))
                {
                    images = row["images"].ToString();
                }
                if (row.Table.Columns.Contains("IncidentDate") && !string.IsNullOrEmpty(row["IncidentDate"].ToString()))
                {
                    IncidentDate = row["IncidentDate"].ToString();
                }
                if (row.Table.Columns.Contains("ReserveDate") && !string.IsNullOrEmpty(row["ReserveDate"].ToString()))
                {
                    ReserveDate = row["ReserveDate"].ToString();
                }
                if (row.Table.Columns.Contains("TypeID") && !string.IsNullOrEmpty(row["TypeID"].ToString()))
                {
                    TypeID = row["TypeID"].ToString();
                }
                if (row.Table.Columns.Contains("DealLimit") && !string.IsNullOrEmpty(row["DealLimit"].ToString()))
                {
                    DealLimit = row["DealLimit"].ToString();
                }

                //查询小区
                Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);

                string tw2Str = PubConstant.tw2bsConnectionString;
                //构造链接字符串
                if (Community == null)
                {
                    return JSONHelper.FromString(false, "该小区不存在");
                }
                //构建链接字符串
                string ContionString = new Business.CostInfo().GetConnectionStringStr(Community);
                PubConstant.hmWyglConnectionString = ContionString;
                Global_Var.CorpId = Community.CorpID.ToString();
                Global_Var.LoginCorpID = Community.CorpID.ToString();
                Regex regex = new Regex(@"(initial catalog = [^;]+);");
                if (regex.IsMatch(ContionString))
                {
                    PubConstant.tw2bsConnectionString = regex.Replace(ContionString, @"initial catalog = tw2_bs;");
                }

                IDbConnection con = new SqlConnection(ContionString);

                string IncidentMan = null;
                if (row.Table.Columns.Contains("IncidentMan") && !string.IsNullOrEmpty(row["IncidentMan"].ToString()))
                {
                    IncidentMan = row["IncidentMan"].ToString();
                }
                else
                {
                    DataSet ds = con.ExecuteReader("select CustName from Tb_HSPR_Customer where CustID=" + CustID + "   and CommID=" + Community.CommID, null, null, null, CommandType.Text).ToDataSet();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        IncidentMan = ds.Tables[0].Rows[0]["CustName"].ToString();
                    }
                }

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CommID", Community.CommID);
                parameters.Add("@CustID", CustID);
                parameters.Add("@RoomID", RoomID);
                parameters.Add("@IncidentDate", DateTime.Now);
                parameters.Add("@IncidentMan", IncidentMan);
                parameters.Add("@IncidentContent", Content);

                parameters.Add("@ReserveDate", IncidentDate);
                parameters.Add("@Phone", Phone);
                parameters.Add("@IncidentImgs", images);
                parameters.Add("@TypeID", TypeID);
                parameters.Add("@DealLimit", DealLimit);
                parameters.Add("@EmergencyDegree", 0);

                con = new SqlConnection(ContionString);
                con.Execute("Proc_HSPR_IncidentAccept_PhoneInsert", parameters, null, null, CommandType.StoredProcedure);

                con = new SqlConnection(ContionString);

                //获取当前报事
                string str = "select * from Tb_HSPR_IncidentAccept where CommID=@CommID  and Phone=@Phone  and IncidentImgs=@IncidentImgs and IncidentContent=@IncidentContent";
                dynamic model = con.Query(str, new
                {
                    CommID = Community.CommID,
                    Phone = Phone,
                    IncidentImgs = images,
                    IncidentContent = Content
                }).LastOrDefault();

                string IncidentID = "";
                //zw增加model非空验证
                if (model != null)
                {
                    try
                    {
                        IncidentID = model.IncidentID + "";
                        if (string.IsNullOrEmpty(model.TypeID))
                        {
                            IncidentAcceptPush.SynchPushIndoorIncidentWithoutIncidentType(new Tb_HSPR_IncidentAccept()
                            {
                                TypeID = model.TypeID,
                                CommID = model.CommID,
                                RoomID = model.RoomID,
                                IncidentID = model.IncidentID,
                                IncidentPlace = model.IncidentPlace,
                                IncidentMan = model.IncidentMan,
                            });
                        }
                        else
                        {
                            IncidentAcceptPush.SynchPushIndoorIncident(new Tb_HSPR_IncidentAccept()
                            {
                                TypeID = model.TypeID,
                                CommID = model.CommID,
                                RoomID = model.RoomID,
                                IncidentID = model.IncidentID,
                                IncidentPlace = model.IncidentPlace,
                                IncidentMan = model.IncidentMan,
                            });
                        }

                        // 鸿坤实时推送报事
                        if (Community.CorpID == 1973)
                        {
                            RealTimeIncident_HK(model);
                        }
                    }
                    catch(Exception ex11)
                    {

                    }

                }

                return JSONHelper.FromString(true, "报事成功!稍后会有人员与您联系!");  //JSONHelper.FromStringZL(true, "报事成功!稍后会有人员与您联系!", IncidentID);//
            }
            catch(Exception ex)
            {
                WriteLog(ex.Message);
                return JSONHelper.FromString(true, "报事成功!稍后会有人员与您联系!");//JSONHelper.FromStringZL(true, "报事失败,请联系管家!", "");
            }
        }


        #region 北方报事查询接口
        /// <summary>
        /// 报事查询
        /// 公司名称：   CorpName【必填】
        /// 当前页码：   PageIndex选填【默认1】
        /// 每页条数：   PageSize 选填【默认20】
        /// 小区编号：   CommIDs选填【格式：'****','****','****'】
        /// 报事类别:    TypeID选 填
        /// 报事类型：   SelClass选填 【格式：1：书面派工；2：口头派工】
        /// 查询范围：   IsDelete 选填【默认只查询有效的】
        /// 报事提交方式： hiInfoSource【网上提交；手机提交；其它方式；电话提交；现场提交】
        /// 报事区域：   IncidentPlace【业主权属；公共区域】
        /// 是否已转协调： hiIsDispTypeText【true】
        /// 派工/协调单编号：CoordinateNum
        /// 派工类别：   DispTypeName【派工单；协调单】
        /// 客户编号：   CustID
        /// 客户名称：   CustName
        /// 房间ID：      RoomID
        /// 房间编号：   RoomSign
        /// 派工状态：   DispTypeState【未分派；已分派】
        /// 回访状态：    DrReply【未回访；已回访】
        /// 处理状态:   State【未完结、已逾期、已完结】
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string NewIncidentSeachList(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("CorpName") || string.IsNullOrEmpty(Row["CorpName"].ToString()))
            {
                return JSONHelper.FromString(false, "公司名称不能为空");
            }
            //获取链接字符串
            string ConStr = Connection.GetCorpSQLContion_new("SQLConnection", Row["CorpName"].ToString(), 0);


            int PageCount, Counts;
            int Page = 1;
            int Rows = 20;
            if (Row.Table.Columns.Contains("PageIndex"))
            {
                Page = AppGlobal.StrToInt(Row["PageIndex"].ToString());
            }
            if (Row.Table.Columns.Contains("PageSize"))
            {
                Rows = AppGlobal.StrToInt(Row["PageSize"].ToString());
            }

            string IncidentStartDate = "";
            string IncidentEndDate = "";

            string StrSql = GetSQL(Row);
            if (Row.Table.Columns.Contains("IncidentStartDate"))
            {

                StrSql = StrSql + " and  IncidentDate>='" + AppGlobal.StrToStr(Row["IncidentStartDate"].ToString()) + "' "  ;
         
            }

            if (Row.Table.Columns.Contains("IncidentEndDate"))
            {
                StrSql = StrSql + " and IncidentDate<='" + AppGlobal.StrToStr(Row["IncidentEndDate"].ToString()) + "' ";
        
            }



            if (Row.Table.Columns.Contains("MainStartDate"))
            {
                StrSql = StrSql + " and MainEndDate>='" + AppGlobal.StrToStr(Row["MainStartDate"].ToString()) + "' ";

            }

            if (Row.Table.Columns.Contains("MainEndDate"))
            {
                StrSql = StrSql + " and MainEndDate<='" + AppGlobal.StrToStr(Row["MainEndDate"].ToString()) + "' ";

            }

            DataSet Ds = BaseGetData.GetList(ConStr, out PageCount, out Counts, StrSql.ToString(), Page, Rows, "IncidentDate", 1, "view_HSPR_IncidentSeachBF_Filter", "IncidentID");

            return JSONHelper.FromString(Ds.Tables[0]);
        }


        #region 返回查询条件

        private string GetSQL(DataRow Row)
        {

            string strSQL = "";

            if (Row.Table.Columns.Contains("CommIDs"))
            {
                if (!string.IsNullOrEmpty(Row["CommIDs"].ToString()) && Row["CommIDs"].ToString() != "0")
                {
                    strSQL = " and CommID in( " + Row["CommIDs"].ToString() + " ) ";
                }
            }


            string TypeID = "";
            if (Row.Table.Columns.Contains("TypeID"))
            {
                TypeID = Row["TypeID"].ToString();
            }
            string SelClass = "";
            if (Row.Table.Columns.Contains("SelClass"))
            {
                SelClass = Row["SelClass"].ToString();
            }
            string IsDelete = "0";
            if (Row.Table.Columns.Contains("IsDelete"))
            {
                IsDelete = Row["IsDelete"].ToString();
            }
            string InfoSource = "";
            if (Row.Table.Columns.Contains("hiInfoSource"))
            {
                InfoSource = Row["hiInfoSource"].ToString();
            }

            string IncidentPlace = "";
            if (Row.Table.Columns.Contains("IncidentPlace"))
            {
                IncidentPlace = Row["IncidentPlace"].ToString();
            }
            string IsDispTypeText = "";
            if (Row.Table.Columns.Contains("hiIsDispTypeText"))
            {
                IsDispTypeText = Row["hiIsDispTypeText"].ToString();
            }
            string IncidentNum = "";
            if (Row.Table.Columns.Contains("IncidentNum"))
            {
                IncidentNum = Row["IncidentNum"].ToString();
            }
            string CoordinateNum = "";
            if (Row.Table.Columns.Contains("CoordinateNum"))
            {
                CoordinateNum = Row["CoordinateNum"].ToString();
            }
            string CoordinateNum2 = "";
            if (Row.Table.Columns.Contains("CoordinateNum2"))
            {
                CoordinateNum2 = Row["CoordinateNum2"].ToString();
            }
            string CoordinateNum3 = "";
            if (Row.Table.Columns.Contains("CoordinateNum3"))
            {
                CoordinateNum3 = Row["CoordinateNum3"].ToString();
            }
            string CustID = "";
            if (Row.Table.Columns.Contains("CustID"))
            {
                CustID = Row["CustID"].ToString();
            }
            string CustName = "";
            if (Row.Table.Columns.Contains("CustName"))
            {
                CustName = Row["CustName"].ToString();
            }
            string RoomID = "";
            if (Row.Table.Columns.Contains("RoomID"))
            {
                RoomID = Row["RoomID"].ToString();
            }
            string RoomSign = "";
            if (Row.Table.Columns.Contains("RoomSign"))
            {
                RoomSign = Row["RoomSign"].ToString();
            }
            string DispTypeState = "";
            if (Row.Table.Columns.Contains("DispTypeState"))
            {
                DispTypeState = Row["DispTypeState"].ToString();
            }
            string DrReply = "";
            if (Row.Table.Columns.Contains("DrReply"))
            {
                DrReply = Row["DrReply"].ToString();
            }
            string Achievelimit = "";
            if (Row.Table.Columns.Contains("Achievelimit"))
            {
                Achievelimit = Row["Achievelimit"].ToString();
            }
            string DueAmount = "";
            if (Row.Table.Columns.Contains("DueAmount"))
            {
                DueAmount = Row["DueAmount"].ToString();
            }
            string AdmiMan = "";
            if (Row.Table.Columns.Contains("AdmiMan"))
            {
                AdmiMan = Row["AdmiMan"].ToString();
            }

            string BuildSNum = "";
            if (Row.Table.Columns.Contains("BuildSNum"))
            {
                BuildSNum = Row["BuildSNum"].ToString();
            }
            string RegionSNum = "";
            if (Row.Table.Columns.Contains("RegionSNum"))
            {
                RegionSNum = Row["RegionSNum"].ToString();
            }
            string IncidentDate1 = "";
            if (Row.Table.Columns.Contains("IncidentDate1"))
            {
                IncidentDate1 = Row["IncidentDate1"].ToString();
            }
            string IncidentDate2 = "";
            if (Row.Table.Columns.Contains("IncidentDate2"))
            {
                IncidentDate2 = Row["IncidentDate2"].ToString();
            }
            string ReserveDate1 = "";
            if (Row.Table.Columns.Contains("ReserveDate1"))
            {
                ReserveDate1 = Row["ReserveDate1"].ToString();
            }
            string ReserveDate2 = "";
            if (Row.Table.Columns.Contains("ReserveDate2"))
            {
                ReserveDate2 = Row["ReserveDate2"].ToString();
            }
            string DispTypeName = "";
            if (Row.Table.Columns.Contains("DispTypeName"))
            {
                DispTypeName = Row["DispTypeName"].ToString();
            }



            string State = "";
            if (Row.Table.Columns.Contains("State"))
            {
                State = Row["State"].ToString();
            }
            string MainEndDate1 = "";
            if (Row.Table.Columns.Contains("MainEndDate1"))
            {
                MainEndDate1 = Row["MainEndDate1"].ToString();
            }
            string MainEndDate2 = "";
            if (Row.Table.Columns.Contains("MainEndDate2"))
            {
                MainEndDate2 = Row["MainEndDate2"].ToString();
            }
            string DealMan = "";
            if (Row.Table.Columns.Contains("DealMan"))
            {
                DealMan = Row["DealMan"].ToString();
            }
            string FinishUser = "";
            if (Row.Table.Columns.Contains("FinishUser"))
            {
                FinishUser = Row["FinishUser"].ToString();
            }
            string IncidentMan = "";
            if (Row.Table.Columns.Contains("IncidentMan"))
            {
                IncidentMan = Row["IncidentMan"].ToString();
            }
            string RegionalPlace = "";
            if (Row.Table.Columns.Contains("RegionalPlace"))
            {
                RegionalPlace = Row["RegionalPlace"].ToString();
            }
            string RegionalName = "";
            if (Row.Table.Columns.Contains("RegionalName"))
            {
                RegionalName = Row["RegionalName"].ToString();
            }
            string ServiceQuality = "";
            if (Row.Table.Columns.Contains("ServiceQuality"))
            {
                ServiceQuality = Row["ServiceQuality"].ToString();
            }
            string IsGenjin = "";
            if (Row.Table.Columns.Contains("IsGenjin"))
            {
                IsGenjin = Row["IsGenjin"].ToString();
            }
            string Phone = "";
            if (Row.Table.Columns.Contains("Phone"))
            {
                Phone = Row["Phone"].ToString();
            }

            if (TypeID != "")
            {
                string[] _tempTypeID = TypeID.Replace("'", "").Split(',');
                string _temoOrType = "";
                for (int i = 0; i < _tempTypeID.Length; i++)
                {
                    if (_tempTypeID[i].Trim() != "")
                    {
                        if (_temoOrType == "")
                        {
                            _temoOrType += " TypeCode like '%" + _tempTypeID[i] + "%'";
                        }
                        else
                        {
                            _temoOrType += " OR TypeCode like '%" + _tempTypeID[i] + "%'";
                        }
                    }
                }
                if (_temoOrType != "")
                {
                    strSQL = strSQL + " and (" + _temoOrType + ")";
                }
            }


            //报事类型
            if (SelClass != "" && SelClass != "0")
            {
                strSQL = strSQL + " and substring(ClassID,1,1) = " + SelClass + " ";
            }


            //查询范围
            if (IsDelete != "全部" || IsDelete != "")
            {
                strSQL = strSQL + " and isnull(IsDelete,0) = " + IsDelete.ToString() + " ";
            }



            //信息来源
            if (InfoSource != "")
            {
                strSQL = strSQL + " and IncidentMode = '" + InfoSource + "' ";
            }


            //报事区域
            if (IncidentPlace != "")
            {
                strSQL = strSQL + " and IncidentPlace = '" + IncidentPlace + "' ";
            }
            if (IsDispTypeText == "true")
            {
                strSQL = strSQL + " and IsDispTypeText = '已转协调' ";
            }


            //报事编号
            if (IncidentNum != "")
            {
                strSQL = strSQL + " and IncidentNum like '%" + AppGlobal.ToWildcard(IncidentNum) + "%' ";
            }
            if (CoordinateNum != "")
            {
                strSQL = strSQL + "and DispType = 1 and CoordinateNum like '%" + AppGlobal.ToWildcard(CoordinateNum) + "%'";
            }
            if (CoordinateNum2 != "")
            {
                strSQL = strSQL + " and DispType = 2 and CoordinateNum like '%" + AppGlobal.ToWildcard(CoordinateNum2) + "%'";
            }
            if (CoordinateNum3 != "")
            {
                strSQL = strSQL + " and DispType = 3 and CoordinateNum like '%" + AppGlobal.ToWildcard(CoordinateNum3) + "%'";
            }
            if (DispTypeName != "")
            {
                if (DispTypeName == "派工单")
                {
                    strSQL = strSQL + "and DispType = 1";
                }

                if (DispTypeName == "协调单")
                {
                    strSQL = strSQL + "and DispType = 2";
                }
            }

            //楼宇
            if (AppGlobal.StrToInt(BuildSNum.ToString()) != 0)
            {
                strSQL = strSQL + " and BuildSNum = " + AppGlobal.StrToInt(BuildSNum.ToString()).ToString() + " ";
            }
            else if (BuildSNum.ToString().IndexOf(',') > -1)
            {
                strSQL = strSQL + " and BuildSNum in( " + BuildSNum + ") ";
            }

            //组团区域
            if (AppGlobal.StrToInt(RegionSNum.ToString()) != 0)
            {
                strSQL = strSQL + " and RegionSNum = " + AppGlobal.StrToInt(RegionSNum.ToString()).ToString() + " ";
            }

            #region 报事时间


            //string strIncidentDate1 = IncidentDate1;
            //string strIncidentDate2 = IncidentDate2;
            if (IncidentDate1 != "" && IncidentDate2 != "")
            {
                strSQL = strSQL + " and IncidentDate between '" + IncidentDate1 + "' and '" + IncidentDate2 + "' ";
            }


            #endregion

            #region 预约时间

            //string strReserveDate1 = ReserveDate1;
            //string strReserveDate2 = ReserveDate2;
            //strSQL = strSQL + " and ReserveDate between '" + strReserveDate1 + "' and '" + strReserveDate2 + "' ";

            #endregion

            //处理状态DrDealState
            if (State != "")
            {
                strSQL = strSQL + " and State = '" + State.ToString() + "' ";

                //DrDealState = 1 已完结
                if (State == "已完结")
                {
                    #region 完结时间


                    //string strMainEndDate1 = MainEndDate1;
                    //string strMainEndDate2 = MainEndDate2;

                    if (IncidentDate1 != "" && IncidentDate2 != "")
                    {
                        strSQL = strSQL + " and MainEndDate between '" + MainEndDate1 + "' and '" + MainEndDate2 + "' ";
                    }


                    #endregion
                }

            }

            //客户
            if (CustID != "")
            {
                strSQL = strSQL + " and CustID = " + CustID;
            }
            //客户
            if (CustName != "")
            {
                strSQL = strSQL + " and CustName like '%" + AppGlobal.ToWildcard(CustName) + "%' ";
            }
            if (Phone != "")
            {
                strSQL = strSQL + " and Phone like '%" + AppGlobal.ToWildcard(Phone) + "%' ";
            }
            //房号
            if (RoomID != "")
            {
                strSQL = strSQL + " and RoomID = " + RoomID;
            }
            if (RoomSign != "")
            {
                strSQL = strSQL + " and RoomSign like '%" + AppGlobal.ToWildcard(RoomSign) + "%' ";
            }
            //派工类别
            if (DispTypeState != "")
            {
                strSQL = strSQL + " and DispTypeState = '" + DispTypeState + "' ";


            }


            //回访状态
            if (DrReply != "")
            {
                strSQL = strSQL + " and IncidentReply = '" + DrReply + "' ";
            }

            //完成方式
            if (Achievelimit != "")
            {
                strSQL = strSQL + " and Achievelimit = '" + Achievelimit + "' ";
            }

            //有无费用
            if (DueAmount != "")
            {
                if (DueAmount == "有")
                {
                    strSQL = strSQL + " and isnull(DueAmount,0) > 0 ";
                }
                if (DueAmount == "无")
                {
                    strSQL = strSQL + " and isnull(DueAmount,0) = 0 ";
                }
            }
            if (AdmiMan != "")
            {
                strSQL = strSQL + " and AdmiMan like '%" + AppGlobal.ToWildcard(AdmiMan) + "%' ";
            }

            //处理人
            if (DealMan != "")
            {
                strSQL = strSQL + " and DealMan like '%" + AppGlobal.ToWildcard(DealMan) + "%' ";
            }

            //完结人
            if (FinishUser != "")
            {
                strSQL = strSQL + " and FinishUser like '%" + AppGlobal.ToWildcard(FinishUser) + "%' ";
            }
            //报事人
            if (IncidentMan != "")
            {
                strSQL = strSQL + " and IncidentMan like '%" + AppGlobal.ToWildcard(IncidentMan) + "%' ";
            }

            //公区位置
            if (RegionalPlace != "")
            {
                strSQL = strSQL + " and RegionalPlace like '%" + AppGlobal.ToWildcard(RegionalPlace) + "%' ";
            }

            //公区名称
            if (RegionalName != "")
            {
                strSQL = strSQL + " and RegionalName like '%" + AppGlobal.ToWildcard(RegionalName) + "%' ";
            }

            //服务质量评价
            if (ServiceQuality != "")
            {
                strSQL = strSQL + " and ServiceQuality = '" + ServiceQuality + "' ";
            }
            if (IsGenjin != "")
            {

                if (IsGenjin == "有")
                {
                    strSQL = strSQL + " and RegList is not null";
                }
                else
                {
                    strSQL = strSQL + " and RegList is null";
                }
            }

            string strExtensionDays = "";
            string strCompSymbol = "";
            if (Row.Table.Columns.Contains("ExtensionDays"))
            {
                strExtensionDays = Row["ExtensionDays"].ToString();
            }
            if (Row.Table.Columns.Contains("CompSymbol"))
            {
                strCompSymbol = Row["CompSymbol"].ToString();
            }
            if (strCompSymbol != "")
            {
                if (AppGlobal.StrToInt(strExtensionDays) != 0)
                {
                    strSQL = strSQL + " and isnull(ExtensionDays,0)  " + strCompSymbol + " '" + strExtensionDays + "' ";
                }
            }
            return strSQL;

        }

        #endregion
        #endregion




        #region 中粮公区报事【提供给中粮呼叫中心】
        /// <summary>
        /// 中粮公区报事   
        /// 中粮呼叫中心接到报事单后，判断如果是物业的，调用此接口，将报事信息写到ERP中，
        /// ERP在报事完结后，通过调用对方提供的接口将报事状态回传过去【该操作做在ERP中】
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string SetIncidentAcceptPhoneInsertRegion_ZL(DataRow row)
        {

            if (!row.Table.Columns.Contains("Commid") || string.IsNullOrEmpty(row["Commid"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            if (!row.Table.Columns.Contains("Phone") || string.IsNullOrEmpty(row["Phone"].ToString()))
            {
                return JSONHelper.FromString(false, "联系方式不能为空");
            }

            if (!row.Table.Columns.Contains("InfoSource") || string.IsNullOrEmpty(row["Phone"].ToString()))
            {
                return JSONHelper.FromString(false, "报事来源不能为空");
            }

            if (!row.Table.Columns.Contains("IncidentClass") || string.IsNullOrEmpty(row["Phone"].ToString()))
            {
                return JSONHelper.FromString(false, "报事类型不能为空");
            }
            //if (!row.Table.Columns.Contains("IncidentAcceptID") || string.IsNullOrEmpty(row["IncidentAcceptID"].ToString()))
            //{
            //    return JSONHelper.FromString(false, "客户系统报事ID不能为空");
            //}           

            //string CustName = row["CustName"].ToString();
            string Phone = row["Phone"].ToString();
            string IncidentContent = row["IncidentContent"].ToString();
            string IncidentImgs = row["IncidentImgs"].ToString();
            string IncidentIDs = "";
            string Commid = "";
            //string IncidentClass = ",1753040000000044,";
            //if (row["IncidentClass"].ToString().Equals("2"))
            //{
            //    IncidentClass = ",1755410000000069,";
            //}
            string InfoSource = row["InfoSource"].ToString();
            //string IncidentAcceptID = "";//客户系统报事ID
            try
            {
                Commid = row["Commid"].ToString();

                IDbConnection con = new SqlConnection(AppGlobal.GetConnectionString("SQLConnectionZL"));
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CommID", Commid);
                parameters.Add("@CustID", 0);
                parameters.Add("@IncidentContent", IncidentContent);
                parameters.Add("@Phone", Phone);
                parameters.Add("@IncidentImgs", IncidentImgs);
                //parameters.Add("@IncidentClass", IncidentClass);
                parameters.Add("@InfoSource", InfoSource);
                parameters.Add("@IncidentIDs", IncidentIDs, DbType.String, ParameterDirection.Output);
                con.ExecuteScalar("Proc_HSPR_IncidentAccept_PhoneInsert_Region_ZL", parameters, null, null, CommandType.StoredProcedure);
                IncidentIDs = parameters.Get<String>("@IncidentIDs");//获取数据库输出的值

                DynamicParameters param = new DynamicParameters();
                param.Add("@CutID", 0);
                param.Add("@RoleCode", "客服部");
                param.Add("@CommID", Commid);
                param.Add("@MsgTitle", "你有一条新的报事信息");
                param.Add("@Content", "<a href='/HM/M_Main/IncidentNew/IncidentAcceptManage.aspx?IncidentID=" + IncidentIDs + "&OpType=edit'>你有一条新的报事信息</a>");
                param.Add("@SendTime", DateTime.Now);
                param.Add("@MsgType", 1);
                param.Add("@SendMan", null);
                param.Add("@MsgState", 1);
                param.Add("@IsDeleteSend", null);
                param.Add("@IsDeleteRead", null);
                param.Add("@URL", "<a href='/HM/M_Main/IncidentNew/IncidentAcceptManage.aspx?IncidentID=" + IncidentIDs + "&OpType=edit'>你有一条新的报事信息</a>");
                param.Add("@HaveSendUsers", null);
                param.Add("@IsRemind", 0);
                con.ExecuteScalar("Proc_Tb_Sys_Message_ADD_CURSOR", param, null, null, CommandType.StoredProcedure);

                con.Dispose();

            }
            catch (Exception ex)
            {
                return JSONHelper.FromString(false, ex.Message);
            }



            return JSONHelper.FromString(true, IncidentIDs);
        }


        /// <summary>
        /// 查询项目名称
        /// </summary>
        /// <param name="row"></param>
        /// 根据城市名查询项目，如未传则查询所有项目
        /// 返回：commid,City,COnnName
        /// <returns></returns>
        public string GetCOmmunity(DataRow row)
        {
            string City = "";
            DataTable dt = null;
            try
            {
                if (row.Table.Columns.Contains("City") && !string.IsNullOrEmpty(row["City"].ToString()))
                {
                    City = row["City"].ToString();
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("select commid,Province+City+Borough as City,CommName from Tb_HSPR_Community");
                sb.Append(" where ISNULL(IsDelete,0)=0 ");
                if (City != "")
                {
                    sb.Append(" and Province+City+Borough  like '%" + City + "%'");
                }
                IDbConnection con = new SqlConnection(AppGlobal.GetConnectionString("SQLConnectionZL"));

                dt = con.ExecuteReader(sb.ToString()).ToDataSet().Tables[0];
                con.Dispose();
            }
            catch (Exception ex)
            {
                return JSONHelper.FromString(false, ex.Message);
            }

            return JSONHelper.FromString(dt);

        }


        #endregion



        #region 美利山呼叫中心口派报事

        private string MLSIncidentTelComming(DataRow Row)
        {
            string result = "";

            try
            {
                if (!Row.Table.Columns.Contains("Phone") || string.IsNullOrEmpty(Row["Phone"].ToString()))
                {
                    return JSONHelper.FromString(false, "来电号码不能为空");
                }

                string Phone = Row["Phone"].ToString();

                using (IDbConnection IDbConn = new SqlConnection(PubConstant.GetConnectionString("MLSConnection").ToString()))
                {
                    DataTable dt = IDbConn.ExecuteReader("SELECT * FROM view_HSPR_Telephone_Filter_Model WHERE  IsDelete = 0 AND CustSNum = 1 and charindex(@Phone,MobilePhone)>0",
                        new { Phone = Phone }, null, null, CommandType.Text).ToDataSet().Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        result = JSONHelper.FromString(true, "1");
                    }
                    else
                    {
                        result = JSONHelper.FromString(true, "2"); //'2'  代表 号码无法在天问系统找到匹配的客户
                    }

                }
            }
            catch (Exception ex)
            {
                result = JSONHelper.FromString(false, ex.ToString());
            }

            return result;
        }

        private string MLSIncidentAcceptOral(DataRow row)
        {
            string s = "";
            string result = "";
            try
            {
                if (!row.Table.Columns.Contains("Phone") || string.IsNullOrEmpty(row["Phone"].ToString()))
                {
                    return JSONHelper.FromString(false, "来电号码不能为空");
                }
                if (!row.Table.Columns.Contains("OrderType") || string.IsNullOrEmpty(row["OrderType"].ToString()))
                {
                    return JSONHelper.FromString(false, "门岗放行详情不能为空");
                }

                string Phone = row["Phone"].ToString();
                string ordertype = row["OrderType"].ToString();




                using (IDbConnection Conn = new SqlConnection(PubConstant.GetConnectionString("MLSConnection").ToString()))
                {
                    DynamicParameters dp = new DynamicParameters();

                    DataTable dt = Conn.ExecuteReader("SELECT * FROM view_HSPR_Telephone_Filter_Model WHERE  IsDelete = 0 AND CustSNum = 1 and charindex(@Phone,MobilePhone)>0",
                        new { Phone = Phone }, null, null, CommandType.Text).ToDataSet().Tables[0];


                    if (dt.Rows.Count > 0)
                    {
                        dp.Add("@Phone", Phone);
                        dp.Add("@ordertype", ordertype);
                        dp.Add("@result", "", DbType.String, ParameterDirection.Output);

                        Conn.Close();

                        IDbConnection IConn = new SqlConnection(PubConstant.GetConnectionString("MLSConnection").ToString());


                        IConn.Execute("proc_hspr_IncidentAccept_InsertOral", dp, null, 120, CommandType.StoredProcedure);

                        string IncidentID = dp.Get<string>("@result");

                        #region 推送

                        string commName = "", roomSign = "";

                        List<string> snatchObjects = new List<string>();        // 具有门岗权限的对象

                        string sqlList1 = string.Format(@"SELECT * from view_HSPR_IncidentSeach_Filter where isnull(IsDelete,0)=0 AND IncidentID=@IncidentID");
                        dynamic IncidentInfo = IConn.Query(sqlList1, new { IncidentID = IncidentID }).FirstOrDefault();

                        if (IncidentInfo != null)
                        {
                            commName = IncidentInfo.CommName;
                            roomSign = IncidentInfo.RoomSign;
                        }

                        //string NetType = row["Net"].ToString();
                        //string Account = row["Account"].ToString();
                        string NetType = "4";

                        //双线电信
                        if (NetType == "4")
                        {
                            PubConstant.tw2bsConnectionString = Global_Fun.GetConnectionString("19Connection");
                        }

                        string tw2bsConnectionString = PubConstant.tw2bsConnectionString;
                        //string corpId = Global_Var.CorpId;
                        //string corpId = Account.Substring(0, 4);
                        string corpId = "2081";

                        if (Common.Push.GetAppKeyAndAppSecret(tw2bsConnectionString, corpId, out string appIdentifier, out string appKey, out string appSecret))
                        {

                            // 推送
                            PushModel pushModel = new PushModel(appKey, appSecret)
                            {
                                AppIdentifier = appIdentifier,
                                Badge = 1,
                                KeyInfomation = IncidentInfo.IncidentID.ToString(),
                            };

                            pushModel.Audience.Category = PushAudienceCategory.Alias;
                            pushModel.Extras.Add("CommID", IncidentInfo.CommID.ToString());
                            pushModel.Extras.Add("IncidentID", IncidentInfo.IncidentID.ToString());
                            pushModel.Extras.Add("IncidentPlace", IncidentInfo.IncidentPlace);


                            // 3、判断报事类别是否设置了处理岗位，获取具有抢单权限的人，这里的处理岗位，是针对通用岗位
                            string sql = string.Format(@"SELECT RoleCode FROM Tb_HSPR_IncidentTypeProcessPost 
                                                        WHERE CorpTypeID IN (SELECT CorpTypeID FROM Tb_HSPR_IncidentType 
                                                        WHERE TypeID IN (SELECT Convert(nvarchar(50), ColName) FROM dbo.funSplitTabel('{0}', ',')))", IncidentInfo.TypeID);

                            IEnumerable<dynamic> snatchPostResult = new List<dynamic>();
                            snatchPostResult = IConn.Query(sql);

                            if (snatchPostResult.Count() > 0)
                            {
                                // 获取所有具有处理岗位权限的人员的手机号码
                                sql = string.Format(@"SELECT MobileTel FROM view_Sys_User_RoleData_Filter2 
                                                    WHERE CommID={0} AND IsMobile=1 AND isnull(MobileTel,'')<>'' 
                                                    AND UserCode IN (SELECT UserCode FROM Tb_Sys_UserRole WHERE RoleCode IN(
                                                        SELECT RoleCode FROM Tb_Sys_Role WHERE SysRoleCode IN (
                                                            SELECT RoleCode from Tb_HSPR_IncidentTypeProcessPost WHERE CorpTypeID IN 
                                                                (SELECT CorpTypeID FROM Tb_HSPR_IncidentType WHERE TypeID IN 
                                                    (SELECT Convert(nvarchar(50), ColName) FROM dbo.funSplitTabel('{1}', ','))))))", IncidentInfo.CommID, IncidentInfo.TypeID);

                                IEnumerable<dynamic> searchResult = IConn.Query(sql);

                                if (searchResult.Count() > 0)
                                {
                                    foreach (dynamic item in searchResult)
                                    {
                                        snatchObjects.Add(item.MobileTel);
                                    }

                                    pushModel.Message = string.Format("【{0}{1}】有一条口派报事需要处理，报事人：{2}，点击查看详情。", commName, roomSign, IncidentInfo.IncidentMan);

                                    pushModel.Command = PushCommand.INCIDENT_PROCESSING;
                                    pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];
                                    pushModel.Audience.Objects.AddRange(snatchObjects);

                                    TWTools.Push.Push.SendAsync(pushModel);

                                    result = "成功";
                                    s = JSONHelper.FromString(true, "推送成功");
                                }
                                else
                                {
                                    s = JSONHelper.FromString(true, "没有获取到门岗人员的手机号码");
                                }
                            }
                            else
                            {
                                s = JSONHelper.FromString(true, "没有设置处理的通用岗位");
                            }
                        }

                        #endregion

                    }

                    return s;
                }
            }
            catch (Exception ex)
            {
                return JSONHelper.FromString(false, ex.ToString());
            }

        }
        #endregion


        #region 鸿坤实时推送报事至呼叫中心
        private void RealTimeIncident_HK(dynamic model)
        {

            try
            {     //鸿坤单独
                if (Global_Var.LoginCorpID == "1973")
                  
                    {
                  

                    #region 同步新增报事   
                    Dictionary<string, string> dir = new Dictionary<string, string>();
                    dir.Add("incidentID", model.IncidentID.ToString());
                    dir.Add("commID", model.CommID.ToString());
                    dir.Add("custID", model.CustID.ToString());
                    dir.Add("roomID", model.RoomID.ToString());
                    dir.Add("corpTypeID", model.TypeID);
                    dir.Add("incidentPlace", string.IsNullOrEmpty(model.IncidentPlace) ? "" : model.IncidentPlace.ToString()  );
                    dir.Add("incidentMan", string.IsNullOrEmpty(model.IncidentMan) ? "" : model.IncidentMan.ToString()   );
                    dir.Add("incidentDate", model.IncidentDate.ToString());
                    dir.Add("incidentMode", model.IncidentMode);
                    dir.Add("dealLimit", model.DealLimit.ToString());
                    dir.Add("replyLimit", model.ReplyLimit.ToString());
                    dir.Add("incidentContent", string.IsNullOrEmpty(model.IncidentContent) ? "" : model.IncidentContent.ToString()    );
                    dir.Add("reserveDate", model.ReserveDate.ToString());
                    dir.Add("phone", string.IsNullOrEmpty(model.Phone) ? "" : model.Phone.ToString()     );
                    dir.Add("admiMan", string.IsNullOrEmpty(model.AdmiMan) ? "" : model.AdmiMan.ToString()  );
                    dir.Add("admiDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dir.Add("dispType", string.IsNullOrEmpty(model.DispType) ? "" : model.DispType.ToString()  );
                    dir.Add("dispMan", string.IsNullOrEmpty(model.DispMan) ? "" : model.DispMan.ToString());
                    dir.Add("dispDate", model.DispDate== null ?"":model.DispDate.ToString());
                    dir.Add("dispLimit", model.DispLimit == null ? "" : model.DispLimit.ToString());
                    dir.Add("dealMan",   model.DealMan == null ? "" : model.DealMan.ToString());
                    dir.Add("coordinateNum", model.CoordinateNum == null ? "" : model.CoordinateNum.ToString() );
                    dir.Add("mainEndDate", "");
                    dir.Add("isDeal", "0");//0:未完结1：完结     
                    dir.Add("incidentnum", string.IsNullOrEmpty(model.IncidentNum) ? "" : model.IncidentNum.ToString()    );
                    dir.Add("operateType", "0");//0：新增，1：修改，2：删除 
                    
                    SynchronizeIncidentData.SynchronizeData_WorkOrderInfo(dir, Connection.GetConnection("8"), "业主App报事");

                    #endregion
                }



            }
            //catch (Exception ex)
            //{
            //    WriteLog(ex.Message);

            //}
            catch
            {


            }

        }

        public string RturnString(string text) {

            try
            {
                return string.IsNullOrEmpty(text) ? "" : text;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }

            return "";
        }

 
            #endregion


            public static void WriteLog(string strLog)
        {
            string sFilePath = "d:\\" + DateTime.Now.ToString("yyyyMM");
            string sFileName = "rizhi" + DateTime.Now.ToString("dd") + ".log";
            sFileName = sFilePath + "\\" + sFileName; //文件的绝对路径
            if (!Directory.Exists(sFilePath))//验证路径是否存在
            {
                Directory.CreateDirectory(sFilePath);
                //不存在则创建
            }
            FileStream fs;
            StreamWriter sw;
            if (File.Exists(sFileName))
            //验证文件是否存在，有则追加，无则创建
            {
                fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
            }
            sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "   ---   " + strLog);
            sw.Close();
            fs.Close();
        }

    }
}
