using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
namespace Business
{
    public class SearchCustomerManage_v2 : PubInfo
    {
        public SearchCustomerManage_v2()
        {
            base.Token = "20190604SearchCustomerManage_v2";
        }

        public override void Operate(ref Transfer Trans)
        {
            try
            {
                DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];

                // 验证登录
                if (!new Login().isLogin(ref Trans))
                    return;

                switch (Trans.Command)
                {
                    case "FuzzySearchCustName":  // 模糊查询业主姓名
                        Trans.Result = FuzzySearchCustName(Row);
                        break;
                    case "FuzzySearchRoomSign":  // 模糊查询房屋编号
                        Trans.Result = FuzzySearchRoomSign(Row);
                        break;
                    case "FuzzySearchMobile":  // 模糊查询业主手机
                        Trans.Result = FuzzySearchMobile(Row);
                        break;
                    case "FuzzySearchCarpark":  // 模糊查询车位编号
                        Trans.Result = FuzzySearchCarpark(Row);
                        break;
                    case "FuzzySearchParking":  // 模糊查询车位
                        Trans.Result = FuzzySearchParking(Row);
                        break;
                    case "GetCustomerList":  // 查询客户列表
                        Trans.Result = GetCustomerList(Row);
                        break;
                    case "GetCustomerInfo":  //  查询客户相关模块信息
                        Trans.Result = GetCustomerInfo(Row);
                        break;
                    default:
                        Trans.Result = new ApiResult(false, "接口不存在").toJson();
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                Trans.Result = new ApiResult(false, ex.Message + Environment.CommandLine + ex.StackTrace).toJson();
            }

        }

        /// <summary>
        /// 获取客户信息模块信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetCustomerInfo(DataRow row)
        {
            #region 获取参数
            string CommID = string.Empty;
            if (row.Table.Columns.Contains("CommID"))
            {
                CommID = row["CommID"].ToString();
            }
            if (string.IsNullOrEmpty(CommID))
            {
                return new ApiResult(false, "请先切换到具体小区").toJson();
            }
            string CustID = string.Empty;
            if (row.Table.Columns.Contains("CustID"))
            {
                CustID = row["CustID"].ToString();
            }

            // Type类型，0=客户信息，1=房屋信息，2=车位信息，3=家庭信息，4=装修信息，5=报事信息，
            //  6 =投诉信息，7=委托信息，8=邮包信息，9=拜访信息，10=欠费信息，11=预交信息,12=办卡信息，
            if (!row.Table.Columns.Contains("CustID") || !int.TryParse(row["Type"].ToString(), out int Type))
            {
                Type = 0;
            }
            #region 获取分页参数
            if (!row.Table.Columns.Contains("Page") || !int.TryParse(row["Page"].ToString(), out int Page))
            {
                Page = 1;
            }
            if (Page <= 0)
            {
                Page = 1;
            }
            if (!row.Table.Columns.Contains("Size") || !int.TryParse(row["Size"].ToString(), out int Size))
            {
                Size = 10;
            }
            if (Size <= 0)
            {
                Size = 10;
            }
            // 规则page*size +1 , (page + 1)*size，page 从0开始
            int Start = (Page - 1) * Size + 1;
            int End = Page * Size;
            #endregion
            #endregion

            switch (Type)
            {
                case 1:
                    return GetCustomerType1(CustID, CommID, Start, End, Size);
                case 2:
                    return GetCustomerType2(CustID, CommID, Start, End, Size);
                case 3:
                    return GetCustomerType3(CustID, CommID, Start, End, Size);
                case 4:
                    return GetCustomerType4(CustID, CommID, Start, End, Size);
                case 5:
                    return GetCustomerType5(CustID, CommID, Start, End, Size);
                case 6:
                    return GetCustomerType6(CustID, CommID, Start, End, Size);
                case 7:
                    return GetCustomerType7(CustID, CommID, Start, End, Size);
                case 8:
                    return GetCustomerType8(CustID, CommID, Start, End, Size);
                case 9:
                    return GetCustomerType9(CustID, CommID, Page, Size);
                default: // 默认0走default分支
                    return GetCustomerType0(CommID, CustID);
            }
        }
        #region 获取客户信息模块
        private string GetCustomerType0(string CommID, string CustID)
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT CustID,CustName,Sex,Nationality,PaperCode,Birthday,WorkUnit,
                                (SELECT CustTypeName FROM view_HSPR_CustType_Filter WHERE CustTypeID=a.CustTypeID) AS CustTypeName,
                                (SELECT DictionaryName FROM Tb_Dictionary_PaperName WHERE DictionaryCode=a.PaperName) AS PaperName,
                                (SELECT DictionaryName FROM Tb_Dictionary_Job WHERE DictionaryCode=a.Job) AS Job,
                                FixedTel,MobilePhone,Address,PostCode,Recipient,Memo
                            FROM Tb_HSPR_Customer a WHERE a.CommID=@CommID AND a.CustID=@CustID";

                var custInfo = conn.Query(sql, new { CommID, CustID }).FirstOrDefault();
                if (custInfo == null)
                {
                    return new ApiResult(false, "不存在该业主信息").toJson();
                }

