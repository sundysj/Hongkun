using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using MobileSoft.Common;
using System.Data;
using MobileSoft.DBUtility;
using System.Data.SqlClient;
using Dapper;
using Model.HSPR;
using KernelDev.DataAccess;
using MobileSoft.Model.HSPR;
using MobileSoft.Model.Unified;
using Model;
namespace Business
{
    public class HKCallCenter : PubInfo
    {
        Tb_HSPR_IncidentError log = new Tb_HSPR_IncidentError();
        public HKCallCenter()
        {
            base.Token = "1A63C9BCA158";
        }

        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            log.Method = Trans.Command;
            log.Parameter = Trans.Attribute;
            log.State = "鸿坤400接口";
            log.ErrorDate = DateTime.Now;

            switch (Trans.Command)
            {
                //获取全部房屋信息
                case "GetRoomList":
                    Trans.Result = GetRoomList(Row);
                    break;
                    
                //获取全部房屋信息
                case "GetCustList":
                    Trans.Result = GetCustList(Row);
                    break;
                //获取全部家庭成员信息
                case "GetHouseHoldList":
                    Trans.Result = GetHouseHoldList(Row);
                    break;
                //获取报事类别
                case "GetIncidentTypeList":
                    Trans.Result = GetIncidentTypeList(Row);
                    break;
                //户内报事
                case "SetIncidentAcceptPhoneInsert":
                    Trans.Result = SetIncidentAcceptPhoneInsert(Row);
                    break;

                //报事回访
                case "SetIncidentAcceptReplyInsert":
                    Trans.Result = SetIncidentAcceptReplyInsert(Row);
                    break;
                //报事跟进
                case "SetIncidentCoordinateInsert":
                    Trans.Result = SetIncidentCoordinateInsert(Row);
                    break;
                //获取业主历史缴费
                case "GetHistoricalPaymentList":
                    Trans.Result = GetHistoricalPaymentList(Row);
                    break;
                //获取报事类别树形
                case "GetIncidentTypeTree":
                    Trans.Result = GetIncidentTypeTree(Row);
                    break;


                //公区报事
                case "SetIncidentAcceptObjectInsert":
                    Trans.Result = SetIncidentAcceptObjectInsert(Row);
                    break;

                    


            }
        }
        /// <summary>
        /// 获取全部房屋信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetRoomList(DataRow row) {
             
