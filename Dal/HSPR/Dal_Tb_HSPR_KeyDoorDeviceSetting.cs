using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_KeyDoorDeviceSetting。
	/// </summary>
	public class Dal_Tb_HSPR_KeyDoorDeviceSetting
	{
		public Dal_Tb_HSPR_KeyDoorDeviceSetting()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("DoorID", "Tb_HSPR_KeyDoorDeviceSetting"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long DoorID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@DoorID", SqlDbType.BigInt)};
			parameters[0].Value = DoorID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_KeyDoorDeviceSetting_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_KeyDoorDeviceSetting model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@DoorID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@DoorType", SqlDbType.SmallInt,2),
					new SqlParameter("@DoorNum", SqlDbType.NVarChar,50),
					new SqlParameter("@DoorName", SqlDbType.NVarChar,50),
					new SqlParameter("@DeviceAddRess", SqlDbType.NVarChar,50),
					new SqlParameter("@BuildSNum", SqlDbType.Int,4),
					new SqlParameter("@UnitSNum", SqlDbType.Int,4),
					new SqlParameter("@Memo", SqlDbType.NVarChar,200),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.DoorID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.DoorType;
			parameters[3].Value = model.DoorNum;
			parameters[4].Value = model.DoorName;
			parameters[5].Value = model.DeviceAddRess;
			parameters[6].Value = model.BuildSNum;
			parameters[7].Value = model.UnitSNum;
			parameters[8].Value = model.Memo;
			parameters[9].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_KeyDoorDeviceSetting_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_KeyDoorDeviceSetting model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@DoorID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@DoorType", SqlDbType.SmallInt,2),
					new SqlParameter("@DoorNum", SqlDbType.NVarChar,50),
					new SqlParameter("@DoorName", SqlDbType.NVarChar,50),
					new SqlParameter("@DeviceAddRess", SqlDbType.NVarChar,50),
					new SqlParameter("@BuildSNum", SqlDbType.Int,4),
					new SqlParameter("@UnitSNum", SqlDbType.Int,4),
					new SqlParameter("@Memo", SqlDbType.NVarChar,200),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.DoorID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.DoorType;
			parameters[3].Value = model.DoorNum;
			parameters[4].Value = model.DoorName;
			parameters[5].Value = model.DeviceAddRess;
			parameters[6].Value = model.BuildSNum;
			parameters[7].Value = model.UnitSNum;
			parameters[8].Value = model.Memo;
			parameters[9].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_KeyDoorDeviceSetting_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long DoorID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@DoorID", SqlDbType.BigInt)};
			parameters[0].Value = DoorID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_KeyDoorDeviceSetting_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_KeyDoorDeviceSetting GetModel(long DoorID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@DoorID", SqlDbType.BigInt)};
			parameters[0].Value = DoorID;

			MobileSoft.Model.HSPR.Tb_HSPR_KeyDoorDeviceSetting model=new MobileSoft.Model.HSPR.Tb_HSPR_KeyDoorDeviceSetting();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_KeyDoorDeviceSetting_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["DoorID"].ToString()!="")
				{
					model.DoorID=long.Parse(ds.Tables[0].Rows[0]["DoorID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DoorType"].ToString()!="")
				{
					model.DoorType=int.Parse(ds.Tables[0].Rows[0]["DoorType"].ToString());
				}
				model.DoorNum=ds.Tables[0].Rows[0]["DoorNum"].ToString();
				model.DoorName=ds.Tables[0].Rows[0]["DoorName"].ToString();
				model.DeviceAddRess=ds.Tables[0].Rows[0]["DeviceAddRess"].ToString();
				if(ds.Tables[0].Rows[0]["BuildSNum"].ToString()!="")
				{
					model.BuildSNum=int.Parse(ds.Tables[0].Rows[0]["BuildSNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UnitSNum"].ToString()!="")
				{
					model.UnitSNum=int.Parse(ds.Tables[0].Rows[0]["UnitSNum"].ToString());
				}
				model.Memo=ds.Tables[0].Rows[0]["Memo"].ToString();
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
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
            strSql.Append("select DoorName,DeviceAddRess FROM Tb_HSPR_KeyDoorDeviceSetting");

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
            strSql.Append(" DoorID,CommID,DoorType,DoorNum,DoorName,DeviceAddRess,BuildSNum,UnitSNum,Memo,IsDelete,DeviceAddRessWifi,DeviceAddRessWifiPwd  ");
			strSql.Append(" FROM Tb_HSPR_KeyDoorDeviceSetting ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_KeyDoorDeviceSetting WHERE 1=1 " + StrCondition;
			parameters[6].Value = "DoorID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