                sql = @"SELECT CustInterests FROM Tb_HSPR_CustomerInterests WHERE CommID = @CommID AND CustID = @CustID";
                custInfo.Interests = conn.Query<string>(sql, new { CommID, CustID });
                return new ApiResult(true, custInfo).toJson();
            }
        }

        /// <summary>
        /// 房屋信息
        /// </summary>
        private string GetCustomerType1(string CustID, string CommID, int Start, int End, int Size)
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT COUNT(1) FROM view_HSPR_CustomerLive_Filter a 
                            WHERE (LiveType=1 OR LiveType=2) AND ISNULL(IsDelLive, 0) = 0 AND ISNULL(IsDelete, 0) = 0 
	                        AND CommID=@CommID AND CustID=@CustID";

                long count = conn.QueryFirstOrDefault<long>(sql, new { CommID, CustID });
                long pageRes = count % Size > 0 ? (count / Size) + 1 : count / Size;
                long countRes = count;

                sql = @"SELECT RoomSign,RoomName,IsSale,IsDebts,BuildArea,CalcArea,GardenArea,CommonArea,YardArea,
                               InteriorArea,PropertyArea,PayBeginDate,LiveState,RoomFittingTime,RoomTypeName,RoomStayTime,
                               RoomModel,PayStateName,ActualSubDate
                        FROM
                        (
                            SELECT *,ROW_NUMBER () OVER ( ORDER BY CustID DESC ) AS RowId 
                            FROM view_HSPR_CustomerLive_Filter a 
                            WHERE (LiveType=1 OR LiveType=2)
                            AND ISNULL(IsDelLive, 0)=0 AND ISNULL(IsDelete, 0)=0 AND CommID=@CommID AND CustID=@CustID
                        ) AS a WHERE
                        RowId BETWEEN @Start AND @END";

                var roomList = conn.Query(sql, new { CommID = CommID, CustID = CustID, Start = Start, End = End });

                return new ApiPageResult(true, roomList, pageRes, countRes).toJson();
            }
        }

        /// <summary>
        /// 车位信息
        /// </summary>
        private string GetCustomerType2(string CustID, string CommID, int Start, int End, int Size)
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT COUNT(1) FROM Tb_HSPR_Parking a 
                            WHERE ISNULL( IsDelete, 0 )=0 AND CommID=@CommID AND CustID=@CustID";

                long count = conn.QueryFirstOrDefault<long>(sql, new { CommID, CustID });
                long pageRes = count % Size > 0 ? (count / Size) + 1 : count / Size;
                long countRes = count;

                sql = @"SELECT CarParkID,CarparkName,ParkName,ParkType,ParkArea,UseState,PropertyUses,StanName,
                               ChargeCycleName,CustName,RoomSign,ParkStartDate,ParkEndDate,ParkingCarSign,CarSign AS CarNum 
                        FROM
                         (
                            SELECT *, ROW_NUMBER () OVER (ORDER BY CustID DESC) AS RowId
                            FROM view_HSPR_ParkingSel_Filter a
                            WHERE ISNULL(IsDelete, 0)=0 AND CommID=@CommID AND CustID=@CustID
                        ) AS a WHERE
                        RowId BETWEEN @Start AND @END;";

                var parkingList = conn.Query(sql, new { CommID = CommID, CustID = CustID, Start = Start, End = End });

                return new ApiPageResult(true, parkingList, pageRes, countRes).toJson();
            }
        }

        /// <summary>
        /// 家庭信息
        /// </summary>
        private string GetCustomerType3(string CustID, string CommID, int Start, int End, int Size)
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT COUNT(1) FROM Tb_HSPR_Household a 
                            WHERE ISNULL(IsDelete, 0)=0 AND CommID=@CommID AND CustID=@CustID";

                long count = conn.QueryFirstOrDefault<long>(sql, new { CommID = CommID, CustID = CustID });
                long pageRes = count % Size > 0 ? (count / Size) + 1 : count / Size;
                long countRes = count;

                sql = @"SELECT * FROM
                        (
                            SELECT *,ROW_NUMBER () OVER ( ORDER BY CustID DESC ) AS RowId 
                            FROM view_HSPR_Household_Filter a 
                            WHERE ISNULL( IsDelete, 0)=0 AND CommID=@CommID AND CustID=@CustID 
	                    ) AS a WHERE
	                    RowId BETWEEN @Start AND @END";

                var familyList = conn.Query(sql, new { CommID = CommID, CustID = CustID, Start = Start, End = End });

                sql = @"SELECT HouseholdInterests FROM Tb_HSPR_HouseholdInterests WHERE HouseholdId=@HouseholdId";
                foreach (dynamic item in familyList)
                {
                    item.Interests = conn.Query<string>(sql, new { HouseholdId = item.HoldID });
                }

                return new ApiPageResult(true, familyList, pageRes, countRes).toJson();
            }
        }

        /// <summary>
        /// 装修信息
        /// </summary>
        private string GetCustomerType4(string CustID, string CommID, int Start, int End, int Size)
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT COUNT(1) FROM Tb_Reno_Renovation a 
                            WHERE ISNULL(a.IsDelete,0)=0 AND a.CommID=@CommID AND a.CustID=@CustID";

                long count = conn.QueryFirstOrDefault<long>(sql, new { CommID, CustID });
                long pageRes = count % Size > 0 ? (count / Size) + 1 : count / Size;
                long countRes = count;

                sql = @"SELECT * FROM
                        (
                            SELECT a.StartDate,a.EstimateEndDate,a.ActualCompleteDate,a.RenovationState,a.BuildCompany,a.HandleCertificateCount,
                                   (SELECT RoomSign FROM Tb_HSPR_Room WHERE RoomID=a.RoomID) AS RoomSign,
                                   (SELECT ISNULL(SUM(ISNULL(DueAmount,0.00)),0.00) FROM View_Tb_Reno_Fees 
                                        WHERE ISNULL(IsDelete,0)=0 AND RID=a.ID) AS ChargeAmount,
                                   (SELECT COUNT(ID) FROM Tb_Reno_Patrol 
                                        WHERE ISNULL(IsDelete,0)=0 AND RectificationSingle='是' AND RID=a.ID) AS RectificationSingleCount,
                                   ROW_NUMBER() OVER(ORDER BY a.StartDate DESC) AS RowId
                            FROM View_Tb_Reno_Renovation a WHERE ISNULL(a.IsDelete,0) = 0 AND a.CommID=@CommID AND CustID=@CustID
                        ) AS a WHERE
                        RowId BETWEEN @Start AND @End";

                var list = conn.Query(sql, new { CommID, CustID, Start, End }).ToList();

                return new ApiPageResult(true, list, pageRes, countRes).toJson();
            }
        }

        /// <summary>
        /// 报事信息
        /// </summary>
        private string GetCustomerType5(string CustID, string CommID, int Start, int End, int Size, int IsTousu = -1)
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT COUNT(1) FROM Tb_HSPR_IncidentAccept a 
                            WHERE ISNULL( IsDelete, 0 )=0 AND CommID=@CommID AND CustID=@CustID";

                long count = conn.QueryFirstOrDefault<long>(sql, new { CommID, CustID });
                long pageRes = count % Size > 0 ? (count / Size) + 1 : count / Size;
                long countRes = count;

                var chargeConnString = Global_Fun.BurstConnectionString(AppGlobal.StrToInt(CommID), Global_Fun.BURST_TYPE_CHARGE);
                using (var chargeConn = new SqlConnection(chargeConnString))
                {
                    sql = $@"SELECT * FROM
                            (
                                SELECT *,ROW_NUMBER () OVER ( ORDER BY IncidentDate DESC ) AS RowId
                                FROM
                                (
                                    SELECT x.CommID,x.CustID,x.IncidentNum,x.RoomSign,x.IncidentPlace,x.IncidentContent,
                                        x.IncidentDate,x.DealMan,x.DealSituation,x.IsDelete,x.IsTouSu,
                                        (SELECT isnull(sum(isnull(y.DueAmount,0)),0) FROM Tb_HSPR_Fees y
                                            WHERE isnull(IsDelete,0)=0 AND y.IncidentID=x.IncidentID) AS ChargeAmount,
                                        (SELECT TOP 1 CASE y.ReplyResult WHEN 1 THEN '成功回访' ELSE '不成功回访' END
                                            FROM {Global_Var.ERPDatabaseName}.dbo.Tb_HSPR_IncidentReply y
                                            WHERE y.IncidentID=x.IncidentID ORDER BY y.ReplyResult DESC) AS ReplyResult,
                                        (SELECT TOP 1 y.ServiceQuality FROM {Global_Var.ERPDatabaseName}.dbo.Tb_HSPR_IncidentReply y
                                            WHERE y.IncidentID=x.IncidentID ORDER BY y.ReplyResult DESC) AS ServiceQuality,
                                        (SELECT TOP 1 y.ReplyContent FROM {Global_Var.ERPDatabaseName}.dbo.Tb_HSPR_IncidentReply y
                                            WHERE y.IncidentID=x.IncidentID ORDER BY y.ReplyResult DESC) AS ReplyContent,
                                        (SELECT y.TypeName FROM {Global_Var.ERPDatabaseName}.dbo.Tb_HSPR_CorpIncidentType y
                                            WHERE y.CorpTypeID=isnull(x.FineCorpTypeID,x.BigCorpTypeID)) AS TypeName
                                    FROM {Global_Var.ERPDatabaseName}.dbo.view_HSPR_IncidentAccept_Filter x
                                ) a
                                WHERE ISNULL(IsDelete, 0)=0 AND CommID=@CommID AND CustID=@CustID {(IsTousu == 1 ? " AND isnull(IsTousu,0)=1" : "")}
                            ) AS a WHERE
                            RowId BETWEEN @Start AND @END;";

                    var IncidentList = chargeConn.Query(sql, new { CommID = CommID, CustID = CustID, Start = Start, End = End });

                    return new ApiPageResult(true, IncidentList, pageRes, countRes).toJson();
                }
            }
        }

        /// <summary>
        /// 投诉信息
        /// </summary>
        private string GetCustomerType6(string CustID, string CommID, int Start, int End, int Size)
        {
            return GetCustomerType5(CustID, CommID, Start, End, Size, 1);
        }

        /// <summary>
        /// 办卡信息
        /// </summary>
        private string GetCustomerType7(string CustID, string CommID, int Start, int End, int Size)
        {

            if (Global_Var.LoginCorpID == "6008")
            {
                using (var conn = new SqlConnection(Global_Fun.BurstConnectionString(AppGlobal.StrToInt(CommID), Global_Fun.BURST_TYPE_CHARGE)))
                {
                    string countSql = @"SELECT COUNT(*) FROM View_HSPR_CustCard_Filter a WHERE
	                                                ISNULL( IsDelete, 0 ) = 0 
	                                                AND CommID = @CommID 
	                                                AND CustID = @CustID";
                    long count = conn.QueryFirstOrDefault<long>(countSql, new { CommID, CustID });
                    long pageRes = count % Size > 0 ? (count / Size) + 1 : count / Size;
                    long countRes = count;

                    List<dynamic> roomList = conn.Query(@"SELECT CustName,CyRoomSign,CardDate,CardState,CardNum
                                                    FROM(SELECT *,ROW_NUMBER () OVER ( ORDER BY CardNum DESC ) AS RowId FROM
	                                                View_HSPR_CustCard_Filter a WHERE
	                                                ISNULL( IsDelete, 0 ) = 0 
	                                                AND CommID = @CommID 
	                                                AND CustID = @CustID 
	                                                ) AS a WHERE
	                                                RowId BETWEEN @Start 
	                                                AND @END", new
                    {
                        CommID = CommID,
                        CustID = CustID,
                        Start = Start,
                        End = End
                    }).ToList();

                    return new ApiPageResult(true, roomList, pageRes, countRes).toJson();
                }
            }
            else
            {
                using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    string countSql = @"SELECT COUNT(*) FROM View_HSPR_CustCard_Filter a WHERE
	                                                ISNULL( IsDelete, 0 ) = 0 
	                                                AND CommID = @CommID 
	                                                AND CustID = @CustID";
                    long count = conn.QueryFirstOrDefault<long>(countSql, new { CommID, CustID });
                    long pageRes = count % Size > 0 ? (count / Size) + 1 : count / Size;
                    long countRes = count;

                    List<dynamic> roomList = conn.Query(@"SELECT CustName,CyRoomSign,CardDate,CardState,CardNum
                                                    FROM(SELECT *,ROW_NUMBER () OVER ( ORDER BY CardNum DESC ) AS RowId FROM
	                                                View_HSPR_CustCard_Filter a WHERE
	                                                ISNULL( IsDelete, 0 ) = 0 
	                                                AND CommID = @CommID 
	                                                AND CustID = @CustID 
	                                                ) AS a WHERE
	                                                RowId BETWEEN @Start 
	                                                AND @END", new
                    {
                        CommID = CommID,
                        CustID = CustID,
                        Start = Start,
                        End = End
                    }).ToList();

                    return new ApiPageResult(true, roomList, pageRes, countRes).toJson();
                }
            }



        }

        /// <summary>
        /// 预缴信息
        /// </summary>
        private string GetCustomerType8(string CustID, string CommID, int Start, int End, int Size)
        {
            var chargeConnString = Global_Fun.BurstConnectionString(AppGlobal.StrToInt(CommID), Global_Fun.BURST_TYPE_CHARGE);
            using (var conn = new SqlConnection(chargeConnString))
            {
                var sql = @"SELECT COUNT(*) FROM Tb_HSPR_PreCostsDetail a 
                            WHERE PrecAmount>0 AND ISNULL(IsDelete,0)=0 AND CommID=@CommID AND CustID=@CustID";

                long count = conn.QueryFirstOrDefault<long>(sql, new { CommID, CustID });
                long pageRes = count % Size > 0 ? (count / Size) + 1 : count / Size;
                long countRes = count;

                var FeesList = conn.Query(@"SELECT CostName,RoomSign,PrecAmount,PrecMemo,ParkNames,isnull(NewPrecAmount,0) AS NewPrecAmount
                                            FROM(SELECT *,ROW_NUMBER () OVER ( ORDER BY CustID DESC ) AS RowId FROM
	                                        view_HSPR_PreCostsDetail_Filter a 
                                            WHERE PrecAmount>0 
											AND ISNULL(IsDelete,0)=0
	                                        AND CommID = @CommID 
	                                        AND CustID = @CustID 
	                                        ) AS a WHERE
	                                        RowId BETWEEN @Start 
	                                        AND @END", new
                {
                    CommID = CommID,
                    CustID = CustID,
                    Start = Start,
                    End = End
                }).ToList();

                return new ApiPageResult(true, FeesList, pageRes, countRes).toJson();
            }
        }

        /// <summary>
        /// 拜访信息
        /// </summary>
        private string GetCustomerType9(string CustID, string CommID, int Page, int Size)
        {
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = $@"SELECT a.ID,b.PlanName,a.RoomSign,a.RoomName,c.UserName AS VisitUserName,CompleteTime,a.QuestionnaireRealScore,VisitSummary
                            FROM Tb_Visit_VisitingCustomersDetail a
                            LEFT JOIN Tb_Visit_Plan b ON a.PlanID=b.ID
                            LEFT JOIN Tb_Sys_User c ON a.VisitUserCode=c.UserCode
                            WHERE a.CustID={CustID} AND isnull(a.CompleteTime,'')<>'' AND isnull(a.IsDelete,0)=0";

                var result = GetListDapper(out int pageCount, out int count, sql, Page, Size, "CompleteTime", 1, "ID", conn);

                return new ApiPageResult(true, result, pageCount, count).toJson();
            }
        }
        #endregion

        /// <summary>
        /// 客户查询
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetCustomerList(DataRow row)
        {
            #region 获取参数
            string CommID = string.Empty;
            if (row.Table.Columns.Contains("CommID"))
            {
                CommID = row["CommID"].ToString();
            }
            if (string.IsNullOrEmpty(CommID))
            {
                return new ApiResult(false, "请先切换到具体小区").toJson();
            }
            #region 获取分页参数
            if (!row.Table.Columns.Contains("Page") || !int.TryParse(row["Page"].ToString(), out int Page))
            {
                Page = 1;
            }
            if (Page <= 0)
            {
                Page = 1;
            }
            if (!row.Table.Columns.Contains("Size") || !int.TryParse(row["Size"].ToString(), out int Size))
            {
                Size = 10;
            }
            if (Size <= 0)
            {
                Size = 10;
            }
            // 规则page*size +1 , (page + 1)*size，page 从0开始
            int Start = (Page - 1) * Size + 1;
            int End = (Page + 1) * Size;
            #endregion
            #region 获取分类查询参数
            if (!row.Table.Columns.Contains("IsOwe") || !int.TryParse(row["IsOwe"].ToString(), out int IsOwe))
            {
                IsOwe = 0;
            }
            if (!row.Table.Columns.Contains("IsIncident") || !int.TryParse(row["IsIncident"].ToString(), out int IsIncident))
            {
                IsIncident = 0;
            }
            if (!row.Table.Columns.Contains("IsTousu") || !int.TryParse(row["IsTousu"].ToString(), out int IsTousu))
            {
                IsTousu = 0;
            }
            if (!row.Table.Columns.Contains("IsDecorate") || !int.TryParse(row["IsDecorate"].ToString(), out int IsDecorate))
            {
                IsDecorate = 0;
            }
            #endregion
            #region 获取精准查询参数
            string CustID = string.Empty;
            if (row.Table.Columns.Contains("CustID"))
            {
                CustID = row["CustID"].ToString();
            }
            string RoomID = string.Empty;
            if (row.Table.Columns.Contains("RoomID"))
            {
                RoomID = row["RoomID"].ToString();
            }
            string Mobile = string.Empty;
            if (row.Table.Columns.Contains("Mobile"))
            {
                Mobile = row["Mobile"].ToString();
            }
            string CarparkID = string.Empty;
            if (row.Table.Columns.Contains("CarparkID"))
            {
                CarparkID = row["CarparkID"].ToString();
            }
            #endregion  
            #endregion

            using (var erpConn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var chargeConnString = Global_Fun.BurstConnectionString(AppGlobal.StrToInt(CommID), Global_Fun.BURST_TYPE_CHARGE);
                using (var chargeConn = new SqlConnection(chargeConnString))
                {
                    string OrderBySql = "ORDER BY OweCount DESC,IncidentCount DESC,TousuCount DESC,RenoCount DESC,CustID DESC";
                    string WhereSql = string.Empty;
                    // 分类查询
                    if (string.IsNullOrEmpty(CustID) && string.IsNullOrEmpty(RoomID) && string.IsNullOrEmpty(Mobile) && string.IsNullOrEmpty(CarparkID))
                    {
                        WhereSql += " WHERE ISNULL(a.IsDelete,0) = 0 AND a.CommID = @CommID";
                        OrderBySql = "ORDER BY ";
                        if (IsOwe != 0)
                        {
                            WhereSql += " AND a.OweCount > 0";
                            OrderBySql += " OweCount DESC,";
                        }
                        if (IsIncident != 0)
                        {
                            WhereSql += " AND a.IncidentCount > 0";
                            OrderBySql += " IncidentCount DESC,";
                        }
                        if (IsTousu != 0)
                        {
                            WhereSql += " AND a.TousuCount > 0";
                            OrderBySql += " TousuCount DESC,";
                        }
                        if (IsDecorate != 0)
                        {
                            WhereSql += " AND a.RenoCount > 0";
                            OrderBySql += " RenoCount DESC,";
                        }
                        OrderBySql += "CustID DESC";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(CustID))
                        {
                            WhereSql += " WHERE ISNULL(a.IsDelete,0) = 0 AND a.CommID = @CommID AND a.CustID = @CustID";
                        }
                        else if (!string.IsNullOrEmpty(RoomID))
                        {
                            CustID = erpConn.QueryFirstOrDefault<string>("SELECT CustID FROM view_HSPR_CustomerLive_Filter WHERE (LiveType = 1 OR LiveType = 2) AND ISNULL(IsDelLive,0) = 0 AND ISNULL(IsDelete,0) = 0 AND CommID = @CommID AND RoomID = @RoomID", new { CommID, RoomID });
                            WhereSql += " WHERE ISNULL(a.IsDelete,0) = 0 AND a.CommID = @CommID AND a.CustID = @CustID";
                        }
                        else if (!string.IsNullOrEmpty(Mobile))
                        {
                            WhereSql += " WHERE ISNULL(a.IsDelete,0) = 0 AND a.CommID = @CommID AND a.MobilePhone = @Mobile";
                        }
                        else if (!string.IsNullOrEmpty(CarparkID))
                        {
                            CustID = erpConn.QueryFirstOrDefault<string>("SELECT CustID FROM Tb_HSPR_Parking WHERE ISNULL(IsDelete,0) = 0 AND CommID = @CommID AND ParkID = @CarparkID", new { CommID, CarparkID });
                            WhereSql += " WHERE ISNULL(a.IsDelete,0) = 0 AND a.CommID = @CommID AND a.CustID = @CustID";
                        }
                        else
                        {
                            WhereSql += " WHERE ISNULL(a.IsDelete,0) = 0 AND a.CommID = @CommID";
                        }
                    }

                    var sql = $@"SELECT COUNT(CustID) FROM (SELECT 
                            (
	                            SELECT ISNULL(SUM(DebtsAmount),0.00) + ISNULL(SUM(LateFeeAmount),0.00) 
                                FROM view_HSPR_Fees_SearchFilter 
	                            WHERE DebtsAmount>0 AND ISNULL(IsCharge,0)=0 AND ISNULL(IsBank,0)=0 AND ISNULL(IsPrec,0)=0 
	                            AND ISNULL(IsFreeze,0)=0 AND CommID=a.CommID AND CustID=a.CustID
                            ) AS OweCount,	
                            (
	                            SELECT COUNT(IncidentID) FROM {Global_Var.ERPDatabaseName}.dbo.Tb_HSPR_IncidentAccept 
	                            WHERE ISNULL(IsDelete,0)=0 AND ISNULL(DealState,0)!=1 AND CommID=a.CommID AND CustID=a.CustID
                            ) AS IncidentCount,
                            (
	                            SELECT COUNT(IncidentID) FROM {Global_Var.ERPDatabaseName}.dbo.Tb_HSPR_IncidentAccept 
	                            WHERE ISNULL(IsDelete, 0)=0 AND ISNULL(DealState,0)!=1 AND ISNULL(IsTousu,0)=1 AND CommID=a.CommID AND CustID = a.CustID
                            ) AS TousuCount,
                            (
	                            SELECT COUNT(ID) FROM {Global_Var.ERPDatabaseName}.dbo.Tb_Reno_Renovation 
	                            WHERE ISNULL(IsDelete, 0)=0 AND RenovationState!='完工' AND CommID=a.CommID AND CustID=a.CustID
                            ) AS RenoCount,
                            * FROM {Global_Var.ERPDatabaseName}.dbo.Tb_HSPR_Customer a) as a {WhereSql}";

                    long count = chargeConn.QueryFirstOrDefault<long>(sql, new { CommID, CustID, RoomID, Mobile, CarparkID });
                    long pageRes = count % Size > 0 ? (count / Size) + 1 : count / Size;
                    long countRes = count;

                    sql = $@"SELECT * FROM (SELECT a.*,ROW_NUMBER() OVER({OrderBySql}) AS RowId FROM (SELECT 
                            (
	                            SELECT ISNULL(SUM(DebtsAmount),0.00) + ISNULL(SUM(LateFeeAmount),0.00) 
                                FROM view_HSPR_Fees_SearchFilter 
	                            WHERE DebtsAmount>0 AND ISNULL(IsCharge,0)=0 AND ISNULL(IsBank,0)=0 AND ISNULL(IsPrec,0)=0 
	                            AND ISNULL(IsFreeze,0)=0 AND CommID=a.CommID AND CustID=a.CustID
                            ) AS OweCount,	
                            (
	                            SELECT COUNT(IncidentID) FROM {Global_Var.ERPDatabaseName}.dbo.Tb_HSPR_IncidentAccept 
	                            WHERE ISNULL(IsDelete,0)=0 AND ISNULL(DealState,0)!=1 AND CommID=a.CommID AND CustID=a.CustID
                            ) AS IncidentCount,
                            (
	                            SELECT COUNT(IncidentID) FROM {Global_Var.ERPDatabaseName}.dbo.Tb_HSPR_IncidentAccept 
	                            WHERE ISNULL(IsDelete, 0)=0 AND ISNULL(DealState,0)!=1 AND ISNULL(IsTousu,0)=1 AND CommID=a.CommID AND CustID = a.CustID
                            ) AS TousuCount,
                            (
	                            SELECT COUNT(ID) FROM {Global_Var.ERPDatabaseName}.dbo.Tb_Reno_Renovation 
	                            WHERE ISNULL(IsDelete, 0)=0 AND RenovationState!='完工' AND CommID=a.CommID AND CustID=a.CustID
                            ) AS RenoCount,
                            * FROM {Global_Var.ERPDatabaseName}.dbo.Tb_HSPR_Customer a) as a {WhereSql}) AS a 
                            WHERE RowId BETWEEN @Start AND @End";
                    List<dynamic> list = chargeConn.Query(sql, new { CommID, CustID, RoomID, Mobile, CarparkID, Start, End }).ToList();
                    if (null == list)
                    {
                        list = new List<dynamic>();
                    }
                    List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();
                    foreach (dynamic item in list)
                    {
                        var dic = new Dictionary<string, object>();
                        dic.Add("CustID", Convert.ToString(item.CustID));
                        dic.Add("CustName", Convert.ToString(item.CustName));
                        dic.Add("MobilePhone", Convert.ToString(item.MobilePhone));

                        #region 获取客户房产列表
                        List<dynamic> RoomList = erpConn.Query("SELECT RoomID,RoomSign,RoomName FROM view_HSPR_CustomerLive_Filter WHERE (LiveType = 1 OR LiveType = 2) AND ISNULL(IsDelLive,0) = 0 AND ISNULL(IsDelete,0) = 0 AND CommID = @CommID AND CustID = @CustID", new { CommID = item.CommID, CustID = item.CustID }).ToList();
                        if (null == RoomList)
                        {
                            RoomList = new List<dynamic>();
                        }
                        dic.Add("RoomList", RoomList);
                        #endregion
                        #region 获取客户欠费总笔数
                        long OweCount = chargeConn.QueryFirstOrDefault<long>("SELECT COUNT(FeesID) FROM view_HSPR_Fees_SearchFilter WHERE DebtsAmount > 0 AND ISNULL(IsCharge, 0) = 0 AND ISNULL(IsBank, 0) = 0 AND ISNULL(IsPrec, 0) = 0 AND ISNULL(IsFreeze, 0) = 0 AND CommID = @CommID AND CustID = @CustID", new { CommID = item.CommID, CustID = item.CustID });
                        if (OweCount < 0)
                        {
                            OweCount = 0;
                        }
                        dic.Add("OweCount", OweCount);
                        #endregion
                        #region 获取客户欠费总金额
                        decimal OweAmount = chargeConn.QueryFirstOrDefault<decimal>("SELECT ISNULL(SUM(DebtsAmount),0.00) + ISNULL(SUM(LateFeeAmount),0.00) FROM view_HSPR_Fees_SearchFilter WHERE DebtsAmount > 0 AND ISNULL(IsCharge, 0) = 0 AND ISNULL(IsBank, 0) = 0 AND ISNULL(IsPrec, 0) = 0 AND ISNULL(IsFreeze, 0) = 0 AND CommID = @CommID AND CustID = @CustID", new { CommID = item.CommID, CustID = item.CustID });
                        if (OweAmount < 0.00M)
                        {
                            OweAmount = 0.00M;
                        }
                        dic.Add("OweAmount", OweAmount);
                        #endregion
                        #region 获取客户欠费账龄
                        long OweMonth = chargeConn.QueryFirstOrDefault<long>("SELECT ISNULL(DATEDIFF(mm,MIN(FeesStateDate),MAX(FeesEndDate)),0) FROM view_HSPR_Fees_SearchFilter WHERE DebtsAmount > 0 AND ISNULL(IsCharge, 0) = 0 AND ISNULL(IsBank, 0) = 0 AND ISNULL(IsPrec, 0) = 0 AND ISNULL(IsFreeze, 0) = 0 AND CommID = @CommID AND CustID = @CustID", new { CommID = item.CommID, CustID = item.CustID });
                        if (OweMonth < 0)
                        {
                            OweMonth = 0;
                        }
                        dic.Add("OweMonth", OweMonth);
                        #endregion
                        #region 获取客户预交余额
                        decimal PrecAmount = chargeConn.QueryFirstOrDefault<decimal>("SELECT ISNULL(SUM(PrecAmount),0.00) FROM view_HSPR_PreCosts_Filter WHERE PrecAmount > 0 AND CommID = @CommID AND CustID = @CustID", new { CommID = item.CommID, CustID = item.CustID });
                        if (PrecAmount < 0.00M)
                        {
                            PrecAmount = 0.00M;
                        }
                        dic.Add("PrecAmount", PrecAmount);
                        #endregion
                        #region 获取客户报事数量
                        dic.Add("IncidentCount", item.IncidentCount);
                        #endregion
                        #region 获取客户投诉报事数量
                        dic.Add("TousuCount", item.TousuCount);
                        #endregion
                        #region 获取客户投诉报事数量
                        dynamic RenoInfo = erpConn.QueryFirstOrDefault<dynamic>("SELECT MIN(StartDate) AS RenoStartDate,MAX(EstimateEndDate) AS RenoEndDate FROM Tb_Reno_Renovation WHERE ISNULL(IsDelete, 0) = 0 AND RenovationState != '完工' AND CommID = @CommID AND CustID = @CustID", new { CommID = item.CommID, CustID = item.CustID });
                        string RenoStartDate = string.Empty;
                        string RenoEndDate = string.Empty;
                        if (null != RenoInfo)
                        {
                            RenoStartDate = Convert.ToString(RenoInfo.RenoStartDate);
                            RenoEndDate = Convert.ToString(RenoInfo.RenoEndDate);
                        }
                        dic.Add("RenoStartDate", RenoStartDate);
                        dic.Add("RenoEndDate", RenoEndDate);
                        #endregion
                        resultList.Add(dic);
                    }
                    return new ApiPageResult(true, resultList, pageRes, countRes).toJson();
                }
            }
        }

        /// <summary>
        /// 模糊查询业主姓名
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string FuzzySearchCustName(DataRow row)
        {
            #region 获取参数
            string CommID = string.Empty;
            if (row.Table.Columns.Contains("CommID"))
            {
                CommID = row["CommID"].ToString();
            }
            string CustName = string.Empty;
            if (row.Table.Columns.Contains("CustName"))
            {
                CustName = row["CustName"].ToString();
            }
            #endregion

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = @"SELECT CustID, CustName, ISNULL(MobilePhone,'') AS MobilePhone FROM Tb_HSPR_Customer 
                            WHERE CommID = @CommID AND ISNULL(IsDelete, 0)=0 AND CustName LIKE @CustName ";
                List<dynamic> list = conn.Query(sql, new { CommID, CustName = $"%{CustName}%" }).ToList();
                if (null == list)
                {
                    list = new List<dynamic>();
                }
                return new ApiResult(true, list).toJson();
            }
        }

        /// <summary>
        /// 模糊查询房屋编号
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string FuzzySearchRoomSign(DataRow row)
        {
            #region 获取参数
            string CommID = string.Empty;
            if (row.Table.Columns.Contains("CommID"))
            {
                CommID = row["CommID"].ToString();
            }
            string RoomSign = string.Empty;
            if (row.Table.Columns.Contains("RoomSign"))
            {
                RoomSign = row["RoomSign"].ToString();
            }
            #endregion
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT RoomID, RoomName, RoomSign FROM Tb_HSPR_Room
                            WHERE CommID = @CommID AND ISNULL(IsDelete, 0) = 0 AND (RoomSign LIKE @RoomSign OR RoomName LIKE @RoomSign)";
                List<dynamic> list = conn.Query(sql, new { CommID, RoomSign = $"%{RoomSign}%" }).ToList();
                if (null == list)
                {
                    list = new List<dynamic>();
                }
                return new ApiResult(true, list).toJson();
            }
        }

        /// <summary>
        /// 模糊查询业主电话
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string FuzzySearchMobile(DataRow row)
        {
            #region 获取参数
            string CommID = string.Empty;
            if (row.Table.Columns.Contains("CommID"))
            {
                CommID = row["CommID"].ToString();
            }
            string Mobile = string.Empty;
            if (row.Table.Columns.Contains("Mobile"))
            {
                Mobile = row["Mobile"].ToString();
            }
            #endregion

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = @"SELECT CustID, CustName, ISNULL(MobilePhone,'') AS MobilePhone FROM Tb_HSPR_Customer 
                            WHERE CommID = @CommID AND ISNULL(IsDelete, 0) = 0 AND MobilePhone LIKE @MobilePhone ";
                List<dynamic> list = conn.Query(sql, new { CommID, MobilePhone = $"%{Mobile}%" }).ToList();
                if (null == list)
                {
                    list = new List<dynamic>();
                }
                return new ApiResult(true, list).toJson();
            }
        }

        /// <summary>
        /// 模糊查询车位
        /// </summary>
        private string FuzzySearchParking(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var parkName = default(string);

            if (row.Table.Columns.Contains("ParkName"))
            {
                parkName = row["ParkName"].ToString();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT ParkID,ParkName,ParkMemo FROM Tb_HSPR_Parking WHERE CommID=@CommID";
                if (!string.IsNullOrEmpty(parkName))
                {
                    sql += " AND ParkName LIKE @ParkName";
                }

                var list = conn.Query(sql, new { CommID = commId, ParkName = $"%{parkName}%" }).ToList();

                return new ApiResult(true, list).toJson();
            }
        }

        /// <summary>
        /// 模糊查询车位空间
        /// </summary>
        private string FuzzySearchCarpark(DataRow row)
        {
            #region 获取参数
            string CommID = string.Empty;
            if (row.Table.Columns.Contains("CommID"))
            {
                CommID = row["CommID"].ToString();
            }
            string CarparkName = string.Empty;
            if (row.Table.Columns.Contains("CarparkName"))
            {
                CarparkName = row["CarparkName"].ToString();
            }
            #endregion
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = @"SELECT CarparkID, CarparkName, CarparkPosition FROM Tb_HSPR_Carpark
                            WHERE CommID = @CommID AND ISNULL(IsDelete, 0) = 0 AND CarparkName LIKE @CarparkName";

                List<dynamic> list = conn.Query(sql, new { CommID, CarparkName = $"%{CarparkName}%" }).ToList();
                if (null == list)
                {
                    list = new List<dynamic>();
                }
                return new ApiResult(true, list).toJson();
            }
        }
    }
}
