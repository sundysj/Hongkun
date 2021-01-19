using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Business;
using MobileSoft.DBUtility;
using MobileSoft.Model.HSPR;
using MobileSoft.BLL.HSPR;
using MobileSoft.Common;
using System.Data;
using System.Text;
using System.Reflection;
using Dapper;
using DapperExtensions;
using System.Data.SqlClient;
using System.IO;
namespace Service
{
    /// <summary>
    /// HJ_Service 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]

    public class HJ_Service : System.Web.Services.WebService
    {
        #region CRM楼栋接口
        [WebMethod]
        public string TwBuildingService(string strCommName, List<BuildEntity> ModelList)
        {
            #region 接受参数
            int iCorpID = 0;
            string strErrMsg = "";
            string strCon = "";
            string strReturn = "";
            int iCommID = 0;
            #endregion

            #region 变量定义

            int IsSucc = 0;
            string SQLContionString = "";
            StringBuilder sListContent = new StringBuilder("");

            long BuildID = 0;  //楼栋ID
            long CommID = 0; //小区ID
            string BuildSign = "";
            string BuildName = "";//楼栋名称
            string BuildType = "";
            string BuildUses = "";
            string PropertyRights = "";
            string PropertyUses = "";
            string BuildHeight = "";
            int FloorsNum = 0;
            int UnitNum = 0;
            int HouseholdsNum = 0;
            int UnderFloorsNum = 0;
            string NamingPatterns = "";
            int BuildSNum = 0; //楼栋标识
            int PerFloorNum = 0;
            int RegionSNum = 0;//组团标识
            int IsDelete = 0;
            int PropertyYear = 0;
            DateTime CompleteDate = DateTime.Now;
            DateTime DeliveryTime = DateTime.Now;
            decimal BuildPermitArea = 0;
            decimal PropertyArea = 0;
            string MappingStatus = "";
            decimal PropertyFeeStandard = 0;
            #endregion

            #region 获取链接字符串
            try
            {
                strCon = Global_Fun.GetConnectionString("SQLConnection");
            }
            catch (Exception ex)
            {
                strReturn = ex.Message.ToString();
            }
            #region 验证小区名称

            string strSQLCommName = " and CommName = '" + strCommName + "' and isnull(IsDelete,0) = 0 ";

            DataTable dTableComm = (new Business.TWBusinRule(strCon)).HSPR_Community_Filter(strSQLCommName);

            if (dTableComm.Rows.Count > 0)
            {
                DataRow DRowCorp = dTableComm.Rows[0];
                iCorpID = AppGlobal.StrToInt(DRowCorp["CorpID"].ToString());
                iCommID = AppGlobal.StrToInt(DRowCorp["CommID"].ToString());
            }
            dTableComm.Dispose();
            #endregion
            try
            {
                if (iCorpID != 0) { SQLContionString = Connection.GetCorpCRMSQLContion(strCon, iCorpID); }
            }
            catch
            {
                strReturn = "传入的小区名称不存在！";
            }
            #endregion 

            if (!string.IsNullOrEmpty(SQLContionString))
            {
                #region 主业务
                try
                {


                    long MaxBuildid = 0;
                    MaxBuildid = (new Business.TWBusinRule(SQLContionString)).HSPR_Building_GetMaxNum(iCommID, "");



                    //DataTable dTableModel = FillDataTable(ModelList);//接收实体类转换成DataTable
                    #region 遍历List<BuildEntity> ModelList插入数据


                    // string strBuildName = ModelList.ForEach(newss;
                    BuildID = MaxBuildid + 1;

                    foreach (BuildEntity model in ModelList)
                    {

                        CommID = iCommID;
                        BuildID = (new Business.TWBusinRule(SQLContionString)).HSPR_Building_GetMaxNum(iCommID, "");
                        BuildSNum = (new Business.TWBusinRule(SQLContionString)).HSPR_Building_GetSNum(iCommID);
                        RegionSNum = 0;
                        BuildName = model.Name.ToString();
                        BuildUses = model.Number.ToString(); //明源楼栋主键

                        #region 插入楼栋信息


                        DataTable dTableBuildTemP = (new Business.TWBusinRule(SQLContionString)).HSPR_Building_Filter(" and CommID = " + iCommID + " and BuildName = '"+ BuildName+ "' and  IsDelete = 0 ");
                        if (dTableBuildTemP.Rows.Count > 0)
                        {
                            strReturn = strReturn + "strErrMsg:楼栋" + BuildName + "已存在";
                        }
                        else
                        {
                            (new Business.TWBusinRule(SQLContionString)).HSPR_Building_Insert(BuildID, CommID, BuildSign, BuildName, BuildType, BuildUses, PropertyRights, PropertyUses, BuildHeight, FloorsNum, UnitNum, HouseholdsNum, UnderFloorsNum, NamingPatterns, BuildSNum, PerFloorNum, RegionSNum, IsDelete, PropertyYear, CompleteDate, DeliveryTime, BuildPermitArea, PropertyArea, MappingStatus, PropertyFeeStandard);
                        }

                        #endregion

                    }
                    #endregion 

                    //strReturn = "楼栋更新成功!";
                    //strReturn = strReturn + " IsSucc=1";
                }
                catch (Exception ex)
                {
                    strErrMsg = ex.Message.ToString();
                    strReturn = "strErrMsg:" + strErrMsg + ";" + sListContent.ToString() + "IsSucc=" + IsSucc;
                }
                #endregion
            }
            else
            {
                strErrMsg += "链接字符串异常或为空！";
                strReturn = strErrMsg;
            }
            return strReturn;
        }
        #endregion

