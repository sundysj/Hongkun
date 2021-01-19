using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.System
{
	/// <summary>
	/// 数据访问类Dal_Tb_System_Register。
	/// </summary>
	public class Dal_Tb_System_Register
	{
		public Dal_Tb_System_Register()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("RegType", "Tb_System_Register"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long RegID,int RegType)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RegID", SqlDbType.BigInt),
					new SqlParameter("@RegType", SqlDbType.SmallInt)};
			parameters[0].Value = RegID;
			parameters[1].Value = RegType;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_System_Register_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.System.Tb_System_Register model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RegID", SqlDbType.BigInt,8),
					new SqlParameter("@RegType", SqlDbType.SmallInt,2),
					new SqlParameter("@RegMode", SqlDbType.SmallInt,2),
					new SqlParameter("@RegUserName", SqlDbType.NVarChar,20),
					new SqlParameter("@RegPassWord", SqlDbType.NVarChar,20),
					new SqlParameter("@RegQuestion", SqlDbType.NVarChar,30),
					new SqlParameter("@RegAnswer", SqlDbType.NVarChar,30),
					new SqlParameter("@RegCorpName", SqlDbType.NVarChar,50),
					new SqlParameter("@CorpAddress", SqlDbType.NVarChar,100),
					new SqlParameter("@ZipCode", SqlDbType.NVarChar,10),
					new SqlParameter("@LinkMan", SqlDbType.NVarChar,20),
					new SqlParameter("@Tel", SqlDbType.NVarChar,20),
					new SqlParameter("@Fax", SqlDbType.NVarChar,20),
					new SqlParameter("@Email", SqlDbType.NVarChar,50),
					new SqlParameter("@WebName", SqlDbType.NVarChar,100),
					new SqlParameter("@RegDate", SqlDbType.DateTime),
					new SqlParameter("@IsAuditing", SqlDbType.SmallInt,2),
					new SqlParameter("@IsSucc", SqlDbType.SmallInt,2),
					new SqlParameter("@ReturnMsg", SqlDbType.NVarChar,20),
					new SqlParameter("@Province", SqlDbType.NVarChar,20),
					new SqlParameter("@City", SqlDbType.NVarChar,20),
					new SqlParameter("@Borough", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpTypeName", SqlDbType.NVarChar,20),
					new SqlParameter("@Street", SqlDbType.NVarChar,200),
					new SqlParameter("@CommName", SqlDbType.NVarChar,50),
					new SqlParameter("@VSPID", SqlDbType.Int,4)};
			parameters[0].Value = model.RegID;
			parameters[1].Value = model.RegType;
			parameters[2].Value = model.RegMode;
			parameters[3].Value = model.RegUserName;
			parameters[4].Value = model.RegPassWord;
			parameters[5].Value = model.RegQuestion;
			parameters[6].Value = model.RegAnswer;
			parameters[7].Value = model.RegCorpName;
			parameters[8].Value = model.CorpAddress;
			parameters[9].Value = model.ZipCode;
			parameters[10].Value = model.LinkMan;
			parameters[11].Value = model.Tel;
			parameters[12].Value = model.Fax;
			parameters[13].Value = model.Email;
			parameters[14].Value = model.WebName;
			parameters[15].Value = model.RegDate;
			parameters[16].Value = model.IsAuditing;
			parameters[17].Value = model.IsSucc;
			parameters[18].Value = model.ReturnMsg;
			parameters[19].Value = model.Province;
			parameters[20].Value = model.City;
			parameters[21].Value = model.Borough;
			parameters[22].Value = model.CorpTypeName;
			parameters[23].Value = model.Street;
			parameters[24].Value = model.CommName;
			parameters[25].Value = model.VSPID;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Register_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Register model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RegID", SqlDbType.BigInt,8),
					new SqlParameter("@RegType", SqlDbType.SmallInt,2),
					new SqlParameter("@RegMode", SqlDbType.SmallInt,2),
					new SqlParameter("@RegUserName", SqlDbType.NVarChar,20),
					new SqlParameter("@RegPassWord", SqlDbType.NVarChar,20),
					new SqlParameter("@RegQuestion", SqlDbType.NVarChar,30),
					new SqlParameter("@RegAnswer", SqlDbType.NVarChar,30),
					new SqlParameter("@RegCorpName", SqlDbType.NVarChar,50),
					new SqlParameter("@CorpAddress", SqlDbType.NVarChar,100),
					new SqlParameter("@ZipCode", SqlDbType.NVarChar,10),
					new SqlParameter("@LinkMan", SqlDbType.NVarChar,20),
					new SqlParameter("@Tel", SqlDbType.NVarChar,20),
					new SqlParameter("@Fax", SqlDbType.NVarChar,20),
					new SqlParameter("@Email", SqlDbType.NVarChar,50),
					new SqlParameter("@WebName", SqlDbType.NVarChar,100),
					new SqlParameter("@RegDate", SqlDbType.DateTime),
					new SqlParameter("@IsAuditing", SqlDbType.SmallInt,2),
					new SqlParameter("@IsSucc", SqlDbType.SmallInt,2),
					new SqlParameter("@ReturnMsg", SqlDbType.NVarChar,20),
					new SqlParameter("@Province", SqlDbType.NVarChar,20),
					new SqlParameter("@City", SqlDbType.NVarChar,20),
					new SqlParameter("@Borough", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpTypeName", SqlDbType.NVarChar,20),
					new SqlParameter("@Street", SqlDbType.NVarChar,200),
					new SqlParameter("@CommName", SqlDbType.NVarChar,50),
					new SqlParameter("@VSPID", SqlDbType.Int,4)};
			parameters[0].Value = model.RegID;
			parameters[1].Value = model.RegType;
			parameters[2].Value = model.RegMode;
			parameters[3].Value = model.RegUserName;
			parameters[4].Value = model.RegPassWord;
			parameters[5].Value = model.RegQuestion;
			parameters[6].Value = model.RegAnswer;
			parameters[7].Value = model.RegCorpName;
			parameters[8].Value = model.CorpAddress;
			parameters[9].Value = model.ZipCode;
			parameters[10].Value = model.LinkMan;
			parameters[11].Value = model.Tel;
			parameters[12].Value = model.Fax;
			parameters[13].Value = model.Email;
			parameters[14].Value = model.WebName;
			parameters[15].Value = model.RegDate;
			parameters[16].Value = model.IsAuditing;
			parameters[17].Value = model.IsSucc;
			parameters[18].Value = model.ReturnMsg;
			parameters[19].Value = model.Province;
			parameters[20].Value = model.City;
			parameters[21].Value = model.Borough;
			parameters[22].Value = model.CorpTypeName;
			parameters[23].Value = model.Street;
			parameters[24].Value = model.CommName;
			parameters[25].Value = model.VSPID;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Register_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long RegID,int RegType)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RegID", SqlDbType.BigInt),
					new SqlParameter("@RegType", SqlDbType.SmallInt)};
			parameters[0].Value = RegID;
			parameters[1].Value = RegType;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Register_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Register GetModel(long RegID,int RegType)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@RegID", SqlDbType.BigInt),
					new SqlParameter("@RegType", SqlDbType.SmallInt)};
			parameters[0].Value = RegID;
			parameters[1].Value = RegType;

			MobileSoft.Model.System.Tb_System_Register model=new MobileSoft.Model.System.Tb_System_Register();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_System_Register_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["RegID"].ToString()!="")
				{
					model.RegID=long.Parse(ds.Tables[0].Rows[0]["RegID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RegType"].ToString()!="")
				{
					model.RegType=int.Parse(ds.Tables[0].Rows[0]["RegType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RegMode"].ToString()!="")
				{
					model.RegMode=int.Parse(ds.Tables[0].Rows[0]["RegMode"].ToString());
				}
				model.RegUserName=ds.Tables[0].Rows[0]["RegUserName"].ToString();
				model.RegPassWord=ds.Tables[0].Rows[0]["RegPassWord"].ToString();
				model.RegQuestion=ds.Tables[0].Rows[0]["RegQuestion"].ToString();
				model.RegAnswer=ds.Tables[0].Rows[0]["RegAnswer"].ToString();
				model.RegCorpName=ds.Tables[0].Rows[0]["RegCorpName"].ToString();
				model.CorpAddress=ds.Tables[0].Rows[0]["CorpAddress"].ToString();
				model.ZipCode=ds.Tables[0].Rows[0]["ZipCode"].ToString();
				model.LinkMan=ds.Tables[0].Rows[0]["LinkMan"].ToString();
				model.Tel=ds.Tables[0].Rows[0]["Tel"].ToString();
				model.Fax=ds.Tables[0].Rows[0]["Fax"].ToString();
				model.Email=ds.Tables[0].Rows[0]["Email"].ToString();
				model.WebName=ds.Tables[0].Rows[0]["WebName"].ToString();
				if(ds.Tables[0].Rows[0]["RegDate"].ToString()!="")
				{
					model.RegDate=DateTime.Parse(ds.Tables[0].Rows[0]["RegDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsAuditing"].ToString()!="")
				{
					model.IsAuditing=int.Parse(ds.Tables[0].Rows[0]["IsAuditing"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsSucc"].ToString()!="")
				{
					model.IsSucc=int.Parse(ds.Tables[0].Rows[0]["IsSucc"].ToString());
				}
				model.ReturnMsg=ds.Tables[0].Rows[0]["ReturnMsg"].ToString();
				model.Province=ds.Tables[0].Rows[0]["Province"].ToString();
				model.City=ds.Tables[0].Rows[0]["City"].ToString();
				model.Borough=ds.Tables[0].Rows[0]["Borough"].ToString();
				model.CorpTypeName=ds.Tables[0].Rows[0]["CorpTypeName"].ToString();
				model.Street=ds.Tables[0].Rows[0]["Street"].ToString();
				model.CommName=ds.Tables[0].Rows[0]["CommName"].ToString();
				if(ds.Tables[0].Rows[0]["VSPID"].ToString()!="")
				{
					model.VSPID=int.Parse(ds.Tables[0].Rows[0]["VSPID"].ToString());
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
			strSql.Append("select RegID,RegType,RegMode,RegUserName,RegPassWord,RegQuestion,RegAnswer,RegCorpName,CorpAddress,ZipCode,LinkMan,Tel,Fax,Email,WebName,RegDate,IsAuditing,IsSucc,ReturnMsg,Province,City,Borough,CorpTypeName,Street,CommName,VSPID ");
			strSql.Append(" FROM Tb_System_Register ");
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
			strSql.Append(" RegID,RegType,RegMode,RegUserName,RegPassWord,RegQuestion,RegAnswer,RegCorpName,CorpAddress,ZipCode,LinkMan,Tel,Fax,Email,WebName,RegDate,IsAuditing,IsSucc,ReturnMsg,Province,City,Borough,CorpTypeName,Street,CommName,VSPID ");
			strSql.Append(" FROM Tb_System_Register ");
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
			parameters[5].Value = "SELECT * FROM Tb_System_Register WHERE 1=1 " + StrCondition;
			parameters[6].Value = "RegID,RegType";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

