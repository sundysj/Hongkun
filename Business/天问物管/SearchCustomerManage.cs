using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
namespace Business
{
    public class SearchCustomerManage : PubInfo
    {
        public SearchCustomerManage()
        {
            base.Token = "20170328SearchCustomerManage";
        }

        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            //验证登录
            if (!new Login().isLogin(ref Trans))
                return;

            switch (Trans.Command)
            {

                #region 基础信息

                case "GetCustomerBaseInfo"://获取客户基础信息
                    Trans.Result = GetCustomerBaseInfo(Row);
                    break;
                case "GetCustomerRoomList"://获取客户房屋信息
                    Trans.Result = GetCustomerRoomList(Row);
                    break;
                case "GetCustomerRoomList_shidi"://获取客户房屋信息
                    Trans.Result = GetCustomerRoomList_shidi(Row);
                    break;
                case "GetRoomModel"://获取房屋入住状态
                    Trans.Result = GetRoomModel(Row);
                    break;
                case "EditCustomerRoomInfo"://获取房屋入住状态
                    Trans.Result = EditCustomerRoomInfo(Row);
                    break;
                case "GetCustomerRoomList_Num"://获取房屋数量
                    Trans.Result = GetCustomerRoomList_Num(Row);
                    break;
                case "GetCustomerParkInfo"://获取车位信息
                    Trans.Result = GetCustomerParkInfo(Row);
                    break;
                case "GetCustomerParkInfo_Num"://获取车位数量
                    Trans.Result = GetCustomerParkInfo_Num(Row);
                    break;
                case "GetCustomerList_All"://客户分类查询
                    Trans.Result = GetCustomerList_All(Row);
                    break;
                case "GetCustomerList_All_Num"://客户分类查询数量
                    Trans.Result = GetCustomerList_All_Num(Row);
                    break;
                case "GetQueryValue"://模糊查询查询条件
                    Trans.Result = GetQueryValue(Row);
                    break;
                case "GetCustomerList"://精确查询客户
                    Trans.Result = GetCustomerList(Row);
                    break;
                case "GetRenoKeysList"://查询钥匙
                    Trans.Result = GetRenoKeysList(Row);
                    break;
                case "GetRenoKeysList_Num"://钥匙数量
                    Trans.Result = GetRenoKeysList_Num(Row);
                    break;
                case "GetFamilyInfo"://家庭成员
                    Trans.Result = GetFamilyInfo(Row);
                    break;
                case "GetFamilyInfo_Num"://家庭成员数量
                    Trans.Result = GetFamilyInfo_Num(Row);
                    break;
                case "GetCustAccessCar"://办卡信息
                    Trans.Result = GetCustAccessCar(Row);
                    break;
                case "GetCustAccessCar_Num"://办卡信息数量
                    Trans.Result = GetCustAccessCar_Num(Row);
                    break;
                case "GetRenovation"://装修信息
                    Trans.Result = GetRenovation(Row);
                    break;
                case "GetRenovation_Num"://装修信息数量
                    Trans.Result = GetRenovation_Num(Row);
                    break;
                case "GetIncidentList"://报事信息
                    Trans.Result = GetIncidentList(Row);
                    break;
                case "GetIncidentList_Num"://报事信息数量
                    Trans.Result = GetIncidentList_Num(Row);
                    break;
                case "GetEntrustList"://委托信息
                    Trans.Result = GetEntrustList(Row);
                    break;
                case "GetEntrustList_Num"://委托数量
                    Trans.Result = GetEntrustList_Num(Row);
                    break;
                case "GetFeeInfo"://欠费信息
                    Trans.Result = GetFeeInfo(Row);
                    break;
                case "GetFeeCount"://欠费总额/总条数
                    Trans.Result = GetFeeCount(Row);
                    break;
                case "GetPreInfo"://预交信息
                    Trans.Result = GetPreInfo(Row);
                    break;
                case "GetPreInfo_Num"://预交总条数
                    Trans.Result = GetPreInfo_Num(Row);
                    break;
                #endregion


                default:
                    break;
            }

        }

