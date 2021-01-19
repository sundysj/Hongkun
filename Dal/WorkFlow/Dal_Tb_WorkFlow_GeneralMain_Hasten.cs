using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.WorkFlow
{
	/// <summary>
	/// 数据访问类Dal_Tb_WorkFlow_GeneralMain_Hasten。
	/// </summary>
	public class Dal_Tb_WorkFlow_GeneralMain_Hasten
	{
		public Dal_Tb_WorkFlow_GeneralMain_Hasten()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法


		/// <summary>
		///  增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain_Hasten model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@GeneralMainCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@FQR", SqlDbType.NVarChar,50),
					new SqlParameter("@CBR", SqlDbType.NVarChar,50),
					new SqlParameter("@BCBBM", SqlDbType.NVarChar,50),
					new SqlParameter("@BCBR", SqlDbType.NVarChar,50),
					new SqlParameter("@FQSJ", SqlDbType.SmallDateTime),
					new SqlParameter("@ZXBLR", SqlDbType.NVarChar,50),
					new SqlParameter("@ZXBLSJ", SqlDbType.SmallDateTime),
					new SqlParameter("@CutID", SqlDbType.Int,4)};
			parameters[0].Value = model.GeneralMainCode;
			parameters[1].Value = model.Title;
			parameters[2].Value = model.FQR;
			parameters[3].Value = model.CBR;
			parameters[4].Value = model.BCBBM;
			parameters[5].Value = model.BCBR;
			parameters[6].Value = model.FQSJ;
			parameters[7].Value = model.ZXBLR;
			parameters[8].Value = model.ZXBLSJ;
			parameters[9].Value = model.CutID;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_GeneralMain_Hasten_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain_Hasten model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@GeneralMainCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@FQR", SqlDbType.NVarChar,50),
					new SqlParameter("@CBR", SqlDbType.NVarChar,50),
					new SqlParameter("@BCBBM", SqlDbType.NVarChar,50),
					new SqlParameter("@BCBR", SqlDbType.NVarChar,50),
					new SqlParameter("@FQSJ", SqlDbType.SmallDateTime),
					new SqlParameter("@ZXBLR", SqlDbType.NVarChar,50),
					new SqlParameter("@ZXBLSJ", SqlDbType.SmallDateTime),
					new SqlParameter("@CutID", SqlDbType.Int,4)};
			parameters[0].Value = model.GeneralMainCode;
			parameters[1].Value = model.Title;
			parameters[2].Value = model.FQR;
			parameters[3].Value = model.CBR;
			parameters[4].Value = model.BCBBM;
			parameters[5].Value = model.BCBR;
			parameters[6].Value = model.FQSJ;
			parameters[7].Value = model.ZXBLR;
			parameters[8].Value = model.ZXBLSJ;
			parameters[9].Value = model.CutID;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_GeneralMain_Hasten_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete()
		{
			int rowsAffected;
			SqlParameter[] parameters = {
};

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_GeneralMain_Hasten_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain_Hasten GetModel()
		{
			SqlParameter[] parameters = {
};

			MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain_Hasten model=new MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain_Hasten();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_GeneralMain_Hasten_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["GeneralMainCode"].ToString()!="")
				{
					model.GeneralMainCode=new Guid(ds.Tables[0].Rows[0]["GeneralMainCode"].ToString());
				}
				model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				model.FQR=ds.Tables[0].Rows[0]["FQR"].ToString();
				model.CBR=ds.Tables[0].Rows[0]["CBR"].ToString();
				model.BCBBM=ds.Tables[0].Rows[0]["BCBBM"].ToString();
				model.BCBR=ds.Tables[0].Rows[0]["BCBR"].ToString();
				if(ds.Tables[0].Rows[0]["FQSJ"].ToString()!="")
				{
					model.FQSJ=DateTime.Parse(ds.Tables[0].Rows[0]["FQSJ"].ToString());
				}
				model.ZXBLR=ds.Tables[0].Rows[0]["ZXBLR"].ToString();
				if(ds.Tables[0].Rows[0]["ZXBLSJ"].ToString()!="")
				{
					model.ZXBLSJ=DateTime.Parse(ds.Tables[0].Rows[0]["ZXBLSJ"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CutID"].ToString()!="")
				{
					model.CutID=int.Parse(ds.Tables[0].Rows[0]["CutID"].ToString());
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
			strSql.Append("select GeneralMainCode,Title,FQR,CBR,BCBBM,BCBR,FQSJ,ZXBLR,ZXBLSJ,CutID ");
			strSql.Append(" FROM Tb_WorkFlow_GeneralMain_Hasten ");
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
			strSql.Append(" GeneralMainCode,Title,FQR,CBR,BCBBM,BCBR,FQSJ,ZXBLR,ZXBLSJ,CutID ");
			strSql.Append(" FROM Tb_WorkFlow_GeneralMain_Hasten ");
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
			parameters[5].Value = "SELECT * FROM Tb_WorkFlow_GeneralMain_Hasten WHERE 1=1 " + StrCondition;
			parameters[6].Value = "";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