        #region CRM房屋接口
        [WebMethod]
        public string TwRoomService(string strCommName, List<HouseEntity> ModelList)
        {
            #region 接受参数
            int iCorpID = 0;
            string strErrMsg = "";
            string strCon = "";
            string strReturn = "";
            int iCommID = 0;
            #endregion

            #region 变量定义

            int IsSucc = 0;
            string SQLContionString = "";
            StringBuilder sListContent = new StringBuilder("");

            long RoomID = 0;
            long CommID = 0;
            string RoomSign = "";
            string RoomName = ""; ;
            int RegionSNum = 0;
            int BuildSNum = 0;
            int UnitSNum = 0;
            int FloorSNum = 0;
            int RoomSNum = 0;
            string RoomModel = "";
            string RoomType = "";
            string PropertyRights = "";
            string RoomTowards = "";
            decimal BuildArea = 0;
            decimal InteriorArea = 0;
            decimal CommonArea = 0;
            string RightsSign = "";
            string PropertyUses = "";
            int RoomState = 1;
            long ChargeTypeID = 0;
            int UsesState = 0;
            string FloorHeight = "";
            string BuildStructure = "";
            decimal PoolRatio = 0;
            string BearParameters = "";
            string Renovation = "";
            string Configuration = "";
            string Advertising = "";
            int IsDelete = 0;
            int IsSplitUnite = 0;
            decimal GardenArea = 0;
            decimal PropertyArea = 0;
            int AreaType = 0;
            decimal YardArea = 0;
            long BedTypeID = 0;
            int UseType = 0;
            DateTime getHouseStartDate = DateTime.Now;
            DateTime getHouseEndDate = DateTime.Now;
            string SaleState = "";
            int PayState = 0;
            //DateTime ContSubDate = DateTime.Now;
            //DateTime TakeOverDate = DateTime.Now;
            //DateTime ActualSubDate = DateTime.Now;
            //DateTime FittingTime = DateTime.Now;
            //DateTime StayTime = DateTime.Now;
            //DateTime PayBeginDate = DateTime.Now;
            string ContractSign = "";
            string BuildsRenovation = "";

            #endregion

            #region 获取链接字符串
            try
            {
                strCon = Global_Fun.GetConnectionString("SQLConnection");
            }
            catch (Exception ex)
            {
                strReturn = ex.Message.ToString();
            }
            #region 验证小区名称

            string strSQLCommName = " and CommName = '" + strCommName + "' and isnull(IsDelete,0) = 0 ";

            DataTable dTableComm = (new Business.TWBusinRule(strCon)).HSPR_Community_Filter(strSQLCommName);

            if (dTableComm.Rows.Count > 0)
            {
                DataRow DRowCorp = dTableComm.Rows[0];
                iCorpID = AppGlobal.StrToInt(DRowCorp["CorpID"].ToString());
                iCommID = AppGlobal.StrToInt(DRowCorp["CommID"].ToString());
            }
            dTableComm.Dispose();
            #endregion
            try
            {
                if (iCorpID != 0) { SQLContionString = Connection.GetCorpCRMSQLContion(strCon, iCorpID); }
            }
            catch
            {
                strReturn = "传入的小区名称不存在！";
            }
            #endregion
            HouseEntity Models = new HouseEntity();
            if (!string.IsNullOrEmpty(SQLContionString))
            {
                #region 主业务
                try
                {



                    DataTable dTable = (new Business.TWBusinRule(SQLContionString)).HSPR_Building_Filter(" and CommID = " + iCommID + " and  IsDelete = 0 ");


                    //DataTable dTableModel = FillDataTable(ModelList);//接收实体类转换成DataTable
                    #region 遍历List<HouseEntity> ModelList

                    foreach (HouseEntity model in ModelList)
                    {
                        Models = model;
                        RoomID = (new Business.TWBusinRule(SQLContionString)).HSPR_Room_GetMaxNum(iCommID, "");
                        CommID = iCommID;
                        //BuildSNum = TempBuildSNum += 1;
                        RegionSNum = 0;
                        if (model.Purpose == "别墅")
                        {
                            UnitSNum = 1;
                            FloorSNum = 1;
                        }
                        else
                        {
                            try
                            {
                                UnitSNum = AppGlobal.StrToInt(model.Number.Substring(model.Number.Length - 6, 1));
                                FloorSNum = AppGlobal.StrToInt(model.Number.Substring(model.Number.Length - 4, 2));
                            }
                            catch
                            {
                                UnitSNum = 1;
                                FloorSNum = 1;
                            }
                        }
                        RoomSNum = AppGlobal.StrToInt(model.Number.Substring(model.Number.Length - 2));

                        RoomSign = model.Number;//房屋编号
                        RoomName = model.HouseName;//明源房间名称
                                                   //RoomName = model.Number;//房屋名称
                        PropertyRights = "自有产权";//产权性质
                        BuildArea = model.BuildArea;//产权面积
                        InteriorArea = model.ForecastSetArea;//套内面积
                        string ActualSubDate = DateTime.Now.ToString();//明源交楼时间
                        string ContSubDate = DateTime.Now.ToString();//明源交楼时间
                        try
                        {
                            ActualSubDate = DateTime.Parse(model.ActualDate.ToString()).ToString();
                            ContSubDate = DateTime.Parse(model.FactDate.ToString()).ToString();

                        }
                        catch
                        {
                            ActualSubDate = DateTime.Now.ToString();
                            ContSubDate = DateTime.Now.ToString();
                        }
                        if (DateTime.Parse(model.FactDate.ToString()).Year < 2000)
                        {

                            ContSubDate = "";
                        }
                        if (DateTime.Parse(model.ActualDate.ToString()).Year < 2000)
                        {

                            ActualSubDate = "";
                        }
                        string PayBeginDate = ActualSubDate.ToString();


                        //PayBeginDate = model.FactDate;//明源交楼时间也是开始收费时间
                        CommonArea = 0; //公摊面积
                        PropertyUses = model.Purpose;//使用性质
                                                     //if (model.Purpose != "住宅")
                                                     //{ RoomState = 3; }//使用状态
                        UsesState = 1;
                        BuildsRenovation = model.Number; //保存明源房屋主键
                        string str = "BuildUses = '" + model.BuildNumber.ToString() + "' ";
                        DataRow DR = dTable.Select(str)[0];
                        if (DR.Table.Rows.Count > 0)
                        {
                            BuildSNum = AppGlobal.StrToInt(DR["BuildSNum"].ToString());
                        }
                        else
                        {
                            BuildSNum = 0;
                        }



                        DataTable dTableBuildTemP = (new Business.TWBusinRule(SQLContionString)).HSPR_Room_Filter(" and CommID = " + iCommID + " and RoomSign = '" + RoomSign + "' and  IsDelete = 0 ");
                        if (dTableBuildTemP.Rows.Count > 0)
                        {
                            strReturn = strReturn + "strErrMsg:房屋" + RoomSign + "已存在";
                        }
                        else
                        {


                            #region 插入房屋信息
                            (new Business.TWBusinRule(SQLContionString)).HSPR_Room_Insert(RoomID, CommID, RoomSign, RoomName, RegionSNum, BuildSNum, UnitSNum, FloorSNum, RoomSNum, RoomModel, RoomType, PropertyRights, RoomTowards, BuildArea, InteriorArea, CommonArea,
                           RightsSign, PropertyUses, null, ChargeTypeID, UsesState, FloorHeight, BuildStructure, PoolRatio, BearParameters, Renovation, Configuration, Advertising, IsDelete, IsSplitUnite, GardenArea,
                           PropertyArea, AreaType, YardArea, BedTypeID, UseType, getHouseStartDate, getHouseEndDate, SaleState, PayState,
                           ContSubDate, null, ActualSubDate, null, null, PayBeginDate, ContractSign, BuildsRenovation);
                            #endregion
                        }



                    }
                    #endregion 

                    strReturn = "房屋更新成功!";
                    strReturn = strReturn + " IsSucc=1";
                }
                catch (Exception ex)
                {
                    strErrMsg = ex.Message.ToString() + ex.StackTrace;
                    strReturn = "strErrMsg:" + strErrMsg + ";" + sListContent.ToString() + "IsSucc=" + IsSucc + ";Models=" + Models.Number + "aaa=" + Models.Number.Length.ToString();
                }
                #endregion
            }
            else
            {
                strErrMsg += "链接字符串异常或为空！";
                strReturn = strErrMsg;
            }
            return strReturn;
        }
        #endregion

