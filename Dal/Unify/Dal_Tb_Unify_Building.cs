using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Unify
{
	/// <summary>
	/// 数据访问类Dal_Tb_Unify_Building。
	/// </summary>
	public class Dal_Tb_Unify_Building
	{
		public Dal_Tb_Unify_Building()
		{
			DbHelperSQL.ConnectionString = PubConstant.SQIBSContionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid BuildSynchCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@BuildSynchCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = BuildSynchCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Unify_Building_Exists",parameters,out rowsAffected);
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
		public void Add(ehome.Model.Unify.Tb_Unify_Building model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@BuildSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CommSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@UnBuildID", SqlDbType.BigInt,8),
					new SqlParameter("@BuildSNum", SqlDbType.Int,4),
					new SqlParameter("@RegionSNum", SqlDbType.Int,4),
					new SqlParameter("@BuildSign", SqlDbType.NVarChar,10),
					new SqlParameter("@BuildName", SqlDbType.NVarChar,20),
					new SqlParameter("@BuildType", SqlDbType.NVarChar,20),
					new SqlParameter("@BuildUses", SqlDbType.NVarChar,20),
					new SqlParameter("@PropertyRights", SqlDbType.NVarChar,20),
					new SqlParameter("@PropertyUses", SqlDbType.NVarChar,20),
					new SqlParameter("@BuildHeight", SqlDbType.NVarChar,20),
					new SqlParameter("@FloorsNum", SqlDbType.Int,4),
					new SqlParameter("@UnderFloorsNum", SqlDbType.Int,4),
					new SqlParameter("@UnitNum", SqlDbType.Int,4),
					new SqlParameter("@NamingPatterns", SqlDbType.NVarChar,30),
					new SqlParameter("@PerFloorNum", SqlDbType.Int,4),
					new SqlParameter("@HouseholdsNum", SqlDbType.Int,4),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@OrdSNum", SqlDbType.BigInt,8)};
			parameters[0].Value = model.BuildSynchCode;
			parameters[1].Value = model.CommSynchCode;
			parameters[2].Value = model.UnBuildID;
			parameters[3].Value = model.BuildSNum;
			parameters[4].Value = model.RegionSNum;
			parameters[5].Value = model.BuildSign;
			parameters[6].Value = model.BuildName;
			parameters[7].Value = model.BuildType;
			parameters[8].Value = model.BuildUses;
			parameters[9].Value = model.PropertyRights;
			parameters[10].Value = model.PropertyUses;
			parameters[11].Value = model.BuildHeight;
			parameters[12].Value = model.FloorsNum;
			parameters[13].Value = model.UnderFloorsNum;
			parameters[14].Value = model.UnitNum;
			parameters[15].Value = model.NamingPatterns;
			parameters[16].Value = model.PerFloorNum;
			parameters[17].Value = model.HouseholdsNum;
			parameters[18].Value = model.IsDelete;
			parameters[19].Value = model.SynchFlag;
			parameters[20].Value = model.OrdSNum;

			DbHelperSQL.RunProcedure("Proc_Tb_Unify_Building_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(ehome.Model.Unify.Tb_Unify_Building model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@BuildSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CommSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@UnBuildID", SqlDbType.BigInt,8),
					new SqlParameter("@BuildSNum", SqlDbType.Int,4),
					new SqlParameter("@RegionSNum", SqlDbType.Int,4),
					new SqlParameter("@BuildSign", SqlDbType.NVarChar,10),
					new SqlParameter("@BuildName", SqlDbType.NVarChar,20),
					new SqlParameter("@BuildType", SqlDbType.NVarChar,20),
					new SqlParameter("@BuildUses", SqlDbType.NVarChar,20),
					new SqlParameter("@PropertyRights", SqlDbType.NVarChar,20),
					new SqlParameter("@PropertyUses", SqlDbType.NVarChar,20),
					new SqlParameter("@BuildHeight", SqlDbType.NVarChar,20),
					new SqlParameter("@FloorsNum", SqlDbType.Int,4),
					new SqlParameter("@UnderFloorsNum", SqlDbType.Int,4),
					new SqlParameter("@UnitNum", SqlDbType.Int,4),
					new SqlParameter("@NamingPatterns", SqlDbType.NVarChar,30),
					new SqlParameter("@PerFloorNum", SqlDbType.Int,4),
					new SqlParameter("@HouseholdsNum", SqlDbType.Int,4),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@OrdSNum", SqlDbType.BigInt,8)};
			parameters[0].Value = model.BuildSynchCode;
			parameters[1].Value = model.CommSynchCode;
			parameters[2].Value = model.UnBuildID;
			parameters[3].Value = model.BuildSNum;
			parameters[4].Value = model.RegionSNum;
			parameters[5].Value = model.BuildSign;
			parameters[6].Value = model.BuildName;
			parameters[7].Value = model.BuildType;
			parameters[8].Value = model.BuildUses;
			parameters[9].Value = model.PropertyRights;
			parameters[10].Value = model.PropertyUses;
			parameters[11].Value = model.BuildHeight;
			parameters[12].Value = model.FloorsNum;
			parameters[13].Value = model.UnderFloorsNum;
			parameters[14].Value = model.UnitNum;
			parameters[15].Value = model.NamingPatterns;
			parameters[16].Value = model.PerFloorNum;
			parameters[17].Value = model.HouseholdsNum;
			parameters[18].Value = model.IsDelete;
			parameters[19].Value = model.SynchFlag;
			parameters[20].Value = model.OrdSNum;

			DbHelperSQL.RunProcedure("Proc_Tb_Unify_Building_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(Guid BuildSynchCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@BuildSynchCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = BuildSynchCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Unify_Building_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ehome.Model.Unify.Tb_Unify_Building GetModel(Guid BuildSynchCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@BuildSynchCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = BuildSynchCode;

			ehome.Model.Unify.Tb_Unify_Building model=new ehome.Model.Unify.Tb_Unify_Building();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Unify_Building_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["BuildSynchCode"].ToString()!="")
				{
					model.BuildSynchCode=new Guid(ds.Tables[0].Rows[0]["BuildSynchCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommSynchCode"].ToString()!="")
				{
					model.CommSynchCode=new Guid(ds.Tables[0].Rows[0]["CommSynchCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UnBuildID"].ToString()!="")
				{
					model.UnBuildID=long.Parse(ds.Tables[0].Rows[0]["UnBuildID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BuildSNum"].ToString()!="")
				{
					model.BuildSNum=int.Parse(ds.Tables[0].Rows[0]["BuildSNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RegionSNum"].ToString()!="")
				{
					model.RegionSNum=int.Parse(ds.Tables[0].Rows[0]["RegionSNum"].ToString());
				}
				model.BuildSign=ds.Tables[0].Rows[0]["BuildSign"].ToString();
				model.BuildName=ds.Tables[0].Rows[0]["BuildName"].ToString();
				model.BuildType=ds.Tables[0].Rows[0]["BuildType"].ToString();
				model.BuildUses=ds.Tables[0].Rows[0]["BuildUses"].ToString();
				model.PropertyRights=ds.Tables[0].Rows[0]["PropertyRights"].ToString();
				model.PropertyUses=ds.Tables[0].Rows[0]["PropertyUses"].ToString();
				model.BuildHeight=ds.Tables[0].Rows[0]["BuildHeight"].ToString();
				if(ds.Tables[0].Rows[0]["FloorsNum"].ToString()!="")
				{
					model.FloorsNum=int.Parse(ds.Tables[0].Rows[0]["FloorsNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UnderFloorsNum"].ToString()!="")
				{
					model.UnderFloorsNum=int.Parse(ds.Tables[0].Rows[0]["UnderFloorsNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UnitNum"].ToString()!="")
				{
					model.UnitNum=int.Parse(ds.Tables[0].Rows[0]["UnitNum"].ToString());
				}
				model.NamingPatterns=ds.Tables[0].Rows[0]["NamingPatterns"].ToString();
				if(ds.Tables[0].Rows[0]["PerFloorNum"].ToString()!="")
				{
					model.PerFloorNum=int.Parse(ds.Tables[0].Rows[0]["PerFloorNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["HouseholdsNum"].ToString()!="")
				{
					model.HouseholdsNum=int.Parse(ds.Tables[0].Rows[0]["HouseholdsNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SynchFlag"].ToString()!="")
				{
					model.SynchFlag=int.Parse(ds.Tables[0].Rows[0]["SynchFlag"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OrdSNum"].ToString()!="")
				{
					model.OrdSNum=long.Parse(ds.Tables[0].Rows[0]["OrdSNum"].ToString());
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
			strSql.Append(" FROM View_Unify_Building_Filter ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, string fieldOrder,string fieldList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (fieldList != "")
            {
                strSql.Append(fieldList);
            }
            else 
            {
                strSql.Append(" * ");
            }
            strSql.Append(" FROM View_Unify_Building_Filter ");
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
		public DataSet GetList(int Top,string strWhere,string fieldOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" BuildSynchCode,CommSynchCode,UnBuildID,BuildSNum,RegionSNum,BuildSign,BuildName,BuildType,BuildUses,PropertyRights,PropertyUses,BuildHeight,FloorsNum,UnderFloorsNum,UnitNum,NamingPatterns,PerFloorNum,HouseholdsNum,IsDelete,SynchFlag,OrdSNum ");
			strSql.Append(" FROM Tb_Unify_Building ");
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
			parameters[5].Value = "SELECT * FROM Tb_Unify_Building WHERE 1=1 " + StrCondition;
			parameters[6].Value = "BuildSynchCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

