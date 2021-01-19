using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_IncidentAccept。
	/// </summary>
	public class Dal_Tb_HSPR_IncidentAccept
	{
		public Dal_Tb_HSPR_IncidentAccept()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法


		/// <summary>
		///  增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_IncidentAccept model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IncidentID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@TypeID", SqlDbType.NVarChar,4000),
					new SqlParameter("@IncidentNum", SqlDbType.NVarChar,20),
					new SqlParameter("@IncidentPlace", SqlDbType.NVarChar,20),
					new SqlParameter("@IncidentMan", SqlDbType.NVarChar,20),
					new SqlParameter("@IncidentDate", SqlDbType.DateTime),
					new SqlParameter("@IncidentMode", SqlDbType.NVarChar,20),
					new SqlParameter("@DealLimit", SqlDbType.Int,4),
					new SqlParameter("@IncidentContent", SqlDbType.NVarChar,1000),
					new SqlParameter("@ReserveDate", SqlDbType.DateTime),
					new SqlParameter("@ReserveLimit", SqlDbType.Int,4),
					new SqlParameter("@Phone", SqlDbType.NVarChar,50),
					new SqlParameter("@AdmiMan", SqlDbType.NVarChar,20),
					new SqlParameter("@AdmiDate", SqlDbType.DateTime),
					new SqlParameter("@DispType", SqlDbType.SmallInt,2),
					new SqlParameter("@DispMan", SqlDbType.NVarChar,20),
					new SqlParameter("@DispDate", SqlDbType.DateTime),
					new SqlParameter("@DealMan", SqlDbType.NVarChar,200),
					new SqlParameter("@CoordinateNum", SqlDbType.NVarChar,20),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@MainStartDate", SqlDbType.DateTime),
					new SqlParameter("@MainEndDate", SqlDbType.DateTime),
					new SqlParameter("@DealSituation", SqlDbType.NVarChar,100),
					new SqlParameter("@CustComments", SqlDbType.NVarChar,50),
					new SqlParameter("@ServiceQuality", SqlDbType.NVarChar,50),
					new SqlParameter("@ArticlesFacilities", SqlDbType.NVarChar,50),
					new SqlParameter("@DealState", SqlDbType.SmallInt,2),
					new SqlParameter("@IncidentMemo", SqlDbType.NVarChar,100),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@Reasons", SqlDbType.NVarChar,200),
					new SqlParameter("@RegionalID", SqlDbType.BigInt,8),
					new SqlParameter("@DeleteReasons", SqlDbType.NVarChar,200),
					new SqlParameter("@DeleteDate", SqlDbType.DateTime),
					new SqlParameter("@TypeCode", SqlDbType.NVarChar,4000),
					new SqlParameter("@Signatory", SqlDbType.NVarChar,50),
					new SqlParameter("@IsStatistics", SqlDbType.SmallInt,2),
					new SqlParameter("@FinishUser", SqlDbType.NVarChar,20),
					new SqlParameter("@DueAmount", SqlDbType.Decimal,9),
					new SqlParameter("@IsTell", SqlDbType.SmallInt,2),
					new SqlParameter("@DeviceID", SqlDbType.BigInt,8),
					new SqlParameter("@PrintTime", SqlDbType.DateTime),
					new SqlParameter("@PrintCount", SqlDbType.Int,4),
					new SqlParameter("@PrintUserName", SqlDbType.NVarChar,20),
					new SqlParameter("@IsReceipt", SqlDbType.Int,4),
					new SqlParameter("@ReceiptUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@LocationID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.IncidentID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CustID;
			parameters[3].Value = model.RoomID;
			parameters[4].Value = model.TypeID;
			parameters[5].Value = model.IncidentNum;
			parameters[6].Value = model.IncidentPlace;
			parameters[7].Value = model.IncidentMan;
			parameters[8].Value = model.IncidentDate;
			parameters[9].Value = model.IncidentMode;
			parameters[10].Value = model.DealLimit;
			parameters[11].Value = model.IncidentContent;
			parameters[12].Value = model.ReserveDate;
			parameters[13].Value = model.ReserveLimit;
			parameters[14].Value = model.Phone;
			parameters[15].Value = model.AdmiMan;
			parameters[16].Value = model.AdmiDate;
			parameters[17].Value = model.DispType;
			parameters[18].Value = model.DispMan;
			parameters[19].Value = model.DispDate;
			parameters[20].Value = model.DealMan;
			parameters[21].Value = model.CoordinateNum;
			parameters[22].Value = model.EndDate;
			parameters[23].Value = model.MainStartDate;
			parameters[24].Value = model.MainEndDate;
			parameters[25].Value = model.DealSituation;
			parameters[26].Value = model.CustComments;
			parameters[27].Value = model.ServiceQuality;
			parameters[28].Value = model.ArticlesFacilities;
			parameters[29].Value = model.DealState;
			parameters[30].Value = model.IncidentMemo;
			parameters[31].Value = model.IsDelete;
			parameters[32].Value = model.Reasons;
			parameters[33].Value = model.RegionalID;
			parameters[34].Value = model.DeleteReasons;
			parameters[35].Value = model.DeleteDate;
			parameters[36].Value = model.TypeCode;
			parameters[37].Value = model.Signatory;
			parameters[38].Value = model.IsStatistics;
			parameters[39].Value = model.FinishUser;
			parameters[40].Value = model.DueAmount;
			parameters[41].Value = model.IsTell;
			parameters[42].Value = model.DeviceID;
			parameters[43].Value = model.PrintTime;
			parameters[44].Value = model.PrintCount;
			parameters[45].Value = model.PrintUserName;
			parameters[46].Value = model.IsReceipt;
			parameters[47].Value = model.ReceiptUserName;
			parameters[48].Value = model.LocationID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentAccept_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_IncidentAccept model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IncidentID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@TypeID", SqlDbType.NVarChar,4000),
					new SqlParameter("@IncidentNum", SqlDbType.NVarChar,20),
					new SqlParameter("@IncidentPlace", SqlDbType.NVarChar,20),
					new SqlParameter("@IncidentMan", SqlDbType.NVarChar,20),
					new SqlParameter("@IncidentDate", SqlDbType.DateTime),
					new SqlParameter("@IncidentMode", SqlDbType.NVarChar,20),
					new SqlParameter("@DealLimit", SqlDbType.Int,4),
					new SqlParameter("@IncidentContent", SqlDbType.NVarChar,1000),
					new SqlParameter("@ReserveDate", SqlDbType.DateTime),
					new SqlParameter("@ReserveLimit", SqlDbType.Int,4),
					new SqlParameter("@Phone", SqlDbType.NVarChar,50),
					new SqlParameter("@AdmiMan", SqlDbType.NVarChar,20),
					new SqlParameter("@AdmiDate", SqlDbType.DateTime),
					new SqlParameter("@DispType", SqlDbType.SmallInt,2),
					new SqlParameter("@DispMan", SqlDbType.NVarChar,20),
					new SqlParameter("@DispDate", SqlDbType.DateTime),
					new SqlParameter("@DealMan", SqlDbType.NVarChar,200),
					new SqlParameter("@CoordinateNum", SqlDbType.NVarChar,20),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@MainStartDate", SqlDbType.DateTime),
					new SqlParameter("@MainEndDate", SqlDbType.DateTime),
					new SqlParameter("@DealSituation", SqlDbType.NVarChar,100),
					new SqlParameter("@CustComments", SqlDbType.NVarChar,50),
					new SqlParameter("@ServiceQuality", SqlDbType.NVarChar,50),
					new SqlParameter("@ArticlesFacilities", SqlDbType.NVarChar,50),
					new SqlParameter("@DealState", SqlDbType.SmallInt,2),
					new SqlParameter("@IncidentMemo", SqlDbType.NVarChar,100),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@Reasons", SqlDbType.NVarChar,200),
					new SqlParameter("@RegionalID", SqlDbType.BigInt,8),
					new SqlParameter("@DeleteReasons", SqlDbType.NVarChar,200),
					new SqlParameter("@DeleteDate", SqlDbType.DateTime),
					new SqlParameter("@TypeCode", SqlDbType.NVarChar,4000),
					new SqlParameter("@Signatory", SqlDbType.NVarChar,50),
					new SqlParameter("@IsStatistics", SqlDbType.SmallInt,2),
					new SqlParameter("@FinishUser", SqlDbType.NVarChar,20),
					new SqlParameter("@DueAmount", SqlDbType.Decimal,9),
					new SqlParameter("@IsTell", SqlDbType.SmallInt,2),
					new SqlParameter("@DeviceID", SqlDbType.BigInt,8),
					new SqlParameter("@PrintTime", SqlDbType.DateTime),
					new SqlParameter("@PrintCount", SqlDbType.Int,4),
					new SqlParameter("@PrintUserName", SqlDbType.NVarChar,20),
					new SqlParameter("@IsReceipt", SqlDbType.Int,4),
					new SqlParameter("@ReceiptUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@LocationID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.IncidentID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CustID;
			parameters[3].Value = model.RoomID;
			parameters[4].Value = model.TypeID;
			parameters[5].Value = model.IncidentNum;
			parameters[6].Value = model.IncidentPlace;
			parameters[7].Value = model.IncidentMan;
			parameters[8].Value = model.IncidentDate;
			parameters[9].Value = model.IncidentMode;
			parameters[10].Value = model.DealLimit;
			parameters[11].Value = model.IncidentContent;
			parameters[12].Value = model.ReserveDate;
			parameters[13].Value = model.ReserveLimit;
			parameters[14].Value = model.Phone;
			parameters[15].Value = model.AdmiMan;
			parameters[16].Value = model.AdmiDate;
			parameters[17].Value = model.DispType;
			parameters[18].Value = model.DispMan;
			parameters[19].Value = model.DispDate;
			parameters[20].Value = model.DealMan;
			parameters[21].Value = model.CoordinateNum;
			parameters[22].Value = model.EndDate;
			parameters[23].Value = model.MainStartDate;
			parameters[24].Value = model.MainEndDate;
			parameters[25].Value = model.DealSituation;
			parameters[26].Value = model.CustComments;
			parameters[27].Value = model.ServiceQuality;
			parameters[28].Value = model.ArticlesFacilities;
			parameters[29].Value = model.DealState;
			parameters[30].Value = model.IncidentMemo;
			parameters[31].Value = model.IsDelete;
			parameters[32].Value = model.Reasons;
			parameters[33].Value = model.RegionalID;
			parameters[34].Value = model.DeleteReasons;
			parameters[35].Value = model.DeleteDate;
			parameters[36].Value = model.TypeCode;
			parameters[37].Value = model.Signatory;
			parameters[38].Value = model.IsStatistics;
			parameters[39].Value = model.FinishUser;
			parameters[40].Value = model.DueAmount;
			parameters[41].Value = model.IsTell;
			parameters[42].Value = model.DeviceID;
			parameters[43].Value = model.PrintTime;
			parameters[44].Value = model.PrintCount;
			parameters[45].Value = model.PrintUserName;
			parameters[46].Value = model.IsReceipt;
			parameters[47].Value = model.ReceiptUserName;
			parameters[48].Value = model.LocationID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentAccept_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete()
		{
			int rowsAffected;
			SqlParameter[] parameters = {
};

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentAccept_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_IncidentAccept GetModel()
		{
			SqlParameter[] parameters = {
};

			MobileSoft.Model.HSPR.Tb_HSPR_IncidentAccept model=new MobileSoft.Model.HSPR.Tb_HSPR_IncidentAccept();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentAccept_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["IncidentID"].ToString()!="")
				{
					model.IncidentID=long.Parse(ds.Tables[0].Rows[0]["IncidentID"].ToString());
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
				model.TypeID=ds.Tables[0].Rows[0]["TypeID"].ToString();
				model.IncidentNum=ds.Tables[0].Rows[0]["IncidentNum"].ToString();
				model.IncidentPlace=ds.Tables[0].Rows[0]["IncidentPlace"].ToString();
				model.IncidentMan=ds.Tables[0].Rows[0]["IncidentMan"].ToString();
				if(ds.Tables[0].Rows[0]["IncidentDate"].ToString()!="")
				{
					model.IncidentDate=DateTime.Parse(ds.Tables[0].Rows[0]["IncidentDate"].ToString());
				}
				model.IncidentMode=ds.Tables[0].Rows[0]["IncidentMode"].ToString();
				if(ds.Tables[0].Rows[0]["DealLimit"].ToString()!="")
				{
					model.DealLimit=int.Parse(ds.Tables[0].Rows[0]["DealLimit"].ToString());
				}
				model.IncidentContent=ds.Tables[0].Rows[0]["IncidentContent"].ToString();
				if(ds.Tables[0].Rows[0]["ReserveDate"].ToString()!="")
				{
					model.ReserveDate=DateTime.Parse(ds.Tables[0].Rows[0]["ReserveDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReserveLimit"].ToString()!="")
				{
					model.ReserveLimit=int.Parse(ds.Tables[0].Rows[0]["ReserveLimit"].ToString());
				}
				model.Phone=ds.Tables[0].Rows[0]["Phone"].ToString();
				model.AdmiMan=ds.Tables[0].Rows[0]["AdmiMan"].ToString();
				if(ds.Tables[0].Rows[0]["AdmiDate"].ToString()!="")
				{
					model.AdmiDate=DateTime.Parse(ds.Tables[0].Rows[0]["AdmiDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DispType"].ToString()!="")
				{
					model.DispType=int.Parse(ds.Tables[0].Rows[0]["DispType"].ToString());
				}
				model.DispMan=ds.Tables[0].Rows[0]["DispMan"].ToString();
				if(ds.Tables[0].Rows[0]["DispDate"].ToString()!="")
				{
					model.DispDate=DateTime.Parse(ds.Tables[0].Rows[0]["DispDate"].ToString());
				}
				model.DealMan=ds.Tables[0].Rows[0]["DealMan"].ToString();
				model.CoordinateNum=ds.Tables[0].Rows[0]["CoordinateNum"].ToString();
				if(ds.Tables[0].Rows[0]["EndDate"].ToString()!="")
				{
					model.EndDate=DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MainStartDate"].ToString()!="")
				{
					model.MainStartDate=DateTime.Parse(ds.Tables[0].Rows[0]["MainStartDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MainEndDate"].ToString()!="")
				{
					model.MainEndDate=DateTime.Parse(ds.Tables[0].Rows[0]["MainEndDate"].ToString());
				}
				model.DealSituation=ds.Tables[0].Rows[0]["DealSituation"].ToString();
				model.CustComments=ds.Tables[0].Rows[0]["CustComments"].ToString();
				model.ServiceQuality=ds.Tables[0].Rows[0]["ServiceQuality"].ToString();
				model.ArticlesFacilities=ds.Tables[0].Rows[0]["ArticlesFacilities"].ToString();
				if(ds.Tables[0].Rows[0]["DealState"].ToString()!="")
				{
					model.DealState=int.Parse(ds.Tables[0].Rows[0]["DealState"].ToString());
				}
				model.IncidentMemo=ds.Tables[0].Rows[0]["IncidentMemo"].ToString();
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				model.Reasons=ds.Tables[0].Rows[0]["Reasons"].ToString();
				if(ds.Tables[0].Rows[0]["RegionalID"].ToString()!="")
				{
					model.RegionalID=long.Parse(ds.Tables[0].Rows[0]["RegionalID"].ToString());
				}
				model.DeleteReasons=ds.Tables[0].Rows[0]["DeleteReasons"].ToString();
				if(ds.Tables[0].Rows[0]["DeleteDate"].ToString()!="")
				{
					model.DeleteDate=DateTime.Parse(ds.Tables[0].Rows[0]["DeleteDate"].ToString());
				}
				model.TypeCode=ds.Tables[0].Rows[0]["TypeCode"].ToString();
				model.Signatory=ds.Tables[0].Rows[0]["Signatory"].ToString();
				if(ds.Tables[0].Rows[0]["IsStatistics"].ToString()!="")
				{
					model.IsStatistics=int.Parse(ds.Tables[0].Rows[0]["IsStatistics"].ToString());
				}
				model.FinishUser=ds.Tables[0].Rows[0]["FinishUser"].ToString();
				if(ds.Tables[0].Rows[0]["DueAmount"].ToString()!="")
				{
					model.DueAmount=decimal.Parse(ds.Tables[0].Rows[0]["DueAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsTell"].ToString()!="")
				{
					model.IsTell=int.Parse(ds.Tables[0].Rows[0]["IsTell"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DeviceID"].ToString()!="")
				{
					model.DeviceID=long.Parse(ds.Tables[0].Rows[0]["DeviceID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PrintTime"].ToString()!="")
				{
					model.PrintTime=DateTime.Parse(ds.Tables[0].Rows[0]["PrintTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PrintCount"].ToString()!="")
				{
					model.PrintCount=int.Parse(ds.Tables[0].Rows[0]["PrintCount"].ToString());
				}
				model.PrintUserName=ds.Tables[0].Rows[0]["PrintUserName"].ToString();
				if(ds.Tables[0].Rows[0]["IsReceipt"].ToString()!="")
				{
					model.IsReceipt=int.Parse(ds.Tables[0].Rows[0]["IsReceipt"].ToString());
				}
				model.ReceiptUserName=ds.Tables[0].Rows[0]["ReceiptUserName"].ToString();
				if(ds.Tables[0].Rows[0]["LocationID"].ToString()!="")
				{
					model.LocationID=long.Parse(ds.Tables[0].Rows[0]["LocationID"].ToString());
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
            strSql.Append(" FROM view_HSPR_IncidentSeach_Filter ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 获得数据列表条数
        /// </summary>
        public DataSet GetListCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select COUNT(*) AS TotalCount ");
            strSql.Append(" FROM View_HSPR_IncidentAccept_Filter ");
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
			strSql.Append(" * ");
            strSql.Append(" FROM View_HSPR_IncidentAccept_Filter ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_IncidentAccept WHERE 1=1 " + StrCondition;
			parameters[6].Value = "";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法

            //户内报事
        public void IncidentAcceptPhoneInsert(int CommID, long CustID, long RoomID, string IncidentDate, string IncidentContent, string IncidentMan, string ReserveDate, string Phone, string IncidentImgs,string TypeID)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@CommID", SqlDbType.BigInt),
                              new SqlParameter("@CustID", SqlDbType.BigInt),
                              new SqlParameter("@RoomID", SqlDbType.BigInt),
                              new SqlParameter("@IncidentDate", SqlDbType.VarChar,3999),
                              new SqlParameter("@IncidentContent", SqlDbType.VarChar,3999),
                              new SqlParameter("@IncidentMan", SqlDbType.VarChar,3999),
                              new SqlParameter("@ReserveDate", SqlDbType.VarChar,3999),
                              new SqlParameter("@Phone", SqlDbType.VarChar,3999),
                              new SqlParameter("@IncidentImgs", SqlDbType.VarChar,3999),
                              new SqlParameter("@TypeID", SqlDbType.VarChar,3999)
                                              };
                  parameters[0].Value = CommID;
                  parameters[1].Value = CustID;
                  parameters[2].Value = RoomID;
                  parameters[3].Value = IncidentDate;
                  parameters[4].Value = IncidentContent;
                  parameters[5].Value = IncidentMan;
                  parameters[6].Value = ReserveDate;
                  parameters[7].Value = Phone;
                  parameters[8].Value = IncidentImgs;
                  parameters[9].Value = TypeID;
                  DbHelperSQL.RunProcedure("Proc_HSPR_IncidentAccept_PhoneInsert", parameters);
            }

            //公区报事
        public void PubIncidentAcceptPhoneInsert(int CommID, long RegionalID, string IncidentDate, string IncidentMan, string IncidentContent, string ReserveDate, string Phone, string IncidentImgs,string TypeID,string CustID)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@CommID", SqlDbType.BigInt),
                              new SqlParameter("@RegionalID", SqlDbType.BigInt),
                              new SqlParameter("@IncidentDate", SqlDbType.VarChar,3999),
                              new SqlParameter("@IncidentMan", SqlDbType.VarChar,3999),
                              new SqlParameter("@IncidentContent", SqlDbType.VarChar,3999),
                              new SqlParameter("@ReserveDate", SqlDbType.VarChar,3999),
                              new SqlParameter("@Phone", SqlDbType.VarChar,3999),
                              new SqlParameter("@IncidentImgs", SqlDbType.VarChar,3999),
                              new SqlParameter("@TypeID", SqlDbType.VarChar,3999),
                              new SqlParameter("@CustID", SqlDbType.VarChar,3999)
                                              };
                  parameters[0].Value = CommID;
                  parameters[1].Value = RegionalID;
                  parameters[2].Value = IncidentDate;
                  parameters[3].Value = IncidentMan;
                  parameters[4].Value = IncidentContent;
                  parameters[5].Value = ReserveDate;
                  parameters[6].Value = Phone;
                  parameters[7].Value = IncidentImgs;
                  parameters[8].Value = TypeID;
                  parameters[9].Value = CustID;
                  DbHelperSQL.RunProcedure("Proc_HSPR_PubIncidentAccept_PhoneInsert", parameters);
            }

            //报事处理
            public void PubIncidentPhoneUpdate(int CommID, long IncidentID, string DealContent, string ReceiptUserName,string CustIdea,int IsOVer)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@CommID", SqlDbType.BigInt),
                              new SqlParameter("@IncidentID", SqlDbType.BigInt),
                              new SqlParameter("@ReceiptUserName", SqlDbType.VarChar,3999),
                              new SqlParameter("@DealContent", SqlDbType.VarChar,3999),
                              new SqlParameter("@CustIdea", SqlDbType.VarChar,3999),
                              new SqlParameter("@IsOVer", SqlDbType.Int)
                                              };
                  parameters[0].Value = CommID;
                  parameters[1].Value = IncidentID;
                  parameters[2].Value = ReceiptUserName;
                  parameters[3].Value = DealContent;
                  parameters[4].Value = CustIdea;
                  parameters[5].Value = IsOVer;

                  DbHelperSQL.RunProcedure("Proc_HSPR_PubIncident_MobilePhoneUpdate", parameters);
            }
	}
}