        #region CRM业主接口
        [WebMethod]
        public string TwCustomerService(string strCommName, List<CustomerEntity> ModelList)
        {
            #region 接受参数
            int iCorpID = 1000;
            string strErrMsg = "";
            string strCon = "";
            string strReturn = "";
            int iCommID = 0;
            #endregion

            #region 变量定义

            int IsSucc = 0;
            string SQLContionString = "";
            StringBuilder sListContent = new StringBuilder("");

            long CustID = 0;
            int CommID = 0;
            long CustTypeID = 0;
            string CustName = "";
            string FixedTel = "";
            string MobilePhone = "";
            string FaxTel = "";
            string EMail = "";
            string Surname = "";
            string Name = "";
            string Sex = "";
            DateTime Birthday = System.DateTime.Now;
            string Nationality = "";
            string WorkUnit = "";
            string PaperName = "";
            string PaperCode = "";
            string PassSign = "";
            string LegalRepr = "";
            string LegalReprTel = "";
            string Charge = "";
            string ChargeTel = "";
            string Linkman = "";
            string LinkmanTel = "";
            string BankName = "";
            string BankIDs = "";
            string BankAccount = "";
            string BankAgreement = "";
            string InquirePwd = "";
            string InquireAccount = "";
            string Memo = "";
            int IsUnit = 0;
            int IsDelete = 0;
            string Address = "";
            string PostCode = "";
            string Recipient = "";
            string Hobbies = "";
            string Job = "";
            DateTime SendCardDate = System.DateTime.Now;
            int IsSendCard = 0;

            long CustomerRoomID = 0;
            string HouseNumber = "";
            long LiveID = 0;
            int LiveType = 1;
            #endregion

            #region 获取链接字符串
            try
            {
                strCon = Global_Fun.GetConnectionString("SQLConnection");
            }
            catch (Exception ex)
            {
                strReturn = ex.Message.ToString();
            }
            #region 验证小区名称

            string strSQLCommName = " and CommName = '" + strCommName + "' and isnull(IsDelete,0) = 0 ";

            DataTable dTableComm = (new Business.TWBusinRule(strCon)).HSPR_Community_Filter(strSQLCommName);

            if (dTableComm.Rows.Count > 0)
            {
                DataRow DRowCorp = dTableComm.Rows[0];
                iCorpID = AppGlobal.StrToInt(DRowCorp["CorpID"].ToString());
                iCommID = AppGlobal.StrToInt(DRowCorp["CommID"].ToString());
            }
            dTableComm.Dispose();
            #endregion
            try
            {
                if (iCorpID != 0) { SQLContionString = Connection.GetCorpCRMSQLContion(strCon, iCorpID); }
            }
            catch
            {
                strReturn = "传入的小区名称不存在！";
            }
            #endregion 

            if (!string.IsNullOrEmpty(SQLContionString))
            {
                #region 主业务
                try
                {



                    #region 遍历List<CustomerEntity> ModelList
                    foreach (CustomerEntity model in ModelList)
                    {
                        CustID = (new Business.TWBusinRule(SQLContionString)).HSPR_Customer_GetMaxNum(iCommID, "");
                        CustomerRoomID = 0;

                        CommID = iCommID;
                        CustName = model.Name;
                        MobilePhone = model.Mobile;
                        PaperName = model.CertificateType;
                        PaperCode = model.PersonID;
                        LinkmanTel = model.Phone;
                        Address = model.Address;



                        DataTable dTableRoom = (new Business.TWBusinRule(SQLContionString)).HSPR_Room_Filter("and CommID = " + iCommID + " and IsDelete = 0  and RoomSign='" + model.CustomerNumber + "'");

                        if (dTableRoom.Rows.Count > 0)
                        {
                            CustomerRoomID = AppGlobal.StrToLong(dTableRoom.Rows[0]["RoomID"].ToString());
                        }


                        LiveID = AppGlobal.StrToLong(BulidAutoDateCode(4));
                      

                        DataTable dTableCustP = (new Business.TWBusinRule(SQLContionString)).HSPR_Customer_Filter(" and CommID = " + iCommID + " and CustName like '%" + CustName + "%' and PaperCode like '%"+ PaperCode + "%'  and   IsDelete = 0 ");
                        if (dTableCustP.Rows.Count > 0)
                        {
                            CustID = AppGlobal.StrToLong(dTableCustP.Rows[0]["CustID"].ToString());
                        }
                        else
                        {
                            (new Business.TWBusinRule(SQLContionString)).HSPR_Customer_Insert(CustID, CommID, CustTypeID, CustName, FixedTel, MobilePhone, FaxTel, EMail, Surname, Name, Sex, Birthday, Nationality, WorkUnit, PaperName, PaperCode, PassSign, LegalRepr, LegalReprTel, Charge, ChargeTel, Linkman, LinkmanTel, BankName, BankIDs, BankAccount, BankAgreement, InquirePwd, InquireAccount, Memo, IsUnit, IsDelete, Address, PostCode, Recipient, Hobbies, Job, SendCardDate, IsSendCard);   
                        }
                        DataTable dTableCustP2 = (new Business.TWBusinRule(SQLContionString)).HSPR_CustomerLive_Filter(" and CustID="+CustID+" and RoomID = "+CustomerRoomID+" and IsActive = 1 ");
                        if (dTableCustP2.Rows.Count < 1)
                        {
                            (new Business.TWBusinRule(SQLContionString)).HSPR_CustomerLive_InsUpdate(LiveID, CustomerRoomID, iCommID, CustID, LiveType);
                        }
                       
                    }
                    #endregion 

                    strReturn = "业主更新成功!";
                    strReturn = strReturn + " IsSucc=1";

                    //strReturn = "房屋更新成功!";
                    //strReturn = strReturn + " IsSucc=1";
                }
                catch (Exception ex)
                {
                    strErrMsg = ex.Message.ToString();
                    strReturn = "strErrMsg:" + strErrMsg + ";" + sListContent.ToString() + "IsSucc=" + IsSucc;
                }
                #endregion
            }
            else
            {
                strErrMsg += "链接字符串异常或为空！";
                strReturn = strErrMsg;
            }
            return strReturn;
        }
        #endregion

