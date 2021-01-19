using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace HM.DAL.Qm
{
	/// <summary>
	/// 数据访问类Dal_Tb_Qm_Standard。
	/// </summary>
	public class Dal_Tb_Qm_Standard
	{
		public Dal_Tb_Qm_Standard()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string Id)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,50)};
			parameters[0].Value = Id;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Qm_Standard_Exists",parameters,out rowsAffected);
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
		public void Add(HM.Model.Qm.Tb_Qm_Standard model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@Code", SqlDbType.NVarChar,50),
					new SqlParameter("@SuitableItemTypeId", SqlDbType.VarChar,36),
					new SqlParameter("@Professional", SqlDbType.VarChar,36),
					new SqlParameter("@Type", SqlDbType.VarChar,36),
					new SqlParameter("@TypeDescription", SqlDbType.NVarChar,3999),
					new SqlParameter("@CheckStandard", SqlDbType.NVarChar,3999),
					new SqlParameter("@CheckWay", SqlDbType.NVarChar,3999),
					new SqlParameter("@Point", SqlDbType.Decimal,9),
					new SqlParameter("@IsCoerce", SqlDbType.Int,4),
					new SqlParameter("@IsUse", SqlDbType.Int,4),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.Code;
			parameters[2].Value = model.SuitableItemTypeId;
			parameters[3].Value = model.Professional;
			parameters[4].Value = model.Type;
			parameters[5].Value = model.TypeDescription;
			parameters[6].Value = model.CheckStandard;
			parameters[7].Value = model.CheckWay;
			parameters[8].Value = model.Point;
			parameters[9].Value = model.IsCoerce;
			parameters[10].Value = model.IsUse;
			parameters[11].Value = model.IsDelete;
			parameters[12].Value = model.Sort;

			DbHelperSQL.RunProcedure("Proc_Tb_Qm_Standard_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(HM.Model.Qm.Tb_Qm_Standard model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@Code", SqlDbType.NVarChar,50),
					new SqlParameter("@SuitableItemTypeId", SqlDbType.VarChar,36),
					new SqlParameter("@Professional", SqlDbType.VarChar,36),
					new SqlParameter("@Type", SqlDbType.VarChar,36),
					new SqlParameter("@TypeDescription", SqlDbType.NVarChar,3999),
					new SqlParameter("@CheckStandard", SqlDbType.NVarChar,3999),
					new SqlParameter("@CheckWay", SqlDbType.NVarChar,3999),
					new SqlParameter("@Point", SqlDbType.Decimal,9),
					new SqlParameter("@IsCoerce", SqlDbType.Int,4),
					new SqlParameter("@IsUse", SqlDbType.Int,4),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.Code;
			parameters[2].Value = model.SuitableItemTypeId;
			parameters[3].Value = model.Professional;
			parameters[4].Value = model.Type;
			parameters[5].Value = model.TypeDescription;
			parameters[6].Value = model.CheckStandard;
			parameters[7].Value = model.CheckWay;
			parameters[8].Value = model.Point;
			parameters[9].Value = model.IsCoerce;
			parameters[10].Value = model.IsUse;
			parameters[11].Value = model.IsDelete;
			parameters[12].Value = model.Sort;

			DbHelperSQL.RunProcedure("Proc_Tb_Qm_Standard_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string Id)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,50)};
			parameters[0].Value = Id;

			DbHelperSQL.RunProcedure("Proc_Tb_Qm_Standard_Delete",parameters,out rowsAffected);
		}
        /// <summary>
        /// 批量更新
        /// </summary>
        public void updateIsUse(string Ids, int Is)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1200),
             new SqlParameter("@Is", SqlDbType.Int,4)};
            parameters[0].Value = Ids;
            parameters[1].Value = Is;
            DbHelperSQL.RunProcedure("Proc_Tb_Qm_Standard_updateIsUse", parameters, out rowsAffected);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HM.Model.Qm.Tb_Qm_Standard GetModel(string Id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,50)};
			parameters[0].Value = Id;

			HM.Model.Qm.Tb_Qm_Standard model=new HM.Model.Qm.Tb_Qm_Standard();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Qm_Standard_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.Id=ds.Tables[0].Rows[0]["Id"].ToString();
				model.Code=ds.Tables[0].Rows[0]["Code"].ToString();
				model.SuitableItemTypeId=ds.Tables[0].Rows[0]["SuitableItemTypeId"].ToString();
				model.Professional=ds.Tables[0].Rows[0]["Professional"].ToString();
				model.Type=ds.Tables[0].Rows[0]["Type"].ToString();
				model.TypeDescription=ds.Tables[0].Rows[0]["TypeDescription"].ToString();
				model.CheckStandard=ds.Tables[0].Rows[0]["CheckStandard"].ToString();
				model.CheckWay=ds.Tables[0].Rows[0]["CheckWay"].ToString();
				if(ds.Tables[0].Rows[0]["Point"].ToString()!="")
				{
					model.Point=decimal.Parse(ds.Tables[0].Rows[0]["Point"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsCoerce"].ToString()!="")
				{
					model.IsCoerce=int.Parse(ds.Tables[0].Rows[0]["IsCoerce"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsUse"].ToString()!="")
				{
					model.IsUse=int.Parse(ds.Tables[0].Rows[0]["IsUse"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Sort"].ToString()!="")
				{
					model.Sort=int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
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
			strSql.Append("select Id,Code,SuitableItemTypeId,Professional,Type,TypeDescription,CheckStandard,CheckWay,Point,IsCoerce,IsUse,IsDelete,Sort ");
			strSql.Append(" FROM Tb_Qm_Standard ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
        public DataSet GetMaxList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAX(Sort) ");
            strSql.Append(" FROM Tb_Qm_Standard ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
			strSql.Append(" Id,Code,SuitableItemTypeId,Professional,Type,TypeDescription,CheckStandard,CheckWay,Point,IsCoerce,IsUse,IsDelete,Sort ");
			strSql.Append(" FROM Tb_Qm_Standard ");
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
			parameters[5].Value = "SELECT * FROM Tb_Qm_Standard WHERE 1=1 " + StrCondition;
			parameters[6].Value = "Id";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