            string backstr = "";
            try
            {
                int PageIndex = 1;
                int PageSize = 20;

                if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
                {
                    PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
                }
                if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
                {
                    PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
                }
                 

                
                    string strSql = @"  select CommID
                                            ,CommName
                                            ,RoomID
                                            ,BuildName
                                            ,BuildID
                                            ,FloorSnum
                                            ,UnitsNum
                                            ,RoomSign
                                            ,ActualSubDate
                                            ,StateName
                                            ,PayState
                                            ,BuildArea
                                            ,InteriorArea
                                            CommonArea
                                               from [view_HSPR_Room_InterfaceFilter] ";
                 //   DataSet ds = Conn.ExecuteReader(strSql, null, null, null, CommandType.Text).ToDataSet();
                  
                    int pageCount;
                    int Counts;

                    DataSet ds = BussinessCommon.GetList(out pageCount, out Counts, strSql.ToString(), PageIndex, PageSize, 
                        "CommID", 0, "CommID", Connection.GetConnection("8"), "*");

                    log.ErrorContent = "true";
                    LogAdd(log);
                    return JSONHelper.FromString(true, ds.Tables[0]);
               
            }
            catch (Exception ex)
            {
                backstr = ex.Message;
                log.ErrorContent = "false," + backstr;
                LogAdd(log);
                return JSONHelper.FromString(false, backstr);
            }

        }


        /// <summary>
        /// 获取全部客户信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetCustList(DataRow row)
        {

            string backstr = "";
            try
            {

                int PageIndex = 1;
                int PageSize = 20;

                if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
                {
                    PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
                }
                if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
                {
                    PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
                }

                string strSql = @"    select CommID,
                                        LiveType,
                                        CustID,
                                        RoomID,
                                        CustName,
                                        PaperCode,
                                        PaperName,
                                        MobilePhone,
                                        BirthDay,
                                        Sex,
                                        AddRess,
                                        LinkManTel from view_HSPR_Customer_InterfaceFilter where isnull(isdelete,0)=0";

                int pageCount;
                int Counts;

                DataSet ds = BussinessCommon.GetList(out pageCount, out Counts, strSql.ToString(), PageIndex, PageSize,
                    "CommID", 0, "CommID", Connection.GetConnection("8"), "*");


                log.ErrorContent = "true";
                LogAdd(log);
                return JSONHelper.FromString(true, ds.Tables[0]);
                
            }
            catch (Exception ex)
            {
                backstr = ex.Message;
                log.ErrorContent = "false," + backstr;
                LogAdd(log);
                return JSONHelper.FromString(false, backstr);
            }

        }


        /// <summary>
        /// 获取全部家庭成员信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetHouseHoldList(DataRow row)
        {

            string backstr = "";
            try
            {
                int PageIndex = 1;
                int PageSize = 20;

                if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
                {
                    PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
                }
                if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
                {
                    PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
                }

                string strSql = @"   
                                 select 
                                 CommID,
                                HoldID,
                                CustID,
                                RoomID,
                                HoldName,
                                PaperCode,
                                PaperName,
                                MobilePhone,
                                BirthDay,
                                Sex,
                                AddRess,
                                LinkManTel,
                                RelationshipName 
                                from [view_HSPR_Household_InterfaceFilter] where  isnull( isdelete,0)=0
                                ";
                 
                int pageCount;
                int Counts;

                DataSet ds = BussinessCommon.GetList(out pageCount, out Counts, strSql.ToString(), PageIndex, PageSize,
                    "CommID", 0, "CommID", Connection.GetConnection("8"), "*");
                 
                log.ErrorContent = "true";
                LogAdd(log);
                return JSONHelper.FromString(true, ds.Tables[0]);
                 
            
            }
            catch (Exception ex)
            {
                backstr = ex.Message;
                log.ErrorContent = "false," + backstr;
                LogAdd(log);
                return JSONHelper.FromString(false, backstr);
            }

        }
         

        /// <summary>
        /// 报事类别
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetIncidentTypeList(DataRow row)
        {
            

            string backstr = "";
            try
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
                int PageIndex = 1;
                int PageSize = 20;

                if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
                {
                    PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
                }
                if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
                {
                    PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
                }
                string strSql = @"   
                                 select t.CorpTypeID,
                                t.TypeName,
                                t.DealLimit,
                                t.ReserveHint,
                                t.IncidentPlace,
                                t.ReplyLimit,t.IsTreeRoot from Tb_HSPR_IncidentType
                                 as t inner join Tb_HSPR_CorpIncidentType as i on i.CorpTypeID = t.CorpTypeID
                                  where   t.CommID = '" + Community.CommID + "' and i.IsDelete = 0  ";
                ///t.IsTreeRoot = 0

                int pageCount;
                int Counts;

                DataSet ds = BussinessCommon.GetList(out pageCount, out Counts, strSql.ToString(), PageIndex, PageSize,
                    "CommID", 0, "CommID", Connection.GetConnection("8"), "*");
                 
                log.ErrorContent = "true";
                LogAdd(log);
                return JSONHelper.FromString(true, ds.Tables[0]); 

            }
            catch (Exception ex)
            {
                backstr = ex.Message;
                log.ErrorContent = "false," + backstr;
                LogAdd(log);
                return JSONHelper.FromString(false, backstr);
            }

        }

        /// <summary>
        /// 报事类别树 json 
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetIncidentTypeTree(DataRow row)
        {


            string backstr = "";
            try
            {
                if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
                {
                    return JSONHelper.FromString(false, "小区编号不能为空");
                }

                string CommID = row["CommID"].ToString();

                ////查询小区
                //Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
                ////构造链接字符串
                //if (Community == null)
                //{
                //    return JSONHelper.FromString(false, "该小区不存在");
                //}
                 
                using (IDbConnection Conn = new SqlConnection(Connection.GetConnection("8")))
                {
                    EasyUiTree Eut = new EasyUiTree();
                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@CommID ", CommID);
                    parameters.Add("@ClassID ", 0);
                    parameters.Add("@CostName ", "");
                    parameters.Add("@IsEnabled ", 1);
                    parameters.Add("@@IncidentPlace ", "业主权属");  
                    DataSet Ds = Conn.ExecuteReader("Proc_HSPR_IncidentType_GetAllNodes2", parameters, null, null, CommandType.StoredProcedure).ToDataSet();
                    backstr= Eut.GetTreeJsonByTable(Ds.Tables[0], "TypeCode", "TypeName", "PrentTypeCode", "", "TypeCode,TypeID,IsTreeRoot,RatedWorkHour,KPIRatio,FullTypeName");
                    log.ErrorContent = "true";
                    LogAdd(log);
                    return backstr;
                }
                 
               
            }
            catch (Exception ex)
            {
                backstr = ex.Message;
                log.ErrorContent = "false," + backstr;
                LogAdd(log);
                return JSONHelper.FromString(false, backstr);
            }

        }




        /// <summary>
        /// 户内报事
        /// </summary>
        /// <param name="row"></param> 
        /// <returns></returns>
        private string SetIncidentAcceptPhoneInsert(DataRow row)
        {


            string backstr = "";
            try
            {
                #region 报事
                if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
                {
                    return JSONHelper.FromString(false, "项目ID不能为空");
                }
                if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
                {
                    return JSONHelper.FromString(false, "客户ID不能为空");
                }

                if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
                {
                    return JSONHelper.FromString(false, "房屋ID不能为空");
                }

                if (!row.Table.Columns.Contains("CorpTypeID") || string.IsNullOrEmpty(row["CorpTypeID"].ToString()))
                {
                    return JSONHelper.FromString(false, "报事类别ID不能为空");
                }

                //if (!row.Table.Columns.Contains("IncidentNum") || string.IsNullOrEmpty(row["IncidentNum"].ToString()))
                //{
                //    return JSONHelper.FromString(false, "报事编号不能为空");
                //}
                //if (!row.Table.Columns.Contains("IncidentPlace") || string.IsNullOrEmpty(row["IncidentPlace"].ToString()))
                //{
                //    return JSONHelper.FromString(false, "报事区域不能为空(参数: 公区报事 or 业主权属)");
                //}

                if (!row.Table.Columns.Contains("IncidentMan") || string.IsNullOrEmpty(row["IncidentMan"].ToString()))
                {
                    return JSONHelper.FromString(false, "报事人不能为空");
                }
                if (!row.Table.Columns.Contains("IncidentDate") || string.IsNullOrEmpty(row["IncidentDate"].ToString()))
                {
                    return JSONHelper.FromString(false, "报事时间不能为空");
                }

                //if (!row.Table.Columns.Contains("IncidentMode") || string.IsNullOrEmpty(row["IncidentMode"].ToString()))
                //{
                //    return JSONHelper.FromString(false, "报事来源不能为空");
                //}

                if (!row.Table.Columns.Contains("DealLimit") || string.IsNullOrEmpty(row["DealLimit"].ToString()))
                {
                    return JSONHelper.FromString(false, "处理时限不能为空");
                }
                if (!row.Table.Columns.Contains("ReplyLimit") || string.IsNullOrEmpty(row["ReplyLimit"].ToString()))
                {
                    return JSONHelper.FromString(false, "回访时限不能为空");
                }
                if (!row.Table.Columns.Contains("IncidentContent") || string.IsNullOrEmpty(row["IncidentContent"].ToString()))
                {
                    return JSONHelper.FromString(false, "报事内容不能为空");
                }
                string ReserveDate = "";
                if (row.Table.Columns.Contains("ReserveDate") || !string.IsNullOrEmpty(row["ReserveDate"].ToString()))
                {
                    ReserveDate = row["ReserveDate"].ToString();
                }
                if (!row.Table.Columns.Contains("Phone") || string.IsNullOrEmpty(row["Phone"].ToString()))
                {
                    return JSONHelper.FromString(false, "报事人电话不能为空");
                }
                if (!row.Table.Columns.Contains("AdmiMan") || string.IsNullOrEmpty(row["AdmiMan"].ToString()))
                {
                    return JSONHelper.FromString(false, "受理人不能为空");
                }

                if (!row.Table.Columns.Contains("AdmiDate") || string.IsNullOrEmpty(row["AdmiDate"].ToString()))
                {
                    return JSONHelper.FromString(false, "受理时间不能为空");
                }
                if (!row.Table.Columns.Contains("CallCenterID") || string.IsNullOrEmpty(row["CallCenterID"].ToString()))
                {
                    return JSONHelper.FromString(false, "400呼叫中心ID不能为空");
                }
                string IncidentImgs = "";
                if (row.Table.Columns.Contains("images") || !string.IsNullOrEmpty(row["images"].ToString()))
                {
                    IncidentImgs = row["images"].ToString();
                }


                #endregion
                #region 参数

                string CommID = row["CommID"].ToString();
                string CustID = row["CustID"].ToString();
                string RoomID = row["RoomID"].ToString();

                string CorpTypeID = row["CorpTypeID"].ToString();
               // string IncidentNum = row["IncidentNum"].ToString();
            //  string IncidentPlace = row["IncidentPlace"].ToString();
                string IncidentMan = row["IncidentMan"].ToString();
                string IncidentDate = row["IncidentDate"].ToString();
              //  string IncidentMode = row["IncidentMode"].ToString();
                string DealLimit = row["DealLimit"].ToString();
                string ReplyLimit = row["ReplyLimit"].ToString();
                string IncidentContent = row["IncidentContent"].ToString();
               
                string Phone = row["Phone"].ToString();
                string AdmiMan = row["AdmiMan"].ToString();
                string AdmiDate = row["AdmiDate"].ToString();
             

                string CallCenterID = row["CallCenterID"].ToString(); 
                #endregion

                using (IDbConnection Conn = new SqlConnection(Connection.GetConnection("8")))
                {
               
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@CommID", CommID);
                    parameters.Add("@CustID", CustID);
                    parameters.Add("@RoomID", RoomID); 
                    parameters.Add("@TypeID", CorpTypeID);
                   //parameters.Add("@IncidentNum", IncidentNum);    // erp 系统生成
                 //  parameters.Add("@IncidentPlace", IncidentPlace);  //业主报事
                    parameters.Add("@IncidentMan", IncidentMan);
                    parameters.Add("@IncidentDate", IncidentDate);
                  //  parameters.Add("@IncidentMode", IncidentMode); //报事来源
                    parameters.Add("@DealLimit", DealLimit);
                    parameters.Add("@ReplyLimit", ReplyLimit);
                    parameters.Add("@IncidentContent", IncidentContent);
                    parameters.Add("@ReserveDate", ReserveDate);
                    parameters.Add("@Phone", Phone);
                    parameters.Add("@AdmiMan", AdmiMan);
                    parameters.Add("@AdmiDate", AdmiDate);
                    parameters.Add("@CallCenterID", CallCenterID); 
                    parameters.Add("@IncidentImgs", IncidentImgs); 
                    Conn.Execute("Proc_HSPR_IncidentAccept_InterfaceInsert", parameters, null, null, CommandType.StoredProcedure);

                    //获取当前报事
                    string str = @"select TOP 1 * from Tb_HSPR_IncidentAccept 
                             where CommID = @CommID and RoomID = @RoomID 
                             and Phone = @Phone 
                             and IncidentContent = @IncidentContent ORDER BY IncidentDate DESC";
                    Tb_HSPR_IncidentAccept model = Conn.Query<Tb_HSPR_IncidentAccept>(str, new { CommID = CommID, RoomID = RoomID, Phone = Phone, IncidentContent = IncidentContent }).LastOrDefault();
                   
                    #region app推送
                    PubConstant.hmWyglConnectionString = Connection.GetConnection("8");
                    PubConstant.tw2bsConnectionString = Global_Fun.GetConnectionString("PublicConnectionString");
                    Global_Var.CorpId = "1973";
                    //model非空验证
                    if (model != null)
                    {
                        if (string.IsNullOrEmpty(model.TypeID))
                        {
                            IncidentAcceptPush.SynchPushIndoorIncidentWithoutIncidentType(model);
                        }
                        else
                        {
                            IncidentAcceptPush.SynchPushIndoorIncident(model);
                        }
                    } 
                    #endregion


                    backstr = "{\"Result\":\"true\",\"IncidentID\":\"" + model.IncidentID + "\",\"IncidentNum\":\"" + model.IncidentNum + "\"}";
                    log.ErrorContent = "true";
                    LogAdd(log);
               // return JSONHelper.FromString(true, "报事成功!稍后会有人员与您联系!");
                    return backstr;
                }
                  
               

            }
            catch (Exception ex)
            {
                backstr = ex.Message;
                log.ErrorContent = "false," + backstr;
                LogAdd(log);
                return JSONHelper.FromString(false, backstr);
            } 
             
        }
         
        /// <summary>
        /// 公区报事
        /// </summary>
        /// <param name="row"></param> 
        /// <returns></returns>
        private string SetIncidentAcceptObjectInsert(DataRow row)
        {


            string backstr = "";
            try
            {
                #region 报事
                if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
                {
                    return JSONHelper.FromString(false, "项目ID不能为空");
                }
                if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
                {
                    return JSONHelper.FromString(false, "客户ID不能为空");
                }

               

                if (!row.Table.Columns.Contains("CorpTypeID") || string.IsNullOrEmpty(row["CorpTypeID"].ToString()))
                {
                    return JSONHelper.FromString(false, "报事类别ID不能为空");
                }

             

                if (!row.Table.Columns.Contains("IncidentMan") || string.IsNullOrEmpty(row["IncidentMan"].ToString()))
                {
                    return JSONHelper.FromString(false, "报事人不能为空");
                }
                if (!row.Table.Columns.Contains("IncidentDate") || string.IsNullOrEmpty(row["IncidentDate"].ToString()))
                {
                    return JSONHelper.FromString(false, "报事时间不能为空");
                }

            

                if (!row.Table.Columns.Contains("DealLimit") || string.IsNullOrEmpty(row["DealLimit"].ToString()))
                {
                    return JSONHelper.FromString(false, "处理时限不能为空");
                }
                if (!row.Table.Columns.Contains("ReplyLimit") || string.IsNullOrEmpty(row["ReplyLimit"].ToString()))
                {
                    return JSONHelper.FromString(false, "回访时限不能为空");
                }
                if (!row.Table.Columns.Contains("IncidentContent") || string.IsNullOrEmpty(row["IncidentContent"].ToString()))
                {
                    return JSONHelper.FromString(false, "报事内容不能为空");
                }
                string ReserveDate = "";
                if (row.Table.Columns.Contains("ReserveDate") || !string.IsNullOrEmpty(row["ReserveDate"].ToString()))
                {
                    ReserveDate = row["ReserveDate"].ToString();
                }
                if (!row.Table.Columns.Contains("Phone") || string.IsNullOrEmpty(row["Phone"].ToString()))
                {
                    return JSONHelper.FromString(false, "报事人电话不能为空");
                }
                if (!row.Table.Columns.Contains("AdmiMan") || string.IsNullOrEmpty(row["AdmiMan"].ToString()))
                {
                    return JSONHelper.FromString(false, "受理人不能为空");
                }

                if (!row.Table.Columns.Contains("AdmiDate") || string.IsNullOrEmpty(row["AdmiDate"].ToString()))
                {
                    return JSONHelper.FromString(false, "受理时间不能为空");
                }
                if (!row.Table.Columns.Contains("CallCenterID") || string.IsNullOrEmpty(row["CallCenterID"].ToString()))
                {
                    return JSONHelper.FromString(false, "400呼叫中心ID不能为空");
                }
                string IncidentImgs = "";
                if (row.Table.Columns.Contains("images") || !string.IsNullOrEmpty(row["images"].ToString()))
                {
                    IncidentImgs = row["images"].ToString();
                }

                if (!row.Table.Columns.Contains("RegionalID") || string.IsNullOrEmpty(row["RegionalID"].ToString()))
                {
                    return JSONHelper.FromString(false, "RegionalID 公区ID不能为空");
                }
                string LocationID = "0";
                if (!row.Table.Columns.Contains("LocationID") || string.IsNullOrEmpty(row["LocationID"].ToString()))
                {
                    LocationID = "0";
                }
                else { LocationID = row["LocationID"].ToString(); }
                #endregion
                #region 参数

                string CommID = row["CommID"].ToString();
                string CustID = row["CustID"].ToString();
              

                string CorpTypeID = row["CorpTypeID"].ToString();
                // string IncidentNum = row["IncidentNum"].ToString();
                //  string IncidentPlace = row["IncidentPlace"].ToString();
                string IncidentMan = row["IncidentMan"].ToString();
                string IncidentDate = row["IncidentDate"].ToString();
                //  string IncidentMode = row["IncidentMode"].ToString();
                string DealLimit = row["DealLimit"].ToString();
                string ReplyLimit = row["ReplyLimit"].ToString();
                string IncidentContent = row["IncidentContent"].ToString();

                string Phone = row["Phone"].ToString();
                string AdmiMan = row["AdmiMan"].ToString();
                string AdmiDate = row["AdmiDate"].ToString();


                string CallCenterID = row["CallCenterID"].ToString();
                string RegionalID = row["RegionalID"].ToString();
                 
                #endregion

                using (IDbConnection Conn = new SqlConnection(Connection.GetConnection("8")))
                {

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@CommID", CommID);
                    parameters.Add("@CustID", CustID);
                  
                    parameters.Add("@TypeID", CorpTypeID);
                    //parameters.Add("@IncidentNum", IncidentNum);    // erp 系统生成
                    //  parameters.Add("@IncidentPlace", IncidentPlace);  //业主报事
                    parameters.Add("@IncidentMan", IncidentMan);
                    parameters.Add("@IncidentDate", IncidentDate);
                    //  parameters.Add("@IncidentMode", IncidentMode); //报事来源
                    parameters.Add("@DealLimit", DealLimit);
                    parameters.Add("@ReplyLimit", ReplyLimit);
                    parameters.Add("@IncidentContent", IncidentContent);
                    parameters.Add("@ReserveDate", ReserveDate);
                    parameters.Add("@Phone", Phone);
                    parameters.Add("@AdmiMan", AdmiMan);
                    parameters.Add("@AdmiDate", AdmiDate);
                    parameters.Add("@CallCenterID", CallCenterID);
                    parameters.Add("@RegionalID", RegionalID);
                    parameters.Add("@LocationID", LocationID);
                    parameters.Add("@IncidentImgs", IncidentImgs);
                    Conn.Execute("Proc_HSPR_IncidentAccept_InterfaceInsert_Region", parameters, null, null, CommandType.StoredProcedure);

                    //获取当前报事
                    string str = @"select TOP 1 * from Tb_HSPR_IncidentAccept 
                             where CommID = @CommID and RegionalID = @RegionalID 
                             and Phone = @Phone 
                             and IncidentContent = @IncidentContent ORDER BY IncidentDate DESC";
                    Tb_HSPR_IncidentAccept model = Conn.Query<Tb_HSPR_IncidentAccept>(str, new { CommID = CommID, RegionalID = RegionalID, Phone = Phone, IncidentContent = IncidentContent }).LastOrDefault();
                   
                    
                    #region 推送
                    PubConstant.hmWyglConnectionString = Connection.GetConnection("8");
                    PubConstant.tw2bsConnectionString = Global_Fun.GetConnectionString("PublicConnectionString");
                    Global_Var.CorpId = "1973";
                    //获取当前报事 
                    IncidentAcceptPush.SynchPushPublicIncident(model);
                    #endregion


                  //  backstr = "{\"Result\":\"true\",\"IncidentID\":\"" + model.IncidentID + "\"}";
                    backstr = "{\"Result\":\"true\",\"IncidentID\":\"" + model.IncidentID + "\",\"IncidentNum\":\"" + model.IncidentNum + "\"}";
                    log.ErrorContent = "true";
                    LogAdd(log);
                    // return JSONHelper.FromString(true, "报事成功!稍后会有人员与您联系!");
                    return backstr;
                }



            }
            catch (Exception ex)
            {
                backstr = ex.Message;
                log.ErrorContent = "false," + backstr;
                LogAdd(log);
                return JSONHelper.FromString(false, backstr);
            }



        }



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
            string str = @" select RegionalID,	CommID	, 	RegionalPlace,	RegionalMemo  from  [Tb_HSPR_IncidentRegional] where IsDelete=0 CommID = " + CommID;
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
            string sqlStr = " select LocationID,LocationName from  Tb_HSPR_IncidentLocation where CommID=@CommID and RegionalID=@RegionalID  and IsDelete=0";
            List<Tb_HSPR_IncidentLocation> list = con.Query<Tb_HSPR_IncidentLocation>(sqlStr, new { CommID = Community.CommID, RegionalID = RegionalID }).ToList<Tb_HSPR_IncidentLocation>();
            return JSONHelper.FromString<Tb_HSPR_IncidentLocation>(list);
        }


         
        /// <summary>
        /// 报事回访
        /// </summary>
        /// <param name="row"></param> 
        /// <returns></returns>
        private string SetIncidentAcceptReplyInsert(DataRow row)
        { 
            string backstr = "";
            try
            {
                #region 报事
                if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
                {
                    return JSONHelper.FromString(false, "项目ID不能为空");
                }
                if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
                {
                    return JSONHelper.FromString(false, "工单ID不能为空");
                }

                if (!row.Table.Columns.Contains("ReplyType") || string.IsNullOrEmpty(row["ReplyType"].ToString()))
                {
                    return JSONHelper.FromString(false, "回访类型不能为空"); 
                }

                if (!row.Table.Columns.Contains("RespondentsMan") || string.IsNullOrEmpty(row["RespondentsMan"].ToString()))
                {
                    return JSONHelper.FromString(false, "受访人不能为空");
                }
                if (!row.Table.Columns.Contains("ReplyMan") || string.IsNullOrEmpty(row["ReplyMan"].ToString()))
                {
                    return JSONHelper.FromString(false, "回访人不能为空");
                }

                if (!row.Table.Columns.Contains("ReplyDate") || string.IsNullOrEmpty(row["ReplyDate"].ToString()))
                {
                    return JSONHelper.FromString(false, "回访时间不能为空");
                }
                if (!row.Table.Columns.Contains("ReplyContent") || string.IsNullOrEmpty(row["ReplyContent"].ToString()))
                {
                    return JSONHelper.FromString(false, "回访内容不能为空");
                }
                if (!row.Table.Columns.Contains("ServiceQuality") || string.IsNullOrEmpty(row["ServiceQuality"].ToString()))
                {
                    return JSONHelper.FromString(false, "服务质量评价不能为空");
                }
                if (!row.Table.Columns.Contains("ReplyWay") || string.IsNullOrEmpty(row["ReplyWay"].ToString()))
                {
                    return JSONHelper.FromString(false, "回访方式不能为空");
                }
                  
                if (!row.Table.Columns.Contains("CallCenterID") || string.IsNullOrEmpty(row["CallCenterID"].ToString()))
                {
                    return JSONHelper.FromString(false, "呼叫中心工单ID不能为空");
                }

                if (!row.Table.Columns.Contains("ReplyResult") || string.IsNullOrEmpty(row["ReplyResult"].ToString()))
                {
                    return JSONHelper.FromString(false, "ReplyResult[回访结果]不能为空");
                }
                string ReplyMemo = "";  //备注
                if (row.Table.Columns.Contains("ReplyMemo") || !string.IsNullOrEmpty(row["ReplyMemo"].ToString()))
                {
                    ReplyMemo= row["ReplyMemo"].ToString();
                }


                
                #endregion
                string CommID = row["CommID"].ToString(); 
                string IncidentID = row["IncidentID"].ToString();
                string ReplyType = row["ReplyType"].ToString();
                string RespondentsMan = row["RespondentsMan"].ToString();
                string ReplyMan = row["ReplyMan"].ToString();
                string ReplyDate = row["ReplyDate"].ToString();
                string ReplyContent = row["ReplyContent"].ToString();
                string ServiceQuality = row["ServiceQuality"].ToString();
                string ReplyWay = row["ReplyWay"].ToString();
                string ReplyResult = row["ReplyResult"].ToString();    //回访结果   [成功回访] [不成功回访]
                string CallCenterID = row["CallCenterID"].ToString();

       

                using (IDbConnection Conn = new SqlConnection(Connection.GetConnection("8")))
                {

                    DynamicParameters parameters = new DynamicParameters();
                     
                    parameters.Add("@CommID ", CommID); 
                    parameters.Add("@IncidentID ", IncidentID);
                    parameters.Add("@ReplyType ", ReplyType);
                    parameters.Add("@RespondentsMan ", RespondentsMan);
                    parameters.Add("@ReplyMan ", ReplyMan);
                    parameters.Add("@ReplyDate ", ReplyDate);
                    parameters.Add("@ReplyContent ", ReplyContent);
                    parameters.Add("@ServiceQuality ", ServiceQuality);
                    parameters.Add("@ReplyWay ", ReplyWay);
                    parameters.Add("@ReplyResult ", ReplyResult);
                    parameters.Add("@ReplyMemo ", ReplyMemo);
                    parameters.Add("@CallCenterID ", CallCenterID); 
                    Conn.Execute("Proc_HSPR_IncidentReply_InterfaceInsert", parameters, null, null, CommandType.StoredProcedure);
                     
                    log.ErrorContent = "true";
                    LogAdd(log);
                    return JSONHelper.FromString(true, "回访成功!");
                }



            }
            catch (Exception ex)
            {
                backstr = ex.Message;
                log.ErrorContent = "false," + backstr;
                LogAdd(log);
                return JSONHelper.FromString(false, backstr);
            }




        }
         

        /// <summary>
        /// 报事跟进
        /// </summary>
        /// <param name="row"></param> 
        /// <returns></returns>
        private string SetIncidentCoordinateInsert(DataRow row)
        {
            string backstr = "";
            try
            {
                #region 报事
                if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
                {
                    return JSONHelper.FromString(false, "项目ID不能为空");
                }
                if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
                {
                    return JSONHelper.FromString(false, "工单ID不能为空");
                }
                if (!row.Table.Columns.Contains("CoordinateMan") || string.IsNullOrEmpty(row["CoordinateMan"].ToString()))
                {
                    return JSONHelper.FromString(false, "协调/跟进人不能为空");
                }
                if (!row.Table.Columns.Contains("CoordinateDate") || string.IsNullOrEmpty(row["CoordinateDate"].ToString()))
                {
                    return JSONHelper.FromString(false, "协调/跟进日期不能为空");
                }
                if (!row.Table.Columns.Contains("CoordinateContent") || string.IsNullOrEmpty(row["CoordinateContent"].ToString()))
                {
                    return JSONHelper.FromString(false, "协调/跟进内容不能为空");
                }
                if (!row.Table.Columns.Contains("CallCenterID") || string.IsNullOrEmpty(row["CallCenterID"].ToString()))
                {
                    return JSONHelper.FromString(false, "呼叫中心工单ID不能为空");
                }

                #endregion
             
                string CommID = row["CommID"].ToString(); 
                string IncidentID = row["IncidentID"].ToString();
                string CoordinateMan = row["CoordinateMan"].ToString();
                string CoordinateDate = row["CoordinateDate"].ToString();
                string CoordinateContent = row["CoordinateContent"].ToString();
                string CallCenterID = row["CallCenterID"].ToString();

                using (IDbConnection Conn = new SqlConnection(Connection.GetConnection("8")))
                {
                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@CommID ", CommID);
                    parameters.Add("@IncidentID ", IncidentID); 
                    parameters.Add("@CoordinateMan ", CoordinateMan);
                    parameters.Add("@CoordinateDate ", CoordinateDate);
                    parameters.Add("@CoordinateContent ", CoordinateContent); 
                    parameters.Add("@CallCenterID ", CallCenterID);
                    Conn.Execute("Proc_HSPR_IncidentCoordinate_InterFaceInsert", parameters, null, null, CommandType.StoredProcedure);
                     
                    log.ErrorContent = "true";
                    LogAdd(log);
                    return JSONHelper.FromString(true, "跟进成功!");
                } 

            }
            catch (Exception ex)
            {
                backstr = ex.Message;
                log.ErrorContent = "false," + backstr;
                LogAdd(log);
                return JSONHelper.FromString(false, backstr);
            }




        }



        /// <summary>
        /// 用户编码：CustID【必填】
        /// 小区编码：CommID[必填]
        /// 开始日期：StateDate【必填】
        /// 结束日期：EndDate【必填】
        /// 房屋编号：RoomID 【选填】
        /// 页码：PageIndex【必填】默认1
        /// 分页条数：PageSize【必填】默认20
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        #region 历史缴费
        private string GetHistoricalPaymentList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("StateDate") || string.IsNullOrEmpty(row["StateDate"].ToString()))
            {
                return JSONHelper.FromString(false, "开始日期不能为空");
            }
            if (!row.Table.Columns.Contains("EndDate") || string.IsNullOrEmpty(row["EndDate"].ToString()))
            {
                return JSONHelper.FromString(false, "结束日期不能为空");
            }
            if (AppGlobal.StrToDate(row["EndDate"].ToString()) < AppGlobal.StrToDate(row["StateDate"].ToString()))
            {
                return JSONHelper.FromString(false, "开始日期不能小于结束日期");
            }

            int PageIndex = 1;
            int PageSize = 20;
            string RoomID = "0";
            if (row.Table.Columns.Contains("RoomID"))
            {
                RoomID = row["RoomID"].ToString();
            }
            if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            string CustID = row["CustID"].ToString();
            string CommID = row["CommID"].ToString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            //构建链接字符串
            GetConnectionString(Community);

            //生成票据信息
            int rowsAffected;
            SqlParameter[] sqlstr = {
                new SqlParameter("@CommID",SqlDbType.Int),
                new SqlParameter("@CustID",SqlDbType.BigInt),
                new SqlParameter("@RoomID",SqlDbType.BigInt)
            };
            sqlstr[0].Value = Community.CommID;
            sqlstr[1].Value = CustID;
            sqlstr[2].Value = RoomID;
            new DbHelperSQLP(Global_Var.CorpSQLConnstr).RunProcedure("Proc_HSPR_CustomerBillSign_Cre", sqlstr, out rowsAffected);

            //查询历史缴费单据
            StringBuilder ss = new StringBuilder();
            ss.Append("select CommID,BillsDate,BillsAmount,ReceID,BillType,ChargeMode from view_HSPR_CustomerBillSign_Filter");
            ss.AppendFormat(" where CommID={0}", Community.CommID);
            ss.AppendFormat(" and CustID={0}", CustID);
            ss.AppendFormat("  and RoomID={0}", RoomID);
            ss.AppendFormat(" and BillsDate between '{0}' and '{1}'", row["StateDate"].ToString(), row["EndDate"].ToString());
            ss.Append(" and IsDelete=0");

            int pageCount;
            int Counts;
            DataSet ds_lishi = GetList(out pageCount, out Counts, ss.ToString(), PageIndex, PageSize, "BillsDate", 1, "BillsDate");

            return GetResult(row, RoomID, CustID, Community, ds_lishi);
        }






        private string GetResult(DataRow row, string RoomID, string CustID, Tb_Community Community, DataSet ds_lishi)
        {
            StringBuilder str = new StringBuilder();
            str.Append("{\"Result\":\"true\",\"data\":[");
            if (ds_lishi.Tables.Count > 0 && ds_lishi.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow item in ds_lishi.Tables[0].Rows)
                {
                    DataSet ds = new DataSet();
                    DataSet ds_2 = new DataSet();
                    //BillType缴费类型：1实收，2预存，3退款，4预存退款
                    //获取明细 类型为1 实收\
                    //string method = "";//缴费方式
                    //if (item["ChargeMode"].ToString()=="自助缴费"|| item["ChargeMode"].ToString() =="APP支付")
                    //{
                    //    method = "APP支付";
                    //}
                    //else
                    //{
                    //    method = "前台结算";
                    //}

                    if (item["BillType"].ToString() == "1")
                    {
                        SqlParameter[] par = {
                            new SqlParameter("@CommID",SqlDbType.Int),
                            new SqlParameter("@ReceID",SqlDbType.BigInt),
                            new SqlParameter("@ReceiptType",SqlDbType.Int)
                        };
                        par[0].Value = item["CommID"].ToString();
                        par[1].Value = item["ReceID"].ToString();
                        par[2].Value = 1;
                        ds = new DbHelperSQLP(Global_Var.CorpSQLConnstr).RunProcedure("Proc_HSPR_NewFeesReceipts_DetailFilter", par, "RetDataSet");
                    }

                    //获取明细 类型为2 、4
                    if (item["BillType"].ToString() == "2" || item["BillType"].ToString() == "4")
                    {
                        string strsql = "select  CommID,CostID,CustID,RoomID,CostName,PrecAmount as ChargeAmount,CONVERT(varchar(50), YEAR( PrecDate))+'年'+CONVERT(varchar(50), MONTH (PrecDate))+'月' as YearMonth, '预存' as 'BillType'  from  view_HSPR_PreCostsDetail_Filter where CommID=" + item["CommID"].ToString() + " and ReceID='" + item["ReceID"].ToString() + "' and IsDelete=0 and isnull(SourceType,0) = 0 ";
                        ds_2 = new DbHelperSQLP(Global_Var.CorpSQLConnstr).Query(strsql);
                    }
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        //删除多余的列
                        ds.Tables[0].Columns.Remove("FeesID");
                        ds.Tables[0].Columns.Remove("StanMemo");
                        ds.Tables[0].Columns.Remove("MeterSign");
                        ds.Tables[0].Columns.Remove("HandID");
                        ds.Tables[0].Columns.Remove("IncidentID");

                        ds.Tables[0].Columns.Remove("LeaseContID");
                        ds.Tables[0].Columns.Remove("DueAmount");
                        ds.Tables[0].Columns.Remove("OFeesMemo");
                        ds.Tables[0].Columns.Remove("FeesMemo");
                        ds.Tables[0].Columns.Remove("FeesDueDate");

                        ds.Tables[0].Columns.Remove("FeesStateDate");
                        ds.Tables[0].Columns.Remove("FeesEndDate");
                        ds.Tables[0].Columns.Remove("AccountsDueDate");
                        ds.Tables[0].Columns.Remove("RoomSign");
                        ds.Tables[0].Columns.Remove("ReceID");

                        //ds.Tables[0].Columns.Remove("LateFeeAmount");
                        ds.Tables[0].Columns.Remove("WaivAmount");
                        ds.Tables[0].Columns.Remove("PerStanAmount");
                        ds.Tables[0].Columns.Remove("TotalAmount");

                        ds.Tables[0].Columns.Add(new DataColumn("BillType"));
                        foreach (DataRow row_Item in ds.Tables[0].Rows)
                        {
                            row_Item["BillType"] = "实收";
                        }
                    }
                    //合并明细  
                    if (ds_2 != null && ds_2.Tables.Count > 0 && ds_2.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item_ds2 in ds_2.Tables[0].Rows)
                        {
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                ds.Tables[0].Rows.Add(item_ds2);
                            }
                            else
                            {
                                ds = ds_2;
                                break;
                            }


                        }
                    }

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (i == 0)
                        {
                            str.Append("{");
                        }
                        else
                        {
                            str.Append(",{");
                        }
                        str.Append("\"BillsAmount\":\"" + item["BillsAmount"] + "\",\"ChargeMode\":\"" + item["ChargeMode"] + "\",\"BillsDate\":\"" + item["BillsDate"] + "\",");
                        str.Append(GetDataForJson(ds) + "}");
                    }
                    else
                    {
                        if (i == 0)
                        {
                            str.Append("{");
                        }
                        else
                        {
                            str.Append(",{");
                        }
                        //如果没有详情需要显示，则启用此字符串else 注释
                        str.Append("\"BillsAmount\":\"" + item["BillsAmount"] + "\",\"ChargeMode\":\"" + item["ChargeMode"] + "\",\"BillsDate\":\"" + item["BillsDate"] + "\",\"Content\":[]");
                        str.Append("}");
                    }
                    i++;
                }
                str.Append("]");
            }
            else
            {
                str.Append("]");
            }
            str.Append("}");
            return str.ToString();
        }
        #endregion
        #region 公共

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="PageCount">总页数</param>
        /// <param name="Counts">总条数</param>
        /// <param name="StrCondition">执行语句</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">每页多少条</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">升序/降序</param>
        /// <param name="ID">主键</param>
        /// <returns></returns>
        internal DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort, string ID)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@FldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@FldSort", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Sort", SqlDbType.Int),
                    new SqlParameter("@StrCondition", SqlDbType.VarChar, 8000),
                    new SqlParameter("@Id", SqlDbType.VarChar, 50),
                    new SqlParameter("@PageCount", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    new SqlParameter("@Counts", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    };
            parameters[0].Value = "*";
            parameters[1].Value = PageSize;
            parameters[2].Value = PageIndex;
            parameters[3].Value = SortField;
            parameters[4].Value = Sort;
            parameters[5].Value = StrCondition;
            parameters[6].Value = ID;
            DataSet Ds = new DbHelperSQLP(Global_Var.CorpSQLConnstr).RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = AppGlobal.StrToInt(parameters[7].Value.ToString());
            Counts = AppGlobal.StrToInt(parameters[8].Value.ToString());
            return Ds;
        }

        /// <summary>
        /// 判断是否包含此字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool CheckStr(string str, string[] s)
        {
            bool bl = true;
            foreach (string item in s)
            {
                if (str == item)
                {
                    bl = false;
                }
            }
            return bl;
        } 




        /// <summary>
        /// 根据日期分组
        /// </summary>
        /// <param name="ds">查询结果集</param>
        /// <returns>JSON</returns>
        public string GetDataSetForGroupJsonn(DataSet ds)
        {
            StringBuilder sbStr = new StringBuilder();
            if (ds != null && ds.Tables.Count > 0)
            {
                string str = "";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        str += ds.Tables[0].Rows[i]["FeeDueYearMonth"];
                    }
                    if (i > 0 && CheckStr(ds.Tables[0].Rows[i]["FeeDueYearMonth"].ToString(), str.Split(',')))
                    {
                        str += "," + ds.Tables[0].Rows[i]["FeeDueYearMonth"];
                    }
                }

                sbStr.Append("{\"Result\":\"true\",");
                sbStr.Append("\"data\":[");
                if (!string.IsNullOrEmpty(str))
                {
                    int j = 0;
                    foreach (string item in str.Split(','))
                    {
                        if (j == 0)
                        {
                            sbStr.Append("{\"Title\":\"" + item + "\",\"Content\":[");
                            int i = 0;
                            foreach (var item_row in ds.Tables[0].Select(" FeeDueYearMonth='" + item + "'"))
                            {
                                if (i == 0)
                                {
                                    sbStr.Append(JSONHelper.FromDataRow(item_row));
                                }
                                else
                                {
                                    sbStr.Append("," + JSONHelper.FromDataRow(item_row));
                                }
                                i++;
                            }
                            sbStr.Append("]}");
                        }
                        else
                        {
                            sbStr.Append(",{\"Title\":\"" + item + "\",\"Content\":[");
                            int i = 0;
                            foreach (var item_row in ds.Tables[0].Select(" FeeDueYearMonth='" + item + "'"))
                            {
                                if (i == 0)
                                {
                                    sbStr.Append(JSONHelper.FromDataRow(item_row));
                                }
                                else
                                {
                                    sbStr.Append("," + JSONHelper.FromDataRow(item_row));
                                }
                                i++;
                            }
                            sbStr.Append("]}");
                        }
                        j++;
                    }
                }
                sbStr.Append("]}");
            }
            return sbStr.ToString();
        }


        private string GetDataForJson(DataSet ds)
        {
            StringBuilder sbStr = new StringBuilder();
            if (ds != null && ds.Tables.Count > 0)
            {
                string str = "";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        str += ds.Tables[0].Rows[i]["YearMonth"];
                    }
                    if (i > 0 && CheckStr(ds.Tables[0].Rows[i]["YearMonth"].ToString(), str.Split(',')))
                    {
                        str += "," + ds.Tables[0].Rows[i]["YearMonth"];
                    }
                }

                sbStr.Append("\"Content\":[");
                if (!string.IsNullOrEmpty(str))
                {
                    int j = 0;
                    foreach (string item in str.Split(','))
                    {
                        if (j == 0)
                        {
                            //sbStr.Append("{\"Title\":\"" + item + "\",\"Content\":[");
                            int i = 0;
                            foreach (var item_row in ds.Tables[0].Select(" YearMonth='" + item + "'"))
                            {
                                if (i == 0)
                                {
                                    sbStr.Append(JSONHelper.FromDataRow(item_row));
                                }
                                else
                                {
                                    sbStr.Append("," + JSONHelper.FromDataRow(item_row));
                                }
                                i++;
                            }
                            //sbStr.Append("}");
                        }
                        else
                        {
                            sbStr.Append(",");
                            int i = 0;
                            foreach (var item_row in ds.Tables[0].Select(" YearMonth='" + item + "'"))
                            {
                                if (i == 0)
                                {
                                    sbStr.Append(JSONHelper.FromDataRow(item_row));
                                }
                                else
                                {
                                    sbStr.Append("," + JSONHelper.FromDataRow(item_row));
                                }
                                i++;
                            }
                            //sbStr.Append("}");
                        }
                        j++;
                    }
                }
                sbStr.Append("]");
            }
            return sbStr.ToString();
        }



        #endregion



        public void LogAdd(Tb_HSPR_IncidentError model)
        {
            DataAccess DAccess = new DataAccess(Connection.GetConnection("8"));
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_Tb_HSPR_IncidentError_ADD";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@State,@ErrorContent,@Method,@ErrorDate,@Parameter";

            dbParams.Add("State", model.State, SqlDbType.NVarChar);
            dbParams.Add("ErrorContent", model.ErrorContent, SqlDbType.Text);
            dbParams.Add("Method", model.Method, SqlDbType.Text);
            dbParams.Add("ErrorDate", model.ErrorDate, SqlDbType.DateTime);
            dbParams.Add("Parameter", model.Parameter, SqlDbType.Text);

            DAccess.DataTable(dbParams);
        }


    }
}


