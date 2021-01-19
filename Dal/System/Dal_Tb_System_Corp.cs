using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.System
{
	/// <summary>
	/// 数据访问类Dal_Tb_System_Corp。
	/// </summary>
	public class Dal_Tb_System_Corp
	{
		public Dal_Tb_System_Corp()
		{
			DbHelperSQL.ConnectionString = PubConstant.tw2bsConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long CorpSID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CorpSID", SqlDbType.BigInt)};
			parameters[0].Value = CorpSID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_System_Corp_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.System.Tb_System_Corp model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CorpSID", SqlDbType.BigInt,8),
					new SqlParameter("@CorpID", SqlDbType.Int,4),
					new SqlParameter("@RegType", SqlDbType.SmallInt,2),
					new SqlParameter("@RegMode", SqlDbType.SmallInt,2),
					new SqlParameter("@CorpName", SqlDbType.NVarChar,100),
					new SqlParameter("@CorpShortName", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpTypeName", SqlDbType.NVarChar,20),
					new SqlParameter("@Province", SqlDbType.NVarChar,20),
					new SqlParameter("@City", SqlDbType.NVarChar,20),
					new SqlParameter("@Borough", SqlDbType.NVarChar,20),
					new SqlParameter("@Street", SqlDbType.NVarChar,200),
					new SqlParameter("@CommName", SqlDbType.NVarChar,50),
					new SqlParameter("@LoginCode", SqlDbType.NVarChar,50),
					new SqlParameter("@LoginPassWD", SqlDbType.NVarChar,30),
					new SqlParameter("@PwdQuestion", SqlDbType.NVarChar,50),
					new SqlParameter("@PwdAnswer", SqlDbType.NVarChar,30),
					new SqlParameter("@CorpAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@CorpPost", SqlDbType.NVarChar,10),
					new SqlParameter("@CorpDeputy", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpLinkMan", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpMobileTel", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpWorkedTel", SqlDbType.NVarChar,50),
					new SqlParameter("@CorpFax", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpEmail", SqlDbType.NVarChar,50),
					new SqlParameter("@CorpWeb", SqlDbType.NVarChar,50),
					new SqlParameter("@AdminUserName", SqlDbType.NVarChar,20),
					new SqlParameter("@AdminSex", SqlDbType.NVarChar,10),
					new SqlParameter("@AdminUserTel", SqlDbType.NVarChar,20),
					new SqlParameter("@AdminUserEmail", SqlDbType.NVarChar,50),
					new SqlParameter("@DBServer", SqlDbType.NVarChar,20),
					new SqlParameter("@DBName", SqlDbType.NVarChar,20),
					new SqlParameter("@DBUser", SqlDbType.NVarChar,20),
					new SqlParameter("@DBPwd", SqlDbType.NVarChar,20),
					new SqlParameter("@RegDate", SqlDbType.DateTime),
					new SqlParameter("@BranchNum", SqlDbType.Int,4),
					new SqlParameter("@CommNum", SqlDbType.Int,4),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@ImgLogo1", SqlDbType.NVarChar,100),
					new SqlParameter("@ImgLogo2", SqlDbType.NVarChar,100),
					new SqlParameter("@SysDir", SqlDbType.NVarChar,10),
					new SqlParameter("@SysVersion", SqlDbType.NVarChar,20),
					new SqlParameter("@QQ", SqlDbType.NVarChar,20),
					new SqlParameter("@StoreImage", SqlDbType.NVarChar,100),
					new SqlParameter("@VSPID", SqlDbType.Int,4),
					new SqlParameter("@CorpSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@LogoImgUrl", SqlDbType.NVarChar,50),
					new SqlParameter("@CorpSort", SqlDbType.Int,4),
					new SqlParameter("@IsRecommend", SqlDbType.SmallInt,2),
					new SqlParameter("@RecommendIndex", SqlDbType.NVarChar,5),
					new SqlParameter("@ActualCommSnum", SqlDbType.Int,4),
					new SqlParameter("@ContractMemo", SqlDbType.NVarChar,2000),
					new SqlParameter("@MobileSnum", SqlDbType.Int,4),
					new SqlParameter("@ActualMobileSnum", SqlDbType.Int,4),
					new SqlParameter("@CallVersion", SqlDbType.NVarChar,20),
					new SqlParameter("@IsValidCode", SqlDbType.SmallInt,2),
					new SqlParameter("@IsFees", SqlDbType.SmallInt,2),
					new SqlParameter("@ContractBeginDate", SqlDbType.DateTime),
					new SqlParameter("@ContractEndDate", SqlDbType.DateTime),
					new SqlParameter("@UseBeginDate", SqlDbType.DateTime),
					new SqlParameter("@UseEndDate", SqlDbType.DateTime),
					new SqlParameter("@AllowanceDate", SqlDbType.DateTime),
					new SqlParameter("@YiZhiFuNum", SqlDbType.VarChar,50),
					new SqlParameter("@YiZhiFuKey", SqlDbType.VarChar,50),
					new SqlParameter("@YiJiFuHuoBanId", SqlDbType.VarChar,50),
					new SqlParameter("@YiJiFuNum", SqlDbType.VarChar,50),
					new SqlParameter("@YiJiFuKey", SqlDbType.VarChar,50)};
			parameters[0].Value = model.CorpSID;
			parameters[1].Value = model.CorpID;
			parameters[2].Value = model.RegType;
			parameters[3].Value = model.RegMode;
			parameters[4].Value = model.CorpName;
			parameters[5].Value = model.CorpShortName;
			parameters[6].Value = model.CorpTypeName;
			parameters[7].Value = model.Province;
			parameters[8].Value = model.City;
			parameters[9].Value = model.Borough;
			parameters[10].Value = model.Street;
			parameters[11].Value = model.CommName;
			parameters[12].Value = model.LoginCode;
			parameters[13].Value = model.LoginPassWD;
			parameters[14].Value = model.PwdQuestion;
			parameters[15].Value = model.PwdAnswer;
			parameters[16].Value = model.CorpAddress;
			parameters[17].Value = model.CorpPost;
			parameters[18].Value = model.CorpDeputy;
			parameters[19].Value = model.CorpLinkMan;
			parameters[20].Value = model.CorpMobileTel;
			parameters[21].Value = model.CorpWorkedTel;
			parameters[22].Value = model.CorpFax;
			parameters[23].Value = model.CorpEmail;
			parameters[24].Value = model.CorpWeb;
			parameters[25].Value = model.AdminUserName;
			parameters[26].Value = model.AdminSex;
			parameters[27].Value = model.AdminUserTel;
			parameters[28].Value = model.AdminUserEmail;
			parameters[29].Value = model.DBServer;
			parameters[30].Value = model.DBName;
			parameters[31].Value = model.DBUser;
			parameters[32].Value = model.DBPwd;
			parameters[33].Value = model.RegDate;
			parameters[34].Value = model.BranchNum;
			parameters[35].Value = model.CommNum;
			parameters[36].Value = model.IsDelete;
			parameters[37].Value = model.ImgLogo1;
			parameters[38].Value = model.ImgLogo2;
			parameters[39].Value = model.SysDir;
			parameters[40].Value = model.SysVersion;
			parameters[41].Value = model.QQ;
			parameters[42].Value = model.StoreImage;
			parameters[43].Value = model.VSPID;
			parameters[44].Value = model.CorpSynchCode;
			parameters[45].Value = model.SynchFlag;
			parameters[46].Value = model.LogoImgUrl;
			parameters[47].Value = model.CorpSort;
			parameters[48].Value = model.IsRecommend;
			parameters[49].Value = model.RecommendIndex;
			parameters[50].Value = model.ActualCommSnum;
			parameters[51].Value = model.ContractMemo;
			parameters[52].Value = model.MobileSnum;
			parameters[53].Value = model.ActualMobileSnum;
			parameters[54].Value = model.CallVersion;
			parameters[55].Value = model.IsValidCode;
			parameters[56].Value = model.IsFees;
			parameters[57].Value = model.ContractBeginDate;
			parameters[58].Value = model.ContractEndDate;
			parameters[59].Value = model.UseBeginDate;
			parameters[60].Value = model.UseEndDate;
			parameters[61].Value = model.AllowanceDate;
			parameters[62].Value = model.YiZhiFuNum;
			parameters[63].Value = model.YiZhiFuKey;
			parameters[64].Value = model.YiJiFuHuoBanId;
			parameters[65].Value = model.YiJiFuNum;
			parameters[66].Value = model.YiJiFuKey;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Corp_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Corp model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CorpSID", SqlDbType.BigInt,8),
					new SqlParameter("@CorpID", SqlDbType.Int,4),
					new SqlParameter("@RegType", SqlDbType.SmallInt,2),
					new SqlParameter("@RegMode", SqlDbType.SmallInt,2),
					new SqlParameter("@CorpName", SqlDbType.NVarChar,100),
					new SqlParameter("@CorpShortName", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpTypeName", SqlDbType.NVarChar,20),
					new SqlParameter("@Province", SqlDbType.NVarChar,20),
					new SqlParameter("@City", SqlDbType.NVarChar,20),
					new SqlParameter("@Borough", SqlDbType.NVarChar,20),
					new SqlParameter("@Street", SqlDbType.NVarChar,200),
					new SqlParameter("@CommName", SqlDbType.NVarChar,50),
					new SqlParameter("@LoginCode", SqlDbType.NVarChar,50),
					new SqlParameter("@LoginPassWD", SqlDbType.NVarChar,30),
					new SqlParameter("@PwdQuestion", SqlDbType.NVarChar,50),
					new SqlParameter("@PwdAnswer", SqlDbType.NVarChar,30),
					new SqlParameter("@CorpAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@CorpPost", SqlDbType.NVarChar,10),
					new SqlParameter("@CorpDeputy", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpLinkMan", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpMobileTel", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpWorkedTel", SqlDbType.NVarChar,50),
					new SqlParameter("@CorpFax", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpEmail", SqlDbType.NVarChar,50),
					new SqlParameter("@CorpWeb", SqlDbType.NVarChar,50),
					new SqlParameter("@AdminUserName", SqlDbType.NVarChar,20),
					new SqlParameter("@AdminSex", SqlDbType.NVarChar,10),
					new SqlParameter("@AdminUserTel", SqlDbType.NVarChar,20),
					new SqlParameter("@AdminUserEmail", SqlDbType.NVarChar,50),
					new SqlParameter("@DBServer", SqlDbType.NVarChar,20),
					new SqlParameter("@DBName", SqlDbType.NVarChar,20),
					new SqlParameter("@DBUser", SqlDbType.NVarChar,20),
					new SqlParameter("@DBPwd", SqlDbType.NVarChar,20),
					new SqlParameter("@RegDate", SqlDbType.DateTime),
					new SqlParameter("@BranchNum", SqlDbType.Int,4),
					new SqlParameter("@CommNum", SqlDbType.Int,4),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@ImgLogo1", SqlDbType.NVarChar,100),
					new SqlParameter("@ImgLogo2", SqlDbType.NVarChar,100),
					new SqlParameter("@SysDir", SqlDbType.NVarChar,10),
					new SqlParameter("@SysVersion", SqlDbType.NVarChar,20),
					new SqlParameter("@QQ", SqlDbType.NVarChar,20),
					new SqlParameter("@StoreImage", SqlDbType.NVarChar,100),
					new SqlParameter("@VSPID", SqlDbType.Int,4),
					new SqlParameter("@CorpSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@LogoImgUrl", SqlDbType.NVarChar,50),
					new SqlParameter("@CorpSort", SqlDbType.Int,4),
					new SqlParameter("@IsRecommend", SqlDbType.SmallInt,2),
					new SqlParameter("@RecommendIndex", SqlDbType.NVarChar,5),
					new SqlParameter("@ActualCommSnum", SqlDbType.Int,4),
					new SqlParameter("@ContractMemo", SqlDbType.NVarChar,2000),
					new SqlParameter("@MobileSnum", SqlDbType.Int,4),
					new SqlParameter("@ActualMobileSnum", SqlDbType.Int,4),
					new SqlParameter("@CallVersion", SqlDbType.NVarChar,20),
					new SqlParameter("@IsValidCode", SqlDbType.SmallInt,2),
					new SqlParameter("@IsFees", SqlDbType.SmallInt,2),
					new SqlParameter("@ContractBeginDate", SqlDbType.DateTime),
					new SqlParameter("@ContractEndDate", SqlDbType.DateTime),
					new SqlParameter("@UseBeginDate", SqlDbType.DateTime),
					new SqlParameter("@UseEndDate", SqlDbType.DateTime),
					new SqlParameter("@AllowanceDate", SqlDbType.DateTime),
					new SqlParameter("@YiZhiFuNum", SqlDbType.VarChar,50),
					new SqlParameter("@YiZhiFuKey", SqlDbType.VarChar,50),
					new SqlParameter("@YiJiFuHuoBanId", SqlDbType.VarChar,50),
					new SqlParameter("@YiJiFuNum", SqlDbType.VarChar,50),
					new SqlParameter("@YiJiFuKey", SqlDbType.VarChar,50)};
			parameters[0].Value = model.CorpSID;
			parameters[1].Value = model.CorpID;
			parameters[2].Value = model.RegType;
			parameters[3].Value = model.RegMode;
			parameters[4].Value = model.CorpName;
			parameters[5].Value = model.CorpShortName;
			parameters[6].Value = model.CorpTypeName;
			parameters[7].Value = model.Province;
			parameters[8].Value = model.City;
			parameters[9].Value = model.Borough;
			parameters[10].Value = model.Street;
			parameters[11].Value = model.CommName;
			parameters[12].Value = model.LoginCode;
			parameters[13].Value = model.LoginPassWD;
			parameters[14].Value = model.PwdQuestion;
			parameters[15].Value = model.PwdAnswer;
			parameters[16].Value = model.CorpAddress;
			parameters[17].Value = model.CorpPost;
			parameters[18].Value = model.CorpDeputy;
			parameters[19].Value = model.CorpLinkMan;
			parameters[20].Value = model.CorpMobileTel;
			parameters[21].Value = model.CorpWorkedTel;
			parameters[22].Value = model.CorpFax;
			parameters[23].Value = model.CorpEmail;
			parameters[24].Value = model.CorpWeb;
			parameters[25].Value = model.AdminUserName;
			parameters[26].Value = model.AdminSex;
			parameters[27].Value = model.AdminUserTel;
			parameters[28].Value = model.AdminUserEmail;
			parameters[29].Value = model.DBServer;
			parameters[30].Value = model.DBName;
			parameters[31].Value = model.DBUser;
			parameters[32].Value = model.DBPwd;
			parameters[33].Value = model.RegDate;
			parameters[34].Value = model.BranchNum;
			parameters[35].Value = model.CommNum;
			parameters[36].Value = model.IsDelete;
			parameters[37].Value = model.ImgLogo1;
			parameters[38].Value = model.ImgLogo2;
			parameters[39].Value = model.SysDir;
			parameters[40].Value = model.SysVersion;
			parameters[41].Value = model.QQ;
			parameters[42].Value = model.StoreImage;
			parameters[43].Value = model.VSPID;
			parameters[44].Value = model.CorpSynchCode;
			parameters[45].Value = model.SynchFlag;
			parameters[46].Value = model.LogoImgUrl;
			parameters[47].Value = model.CorpSort;
			parameters[48].Value = model.IsRecommend;
			parameters[49].Value = model.RecommendIndex;
			parameters[50].Value = model.ActualCommSnum;
			parameters[51].Value = model.ContractMemo;
			parameters[52].Value = model.MobileSnum;
			parameters[53].Value = model.ActualMobileSnum;
			parameters[54].Value = model.CallVersion;
			parameters[55].Value = model.IsValidCode;
			parameters[56].Value = model.IsFees;
			parameters[57].Value = model.ContractBeginDate;
			parameters[58].Value = model.ContractEndDate;
			parameters[59].Value = model.UseBeginDate;
			parameters[60].Value = model.UseEndDate;
			parameters[61].Value = model.AllowanceDate;
			parameters[62].Value = model.YiZhiFuNum;
			parameters[63].Value = model.YiZhiFuKey;
			parameters[64].Value = model.YiJiFuHuoBanId;
			parameters[65].Value = model.YiJiFuNum;
			parameters[66].Value = model.YiJiFuKey;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Corp_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long CorpSID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CorpSID", SqlDbType.BigInt)};
			parameters[0].Value = CorpSID;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Corp_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Corp GetModel(long CorpSID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@CorpSID", SqlDbType.BigInt)};
			parameters[0].Value = CorpSID;

			MobileSoft.Model.System.Tb_System_Corp model=new MobileSoft.Model.System.Tb_System_Corp();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_System_Corp_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["CorpSID"].ToString()!="")
				{
					model.CorpSID=long.Parse(ds.Tables[0].Rows[0]["CorpSID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CorpID"].ToString()!="")
				{
					model.CorpID=int.Parse(ds.Tables[0].Rows[0]["CorpID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RegType"].ToString()!="")
				{
					model.RegType=int.Parse(ds.Tables[0].Rows[0]["RegType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RegMode"].ToString()!="")
				{
					model.RegMode=int.Parse(ds.Tables[0].Rows[0]["RegMode"].ToString());
				}
				model.CorpName=ds.Tables[0].Rows[0]["CorpName"].ToString();
				model.CorpShortName=ds.Tables[0].Rows[0]["CorpShortName"].ToString();
				model.CorpTypeName=ds.Tables[0].Rows[0]["CorpTypeName"].ToString();
				model.Province=ds.Tables[0].Rows[0]["Province"].ToString();
				model.City=ds.Tables[0].Rows[0]["City"].ToString();
				model.Borough=ds.Tables[0].Rows[0]["Borough"].ToString();
				model.Street=ds.Tables[0].Rows[0]["Street"].ToString();
				model.CommName=ds.Tables[0].Rows[0]["CommName"].ToString();
				model.LoginCode=ds.Tables[0].Rows[0]["LoginCode"].ToString();
				model.LoginPassWD=ds.Tables[0].Rows[0]["LoginPassWD"].ToString();
				model.PwdQuestion=ds.Tables[0].Rows[0]["PwdQuestion"].ToString();
				model.PwdAnswer=ds.Tables[0].Rows[0]["PwdAnswer"].ToString();
				model.CorpAddress=ds.Tables[0].Rows[0]["CorpAddress"].ToString();
				model.CorpPost=ds.Tables[0].Rows[0]["CorpPost"].ToString();
				model.CorpDeputy=ds.Tables[0].Rows[0]["CorpDeputy"].ToString();
				model.CorpLinkMan=ds.Tables[0].Rows[0]["CorpLinkMan"].ToString();
				model.CorpMobileTel=ds.Tables[0].Rows[0]["CorpMobileTel"].ToString();
				model.CorpWorkedTel=ds.Tables[0].Rows[0]["CorpWorkedTel"].ToString();
				model.CorpFax=ds.Tables[0].Rows[0]["CorpFax"].ToString();
				model.CorpEmail=ds.Tables[0].Rows[0]["CorpEmail"].ToString();
				model.CorpWeb=ds.Tables[0].Rows[0]["CorpWeb"].ToString();
				model.AdminUserName=ds.Tables[0].Rows[0]["AdminUserName"].ToString();
				model.AdminSex=ds.Tables[0].Rows[0]["AdminSex"].ToString();
				model.AdminUserTel=ds.Tables[0].Rows[0]["AdminUserTel"].ToString();
				model.AdminUserEmail=ds.Tables[0].Rows[0]["AdminUserEmail"].ToString();
				model.DBServer=ds.Tables[0].Rows[0]["DBServer"].ToString();
				model.DBName=ds.Tables[0].Rows[0]["DBName"].ToString();
				model.DBUser=ds.Tables[0].Rows[0]["DBUser"].ToString();
				model.DBPwd=ds.Tables[0].Rows[0]["DBPwd"].ToString();
				if(ds.Tables[0].Rows[0]["RegDate"].ToString()!="")
				{
					model.RegDate=DateTime.Parse(ds.Tables[0].Rows[0]["RegDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BranchNum"].ToString()!="")
				{
					model.BranchNum=int.Parse(ds.Tables[0].Rows[0]["BranchNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommNum"].ToString()!="")
				{
					model.CommNum=int.Parse(ds.Tables[0].Rows[0]["CommNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				model.ImgLogo1=ds.Tables[0].Rows[0]["ImgLogo1"].ToString();
				model.ImgLogo2=ds.Tables[0].Rows[0]["ImgLogo2"].ToString();
				model.SysDir=ds.Tables[0].Rows[0]["SysDir"].ToString();
				model.SysVersion=ds.Tables[0].Rows[0]["SysVersion"].ToString();
				model.QQ=ds.Tables[0].Rows[0]["QQ"].ToString();
				model.StoreImage=ds.Tables[0].Rows[0]["StoreImage"].ToString();
				if(ds.Tables[0].Rows[0]["VSPID"].ToString()!="")
				{
					model.VSPID=int.Parse(ds.Tables[0].Rows[0]["VSPID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CorpSynchCode"].ToString()!="")
				{
					model.CorpSynchCode=new Guid(ds.Tables[0].Rows[0]["CorpSynchCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SynchFlag"].ToString()!="")
				{
					model.SynchFlag=int.Parse(ds.Tables[0].Rows[0]["SynchFlag"].ToString());
				}
				model.LogoImgUrl=ds.Tables[0].Rows[0]["LogoImgUrl"].ToString();
				if(ds.Tables[0].Rows[0]["CorpSort"].ToString()!="")
				{
					model.CorpSort=int.Parse(ds.Tables[0].Rows[0]["CorpSort"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsRecommend"].ToString()!="")
				{
					model.IsRecommend=int.Parse(ds.Tables[0].Rows[0]["IsRecommend"].ToString());
				}
				model.RecommendIndex=ds.Tables[0].Rows[0]["RecommendIndex"].ToString();
				if(ds.Tables[0].Rows[0]["ActualCommSnum"].ToString()!="")
				{
					model.ActualCommSnum=int.Parse(ds.Tables[0].Rows[0]["ActualCommSnum"].ToString());
				}
				model.ContractMemo=ds.Tables[0].Rows[0]["ContractMemo"].ToString();
				if(ds.Tables[0].Rows[0]["MobileSnum"].ToString()!="")
				{
					model.MobileSnum=int.Parse(ds.Tables[0].Rows[0]["MobileSnum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ActualMobileSnum"].ToString()!="")
				{
					model.ActualMobileSnum=int.Parse(ds.Tables[0].Rows[0]["ActualMobileSnum"].ToString());
				}
				model.CallVersion=ds.Tables[0].Rows[0]["CallVersion"].ToString();
				if(ds.Tables[0].Rows[0]["IsValidCode"].ToString()!="")
				{
					model.IsValidCode=int.Parse(ds.Tables[0].Rows[0]["IsValidCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsFees"].ToString()!="")
				{
					model.IsFees=int.Parse(ds.Tables[0].Rows[0]["IsFees"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ContractBeginDate"].ToString()!="")
				{
					model.ContractBeginDate=DateTime.Parse(ds.Tables[0].Rows[0]["ContractBeginDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ContractEndDate"].ToString()!="")
				{
					model.ContractEndDate=DateTime.Parse(ds.Tables[0].Rows[0]["ContractEndDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UseBeginDate"].ToString()!="")
				{
					model.UseBeginDate=DateTime.Parse(ds.Tables[0].Rows[0]["UseBeginDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UseEndDate"].ToString()!="")
				{
					model.UseEndDate=DateTime.Parse(ds.Tables[0].Rows[0]["UseEndDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AllowanceDate"].ToString()!="")
				{
					model.AllowanceDate=DateTime.Parse(ds.Tables[0].Rows[0]["AllowanceDate"].ToString());
				}
				model.YiZhiFuNum=ds.Tables[0].Rows[0]["YiZhiFuNum"].ToString();
				model.YiZhiFuKey=ds.Tables[0].Rows[0]["YiZhiFuKey"].ToString();
				model.YiJiFuHuoBanId=ds.Tables[0].Rows[0]["YiJiFuHuoBanId"].ToString();
				model.YiJiFuNum=ds.Tables[0].Rows[0]["YiJiFuNum"].ToString();
				model.YiJiFuKey=ds.Tables[0].Rows[0]["YiJiFuKey"].ToString();
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
			strSql.Append("select * ");
			strSql.Append(" FROM Tb_System_Corp ");
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
			strSql.Append(" * ");
			strSql.Append(" FROM Tb_System_Corp ");
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
			parameters[5].Value = "SELECT * FROM Tb_System_Corp WHERE 1=1 " + StrCondition;
			parameters[6].Value = "CorpSID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法

        public DataTable System_Corp_GetVersion(int CorpID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@CorpID", SqlDbType.NVarChar,50)
                                              };
            parameters[0].Value = CorpID;

            DataTable result = DbHelperSQL.RunProcedure("Proc_System_Corp_GetVersion", parameters, "RetDataSet").Tables[0];

            return result;
        }
	}
}

