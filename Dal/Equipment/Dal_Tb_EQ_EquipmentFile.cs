using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace HM.DAL.Eq
{
	/// <summary>
	/// 数据访问类Dal_Tb_EQ_EquipmentFile。
	/// </summary>
	public class Dal_Tb_EQ_EquipmentFile
	{
		public Dal_Tb_EQ_EquipmentFile()
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_EQ_EquipmentFile_Exists",parameters,out rowsAffected);
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
		public void Add(HM.Model.Eq.Tb_EQ_EquipmentFile model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@EquiId", SqlDbType.VarChar,36),
					new SqlParameter("@FileName", SqlDbType.NVarChar,200),
					new SqlParameter("@Fix", SqlDbType.NVarChar,20),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
					new SqlParameter("@FilePath", SqlDbType.NVarChar,200),
					new SqlParameter("@PhoneName", SqlDbType.NVarChar,200),
					new SqlParameter("@PhotoTime", SqlDbType.DateTime),
					new SqlParameter("@PhotoPId", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.EquiId;
			parameters[2].Value = model.FileName;
			parameters[3].Value = model.Fix;
			parameters[4].Value = model.IsDelete;
			parameters[5].Value = model.FilePath;
			parameters[6].Value = model.PhoneName;
			parameters[7].Value = model.PhotoTime;
			parameters[8].Value = model.PhotoPId;

			DbHelperSQL.RunProcedure("Proc_Tb_EQ_EquipmentFile_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(HM.Model.Eq.Tb_EQ_EquipmentFile model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@EquiId", SqlDbType.VarChar,36),
					new SqlParameter("@FileName", SqlDbType.NVarChar,200),
					new SqlParameter("@Fix", SqlDbType.NVarChar,20),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
					new SqlParameter("@FilePath", SqlDbType.NVarChar,200),
					new SqlParameter("@PhoneName", SqlDbType.NVarChar,200),
					new SqlParameter("@PhotoTime", SqlDbType.DateTime),
					new SqlParameter("@PhotoPId", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.EquiId;
			parameters[2].Value = model.FileName;
			parameters[3].Value = model.Fix;
			parameters[4].Value = model.IsDelete;
			parameters[5].Value = model.FilePath;
			parameters[6].Value = model.PhoneName;
			parameters[7].Value = model.PhotoTime;
			parameters[8].Value = model.PhotoPId;

			DbHelperSQL.RunProcedure("Proc_Tb_EQ_EquipmentFile_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_EQ_EquipmentFile_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public HM.Model.Eq.Tb_EQ_EquipmentFile GetModel(string Id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,50)};
			parameters[0].Value = Id;

			HM.Model.Eq.Tb_EQ_EquipmentFile model=new HM.Model.Eq.Tb_EQ_EquipmentFile();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_EQ_EquipmentFile_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.Id=ds.Tables[0].Rows[0]["Id"].ToString();
				model.EquiId=ds.Tables[0].Rows[0]["EquiId"].ToString();
				model.FileName=ds.Tables[0].Rows[0]["FileName"].ToString();
				model.Fix=ds.Tables[0].Rows[0]["Fix"].ToString();
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				model.FilePath=ds.Tables[0].Rows[0]["FilePath"].ToString();
				model.PhoneName=ds.Tables[0].Rows[0]["PhoneName"].ToString();
				if(ds.Tables[0].Rows[0]["PhotoTime"].ToString()!="")
				{
					model.PhotoTime=DateTime.Parse(ds.Tables[0].Rows[0]["PhotoTime"].ToString());
				}
				model.PhotoPId=ds.Tables[0].Rows[0]["PhotoPId"].ToString();
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
			strSql.Append("select Id,EquiId,FileName,Fix,IsDelete,FilePath,PhoneName,PhotoTime,PhotoPId ");
			strSql.Append(" FROM Tb_EQ_EquipmentFile ");
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
			strSql.Append(" Id,EquiId,FileName,Fix,IsDelete,FilePath,PhoneName,PhotoTime,PhotoPId ");
			strSql.Append(" FROM Tb_EQ_EquipmentFile ");
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
			parameters[5].Value = "SELECT * FROM Tb_EQ_EquipmentFile WHERE 1=1 " + StrCondition;
			parameters[6].Value = "Id";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