#region EasyUiTree类

public class EasyUiTree
{
    /// <summary>
    /// 根据DataTable生成EasyUi Json树结构
    /// </summary>
    /// <param name="tabel">数据源</param>
    /// <param name="idCol">ID列</param>
    /// <param name="txtCol">Text列</param>
    /// <param name="rela">关系字段</param>
    /// <param name="pId">父ID的起始值</param>

    StringBuilder result = new StringBuilder();
    StringBuilder sb = new StringBuilder();

    public string GetTreeJsonByTable(DataTable tabel, string idCol, string txtCol, string rela, object pId, string KeyName, string IcoName, string BCheck)
    {
        result.Append(sb.ToString());
        sb.Clear();
        if (tabel.Rows.Count > 0)
        {
            sb.Append("[");
            string filer = string.Format("{0}='{1}'", rela, pId);
            DataRow[] rows = tabel.Select(filer);
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "\",\"checkbox\":" + BCheck + ",\"iconCls\":\"" + IcoName + "\",\"state\":\"open\",\"attributes\":{\"" + KeyName + "\":\"" + row[KeyName].ToString() + "\"}");
                    if (tabel.Select(string.Format("{0}='{1}'", rela, row[idCol])).Length > 0)
                    {
                        sb.Append(",\"children\":");
                        GetTreeJsonByTable(tabel, idCol, txtCol, rela, row[idCol], KeyName, IcoName, BCheck);
                        result.Append(sb.ToString());
                        sb.Clear();
                    }
                    result.Append(sb.ToString());
                    sb.Clear();
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
            result.Append(sb.ToString());
            sb.Clear();
        }
        return result.ToString();
    }
    /// <summary>
    /// 从DataTable中返回EasyUi 树（常用）
    /// </summary>
    /// <param name="tabel">数据表</param>
    /// <param name="idCol">主键列</param>
    /// <param name="txtCol">显示的列</param>
    /// <param name="rela">父键ID名称</param>
    /// <param name="pId">初始树节点的值</param>
    /// <param name="Attribute">点击的属性值</param>
    /// <returns></returns>
    public string GetTreeJsonByTable(DataTable tabel, string idCol, string txtCol, string rela, object pId, string Attribute)
    {
        result.Append(sb.ToString());
        sb.Clear();
        if (tabel.Rows.Count > 0)
        {
            sb.Append("[");
            string filer = string.Format("ISNULL({0},'')='{1}'", rela, pId);
            DataRow[] rows = tabel.Select(filer);
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    string strAttribute = "";
                    string[] arrKey = Attribute.Split(',');
                    for (int i = 0; i < arrKey.Length; i++)
                    {
                        strAttribute = strAttribute + ",\"" + arrKey[i].ToString() + "\":\"" + row[arrKey[i].ToString()].ToString() + "\"";
                    }
                    if (strAttribute != "") strAttribute = strAttribute.Substring(1);
                    sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "\",\"state\":\"open\",\"attributes\":{" + strAttribute + "}");
                    if (tabel.Select(string.Format("{0}='{1}'", rela, row[idCol])).Length > 0)
                    {
                        sb.Append(",\"children\":");
                        GetTreeJsonByTable(tabel, idCol, txtCol, rela, row[idCol], Attribute);
                        result.Append(sb.ToString());
                        sb.Clear();
                    }
                    result.Append(sb.ToString());
                    sb.Clear();
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
            result.Append(sb.ToString());
            sb.Clear();
        }
        return result.ToString();
    }

    /// <summary>
    /// 从DataTable中返回EasyUi 树（常用）,列直接生成在行上不用Attribute上面
    /// </summary>
    /// <param name="tabel">数据表</param>
    /// <param name="idCol">主键列</param>
    /// <param name="txtCol">显示的列</param>
    /// <param name="rela">父键ID名称</param>
    /// <param name="pId">初始树节点的值</param>
    /// <param name="Attribute">点击的属性值</param>
    /// <returns></returns>
    public string GetTreeJsonByTableDirect(DataTable tabel, string idCol, string txtCol, string rela, object pId, string Attribute)
    {
        result.Append(sb.ToString());
        sb.Clear();
        if (tabel.Rows.Count > 0)
        {
            sb.Append("[");
            string filer = string.Format("ISNULL({0},'')='{1}'", rela, pId);
            DataRow[] rows = tabel.Select(filer);
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    string strAttribute = "";
                    string[] arrKey = Attribute.Split(',');
                    for (int i = 0; i < arrKey.Length; i++)
                    {
                        strAttribute = strAttribute + ",\"" + arrKey[i].ToString() + "\":\"" + row[arrKey[i].ToString()].ToString() + "\"";
                    }
                    //if (strAttribute != "") strAttribute = strAttribute.Substring(1);
                    sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "\",\"state\":\"open\"" + strAttribute);
                    if (tabel.Select(string.Format("{0}='{1}'", rela, row[idCol])).Length > 0)
                    {
                        sb.Append(",\"children\":");
                        GetTreeJsonByTableDirect(tabel, idCol, txtCol, rela, row[idCol], Attribute);
                        result.Append(sb.ToString());
                        sb.Clear();
                    }
                    result.Append(sb.ToString());
                    sb.Clear();
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
            result.Append(sb.ToString());
            sb.Clear();
        }
        return result.ToString();
    }
    public string GetTreeJsonByTable_Asynchronous(DataTable tabel, string idCol, string txtCol, string rela, object pId, string Attribute)
    {
        result.Append(sb.ToString());
        sb.Clear();
        if (tabel.Rows.Count > 0)
        {
            sb.Append("[");
            string filer = string.Format("ISNULL({0},'')='{1}'", rela, pId);
            DataRow[] rows = tabel.Select(filer);
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    string strAttribute = "";
                    string[] arrKey = Attribute.Split(',');
                    for (int i = 0; i < arrKey.Length; i++)
                    {
                        strAttribute = strAttribute + ",\"" + arrKey[i].ToString() + "\":\"" + row[arrKey[i].ToString()].ToString() + "\"";
                    }
                    if (strAttribute != "") strAttribute = strAttribute.Substring(1);

                    if ( DataSecurity.StrToInt(row["ChildCount"].ToString()) > 0)
                    {
                        sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "\",\"state\":\"closed\",\"attributes\":{" + strAttribute + "}");
                    }
                    else
                    {
                        sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "\",\"state\":\"open\",\"attributes\":{" + strAttribute + "}");
                    }
                    result.Append(sb.ToString());
                    sb.Clear();
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
            result.Append(sb.ToString());
            sb.Clear();
        }
        return result.ToString();
    }


    public string GetTreeJsonByTable_Asynchronous1(DataTable tabel, string idCol, string txtCol, string rela, object pId, string Attribute)
    {
        result.Append(sb.ToString());
        sb.Clear();
        if (tabel.Rows.Count > 0)
        {
            sb.Append("[");
            foreach (DataRow row in tabel.Rows)
            {
                string strAttribute = "";
                string[] arrKey = Attribute.Split(',');
                for (int i = 0; i < arrKey.Length; i++)
                {
                    strAttribute = strAttribute + ",\"" + arrKey[i].ToString() + "\":\"" + row[arrKey[i].ToString()].ToString() + "\"";
                }
                if (strAttribute != "") strAttribute = strAttribute.Substring(1);

                if ( DataSecurity.StrToInt(row["IsTreeRoot"].ToString()) == 2)
                {
                    sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "\",\"state\":\"closed\",\"attributes\":{" + strAttribute + "}");
                }
                else
                {
                    sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "\",\"state\":\"open\",\"attributes\":{" + strAttribute + "}");
                }


                result.Append(sb.ToString());
                sb.Clear();
                sb.Append("},");
            }
            sb = sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            result.Append(sb.ToString());
            sb.Clear();
        }
        return result.ToString();
    }
    public string GetTreeJsonByTableChange(DataTable tabel, string idCol, string txtCol, string rela, object pId, string Attribute)
    {
        result.Append(sb.ToString());
        sb.Clear();
        if (tabel.Rows.Count > 0)
        {
            sb.Append("[");
            string filer = string.Format("ISNULL({0},'')='{1}'", rela, pId);
            DataRow[] rows = tabel.Select("1=1");
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    string strAttribute = "";
                    string[] arrKey = Attribute.Split(',');
                    for (int i = 0; i < arrKey.Length; i++)
                    {
                        strAttribute = strAttribute + ",\"" + arrKey[i].ToString() + "\":\"" + row[arrKey[i].ToString()].ToString() + "\"";
                    }
                    if (strAttribute != "") strAttribute = strAttribute.Substring(1);
                    sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "\",\"state\":\"open\",\"attributes\":{" + strAttribute + "}");
                    if (tabel.Select(string.Format("{0}='{1}'", rela, row[idCol])).Length > 0)
                    {
                        sb.Append(",\"children\":");
                        GetTreeJsonByTable(tabel, idCol, txtCol, rela, row[idCol], Attribute);
                        result.Append(sb.ToString());
                        sb.Clear();
                    }
                    result.Append(sb.ToString());
                    sb.Clear();
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
            result.Append(sb.ToString());
            sb.Clear();
        }
        return result.ToString();
    }
    public string GetTreeJsonByTableSort(DataTable tabel, string idCol, string txtCol, string rela, object pId, string Attribute, string Sort)
    {
        result.Append(sb.ToString());
        sb.Clear();
        if (tabel.Rows.Count > 0)
        {
            sb.Append("[");
            string filer = string.Format("ISNULL({0},'')='{1}'", rela, pId);
            DataRow[] rows = tabel.Select(filer, Sort);
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    string strAttribute = "";
                    string[] arrKey = Attribute.Split(',');
                    for (int i = 0; i < arrKey.Length; i++)
                    {
                        strAttribute = strAttribute + ",\"" + arrKey[i].ToString() + "\":\"" + row[arrKey[i].ToString()].ToString() + "\"";
                    }
                    if (strAttribute != "") strAttribute = strAttribute.Substring(1);
                    sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "\",\"state\":\"open\",\"attributes\":{" + strAttribute + "}");
                    if (tabel.Select(string.Format("{0}='{1}'", rela, row[idCol])).Length > 0)
                    {
                        sb.Append(",\"children\":");
                        GetTreeJsonByTableSort(tabel, idCol, txtCol, rela, row[idCol], Attribute, Sort);
                        result.Append(sb.ToString());
                        sb.Clear();
                    }
                    result.Append(sb.ToString());
                    sb.Clear();
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
            result.Append(sb.ToString());
            sb.Clear();
        }
        return result.ToString();
    }
    public string GetTreeJsonByTableSortNew(DataTable tabel, string idCol, string txtCol, string rela, object pId, string Attribute, string Sort, string count)
    {
        result.Append(sb.ToString());
        sb.Clear();
        if (tabel.Rows.Count > 0)
        {
            sb.Append("[");
            string filer = string.Format("ISNULL({0},'')='{1}'", rela, pId);
            DataRow[] rows = tabel.Select(filer, Sort);
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    string strAttribute = "";
                    string[] arrKey = Attribute.Split(',');
                    for (int i = 0; i < arrKey.Length; i++)
                    {
                        strAttribute = strAttribute + ",\"" + arrKey[i].ToString() + "\":\"" + row[arrKey[i].ToString()].ToString() + "\"";
                    }
                    if (strAttribute != "") strAttribute = strAttribute.Substring(1);
                    //注释这句是带了岗位数量的
                    //  sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "("+ row[count] + ")\",\"state\":\"open\",\"attributes\":{" + strAttribute + "}");
                    sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "\",\"state\":\"open\",\"attributes\":{" + strAttribute + "}");

                    if (tabel.Select(string.Format("{0}='{1}'", rela, row[idCol])).Length > 0)
                    {
                        sb.Append(",\"children\":");
                        GetTreeJsonByTableSortNew(tabel, idCol, txtCol, rela, row[idCol], Attribute, Sort, count);
                        result.Append(sb.ToString());
                        sb.Clear();
                    }
                    result.Append(sb.ToString());
                    sb.Clear();
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
            result.Append(sb.ToString());
            sb.Clear();
        }
        return result.ToString();
    }
    /// <summary>
    /// 从DataTable中返回EasyUi 树（常用）
    /// </summary>
    /// <param name="tabel">数据表</param>
    /// <param name="idCol">主键列</param>
    /// <param name="txtCol">显示的列</param>
    /// <param name="rela">父键ID名称</param>
    /// <param name="pId">初始树节点的值</param>
    /// <param name="Attribute">点击的属性值</param>
    /// <returns></returns>
    public string GetTreeJsonByTableNew(DataTable tabel, string idCol, string txtCol, string rela, object pId, string Attribute)
    {
        result.Append(sb.ToString());
        sb.Clear();
        if (tabel.Rows.Count > 0)
        {
            sb.Append("[");
            string filer = string.Format("ISNULL({000},'')='{1}'", rela, pId);
            DataRow[] rows = tabel.Select(filer);
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    string strAttribute = "";
                    string[] arrKey = Attribute.Split(',');
                    for (int i = 0; i < arrKey.Length; i++)
                    {
                        strAttribute = strAttribute + ",\"" + arrKey[i].ToString() + "\":\"" + row[arrKey[i].ToString()].ToString() + "\"";
                    }
                    if (strAttribute != "") strAttribute = strAttribute.Substring(1);
                    sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "\",\"state\":\"open\",\"attributes\":{" + strAttribute + "}");
                    if (tabel.Select(string.Format("{000}='{1}'", rela, row[idCol])).Length > 0)
                    {
                        sb.Append(",\"children\":");
                        GetTreeJsonByTable(tabel, idCol, txtCol, rela, row[idCol], Attribute);
                        result.Append(sb.ToString());
                        sb.Clear();
                    }
                    result.Append(sb.ToString());
                    sb.Clear();
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
            result.Append(sb.ToString());
            sb.Clear();
        }
        return result.ToString();
    }

    /// <summary>
    /// 从DataTable中返回EasyUi 树（常用）
    /// </summary>
    /// <param name="tabel">数据表</param>
    /// <param name="idCol">主键列</param>
    /// <param name="txtCol">显示的列</param>
    /// <param name="rela">父键ID名称</param>
    /// <param name="pId">初始树节点的值</param>
    /// <param name="Attribute">点击的属性值</param>
    /// <param name="state">是否展开(closed/open)</param>
    /// <returns></returns>
    public string GetStateTreeJsonByTable(DataTable tabel, string idCol, string txtCol, string rela, object pId, string Attribute, string state)
    {
        result.Append(sb.ToString());
        sb.Clear();
        if (tabel.Rows.Count > 0)
        {
            sb.Append("[");
            string filer = string.Format("ISNULL({0},'')='{1}'", rela, pId);
            DataRow[] rows = tabel.Select(filer);
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    string strAttribute = "";
                    string[] arrKey = Attribute.Split(',');
                    for (int i = 0; i < arrKey.Length; i++)
                    {
                        strAttribute = strAttribute + ",\"" + arrKey[i].ToString() + "\":\"" + row[arrKey[i].ToString()].ToString() + "\"";
                    }
                    if (strAttribute != "") strAttribute = strAttribute.Substring(1);
                    sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "\",\"state\":\"" + state + "\",\"attributes\":{" + strAttribute + "}");
                    if (tabel.Select(string.Format("{0}='{1}'", rela, row[idCol])).Length > 0)
                    {
                        sb.Append(",\"children\":");
                        GetStateTreeJsonByTable(tabel, idCol, txtCol, rela, row[idCol], Attribute, "open");
                        result.Append(sb.ToString());
                        sb.Clear();
                    }
                    result.Append(sb.ToString());
                    sb.Clear();
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
            result.Append(sb.ToString());
            sb.Clear();
        }
        return result.ToString();
    }

    public string GetStateTreeJsonByTableCloseAll(DataTable tabel, string idCol, string txtCol, string rela, object pId, string Attribute, string state)
    {
        result.Append(sb.ToString());
        sb.Clear();
        if (tabel.Rows.Count > 0)
        {
            sb.Append("[");
            string filer = string.Format("ISNULL({0},'')='{1}'", rela, pId);
            DataRow[] rows = tabel.Select(filer);
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    string strAttribute = "";
                    string[] arrKey = Attribute.Split(',');
                    for (int i = 0; i < arrKey.Length; i++)
                    {
                        strAttribute = strAttribute + ",\"" + arrKey[i].ToString() + "\":\"" + row[arrKey[i].ToString()].ToString() + "\"";
                    }
                    if (strAttribute != "") strAttribute = strAttribute.Substring(1);
                    sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "\",\"state\":\"" + state + "\",\"attributes\":{" + strAttribute + "}");
                    if (tabel.Select(string.Format("{0}='{1}'", rela, row[idCol])).Length > 0)
                    {
                        sb.Append(",\"children\":");
                        GetStateTreeJsonByTableCloseAll(tabel, idCol, txtCol, rela, row[idCol], Attribute, "closed");
                        result.Append(sb.ToString());
                        sb.Clear();
                    }
                    result.Append(sb.ToString());
                    sb.Clear();
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
            result.Append(sb.ToString());
            sb.Clear();
        }
        return result.ToString();
    }
    /// <summary>
    /// 从DataTable中返回EasyUi 树
    /// </summary>
    /// <param name="tabel">数据表</param>
    /// <param name="idCol">主键列</param>
    /// <param name="txtCol">显示的列</param>
    /// <param name="rela">父键ID名称</param>
    /// <param name="pId">父键ID值</param>
    /// <param name="KeyName">属性</param>
    /// <param name="IcoName">图标</param>
    /// <returns></returns>
    public string GetTreeJsonByTable(DataTable tabel, string idCol, string txtCol, string rela, object pId, string KeyName, string IcoName)
    {
        result.Append(sb.ToString());
        sb.Clear();
        if (tabel.Rows.Count > 0)
        {
            sb.Append("[");
            string filer = string.Format("{0}='{1}'", rela, pId);
            DataRow[] rows = tabel.Select(filer);
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "\",\"iconCls\":\"" + IcoName + "\",\"state\":\"open\",\"attributes\":{\"" + KeyName + "\":\"" + row[KeyName].ToString() + "\"}");
                    if (tabel.Select(string.Format("{0}='{1}'", rela, row[idCol])).Length > 0)
                    {
                        sb.Append(",\"children\":");
                        GetTreeJsonByTable(tabel, idCol, txtCol, rela, row[idCol], KeyName, IcoName);
                        result.Append(sb.ToString());
                        sb.Clear();
                    }
                    result.Append(sb.ToString());
                    sb.Clear();
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
            result.Append(sb.ToString());
            sb.Clear();
        }
        return result.ToString();
    }



    /// <summary>
    /// 从DataTable中返回EasyUi 树（常用）
    /// </summary>
    /// <param name="tabel">数据表</param>
    /// <param name="idCol">主键列</param>
    /// <param name="txtCol">显示的列</param>
    /// <param name="rela">父键ID名称</param>
    /// <param name="pId">初始树节点的值</param>
    /// <param name="Attribute">点击的属性值</param>
    /// <returns></returns>
    public string GetTreeJsonByTable_New(DataTable tabel, int rowCount, string idCol, string txtCol, string rela, object pId, string Attribute)
    {
        result.Append(sb.ToString());
        sb.Clear();

        if (tabel.Rows.Count > 0)
        {
            //DataRow[] rowNew=null;
            //DataRow[] rows = tabel.Select();
            //if (removeRow != null)
            //{
            //    tabel.Rows.Remove(removeRow);
            //    rowNew = tabel.Select();
            //}
            string filer = string.Format("ISNULL({0},'')='{1}'", rela, pId);
            DataRow[] rows = tabel.Select(filer);
            if (rowCount > 0)
            {
                rows = tabel.Select(filer);
            }
            else
            {
                rows = tabel.Select();
            }

            sb.Append("[");
            //string filer = string.Format("ISNULL({0},'')='{1}'", rela, pId);
            //string filerTwo = string.Format("{0}='{1}'", rela, pId);
            //if (pId.ToString() == "")
            //{
            //    rows = tabel.Select(filer);
            //}
            //else
            //{
            //    rows = tabel.Select(filerTwo);
            //}

            if (rows.Length > 0)
            {
                // var foreachRows = removeRow == null ? rows : rowNew;
                foreach (DataRow row in rows)
                {
                    string strAttribute = "";
                    string[] arrKey = Attribute.Split(',');
                    for (int i = 0; i < arrKey.Length; i++)
                    {
                        if (arrKey[i].ToString() == "indexselect")
                        {

                            strAttribute = strAttribute + ",\"" + arrKey[i].ToString() + "\":\"" + (Convert.ToInt32(row[arrKey[i].ToString()]) + 1).ToString() + "\"";
                        }
                        else
                        {
                            strAttribute = strAttribute + ",\"" + arrKey[i].ToString() + "\":\"" + row[arrKey[i].ToString()].ToString() + "\"";
                        }

                    }
                    if (strAttribute != "") strAttribute = strAttribute.Substring(1);


                    sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "\",\"state\":\"open\",\"attributes\":{" + strAttribute + "}");



                    if (row["PrentOrganCode"].ToString() == "")
                    {
                        if (tabel.Select(string.Format("{0}='{1}'", rela, row[idCol])).Length > 0)
                        {
                            sb.Append(",\"children\":");
                            GetTreeJsonByTable_New(tabel, rowCount, idCol, txtCol, rela, row[idCol], Attribute);
                            result.Append(sb.ToString());
                            sb.Clear();
                        }
                    }
                    else
                    {
                        int ss = Convert.ToInt32(row["HasComm"].ToString());
                        if (ss > 0)
                        {
                            sb.Replace("open", "closed");
                        }
                    }
                    result.Append(sb.ToString());
                    sb.Clear();
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
            result.Append(sb.ToString());
            sb.Clear();
        }
        return result.ToString();
    }
    /// <summary>
    /// 从DataTable中返回EasyUi 树（常用） by zmh 2017-09-14
    /// </summary>
    /// <param name="tabel">数据表</param>
    /// <param name="idCol">主键列</param>
    /// <param name="txtCol">显示的列</param>
    /// <param name="rela">父键ID名称</param>
    /// <param name="pId">初始树节点的值</param>
    /// <param name="Attribute">点击的属性值</param>
    /// <param name="state">是否展开(closed/open)</param>
    /// <returns></returns>
    public string GetStateTreeJsonByTable_Ex(DataTable tabel, string idCol, string txtCol, string rela, object pId, string Attribute, string state)
    {
        result.Append(sb.ToString());
        sb.Clear();
        if (tabel.Rows.Count > 0)
        {
            sb.Append("[");
            string filer = string.Format("ISNULL({0},'')='{1}'", rela, pId);
            DataRow[] rows = tabel.Select(filer);
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    string strAttribute = "";
                    string[] arrKey = Attribute.Split(',');
                    for (int i = 0; i < arrKey.Length; i++)
                    {
                        strAttribute = strAttribute + ",\"" + arrKey[i].ToString() + "\":\"" + row[arrKey[i].ToString()].ToString() + "\"";
                    }
                    if (strAttribute != "") strAttribute = strAttribute.Substring(1);
                    sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "\",\"state\":\"" + state + "\"," + strAttribute);
                    if (tabel.Select(string.Format("{0}='{1}'", rela, row[idCol])).Length > 0)
                    {
                        sb.Append(",\"children\":");
                        GetStateTreeJsonByTable_Ex(tabel, idCol, txtCol, rela, row[idCol], Attribute, "open");
                        result.Append(sb.ToString());
                        sb.Clear();
                    }
                    result.Append(sb.ToString());
                    sb.Clear();
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
            result.Append(sb.ToString());
            sb.Clear();
        }
        return result.ToString();
    }
}
#endregion

