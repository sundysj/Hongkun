using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_Room。
	/// </summary>
	public class Dal_Tb_HSPR_Room
	{
		public Dal_Tb_HSPR_Room()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long RoomID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RoomID", SqlDbType.BigInt)};
			parameters[0].Value = RoomID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Room_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_Room model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@RoomSign", SqlDbType.NVarChar,20),
					new SqlParameter("@RoomName", SqlDbType.NVarChar,200),
					new SqlParameter("@RegionSNum", SqlDbType.Int,4),
					new SqlParameter("@BuildSNum", SqlDbType.Int,4),
					new SqlParameter("@UnitSNum", SqlDbType.Int,4),
					new SqlParameter("@FloorSNum", SqlDbType.Int,4),
					new SqlParameter("@RoomSNum", SqlDbType.Int,4),
					new SqlParameter("@RoomModel", SqlDbType.NVarChar,20),
					new SqlParameter("@RoomType", SqlDbType.NVarChar,20),
					new SqlParameter("@PropertyRights", SqlDbType.NVarChar,20),
					new SqlParameter("@RoomTowards", SqlDbType.NVarChar,10),
					new SqlParameter("@BuildArea", SqlDbType.Decimal,9),
					new SqlParameter("@InteriorArea", SqlDbType.Decimal,9),
					new SqlParameter("@CommonArea", SqlDbType.Decimal,9),
					new SqlParameter("@RightsSign", SqlDbType.NVarChar,50),
					new SqlParameter("@PropertyUses", SqlDbType.NVarChar,20),
					new SqlParameter("@RoomState", SqlDbType.Int,4),
					new SqlParameter("@ChargeTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@UsesState", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@FloorHeight", SqlDbType.NVarChar,30),
					new SqlParameter("@BuildStructure", SqlDbType.NVarChar,30),
					new SqlParameter("@PoolRatio", SqlDbType.Decimal,9),
					new SqlParameter("@BearParameters", SqlDbType.NVarChar,30),
					new SqlParameter("@Renovation", SqlDbType.NVarChar,30),
					new SqlParameter("@Configuration", SqlDbType.NVarChar,80),
					new SqlParameter("@Advertising", SqlDbType.NVarChar,30),
					new SqlParameter("@IsSplitUnite", SqlDbType.SmallInt,2),
					new SqlParameter("@GardenArea", SqlDbType.Decimal,9),
					new SqlParameter("@PropertyArea", SqlDbType.Decimal,9),
					new SqlParameter("@AreaType", SqlDbType.SmallInt,2),
					new SqlParameter("@YardArea", SqlDbType.Decimal,9),
					new SqlParameter("@DelUser", SqlDbType.NVarChar,50),
					new SqlParameter("@DelDate", SqlDbType.DateTime),
					new SqlParameter("@ResideType", SqlDbType.SmallInt,2),
					new SqlParameter("@RoomSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@BedTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@UseType", SqlDbType.SmallInt,2),
					new SqlParameter("@IsFrozen", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.RoomID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.RoomSign;
			parameters[3].Value = model.RoomName;
			parameters[4].Value = model.RegionSNum;
			parameters[5].Value = model.BuildSNum;
			parameters[6].Value = model.UnitSNum;
			parameters[7].Value = model.FloorSNum;
			parameters[8].Value = model.RoomSNum;
			parameters[9].Value = model.RoomModel;
			parameters[10].Value = model.RoomType;
			parameters[11].Value = model.PropertyRights;
			parameters[12].Value = model.RoomTowards;
			parameters[13].Value = model.BuildArea;
			parameters[14].Value = model.InteriorArea;
			parameters[15].Value = model.CommonArea;
			parameters[16].Value = model.RightsSign;
			parameters[17].Value = model.PropertyUses;
			parameters[18].Value = model.RoomState;
			parameters[19].Value = model.ChargeTypeID;
			parameters[20].Value = model.UsesState;
			parameters[21].Value = model.IsDelete;
			parameters[22].Value = model.FloorHeight;
			parameters[23].Value = model.BuildStructure;
			parameters[24].Value = model.PoolRatio;
			parameters[25].Value = model.BearParameters;
			parameters[26].Value = model.Renovation;
			parameters[27].Value = model.Configuration;
			parameters[28].Value = model.Advertising;
			parameters[29].Value = model.IsSplitUnite;
			parameters[30].Value = model.GardenArea;
			parameters[31].Value = model.PropertyArea;
			parameters[32].Value = model.AreaType;
			parameters[33].Value = model.YardArea;
			parameters[34].Value = model.DelUser;
			parameters[35].Value = model.DelDate;
			parameters[36].Value = model.ResideType;
			parameters[37].Value = model.RoomSynchCode;
			parameters[38].Value = model.SynchFlag;
			parameters[39].Value = model.BedTypeID;
			parameters[40].Value = model.UseType;
			parameters[41].Value = model.IsFrozen;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Room_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_Room model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@RoomSign", SqlDbType.NVarChar,20),
					new SqlParameter("@RoomName", SqlDbType.NVarChar,200),
					new SqlParameter("@RegionSNum", SqlDbType.Int,4),
					new SqlParameter("@BuildSNum", SqlDbType.Int,4),
					new SqlParameter("@UnitSNum", SqlDbType.Int,4),
					new SqlParameter("@FloorSNum", SqlDbType.Int,4),
					new SqlParameter("@RoomSNum", SqlDbType.Int,4),
					new SqlParameter("@RoomModel", SqlDbType.NVarChar,20),
					new SqlParameter("@RoomType", SqlDbType.NVarChar,20),
					new SqlParameter("@PropertyRights", SqlDbType.NVarChar,20),
					new SqlParameter("@RoomTowards", SqlDbType.NVarChar,10),
					new SqlParameter("@BuildArea", SqlDbType.Decimal,9),
					new SqlParameter("@InteriorArea", SqlDbType.Decimal,9),
					new SqlParameter("@CommonArea", SqlDbType.Decimal,9),
					new SqlParameter("@RightsSign", SqlDbType.NVarChar,50),
					new SqlParameter("@PropertyUses", SqlDbType.NVarChar,20),
					new SqlParameter("@RoomState", SqlDbType.Int,4),
					new SqlParameter("@ChargeTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@UsesState", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@FloorHeight", SqlDbType.NVarChar,30),
					new SqlParameter("@BuildStructure", SqlDbType.NVarChar,30),
					new SqlParameter("@PoolRatio", SqlDbType.Decimal,9),
					new SqlParameter("@BearParameters", SqlDbType.NVarChar,30),
					new SqlParameter("@Renovation", SqlDbType.NVarChar,30),
					new SqlParameter("@Configuration", SqlDbType.NVarChar,80),
					new SqlParameter("@Advertising", SqlDbType.NVarChar,30),
					new SqlParameter("@IsSplitUnite", SqlDbType.SmallInt,2),
					new SqlParameter("@GardenArea", SqlDbType.Decimal,9),
					new SqlParameter("@PropertyArea", SqlDbType.Decimal,9),
					new SqlParameter("@AreaType", SqlDbType.SmallInt,2),
					new SqlParameter("@YardArea", SqlDbType.Decimal,9),
					new SqlParameter("@DelUser", SqlDbType.NVarChar,50),
					new SqlParameter("@DelDate", SqlDbType.DateTime),
					new SqlParameter("@ResideType", SqlDbType.SmallInt,2),
					new SqlParameter("@RoomSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@BedTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@UseType", SqlDbType.SmallInt,2),
					new SqlParameter("@IsFrozen", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.RoomID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.RoomSign;
			parameters[3].Value = model.RoomName;
			parameters[4].Value = model.RegionSNum;
			parameters[5].Value = model.BuildSNum;
			parameters[6].Value = model.UnitSNum;
			parameters[7].Value = model.FloorSNum;
			parameters[8].Value = model.RoomSNum;
			parameters[9].Value = model.RoomModel;
			parameters[10].Value = model.RoomType;
			parameters[11].Value = model.PropertyRights;
			parameters[12].Value = model.RoomTowards;
			parameters[13].Value = model.BuildArea;
			parameters[14].Value = model.InteriorArea;
			parameters[15].Value = model.CommonArea;
			parameters[16].Value = model.RightsSign;
			parameters[17].Value = model.PropertyUses;
			parameters[18].Value = model.RoomState;
			parameters[19].Value = model.ChargeTypeID;
			parameters[20].Value = model.UsesState;
			parameters[21].Value = model.IsDelete;
			parameters[22].Value = model.FloorHeight;
			parameters[23].Value = model.BuildStructure;
			parameters[24].Value = model.PoolRatio;
			parameters[25].Value = model.BearParameters;
			parameters[26].Value = model.Renovation;
			parameters[27].Value = model.Configuration;
			parameters[28].Value = model.Advertising;
			parameters[29].Value = model.IsSplitUnite;
			parameters[30].Value = model.GardenArea;
			parameters[31].Value = model.PropertyArea;
			parameters[32].Value = model.AreaType;
			parameters[33].Value = model.YardArea;
			parameters[34].Value = model.DelUser;
			parameters[35].Value = model.DelDate;
			parameters[36].Value = model.ResideType;
			parameters[37].Value = model.RoomSynchCode;
			parameters[38].Value = model.SynchFlag;
			parameters[39].Value = model.BedTypeID;
			parameters[40].Value = model.UseType;
			parameters[41].Value = model.IsFrozen;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Room_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long RoomID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RoomID", SqlDbType.BigInt)};
			parameters[0].Value = RoomID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Room_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_Room GetModel(long RoomID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@RoomID", SqlDbType.BigInt)};
			parameters[0].Value = RoomID;

			MobileSoft.Model.HSPR.Tb_HSPR_Room model=new MobileSoft.Model.HSPR.Tb_HSPR_Room();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Room_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["RoomID"].ToString()!="")
				{
					model.RoomID=long.Parse(ds.Tables[0].Rows[0]["RoomID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				model.RoomSign=ds.Tables[0].Rows[0]["RoomSign"].ToString();
				model.RoomName=ds.Tables[0].Rows[0]["RoomName"].ToString();
				if(ds.Tables[0].Rows[0]["RegionSNum"].ToString()!="")
				{
					model.RegionSNum=int.Parse(ds.Tables[0].Rows[0]["RegionSNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BuildSNum"].ToString()!="")
				{
					model.BuildSNum=int.Parse(ds.Tables[0].Rows[0]["BuildSNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UnitSNum"].ToString()!="")
				{
					model.UnitSNum=int.Parse(ds.Tables[0].Rows[0]["UnitSNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FloorSNum"].ToString()!="")
				{
					model.FloorSNum=int.Parse(ds.Tables[0].Rows[0]["FloorSNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RoomSNum"].ToString()!="")
				{
					model.RoomSNum=int.Parse(ds.Tables[0].Rows[0]["RoomSNum"].ToString());
				}
				model.RoomModel=ds.Tables[0].Rows[0]["RoomModel"].ToString();
				model.RoomType=ds.Tables[0].Rows[0]["RoomType"].ToString();
				model.PropertyRights=ds.Tables[0].Rows[0]["PropertyRights"].ToString();
				model.RoomTowards=ds.Tables[0].Rows[0]["RoomTowards"].ToString();
				if(ds.Tables[0].Rows[0]["BuildArea"].ToString()!="")
				{
					model.BuildArea=decimal.Parse(ds.Tables[0].Rows[0]["BuildArea"].ToString());
				}
				if(ds.Tables[0].Rows[0]["InteriorArea"].ToString()!="")
				{
					model.InteriorArea=decimal.Parse(ds.Tables[0].Rows[0]["InteriorArea"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommonArea"].ToString()!="")
				{
					model.CommonArea=decimal.Parse(ds.Tables[0].Rows[0]["CommonArea"].ToString());
				}
				model.RightsSign=ds.Tables[0].Rows[0]["RightsSign"].ToString();
				model.PropertyUses=ds.Tables[0].Rows[0]["PropertyUses"].ToString();
				if(ds.Tables[0].Rows[0]["RoomState"].ToString()!="")
				{
					model.RoomState=int.Parse(ds.Tables[0].Rows[0]["RoomState"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChargeTypeID"].ToString()!="")
				{
					model.ChargeTypeID=long.Parse(ds.Tables[0].Rows[0]["ChargeTypeID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UsesState"].ToString()!="")
				{
					model.UsesState=int.Parse(ds.Tables[0].Rows[0]["UsesState"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				model.FloorHeight=ds.Tables[0].Rows[0]["FloorHeight"].ToString();
				model.BuildStructure=ds.Tables[0].Rows[0]["BuildStructure"].ToString();
				if(ds.Tables[0].Rows[0]["PoolRatio"].ToString()!="")
				{
					model.PoolRatio=decimal.Parse(ds.Tables[0].Rows[0]["PoolRatio"].ToString());
				}
				model.BearParameters=ds.Tables[0].Rows[0]["BearParameters"].ToString();
				model.Renovation=ds.Tables[0].Rows[0]["Renovation"].ToString();
				model.Configuration=ds.Tables[0].Rows[0]["Configuration"].ToString();
				model.Advertising=ds.Tables[0].Rows[0]["Advertising"].ToString();
				if(ds.Tables[0].Rows[0]["IsSplitUnite"].ToString()!="")
				{
					model.IsSplitUnite=int.Parse(ds.Tables[0].Rows[0]["IsSplitUnite"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GardenArea"].ToString()!="")
				{
					model.GardenArea=decimal.Parse(ds.Tables[0].Rows[0]["GardenArea"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PropertyArea"].ToString()!="")
				{
					model.PropertyArea=decimal.Parse(ds.Tables[0].Rows[0]["PropertyArea"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AreaType"].ToString()!="")
				{
					model.AreaType=int.Parse(ds.Tables[0].Rows[0]["AreaType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["YardArea"].ToString()!="")
				{
					model.YardArea=decimal.Parse(ds.Tables[0].Rows[0]["YardArea"].ToString());
				}
				model.DelUser=ds.Tables[0].Rows[0]["DelUser"].ToString();
				if(ds.Tables[0].Rows[0]["DelDate"].ToString()!="")
				{
					model.DelDate=DateTime.Parse(ds.Tables[0].Rows[0]["DelDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ResideType"].ToString()!="")
				{
					model.ResideType=int.Parse(ds.Tables[0].Rows[0]["ResideType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RoomSynchCode"].ToString()!="")
				{
					model.RoomSynchCode=new Guid(ds.Tables[0].Rows[0]["RoomSynchCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SynchFlag"].ToString()!="")
				{
					model.SynchFlag=int.Parse(ds.Tables[0].Rows[0]["SynchFlag"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BedTypeID"].ToString()!="")
				{
					model.BedTypeID=long.Parse(ds.Tables[0].Rows[0]["BedTypeID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UseType"].ToString()!="")
				{
					model.UseType=int.Parse(ds.Tables[0].Rows[0]["UseType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsFrozen"].ToString()!="")
				{
					model.IsFrozen=int.Parse(ds.Tables[0].Rows[0]["IsFrozen"].ToString());
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
			strSql.Append("select RoomID,CommID,RoomSign,RoomName,RegionSNum,BuildSNum,UnitSNum,FloorSNum,RoomSNum,RoomModel,RoomType,PropertyRights,RoomTowards,BuildArea,InteriorArea,CommonArea,RightsSign,PropertyUses,RoomState,ChargeTypeID,UsesState,IsDelete,FloorHeight,BuildStructure,PoolRatio,BearParameters,Renovation,Configuration,Advertising,IsSplitUnite,GardenArea,PropertyArea,AreaType,YardArea,DelUser,DelDate,ResideType,RoomSynchCode,SynchFlag,BedTypeID,UseType,IsFrozen ");
            strSql.Append(" FROM View_HSPR_Room_Filter ");
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
                  strSql.Append(" FROM View_HSPR_Room_Filter ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_Room WHERE 1=1 " + StrCondition;
			parameters[6].Value = "RoomID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