        #region CRM联名业主
        [WebMethod]
        public string TwCustomerMemberService(string strCommName, List<CustomerMemberEntity> ModelList)
        {
            #region 接受参数
            int iCorpID = 1000;
            string strErrMsg = "";
            string strCon = "";
            string strReturn = "";
            int iCommID = 0;
            #endregion

            #region 变量定义

            int IsSucc = 0;
            string SQLContionString = "";
            StringBuilder sListContent = new StringBuilder("");


            long CustID = 0;
            long RoomID = 0;
            string Surname = "";
            string Name = "";
            string Sex = "";
            DateTime Birthday = DateTime.Now;
            string Nationality = "";
            string WorkUnit = "";
            string PaperName = "";
            string PaperCode = "";
            string PassSign = "";
            string MobilePhone = "";
            string Relationship = "";
            DateTime StayTime = DateTime.Now;
            string ChargeCause = "";
            DateTime ChargeTime = DateTime.Now;
            string InquirePwd = "";
            string InquireAccount = "";
            string Memo = "";
            string MemberCode = "";
            string MemberName = "";
            string Job = "";
            int IsDelete = 0;
            string Linkman = "";
            string LinkManTel = "";
            string FixedTel = "";

            DateTime SendCardDate = System.DateTime.Now;
            
            #endregion

            #region 获取链接字符串
            try
            {
                strCon = Global_Fun.GetConnectionString("SQLConnection");
            }
            catch (Exception ex)
            {
                strReturn = ex.Message.ToString();
            }
            #region 验证小区名称

            string strSQLCommName = " and CommName = '" + strCommName + "' and isnull(IsDelete,0) = 0 ";

            DataTable dTableComm = (new Business.TWBusinRule(strCon)).HSPR_Community_Filter(strSQLCommName);

            if (dTableComm.Rows.Count > 0)
            {
                DataRow DRowCorp = dTableComm.Rows[0];
                iCorpID = AppGlobal.StrToInt(DRowCorp["CorpID"].ToString());
                iCommID = AppGlobal.StrToInt(DRowCorp["CommID"].ToString());
            }
            dTableComm.Dispose();
            #endregion
            try
            {
                if (iCorpID != 0) { SQLContionString = Connection.GetCorpCRMSQLContion(strCon, iCorpID); }
            }
            catch
            {
                strReturn = "传入的小区名称不存在！";
            }
            #endregion 
            if (!string.IsNullOrEmpty(SQLContionString))
            {



                #region 遍历List<CustomerMemberEntity> ModelList
                foreach (CustomerMemberEntity model in ModelList)
                {

                    //DataRow Dr = dTableCustomerLive.Select("RoomSign='" + model.CustomerNumber + "'")[0];


                    DataTable dTableCustomerLive = (new Business.TWBusinRule(SQLContionString)).HSPR_CustomerLive_Filter("and CommID = " + iCommID + " and RoomSign='" + model.CustomerNumber + "' and IsDelete = 0 and IsActive = 1");
                    (new Business.TWBusinRule(SQLContionString)).HSPR_CustomerUpdate_HJ(iCommID, model.CustomerNumber, model.Name, model.PersonID, model.Mobile);
                    if (dTableCustomerLive.Rows.Count > 0)
                    {
                        DataRow Dr = dTableCustomerLive.Rows[0];
                        RoomID = AppGlobal.StrToLong(Dr["RoomID"].ToString());
                        CustID = AppGlobal.StrToLong(Dr["CustID"].ToString());
                        (new Business.TWBusinRule(SQLContionString)).HSPR_Household_Insert(iCommID, CustID, RoomID, Surname, "", model.Sex, null, Nationality, WorkUnit, model.CertificateType, model.PersonID, PassSign, model.Mobile, "0013", StayTime, ChargeCause, ChargeTime, InquirePwd, InquireAccount, Memo, MemberCode, model.Name, "", 0, model.Name, model.Phone, "");
                    }

                       
                    
                    strReturn += strCommName+"项目,"+model.Name +model.PersonID+ "联名业主更新成功!";
                    strReturn = strReturn + " IsSucc=1";

                    //if (dTableCustomerLive.Rows.Count > 0)
                    //{
                    //    //    CustID = (new Business.TWBusinRule(SQLContionString)).HSPR_Customer_GetMaxNum(iCommID, "");
                    //    DataRow Dr = dTableCustomerLive.Rows[0];
                    //    CustID = AppGlobal.StrToLong(Dr["CustID"].ToString());
                    //    string CustName = Dr["CustName"].ToString() + "," + model.Name;
                    //    MobilePhone = Dr["MobilePhone"].ToString() + "," + model.Mobile;
                    //    PaperCode = Dr["PaperCode"].ToString() + "," + model.PersonID;
                    //    string LinkmanTel = Dr["LinkmanTel"].ToString() + "," + model.Phone;
                    //    string Address = model.Address;

                    //    RoomID = AppGlobal.StrToLong(Dr["RoomID"].ToString());



                    //    //DataTable dTableCustP = (new Business.TWBusinRule(SQLContionString)).HSPR_Customer_Filter(" and CommID = " + iCommID + " and CustName like '%" + model.Name + "%' and PaperCode like '%" + model.PersonID + "%'  and   IsDelete = 0 ");
                    //    //if (dTableCustP.Rows.Count >0 )
                    //    //{

                    //    //    strErrMsg += model.Name + "," + model.PersonID + ",已存在！";
                    //    //    strReturn = strErrMsg;
                    //    //}
                    //    //else
                    //    //{
                    //        //IDbConnection con = new SqlConnection(SQLContionString);
                    //        //StringBuilder sb = new StringBuilder();
                    //        //sb.Append("update Tb_HSPR_Customer set CustName = '" + CustName + "' , MobilePhone = '" + MobilePhone + "'");
                    //        //sb.Append(",PaperCode = '" + PaperCode + "' ");
                    //        //sb.Append(" where CommID = '" + iCommID + "' and CustID = '" + CustID + "'");
                    //        //con.Execute(sb.ToString());
                    //        //sb.Clear();

                    //        //con.Dispose();
                    //        (new Business.TWBusinRule(SQLContionString)).HSPR_CustomerUpdate_HJ(iCommID, CustID, CustName, PaperCode, MobilePhone);

                           

                    //        //strReturn += model.Name+"联名业主更新成功!";
                    //        //strReturn = strReturn + " IsSucc=1";
                    //    //}


                    //}





                }
                #endregion

            }
            else
            {
                strErrMsg += "链接字符串异常或为空！";
                strReturn = strErrMsg;
            }
            return strReturn;
        }
        #endregion

