using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_Customer。
	/// </summary>
	public class Dal_Tb_HSPR_Customer
	{
		public Dal_Tb_HSPR_Customer()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long CustID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CustID", SqlDbType.BigInt)};
			parameters[0].Value = CustID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Customer_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_Customer model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@CustName", SqlDbType.NVarChar,50),
					new SqlParameter("@FixedTel", SqlDbType.NVarChar,500),
					new SqlParameter("@MobilePhone", SqlDbType.NVarChar,500),
					new SqlParameter("@FaxTel", SqlDbType.NVarChar,500),
					new SqlParameter("@EMail", SqlDbType.NVarChar,500),
					new SqlParameter("@Surname", SqlDbType.NVarChar,10),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@Sex", SqlDbType.NVarChar,10),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@Nationality", SqlDbType.NVarChar,30),
					new SqlParameter("@WorkUnit", SqlDbType.NVarChar,300),
					new SqlParameter("@PaperName", SqlDbType.NVarChar,20),
					new SqlParameter("@PaperCode", SqlDbType.NVarChar,30),
					new SqlParameter("@PassSign", SqlDbType.NVarChar,20),
					new SqlParameter("@LegalRepr", SqlDbType.NVarChar,20),
					new SqlParameter("@LegalReprTel", SqlDbType.NVarChar,20),
					new SqlParameter("@Charge", SqlDbType.NVarChar,20),
					new SqlParameter("@ChargeTel", SqlDbType.NVarChar,20),
					new SqlParameter("@Linkman", SqlDbType.NVarChar,20),
					new SqlParameter("@LinkmanTel", SqlDbType.NVarChar,20),
					new SqlParameter("@BankName", SqlDbType.NVarChar,20),
					new SqlParameter("@BankIDs", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAccount", SqlDbType.NVarChar,30),
					new SqlParameter("@BankAgreement", SqlDbType.NVarChar,20),
					new SqlParameter("@InquirePwd", SqlDbType.NVarChar,20),
					new SqlParameter("@InquireAccount", SqlDbType.NVarChar,20),
					new SqlParameter("@Memo", SqlDbType.NVarChar,1000),
					new SqlParameter("@IsUnit", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@Address", SqlDbType.NVarChar,1000),
					new SqlParameter("@PostCode", SqlDbType.NVarChar,100),
					new SqlParameter("@Recipient", SqlDbType.NVarChar,100),
					new SqlParameter("@Hobbies", SqlDbType.NVarChar,200),
					new SqlParameter("@LiveType1", SqlDbType.SmallInt,2),
					new SqlParameter("@LiveType2", SqlDbType.SmallInt,2),
					new SqlParameter("@LiveType3", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDebts", SqlDbType.SmallInt,2),
					new SqlParameter("@IsUsual", SqlDbType.SmallInt,2),
					new SqlParameter("@RoomSigns", SqlDbType.Text),
					new SqlParameter("@RoomCount", SqlDbType.Int,4),
					new SqlParameter("@Job", SqlDbType.NVarChar,50),
					new SqlParameter("@MGradeSign", SqlDbType.NVarChar,30),
					new SqlParameter("@BankCode", SqlDbType.NVarChar,20),
					new SqlParameter("@BankNo", SqlDbType.NVarChar,20),
					new SqlParameter("@CustSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@SendCardDate", SqlDbType.DateTime),
					new SqlParameter("@IsSendCard", SqlDbType.SmallInt,2),
					new SqlParameter("@UnCustID", SqlDbType.BigInt,8),
					new SqlParameter("@IsSelfPay", SqlDbType.SmallInt,2),
					new SqlParameter("@PayEnPass", SqlDbType.NVarChar,50),
					new SqlParameter("@PersonRole", SqlDbType.BigInt,8),
					new SqlParameter("@OrganizeCode", SqlDbType.NVarChar,100),
					new SqlParameter("@JoinDate", SqlDbType.DateTime),
					new SqlParameter("@IsHostel", SqlDbType.Int,4),
					new SqlParameter("@BindMobile", SqlDbType.NVarChar,50),
					new SqlParameter("@CustBedLiveState", SqlDbType.Int,4),
					new SqlParameter("@CustBedLiveNum", SqlDbType.BigInt,8),
					new SqlParameter("@CustBedLiveDate", SqlDbType.DateTime),
					new SqlParameter("@CustBedExitDate", SqlDbType.DateTime),
					new SqlParameter("@RegisterDate", SqlDbType.DateTime),
					new SqlParameter("@ArrearsSubID", SqlDbType.BigInt,8),
					new SqlParameter("@BankProvince", SqlDbType.NVarChar,50),
					new SqlParameter("@BankCity", SqlDbType.NVarChar,50),
					new SqlParameter("@BankInfo", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.CustID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CustTypeID;
			parameters[3].Value = model.CustName;
			parameters[4].Value = model.FixedTel;
			parameters[5].Value = model.MobilePhone;
			parameters[6].Value = model.FaxTel;
			parameters[7].Value = model.EMail;
			parameters[8].Value = model.Surname;
			parameters[9].Value = model.Name;
			parameters[10].Value = model.Sex;
			parameters[11].Value = model.Birthday;
			parameters[12].Value = model.Nationality;
			parameters[13].Value = model.WorkUnit;
			parameters[14].Value = model.PaperName;
			parameters[15].Value = model.PaperCode;
			parameters[16].Value = model.PassSign;
			parameters[17].Value = model.LegalRepr;
			parameters[18].Value = model.LegalReprTel;
			parameters[19].Value = model.Charge;
			parameters[20].Value = model.ChargeTel;
			parameters[21].Value = model.Linkman;
			parameters[22].Value = model.LinkmanTel;
			parameters[23].Value = model.BankName;
			parameters[24].Value = model.BankIDs;
			parameters[25].Value = model.BankAccount;
			parameters[26].Value = model.BankAgreement;
			parameters[27].Value = model.InquirePwd;
			parameters[28].Value = model.InquireAccount;
			parameters[29].Value = model.Memo;
			parameters[30].Value = model.IsUnit;
			parameters[31].Value = model.IsDelete;
			parameters[32].Value = model.Address;
			parameters[33].Value = model.PostCode;
			parameters[34].Value = model.Recipient;
			parameters[35].Value = model.Hobbies;
			parameters[36].Value = model.LiveType1;
			parameters[37].Value = model.LiveType2;
			parameters[38].Value = model.LiveType3;
			parameters[39].Value = model.IsDebts;
			parameters[40].Value = model.IsUsual;
			parameters[41].Value = model.RoomSigns;
			parameters[42].Value = model.RoomCount;
			parameters[43].Value = model.Job;
			parameters[44].Value = model.MGradeSign;
			parameters[45].Value = model.BankCode;
			parameters[46].Value = model.BankNo;
			parameters[47].Value = model.CustSynchCode;
			parameters[48].Value = model.SynchFlag;
			parameters[49].Value = model.SendCardDate;
			parameters[50].Value = model.IsSendCard;
			parameters[51].Value = model.UnCustID;
			parameters[52].Value = model.IsSelfPay;
			parameters[53].Value = model.PayEnPass;
			parameters[54].Value = model.PersonRole;
			parameters[55].Value = model.OrganizeCode;
			parameters[56].Value = model.JoinDate;
			parameters[57].Value = model.IsHostel;
			parameters[58].Value = model.BindMobile;
			parameters[59].Value = model.CustBedLiveState;
			parameters[60].Value = model.CustBedLiveNum;
			parameters[61].Value = model.CustBedLiveDate;
			parameters[62].Value = model.CustBedExitDate;
			parameters[63].Value = model.RegisterDate;
			parameters[64].Value = model.ArrearsSubID;
			parameters[65].Value = model.BankProvince;
			parameters[66].Value = model.BankCity;
			parameters[67].Value = model.BankInfo;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Customer_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_Customer model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@CustName", SqlDbType.NVarChar,50),
					new SqlParameter("@FixedTel", SqlDbType.NVarChar,500),
					new SqlParameter("@MobilePhone", SqlDbType.NVarChar,500),
					new SqlParameter("@FaxTel", SqlDbType.NVarChar,500),
					new SqlParameter("@EMail", SqlDbType.NVarChar,500),
					new SqlParameter("@Surname", SqlDbType.NVarChar,10),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@Sex", SqlDbType.NVarChar,10),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@Nationality", SqlDbType.NVarChar,30),
					new SqlParameter("@WorkUnit", SqlDbType.NVarChar,300),
					new SqlParameter("@PaperName", SqlDbType.NVarChar,20),
					new SqlParameter("@PaperCode", SqlDbType.NVarChar,30),
					new SqlParameter("@PassSign", SqlDbType.NVarChar,20),
					new SqlParameter("@LegalRepr", SqlDbType.NVarChar,20),
					new SqlParameter("@LegalReprTel", SqlDbType.NVarChar,20),
					new SqlParameter("@Charge", SqlDbType.NVarChar,20),
					new SqlParameter("@ChargeTel", SqlDbType.NVarChar,20),
					new SqlParameter("@Linkman", SqlDbType.NVarChar,20),
					new SqlParameter("@LinkmanTel", SqlDbType.NVarChar,20),
					new SqlParameter("@BankName", SqlDbType.NVarChar,20),
					new SqlParameter("@BankIDs", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAccount", SqlDbType.NVarChar,30),
					new SqlParameter("@BankAgreement", SqlDbType.NVarChar,20),
					new SqlParameter("@InquirePwd", SqlDbType.NVarChar,20),
					new SqlParameter("@InquireAccount", SqlDbType.NVarChar,20),
					new SqlParameter("@Memo", SqlDbType.NVarChar,1000),
					new SqlParameter("@IsUnit", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@Address", SqlDbType.NVarChar,1000),
					new SqlParameter("@PostCode", SqlDbType.NVarChar,100),
					new SqlParameter("@Recipient", SqlDbType.NVarChar,100),
					new SqlParameter("@Hobbies", SqlDbType.NVarChar,200),
					new SqlParameter("@LiveType1", SqlDbType.SmallInt,2),
					new SqlParameter("@LiveType2", SqlDbType.SmallInt,2),
					new SqlParameter("@LiveType3", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDebts", SqlDbType.SmallInt,2),
					new SqlParameter("@IsUsual", SqlDbType.SmallInt,2),
					new SqlParameter("@RoomSigns", SqlDbType.Text),
					new SqlParameter("@RoomCount", SqlDbType.Int,4),
					new SqlParameter("@Job", SqlDbType.NVarChar,50),
					new SqlParameter("@MGradeSign", SqlDbType.NVarChar,30),
					new SqlParameter("@BankCode", SqlDbType.NVarChar,20),
					new SqlParameter("@BankNo", SqlDbType.NVarChar,20),
					new SqlParameter("@CustSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@SendCardDate", SqlDbType.DateTime),
					new SqlParameter("@IsSendCard", SqlDbType.SmallInt,2),
					new SqlParameter("@UnCustID", SqlDbType.BigInt,8),
					new SqlParameter("@IsSelfPay", SqlDbType.SmallInt,2),
					new SqlParameter("@PayEnPass", SqlDbType.NVarChar,50),
					new SqlParameter("@PersonRole", SqlDbType.BigInt,8),
					new SqlParameter("@OrganizeCode", SqlDbType.NVarChar,100),
					new SqlParameter("@JoinDate", SqlDbType.DateTime),
					new SqlParameter("@IsHostel", SqlDbType.Int,4),
					new SqlParameter("@BindMobile", SqlDbType.NVarChar,50),
					new SqlParameter("@CustBedLiveState", SqlDbType.Int,4),
					new SqlParameter("@CustBedLiveNum", SqlDbType.BigInt,8),
					new SqlParameter("@CustBedLiveDate", SqlDbType.DateTime),
					new SqlParameter("@CustBedExitDate", SqlDbType.DateTime),
					new SqlParameter("@RegisterDate", SqlDbType.DateTime),
					new SqlParameter("@ArrearsSubID", SqlDbType.BigInt,8),
					new SqlParameter("@BankProvince", SqlDbType.NVarChar,50),
					new SqlParameter("@BankCity", SqlDbType.NVarChar,50),
					new SqlParameter("@BankInfo", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.CustID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CustTypeID;
			parameters[3].Value = model.CustName;
			parameters[4].Value = model.FixedTel;
			parameters[5].Value = model.MobilePhone;
			parameters[6].Value = model.FaxTel;
			parameters[7].Value = model.EMail;
			parameters[8].Value = model.Surname;
			parameters[9].Value = model.Name;
			parameters[10].Value = model.Sex;
			parameters[11].Value = model.Birthday;
			parameters[12].Value = model.Nationality;
			parameters[13].Value = model.WorkUnit;
			parameters[14].Value = model.PaperName;
			parameters[15].Value = model.PaperCode;
			parameters[16].Value = model.PassSign;
			parameters[17].Value = model.LegalRepr;
			parameters[18].Value = model.LegalReprTel;
			parameters[19].Value = model.Charge;
			parameters[20].Value = model.ChargeTel;
			parameters[21].Value = model.Linkman;
			parameters[22].Value = model.LinkmanTel;
			parameters[23].Value = model.BankName;
			parameters[24].Value = model.BankIDs;
			parameters[25].Value = model.BankAccount;
			parameters[26].Value = model.BankAgreement;
			parameters[27].Value = model.InquirePwd;
			parameters[28].Value = model.InquireAccount;
			parameters[29].Value = model.Memo;
			parameters[30].Value = model.IsUnit;
			parameters[31].Value = model.IsDelete;
			parameters[32].Value = model.Address;
			parameters[33].Value = model.PostCode;
			parameters[34].Value = model.Recipient;
			parameters[35].Value = model.Hobbies;
			parameters[36].Value = model.LiveType1;
			parameters[37].Value = model.LiveType2;
			parameters[38].Value = model.LiveType3;
			parameters[39].Value = model.IsDebts;
			parameters[40].Value = model.IsUsual;
			parameters[41].Value = model.RoomSigns;
			parameters[42].Value = model.RoomCount;
			parameters[43].Value = model.Job;
			parameters[44].Value = model.MGradeSign;
			parameters[45].Value = model.BankCode;
			parameters[46].Value = model.BankNo;
			parameters[47].Value = model.CustSynchCode;
			parameters[48].Value = model.SynchFlag;
			parameters[49].Value = model.SendCardDate;
			parameters[50].Value = model.IsSendCard;
			parameters[51].Value = model.UnCustID;
			parameters[52].Value = model.IsSelfPay;
			parameters[53].Value = model.PayEnPass;
			parameters[54].Value = model.PersonRole;
			parameters[55].Value = model.OrganizeCode;
			parameters[56].Value = model.JoinDate;
			parameters[57].Value = model.IsHostel;
			parameters[58].Value = model.BindMobile;
			parameters[59].Value = model.CustBedLiveState;
			parameters[60].Value = model.CustBedLiveNum;
			parameters[61].Value = model.CustBedLiveDate;
			parameters[62].Value = model.CustBedExitDate;
			parameters[63].Value = model.RegisterDate;
			parameters[64].Value = model.ArrearsSubID;
			parameters[65].Value = model.BankProvince;
			parameters[66].Value = model.BankCity;
			parameters[67].Value = model.BankInfo;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Customer_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long CustID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CustID", SqlDbType.BigInt)};
			parameters[0].Value = CustID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Customer_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_Customer GetModel(long CustID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@CustID", SqlDbType.BigInt)};
			parameters[0].Value = CustID;

			MobileSoft.Model.HSPR.Tb_HSPR_Customer model=new MobileSoft.Model.HSPR.Tb_HSPR_Customer();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Customer_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["CustID"].ToString()!="")
				{
					model.CustID=long.Parse(ds.Tables[0].Rows[0]["CustID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CustTypeID"].ToString()!="")
				{
					model.CustTypeID=long.Parse(ds.Tables[0].Rows[0]["CustTypeID"].ToString());
				}
				model.CustName=ds.Tables[0].Rows[0]["CustName"].ToString();
				model.FixedTel=ds.Tables[0].Rows[0]["FixedTel"].ToString();
				model.MobilePhone=ds.Tables[0].Rows[0]["MobilePhone"].ToString();
				model.FaxTel=ds.Tables[0].Rows[0]["FaxTel"].ToString();
				model.EMail=ds.Tables[0].Rows[0]["EMail"].ToString();
				model.Surname=ds.Tables[0].Rows[0]["Surname"].ToString();
				model.Name=ds.Tables[0].Rows[0]["Name"].ToString();
				model.Sex=ds.Tables[0].Rows[0]["Sex"].ToString();
				if(ds.Tables[0].Rows[0]["Birthday"].ToString()!="")
				{
					model.Birthday=DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
				}
				model.Nationality=ds.Tables[0].Rows[0]["Nationality"].ToString();
				model.WorkUnit=ds.Tables[0].Rows[0]["WorkUnit"].ToString();
				model.PaperName=ds.Tables[0].Rows[0]["PaperName"].ToString();
				model.PaperCode=ds.Tables[0].Rows[0]["PaperCode"].ToString();
				model.PassSign=ds.Tables[0].Rows[0]["PassSign"].ToString();
				model.LegalRepr=ds.Tables[0].Rows[0]["LegalRepr"].ToString();
				model.LegalReprTel=ds.Tables[0].Rows[0]["LegalReprTel"].ToString();
				model.Charge=ds.Tables[0].Rows[0]["Charge"].ToString();
				model.ChargeTel=ds.Tables[0].Rows[0]["ChargeTel"].ToString();
				model.Linkman=ds.Tables[0].Rows[0]["Linkman"].ToString();
				model.LinkmanTel=ds.Tables[0].Rows[0]["LinkmanTel"].ToString();
				model.BankName=ds.Tables[0].Rows[0]["BankName"].ToString();
				model.BankIDs=ds.Tables[0].Rows[0]["BankIDs"].ToString();
				model.BankAccount=ds.Tables[0].Rows[0]["BankAccount"].ToString();
				model.BankAgreement=ds.Tables[0].Rows[0]["BankAgreement"].ToString();
				model.InquirePwd=ds.Tables[0].Rows[0]["InquirePwd"].ToString();
				model.InquireAccount=ds.Tables[0].Rows[0]["InquireAccount"].ToString();
				model.Memo=ds.Tables[0].Rows[0]["Memo"].ToString();
				if(ds.Tables[0].Rows[0]["IsUnit"].ToString()!="")
				{
					model.IsUnit=int.Parse(ds.Tables[0].Rows[0]["IsUnit"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				model.Address=ds.Tables[0].Rows[0]["Address"].ToString();
				model.PostCode=ds.Tables[0].Rows[0]["PostCode"].ToString();
				model.Recipient=ds.Tables[0].Rows[0]["Recipient"].ToString();
				model.Hobbies=ds.Tables[0].Rows[0]["Hobbies"].ToString();
				if(ds.Tables[0].Rows[0]["LiveType1"].ToString()!="")
				{
					model.LiveType1=int.Parse(ds.Tables[0].Rows[0]["LiveType1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LiveType2"].ToString()!="")
				{
					model.LiveType2=int.Parse(ds.Tables[0].Rows[0]["LiveType2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LiveType3"].ToString()!="")
				{
					model.LiveType3=int.Parse(ds.Tables[0].Rows[0]["LiveType3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDebts"].ToString()!="")
				{
					model.IsDebts=int.Parse(ds.Tables[0].Rows[0]["IsDebts"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsUsual"].ToString()!="")
				{
					model.IsUsual=int.Parse(ds.Tables[0].Rows[0]["IsUsual"].ToString());
				}
				model.RoomSigns=ds.Tables[0].Rows[0]["RoomSigns"].ToString();
				if(ds.Tables[0].Rows[0]["RoomCount"].ToString()!="")
				{
					model.RoomCount=int.Parse(ds.Tables[0].Rows[0]["RoomCount"].ToString());
				}
				model.Job=ds.Tables[0].Rows[0]["Job"].ToString();
				model.MGradeSign=ds.Tables[0].Rows[0]["MGradeSign"].ToString();
				model.BankCode=ds.Tables[0].Rows[0]["BankCode"].ToString();
				model.BankNo=ds.Tables[0].Rows[0]["BankNo"].ToString();
				if(ds.Tables[0].Rows[0]["CustSynchCode"].ToString()!="")
				{
					model.CustSynchCode=new Guid(ds.Tables[0].Rows[0]["CustSynchCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SynchFlag"].ToString()!="")
				{
					model.SynchFlag=int.Parse(ds.Tables[0].Rows[0]["SynchFlag"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SendCardDate"].ToString()!="")
				{
					model.SendCardDate=DateTime.Parse(ds.Tables[0].Rows[0]["SendCardDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsSendCard"].ToString()!="")
				{
					model.IsSendCard=int.Parse(ds.Tables[0].Rows[0]["IsSendCard"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UnCustID"].ToString()!="")
				{
					model.UnCustID=long.Parse(ds.Tables[0].Rows[0]["UnCustID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsSelfPay"].ToString()!="")
				{
					model.IsSelfPay=int.Parse(ds.Tables[0].Rows[0]["IsSelfPay"].ToString());
				}
				model.PayEnPass=ds.Tables[0].Rows[0]["PayEnPass"].ToString();
				if(ds.Tables[0].Rows[0]["PersonRole"].ToString()!="")
				{
					model.PersonRole=long.Parse(ds.Tables[0].Rows[0]["PersonRole"].ToString());
				}
				model.OrganizeCode=ds.Tables[0].Rows[0]["OrganizeCode"].ToString();
				if(ds.Tables[0].Rows[0]["JoinDate"].ToString()!="")
				{
					model.JoinDate=DateTime.Parse(ds.Tables[0].Rows[0]["JoinDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsHostel"].ToString()!="")
				{
					model.IsHostel=int.Parse(ds.Tables[0].Rows[0]["IsHostel"].ToString());
				}
				model.BindMobile=ds.Tables[0].Rows[0]["BindMobile"].ToString();
				if(ds.Tables[0].Rows[0]["CustBedLiveState"].ToString()!="")
				{
					model.CustBedLiveState=int.Parse(ds.Tables[0].Rows[0]["CustBedLiveState"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CustBedLiveNum"].ToString()!="")
				{
					model.CustBedLiveNum=long.Parse(ds.Tables[0].Rows[0]["CustBedLiveNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CustBedLiveDate"].ToString()!="")
				{
					model.CustBedLiveDate=DateTime.Parse(ds.Tables[0].Rows[0]["CustBedLiveDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CustBedExitDate"].ToString()!="")
				{
					model.CustBedExitDate=DateTime.Parse(ds.Tables[0].Rows[0]["CustBedExitDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RegisterDate"].ToString()!="")
				{
					model.RegisterDate=DateTime.Parse(ds.Tables[0].Rows[0]["RegisterDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ArrearsSubID"].ToString()!="")
				{
					model.ArrearsSubID=long.Parse(ds.Tables[0].Rows[0]["ArrearsSubID"].ToString());
				}
				model.BankProvince=ds.Tables[0].Rows[0]["BankProvince"].ToString();
				model.BankCity=ds.Tables[0].Rows[0]["BankCity"].ToString();
				model.BankInfo=ds.Tables[0].Rows[0]["BankInfo"].ToString();
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
			strSql.Append("select CustID,CommID,CustTypeID,CustName,FixedTel,MobilePhone,FaxTel,EMail,Surname,Name,Sex,Birthday,Nationality,WorkUnit,PaperName,PaperCode,PassSign,LegalRepr,LegalReprTel,Charge,ChargeTel,Linkman,LinkmanTel,BankName,BankIDs,BankAccount,BankAgreement,InquirePwd,InquireAccount,Memo,IsUnit,IsDelete,Address,PostCode,Recipient,Hobbies,LiveType1,LiveType2,LiveType3,IsDebts,IsUsual,RoomSigns,RoomCount,Job,MGradeSign,BankCode,BankNo,CustSynchCode,SynchFlag,SendCardDate,IsSendCard,UnCustID,IsSelfPay,PayEnPass,PersonRole,OrganizeCode,JoinDate,IsHostel,BindMobile,CustBedLiveState,CustBedLiveNum,CustBedLiveDate,CustBedExitDate,RegisterDate,ArrearsSubID,BankProvince,BankCity,BankInfo ");
			strSql.Append(" FROM Tb_HSPR_Customer ");
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
			strSql.Append(" CustID,CommID,CustTypeID,CustName,FixedTel,MobilePhone,FaxTel,EMail,Surname,Name,Sex,Birthday,Nationality,WorkUnit,PaperName,PaperCode,PassSign,LegalRepr,LegalReprTel,Charge,ChargeTel,Linkman,LinkmanTel,BankName,BankIDs,BankAccount,BankAgreement,InquirePwd,InquireAccount,Memo,IsUnit,IsDelete,Address,PostCode,Recipient,Hobbies,LiveType1,LiveType2,LiveType3,IsDebts,IsUsual,RoomSigns,RoomCount,Job,MGradeSign,BankCode,BankNo,CustSynchCode,SynchFlag,SendCardDate,IsSendCard,UnCustID,IsSelfPay,PayEnPass,PersonRole,OrganizeCode,JoinDate,IsHostel,BindMobile,CustBedLiveState,CustBedLiveNum,CustBedLiveDate,CustBedExitDate,RegisterDate,ArrearsSubID,BankProvince,BankCity,BankInfo ");
			strSql.Append(" FROM Tb_HSPR_Customer ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_Customer WHERE 1=1 " + StrCondition;
			parameters[6].Value = "CustID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

