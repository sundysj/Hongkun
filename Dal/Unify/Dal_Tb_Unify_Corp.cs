using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Unify
{
	/// <summary>
	/// 数据访问类Dal_Tb_Unify_Corp。
	/// </summary>
	public class Dal_Tb_Unify_Corp
	{
		public Dal_Tb_Unify_Corp()
		{
            DbHelperSQL.ConnectionString = PubConstant.SQIBSContionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid CorpSynchCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CorpSynchCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = CorpSynchCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Unify_Corp_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.Unify.Tb_Unify_Corp model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CorpSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@UnCorpID", SqlDbType.BigInt,8),
					new SqlParameter("@VSPID", SqlDbType.Int,4),
					new SqlParameter("@CorpID", SqlDbType.Int,4),
					new SqlParameter("@CorpName", SqlDbType.NVarChar,100),
					new SqlParameter("@Province", SqlDbType.NVarChar,20),
					new SqlParameter("@City", SqlDbType.NVarChar,20),
					new SqlParameter("@Borough", SqlDbType.NVarChar,20),
					new SqlParameter("@RegMode", SqlDbType.SmallInt,2),
					new SqlParameter("@CorpTypeName", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@CorpPost", SqlDbType.NVarChar,10),
					new SqlParameter("@CorpDeputy", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpLinkMan", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpMobileTel", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpWorkedTel", SqlDbType.NVarChar,50),
					new SqlParameter("@CorpFax", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpEmail", SqlDbType.NVarChar,50),
					new SqlParameter("@CorpWeb", SqlDbType.NVarChar,50),
					new SqlParameter("@RegDate", SqlDbType.DateTime),
					new SqlParameter("@SysVersion", SqlDbType.NVarChar,20),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@ServerIP", SqlDbType.NVarChar,20),
					new SqlParameter("@SysDir", SqlDbType.NVarChar,10),
					new SqlParameter("@CorpSort", SqlDbType.Int,4),
					new SqlParameter("@IsRecommend", SqlDbType.SmallInt,2),
					new SqlParameter("@RecommendIndex", SqlDbType.NVarChar,5),
					new SqlParameter("@LogoImgUrl", SqlDbType.NVarChar,50),
					new SqlParameter("@CorpShortName", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpSNum", SqlDbType.Int,4),
					new SqlParameter("@Street", SqlDbType.NVarChar,50),
					new SqlParameter("@CommunityName", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.CorpSynchCode;
			parameters[1].Value = model.UnCorpID;
			parameters[2].Value = model.VSPID;
			parameters[3].Value = model.CorpID;
			parameters[4].Value = model.CorpName;
			parameters[5].Value = model.Province;
			parameters[6].Value = model.City;
			parameters[7].Value = model.Borough;
			parameters[8].Value = model.RegMode;
			parameters[9].Value = model.CorpTypeName;
			parameters[10].Value = model.CorpAddress;
			parameters[11].Value = model.CorpPost;
			parameters[12].Value = model.CorpDeputy;
			parameters[13].Value = model.CorpLinkMan;
			parameters[14].Value = model.CorpMobileTel;
			parameters[15].Value = model.CorpWorkedTel;
			parameters[16].Value = model.CorpFax;
			parameters[17].Value = model.CorpEmail;
			parameters[18].Value = model.CorpWeb;
			parameters[19].Value = model.RegDate;
			parameters[20].Value = model.SysVersion;
			parameters[21].Value = model.IsDelete;
			parameters[22].Value = model.SynchFlag;
			parameters[23].Value = model.ServerIP;
			parameters[24].Value = model.SysDir;
			parameters[25].Value = model.CorpSort;
			parameters[26].Value = model.IsRecommend;
			parameters[27].Value = model.RecommendIndex;
			parameters[28].Value = model.LogoImgUrl;
			parameters[29].Value = model.CorpShortName;
			parameters[30].Value = model.CorpSNum;
			parameters[31].Value = model.Street;
			parameters[32].Value = model.CommunityName;

			DbHelperSQL.RunProcedure("Proc_Tb_Unify_Corp_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Unify.Tb_Unify_Corp model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CorpSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@UnCorpID", SqlDbType.BigInt,8),
					new SqlParameter("@VSPID", SqlDbType.Int,4),
					new SqlParameter("@CorpID", SqlDbType.Int,4),
					new SqlParameter("@CorpName", SqlDbType.NVarChar,100),
					new SqlParameter("@Province", SqlDbType.NVarChar,20),
					new SqlParameter("@City", SqlDbType.NVarChar,20),
					new SqlParameter("@Borough", SqlDbType.NVarChar,20),
					new SqlParameter("@RegMode", SqlDbType.SmallInt,2),
					new SqlParameter("@CorpTypeName", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@CorpPost", SqlDbType.NVarChar,10),
					new SqlParameter("@CorpDeputy", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpLinkMan", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpMobileTel", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpWorkedTel", SqlDbType.NVarChar,50),
					new SqlParameter("@CorpFax", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpEmail", SqlDbType.NVarChar,50),
					new SqlParameter("@CorpWeb", SqlDbType.NVarChar,50),
					new SqlParameter("@RegDate", SqlDbType.DateTime),
					new SqlParameter("@SysVersion", SqlDbType.NVarChar,20),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@ServerIP", SqlDbType.NVarChar,20),
					new SqlParameter("@SysDir", SqlDbType.NVarChar,10),
					new SqlParameter("@CorpSort", SqlDbType.Int,4),
					new SqlParameter("@IsRecommend", SqlDbType.SmallInt,2),
					new SqlParameter("@RecommendIndex", SqlDbType.NVarChar,5),
					new SqlParameter("@LogoImgUrl", SqlDbType.NVarChar,50),
					new SqlParameter("@CorpShortName", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpSNum", SqlDbType.Int,4),
					new SqlParameter("@Street", SqlDbType.NVarChar,50),
					new SqlParameter("@CommunityName", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.CorpSynchCode;
			parameters[1].Value = model.UnCorpID;
			parameters[2].Value = model.VSPID;
			parameters[3].Value = model.CorpID;
			parameters[4].Value = model.CorpName;
			parameters[5].Value = model.Province;
			parameters[6].Value = model.City;
			parameters[7].Value = model.Borough;
			parameters[8].Value = model.RegMode;
			parameters[9].Value = model.CorpTypeName;
			parameters[10].Value = model.CorpAddress;
			parameters[11].Value = model.CorpPost;
			parameters[12].Value = model.CorpDeputy;
			parameters[13].Value = model.CorpLinkMan;
			parameters[14].Value = model.CorpMobileTel;
			parameters[15].Value = model.CorpWorkedTel;
			parameters[16].Value = model.CorpFax;
			parameters[17].Value = model.CorpEmail;
			parameters[18].Value = model.CorpWeb;
			parameters[19].Value = model.RegDate;
			parameters[20].Value = model.SysVersion;
			parameters[21].Value = model.IsDelete;
			parameters[22].Value = model.SynchFlag;
			parameters[23].Value = model.ServerIP;
			parameters[24].Value = model.SysDir;
			parameters[25].Value = model.CorpSort;
			parameters[26].Value = model.IsRecommend;
			parameters[27].Value = model.RecommendIndex;
			parameters[28].Value = model.LogoImgUrl;
			parameters[29].Value = model.CorpShortName;
			parameters[30].Value = model.CorpSNum;
			parameters[31].Value = model.Street;
			parameters[32].Value = model.CommunityName;

			DbHelperSQL.RunProcedure("Proc_Tb_Unify_Corp_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(Guid CorpSynchCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CorpSynchCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = CorpSynchCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Unify_Corp_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Unify.Tb_Unify_Corp GetModel(Guid CorpSynchCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@CorpSynchCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = CorpSynchCode;

			MobileSoft.Model.Unify.Tb_Unify_Corp model=new MobileSoft.Model.Unify.Tb_Unify_Corp();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Unify_Corp_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["CorpSynchCode"].ToString()!="")
				{
					model.CorpSynchCode=new Guid(ds.Tables[0].Rows[0]["CorpSynchCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UnCorpID"].ToString()!="")
				{
					model.UnCorpID=long.Parse(ds.Tables[0].Rows[0]["UnCorpID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["VSPID"].ToString()!="")
				{
					model.VSPID=int.Parse(ds.Tables[0].Rows[0]["VSPID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CorpID"].ToString()!="")
				{
					model.CorpID=int.Parse(ds.Tables[0].Rows[0]["CorpID"].ToString());
				}
				model.CorpName=ds.Tables[0].Rows[0]["CorpName"].ToString();
				model.Province=ds.Tables[0].Rows[0]["Province"].ToString();
				model.City=ds.Tables[0].Rows[0]["City"].ToString();
				model.Borough=ds.Tables[0].Rows[0]["Borough"].ToString();
				if(ds.Tables[0].Rows[0]["RegMode"].ToString()!="")
				{
					model.RegMode=int.Parse(ds.Tables[0].Rows[0]["RegMode"].ToString());
				}
				model.CorpTypeName=ds.Tables[0].Rows[0]["CorpTypeName"].ToString();
				model.CorpAddress=ds.Tables[0].Rows[0]["CorpAddress"].ToString();
				model.CorpPost=ds.Tables[0].Rows[0]["CorpPost"].ToString();
				model.CorpDeputy=ds.Tables[0].Rows[0]["CorpDeputy"].ToString();
				model.CorpLinkMan=ds.Tables[0].Rows[0]["CorpLinkMan"].ToString();
				model.CorpMobileTel=ds.Tables[0].Rows[0]["CorpMobileTel"].ToString();
				model.CorpWorkedTel=ds.Tables[0].Rows[0]["CorpWorkedTel"].ToString();
				model.CorpFax=ds.Tables[0].Rows[0]["CorpFax"].ToString();
				model.CorpEmail=ds.Tables[0].Rows[0]["CorpEmail"].ToString();
				model.CorpWeb=ds.Tables[0].Rows[0]["CorpWeb"].ToString();
				if(ds.Tables[0].Rows[0]["RegDate"].ToString()!="")
				{
					model.RegDate=DateTime.Parse(ds.Tables[0].Rows[0]["RegDate"].ToString());
				}
				model.SysVersion=ds.Tables[0].Rows[0]["SysVersion"].ToString();
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SynchFlag"].ToString()!="")
				{
					model.SynchFlag=int.Parse(ds.Tables[0].Rows[0]["SynchFlag"].ToString());
				}
				model.ServerIP=ds.Tables[0].Rows[0]["ServerIP"].ToString();
				model.SysDir=ds.Tables[0].Rows[0]["SysDir"].ToString();
				if(ds.Tables[0].Rows[0]["CorpSort"].ToString()!="")
				{
					model.CorpSort=int.Parse(ds.Tables[0].Rows[0]["CorpSort"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsRecommend"].ToString()!="")
				{
					model.IsRecommend=int.Parse(ds.Tables[0].Rows[0]["IsRecommend"].ToString());
				}
				model.RecommendIndex=ds.Tables[0].Rows[0]["RecommendIndex"].ToString();
				model.LogoImgUrl=ds.Tables[0].Rows[0]["LogoImgUrl"].ToString();
				model.CorpShortName=ds.Tables[0].Rows[0]["CorpShortName"].ToString();
				if(ds.Tables[0].Rows[0]["CorpSNum"].ToString()!="")
				{
					model.CorpSNum=int.Parse(ds.Tables[0].Rows[0]["CorpSNum"].ToString());
				}
				model.Street=ds.Tables[0].Rows[0]["Street"].ToString();
				model.CommunityName=ds.Tables[0].Rows[0]["CommunityName"].ToString();
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
			strSql.Append("select CorpSynchCode,UnCorpID,VSPID,CorpID,CorpName,Province,City,Borough,RegMode,CorpTypeName,CorpAddress,CorpPost,CorpDeputy,CorpLinkMan,CorpMobileTel,CorpWorkedTel,CorpFax,CorpEmail,CorpWeb,RegDate,SysVersion,IsDelete,SynchFlag,ServerIP,SysDir,CorpSort,IsRecommend,RecommendIndex,LogoImgUrl,CorpShortName,CorpSNum,Street,CommunityName ");
			strSql.Append(" FROM Tb_Unify_Corp ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" CorpSynchCode,UnCorpID,VSPID,CorpID,CorpName,Province,City,Borough,RegMode,CorpTypeName,CorpAddress,CorpPost,CorpDeputy,CorpLinkMan,CorpMobileTel,CorpWorkedTel,CorpFax,CorpEmail,CorpWeb,RegDate,SysVersion,IsDelete,SynchFlag,ServerIP,SysDir,CorpSort,IsRecommend,RecommendIndex,LogoImgUrl,CorpShortName,CorpSNum,Street,CommunityName ");
			strSql.Append(" FROM Tb_Unify_Corp ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
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
			parameters[5].Value = "SELECT * FROM Tb_Unify_Corp WHERE 1=1 " + StrCondition;
			parameters[6].Value = "CorpSynchCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