        #region  CRM记录明源流程归档ID信息
        /// <summary>
        /// 记录明源流程归档ID信息
        /// </summary>
        /// <param name="ProcessGUID">ERP传到天问接口的唯一标识GUID</param>
        /// <param name="CommName">归档项目</param>
        /// <param name="ProcessTime">归档时间</param>
        /// <param name="ProcessUserName">归档人</param>
        /// <returns></returns>
        [WebMethod]
        public string SetHJMYFlowArchives(string ProcessGUID, string CommName, string ProcessTime, string ProcessUserName) {
            string strErrMsg = "";
            string strCon = "";
            try
            {
                strCon = Global_Fun.GetConnectionString("ConnectionSql_HJ");
            }
            catch (Exception ex)
            {
                strErrMsg = ex.Message.ToString();
            } 
            try
            {
                using (IDbConnection Conn = new SqlConnection(strCon))
                {

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ProcessGUID", ProcessGUID);
                    parameters.Add("@CommName", CommName);
                    parameters.Add("@ProcessTime", ProcessTime);
                    parameters.Add("@ProcessUserName", ProcessUserName);

                    Conn.Execute("Proc_Tb_HSPR_SetHJMYFlowArchives_ADD", parameters, null, null, CommandType.StoredProcedure); 
                }
                strErrMsg = "成功";
            }
            catch (Exception ex)
            {

                strErrMsg = "失败|" + ex.Message.ToString();
            }

            WriteLog("合景同步明源Log", "合景同步明源：SetHJMYFlowArchives "+strErrMsg);


            return strErrMsg;
        }
        #endregion

