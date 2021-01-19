using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Unify
{
	/// <summary>
	/// 数据访问类Dal_Tb_Unify_Community。
	/// </summary>
	public class Dal_Tb_Unify_Community
	{
		public Dal_Tb_Unify_Community()
		{
			DbHelperSQL.ConnectionString = PubConstant.SQIBSContionString.ToString();
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid CommSynchCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CommSynchCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = CommSynchCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Unify_Community_Exists",parameters,out rowsAffected);
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
		public void Add(ehome.Model.Unify.Tb_Unify_Community model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CommSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CorpSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@UnCommID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CommName", SqlDbType.NVarChar,50),
					new SqlParameter("@CommKind", SqlDbType.NVarChar,20),
					new SqlParameter("@ManageTime", SqlDbType.DateTime),
					new SqlParameter("@ManageKind", SqlDbType.NVarChar,20),
					new SqlParameter("@ProvinceID", SqlDbType.Int,4),
					new SqlParameter("@CityID", SqlDbType.Int,4),
					new SqlParameter("@BoroughID", SqlDbType.Int,4),
					new SqlParameter("@Street", SqlDbType.NVarChar,50),
					new SqlParameter("@GateSign", SqlDbType.NVarChar,20),
					new SqlParameter("@CommAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@CommPost", SqlDbType.NVarChar,10),
					new SqlParameter("@RegDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@CommSNum", SqlDbType.Int,4),
					new SqlParameter("@CommunityName", SqlDbType.NVarChar,50),
					new SqlParameter("@CommunityCode", SqlDbType.NVarChar,40)};
			parameters[0].Value = model.CommSynchCode;
			parameters[1].Value = model.CorpSynchCode;
			parameters[2].Value = model.UnCommID;
			parameters[3].Value = model.CommID;
			parameters[4].Value = model.CommName;
			parameters[5].Value = model.CommKind;
			parameters[6].Value = model.ManageTime;
			parameters[7].Value = model.ManageKind;
			parameters[8].Value = model.ProvinceID;
			parameters[9].Value = model.CityID;
			parameters[10].Value = model.BoroughID;
			parameters[11].Value = model.Street;
			parameters[12].Value = model.GateSign;
			parameters[13].Value = model.CommAddress;
			parameters[14].Value = model.CommPost;
			parameters[15].Value = model.RegDate;
			parameters[16].Value = model.IsDelete;
			parameters[17].Value = model.SynchFlag;
			parameters[18].Value = model.CommSNum;
			parameters[19].Value = model.CommunityName;
			parameters[20].Value = model.CommunityCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Unify_Community_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(ehome.Model.Unify.Tb_Unify_Community model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CommSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CorpSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@UnCommID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CommName", SqlDbType.NVarChar,50),
					new SqlParameter("@CommKind", SqlDbType.NVarChar,20),
					new SqlParameter("@ManageTime", SqlDbType.DateTime),
					new SqlParameter("@ManageKind", SqlDbType.NVarChar,20),
					new SqlParameter("@ProvinceID", SqlDbType.Int,4),
					new SqlParameter("@CityID", SqlDbType.Int,4),
					new SqlParameter("@BoroughID", SqlDbType.Int,4),
					new SqlParameter("@Street", SqlDbType.NVarChar,50),
					new SqlParameter("@GateSign", SqlDbType.NVarChar,20),
					new SqlParameter("@CommAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@CommPost", SqlDbType.NVarChar,10),
					new SqlParameter("@RegDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@CommSNum", SqlDbType.Int,4),
					new SqlParameter("@CommunityName", SqlDbType.NVarChar,50),
					new SqlParameter("@CommunityCode", SqlDbType.NVarChar,40)};
			parameters[0].Value = model.CommSynchCode;
			parameters[1].Value = model.CorpSynchCode;
			parameters[2].Value = model.UnCommID;
			parameters[3].Value = model.CommID;
			parameters[4].Value = model.CommName;
			parameters[5].Value = model.CommKind;
			parameters[6].Value = model.ManageTime;
			parameters[7].Value = model.ManageKind;
			parameters[8].Value = model.ProvinceID;
			parameters[9].Value = model.CityID;
			parameters[10].Value = model.BoroughID;
			parameters[11].Value = model.Street;
			parameters[12].Value = model.GateSign;
			parameters[13].Value = model.CommAddress;
			parameters[14].Value = model.CommPost;
			parameters[15].Value = model.RegDate;
			parameters[16].Value = model.IsDelete;
			parameters[17].Value = model.SynchFlag;
			parameters[18].Value = model.CommSNum;
			parameters[19].Value = model.CommunityName;
			parameters[20].Value = model.CommunityCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Unify_Community_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(Guid CommSynchCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CommSynchCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = CommSynchCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Unify_Community_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ehome.Model.Unify.Tb_Unify_Community GetModel(Guid CommSynchCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@CommSynchCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = CommSynchCode;

			ehome.Model.Unify.Tb_Unify_Community model=new ehome.Model.Unify.Tb_Unify_Community();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Unify_Community_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["CommSynchCode"].ToString()!="")
				{
					model.CommSynchCode=new Guid(ds.Tables[0].Rows[0]["CommSynchCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CorpSynchCode"].ToString()!="")
				{
					model.CorpSynchCode=new Guid(ds.Tables[0].Rows[0]["CorpSynchCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UnCommID"].ToString()!="")
				{
					model.UnCommID=long.Parse(ds.Tables[0].Rows[0]["UnCommID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				model.CommName=ds.Tables[0].Rows[0]["CommName"].ToString();
				model.CommKind=ds.Tables[0].Rows[0]["CommKind"].ToString();
				if(ds.Tables[0].Rows[0]["ManageTime"].ToString()!="")
				{
					model.ManageTime=DateTime.Parse(ds.Tables[0].Rows[0]["ManageTime"].ToString());
				}
				model.ManageKind=ds.Tables[0].Rows[0]["ManageKind"].ToString();
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
				model.Street=ds.Tables[0].Rows[0]["Street"].ToString();
				model.GateSign=ds.Tables[0].Rows[0]["GateSign"].ToString();
				model.CommAddress=ds.Tables[0].Rows[0]["CommAddress"].ToString();
				model.CommPost=ds.Tables[0].Rows[0]["CommPost"].ToString();
				if(ds.Tables[0].Rows[0]["RegDate"].ToString()!="")
				{
					model.RegDate=DateTime.Parse(ds.Tables[0].Rows[0]["RegDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SynchFlag"].ToString()!="")
				{
					model.SynchFlag=int.Parse(ds.Tables[0].Rows[0]["SynchFlag"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommSNum"].ToString()!="")
				{
					model.CommSNum=int.Parse(ds.Tables[0].Rows[0]["CommSNum"].ToString());
				}
				model.CommunityName=ds.Tables[0].Rows[0]["CommunityName"].ToString();
				model.CommunityCode=ds.Tables[0].Rows[0]["CommunityCode"].ToString();
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
			strSql.Append("select CommSynchCode,CorpSynchCode,UnCommID,CommID,CommName,CommKind,ManageTime,ManageKind,ProvinceID,CityID,BoroughID,Street,GateSign,CommAddress,CommPost,RegDate,IsDelete,SynchFlag,CommSNum,CommunityName,CommunityCode ");
			strSql.Append(" FROM Tb_Unify_Community ");
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
            strSql.Append(" FROM view_Unify_CommList_Filter ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + fieldOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string fieldOrder,string fieldList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            if (fieldList != "")
            {
                strSql.Append(fieldList);
            }
            else
            {
                strSql.Append(" * ");
            }
            strSql.Append(" FROM view_Unify_CommList_Filter ");
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
            parameters[5].Value = "SELECT * FROM view_Unify_CommList_Filter WHERE 1=1 " + StrCondition;
			parameters[6].Value = "CommSynchCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