#region  DataSecurity类
public class DataSecurity
{
    #region Sql注入字符串过滤
    public static string FilteSQLStr(string Str)
    {
        string[] Pattern = { "'", " " };
        for (int i = 0; i < Pattern.Length; i++)
        {
            Str = Str.Replace(Pattern[i].ToLower(), "");
        }
        return Str;
    }
    #endregion

    #region Sql注入检查
    public static bool CheckSQL(string Str)
    {
        bool CheckPass = true;
        //如果查找到有单引号,判断下一字符，如果不是汉字，则判断为SQL注入
        if (Str.IndexOf("'") >= 0)
        {
            string[] Temp = Str.Split('\'');
            for (int i = 1; i < Temp.Length; i++)
            {
                string TempStr = Temp[i];
                if (TempStr.Length == 0)
                {
                    CheckPass = false;
                    break;
                }
                char[] c = TempStr.ToCharArray();
                if ((int)c[0] >= 0x4e00 && (int)c[0] <= 0x9fbb)
                {
                    continue;
                }
                else
                {
                    CheckPass = false;
                    break;
                }
            }
        }
        return CheckPass;
    }
    #endregion


    #region 类型转换

    public static int StrToInt(string Str)
    {
        int Ret = 0;

        try
        {
            Ret = Convert.ToInt32(Str);
        }
        catch (Exception)
        {
            Ret = 0;
        }
        return Ret;
    }



