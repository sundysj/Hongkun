using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace HM.DAL.Qm
{
	/// <summary>
	/// 数据访问类Dal_Tb_Qm_TaskAbarbeitung。
	/// </summary>
	public class Dal_Tb_Qm_TaskAbarbeitung
	{
		public Dal_Tb_Qm_TaskAbarbeitung()
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Qm_TaskAbarbeitung_Exists",parameters,out rowsAffected);
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
		public void Add(HM.Model.Qm.Tb_Qm_TaskAbarbeitung model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@TaskId", SqlDbType.VarChar,36),
					new SqlParameter("@PointIds", SqlDbType.NVarChar,500),
					new SqlParameter("@AddPId", SqlDbType.VarChar,36),
					new SqlParameter("@Pictures", SqlDbType.NVarChar,1000),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@CheckTime", SqlDbType.DateTime),
					new SqlParameter("@CheckNote", SqlDbType.NVarChar,500),
					new SqlParameter("@CheckRemark", SqlDbType.NVarChar,500),
					new SqlParameter("@CheckResult", SqlDbType.NVarChar,500),
					new SqlParameter("@CheckResult1", SqlDbType.NVarChar,500),
					new SqlParameter("@ProblemType", SqlDbType.NVarChar,200),
					new SqlParameter("@RectificationPeriod", SqlDbType.NVarChar,50),
					new SqlParameter("@RectificationNote", SqlDbType.NVarChar,500),
					new SqlParameter("@AbarPId", SqlDbType.VarChar,36),
					new SqlParameter("@ReducePoint", SqlDbType.Decimal,5),
					new SqlParameter("@ReducePId", SqlDbType.VarChar,36),
					new SqlParameter("@AbarCheck", SqlDbType.VarChar,36),
					new SqlParameter("@AbarCheckPId", SqlDbType.VarChar,36),
					new SqlParameter("@AbarCheckTime", SqlDbType.DateTime),
					new SqlParameter("@Major", SqlDbType.NVarChar,36),
					new SqlParameter("@ReduceResult", SqlDbType.NVarChar,500),
					new SqlParameter("@ReduceTime", SqlDbType.DateTime),
					new SqlParameter("@ReduceCheckResult", SqlDbType.NVarChar,500),
					new SqlParameter("@ReduceCheckTime", SqlDbType.DateTime),
					new SqlParameter("@IsInTime", SqlDbType.Int,4),
					new SqlParameter("@IsOk", SqlDbType.Int,4),
					new SqlParameter("@CheckStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@Files", SqlDbType.NVarChar,3999),
					new SqlParameter("@Coordinate", SqlDbType.NVarChar,200),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.TaskId;
			parameters[2].Value = model.PointIds;
			parameters[3].Value = model.AddPId;
			parameters[4].Value = model.Pictures;
			parameters[5].Value = model.AddTime;
			parameters[6].Value = model.CheckTime;
			parameters[7].Value = model.CheckNote;
			parameters[8].Value = model.CheckRemark;
			parameters[9].Value = model.CheckResult;
			parameters[10].Value = model.CheckResult1;
			parameters[11].Value = model.ProblemType;
			parameters[12].Value = model.RectificationPeriod;
			parameters[13].Value = model.RectificationNote;
			parameters[14].Value = model.AbarPId;
			parameters[15].Value = model.ReducePoint;
			parameters[16].Value = model.ReducePId;
			parameters[17].Value = model.AbarCheck;
			parameters[18].Value = model.AbarCheckPId;
			parameters[19].Value = model.AbarCheckTime;
			parameters[20].Value = model.Major;
			parameters[21].Value = model.ReduceResult;
			parameters[22].Value = model.ReduceTime;
			parameters[23].Value = model.ReduceCheckResult;
			parameters[24].Value = model.ReduceCheckTime;
			parameters[25].Value = model.IsInTime;
			parameters[26].Value = model.IsOk;
			parameters[27].Value = model.CheckStatus;
			parameters[28].Value = model.Files;
			parameters[29].Value = model.Coordinate;
			parameters[30].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Qm_TaskAbarbeitung_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(HM.Model.Qm.Tb_Qm_TaskAbarbeitung model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@TaskId", SqlDbType.VarChar,36),
					new SqlParameter("@PointIds", SqlDbType.NVarChar,500),
					new SqlParameter("@AddPId", SqlDbType.VarChar,36),
					new SqlParameter("@Pictures", SqlDbType.NVarChar,1000),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@CheckTime", SqlDbType.DateTime),
					new SqlParameter("@CheckNote", SqlDbType.NVarChar,500),
					new SqlParameter("@CheckRemark", SqlDbType.NVarChar,500),
					new SqlParameter("@CheckResult", SqlDbType.NVarChar,500),
					new SqlParameter("@CheckResult1", SqlDbType.NVarChar,500),
					new SqlParameter("@ProblemType", SqlDbType.NVarChar,200),
					new SqlParameter("@RectificationPeriod", SqlDbType.NVarChar,50),
					new SqlParameter("@RectificationNote", SqlDbType.NVarChar,500),
					new SqlParameter("@AbarPId", SqlDbType.VarChar,36),
					new SqlParameter("@ReducePoint", SqlDbType.Decimal,5),
					new SqlParameter("@ReducePId", SqlDbType.VarChar,36),
					new SqlParameter("@AbarCheck", SqlDbType.VarChar,36),
					new SqlParameter("@AbarCheckPId", SqlDbType.VarChar,36),
					new SqlParameter("@AbarCheckTime", SqlDbType.DateTime),
					new SqlParameter("@Major", SqlDbType.NVarChar,36),
					new SqlParameter("@ReduceResult", SqlDbType.NVarChar,500),
					new SqlParameter("@ReduceTime", SqlDbType.DateTime),
					new SqlParameter("@ReduceCheckResult", SqlDbType.NVarChar,500),
					new SqlParameter("@ReduceCheckTime", SqlDbType.DateTime),
					new SqlParameter("@IsInTime", SqlDbType.Int,4),
					new SqlParameter("@IsOk", SqlDbType.Int,4),
					new SqlParameter("@CheckStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@Files", SqlDbType.NVarChar,3999),
					new SqlParameter("@Coordinate", SqlDbType.NVarChar,200),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.TaskId;
			parameters[2].Value = model.PointIds;
			parameters[3].Value = model.AddPId;
			parameters[4].Value = model.Pictures;
			parameters[5].Value = model.AddTime;
			parameters[6].Value = model.CheckTime;
			parameters[7].Value = model.CheckNote;
			parameters[8].Value = model.CheckRemark;
			parameters[9].Value = model.CheckResult;
			parameters[10].Value = model.CheckResult1;
			parameters[11].Value = model.ProblemType;
			parameters[12].Value = model.RectificationPeriod;
			parameters[13].Value = model.RectificationNote;
			parameters[14].Value = model.AbarPId;
			parameters[15].Value = model.ReducePoint;
			parameters[16].Value = model.ReducePId;
			parameters[17].Value = model.AbarCheck;
			parameters[18].Value = model.AbarCheckPId;
			parameters[19].Value = model.AbarCheckTime;
			parameters[20].Value = model.Major;
			parameters[21].Value = model.ReduceResult;
			parameters[22].Value = model.ReduceTime;
			parameters[23].Value = model.ReduceCheckResult;
			parameters[24].Value = model.ReduceCheckTime;
			parameters[25].Value = model.IsInTime;
			parameters[26].Value = model.IsOk;
			parameters[27].Value = model.CheckStatus;
			parameters[28].Value = model.Files;
			parameters[29].Value = model.Coordinate;
			parameters[30].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Qm_TaskAbarbeitung_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_Qm_TaskAbarbeitung_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public HM.Model.Qm.Tb_Qm_TaskAbarbeitung GetModel(string Id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,50)};
			parameters[0].Value = Id;

			HM.Model.Qm.Tb_Qm_TaskAbarbeitung model=new HM.Model.Qm.Tb_Qm_TaskAbarbeitung();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Qm_TaskAbarbeitung_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.Id=ds.Tables[0].Rows[0]["Id"].ToString();
				model.TaskId=ds.Tables[0].Rows[0]["TaskId"].ToString();
				model.PointIds=ds.Tables[0].Rows[0]["PointIds"].ToString();
				model.AddPId=ds.Tables[0].Rows[0]["AddPId"].ToString();
				model.Pictures=ds.Tables[0].Rows[0]["Pictures"].ToString();
				if(ds.Tables[0].Rows[0]["AddTime"].ToString()!="")
				{
					model.AddTime=DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CheckTime"].ToString()!="")
				{
					model.CheckTime=DateTime.Parse(ds.Tables[0].Rows[0]["CheckTime"].ToString());
				}
				model.CheckNote=ds.Tables[0].Rows[0]["CheckNote"].ToString();
				model.CheckRemark=ds.Tables[0].Rows[0]["CheckRemark"].ToString();
				model.CheckResult=ds.Tables[0].Rows[0]["CheckResult"].ToString();
				model.CheckResult1=ds.Tables[0].Rows[0]["CheckResult1"].ToString();
				model.ProblemType=ds.Tables[0].Rows[0]["ProblemType"].ToString();
				model.RectificationPeriod=ds.Tables[0].Rows[0]["RectificationPeriod"].ToString();
				model.RectificationNote=ds.Tables[0].Rows[0]["RectificationNote"].ToString();
				model.AbarPId=ds.Tables[0].Rows[0]["AbarPId"].ToString();
				if(ds.Tables[0].Rows[0]["ReducePoint"].ToString()!="")
				{
					model.ReducePoint=decimal.Parse(ds.Tables[0].Rows[0]["ReducePoint"].ToString());
				}
				model.ReducePId=ds.Tables[0].Rows[0]["ReducePId"].ToString();
				model.AbarCheck=ds.Tables[0].Rows[0]["AbarCheck"].ToString();
				model.AbarCheckPId=ds.Tables[0].Rows[0]["AbarCheckPId"].ToString();
				if(ds.Tables[0].Rows[0]["AbarCheckTime"].ToString()!="")
				{
					model.AbarCheckTime=DateTime.Parse(ds.Tables[0].Rows[0]["AbarCheckTime"].ToString());
				}
				model.Major=ds.Tables[0].Rows[0]["Major"].ToString();
				model.ReduceResult=ds.Tables[0].Rows[0]["ReduceResult"].ToString();
				if(ds.Tables[0].Rows[0]["ReduceTime"].ToString()!="")
				{
					model.ReduceTime=DateTime.Parse(ds.Tables[0].Rows[0]["ReduceTime"].ToString());
				}
				model.ReduceCheckResult=ds.Tables[0].Rows[0]["ReduceCheckResult"].ToString();
				if(ds.Tables[0].Rows[0]["ReduceCheckTime"].ToString()!="")
				{
					model.ReduceCheckTime=DateTime.Parse(ds.Tables[0].Rows[0]["ReduceCheckTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsInTime"].ToString()!="")
				{
					model.IsInTime=int.Parse(ds.Tables[0].Rows[0]["IsInTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsOk"].ToString()!="")
				{
					model.IsOk=int.Parse(ds.Tables[0].Rows[0]["IsOk"].ToString());
				}
				model.CheckStatus=ds.Tables[0].Rows[0]["CheckStatus"].ToString();
				model.Files=ds.Tables[0].Rows[0]["Files"].ToString();
				model.Coordinate=ds.Tables[0].Rows[0]["Coordinate"].ToString();
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
                if (ds.Tables[0].Rows[0]["UploadTime"].ToString() != "")
                {
                    model.UploadTime = DateTime.Parse(ds.Tables[0].Rows[0]["UploadTime"].ToString());
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
			strSql.Append("select Id,TaskId,PointIds,AddPId,Pictures,AddTime,CheckTime,CheckNote,CheckRemark,CheckResult,CheckResult1,ProblemType,RectificationPeriod,RectificationNote,AbarPId,ReducePoint,ReducePId,AbarCheck,AbarCheckPId,AbarCheckTime,Major,ReduceResult,ReduceTime,ReduceCheckResult,ReduceCheckTime,IsInTime,IsOk,CheckStatus,Files,Coordinate,IsDelete ");
			strSql.Append(" FROM Tb_Qm_TaskAbarbeitung ");
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
			strSql.Append(" Id,TaskId,PointIds,AddPId,Pictures,AddTime,CheckTime,CheckNote,CheckRemark,CheckResult,CheckResult1,ProblemType,RectificationPeriod,RectificationNote,AbarPId,ReducePoint,ReducePId,AbarCheck,AbarCheckPId,AbarCheckTime,Major,ReduceResult,ReduceTime,ReduceCheckResult,ReduceCheckTime,IsInTime,IsOk,CheckStatus,Files,Coordinate,IsDelete ");
			strSql.Append(" FROM Tb_Qm_TaskAbarbeitung ");
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
			parameters[5].Value = "SELECT * FROM Tb_Qm_TaskAbarbeitung WHERE 1=1 " + StrCondition;
			parameters[6].Value = "Id";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

