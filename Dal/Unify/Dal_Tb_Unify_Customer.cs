using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Unify
{
    /// <summary>
    /// 数据访问类Dal_Tb_Unify_Customer。
    /// </summary>
    public class Dal_Tb_Unify_Customer
    {
        public Dal_Tb_Unify_Customer()
        {
            DbHelperSQL.ConnectionString = PubConstant.SQIBSContionString;
        }
        #region  成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid CustSynchCode)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@CustSynchCode", SqlDbType.UniqueIdentifier)};
            parameters[0].Value = CustSynchCode;

            int result = DbHelperSQL.RunProcedure("Proc_Tb_Unify_Customer_Exists", parameters, out rowsAffected);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///  增加一条数据
        /// </summary>
        public void Add(MobileSoft.Model.Unify.Tb_Unify_Customer model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@CustSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CommSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@UnCustID", SqlDbType.BigInt,8),
					new SqlParameter("@CustTypeName", SqlDbType.NVarChar,30),
					new SqlParameter("@CustName", SqlDbType.NVarChar,50),
					new SqlParameter("@NickName", SqlDbType.NVarChar,30),
					new SqlParameter("@FixedTel", SqlDbType.NVarChar,500),
					new SqlParameter("@MobilePhone", SqlDbType.NVarChar,500),
					new SqlParameter("@FaxTel", SqlDbType.NVarChar,500),
					new SqlParameter("@EMail", SqlDbType.NVarChar,500),
					new SqlParameter("@LoginPwd", SqlDbType.NVarChar,20),
					new SqlParameter("@Surname", SqlDbType.NVarChar,10),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@Sex", SqlDbType.NVarChar,10),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@Nationality", SqlDbType.NVarChar,30),
					new SqlParameter("@PaperName", SqlDbType.NVarChar,20),
					new SqlParameter("@PaperCode", SqlDbType.NVarChar,30),
					new SqlParameter("@PassSign", SqlDbType.NVarChar,20),
					new SqlParameter("@Address", SqlDbType.NVarChar,1000),
					new SqlParameter("@PostCode", SqlDbType.NVarChar,20),
					new SqlParameter("@MGradeSign", SqlDbType.NVarChar,30),
					new SqlParameter("@Recipient", SqlDbType.NVarChar,100),
					new SqlParameter("@Interests", SqlDbType.NVarChar,500),
					new SqlParameter("@Hobbies", SqlDbType.NVarChar,200),
					new SqlParameter("@WorkUnit", SqlDbType.NVarChar,300),
					new SqlParameter("@Job", SqlDbType.NVarChar,50),
					new SqlParameter("@Linkman", SqlDbType.NVarChar,20),
					new SqlParameter("@LinkmanTel", SqlDbType.NVarChar,20),
					new SqlParameter("@SmsTel", SqlDbType.NVarChar,20),
					new SqlParameter("@IsUnit", SqlDbType.SmallInt,2),
					new SqlParameter("@LegalRepr", SqlDbType.NVarChar,20),
					new SqlParameter("@LegalReprTel", SqlDbType.NVarChar,20),
					new SqlParameter("@Charge", SqlDbType.NVarChar,20),
					new SqlParameter("@ChargeTel", SqlDbType.NVarChar,20),
					new SqlParameter("@ProvinceID", SqlDbType.Int,4),
					new SqlParameter("@CityID", SqlDbType.Int,4),
					new SqlParameter("@BoroughID", SqlDbType.Int,4),
					new SqlParameter("@RoomCount", SqlDbType.Int,4),
					new SqlParameter("@RoomSigns", SqlDbType.NVarChar,2000),
					new SqlParameter("@Memo", SqlDbType.NVarChar,1000),
					new SqlParameter("@LiveType1", SqlDbType.SmallInt,2),
					new SqlParameter("@LiveType2", SqlDbType.SmallInt,2),
					new SqlParameter("@LiveType3", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDebts", SqlDbType.SmallInt,2),
					new SqlParameter("@IsUsual", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@HeadImgURL", SqlDbType.NVarChar,50),
					new SqlParameter("@Introduce", SqlDbType.NVarChar,200),
					new SqlParameter("@Hometown", SqlDbType.NVarChar,200),
					new SqlParameter("@BirAnimal", SqlDbType.NVarChar,50),
					new SqlParameter("@Constellation", SqlDbType.NVarChar,10),
					new SqlParameter("@BloodGroup", SqlDbType.NVarChar,10),
					new SqlParameter("@Stature", SqlDbType.NVarChar,50),
					new SqlParameter("@BodilyForm", SqlDbType.NVarChar,50),
					new SqlParameter("@MaritalStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@Qualification", SqlDbType.NVarChar,50),
					new SqlParameter("@GraduateSchool", SqlDbType.NVarChar,50),
					new SqlParameter("@Occupation", SqlDbType.NVarChar,50),
					new SqlParameter("@Telphone", SqlDbType.NVarChar,50),
					new SqlParameter("@MobileTel", SqlDbType.NVarChar,50),
					new SqlParameter("@EmailSign", SqlDbType.NVarChar,50),
					new SqlParameter("@HomePage", SqlDbType.NVarChar,50),
					new SqlParameter("@QQSign", SqlDbType.NVarChar,50),
					new SqlParameter("@MSNSign", SqlDbType.NVarChar,50),
					new SqlParameter("@BroadbandNum", SqlDbType.NVarChar,30),
					new SqlParameter("@TeleFixedTel", SqlDbType.NVarChar,50),
					new SqlParameter("@TeleMobile", SqlDbType.NVarChar,50),
					new SqlParameter("@IPTVAccount", SqlDbType.NVarChar,50),
					new SqlParameter("@TotalPosts", SqlDbType.Int,4),
					new SqlParameter("@Experience", SqlDbType.Int,4),
					new SqlParameter("@CustCardSign", SqlDbType.NVarChar,30),
					new SqlParameter("@CardSendDate", SqlDbType.DateTime),
					new SqlParameter("@CardActiveDate", SqlDbType.DateTime),
					new SqlParameter("@CardLossDates", SqlDbType.DateTime),
					new SqlParameter("@IsCardSend", SqlDbType.SmallInt,2),
					new SqlParameter("@IsCardActive", SqlDbType.SmallInt,2),
					new SqlParameter("@IsCardLoss", SqlDbType.SmallInt,2),
					new SqlParameter("@OrdSNum", SqlDbType.BigInt,8),
					new SqlParameter("@WanQinId", SqlDbType.VarChar,50),
					new SqlParameter("@WanQinPass", SqlDbType.VarChar,50),
					new SqlParameter("@ShiPingId", SqlDbType.VarChar,50),
					new SqlParameter("@ShiPingPass", SqlDbType.VarChar,50),
					new SqlParameter("@ZhiLenId", SqlDbType.VarChar,50),
					new SqlParameter("@ZhiLenPass", SqlDbType.VarChar,50)};
            parameters[0].Value = model.CustSynchCode;
            parameters[1].Value = model.CommSynchCode;
            parameters[2].Value = model.UnCustID;
            parameters[3].Value = model.CustTypeName;
            parameters[4].Value = model.CustName;
            parameters[5].Value = model.NickName;
            parameters[6].Value = model.FixedTel;
            parameters[7].Value = model.MobilePhone;
            parameters[8].Value = model.FaxTel;
            parameters[9].Value = model.EMail;
            parameters[10].Value = model.LoginPwd;
            parameters[11].Value = model.Surname;
            parameters[12].Value = model.Name;
            parameters[13].Value = model.Sex;
            parameters[14].Value = model.Birthday;
            parameters[15].Value = model.Nationality;
            parameters[16].Value = model.PaperName;
            parameters[17].Value = model.PaperCode;
            parameters[18].Value = model.PassSign;
            parameters[19].Value = model.Address;
            parameters[20].Value = model.PostCode;
            parameters[21].Value = model.MGradeSign;
            parameters[22].Value = model.Recipient;
            parameters[23].Value = model.Interests;
            parameters[24].Value = model.Hobbies;
            parameters[25].Value = model.WorkUnit;
            parameters[26].Value = model.Job;
            parameters[27].Value = model.Linkman;
            parameters[28].Value = model.LinkmanTel;
            parameters[29].Value = model.SmsTel;
            parameters[30].Value = model.IsUnit;
            parameters[31].Value = model.LegalRepr;
            parameters[32].Value = model.LegalReprTel;
            parameters[33].Value = model.Charge;
            parameters[34].Value = model.ChargeTel;
            parameters[35].Value = model.ProvinceID;
            parameters[36].Value = model.CityID;
            parameters[37].Value = model.BoroughID;
            parameters[38].Value = model.RoomCount;
            parameters[39].Value = model.RoomSigns;
            parameters[40].Value = model.Memo;
            parameters[41].Value = model.LiveType1;
            parameters[42].Value = model.LiveType2;
            parameters[43].Value = model.LiveType3;
            parameters[44].Value = model.IsDebts;
            parameters[45].Value = model.IsUsual;
            parameters[46].Value = model.IsDelete;
            parameters[47].Value = model.SynchFlag;
            parameters[48].Value = model.HeadImgURL;
            parameters[49].Value = model.Introduce;
            parameters[50].Value = model.Hometown;
            parameters[51].Value = model.BirAnimal;
            parameters[52].Value = model.Constellation;
            parameters[53].Value = model.BloodGroup;
            parameters[54].Value = model.Stature;
            parameters[55].Value = model.BodilyForm;
            parameters[56].Value = model.MaritalStatus;
            parameters[57].Value = model.Qualification;
            parameters[58].Value = model.GraduateSchool;
            parameters[59].Value = model.Occupation;
            parameters[60].Value = model.Telphone;
            parameters[61].Value = model.MobileTel;
            parameters[62].Value = model.EmailSign;
            parameters[63].Value = model.HomePage;
            parameters[64].Value = model.QQSign;
            parameters[65].Value = model.MSNSign;
            parameters[66].Value = model.BroadbandNum;
            parameters[67].Value = model.TeleFixedTel;
            parameters[68].Value = model.TeleMobile;
            parameters[69].Value = model.IPTVAccount;
            parameters[70].Value = model.TotalPosts;
            parameters[71].Value = model.Experience;
            parameters[72].Value = model.CustCardSign;
            parameters[73].Value = model.CardSendDate;
            parameters[74].Value = model.CardActiveDate;
            parameters[75].Value = model.CardLossDates;
            parameters[76].Value = model.IsCardSend;
            parameters[77].Value = model.IsCardActive;
            parameters[78].Value = model.IsCardLoss;
            parameters[79].Value = model.OrdSNum;
            parameters[80].Value = model.WanQinId;
            parameters[81].Value = model.WanQinPass;
            parameters[82].Value = model.ShiPingId;
            parameters[83].Value = model.ShiPingPass;
            parameters[84].Value = model.ZhiLenId;
            parameters[85].Value = model.ZhiLenPass;

            DbHelperSQL.RunProcedure("Proc_Tb_Unify_Customer_ADD", parameters, out rowsAffected);
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public void Update(MobileSoft.Model.Unify.Tb_Unify_Customer model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@CustSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CommSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@UnCustID", SqlDbType.BigInt,8),
					new SqlParameter("@CustTypeName", SqlDbType.NVarChar,30),
					new SqlParameter("@CustName", SqlDbType.NVarChar,50),
					new SqlParameter("@NickName", SqlDbType.NVarChar,30),
					new SqlParameter("@FixedTel", SqlDbType.NVarChar,500),
					new SqlParameter("@MobilePhone", SqlDbType.NVarChar,500),
					new SqlParameter("@FaxTel", SqlDbType.NVarChar,500),
					new SqlParameter("@EMail", SqlDbType.NVarChar,500),
					new SqlParameter("@LoginPwd", SqlDbType.NVarChar,20),
					new SqlParameter("@Surname", SqlDbType.NVarChar,10),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@Sex", SqlDbType.NVarChar,10),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@Nationality", SqlDbType.NVarChar,30),
					new SqlParameter("@PaperName", SqlDbType.NVarChar,20),
					new SqlParameter("@PaperCode", SqlDbType.NVarChar,30),
					new SqlParameter("@PassSign", SqlDbType.NVarChar,20),
					new SqlParameter("@Address", SqlDbType.NVarChar,1000),
					new SqlParameter("@PostCode", SqlDbType.NVarChar,20),
					new SqlParameter("@MGradeSign", SqlDbType.NVarChar,30),
					new SqlParameter("@Recipient", SqlDbType.NVarChar,100),
					new SqlParameter("@Interests", SqlDbType.NVarChar,500),
					new SqlParameter("@Hobbies", SqlDbType.NVarChar,200),
					new SqlParameter("@WorkUnit", SqlDbType.NVarChar,300),
					new SqlParameter("@Job", SqlDbType.NVarChar,50),
					new SqlParameter("@Linkman", SqlDbType.NVarChar,20),
					new SqlParameter("@LinkmanTel", SqlDbType.NVarChar,20),
					new SqlParameter("@SmsTel", SqlDbType.NVarChar,20),
					new SqlParameter("@IsUnit", SqlDbType.SmallInt,2),
					new SqlParameter("@LegalRepr", SqlDbType.NVarChar,20),
					new SqlParameter("@LegalReprTel", SqlDbType.NVarChar,20),
					new SqlParameter("@Charge", SqlDbType.NVarChar,20),
					new SqlParameter("@ChargeTel", SqlDbType.NVarChar,20),
					new SqlParameter("@ProvinceID", SqlDbType.Int,4),
					new SqlParameter("@CityID", SqlDbType.Int,4),
					new SqlParameter("@BoroughID", SqlDbType.Int,4),
					new SqlParameter("@RoomCount", SqlDbType.Int,4),
					new SqlParameter("@RoomSigns", SqlDbType.NVarChar,2000),
					new SqlParameter("@Memo", SqlDbType.NVarChar,1000),
					new SqlParameter("@LiveType1", SqlDbType.SmallInt,2),
					new SqlParameter("@LiveType2", SqlDbType.SmallInt,2),
					new SqlParameter("@LiveType3", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDebts", SqlDbType.SmallInt,2),
					new SqlParameter("@IsUsual", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@HeadImgURL", SqlDbType.NVarChar,3999),
					new SqlParameter("@Introduce", SqlDbType.NVarChar,200),
					new SqlParameter("@Hometown", SqlDbType.NVarChar,200),
					new SqlParameter("@BirAnimal", SqlDbType.NVarChar,50),
					new SqlParameter("@Constellation", SqlDbType.NVarChar,10),
					new SqlParameter("@BloodGroup", SqlDbType.NVarChar,10),
					new SqlParameter("@Stature", SqlDbType.NVarChar,50),
					new SqlParameter("@BodilyForm", SqlDbType.NVarChar,50),
					new SqlParameter("@MaritalStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@Qualification", SqlDbType.NVarChar,50),
					new SqlParameter("@GraduateSchool", SqlDbType.NVarChar,50),
					new SqlParameter("@Occupation", SqlDbType.NVarChar,50),
					new SqlParameter("@Telphone", SqlDbType.NVarChar,50),
					new SqlParameter("@MobileTel", SqlDbType.NVarChar,50),
					new SqlParameter("@EmailSign", SqlDbType.NVarChar,50),
					new SqlParameter("@HomePage", SqlDbType.NVarChar,50),
					new SqlParameter("@QQSign", SqlDbType.NVarChar,50),
					new SqlParameter("@MSNSign", SqlDbType.NVarChar,50),
					new SqlParameter("@BroadbandNum", SqlDbType.NVarChar,30),
					new SqlParameter("@TeleFixedTel", SqlDbType.NVarChar,50),
					new SqlParameter("@TeleMobile", SqlDbType.NVarChar,50),
					new SqlParameter("@IPTVAccount", SqlDbType.NVarChar,50),
					new SqlParameter("@TotalPosts", SqlDbType.Int,4),
					new SqlParameter("@Experience", SqlDbType.Int,4),
					new SqlParameter("@CustCardSign", SqlDbType.NVarChar,30),
					new SqlParameter("@CardSendDate", SqlDbType.DateTime),
					new SqlParameter("@CardActiveDate", SqlDbType.DateTime),
					new SqlParameter("@CardLossDates", SqlDbType.DateTime),
					new SqlParameter("@IsCardSend", SqlDbType.SmallInt,2),
					new SqlParameter("@IsCardActive", SqlDbType.SmallInt,2),
					new SqlParameter("@IsCardLoss", SqlDbType.SmallInt,2),
					new SqlParameter("@OrdSNum", SqlDbType.BigInt,8),
					new SqlParameter("@WanQinId", SqlDbType.VarChar,50),
					new SqlParameter("@WanQinPass", SqlDbType.VarChar,50),
					new SqlParameter("@ShiPingId", SqlDbType.VarChar,50),
					new SqlParameter("@ShiPingPass", SqlDbType.VarChar,50),
					new SqlParameter("@ZhiLenId", SqlDbType.VarChar,50),
					new SqlParameter("@ZhiLenPass", SqlDbType.VarChar,50),
                    new SqlParameter("@WaterCardNum", SqlDbType.VarChar,36),
                    new SqlParameter("@ElectricityNum", SqlDbType.VarChar,36),
                    new SqlParameter("@GasNum", SqlDbType.VarChar,36)
                                        
                                        };
            parameters[0].Value = model.CustSynchCode;
            parameters[1].Value = model.CommSynchCode;
            parameters[2].Value = model.UnCustID;
            parameters[3].Value = model.CustTypeName;
            parameters[4].Value = model.CustName;
            parameters[5].Value = model.NickName;
            parameters[6].Value = model.FixedTel;
            parameters[7].Value = model.MobilePhone;
            parameters[8].Value = model.FaxTel;
            parameters[9].Value = model.EMail;
            parameters[10].Value = model.LoginPwd;
            parameters[11].Value = model.Surname;
            parameters[12].Value = model.Name;
            parameters[13].Value = model.Sex;
            parameters[14].Value = model.Birthday;
            parameters[15].Value = model.Nationality;
            parameters[16].Value = model.PaperName;
            parameters[17].Value = model.PaperCode;
            parameters[18].Value = model.PassSign;
            parameters[19].Value = model.Address;
            parameters[20].Value = model.PostCode;
            parameters[21].Value = model.MGradeSign;
            parameters[22].Value = model.Recipient;
            parameters[23].Value = model.Interests;
            parameters[24].Value = model.Hobbies;
            parameters[25].Value = model.WorkUnit;
            parameters[26].Value = model.Job;
            parameters[27].Value = model.Linkman;
            parameters[28].Value = model.LinkmanTel;
            parameters[29].Value = model.SmsTel;
            parameters[30].Value = model.IsUnit;
            parameters[31].Value = model.LegalRepr;
            parameters[32].Value = model.LegalReprTel;
            parameters[33].Value = model.Charge;
            parameters[34].Value = model.ChargeTel;
            parameters[35].Value = model.ProvinceID;
            parameters[36].Value = model.CityID;
            parameters[37].Value = model.BoroughID;
            parameters[38].Value = model.RoomCount;
            parameters[39].Value = model.RoomSigns;
            parameters[40].Value = model.Memo;
            parameters[41].Value = model.LiveType1;
            parameters[42].Value = model.LiveType2;
            parameters[43].Value = model.LiveType3;
            parameters[44].Value = model.IsDebts;
            parameters[45].Value = model.IsUsual;
            parameters[46].Value = model.IsDelete;
            parameters[47].Value = model.SynchFlag;
            parameters[48].Value = model.HeadImgURL;
            parameters[49].Value = model.Introduce;
            parameters[50].Value = model.Hometown;
            parameters[51].Value = model.BirAnimal;
            parameters[52].Value = model.Constellation;
            parameters[53].Value = model.BloodGroup;
            parameters[54].Value = model.Stature;
            parameters[55].Value = model.BodilyForm;
            parameters[56].Value = model.MaritalStatus;
            parameters[57].Value = model.Qualification;
            parameters[58].Value = model.GraduateSchool;
            parameters[59].Value = model.Occupation;
            parameters[60].Value = model.Telphone;
            parameters[61].Value = model.MobileTel;
            parameters[62].Value = model.EmailSign;
            parameters[63].Value = model.HomePage;
            parameters[64].Value = model.QQSign;
            parameters[65].Value = model.MSNSign;
            parameters[66].Value = model.BroadbandNum;
            parameters[67].Value = model.TeleFixedTel;
            parameters[68].Value = model.TeleMobile;
            parameters[69].Value = model.IPTVAccount;
            parameters[70].Value = model.TotalPosts;
            parameters[71].Value = model.Experience;
            parameters[72].Value = model.CustCardSign;
            parameters[73].Value = model.CardSendDate;
            parameters[74].Value = model.CardActiveDate;
            parameters[75].Value = model.CardLossDates;
            parameters[76].Value = model.IsCardSend;
            parameters[77].Value = model.IsCardActive;
            parameters[78].Value = model.IsCardLoss;
            parameters[79].Value = model.OrdSNum;
            parameters[80].Value = model.WanQinId;
            parameters[81].Value = model.WanQinPass;
            parameters[82].Value = model.ShiPingId;
            parameters[83].Value = model.ShiPingPass;
            parameters[84].Value = model.ZhiLenId;
            parameters[85].Value = model.ZhiLenPass;

            parameters[86].Value = model.WaterCardNum;
            parameters[87].Value = model.ElectricityNum;
            parameters[88].Value = model.GasNum;

            DbHelperSQL.RunProcedure("Proc_Tb_Unify_Customer_Update", parameters, out rowsAffected);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(Guid CustSynchCode)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@CustSynchCode", SqlDbType.UniqueIdentifier)};
            parameters[0].Value = CustSynchCode;

            DbHelperSQL.RunProcedure("Proc_Tb_Unify_Customer_Delete", parameters, out rowsAffected);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MobileSoft.Model.Unify.Tb_Unify_Customer GetModel(Guid CustSynchCode)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@CustSynchCode", SqlDbType.UniqueIdentifier)};
            parameters[0].Value = CustSynchCode;

            MobileSoft.Model.Unify.Tb_Unify_Customer model = new MobileSoft.Model.Unify.Tb_Unify_Customer();
            DataSet ds = DbHelperSQL.RunProcedure("Proc_Tb_Unify_Customer_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CustSynchCode"].ToString() != "")
                {
                    model.CustSynchCode = new Guid(ds.Tables[0].Rows[0]["CustSynchCode"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CommSynchCode"].ToString() != "")
                {
                    model.CommSynchCode = new Guid(ds.Tables[0].Rows[0]["CommSynchCode"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UnCustID"].ToString() != "")
                {
                    model.UnCustID = long.Parse(ds.Tables[0].Rows[0]["UnCustID"].ToString());
                }
                model.CustTypeName = ds.Tables[0].Rows[0]["CustTypeName"].ToString();
                model.CustName = ds.Tables[0].Rows[0]["CustName"].ToString();
                model.NickName = ds.Tables[0].Rows[0]["NickName"].ToString();
                model.FixedTel = ds.Tables[0].Rows[0]["FixedTel"].ToString();
                model.MobilePhone = ds.Tables[0].Rows[0]["MobilePhone"].ToString();
                model.FaxTel = ds.Tables[0].Rows[0]["FaxTel"].ToString();
                model.EMail = ds.Tables[0].Rows[0]["EMail"].ToString();
                model.LoginPwd = ds.Tables[0].Rows[0]["LoginPwd"].ToString();
                model.Surname = ds.Tables[0].Rows[0]["Surname"].ToString();
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                model.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                if (ds.Tables[0].Rows[0]["Birthday"].ToString() != "")
                {
                    model.Birthday = DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
                }
                model.Nationality = ds.Tables[0].Rows[0]["Nationality"].ToString();
                model.PaperName = ds.Tables[0].Rows[0]["PaperName"].ToString();
                model.PaperCode = ds.Tables[0].Rows[0]["PaperCode"].ToString();
                model.PassSign = ds.Tables[0].Rows[0]["PassSign"].ToString();
                model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                model.PostCode = ds.Tables[0].Rows[0]["PostCode"].ToString();
                model.MGradeSign = ds.Tables[0].Rows[0]["MGradeSign"].ToString();
                model.Recipient = ds.Tables[0].Rows[0]["Recipient"].ToString();
                model.Interests = ds.Tables[0].Rows[0]["Interests"].ToString();
                model.Hobbies = ds.Tables[0].Rows[0]["Hobbies"].ToString();
                model.WorkUnit = ds.Tables[0].Rows[0]["WorkUnit"].ToString();
                model.Job = ds.Tables[0].Rows[0]["Job"].ToString();
                model.Linkman = ds.Tables[0].Rows[0]["Linkman"].ToString();
                model.LinkmanTel = ds.Tables[0].Rows[0]["LinkmanTel"].ToString();
                model.SmsTel = ds.Tables[0].Rows[0]["SmsTel"].ToString();
                if (ds.Tables[0].Rows[0]["IsUnit"].ToString() != "")
                {
                    model.IsUnit = int.Parse(ds.Tables[0].Rows[0]["IsUnit"].ToString());
                }
                model.LegalRepr = ds.Tables[0].Rows[0]["LegalRepr"].ToString();
                model.LegalReprTel = ds.Tables[0].Rows[0]["LegalReprTel"].ToString();
                model.Charge = ds.Tables[0].Rows[0]["Charge"].ToString();
                model.ChargeTel = ds.Tables[0].Rows[0]["ChargeTel"].ToString();
                if (ds.Tables[0].Rows[0]["ProvinceID"].ToString() != "")
                {
                    model.ProvinceID = int.Parse(ds.Tables[0].Rows[0]["ProvinceID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CityID"].ToString() != "")
                {
                    model.CityID = int.Parse(ds.Tables[0].Rows[0]["CityID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BoroughID"].ToString() != "")
                {
                    model.BoroughID = int.Parse(ds.Tables[0].Rows[0]["BoroughID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RoomCount"].ToString() != "")
                {
                    model.RoomCount = int.Parse(ds.Tables[0].Rows[0]["RoomCount"].ToString());
                }
                model.RoomSigns = ds.Tables[0].Rows[0]["RoomSigns"].ToString();
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                if (ds.Tables[0].Rows[0]["LiveType1"].ToString() != "")
                {
                    model.LiveType1 = int.Parse(ds.Tables[0].Rows[0]["LiveType1"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LiveType2"].ToString() != "")
                {
                    model.LiveType2 = int.Parse(ds.Tables[0].Rows[0]["LiveType2"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LiveType3"].ToString() != "")
                {
                    model.LiveType3 = int.Parse(ds.Tables[0].Rows[0]["LiveType3"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsDebts"].ToString() != "")
                {
                    model.IsDebts = int.Parse(ds.Tables[0].Rows[0]["IsDebts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsUsual"].ToString() != "")
                {
                    model.IsUsual = int.Parse(ds.Tables[0].Rows[0]["IsUsual"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsDelete"].ToString() != "")
                {
                    model.IsDelete = int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SynchFlag"].ToString() != "")
                {
                    model.SynchFlag = int.Parse(ds.Tables[0].Rows[0]["SynchFlag"].ToString());
                }
                model.HeadImgURL = ds.Tables[0].Rows[0]["HeadImgURL"].ToString();
                model.Introduce = ds.Tables[0].Rows[0]["Introduce"].ToString();
                model.Hometown = ds.Tables[0].Rows[0]["Hometown"].ToString();
                model.BirAnimal = ds.Tables[0].Rows[0]["BirAnimal"].ToString();
                model.Constellation = ds.Tables[0].Rows[0]["Constellation"].ToString();
                model.BloodGroup = ds.Tables[0].Rows[0]["BloodGroup"].ToString();
                model.Stature = ds.Tables[0].Rows[0]["Stature"].ToString();
                model.BodilyForm = ds.Tables[0].Rows[0]["BodilyForm"].ToString();
                model.MaritalStatus = ds.Tables[0].Rows[0]["MaritalStatus"].ToString();
                model.Qualification = ds.Tables[0].Rows[0]["Qualification"].ToString();
                model.GraduateSchool = ds.Tables[0].Rows[0]["GraduateSchool"].ToString();
                model.Occupation = ds.Tables[0].Rows[0]["Occupation"].ToString();
                model.Telphone = ds.Tables[0].Rows[0]["Telphone"].ToString();
                model.MobileTel = ds.Tables[0].Rows[0]["MobileTel"].ToString();
                model.EmailSign = ds.Tables[0].Rows[0]["EmailSign"].ToString();
                model.HomePage = ds.Tables[0].Rows[0]["HomePage"].ToString();
                model.QQSign = ds.Tables[0].Rows[0]["QQSign"].ToString();
                model.MSNSign = ds.Tables[0].Rows[0]["MSNSign"].ToString();
                model.BroadbandNum = ds.Tables[0].Rows[0]["BroadbandNum"].ToString();
                model.TeleFixedTel = ds.Tables[0].Rows[0]["TeleFixedTel"].ToString();
                model.TeleMobile = ds.Tables[0].Rows[0]["TeleMobile"].ToString();
                model.IPTVAccount = ds.Tables[0].Rows[0]["IPTVAccount"].ToString();
                if (ds.Tables[0].Rows[0]["TotalPosts"].ToString() != "")
                {
                    model.TotalPosts = int.Parse(ds.Tables[0].Rows[0]["TotalPosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Experience"].ToString() != "")
                {
                    model.Experience = int.Parse(ds.Tables[0].Rows[0]["Experience"].ToString());
                }
                model.CustCardSign = ds.Tables[0].Rows[0]["CustCardSign"].ToString();
                if (ds.Tables[0].Rows[0]["CardSendDate"].ToString() != "")
                {
                    model.CardSendDate = DateTime.Parse(ds.Tables[0].Rows[0]["CardSendDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CardActiveDate"].ToString() != "")
                {
                    model.CardActiveDate = DateTime.Parse(ds.Tables[0].Rows[0]["CardActiveDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CardLossDates"].ToString() != "")
                {
                    model.CardLossDates = DateTime.Parse(ds.Tables[0].Rows[0]["CardLossDates"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsCardSend"].ToString() != "")
                {
                    model.IsCardSend = int.Parse(ds.Tables[0].Rows[0]["IsCardSend"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsCardActive"].ToString() != "")
                {
                    model.IsCardActive = int.Parse(ds.Tables[0].Rows[0]["IsCardActive"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsCardLoss"].ToString() != "")
                {
                    model.IsCardLoss = int.Parse(ds.Tables[0].Rows[0]["IsCardLoss"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrdSNum"].ToString() != "")
                {
                    model.OrdSNum = long.Parse(ds.Tables[0].Rows[0]["OrdSNum"].ToString());
                }
                model.WanQinId = ds.Tables[0].Rows[0]["WanQinId"].ToString();
                model.WanQinPass = ds.Tables[0].Rows[0]["WanQinPass"].ToString();
                model.ShiPingId = ds.Tables[0].Rows[0]["ShiPingId"].ToString();
                model.ShiPingPass = ds.Tables[0].Rows[0]["ShiPingPass"].ToString();
                model.ZhiLenId = ds.Tables[0].Rows[0]["ZhiLenId"].ToString();
                model.ZhiLenPass = ds.Tables[0].Rows[0]["ZhiLenPass"].ToString();

                model.WaterCardNum = ds.Tables[0].Rows[0]["WaterCardNum"].ToString();
                model.ElectricityNum = ds.Tables[0].Rows[0]["ElectricityNum"].ToString();
                model.GasNum = ds.Tables[0].Rows[0]["GasNum"].ToString();

                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM View_Unify_Customer_Filter ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string fieldOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM Tb_Unify_Customer ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + fieldOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(string Fields,int Top, string strWhere, string fieldOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" "+Fields+" ");
            strSql.Append(" FROM Tb_Unify_Customer ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + fieldOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort)
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
            parameters[5].Value = "SELECT * FROM View_Unify_Customer_Filter WHERE 1=1 " + StrCondition;
            parameters[6].Value = "CustSynchCode";
            DataSet Ds = DbHelperSQL.RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }

        #endregion  成员方法
    }
}

