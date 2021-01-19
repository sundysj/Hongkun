using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Sys
{
	/// <summary>
	/// 数据访问类Dal_Tb_Sys_TakePicRoomState。
	/// </summary>
	public class Dal_Tb_Sys_TakePicRoomState
	{
		public Dal_Tb_Sys_TakePicRoomState()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long StatID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt)};
			parameters[0].Value = StatID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePicRoomState_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.Sys.Tb_Sys_TakePicRoomState model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt,8),
					new SqlParameter("@StatType", SqlDbType.SmallInt,2),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@StatDate", SqlDbType.DateTime),
					new SqlParameter("@RoomState", SqlDbType.Int,4),
					new SqlParameter("@Counts", SqlDbType.Int,4),
					new SqlParameter("@Counts0", SqlDbType.Int,4),
					new SqlParameter("@Counts1", SqlDbType.Int,4),
					new SqlParameter("@Counts2", SqlDbType.Int,4),
					new SqlParameter("@Counts3", SqlDbType.Int,4),
					new SqlParameter("@Counts4", SqlDbType.Int,4),
					new SqlParameter("@Counts5", SqlDbType.Int,4),
					new SqlParameter("@Counts6", SqlDbType.Int,4),
					new SqlParameter("@Counts7", SqlDbType.Int,4),
					new SqlParameter("@Counts8", SqlDbType.Int,4),
					new SqlParameter("@Area", SqlDbType.Decimal,9),
					new SqlParameter("@Area0", SqlDbType.Decimal,9),
					new SqlParameter("@Area1", SqlDbType.Decimal,9),
					new SqlParameter("@Area2", SqlDbType.Decimal,9),
					new SqlParameter("@Area3", SqlDbType.Decimal,9),
					new SqlParameter("@Area4", SqlDbType.Decimal,9),
					new SqlParameter("@Area5", SqlDbType.Decimal,9),
					new SqlParameter("@Area6", SqlDbType.Decimal,9),
					new SqlParameter("@Area7", SqlDbType.Decimal,9),
					new SqlParameter("@Area8", SqlDbType.Decimal,9)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.StatType;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.OrganCode;
			parameters[4].Value = model.StatDate;
			parameters[5].Value = model.RoomState;
			parameters[6].Value = model.Counts;
			parameters[7].Value = model.Counts0;
			parameters[8].Value = model.Counts1;
			parameters[9].Value = model.Counts2;
			parameters[10].Value = model.Counts3;
			parameters[11].Value = model.Counts4;
			parameters[12].Value = model.Counts5;
			parameters[13].Value = model.Counts6;
			parameters[14].Value = model.Counts7;
			parameters[15].Value = model.Counts8;
			parameters[16].Value = model.Area;
			parameters[17].Value = model.Area0;
			parameters[18].Value = model.Area1;
			parameters[19].Value = model.Area2;
			parameters[20].Value = model.Area3;
			parameters[21].Value = model.Area4;
			parameters[22].Value = model.Area5;
			parameters[23].Value = model.Area6;
			parameters[24].Value = model.Area7;
			parameters[25].Value = model.Area8;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePicRoomState_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Sys.Tb_Sys_TakePicRoomState model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt,8),
					new SqlParameter("@StatType", SqlDbType.SmallInt,2),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@StatDate", SqlDbType.DateTime),
					new SqlParameter("@RoomState", SqlDbType.Int,4),
					new SqlParameter("@Counts", SqlDbType.Int,4),
					new SqlParameter("@Counts0", SqlDbType.Int,4),
					new SqlParameter("@Counts1", SqlDbType.Int,4),
					new SqlParameter("@Counts2", SqlDbType.Int,4),
					new SqlParameter("@Counts3", SqlDbType.Int,4),
					new SqlParameter("@Counts4", SqlDbType.Int,4),
					new SqlParameter("@Counts5", SqlDbType.Int,4),
					new SqlParameter("@Counts6", SqlDbType.Int,4),
					new SqlParameter("@Counts7", SqlDbType.Int,4),
					new SqlParameter("@Counts8", SqlDbType.Int,4),
					new SqlParameter("@Area", SqlDbType.Decimal,9),
					new SqlParameter("@Area0", SqlDbType.Decimal,9),
					new SqlParameter("@Area1", SqlDbType.Decimal,9),
					new SqlParameter("@Area2", SqlDbType.Decimal,9),
					new SqlParameter("@Area3", SqlDbType.Decimal,9),
					new SqlParameter("@Area4", SqlDbType.Decimal,9),
					new SqlParameter("@Area5", SqlDbType.Decimal,9),
					new SqlParameter("@Area6", SqlDbType.Decimal,9),
					new SqlParameter("@Area7", SqlDbType.Decimal,9),
					new SqlParameter("@Area8", SqlDbType.Decimal,9)};
			parameters[0].Value = model.StatID;
			parameters[1].Value = model.StatType;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.OrganCode;
			parameters[4].Value = model.StatDate;
			parameters[5].Value = model.RoomState;
			parameters[6].Value = model.Counts;
			parameters[7].Value = model.Counts0;
			parameters[8].Value = model.Counts1;
			parameters[9].Value = model.Counts2;
			parameters[10].Value = model.Counts3;
			parameters[11].Value = model.Counts4;
			parameters[12].Value = model.Counts5;
			parameters[13].Value = model.Counts6;
			parameters[14].Value = model.Counts7;
			parameters[15].Value = model.Counts8;
			parameters[16].Value = model.Area;
			parameters[17].Value = model.Area0;
			parameters[18].Value = model.Area1;
			parameters[19].Value = model.Area2;
			parameters[20].Value = model.Area3;
			parameters[21].Value = model.Area4;
			parameters[22].Value = model.Area5;
			parameters[23].Value = model.Area6;
			parameters[24].Value = model.Area7;
			parameters[25].Value = model.Area8;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePicRoomState_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long StatID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt)};
			parameters[0].Value = StatID;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePicRoomState_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_TakePicRoomState GetModel(long StatID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt)};
			parameters[0].Value = StatID;

			MobileSoft.Model.Sys.Tb_Sys_TakePicRoomState model=new MobileSoft.Model.Sys.Tb_Sys_TakePicRoomState();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePicRoomState_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["StatID"].ToString()!="")
				{
					model.StatID=long.Parse(ds.Tables[0].Rows[0]["StatID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["StatType"].ToString()!="")
				{
					model.StatType=int.Parse(ds.Tables[0].Rows[0]["StatType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				model.OrganCode=ds.Tables[0].Rows[0]["OrganCode"].ToString();
				if(ds.Tables[0].Rows[0]["StatDate"].ToString()!="")
				{
					model.StatDate=DateTime.Parse(ds.Tables[0].Rows[0]["StatDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RoomState"].ToString()!="")
				{
					model.RoomState=int.Parse(ds.Tables[0].Rows[0]["RoomState"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Counts"].ToString()!="")
				{
					model.Counts=int.Parse(ds.Tables[0].Rows[0]["Counts"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Counts0"].ToString()!="")
				{
					model.Counts0=int.Parse(ds.Tables[0].Rows[0]["Counts0"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Counts1"].ToString()!="")
				{
					model.Counts1=int.Parse(ds.Tables[0].Rows[0]["Counts1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Counts2"].ToString()!="")
				{
					model.Counts2=int.Parse(ds.Tables[0].Rows[0]["Counts2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Counts3"].ToString()!="")
				{
					model.Counts3=int.Parse(ds.Tables[0].Rows[0]["Counts3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Counts4"].ToString()!="")
				{
					model.Counts4=int.Parse(ds.Tables[0].Rows[0]["Counts4"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Counts5"].ToString()!="")
				{
					model.Counts5=int.Parse(ds.Tables[0].Rows[0]["Counts5"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Counts6"].ToString()!="")
				{
					model.Counts6=int.Parse(ds.Tables[0].Rows[0]["Counts6"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Counts7"].ToString()!="")
				{
					model.Counts7=int.Parse(ds.Tables[0].Rows[0]["Counts7"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Counts8"].ToString()!="")
				{
					model.Counts8=int.Parse(ds.Tables[0].Rows[0]["Counts8"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Area"].ToString()!="")
				{
					model.Area=decimal.Parse(ds.Tables[0].Rows[0]["Area"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Area0"].ToString()!="")
				{
					model.Area0=decimal.Parse(ds.Tables[0].Rows[0]["Area0"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Area1"].ToString()!="")
				{
					model.Area1=decimal.Parse(ds.Tables[0].Rows[0]["Area1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Area2"].ToString()!="")
				{
					model.Area2=decimal.Parse(ds.Tables[0].Rows[0]["Area2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Area3"].ToString()!="")
				{
					model.Area3=decimal.Parse(ds.Tables[0].Rows[0]["Area3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Area4"].ToString()!="")
				{
					model.Area4=decimal.Parse(ds.Tables[0].Rows[0]["Area4"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Area5"].ToString()!="")
				{
					model.Area5=decimal.Parse(ds.Tables[0].Rows[0]["Area5"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Area6"].ToString()!="")
				{
					model.Area6=decimal.Parse(ds.Tables[0].Rows[0]["Area6"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Area7"].ToString()!="")
				{
					model.Area7=decimal.Parse(ds.Tables[0].Rows[0]["Area7"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Area8"].ToString()!="")
				{
					model.Area8=decimal.Parse(ds.Tables[0].Rows[0]["Area8"].ToString());
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
			strSql.Append(" FROM View_Sys_TakePicRoomState_Filter ");
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
                  strSql.Append(" FROM View_Sys_TakePicRoomState_Filter ");
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
                  parameters[5].Value = "SELECT * FROM View_Sys_TakePicRoomState_Filter WHERE 1=1 " + StrCondition;
			parameters[6].Value = "StatID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