        private string BulidAutoDateCode(int length)
        {
            System.Threading.Thread.Sleep(5);
            System.DateTime now = System.DateTime.Now;
            string strRe = now.ToString("yyyyMMddhhmmss") + BulidAutoCode("0123456789", length);

            return strRe;
        }
        private string BulidAutoCode(string seed, int length)
        {
            //申明变量
            string outRandomSting = "";
            string strSeed = seed;
            int seedLen;	//= seed.Length;
            int len = length;
            //处理变量
            if (strSeed == null || strSeed.Trim() == "")
            {
                strSeed = "0123456789";
                seedLen = strSeed.Length;
            }
            else
            {
                seedLen = strSeed.Length;
            }

            //开始产生要求长度的随机字符串
            while (len > 0)
            {
                //线程阻滞 10 毫秒后产生随机数,因为这里采用与时间相关的默认种子
                System.Threading.Thread.Sleep(10);
                Random rm = new Random();
                //rm.Next(min,max)是包括 [min,max) 的半开半闭区间
                outRandomSting += strSeed.Substring(rm.Next(0, seedLen), 1);
                len--;
            }
            return outRandomSting;
        }

        #region 明源实体类

        #region 楼栋实体类
        /// <summary>
        /// 楼栋实体类
        /// </summary>
        public partial class BuildEntity
        {

            private string numberField;

            private string otherName1Field;

            private string communityNameField;

            private string otherName2Field;

            private string communityNumberField;

            private string nameField;

            private string saleNameField;

            private string streetField;

            private string addressField;

            private System.DateTime startBuildDateField;

            private System.DateTime joinDateField;

            private decimal planBuildAreaField;

            private decimal buildAreaField;

            private decimal overBuildAreaField;

            private decimal underBuildAreaField;

            private decimal houseNumField;

            private decimal planUseAreaField;

            private decimal useAreaField;

            private string purposeField;