    public static short StrToInt16(string Str)
    {
        short Ret = 0;

        try
        {
            Ret = Convert.ToInt16(Str);
        }
        catch (Exception)
        {
            Ret = 0;
        }
        return Ret;
    }

    public static double StrToDouble(string Str)
    {
        double Ret = 0;

        try
        {
            bool success = double.TryParse(Str, out Ret);
        }
        catch (Exception)
        {
            Ret = 0;
        }
        return Ret;
    }


    public static decimal StrToDecimal(string Str)
    {
        decimal Ret = 0;

        try
        {
            Ret = Convert.ToDecimal(Str);
        }
        catch (Exception)
        {
            Ret = 0;
        }
        return Ret;
    }

    public static Int64 StrToLong(string Str)
    {
        Int64 Ret = 0;

        try
        {
            Ret = Convert.ToInt64(Str);
        }
        catch (Exception)
        {
            Ret = 0;
        }
        return Ret;
    }

    public static DateTime StrToDateTime(string Str)
    {
        DateTime Ret;

        try
        {
            Ret = Convert.ToDateTime(Str);
        }
        catch (Exception)
        {
            Ret = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        }
        return Ret;
    }


    public static string DateToShortDate(string Str)
    {
        string Ret;

        try
        {
            Ret = Convert.ToDateTime(Str).ToString("yyyy-MM-dd");
        }
        catch (Exception)
        {
            Ret = "";
        }
        return Ret;
    }

    #endregion
}
#endregion