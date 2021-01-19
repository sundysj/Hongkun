using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.System
{
	/// <summary>
	/// 数据访问类Dal_Tb_System_Community。
	/// </summary>
	public class Dal_Tb_System_Community
	{
		public Dal_Tb_System_Community()
		{
                  DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid CommCode,long CommID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CommCode", SqlDbType.UniqueIdentifier),
					new SqlParameter("@CommID", SqlDbType.BigInt)};
			parameters[0].Value = CommCode;
			parameters[1].Value = CommID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_System_Community_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.System.Tb_System_Community model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CommCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CommID", SqlDbType.BigInt,8),
					new SqlParameter("@RegType", SqlDbType.SmallInt,2),
					new SqlParameter("@RegMode", SqlDbType.SmallInt,2),
					new SqlParameter("@CommName", SqlDbType.NVarChar,50),
					new SqlParameter("@CommShortName", SqlDbType.NVarChar,20),
					new SqlParameter("@ProvinceID", SqlDbType.Int,4),
					new SqlParameter("@CityID", SqlDbType.Int,4),
					new SqlParameter("@BoroughID", SqlDbType.Int,4),
					new SqlParameter("@StreetID", SqlDbType.Int,4),
					new SqlParameter("@GateSign", SqlDbType.NVarChar,20),
					new SqlParameter("@CommSpell", SqlDbType.NVarChar,20),
					new SqlParameter("@LoginCode", SqlDbType.NVarChar,50),
					new SqlParameter("@LoginPassWD", SqlDbType.NVarChar,30),
					new SqlParameter("@PwdQuestion", SqlDbType.NVarChar,50),
					new SqlParameter("@PwdAnswer", SqlDbType.NVarChar,30),
					new SqlParameter("@CommAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@CommPost", SqlDbType.NVarChar,10),
					new SqlParameter("@CommDeputy", SqlDbType.NVarChar,20),
					new SqlParameter("@CommLinkMan", SqlDbType.NVarChar,20),
					new SqlParameter("@CommMobileTel", SqlDbType.NVarChar,20),
					new SqlParameter("@CommWorkedTel", SqlDbType.NVarChar,50),
					new SqlParameter("@CommFax", SqlDbType.NVarChar,20),
					new SqlParameter("@CommEmail", SqlDbType.NVarChar,50),
					new SqlParameter("@CommWeb", SqlDbType.NVarChar,50),
					new SqlParameter("@SysVersion", SqlDbType.NVarChar,20),
					new SqlParameter("@RegDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@VSPID", SqlDbType.Int,4),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.CommCode;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.RegType;
			parameters[3].Value = model.RegMode;
			parameters[4].Value = model.CommName;
			parameters[5].Value = model.CommShortName;
			parameters[6].Value = model.ProvinceID;
			parameters[7].Value = model.CityID;
			parameters[8].Value = model.BoroughID;
			parameters[9].Value = model.StreetID;
			parameters[10].Value = model.GateSign;
			parameters[11].Value = model.CommSpell;
			parameters[12].Value = model.LoginCode;
			parameters[13].Value = model.LoginPassWD;
			parameters[14].Value = model.PwdQuestion;
			parameters[15].Value = model.PwdAnswer;
			parameters[16].Value = model.CommAddress;
			parameters[17].Value = model.CommPost;
			parameters[18].Value = model.CommDeputy;
			parameters[19].Value = model.CommLinkMan;
			parameters[20].Value = model.CommMobileTel;
			parameters[21].Value = model.CommWorkedTel;
			parameters[22].Value = model.CommFax;
			parameters[23].Value = model.CommEmail;
			parameters[24].Value = model.CommWeb;
			parameters[25].Value = model.SysVersion;
			parameters[26].Value = model.RegDate;
			parameters[27].Value = model.IsDelete;
			parameters[28].Value = model.VSPID;
			parameters[29].Value = model.SynchFlag;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Community_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Community model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CommCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CommID", SqlDbType.BigInt,8),
					new SqlParameter("@RegType", SqlDbType.SmallInt,2),
					new SqlParameter("@RegMode", SqlDbType.SmallInt,2),
					new SqlParameter("@CommName", SqlDbType.NVarChar,50),
					new SqlParameter("@CommShortName", SqlDbType.NVarChar,20),
					new SqlParameter("@ProvinceID", SqlDbType.Int,4),
					new SqlParameter("@CityID", SqlDbType.Int,4),
					new SqlParameter("@BoroughID", SqlDbType.Int,4),
					new SqlParameter("@StreetID", SqlDbType.Int,4),
					new SqlParameter("@GateSign", SqlDbType.NVarChar,20),
					new SqlParameter("@CommSpell", SqlDbType.NVarChar,20),
					new SqlParameter("@LoginCode", SqlDbType.NVarChar,50),
					new SqlParameter("@LoginPassWD", SqlDbType.NVarChar,30),
					new SqlParameter("@PwdQuestion", SqlDbType.NVarChar,50),
					new SqlParameter("@PwdAnswer", SqlDbType.NVarChar,30),
					new SqlParameter("@CommAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@CommPost", SqlDbType.NVarChar,10),
					new SqlParameter("@CommDeputy", SqlDbType.NVarChar,20),
					new SqlParameter("@CommLinkMan", SqlDbType.NVarChar,20),
					new SqlParameter("@CommMobileTel", SqlDbType.NVarChar,20),
					new SqlParameter("@CommWorkedTel", SqlDbType.NVarChar,50),
					new SqlParameter("@CommFax", SqlDbType.NVarChar,20),
					new SqlParameter("@CommEmail", SqlDbType.NVarChar,50),
					new SqlParameter("@CommWeb", SqlDbType.NVarChar,50),
					new SqlParameter("@SysVersion", SqlDbType.NVarChar,20),
					new SqlParameter("@RegDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@VSPID", SqlDbType.Int,4),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.CommCode;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.RegType;
			parameters[3].Value = model.RegMode;
			parameters[4].Value = model.CommName;
			parameters[5].Value = model.CommShortName;
			parameters[6].Value = model.ProvinceID;
			parameters[7].Value = model.CityID;
			parameters[8].Value = model.BoroughID;
			parameters[9].Value = model.StreetID;
			parameters[10].Value = model.GateSign;
			parameters[11].Value = model.CommSpell;
			parameters[12].Value = model.LoginCode;
			parameters[13].Value = model.LoginPassWD;
			parameters[14].Value = model.PwdQuestion;
			parameters[15].Value = model.PwdAnswer;
			parameters[16].Value = model.CommAddress;
			parameters[17].Value = model.CommPost;
			parameters[18].Value = model.CommDeputy;
			parameters[19].Value = model.CommLinkMan;
			parameters[20].Value = model.CommMobileTel;
			parameters[21].Value = model.CommWorkedTel;
			parameters[22].Value = model.CommFax;
			parameters[23].Value = model.CommEmail;
			parameters[24].Value = model.CommWeb;
			parameters[25].Value = model.SysVersion;
			parameters[26].Value = model.RegDate;
			parameters[27].Value = model.IsDelete;
			parameters[28].Value = model.VSPID;
			parameters[29].Value = model.SynchFlag;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Community_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(Guid CommCode,long CommID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CommCode", SqlDbType.UniqueIdentifier),
					new SqlParameter("@CommID", SqlDbType.BigInt)};
			parameters[0].Value = CommCode;
			parameters[1].Value = CommID;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Community_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Community GetModel(Guid CommCode,long CommID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@CommCode", SqlDbType.UniqueIdentifier),
					new SqlParameter("@CommID", SqlDbType.BigInt)};
			parameters[0].Value = CommCode;
			parameters[1].Value = CommID;

			MobileSoft.Model.System.Tb_System_Community model=new MobileSoft.Model.System.Tb_System_Community();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_System_Community_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["CommCode"].ToString()!="")
				{
					model.CommCode=new Guid(ds.Tables[0].Rows[0]["CommCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=long.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RegType"].ToString()!="")
				{
					model.RegType=int.Parse(ds.Tables[0].Rows[0]["RegType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RegMode"].ToString()!="")
				{
					model.RegMode=int.Parse(ds.Tables[0].Rows[0]["RegMode"].ToString());
				}
				model.CommName=ds.Tables[0].Rows[0]["CommName"].ToString();
				model.CommShortName=ds.Tables[0].Rows[0]["CommShortName"].ToString();
				if(ds.Tables[0].Rows[0]["ProvinceID"].ToString()!="")
				{
					model.ProvinceID=int.Parse(ds.Tables[0].Rows[0]["ProvinceID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CityID"].ToString()!="")
				{
					model.CityID=int.Parse(ds.Tables[0].Rows[0]["CityID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BoroughID"].ToString()!="")
				{
					model.BoroughID=int.Parse(ds.Tables[0].Rows[0]["BoroughID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["StreetID"].ToString()!="")
				{
					model.StreetID=int.Parse(ds.Tables[0].Rows[0]["StreetID"].ToString());
				}
				model.GateSign=ds.Tables[0].Rows[0]["GateSign"].ToString();
				model.CommSpell=ds.Tables[0].Rows[0]["CommSpell"].ToString();
				model.LoginCode=ds.Tables[0].Rows[0]["LoginCode"].ToString();
				model.LoginPassWD=ds.Tables[0].Rows[0]["LoginPassWD"].ToString();
				model.PwdQuestion=ds.Tables[0].Rows[0]["PwdQuestion"].ToString();
				model.PwdAnswer=ds.Tables[0].Rows[0]["PwdAnswer"].ToString();
				model.CommAddress=ds.Tables[0].Rows[0]["CommAddress"].ToString();
				model.CommPost=ds.Tables[0].Rows[0]["CommPost"].ToString();
				model.CommDeputy=ds.Tables[0].Rows[0]["CommDeputy"].ToString();
				model.CommLinkMan=ds.Tables[0].Rows[0]["CommLinkMan"].ToString();
				model.CommMobileTel=ds.Tables[0].Rows[0]["CommMobileTel"].ToString();
				model.CommWorkedTel=ds.Tables[0].Rows[0]["CommWorkedTel"].ToString();
				model.CommFax=ds.Tables[0].Rows[0]["CommFax"].ToString();
				model.CommEmail=ds.Tables[0].Rows[0]["CommEmail"].ToString();
				model.CommWeb=ds.Tables[0].Rows[0]["CommWeb"].ToString();
				model.SysVersion=ds.Tables[0].Rows[0]["SysVersion"].ToString();
				if(ds.Tables[0].Rows[0]["RegDate"].ToString()!="")
				{
					model.RegDate=DateTime.Parse(ds.Tables[0].Rows[0]["RegDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["VSPID"].ToString()!="")
				{
					model.VSPID=int.Parse(ds.Tables[0].Rows[0]["VSPID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SynchFlag"].ToString()!="")
				{
					model.SynchFlag=int.Parse(ds.Tables[0].Rows[0]["SynchFlag"].ToString());
				}
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
			strSql.Append(" FROM View_HSPR_Community_Filter ");
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
            strSql.Append(" FROM View_HSPR_Community_Filter ");
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
            parameters[5].Value = "SELECT * FROM View_HSPR_Community_Filter WHERE 1=1 " + StrCondition;
			parameters[6].Value = "CommCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

