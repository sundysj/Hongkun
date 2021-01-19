using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Sys
{
	/// <summary>
	/// 数据访问类Dal_Tb_Sys_User。
	/// </summary>
	public class Dal_Tb_Sys_User
	{
		public Dal_Tb_Sys_User()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string UserCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = UserCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Sys_User_Exists",parameters,out rowsAffected);
			if(result==1)
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
		public void Add(MobileSoft.Model.Sys.Tb_Sys_User model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@DepCode", SqlDbType.NVarChar,20),
					new SqlParameter("@LoginCode", SqlDbType.NVarChar,20),
					new SqlParameter("@PassWord", SqlDbType.NVarChar,50),
					new SqlParameter("@Sex", SqlDbType.NVarChar,10),
					new SqlParameter("@MobileTel", SqlDbType.NVarChar,50),
					new SqlParameter("@WorkedTel", SqlDbType.NVarChar,50),
					new SqlParameter("@PersonalTel", SqlDbType.NVarChar,50),
					new SqlParameter("@IDCardCode", SqlDbType.NVarChar,25),
					new SqlParameter("@LinkmanName", SqlDbType.NVarChar,20),
					new SqlParameter("@LinkmanTel", SqlDbType.NVarChar,50),
					new SqlParameter("@Email", SqlDbType.NVarChar,50),
					new SqlParameter("@IsPublicMail", SqlDbType.SmallInt,2),
					new SqlParameter("@EmployeeCode", SqlDbType.NVarChar,20),
					new SqlParameter("@WorkerSign", SqlDbType.NVarChar,20),
					new SqlParameter("@Duty", SqlDbType.NVarChar,20),
					new SqlParameter("@BankAccounts", SqlDbType.NVarChar,20),
					new SqlParameter("@Qualification", SqlDbType.NVarChar,20),
					new SqlParameter("@NativePlace", SqlDbType.NVarChar,30),
					new SqlParameter("@Speciality", SqlDbType.NVarChar,30),
					new SqlParameter("@MaritalStatus", SqlDbType.NVarChar,20),
					new SqlParameter("@Stature", SqlDbType.NVarChar,20),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@Avoirdupois", SqlDbType.NVarChar,20),
					new SqlParameter("@Nation", SqlDbType.NVarChar,20),
					new SqlParameter("@Politics", SqlDbType.NVarChar,20),
					new SqlParameter("@TechnicTitle", SqlDbType.NVarChar,80),
					new SqlParameter("@Skills", SqlDbType.NVarChar,80),
					new SqlParameter("@GuardCard", SqlDbType.NVarChar,20),
					new SqlParameter("@Computer", SqlDbType.NVarChar,20),
					new SqlParameter("@WorkingTime", SqlDbType.DateTime),
					new SqlParameter("@JoinTime", SqlDbType.DateTime),
					new SqlParameter("@Residence", SqlDbType.NVarChar,80),
					new SqlParameter("@Address", SqlDbType.NVarChar,80),
					new SqlParameter("@Post", SqlDbType.NVarChar,6),
					new SqlParameter("@ArchivesAddr", SqlDbType.NVarChar,20),
					new SqlParameter("@PreInsuranceCode", SqlDbType.NVarChar,20),
					new SqlParameter("@InsuranceCode", SqlDbType.NVarChar,20),
					new SqlParameter("@ResidenceType", SqlDbType.NVarChar,20),
					new SqlParameter("@IsBuyInsurance", SqlDbType.SmallInt,2),
					new SqlParameter("@IsAgreeBuy", SqlDbType.SmallInt,2),
					new SqlParameter("@PayLevel", SqlDbType.NVarChar,20),
					new SqlParameter("@WorkingDate", SqlDbType.DateTime),
					new SqlParameter("@JionDate", SqlDbType.DateTime),
					new SqlParameter("@ProbationDate", SqlDbType.DateTime),
					new SqlParameter("@FormalDate", SqlDbType.DateTime),
					new SqlParameter("@DimissionDate", SqlDbType.DateTime),
					new SqlParameter("@InureDate", SqlDbType.DateTime),
					new SqlParameter("@PraxisDate", SqlDbType.DateTime),
					new SqlParameter("@JoinPartyDate", SqlDbType.DateTime),
					new SqlParameter("@HealthState", SqlDbType.NVarChar,200),
					new SqlParameter("@OldName", SqlDbType.NVarChar,50),
					new SqlParameter("@EyeLeft", SqlDbType.NVarChar,50),
					new SqlParameter("@EyeRight", SqlDbType.NVarChar,50),
					new SqlParameter("@FamilyBirth", SqlDbType.NVarChar,50),
					new SqlParameter("@BloodGroup", SqlDbType.NVarChar,50),
					new SqlParameter("@School", SqlDbType.NVarChar,50),
					new SqlParameter("@Approach", SqlDbType.NVarChar,50),
					new SqlParameter("@OldUnit", SqlDbType.NVarChar,50),
					new SqlParameter("@ForRegional", SqlDbType.NVarChar,20),
					new SqlParameter("@ArchiveState", SqlDbType.SmallInt,2),
					new SqlParameter("@Memo", SqlDbType.NVarChar,1000),
					new SqlParameter("@IsShow", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@DeleteDate", SqlDbType.DateTime),
					new SqlParameter("@WorkAchievement", SqlDbType.NText),
					new SqlParameter("@FamilyContext", SqlDbType.NText),
					new SqlParameter("@InterestLove", SqlDbType.NText),
					new SqlParameter("@StudySuffer", SqlDbType.NText),
					new SqlParameter("@JobSuffer", SqlDbType.NText),
					new SqlParameter("@ChangeLog", SqlDbType.NText),
					new SqlParameter("@IsLogin", SqlDbType.Int,4),
					new SqlParameter("@IsDimission", SqlDbType.Int,4),
					new SqlParameter("@IsHostelSearch", SqlDbType.SmallInt,2),
					new SqlParameter("@OrganizeCode", SqlDbType.NVarChar,100),
					new SqlParameter("@SearchCommID", SqlDbType.Int,4),
					new SqlParameter("@IsMobile", SqlDbType.SmallInt,2),
					new SqlParameter("@MachineCodes", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.UserCode;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.UserName;
			parameters[3].Value = model.DepCode;
			parameters[4].Value = model.LoginCode;
			parameters[5].Value = model.PassWord;
			parameters[6].Value = model.Sex;
			parameters[7].Value = model.MobileTel;
			parameters[8].Value = model.WorkedTel;
			parameters[9].Value = model.PersonalTel;
			parameters[10].Value = model.IDCardCode;
			parameters[11].Value = model.LinkmanName;
			parameters[12].Value = model.LinkmanTel;
			parameters[13].Value = model.Email;
			parameters[14].Value = model.IsPublicMail;
			parameters[15].Value = model.EmployeeCode;
			parameters[16].Value = model.WorkerSign;
			parameters[17].Value = model.Duty;
			parameters[18].Value = model.BankAccounts;
			parameters[19].Value = model.Qualification;
			parameters[20].Value = model.NativePlace;
			parameters[21].Value = model.Speciality;
			parameters[22].Value = model.MaritalStatus;
			parameters[23].Value = model.Stature;
			parameters[24].Value = model.Birthday;
			parameters[25].Value = model.Avoirdupois;
			parameters[26].Value = model.Nation;
			parameters[27].Value = model.Politics;
			parameters[28].Value = model.TechnicTitle;
			parameters[29].Value = model.Skills;
			parameters[30].Value = model.GuardCard;
			parameters[31].Value = model.Computer;
			parameters[32].Value = model.WorkingTime;
			parameters[33].Value = model.JoinTime;
			parameters[34].Value = model.Residence;
			parameters[35].Value = model.Address;
			parameters[36].Value = model.Post;
			parameters[37].Value = model.ArchivesAddr;
			parameters[38].Value = model.PreInsuranceCode;
			parameters[39].Value = model.InsuranceCode;
			parameters[40].Value = model.ResidenceType;
			parameters[41].Value = model.IsBuyInsurance;
			parameters[42].Value = model.IsAgreeBuy;
			parameters[43].Value = model.PayLevel;
			parameters[44].Value = model.WorkingDate;
			parameters[45].Value = model.JionDate;
			parameters[46].Value = model.ProbationDate;
			parameters[47].Value = model.FormalDate;
			parameters[48].Value = model.DimissionDate;
			parameters[49].Value = model.InureDate;
			parameters[50].Value = model.PraxisDate;
			parameters[51].Value = model.JoinPartyDate;
			parameters[52].Value = model.HealthState;
			parameters[53].Value = model.OldName;
			parameters[54].Value = model.EyeLeft;
			parameters[55].Value = model.EyeRight;
			parameters[56].Value = model.FamilyBirth;
			parameters[57].Value = model.BloodGroup;
			parameters[58].Value = model.School;
			parameters[59].Value = model.Approach;
			parameters[60].Value = model.OldUnit;
			parameters[61].Value = model.ForRegional;
			parameters[62].Value = model.ArchiveState;
			parameters[63].Value = model.Memo;
			parameters[64].Value = model.IsShow;
			parameters[65].Value = model.IsDelete;
			parameters[66].Value = model.DeleteDate;
			parameters[67].Value = model.WorkAchievement;
			parameters[68].Value = model.FamilyContext;
			parameters[69].Value = model.InterestLove;
			parameters[70].Value = model.StudySuffer;
			parameters[71].Value = model.JobSuffer;
			parameters[72].Value = model.ChangeLog;
			parameters[73].Value = model.IsLogin;
			parameters[74].Value = model.IsDimission;
			parameters[75].Value = model.IsHostelSearch;
			parameters[76].Value = model.OrganizeCode;
			parameters[77].Value = model.SearchCommID;
			parameters[78].Value = model.IsMobile;
			parameters[79].Value = model.MachineCodes;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_User_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Sys.Tb_Sys_User model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@DepCode", SqlDbType.NVarChar,20),
					new SqlParameter("@LoginCode", SqlDbType.NVarChar,20),
					new SqlParameter("@PassWord", SqlDbType.NVarChar,50),
					new SqlParameter("@Sex", SqlDbType.NVarChar,10),
					new SqlParameter("@MobileTel", SqlDbType.NVarChar,50),
					new SqlParameter("@WorkedTel", SqlDbType.NVarChar,50),
					new SqlParameter("@PersonalTel", SqlDbType.NVarChar,50),
					new SqlParameter("@IDCardCode", SqlDbType.NVarChar,25),
					new SqlParameter("@LinkmanName", SqlDbType.NVarChar,20),
					new SqlParameter("@LinkmanTel", SqlDbType.NVarChar,50),
					new SqlParameter("@Email", SqlDbType.NVarChar,50),
					new SqlParameter("@IsPublicMail", SqlDbType.SmallInt,2),
					new SqlParameter("@EmployeeCode", SqlDbType.NVarChar,20),
					new SqlParameter("@WorkerSign", SqlDbType.NVarChar,20),
					new SqlParameter("@Duty", SqlDbType.NVarChar,20),
					new SqlParameter("@BankAccounts", SqlDbType.NVarChar,20),
					new SqlParameter("@Qualification", SqlDbType.NVarChar,20),
					new SqlParameter("@NativePlace", SqlDbType.NVarChar,30),
					new SqlParameter("@Speciality", SqlDbType.NVarChar,30),
					new SqlParameter("@MaritalStatus", SqlDbType.NVarChar,20),
					new SqlParameter("@Stature", SqlDbType.NVarChar,20),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@Avoirdupois", SqlDbType.NVarChar,20),
					new SqlParameter("@Nation", SqlDbType.NVarChar,20),
					new SqlParameter("@Politics", SqlDbType.NVarChar,20),
					new SqlParameter("@TechnicTitle", SqlDbType.NVarChar,80),
					new SqlParameter("@Skills", SqlDbType.NVarChar,80),
					new SqlParameter("@GuardCard", SqlDbType.NVarChar,20),
					new SqlParameter("@Computer", SqlDbType.NVarChar,20),
					new SqlParameter("@WorkingTime", SqlDbType.DateTime),
					new SqlParameter("@JoinTime", SqlDbType.DateTime),
					new SqlParameter("@Residence", SqlDbType.NVarChar,80),
					new SqlParameter("@Address", SqlDbType.NVarChar,80),
					new SqlParameter("@Post", SqlDbType.NVarChar,6),
					new SqlParameter("@ArchivesAddr", SqlDbType.NVarChar,20),
					new SqlParameter("@PreInsuranceCode", SqlDbType.NVarChar,20),
					new SqlParameter("@InsuranceCode", SqlDbType.NVarChar,20),
					new SqlParameter("@ResidenceType", SqlDbType.NVarChar,20),
					new SqlParameter("@IsBuyInsurance", SqlDbType.SmallInt,2),
					new SqlParameter("@IsAgreeBuy", SqlDbType.SmallInt,2),
					new SqlParameter("@PayLevel", SqlDbType.NVarChar,20),
					new SqlParameter("@WorkingDate", SqlDbType.DateTime),
					new SqlParameter("@JionDate", SqlDbType.DateTime),
					new SqlParameter("@ProbationDate", SqlDbType.DateTime),
					new SqlParameter("@FormalDate", SqlDbType.DateTime),
					new SqlParameter("@DimissionDate", SqlDbType.DateTime),
					new SqlParameter("@InureDate", SqlDbType.DateTime),
					new SqlParameter("@PraxisDate", SqlDbType.DateTime),
					new SqlParameter("@JoinPartyDate", SqlDbType.DateTime),
					new SqlParameter("@HealthState", SqlDbType.NVarChar,200),
					new SqlParameter("@OldName", SqlDbType.NVarChar,50),
					new SqlParameter("@EyeLeft", SqlDbType.NVarChar,50),
					new SqlParameter("@EyeRight", SqlDbType.NVarChar,50),
					new SqlParameter("@FamilyBirth", SqlDbType.NVarChar,50),
					new SqlParameter("@BloodGroup", SqlDbType.NVarChar,50),
					new SqlParameter("@School", SqlDbType.NVarChar,50),
					new SqlParameter("@Approach", SqlDbType.NVarChar,50),
					new SqlParameter("@OldUnit", SqlDbType.NVarChar,50),
					new SqlParameter("@ForRegional", SqlDbType.NVarChar,20),
					new SqlParameter("@ArchiveState", SqlDbType.SmallInt,2),
					new SqlParameter("@Memo", SqlDbType.NVarChar,1000),
					new SqlParameter("@IsShow", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@DeleteDate", SqlDbType.DateTime),
					new SqlParameter("@WorkAchievement", SqlDbType.NText),
					new SqlParameter("@FamilyContext", SqlDbType.NText),
					new SqlParameter("@InterestLove", SqlDbType.NText),
					new SqlParameter("@StudySuffer", SqlDbType.NText),
					new SqlParameter("@JobSuffer", SqlDbType.NText),
					new SqlParameter("@ChangeLog", SqlDbType.NText),
					new SqlParameter("@IsLogin", SqlDbType.Int,4),
					new SqlParameter("@IsDimission", SqlDbType.Int,4),
					new SqlParameter("@IsHostelSearch", SqlDbType.SmallInt,2),
					new SqlParameter("@OrganizeCode", SqlDbType.NVarChar,100),
					new SqlParameter("@SearchCommID", SqlDbType.Int,4),
					new SqlParameter("@IsMobile", SqlDbType.SmallInt,2),
					new SqlParameter("@MachineCodes", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.UserCode;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.UserName;
			parameters[3].Value = model.DepCode;
			parameters[4].Value = model.LoginCode;
			parameters[5].Value = model.PassWord;
			parameters[6].Value = model.Sex;
			parameters[7].Value = model.MobileTel;
			parameters[8].Value = model.WorkedTel;
			parameters[9].Value = model.PersonalTel;
			parameters[10].Value = model.IDCardCode;
			parameters[11].Value = model.LinkmanName;
			parameters[12].Value = model.LinkmanTel;
			parameters[13].Value = model.Email;
			parameters[14].Value = model.IsPublicMail;
			parameters[15].Value = model.EmployeeCode;
			parameters[16].Value = model.WorkerSign;
			parameters[17].Value = model.Duty;
			parameters[18].Value = model.BankAccounts;
			parameters[19].Value = model.Qualification;
			parameters[20].Value = model.NativePlace;
			parameters[21].Value = model.Speciality;
			parameters[22].Value = model.MaritalStatus;
			parameters[23].Value = model.Stature;
			parameters[24].Value = model.Birthday;
			parameters[25].Value = model.Avoirdupois;
			parameters[26].Value = model.Nation;
			parameters[27].Value = model.Politics;
			parameters[28].Value = model.TechnicTitle;
			parameters[29].Value = model.Skills;
			parameters[30].Value = model.GuardCard;
			parameters[31].Value = model.Computer;
			parameters[32].Value = model.WorkingTime;
			parameters[33].Value = model.JoinTime;
			parameters[34].Value = model.Residence;
			parameters[35].Value = model.Address;
			parameters[36].Value = model.Post;
			parameters[37].Value = model.ArchivesAddr;
			parameters[38].Value = model.PreInsuranceCode;
			parameters[39].Value = model.InsuranceCode;
			parameters[40].Value = model.ResidenceType;
			parameters[41].Value = model.IsBuyInsurance;
			parameters[42].Value = model.IsAgreeBuy;
			parameters[43].Value = model.PayLevel;
			parameters[44].Value = model.WorkingDate;
			parameters[45].Value = model.JionDate;
			parameters[46].Value = model.ProbationDate;
			parameters[47].Value = model.FormalDate;
			parameters[48].Value = model.DimissionDate;
			parameters[49].Value = model.InureDate;
			parameters[50].Value = model.PraxisDate;
			parameters[51].Value = model.JoinPartyDate;
			parameters[52].Value = model.HealthState;
			parameters[53].Value = model.OldName;
			parameters[54].Value = model.EyeLeft;
			parameters[55].Value = model.EyeRight;
			parameters[56].Value = model.FamilyBirth;
			parameters[57].Value = model.BloodGroup;
			parameters[58].Value = model.School;
			parameters[59].Value = model.Approach;
			parameters[60].Value = model.OldUnit;
			parameters[61].Value = model.ForRegional;
			parameters[62].Value = model.ArchiveState;
			parameters[63].Value = model.Memo;
			parameters[64].Value = model.IsShow;
			parameters[65].Value = model.IsDelete;
			parameters[66].Value = model.DeleteDate;
			parameters[67].Value = model.WorkAchievement;
			parameters[68].Value = model.FamilyContext;
			parameters[69].Value = model.InterestLove;
			parameters[70].Value = model.StudySuffer;
			parameters[71].Value = model.JobSuffer;
			parameters[72].Value = model.ChangeLog;
			parameters[73].Value = model.IsLogin;
			parameters[74].Value = model.IsDimission;
			parameters[75].Value = model.IsHostelSearch;
			parameters[76].Value = model.OrganizeCode;
			parameters[77].Value = model.SearchCommID;
			parameters[78].Value = model.IsMobile;
			parameters[79].Value = model.MachineCodes;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_User_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string UserCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = UserCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_User_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_User GetModel(string UserCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = UserCode;

			MobileSoft.Model.Sys.Tb_Sys_User model=new MobileSoft.Model.Sys.Tb_Sys_User();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_User_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				model.UserName=ds.Tables[0].Rows[0]["UserName"].ToString();
				model.DepCode=ds.Tables[0].Rows[0]["DepCode"].ToString();
				model.LoginCode=ds.Tables[0].Rows[0]["LoginCode"].ToString();
				model.PassWord=ds.Tables[0].Rows[0]["PassWord"].ToString();
				model.Sex=ds.Tables[0].Rows[0]["Sex"].ToString();
				model.MobileTel=ds.Tables[0].Rows[0]["MobileTel"].ToString();
				model.WorkedTel=ds.Tables[0].Rows[0]["WorkedTel"].ToString();
				model.PersonalTel=ds.Tables[0].Rows[0]["PersonalTel"].ToString();
				model.IDCardCode=ds.Tables[0].Rows[0]["IDCardCode"].ToString();
				model.LinkmanName=ds.Tables[0].Rows[0]["LinkmanName"].ToString();
				model.LinkmanTel=ds.Tables[0].Rows[0]["LinkmanTel"].ToString();
				model.Email=ds.Tables[0].Rows[0]["Email"].ToString();
				if(ds.Tables[0].Rows[0]["IsPublicMail"].ToString()!="")
				{
					model.IsPublicMail=int.Parse(ds.Tables[0].Rows[0]["IsPublicMail"].ToString());
				}
				model.EmployeeCode=ds.Tables[0].Rows[0]["EmployeeCode"].ToString();
				model.WorkerSign=ds.Tables[0].Rows[0]["WorkerSign"].ToString();
				model.Duty=ds.Tables[0].Rows[0]["Duty"].ToString();
				model.BankAccounts=ds.Tables[0].Rows[0]["BankAccounts"].ToString();
				model.Qualification=ds.Tables[0].Rows[0]["Qualification"].ToString();
				model.NativePlace=ds.Tables[0].Rows[0]["NativePlace"].ToString();
				model.Speciality=ds.Tables[0].Rows[0]["Speciality"].ToString();
				model.MaritalStatus=ds.Tables[0].Rows[0]["MaritalStatus"].ToString();
				model.Stature=ds.Tables[0].Rows[0]["Stature"].ToString();
				if(ds.Tables[0].Rows[0]["Birthday"].ToString()!="")
				{
					model.Birthday=DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
				}
				model.Avoirdupois=ds.Tables[0].Rows[0]["Avoirdupois"].ToString();
				model.Nation=ds.Tables[0].Rows[0]["Nation"].ToString();
				model.Politics=ds.Tables[0].Rows[0]["Politics"].ToString();
				model.TechnicTitle=ds.Tables[0].Rows[0]["TechnicTitle"].ToString();
				model.Skills=ds.Tables[0].Rows[0]["Skills"].ToString();
				model.GuardCard=ds.Tables[0].Rows[0]["GuardCard"].ToString();
				model.Computer=ds.Tables[0].Rows[0]["Computer"].ToString();
				if(ds.Tables[0].Rows[0]["WorkingTime"].ToString()!="")
				{
					model.WorkingTime=DateTime.Parse(ds.Tables[0].Rows[0]["WorkingTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["JoinTime"].ToString()!="")
				{
					model.JoinTime=DateTime.Parse(ds.Tables[0].Rows[0]["JoinTime"].ToString());
				}
				model.Residence=ds.Tables[0].Rows[0]["Residence"].ToString();
				model.Address=ds.Tables[0].Rows[0]["Address"].ToString();
				model.Post=ds.Tables[0].Rows[0]["Post"].ToString();
				model.ArchivesAddr=ds.Tables[0].Rows[0]["ArchivesAddr"].ToString();
				model.PreInsuranceCode=ds.Tables[0].Rows[0]["PreInsuranceCode"].ToString();
				model.InsuranceCode=ds.Tables[0].Rows[0]["InsuranceCode"].ToString();
				model.ResidenceType=ds.Tables[0].Rows[0]["ResidenceType"].ToString();
				if(ds.Tables[0].Rows[0]["IsBuyInsurance"].ToString()!="")
				{
					model.IsBuyInsurance=int.Parse(ds.Tables[0].Rows[0]["IsBuyInsurance"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsAgreeBuy"].ToString()!="")
				{
					model.IsAgreeBuy=int.Parse(ds.Tables[0].Rows[0]["IsAgreeBuy"].ToString());
				}
				model.PayLevel=ds.Tables[0].Rows[0]["PayLevel"].ToString();
				if(ds.Tables[0].Rows[0]["WorkingDate"].ToString()!="")
				{
					model.WorkingDate=DateTime.Parse(ds.Tables[0].Rows[0]["WorkingDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["JionDate"].ToString()!="")
				{
					model.JionDate=DateTime.Parse(ds.Tables[0].Rows[0]["JionDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ProbationDate"].ToString()!="")
				{
					model.ProbationDate=DateTime.Parse(ds.Tables[0].Rows[0]["ProbationDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FormalDate"].ToString()!="")
				{
					model.FormalDate=DateTime.Parse(ds.Tables[0].Rows[0]["FormalDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DimissionDate"].ToString()!="")
				{
					model.DimissionDate=DateTime.Parse(ds.Tables[0].Rows[0]["DimissionDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["InureDate"].ToString()!="")
				{
					model.InureDate=DateTime.Parse(ds.Tables[0].Rows[0]["InureDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PraxisDate"].ToString()!="")
				{
					model.PraxisDate=DateTime.Parse(ds.Tables[0].Rows[0]["PraxisDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["JoinPartyDate"].ToString()!="")
				{
					model.JoinPartyDate=DateTime.Parse(ds.Tables[0].Rows[0]["JoinPartyDate"].ToString());
				}
				model.HealthState=ds.Tables[0].Rows[0]["HealthState"].ToString();
				model.OldName=ds.Tables[0].Rows[0]["OldName"].ToString();
				model.EyeLeft=ds.Tables[0].Rows[0]["EyeLeft"].ToString();
				model.EyeRight=ds.Tables[0].Rows[0]["EyeRight"].ToString();
				model.FamilyBirth=ds.Tables[0].Rows[0]["FamilyBirth"].ToString();
				model.BloodGroup=ds.Tables[0].Rows[0]["BloodGroup"].ToString();
				model.School=ds.Tables[0].Rows[0]["School"].ToString();
				model.Approach=ds.Tables[0].Rows[0]["Approach"].ToString();
				model.OldUnit=ds.Tables[0].Rows[0]["OldUnit"].ToString();
				model.ForRegional=ds.Tables[0].Rows[0]["ForRegional"].ToString();
				if(ds.Tables[0].Rows[0]["ArchiveState"].ToString()!="")
				{
					model.ArchiveState=int.Parse(ds.Tables[0].Rows[0]["ArchiveState"].ToString());
				}
				model.Memo=ds.Tables[0].Rows[0]["Memo"].ToString();
				if(ds.Tables[0].Rows[0]["IsShow"].ToString()!="")
				{
					model.IsShow=int.Parse(ds.Tables[0].Rows[0]["IsShow"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DeleteDate"].ToString()!="")
				{
					model.DeleteDate=DateTime.Parse(ds.Tables[0].Rows[0]["DeleteDate"].ToString());
				}
				model.WorkAchievement=ds.Tables[0].Rows[0]["WorkAchievement"].ToString();
				model.FamilyContext=ds.Tables[0].Rows[0]["FamilyContext"].ToString();
				model.InterestLove=ds.Tables[0].Rows[0]["InterestLove"].ToString();
				model.StudySuffer=ds.Tables[0].Rows[0]["StudySuffer"].ToString();
				model.JobSuffer=ds.Tables[0].Rows[0]["JobSuffer"].ToString();
				model.ChangeLog=ds.Tables[0].Rows[0]["ChangeLog"].ToString();
				if(ds.Tables[0].Rows[0]["IsLogin"].ToString()!="")
				{
					model.IsLogin=int.Parse(ds.Tables[0].Rows[0]["IsLogin"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDimission"].ToString()!="")
				{
					model.IsDimission=int.Parse(ds.Tables[0].Rows[0]["IsDimission"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsHostelSearch"].ToString()!="")
				{
					model.IsHostelSearch=int.Parse(ds.Tables[0].Rows[0]["IsHostelSearch"].ToString());
				}
				model.OrganizeCode=ds.Tables[0].Rows[0]["OrganizeCode"].ToString();
				if(ds.Tables[0].Rows[0]["SearchCommID"].ToString()!="")
				{
					model.SearchCommID=int.Parse(ds.Tables[0].Rows[0]["SearchCommID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsMobile"].ToString()!="")
				{
					model.IsMobile=int.Parse(ds.Tables[0].Rows[0]["IsMobile"].ToString());
				}
				model.MachineCodes=ds.Tables[0].Rows[0]["MachineCodes"].ToString();
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
			StringBuilder strSql=new StringBuilder();
            // 2017年6月12日16:14:28，谭洋，添加SortDepCode字段
            strSql.Append("select HeadImg,UserCode,CommID,UserName,DepCode,LoginCode,PassWord,Sex,MobileTel,WorkedTel,PersonalTel,IDCardCode,LinkmanName,LinkmanTel,Email,IsPublicMail,EmployeeCode,WorkerSign,Duty,BankAccounts,Qualification,NativePlace,Speciality,MaritalStatus,Stature,Birthday,Avoirdupois,Nation,Politics,TechnicTitle,Skills,GuardCard,Computer,WorkingTime,JoinTime,Residence,Address,Post,ArchivesAddr,PreInsuranceCode,InsuranceCode,ResidenceType,IsBuyInsurance,IsAgreeBuy,PayLevel,WorkingDate,JionDate,ProbationDate,FormalDate,DimissionDate,InureDate,PraxisDate,JoinPartyDate,HealthState,OldName,EyeLeft,EyeRight,FamilyBirth,BloodGroup,School,Approach,OldUnit,ForRegional,ArchiveState,Memo,IsShow,IsDelete,DeleteDate,WorkAchievement,FamilyContext,InterestLove,StudySuffer,JobSuffer,ChangeLog,IsLogin,IsDimission,IsHostelSearch,OrganizeCode,SearchCommID,IsMobile,MachineCodes, (select SortDepCode FROM Tb_Sys_Department b where b.DepCode=a.DepCode) as SortDepCode ");
			strSql.Append(" FROM Tb_Sys_User  a");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string fieldOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" UserCode,CommID,UserName,DepCode,LoginCode,PassWord,Sex,MobileTel,WorkedTel,PersonalTel,IDCardCode,LinkmanName,LinkmanTel,Email,IsPublicMail,EmployeeCode,WorkerSign,Duty,BankAccounts,Qualification,NativePlace,Speciality,MaritalStatus,Stature,Birthday,Avoirdupois,Nation,Politics,TechnicTitle,Skills,GuardCard,Computer,WorkingTime,JoinTime,Residence,Address,Post,ArchivesAddr,PreInsuranceCode,InsuranceCode,ResidenceType,IsBuyInsurance,IsAgreeBuy,PayLevel,WorkingDate,JionDate,ProbationDate,FormalDate,DimissionDate,InureDate,PraxisDate,JoinPartyDate,HealthState,OldName,EyeLeft,EyeRight,FamilyBirth,BloodGroup,School,Approach,OldUnit,ForRegional,ArchiveState,Memo,IsShow,IsDelete,DeleteDate,WorkAchievement,FamilyContext,InterestLove,StudySuffer,JobSuffer,ChangeLog,IsLogin,IsDimission,IsHostelSearch,OrganizeCode,SearchCommID,IsMobile,MachineCodes ");
			strSql.Append(" FROM Tb_Sys_User ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + fieldOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize,string SortField,int Sort)
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
			parameters[5].Value = "SELECT * FROM Tb_Sys_User WHERE 1=1 " + StrCondition;
			parameters[6].Value = "UserCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

        public string Sys_User_FilterRoles(string UserCode, string OrganCode, int CommID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
                              new SqlParameter("@OrganCode", SqlDbType.VarChar,50),
                              new SqlParameter("@CommID", SqlDbType.Int)
                                              };
            parameters[0].Value = UserCode;
            parameters[1].Value = OrganCode;
            parameters[2].Value = CommID;

            DataTable DataTableResult = DbHelperSQL.RunProcedure("Proc_Sys_User_FilterRoles", parameters, "RetDataSet").Tables[0];

            string result = "";

            if (DataTableResult.Rows.Count > 0)
            {
                result = DataTableResult.Rows[0][0].ToString();
            }

            return result;
        }

            public DataTable FilterRoles(string UserCode, string OrganCode, int CommID)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
                              new SqlParameter("@OrganCode", SqlDbType.VarChar,50),
                              new SqlParameter("@CommID", SqlDbType.Int)
                                              };
                  parameters[0].Value = UserCode;
                  parameters[1].Value = OrganCode;
                  parameters[2].Value = CommID;

                  DataTable result = DbHelperSQL.RunProcedure("Proc_Sys_User_FilterRoles", parameters, "RetDataSet").Tables[0];

                  return result;
            }

		#endregion  成员方法
	}
}