        #region 获取客户基础信息
        /// <summary>
        /// 获取客户基础信息GetCustomerBaseInfo
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// CustID          客户编码
        /// 返回
        /// CustTypeName            客户类别
        /// CustName                客户名称
        /// Sex                     客户性别
        /// PaperCode               证件号码
        /// FixedTel                固定电话
        /// MobilePhone             移动电话
        /// Address                 联系地址
        /// PostCode                邮政编码
        /// EMail                   收件人
        /// <returns></returns>
        private string GetCustomerBaseInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            string strcon = "";
            bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }
            string sql = "select  isnull(CustTypeName,'') as CustTypeName,CustName,isnull( Sex,'') as Sex,PaperCode,FixedTel,MobilePhone,[Address],PostCode,EMail from view_HSPR_Customer_Filter where CommID=" + row["CommID"] + " and CustID=" + row["CustID"];
            IDbConnection con = new SqlConnection(strcon);
            DataTable dt = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet().Tables[0];
            con.Dispose();

            return JSONHelper.FromString(dt);

        }
        #endregion

        #region 获取客户房屋信息
        /// <summary>
        /// 获取客户房屋信息GetCustomerRoomList
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// CustID          客户编码
        /// 返回：
        ///     RoomSign                房屋编号
        ///     IsSale                  是否转让
        ///     UsesState               是否租赁
        ///     BuildArea               建筑面积
        ///     StateName               交房状态
        ///     ActualSubDate           交房时间
        ///     RoomFittingTime         装修时间
        ///     RoomStayTime            入住时间
        ///     PayBeginDate            开始缴费时间
        /// <returns></returns>
        private string GetCustomerRoomList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            string strcon = "";
            bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }
            string sql = "select  RoomSign,IsSale,UsesState,BuildArea,StateName,ActualSubDate,RoomFittingTime,RoomStayTime,PayBeginDate from view_HSPR_CustomerLive_Filter where CommID=" + row["CommID"] + " and CustID=" + row["CustID"];
            IDbConnection con = new SqlConnection(strcon);
            DataTable dt = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet().Tables[0];
            con.Dispose();

            return JSONHelper.FromString(dt);
        }
        private string GetCustomerRoomList_shidi(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            string strcon = "";
            bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }
            string sql = $@"SELECT  
                            A.RoomSign,
							A.RoomID,
                            A.BuildArea,
                            B.DictionaryName AS RoomModelName,
                            B.DictionaryCode AS RoomModelCode,
                            A.ActualSubDate,
                            A.RoomFittingTime,
                            A.RoomStayTime,
							d.StateName AS LiveState,
                            d.RoomState 
                            FROM View_HSPR_CustomerLive_Filter A 
							LEFT JOIN Tb_Dictionary_RoomModel B ON A.RoomModel = B.DictionaryCode 
                            LEFT JOIN Tb_HSPR_Room c ON a.RoomID=c.RoomID
                            LEFT JOIN Tb_HSPR_RoomState d ON c.RoomState=d.RoomState
                            WHERE A.CommID={row["CommID"]} AND A.CustID={row["CustID"]} AND A.IsActive=1 AND A.IsDelLive=0";
            IDbConnection con = new SqlConnection(strcon);
            DataTable dt = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet().Tables[0];
            con.Dispose();

            return JSONHelper.FromString(dt);
        }

        private string GetRoomModel(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
     
            string strcon = "";
            bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }
            string sql = $@"SELECT DictionaryCode,DictionaryName FROM Tb_Dictionary_RoomModel";
            IDbConnection con = new SqlConnection(strcon);
            DataTable dt = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet().Tables[0];
            con.Dispose();

            return JSONHelper.FromString(dt);
        }

        private string EditCustomerRoomInfo(DataRow row)
        {
            #region 参数校验
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }
            string roomID = row["RoomID"].ToString();
            string commID = row["CommID"].ToString();
            string BuildArea = "";
            string ActualSubDate = "";
            string FittingTime = "";
            string RoomModel = ""; 
            string StayTime = "";
            string LiveState = "";
            if (row.Table.Columns.Contains("BuildArea"))
            {
                BuildArea = row["BuildArea"].ToString();
            }
            if (row.Table.Columns.Contains("ActualSubDate"))
            {
                ActualSubDate = row["ActualSubDate"].ToString();
            }
            if (row.Table.Columns.Contains("FittingTime"))
            {
                FittingTime = row["FittingTime"].ToString();
            }
            if (row.Table.Columns.Contains("RoomModel"))
            {
                RoomModel = row["RoomModel"].ToString();
            }
            if (row.Table.Columns.Contains("StayTime"))
            {
                StayTime = row["StayTime"].ToString();
            }
            if (row.Table.Columns.Contains("LiveState"))
            {
                LiveState = row["LiveState"].ToString();
            }
            #endregion

            try
            {
                using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    con.Open();
                    var trans = con.BeginTransaction();

                    var sql = $@"UPDATE Tb_HSPR_Room SET BuildArea='{BuildArea}',ActualSubDate='{ActualSubDate}',
                            FittingTime='{FittingTime}',RoomModel='{RoomModel}',
                            StayTime='{StayTime}',RoomState={LiveState} WHERE RoomID ='{roomID}'";

                    int count = con.Execute(sql, null, trans);
                    trans.Commit();
                    return JSONHelper.FromString(true, "修改成功");
                    //if (count != 1)
                    //{
                    //    trans.Rollback();
                    //    return new ApiResult(false, "修改失败").toJson();
                    //}
                    //else
                    //{
                    //    int updateCount = 0;
                    //    int liveType = LiveState == "已入住" ? 1 : 0;


                    //    int c = con.QueryFirstOrDefault<int>(@"SELECT COUNT(*) FROM Tb_HSPR_BedLive 
                    //    WHERE CommID =@CommID AND RoomID=@RoomID", new { CommID = commID, RoomID = roomID },trans);

                    //    if (c == 0)
                    //    {
                    //        // 有问题
                    //        updateCount = con.Execute($@"INSERT INTO Tb_HSPR_BedLive(LiveID,CommID,RoomID,IsActive,LiveType,IsDelLive,CustID)
                    //                    SELECT  b.LiveID, a.CommID, a.RoomID, {liveType}, b.LiveType, IsDelLive, b.CustID FROM tb_hspr_room a
                    //                    LEFT JOIN Tb_HSPR_CustomerLive b ON a.RoomID = b.RoomID
                    //                     WHERE a.RoomID = {roomID}", null, trans);
                    //    }
                    //    else
                    //    {
                    //        // 有问题
                    //        updateCount = con.Execute($@" UPDATE Tb_HSPR_BedLive SET IsActive={liveType},LiveType=b.LiveType,IsDelLive=b.IsDelLive,CustID=b.CustID 
                    //                            FROM Tb_HSPR_Room a 
                    //                            LEFT JOIN Tb_HSPR_CustomerLive b ON a.RoomID = b.RoomID
                    //                            WHERE a.RoomID = {roomID}", null, trans);
                    //    }
                    //    if (updateCount > 0)
                    //    {
                    //        trans.Commit();
                    //        return new ApiResult(true, "修改成功").toJson();
                    //    }
                    //    else
                    //    {
                    //        trans.Rollback();
                    //        return new ApiResult(false, "修改失败").toJson();
                    //    }
                    //}
                }
            }
            catch (Exception e)
            {
                return new ApiResult(false, "修改失败"+e.Message).toJson();
            }
        }



        /// <summary>
        /// 房屋数量GetCustomerRoomList_Num
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// CustID          客户编码
        /// 返回
        ///         条数
        /// <returns></returns>
        private string GetCustomerRoomList_Num(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            string strcon = "";
            bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }
            string sql = "select  count(RoomSign) from view_HSPR_CustomerLive_Filter where CommID=" + row["CommID"] + " and CustID=" + row["CustID"];
            IDbConnection con = new SqlConnection(strcon);
            string backNum = con.ExecuteScalar(sql, null, null, null, CommandType.Text).ToString();
            con.Dispose();

            return JSONHelper.FromString(true, backNum);
        }


        #endregion

        #region 车位信息
        /// <summary>
        /// 车位信息GetCustomerParkInfo
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// CustID          客户编码
        /// 返回：
        ///     CarparkName                 车位区域
        ///     ParkCategoryName            车位类别
        ///     ParkType                    车位类型
        ///     ParkName                    车位编号
        ///     ParkArea                    车位面积
        ///     StanAmount                  收费标准
        ///     UseState                    当前状态
        ///     NCustName                   客户名称
        ///     NRoomSign                   房屋编号
        ///     ParkingCarSign              停车卡号
        ///     CarSign                     车牌号码
        ///     ParkStartDate               开始时间
        ///     ParkEndDate                 结束时间
        ///     ChargeCycleName             计费周期
        /// <returns></returns>
        private string GetCustomerParkInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            string strcon = "";
            bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }
            string sql = "select CarparkName,ParkCategoryName,ParkType,ParkName,ParkArea,StanAmount,UseState,NCustName,NRoomSign,ParkingCarSign,CarSign,ParkStartDate,ParkEndDate,ChargeCycleName  from view_HSPR_ParkingSel_Filter where CustID=" + row["CustID"] + " and CommID=" + row["CommID"];

            if (Global_Var.LoginCorpID == "1509")
            {
                sql = @"select CarparkName,ParkCategoryName,ParkType,ParkName,ParkArea,StanAmount,UseState,NCustName,
                        NRoomSign,ParkingCarSign,CarSign,ParkStartDate,ParkEndDate,ChargeCycleName,FeesChargeDate,
                        (SELECT max(FeesEndDate) FROM Tb_HSPR_Fees x WHERE x.ParkID=a.ParkID AND (x.IsCharge=1 OR x.IsPrec=1)) AS FeesEndDate 
                        from view_HSPR_ParkingSel_Filter a where CustID=" + row["CustID"] + " and CommID=" + row["CommID"];
            }
            IDbConnection con = new SqlConnection(strcon);
            DataTable dt = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet().Tables[0];
            con.Dispose();

            return JSONHelper.FromString(dt);

        }

        /// <summary>
        /// 车位数量GetCustomerParkInfo_Num
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// CustID          客户编码
        /// 返回
        ///         条数
        /// <returns></returns>
        private string GetCustomerParkInfo_Num(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            string strcon = "";
            bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }
            string sql = "select COUNT( ParkID)  from view_HSPR_ParkingSel_Filter where CustID=" + row["CustID"] + " and CommID=" + row["CommID"];
            IDbConnection con = new SqlConnection(strcon);
            string backNum = con.ExecuteScalar(sql, null, null, null, CommandType.Text).ToString();
            con.Dispose();

            return JSONHelper.FromString(true, backNum);

        }


        #endregion

        #region 钥匙信息
        /// <summary>
        /// 钥匙信息GetRenoKeysList
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// CustID          客户编码
        /// 返回
        ///     CustName            客户名称
        ///     RoomSign            房屋编号
        ///     KeyTypeName         钥匙类型
        ///     BussTypeName        业务类型
        ///     KeyUseTo            钥匙用途
        ///     KeyNum              总数量
        ///     SurplusKeyNum       库存量
        ///     AddUserName         经办人
        ///     AddDate             办理时间
        /// <returns></returns>
        private string GetRenoKeysList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            string strcon = "";
            bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }
            string sql = "SELECT CustName,RoomSign,KeyTypeName,BussTypeName,KeyUseTo,KeyNum,SurplusKeyNum,AddUserName,AddDate  from View_Tb_HSPR_RenoKeys_Filter where ISNULL(IsDelete,0) = 0 and CustID=" + row["CustID"] + " and CommID=" + row["CommID"];
            IDbConnection con = new SqlConnection(strcon);
            DataTable dt = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet().Tables[0];
            con.Dispose();

            return JSONHelper.FromString(dt);

        }


        /// <summary>
        /// 钥匙数量GetRenoKeysList_Num
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// CustID          客户编码
        /// 返回      
        ///         数量
        /// <returns></returns>
        private string GetRenoKeysList_Num(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            string strcon = "";
            bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }
            string sql = "SELECT COUNT(KeyTypeName) from View_Tb_HSPR_RenoKeys_Filter where ISNULL(IsDelete,0) = 0 and CustID=" + row["CustID"] + " and CommID=" + row["CommID"];
            IDbConnection con = new SqlConnection(strcon);

            string str = con.ExecuteScalar(sql, null, null, null, CommandType.Text).ToString();
            con.Dispose();

            return JSONHelper.FromString(true, str);

        }


        #endregion

        #region 家庭成员
        /// <summary>
        /// 家庭成员GetFamilyInfo
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// CustID          客户编码
        /// 返回
        ///     MemberName            成员名称
        ///     Sex                 性别
        ///     Birthday            出生日期
        ///     PaperCode           证件号码
        ///     MobilePhone         移动电话
        ///     RelationshipName    与户主关系
        /// <returns></returns>
        private string GetFamilyInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            string strcon = "";
            bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }
            string sql = "SELECT  MemberName,Sex,Birthday,PaperCode,MobilePhone,RelationshipName from view_HSPR_Household_Filter where ISNULL(IsDelete,0) = 0 and CustID=" + row["CustID"] + " and CommID=" + row["CommID"];
            IDbConnection con = new SqlConnection(strcon);
            DataTable dt = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet().Tables[0];
            con.Dispose();

            return JSONHelper.FromString(dt);

        }

        /// <summary>
        /// 家庭成员数量
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// CustID          客户编码
        /// 返回
        ///     数量
        /// <returns></returns>
        private string GetFamilyInfo_Num(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            string strcon = "";
            bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }
            string sql = "SELECT count(HoldID) from view_HSPR_Household_Filter where ISNULL(IsDelete,0) = 0 and CustID=" + row["CustID"] + " and CommID=" + row["CommID"];
            IDbConnection con = new SqlConnection(strcon);

            string str = con.ExecuteScalar(sql, null, null, null, CommandType.Text).ToString();
            con.Dispose();

            return JSONHelper.FromString(true, str);

        }

        #endregion

        #region 办卡信息
        /// <summary>
        /// 办卡信息
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// CustID          客户编码
        /// 返回
        ///     CustName            户主名称
        ///     YzRoomSign          户主房号
        ///     MemberName           成员名称
        ///     CyRoomSign           成员房号
        ///     RelationshipName     与户主关系
        ///     CardDate            办卡日期
        ///     CardNum             卡号
        ///     CardState           状态
        /// <returns></returns>
        private string GetCustAccessCar(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            string strcon = "";
            bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }
            string sql = "SELECT  CustName,YzRoomSign,MemberName,CyRoomSign,RelationshipName,CardDate,CardNum,CardState from View_HSPR_CustCard_Filter where ISNULL(IsDelete,0) = 0 and CustID=" + row["CustID"] + " and CommID=" + row["CommID"];
            IDbConnection con = new SqlConnection(strcon);
            DataTable dt = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet().Tables[0];
            con.Dispose();

            return JSONHelper.FromString(dt);

        }
        /// <summary>
        /// 办卡信息数量
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// CustID          客户编码
        /// 返回
        ///     数量
        /// <returns></returns>
        private string GetCustAccessCar_Num(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            string strcon = "";
            bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }
            string sql = "SELECT  COUNT(Id)  from View_HSPR_CustCard_Filter where ISNULL(IsDelete,0) = 0 and CustID=" + row["CustID"] + " and CommID=" + row["CommID"];
            IDbConnection con = new SqlConnection(strcon);
            string str = con.ExecuteScalar(sql, null, null, null, CommandType.Text).ToString();
            con.Dispose();

            return JSONHelper.FromString(true, str);

        }


        #endregion

        #region 装修信息
        /// <summary>
        /// 装修信息
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// CustID          客户编码
        /// 返回
        ///     CustName            客户名称
        ///     RoomSign          房屋编号
        ///     RenoPermitSign           许可证号
        ///     ConsUnit           施工单位
        ///     RenoStatus     装修状态
        ///     ApplyDate            开工时间
        ///     ActRenoEndDate             完工时间
        ///     RuleCount           违章次数
        /// <returns></returns>
        private string GetRenovation(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            string strcon = "";
            bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }
            string sql = "SELECT  CustName,RoomSign,RenoPermitSign,ConsUnit,RenoStatus,ApplyDate,ActRenoEndDate,RuleCount from view_HSPR_RenoCust_Filter where   CustID=" + row["CustID"] + " and CommID=" + row["CommID"];
            IDbConnection con = new SqlConnection(strcon);
            DataTable dt = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet().Tables[0];
            con.Dispose();

            return JSONHelper.FromString(dt);

        }
        /// <summary>
        /// 装修信息数量
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// CustID          客户编码
        /// 返回
        ///     数量
        /// <returns></returns>
        private string GetRenovation_Num(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            string strcon = "";
            bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }
            string sql = "SELECT  COUNT(RenoID)  from view_HSPR_RenoCust_Filter where  CustID=" + row["CustID"] + " and CommID=" + row["CommID"];
            IDbConnection con = new SqlConnection(strcon);
            string str = con.ExecuteScalar(sql, null, null, null, CommandType.Text).ToString();
            con.Dispose();

            return JSONHelper.FromString(true, str);

        }

        #endregion

        #region 报事信息
        /// <summary>
        /// 报事信息GetIncidentList
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// CustID          客户编码
        /// queryType       查询类型【0，报事，1投诉，默认0】
        /// PageIndex       
        /// PageSize
        /// 返回
        ///     报事类别            TypeName
        ///     报事区域            IncidentPlace
        ///     报事编号            IncidentNum
        ///     报事时间            IncidentDate
        ///     报事内容            IncidentContent
        ///     派工类别            DispTypeName
        ///     派工单             CoordinateNum
        ///     派工人             DispMan
        ///     派工时间            DispDate
        ///     处理人             DealMan
        ///     完结时间            MainEndDate
        ///     费用              DueAmount
        ///     完结人             FinishUser
        /// <returns></returns>
        private string GetIncidentList(DataRow row)
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
            string queryType = "0";
            if (row.Table.Columns.Contains("PageIndex"))
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize"))
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            if (row.Table.Columns.Contains("queryType"))
            {
                queryType = row["queryType"].ToString();
            }

            string strcon = "";
            bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }
            string sql = "SELECT  TypeName,IncidentPlace,IncidentNum,IncidentDate,IncidentContent,DispTypeName,CoordinateNum,DispMan,DispDate,DealMan,MainEndDate,DueAmount,FinishUser  FROM view_HSPR_IncidentSeach_Filter  where   CustID=" + row["CustID"] + " and CommID=" + row["CommID"] + " and ISNULL(Isdelete,0) = 0";
            if (queryType == "1")
            {
                sql += " and dbo.funCheckIncidentClassID(TypeID, CommID) > 0 ";
            }
            IDbConnection con = new SqlConnection(strcon);
            DataTable dt = null;
            int pageCount;
            int Counts;
            dt = BussinessCommon.GetList(out pageCount, out Counts, sql, PageIndex, PageSize, "IncidentDate", 1, "IncidentNum", strcon, "*").Tables[0];
            con.Dispose();

            return JSONHelper.FromString(dt);

        }
        /// <summary>
        /// 报事信息数量
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// CustID          客户编码
        /// queryType       查询类型【0，报事，1投诉，默认0】
        /// 返回
        ///     数量
        /// <returns></returns>
        private string GetIncidentList_Num(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            string queryType = "0";
            if (row.Table.Columns.Contains("queryType"))
            {
                queryType = row["queryType"].ToString();
            }
            string strcon = "";
            bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }
            string sql = "SELECT  COUNT(RenoID)  from view_HSPR_RenoCust_Filter where  CustID=" + row["CustID"] + " and CommID=" + row["CommID"];
            if (queryType == "1")
            {
                sql += " and dbo.funCheckIncidentClassID(TypeID, CommID) > 0 ";
            }
            IDbConnection con = new SqlConnection(strcon);
            string str = con.ExecuteScalar(sql, null, null, null, CommandType.Text).ToString();
            con.Dispose();

            return JSONHelper.FromString(true, str);

        }

        #endregion

        #region 委托信息
        /// <summary>
        /// 委托信息
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// CustID          客户编码
        /// PageIndex       
        /// PageSize
        /// 返回
        ///     客户名称            CustName
        ///     房屋编号            RoomSign
        ///     委托类别            EntrustType
        ///     联系电话            Mobile
        ///     委托时间            StartDate
        ///     委托书            EntrustBook
        ///     委托物             EntrustThing
        ///     是否办理             IsHandle
        ///     办理人            HandleUser
        ///     办理时间             HandleDate   
        /// <returns></returns>
        private string GetEntrustList(DataRow row)
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
            string queryType = "0";
            if (row.Table.Columns.Contains("PageIndex"))
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize"))
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            string strcon = PubConstant.hmWyglConnectionString;

            string sql = "SELECT  Id,CustName,RoomSign,EntrustType,Mobile,StartDate,EntrustBook,EntrustThing,IsHandle,HandleUser,HandleDate   FROM view_HSPR_Entrust_Filter  where   CustID=" + row["CustID"];

            IDbConnection con = new SqlConnection(strcon);
            DataTable dt = null;
            int pageCount;
            int Counts;
            dt = BussinessCommon.GetList(out pageCount, out Counts, sql, PageIndex, PageSize, "StartDate", 1, "Id", strcon, "*").Tables[0];
            con.Dispose();

            return JSONHelper.FromString(dt);

        }
        /// <summary>
        /// 委托信息数量
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// CustID          客户编码
        /// 返回
        ///     数量
        /// <returns></returns>
        private string GetEntrustList_Num(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }

            string strcon = PubConstant.hmWyglConnectionString;

            string sql = "SELECT  CustName,RoomSign,EntrustType,Mobile,StartDate,EntrustBook,EntrustThing,IsHandle,HandleUserName,HandleDate   FROM view_HSPR_Entrust_Filter  where   CustID=" + row["CustID"] + " and CommID=" + row["CommID"];

            IDbConnection con = new SqlConnection(strcon);
            string str = con.ExecuteScalar(sql, null, null, null, CommandType.Text).ToString();
            con.Dispose();

            return JSONHelper.FromString(true, str);

        }

        #endregion

        #region 欠费信息GetFeeInfo
        /// <summary>
        /// 欠费信息GetFeeInfo
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// CustID          客户编码
        /// PageIndex       
        /// PageSize
        /// 返回
        ///     CostName                费用名称
        ///     RoomSign                房屋编号
        ///     ParkName               车位编号
        ///     FeeDueYearMonth         费用日期
        ///     AccountsDueDate         开始日期
        ///     FeesStateDate           结束日期
        ///     DebtsAmount             欠收金额
        ///     LateFeeAmount           合同违约金
        /// <returns></returns>
        private string GetFeeInfo(DataRow row)
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

            if (row.Table.Columns.Contains("PageIndex"))
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize"))
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            string strcon = "";
            bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }
            string sql = "SELECT  FeesID,CostName,RoomSign,ParkName,FeeDueYearMonth,FeesEndDate AS AccountsDueDate,FeesStateDate,DebtsAmount,LateFeeAmount   FROM view_HSPR_Fees_SearchFilter  where  CustID=" + row["CustID"] + " and CommID=" + row["CommID"] + " and DebtsAmount>0 AND ISNULL(IsPrec,0)=0";

            IDbConnection con = new SqlConnection(strcon);
            DataTable dt = null;
            int pageCount;
            int Counts;
            dt = BussinessCommon.GetList(out pageCount, out Counts, sql, PageIndex, PageSize, "FeeDueYearMonth", 0, "FeesID", strcon, "*").Tables[0];
            con.Dispose();

            return JSONHelper.FromString(dt);

        }

        /// <summary>
        /// 欠费总额/总条数GetFeeCount
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// CustID          客户编码
        /// queryType       查询类型【0：总额，1：数据条数；默认0】
        /// 返回
        ///     总额/总条数
        /// <returns></returns>
        private string GetFeeCount(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            string queryType = "0";
            if (row.Table.Columns.Contains("queryType"))
            {
                queryType = row["queryType"].ToString();
            }

            string sql = "";
            if (queryType == "0")
            {
                sql = "SELECT sum(DebtsAmount)  FROM view_HSPR_Fees_SearchFilter  where CustID=" + row["CustID"] + " and CommID=" + row["CommID"] + " and DebtsAmount>0 AND ISNULL(IsPrec,0)=0";
            }
            else if (queryType == "1")
            {
                sql = "SELECT count(DebtsAmount)  FROM view_HSPR_Fees_SearchFilter  where CustID=" + row["CustID"] + " and CommID=" + row["CommID"] + " and DebtsAmount> AND ISNULL(IsPrec,0)=0";
            }
            else
            {
                return JSONHelper.FromString(false, "查询类型错误");
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                return JSONHelper.FromString(true, conn.ExecuteScalar(sql).ToString());
            }
        }

        #endregion

        #region 预交信息
        /// <summary>
        /// 预交信息GetPreInfo
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// CustID          客户编码
        /// PageIndex       
        /// PageSize
        /// 返回
        ///     CostName                费用名称
        ///     RoomSign                房屋编号
        ///     ParkNames               车位编号
        ///     PrecAmount              预交余额
        ///     Prompt                  余额提示     
        ///     PrecID                  预交ID
        /// <returns></returns>
        private string GetPreInfo(DataRow row)
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

            if (row.Table.Columns.Contains("PageIndex"))
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize"))
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            string strcon = "";
            bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }
            string sql = "select CostName,RoomSign,ParkNames,PrecAmount,余额提示 as Prompt,PrecID from view_HSPR_PreCosts_Filter  where   CustID=" + row["CustID"] + " and CommID=" + row["CommID"] + "  and PrecAmount>0";

            IDbConnection con = new SqlConnection(strcon);
            DataTable dt = null;
            int pageCount;
            int Counts;
            dt = BussinessCommon.GetList(out pageCount, out Counts, sql, PageIndex, PageSize, "PrecID", 0, "PrecID", strcon, "*").Tables[0];
            con.Dispose();

            return JSONHelper.FromString(dt);

        }


        /// <summary>
        /// 预交总条数
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// CustID          客户编码
        /// 返回
        ///     总条数
        /// <returns></returns>
        private string GetPreInfo_Num(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }

            string strcon = "";
            bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }

            string sql = "SELECT  COUNT(PrecID)  FROM view_HSPR_PreCosts_Filter  where   CustID=" + row["CustID"] + " and CommID=" + row["CommID"] + "  and PrecAmount>0";


            IDbConnection con = new SqlConnection(strcon);
            strcon = "";
            strcon = con.ExecuteScalar(sql, null, null, null, CommandType.Text).ToString();

            con.Dispose();

            return JSONHelper.FromString(true, strcon);

        }


        #endregion



        #region 客户分类查询
        /// <summary>
        /// 客户分类查询GetCustomerList_All
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码【必填】
        /// IsFess          当前欠费【默认不传，选择传1】
        /// IsIncident          当前报事【默认不传，选择传1】
        /// IsTousu          当前投诉【默认不传，选择传1】
        /// IsReno          当前装修【默认不传，选择传1】
        /// 返回
        ///       CustID          客户编码
        ///       CommID           小区编码
        ///       CustName          客户名称
        ///       MobilePhone       联系方式
        ///       RoomSign          房间编号
        ///       RoomCount         房间数量
        ///       DebtsAmount欠费总额       IsFess 为1 时显示
        ///       IncidentIDNum 报事数量      IsIncident 为1 时显示
        ///       TousuNum 投诉数量      IsTousu 为1 时显示
        ///       RenoIDNum 装修数量       IsReno 为1 时显示
        /// <returns></returns>
        private string GetCustomerList_All(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            string backstr = "";
            DataTable dt = new DataTable();
            try
            {

                string IsFess = "0";
                string IsIncident = "0";
                string IsTousu = "0";
                string IsReno = "0";
                int PageIndex = 1;
                int PageSize = 10;
                if (row.Table.Columns.Contains("IsFess"))
                {
                    IsFess = row["IsFess"].ToString();
                }
                if (row.Table.Columns.Contains("IsIncident"))
                {
                    IsIncident = row["IsIncident"].ToString();
                }
                if (row.Table.Columns.Contains("IsTousu"))
                {
                    IsTousu = row["IsTousu"].ToString();
                }
                if (row.Table.Columns.Contains("IsReno"))
                {
                    IsReno = row["IsReno"].ToString();
                }
                if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
                {
                    PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
                }
                if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
                {
                    PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
                }

                StringBuilder sb = new StringBuilder();

                sb.Append(@"select * from (select c.CustID, c.CommID, c.CustName, c.MobilePhone, c.RoomSigns as RoomSign, c.RoomSignsFormat, c.RoomCount,
            (select sum(DebtsAmount) from Tb_HSPR_Fees x where DebtsAmount > 0 AND ISNULL(IsCharge,0)=0 AND ISNULL(IsPrec,0)=0 AND x.CustID = c.CustID) AS DebtsAmount,
            (select count(IncidentID) from Tb_HSPR_IncidentAccept y where ISNULL(y.DealState, 0) = 0 and ISNULL(y.IsDelete, 0) = 0 AND y.CustID = c.CustID) AS IncidentIDNum,
            (select count(IncidentID) from Tb_HSPR_IncidentAccept y where ISNULL(y.DealState, 0) = 0 and ISNULL(y.IsDelete, 0) = 0 AND y.CustID = c.CustID AND  dbo.funCheckIncidentClassID(y.TypeID, y.CommID) > 0) AS TousuNum,
            (select count(RenoID) from view_HSPR_RenoCust_Filter z where Isnull(z.IsAct, 0) = 0 AND z.CustID = c.CustID) AS RenoIDNum
                from view_HSPR_Customer_Filter as c) AS a");

                sb.Append(" where a.CommID=" + row["CommID"].ToString());
                //if (IsFess == "1")//欠费
                {
                    sb.Append(" and a.DebtsAmount>0");
                }
                if (IsIncident == "1")//当前报事
                {
                    sb.Append(" and a.IncidentIDNum >0");
                }
                if (IsTousu == "1")//当前投诉 
                {
                    sb.Append("  and a.TousuNum >0");

                }
                if (IsReno == "1")//当前装修
                {
                    sb.Append("  and a.RenoIDNum >0 ");

                }

                int pageCount;
                int Counts;
                dt = GetList(out pageCount, out Counts, sb.ToString(), PageIndex, PageSize, "CustID", 1, "CustID", PubConstant.hmWyglConnectionString).Tables[0];
            }
            catch (Exception ex)
            {

                backstr = ex.Message;
            }

            if (backstr != "")
            {
                return JSONHelper.FromString(false, backstr);
            }
            else
            {
                return JSONHelper.FromString(dt);
            }
        }

        /// <summary>
        /// 客户分类查询数量
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetCustomerList_All_Num(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            string backstr = "";
            DataTable dt = new DataTable();
            int pageCount;
            string Counts = "0";
            try
            {

                string IsFess = "0";
                string IsIncident = "0";
                string IsTousu = "0";
                string IsReno = "0";
                int PageIndex = 1;
                int PageSize = 1;
                if (row.Table.Columns.Contains("IsFess"))
                {
                    IsFess = row["IsFess"].ToString();
                }
                if (row.Table.Columns.Contains("IsIncident"))
                {
                    IsIncident = row["IsIncident"].ToString();
                }
                if (row.Table.Columns.Contains("IsTousu"))
                {
                    IsTousu = row["IsTousu"].ToString();
                }
                if (row.Table.Columns.Contains("IsReno"))
                {
                    IsReno = row["IsReno"].ToString();
                }
                if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
                {
                    PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
                }
                if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
                {
                    PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
                }
                string strcon = "";
                bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
                if (bl == false)
                {
                    return JSONHelper.FromString(false, strcon);
                }

                StringBuilder sb = new StringBuilder();


                sb.Append("select count(*) from (");
                sb.Append(" select  c.CustID,c.CommID,c.CustName,c.MobilePhone,c.RoomSigns as RoomSign,c.RoomCount,f.DebtsAmount,ISNULL( i.IncidentIDNum,0) as IncidentIDNum,ISNULL( s.TousuNum,0) as TousuNum,ISNULL( r.RenoIDNum,0) as RenoIDNum from view_HSPR_Customer_Filter as c");
                sb.Append(" left join (        select  top 1 CustID, RoomSign from view_HSPR_Room_AllFilter group by CustID, RoomSign    ) as m on m.CustID = c.CustID");
                sb.Append(" left join (        select CustID, sum(DebtsAmount) as DebtsAmount from view_HSPR_Fees_Filter where DebtsAmount>0 group by CustID    ) as f on f.CustID = c.CustID");
                sb.Append(" left join (        select CustID, count(IncidentID) as IncidentIDNum from view_HSPR_IncidentAccept_SearchFilter where ISNULL( DealState,0)=0 and ISNULL(IsDelete,0)=0 group by CustID    ) as i on i.CustID = c.CustID");
                sb.Append(" left join (        select CustID, count(IncidentID) as TousuNum from view_HSPR_IncidentAccept_SearchFilter  where ISNULL( DealState,0)=0 and ISNULL(IsDelete,0)=0 and dbo.funCheckIncidentClassID(TypeID, CommID) > 0 group by CustID    ) as s on s.CustID = c.CustID");
                sb.Append(" left join (        select CustID, count(RenoID) as RenoIDNum from view_HSPR_RenoCust_Filter where Isnull(IsAct, 0) = 0 group by CustID    ) as r on r.CustID = c.CustID");
                sb.Append(" ) as t");


                sb.Append(" where CommID=" + row["CommID"].ToString());
                if (IsFess == "1")//欠费
                {
                    sb.Append(" and DebtsAmount>0");
                }
                if (IsIncident == "1")//当前报事
                {
                    sb.Append(" and IncidentIDNum >0");
                }
                if (IsTousu == "1")//当前投诉 
                {
                    sb.Append("  and TousuNum >0");

                }
                if (IsReno == "1")//当前装修
                {
                    sb.Append("  and RenoIDNum >0 ");

                }


                //dt = BussinessCommon.GetList(out pageCount, out Counts, sb.ToString(), PageIndex, PageSize, "CustID", 1, "CustID", strcon, "*").Tables[0];
                IDbConnection con = new SqlConnection(strcon);
                Counts = con.ExecuteScalar(sb.ToString(), null, null, null, CommandType.Text).ToString();

                con.Dispose();
            }
            catch (Exception ex)
            {

                backstr = ex.Message;
            }

            if (backstr != "")
            {
                return JSONHelper.FromString(false, backstr);
            }
            else
            {
                return JSONHelper.FromString(true, Counts);
            }
        }


        #endregion

        #region 客户精确查询
        /// <summary>
        /// 模糊查询 搜索条件GetQueryValue
        /// </summary>
        /// <param name="row"></param>
        /// CommID              小区编码【必填】
        /// whereStr            条件
        /// queryType           模糊搜索类别【默认1姓名，2房号，3电话，4车牌】
        /// 返回：
        ///     1：CustID【客户编码】,CustName【客户名称】
        ///     2：RoomSign【房间编号】
        ///     3：MobilePhone【电话号码】
        ///     4：HandID【车牌编码】,ParkName【车牌号】
        /// <returns></returns>
        private string GetQueryValue(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            DataTable dt = new DataTable();
            string backstr = "";
            try
            {
                string queryType = "1";
                string whereStr = "";
                int PageIndex = 1;
                int PageSize = 5;
                if (row.Table.Columns.Contains("queryType"))
                {
                    queryType = row["queryType"].ToString();
                }
                if (row.Table.Columns.Contains("whereStr"))
                {
                    whereStr = row["whereStr"].ToString();
                }
                if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
                {
                    PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
                }
                if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
                {
                    PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
                }
                string strcon = "";
                bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
                if (bl == false)
                {
                    return JSONHelper.FromString(false, strcon);
                }
                IDbConnection con = new SqlConnection(strcon);

                string sql = "";
                if (queryType == "1")//客户名称
                {
                    sql = "select CustID,CustName from view_HSPR_Customer_Filter  where Commid=" + row["CommID"] + " and CustName like '%" + whereStr + "%'";
                }
                if (queryType == "2")//房号
                {
                    sql = "select RoomSign from view_HSPR_Room_Filter  where Commid=" + row["CommID"] + " and RoomSign like '%" + whereStr + "%'";
                }
                if (queryType == "3")//电话 
                {
                    sql = "select MobilePhone from view_HSPR_Customer_Filter  where Commid=" + row["CommID"] + " and MobilePhone like '%" + whereStr + "%'";
                }
                if (queryType == "4")//车牌
                {
                    sql = "select HandID,ParkName from view_HSPR_ParkingHand_Filter  where Commid=" + row["CommID"] + " and ParkName like '%" + whereStr + "%'";
                }

                int pageCount;
                int Counts;

                dt = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet().Tables[0];
                //dt = BussinessCommon.GetList(out pageCount, out Counts,sql, PageIndex, PageSize, "CustID", 1, "CustID", strcon, "*").Tables[0];
                con.Dispose();
            }
            catch (Exception ex)
            {
                backstr = ex.Message;
            }
            if (backstr != "")
            {
                return JSONHelper.FromString(false, backstr);
            }
            else
            {
                return JSONHelper.FromString(dt);
            }
        }


        /// <summary>
        /// 精确查询GetCustomerList
        /// </summary>
        /// <param name="row"></param>
        /// CommID              小区编码【必填】
        /// whereStr            条件
        /// queryType           模糊搜索类别【默认1姓名，2房号，3电话，4车牌】
        /// 返回
        ///       CustID          客户编码
        ///       CommID           小区编码
        ///       CustName          客户名称
        ///       MobilePhone       联系方式
        ///       RoomSign          房间编号
        ///       RoomCount         房间数量
        ///       DebtsAmount欠费总额       
        ///       IncidentIDNum 报事数量      
        ///       TousuNum 投诉数量     
        ///       RenoIDNum 装修数量      
        /// <returns></returns>
        private string GetCustomerList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            DataTable dt = new DataTable();
            string backstr = "";
            try
            {
                string queryType = "1";
                string whereStr = "";
                if (row.Table.Columns.Contains("queryType"))
                {
                    queryType = row["queryType"].ToString();
                }
                if (row.Table.Columns.Contains("whereStr"))
                {
                    whereStr = row["whereStr"].ToString();
                }
                string strcon = "";
                bool bl = new IncidentAcceptManage().GetDBServerPath(row["CommID"].ToString(), out strcon);
                if (bl == false)
                {
                    return JSONHelper.FromString(false, strcon);
                }
                IDbConnection con = new SqlConnection(strcon);

                StringBuilder sb = new StringBuilder();
                sb.Append("select * from (");
                sb.Append(" select  c.CustID,c.CommID,c.CustName,c.MobilePhone,c.RoomSigns as RoomSign,c.RoomCount,f.DebtsAmount,ISNULL( i.IncidentIDNum,0) as IncidentIDNum,ISNULL( s.TousuNum,0) as TousuNum,ISNULL( r.RenoIDNum,0) as RenoIDNum from view_HSPR_Customer_Filter as c");
                sb.Append(" left join (select  top 1 CustID, RoomSign from view_HSPR_Room_AllFilter group by CustID, RoomSign    ) as m on m.CustID = c.CustID");
                //修复查询出来的数据不正确的问题(2017-08-09)
                //sb.Append(" left join (select CustID, sum(DebtsAmount) as DebtsAmount from view_HSPR_Fees_Filter WHERE FeesEndDate<dateadd(dd,-day(getdate()),dateadd(m,1,getdate())) group by CustID    ) as f on f.CustID = c.CustID");
                //sb.Append(" left join (select CustID, sum(DebtsAmount) as DebtsAmount from view_HSPR_Fees_Filter WHERE ISNULL(IsCharge,0)=0 AND convert(NVARCHAR(50), FeesEndDate, 120)<'" + DateTime.Now.AddMonths(1).ToString("yyyy-MM-01 00:00:00") + "' group by CustID) as f on f.CustID = c.CustID");
                sb.Append(" left join (select CustID, sum(isnull(DebtsAmount,0)) as DebtsAmount from view_HSPR_Fees_Filter WHERE ISNULL(IsCharge,0)=0 AND ISNULL(IsPrec,0)=0 group by CustID) as f on f.CustID = c.CustID");
                sb.Append(" left join (select CustID, count(IncidentID) as IncidentIDNum from view_HSPR_IncidentAccept_SearchFilter group by CustID    ) as i on i.CustID = c.CustID");
                sb.Append(" left join (select CustID, count(IncidentID) as TousuNum from view_HSPR_IncidentAccept_SearchFilter where dbo.funCheckIncidentClassID(TypeID, CommID) > 0 group by CustID    ) as s on s.CustID = c.CustID");
                sb.Append(" left join (select CustID, count(RenoID) as RenoIDNum from view_HSPR_RenoCust_Filter where Isnull(IsAct, 0) = 0 group by CustID    ) as r on r.CustID = c.CustID");
                sb.Append(" ) as t");

                if (queryType == "1")//客户名称
                {
                    sb.Append(" where CustID=" + whereStr);
                }
                if (queryType == "2")//房号
                {
                    sb.AppendFormat(@" where CustID in (SELECT a.CustID FROM Tb_HSPR_CustomerLive a LEFT JOIN Tb_HSPR_Room b ON a.RoomID=b.RoomID 
                                WHERE a.IsActive = 1 AND ISNULL(IsDelLive, 0) = 0 AND b.CommID ={0} AND(b.RoomSign = '{1}' OR b.RoomName = '{1}'))",
                                row["CommID"], row["whereStr"]);
                }
                if (queryType == "3")//电话 
                {
                    sb.AppendFormat(" where MobilePhone='{0}'", whereStr);

                }
                if (queryType == "4")//车牌
                {
                    sb.AppendFormat(" where CustID in (select CustID from view_HSPR_ParkingHand_Filter where HandID={0})", whereStr);
                }

                dt = con.ExecuteReader(sb.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
                con.Dispose();
            }
            catch (Exception ex)
            {
                backstr = ex.Message;
            }
            if (backstr != "")
            {
                return JSONHelper.FromString(false, backstr);
            }
            else
            {
                return JSONHelper.FromString(dt);
            }
        }

        #endregion




    }
}