            private string descriptionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Number
            {
                get
                {
                    return this.numberField;
                }
                set
                {
                    this.numberField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string OtherName1
            {
                get
                {
                    return this.otherName1Field;
                }
                set
                {
                    this.otherName1Field = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string CommunityName
            {
                get
                {
                    return this.communityNameField;
                }
                set
                {
                    this.communityNameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string OtherName2
            {
                get
                {
                    return this.otherName2Field;
                }
                set
                {
                    this.otherName2Field = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string CommunityNumber
            {
                get
                {
                    return this.communityNumberField;
                }
                set
                {
                    this.communityNumberField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string SaleName
            {
                get
                {
                    return this.saleNameField;
                }
                set
                {
                    this.saleNameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Street
            {
                get
                {
                    return this.streetField;
                }
                set
                {
                    this.streetField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Address
            {
                get
                {
                    return this.addressField;
                }
                set
                {
                    this.addressField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public System.DateTime StartBuildDate
            {
                get
                {
                    return this.startBuildDateField;
                }
                set
                {
                    this.startBuildDateField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public System.DateTime JoinDate
            {
                get
                {
                    return this.joinDateField;
                }
                set
                {
                    this.joinDateField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal PlanBuildArea
            {
                get
                {
                    return this.planBuildAreaField;
                }
                set
                {
                    this.planBuildAreaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal BuildArea
            {
                get
                {
                    return this.buildAreaField;
                }
                set
                {
                    this.buildAreaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal OverBuildArea
            {
                get
                {
                    return this.overBuildAreaField;
                }
                set
                {
                    this.overBuildAreaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal UnderBuildArea
            {
                get
                {
                    return this.underBuildAreaField;
                }
                set
                {
                    this.underBuildAreaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal HouseNum
            {
                get
                {
                    return this.houseNumField;
                }
                set
                {
                    this.houseNumField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal PlanUseArea
            {
                get
                {
                    return this.planUseAreaField;
                }
                set
                {
                    this.planUseAreaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal UseArea
            {
                get
                {
                    return this.useAreaField;
                }
                set
                {
                    this.useAreaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Purpose
            {
                get
                {
                    return this.purposeField;
                }
                set
                {
                    this.purposeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Description
            {
                get
                {
                    return this.descriptionField;
                }
                set
                {
                    this.descriptionField = value;
                }
            }
        }
        #endregion

        #region 房间实体类
        /// <summary>
        /// 房间实体类
        /// </summary>
        public partial class HouseEntity
        {
            private string houseNameField;//地产原房屋名称

            private string simpleHouseNumField;

            private string buildNumberField;

            private string houseNumerField;//在明源业主集合里面有对应字段

            private string numberField;

            private string houseTakeStatusField;

            private System.DateTime openingDateField;

            private System.DateTime factDateField;

            private System.DateTime actualDateField;

            private decimal buildAreaField;

            private decimal actualBuiltupAreaField;

            private decimal forecastBuiltupAreaField;

            private decimal forecastSetAreaField;

            private decimal actualSetAreaField;

            private decimal gardenAreaField;

            private string houseShapeField;

            private string directionField;

            private string addressField;

            private string purposeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string HouseName
            {
                get
                {
                    return this.houseNameField;
                }
                set
                {
                    this.houseNameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string SimpleHouseNum
            {
                get
                {
                    return this.simpleHouseNumField;
                }
                set
                {
                    this.simpleHouseNumField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string BuildNumber
            {
                get
                {
                    return this.buildNumberField;
                }
                set
                {
                    this.buildNumberField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string HouseNumer
            {
                get
                {
                    return this.houseNumerField;
                }
                set
                {
                    this.houseNumerField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Number
            {
                get
                {
                    return this.numberField;
                }
                set
                {
                    this.numberField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string HouseTakeStatus
            {
                get
                {
                    return this.houseTakeStatusField;
                }
                set
                {
                    this.houseTakeStatusField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public System.DateTime OpeningDate
            {
                get
                {
                    return this.openingDateField;
                }
                set
                {
                    this.openingDateField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public System.DateTime FactDate
            {
                get
                {
                    return this.factDateField;
                }
                set
                {
                    this.factDateField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public System.DateTime ActualDate
            {
                get
                {
                    return this.actualDateField;
                }
                set
                {
                    this.actualDateField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal BuildArea
            {
                get
                {
                    return this.buildAreaField;
                }
                set
                {
                    this.buildAreaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal ActualBuiltupArea
            {
                get
                {
                    return this.actualBuiltupAreaField;
                }
                set
                {
                    this.actualBuiltupAreaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal ForecastBuiltupArea
            {
                get
                {
                    return this.forecastBuiltupAreaField;
                }
                set
                {
                    this.forecastBuiltupAreaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal ForecastSetArea
            {
                get
                {
                    return this.forecastSetAreaField;
                }
                set
                {
                    this.forecastSetAreaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal ActualSetArea
            {
                get
                {
                    return this.actualSetAreaField;
                }
                set
                {
                    this.actualSetAreaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal GardenArea
            {
                get
                {
                    return this.gardenAreaField;
                }
                set
                {
                    this.gardenAreaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string HouseShape
            {
                get
                {
                    return this.houseShapeField;
                }
                set
                {
                    this.houseShapeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Direction
            {
                get
                {
                    return this.directionField;
                }
                set
                {
                    this.directionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Address
            {
                get
                {
                    return this.addressField;
                }
                set
                {
                    this.addressField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Purpose
            {
                get
                {
                    return this.purposeField;
                }
                set
                {
                    this.purposeField = value;
                }
            }
        }
        #endregion

        #region 业主(客户)实体类
        /// <summary>
        /// 业主(客户)实体类
        /// </summary>
        public partial class CustomerEntity
        {

            private CustomerHouseEntity[] customerHouseEntity;

            private string customerNumberField;

            private string organizationNumberField;

            private string nameField;

            private string customerTypeField;

            private string certificateTypeField;

            private string personIDField;

            private string addressField;

            private string mobileField;

            private string phoneField;

            private string phone2Field;

            private string emailField;

            private string workPlaceField;

            private string sexField;

            private string marryStateField;

            private string customerCountryField;

            private string degreeField;

            private string customerOccpField;

            private string corporationField;

            private string compTypeField;

            private string licenseNumField;

            private bool isOwnerField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("CustomerHouses")]
            public CustomerHouseEntity[] CustomerHouseEntity
            {
                get
                {
                    return this.customerHouseEntity;
                }
                set
                {
                    this.customerHouseEntity = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string CustomerNumber
            {
                get
                {
                    return this.customerNumberField;
                }
                set
                {
                    this.customerNumberField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string OrganizationNumber
            {
                get
                {
                    return this.organizationNumberField;
                }
                set
                {
                    this.organizationNumberField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string CustomerType
            {
                get
                {
                    return this.customerTypeField;
                }
                set
                {
                    this.customerTypeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string CertificateType
            {
                get
                {
                    return this.certificateTypeField;
                }
                set
                {
                    this.certificateTypeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string PersonID
            {
                get
                {
                    return this.personIDField;
                }
                set
                {
                    this.personIDField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Address
            {
                get
                {
                    return this.addressField;
                }
                set
                {
                    this.addressField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Mobile
            {
                get
                {
                    return this.mobileField;
                }
                set
                {
                    this.mobileField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Phone
            {
                get
                {
                    return this.phoneField;
                }
                set
                {
                    this.phoneField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Phone2
            {
                get
                {
                    return this.phone2Field;
                }
                set
                {
                    this.phone2Field = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Email
            {
                get
                {
                    return this.emailField;
                }
                set
                {
                    this.emailField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string WorkPlace
            {
                get
                {
                    return this.workPlaceField;
                }
                set
                {
                    this.workPlaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Sex
            {
                get
                {
                    return this.sexField;
                }
                set
                {
                    this.sexField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string MarryState
            {
                get
                {
                    return this.marryStateField;
                }
                set
                {
                    this.marryStateField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string CustomerCountry
            {
                get
                {
                    return this.customerCountryField;
                }
                set
                {
                    this.customerCountryField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Degree
            {
                get
                {
                    return this.degreeField;
                }
                set
                {
                    this.degreeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string CustomerOccp
            {
                get
                {
                    return this.customerOccpField;
                }
                set
                {
                    this.customerOccpField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Corporation
            {
                get
                {
                    return this.corporationField;
                }
                set
                {
                    this.corporationField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string CompType
            {
                get
                {
                    return this.compTypeField;
                }
                set
                {
                    this.compTypeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string LicenseNum
            {
                get
                {
                    return this.licenseNumField;
                }
                set
                {
                    this.licenseNumField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool IsOwner
            {
                get
                {
                    return this.isOwnerField;
                }
                set
                {
                    this.isOwnerField = value;
                }
            }
        }
        #endregion

        #region 明源客户房间列表实体类
        /// <summary>
        /// 明源客户房间列表实体类
        /// </summary>
        public partial class CustomerHouseEntity
        {
            private string houseNumberField;

            private string buildNumberField;

            private System.DateTime inDateField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string HouseNumber
            {
                get
                {
                    return this.houseNumberField;
                }
                set
                {
                    this.houseNumberField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string BuildNumber
            {
                get
                {
                    return this.buildNumberField;
                }
                set
                {
                    this.buildNumberField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public System.DateTime InDate
            {
                get
                {
                    return this.inDateField;
                }
                set
                {
                    this.inDateField = value;
                }
            }
        }
        #endregion

        #region 子业主
        public partial class CustomerMemberEntity
        {


            private string customerRelation;

            private string customerNumber;
            private string organizationNumber;
            private string name;
            private string customerType;
            private string certificateType;
            private string personID;
            private string address;
            private string mobile;
            private string phone;
            private string phone2;
            private string sex;
            private string corporation;
            private string licenseNum;
            private string isOwner;


            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string CustomerRelation {
                get
                {
                    return this.customerRelation;
                }
                set
                {
                    this.customerRelation = value;
                }
            }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string CustomerNumber {
                get
                {
                    return this.customerNumber;
                }
                set
                {
                    this.customerNumber = value;
                }
            }

            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string OrganizationNumber {
                get
                {
                    return this.organizationNumber;
                }
                set
                {
                    this.organizationNumber = value;
                }
            }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.name;
                }
                set
                {
                    this.name = value;
                }
            }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string CustomerType
            {
                get
                {
                    return this.customerType;
                }
                set
                {
                    this.customerType = value;
                }
            }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string CertificateType
            {
                get
                {
                    return this.certificateType;
                }
                set
                {
                    this.certificateType = value;
                }
            }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string PersonID
            {
                get
                {
                    return this.personID;
                }
                set
                {
                    this.personID = value;
                }
            }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Address
            {
                get
                {
                    return this.address;
                }
                set
                {
                    this.address = value;
                }
            }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Mobile
            {
                get
                {
                    return this.mobile;
                }
                set
                {
                    this.mobile = value;
                }
            }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Phone
            {
                get
                {
                    return this.phone;
                }
                set
                {
                    this.phone = value;
                }
            }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Phone2
            {
                get
                {
                    return this.phone2;
                }
                set
                {
                    this.phone2 = value;
                }
            }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Sex
            {
                get
                {
                    return this.sex;
                }
                set
                {
                    this.sex = value;
                }
            }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Corporation
            {
                get
                {
                    return this.corporation;
                }
                set
                {
                    this.corporation = value;
                }
            }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string LicenseNum
            {
                get
                {
                    return this.licenseNum;
                }
                set
                {
                    this.licenseNum = value;
                }
            }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string IsOwner
            {
                get
                {
                    return this.isOwner;
                }
                set
                {
                    this.isOwner = value;
                }
            }
        }
        #endregion

        #endregion

        #region 实体类转换成DataTable

        /// <summary>
        /// 实体类转换成DataSet
        /// </summary>
        /// <param name="modelList">实体类列表</param>
        /// <returns></returns>
        public DataSet FillDataSet(List<string> modelList)
        {
            if (modelList == null || modelList.Count == 0)
            {
                return null;
            }
            else
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(FillDataTable(modelList));
                return ds;
            }
        }

        /// <summary>
        /// 实体类转换成DataTable
        /// </summary>
        /// <param name="modelList">实体类列表</param>
        /// <returns></returns>
        public DataTable FillDataTable(List<string> modelList)
        {
            if (modelList == null || modelList.Count == 0)
            {
                return null;
            }
            DataTable dt = CreateData(modelList[0]);

            foreach (string model in modelList)
            {
                DataRow dataRow = dt.NewRow();
                foreach (PropertyInfo propertyInfo in typeof(string).GetProperties())
                {
                    dataRow[propertyInfo.Name] = propertyInfo.GetValue(model, null);
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        /// <summary>
        /// 根据实体类得到表结构
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        private DataTable CreateData(string model)
        {
            DataTable dataTable = new DataTable(typeof(string).Name);
            foreach (PropertyInfo propertyInfo in typeof(string).GetProperties())
            {
                dataTable.Columns.Add(new DataColumn(propertyInfo.Name, propertyInfo.PropertyType));
            }
            return dataTable;
        }

        #endregion
        #region 写日志文件
        public void WriteLog(string FileFlag, string Message)
        {
            string fileLogPath = AppDomain.CurrentDomain.BaseDirectory + @"System\Log\";
            
            DateTime d = DateTime.Now;
            string LogFileName = FileFlag + d.ToString("yyyyMMdd").ToString() + ".txt";

            //DirectoryInfo path=new DirectoryInfo(LogFileName);
            //如果日志文件目录不存在,则创建
            if (!Directory.Exists(fileLogPath))
            {
                Directory.CreateDirectory(fileLogPath);
            }

            FileInfo finfo = new FileInfo(fileLogPath + LogFileName);

            try
            {
                FileStream fs = new FileStream(fileLogPath + LogFileName, FileMode.Append);
                StreamWriter strwriter = new StreamWriter(fs);
                try
                {
                    strwriter.WriteLine(Message);
                    strwriter.WriteLine();
                    strwriter.Flush();
                }
                catch
                {
                    //Console.WriteLine("日志文件写入失败信息:"+ee.ToString()); 
                }
                finally
                {
                    strwriter.Close();
                    strwriter = null;
                    fs.Close();
                    fs = null;
                }
            }
            catch
            {

            }
        }
        #endregion
    }
}
