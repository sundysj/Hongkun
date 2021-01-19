using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_Parking。
	/// </summary>
	public class Dal_Tb_HSPR_Parking
	{
		public Dal_Tb_HSPR_Parking()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ParkID", "Tb_HSPR_Parking"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ParkID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ParkID", SqlDbType.BigInt)};
			parameters[0].Value = ParkID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Parking_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_Parking model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ParkID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@ParkType", SqlDbType.NVarChar,20),
					new SqlParameter("@ParkArea", SqlDbType.Decimal,9),
					new SqlParameter("@CarparkID", SqlDbType.Int,4),
					new SqlParameter("@ParkName", SqlDbType.NVarChar,30),
					new SqlParameter("@ParkingNum", SqlDbType.Int,4),
					new SqlParameter("@PropertyRight", SqlDbType.NVarChar,30),
					new SqlParameter("@StanID", SqlDbType.BigInt,8),
					new SqlParameter("@PropertyUses", SqlDbType.NVarChar,20),
					new SqlParameter("@UseState", SqlDbType.NVarChar,30),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@ParkCategory", SqlDbType.SmallInt,2),
					new SqlParameter("@ResideType", SqlDbType.SmallInt,2),
					new SqlParameter("@ParkSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@ParkMemo", SqlDbType.NVarChar,500)};
			parameters[0].Value = model.ParkID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CustID;
			parameters[3].Value = model.RoomID;
			parameters[4].Value = model.ParkType;
			parameters[5].Value = model.ParkArea;
			parameters[6].Value = model.CarparkID;
			parameters[7].Value = model.ParkName;
			parameters[8].Value = model.ParkingNum;
			parameters[9].Value = model.PropertyRight;
			parameters[10].Value = model.StanID;
			parameters[11].Value = model.PropertyUses;
			parameters[12].Value = model.UseState;
			parameters[13].Value = model.IsDelete;
			parameters[14].Value = model.ParkCategory;
			parameters[15].Value = model.ResideType;
			parameters[16].Value = model.ParkSynchCode;
			parameters[17].Value = model.SynchFlag;
			parameters[18].Value = model.ParkMemo;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Parking_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_Parking model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ParkID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@ParkType", SqlDbType.NVarChar,20),
					new SqlParameter("@ParkArea", SqlDbType.Decimal,9),
					new SqlParameter("@CarparkID", SqlDbType.Int,4),
					new SqlParameter("@ParkName", SqlDbType.NVarChar,30),
					new SqlParameter("@ParkingNum", SqlDbType.Int,4),
					new SqlParameter("@PropertyRight", SqlDbType.NVarChar,30),
					new SqlParameter("@StanID", SqlDbType.BigInt,8),
					new SqlParameter("@PropertyUses", SqlDbType.NVarChar,20),
					new SqlParameter("@UseState", SqlDbType.NVarChar,30),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@ParkCategory", SqlDbType.SmallInt,2),
					new SqlParameter("@ResideType", SqlDbType.SmallInt,2),
					new SqlParameter("@ParkSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@ParkMemo", SqlDbType.NVarChar,500)};
			parameters[0].Value = model.ParkID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CustID;
			parameters[3].Value = model.RoomID;
			parameters[4].Value = model.ParkType;
			parameters[5].Value = model.ParkArea;
			parameters[6].Value = model.CarparkID;
			parameters[7].Value = model.ParkName;
			parameters[8].Value = model.ParkingNum;
			parameters[9].Value = model.PropertyRight;
			parameters[10].Value = model.StanID;
			parameters[11].Value = model.PropertyUses;
			parameters[12].Value = model.UseState;
			parameters[13].Value = model.IsDelete;
			parameters[14].Value = model.ParkCategory;
			parameters[15].Value = model.ResideType;
			parameters[16].Value = model.ParkSynchCode;
			parameters[17].Value = model.SynchFlag;
			parameters[18].Value = model.ParkMemo;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Parking_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ParkID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ParkID", SqlDbType.BigInt)};
			parameters[0].Value = ParkID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Parking_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_Parking GetModel(long ParkID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ParkID", SqlDbType.BigInt)};
			parameters[0].Value = ParkID;

			MobileSoft.Model.HSPR.Tb_HSPR_Parking model=new MobileSoft.Model.HSPR.Tb_HSPR_Parking();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Parking_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ParkID"].ToString()!="")
				{
					model.ParkID=long.Parse(ds.Tables[0].Rows[0]["ParkID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CustID"].ToString()!="")
				{
					model.CustID=long.Parse(ds.Tables[0].Rows[0]["CustID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RoomID"].ToString()!="")
				{
					model.RoomID=long.Parse(ds.Tables[0].Rows[0]["RoomID"].ToString());
				}
				model.ParkType=ds.Tables[0].Rows[0]["ParkType"].ToString();
				if(ds.Tables[0].Rows[0]["ParkArea"].ToString()!="")
				{
					model.ParkArea=decimal.Parse(ds.Tables[0].Rows[0]["ParkArea"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CarparkID"].ToString()!="")
				{
					model.CarparkID=int.Parse(ds.Tables[0].Rows[0]["CarparkID"].ToString());
				}
				model.ParkName=ds.Tables[0].Rows[0]["ParkName"].ToString();
				if(ds.Tables[0].Rows[0]["ParkingNum"].ToString()!="")
				{
					model.ParkingNum=int.Parse(ds.Tables[0].Rows[0]["ParkingNum"].ToString());
				}
				model.PropertyRight=ds.Tables[0].Rows[0]["PropertyRight"].ToString();
				if(ds.Tables[0].Rows[0]["StanID"].ToString()!="")
				{
					model.StanID=long.Parse(ds.Tables[0].Rows[0]["StanID"].ToString());
				}
				model.PropertyUses=ds.Tables[0].Rows[0]["PropertyUses"].ToString();
				model.UseState=ds.Tables[0].Rows[0]["UseState"].ToString();
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ParkCategory"].ToString()!="")
				{
					model.ParkCategory=int.Parse(ds.Tables[0].Rows[0]["ParkCategory"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ResideType"].ToString()!="")
				{
					model.ResideType=int.Parse(ds.Tables[0].Rows[0]["ResideType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ParkSynchCode"].ToString()!="")
				{
					model.ParkSynchCode=new Guid(ds.Tables[0].Rows[0]["ParkSynchCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SynchFlag"].ToString()!="")
				{
					model.SynchFlag=int.Parse(ds.Tables[0].Rows[0]["SynchFlag"].ToString());
				}
				model.ParkMemo=ds.Tables[0].Rows[0]["ParkMemo"].ToString();
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
			strSql.Append(" FROM View_HSPR_Parking_Filter ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
      
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere,string TableName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TOP 10 * ");
            strSql.Append(" FROM  " + TableName);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
			strSql.Append(" ParkID,CommID,CustID,RoomID,ParkType,ParkArea,CarparkID,ParkName,ParkingNum,PropertyRight,StanID,PropertyUses,UseState,IsDelete,ParkCategory,ResideType,ParkSynchCode,SynchFlag,ParkMemo ");
			strSql.Append(" FROM Tb_HSPR_Parking ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_Parking WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ParkID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

