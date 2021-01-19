using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_Community。
	/// </summary>
	public class Dal_Tb_HSPR_Community
	{
		public Dal_Tb_HSPR_Community()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("CommID", "Tb_HSPR_Community"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CommID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CommID", SqlDbType.Int,4)};
			parameters[0].Value = CommID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Community_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_Community model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@CorpID", SqlDbType.Int,4),
					new SqlParameter("@CommName", SqlDbType.NVarChar,50),
					new SqlParameter("@CommAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@Province", SqlDbType.NVarChar,20),
					new SqlParameter("@City", SqlDbType.NVarChar,20),
					new SqlParameter("@Borough", SqlDbType.NVarChar,20),
					new SqlParameter("@Street", SqlDbType.NVarChar,50),
					new SqlParameter("@GateSign", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpGroupCode", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpRegionCode", SqlDbType.NVarChar,20),
					new SqlParameter("@CommSpell", SqlDbType.NVarChar,20),
					new SqlParameter("@RegDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@UseBeginDate", SqlDbType.DateTime),
					new SqlParameter("@UseEndDate", SqlDbType.DateTime),
					new SqlParameter("@CommKind", SqlDbType.NVarChar,20),
					new SqlParameter("@ManageTime", SqlDbType.DateTime),
					new SqlParameter("@ManageKind", SqlDbType.NVarChar,20),
					new SqlParameter("@CommSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@ContractBeginDate", SqlDbType.DateTime),
					new SqlParameter("@ContractEndDate", SqlDbType.DateTime),
					new SqlParameter("@SysStartDate", SqlDbType.DateTime),
					new SqlParameter("@SysLogDate", SqlDbType.DateTime),
					new SqlParameter("@IsFees", SqlDbType.SmallInt,2),
					new SqlParameter("@Memo", SqlDbType.NVarChar,2000),
					new SqlParameter("@CommType", SqlDbType.SmallInt,2),
					new SqlParameter("@Num", SqlDbType.Int,4),
					new SqlParameter("@CommunityName", SqlDbType.NVarChar,50),
					new SqlParameter("@CommunityCode", SqlDbType.NVarChar,40)};
			parameters[0].Value = model.CommID;
			parameters[1].Value = model.BranchID;
			parameters[2].Value = model.CorpID;
			parameters[3].Value = model.CommName;
			parameters[4].Value = model.CommAddress;
			parameters[5].Value = model.Province;
			parameters[6].Value = model.City;
			parameters[7].Value = model.Borough;
			parameters[8].Value = model.Street;
			parameters[9].Value = model.GateSign;
			parameters[10].Value = model.CorpGroupCode;
			parameters[11].Value = model.CorpRegionCode;
			parameters[12].Value = model.CommSpell;
			parameters[13].Value = model.RegDate;
			parameters[14].Value = model.IsDelete;
			parameters[15].Value = model.OrganCode;
			parameters[16].Value = model.UseBeginDate;
			parameters[17].Value = model.UseEndDate;
			parameters[18].Value = model.CommKind;
			parameters[19].Value = model.ManageTime;
			parameters[20].Value = model.ManageKind;
			parameters[21].Value = model.CommSynchCode;
			parameters[22].Value = model.SynchFlag;
			parameters[23].Value = model.ContractBeginDate;
			parameters[24].Value = model.ContractEndDate;
			parameters[25].Value = model.SysStartDate;
			parameters[26].Value = model.SysLogDate;
			parameters[27].Value = model.IsFees;
			parameters[28].Value = model.Memo;
			parameters[29].Value = model.CommType;
			parameters[30].Value = model.Num;
			parameters[31].Value = model.CommunityName;
			parameters[32].Value = model.CommunityCode;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Community_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_Community model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@CorpID", SqlDbType.Int,4),
					new SqlParameter("@CommName", SqlDbType.NVarChar,50),
					new SqlParameter("@CommAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@Province", SqlDbType.NVarChar,20),
					new SqlParameter("@City", SqlDbType.NVarChar,20),
					new SqlParameter("@Borough", SqlDbType.NVarChar,20),
					new SqlParameter("@Street", SqlDbType.NVarChar,50),
					new SqlParameter("@GateSign", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpGroupCode", SqlDbType.NVarChar,20),
					new SqlParameter("@CorpRegionCode", SqlDbType.NVarChar,20),
					new SqlParameter("@CommSpell", SqlDbType.NVarChar,20),
					new SqlParameter("@RegDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@UseBeginDate", SqlDbType.DateTime),
					new SqlParameter("@UseEndDate", SqlDbType.DateTime),
					new SqlParameter("@CommKind", SqlDbType.NVarChar,20),
					new SqlParameter("@ManageTime", SqlDbType.DateTime),
					new SqlParameter("@ManageKind", SqlDbType.NVarChar,20),
					new SqlParameter("@CommSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@ContractBeginDate", SqlDbType.DateTime),
					new SqlParameter("@ContractEndDate", SqlDbType.DateTime),
					new SqlParameter("@SysStartDate", SqlDbType.DateTime),
					new SqlParameter("@SysLogDate", SqlDbType.DateTime),
					new SqlParameter("@IsFees", SqlDbType.SmallInt,2),
					new SqlParameter("@Memo", SqlDbType.NVarChar,2000),
					new SqlParameter("@CommType", SqlDbType.SmallInt,2),
					new SqlParameter("@Num", SqlDbType.Int,4),
					new SqlParameter("@CommunityName", SqlDbType.NVarChar,50),
					new SqlParameter("@CommunityCode", SqlDbType.NVarChar,40)};
			parameters[0].Value = model.CommID;
			parameters[1].Value = model.BranchID;
			parameters[2].Value = model.CorpID;
			parameters[3].Value = model.CommName;
			parameters[4].Value = model.CommAddress;
			parameters[5].Value = model.Province;
			parameters[6].Value = model.City;
			parameters[7].Value = model.Borough;
			parameters[8].Value = model.Street;
			parameters[9].Value = model.GateSign;
			parameters[10].Value = model.CorpGroupCode;
			parameters[11].Value = model.CorpRegionCode;
			parameters[12].Value = model.CommSpell;
			parameters[13].Value = model.RegDate;
			parameters[14].Value = model.IsDelete;
			parameters[15].Value = model.OrganCode;
			parameters[16].Value = model.UseBeginDate;
			parameters[17].Value = model.UseEndDate;
			parameters[18].Value = model.CommKind;
			parameters[19].Value = model.ManageTime;
			parameters[20].Value = model.ManageKind;
			parameters[21].Value = model.CommSynchCode;
			parameters[22].Value = model.SynchFlag;
			parameters[23].Value = model.ContractBeginDate;
			parameters[24].Value = model.ContractEndDate;
			parameters[25].Value = model.SysStartDate;
			parameters[26].Value = model.SysLogDate;
			parameters[27].Value = model.IsFees;
			parameters[28].Value = model.Memo;
			parameters[29].Value = model.CommType;
			parameters[30].Value = model.Num;
			parameters[31].Value = model.CommunityName;
			parameters[32].Value = model.CommunityCode;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Community_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int CommID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CommID", SqlDbType.Int,4)};
			parameters[0].Value = CommID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Community_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_Community GetModel(int CommID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@CommID", SqlDbType.Int,4)};
			parameters[0].Value = CommID;

			MobileSoft.Model.HSPR.Tb_HSPR_Community model=new MobileSoft.Model.HSPR.Tb_HSPR_Community();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Community_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BranchID"].ToString()!="")
				{
					model.BranchID=int.Parse(ds.Tables[0].Rows[0]["BranchID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CorpID"].ToString()!="")
				{
					model.CorpID=int.Parse(ds.Tables[0].Rows[0]["CorpID"].ToString());
				}
				model.CommName=ds.Tables[0].Rows[0]["CommName"].ToString();
				model.CommAddress=ds.Tables[0].Rows[0]["CommAddress"].ToString();
				model.Province=ds.Tables[0].Rows[0]["Province"].ToString();
				model.City=ds.Tables[0].Rows[0]["City"].ToString();
				model.Borough=ds.Tables[0].Rows[0]["Borough"].ToString();
				model.Street=ds.Tables[0].Rows[0]["Street"].ToString();
				model.GateSign=ds.Tables[0].Rows[0]["GateSign"].ToString();
				model.CorpGroupCode=ds.Tables[0].Rows[0]["CorpGroupCode"].ToString();
				model.CorpRegionCode=ds.Tables[0].Rows[0]["CorpRegionCode"].ToString();
				model.CommSpell=ds.Tables[0].Rows[0]["CommSpell"].ToString();
				if(ds.Tables[0].Rows[0]["RegDate"].ToString()!="")
				{
					model.RegDate=DateTime.Parse(ds.Tables[0].Rows[0]["RegDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				model.OrganCode=ds.Tables[0].Rows[0]["OrganCode"].ToString();
				if(ds.Tables[0].Rows[0]["UseBeginDate"].ToString()!="")
				{
					model.UseBeginDate=DateTime.Parse(ds.Tables[0].Rows[0]["UseBeginDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UseEndDate"].ToString()!="")
				{
					model.UseEndDate=DateTime.Parse(ds.Tables[0].Rows[0]["UseEndDate"].ToString());
				}
				model.CommKind=ds.Tables[0].Rows[0]["CommKind"].ToString();
				if(ds.Tables[0].Rows[0]["ManageTime"].ToString()!="")
				{
					model.ManageTime=DateTime.Parse(ds.Tables[0].Rows[0]["ManageTime"].ToString());
				}
				model.ManageKind=ds.Tables[0].Rows[0]["ManageKind"].ToString();
				if(ds.Tables[0].Rows[0]["CommSynchCode"].ToString()!="")
				{
					model.CommSynchCode=new Guid(ds.Tables[0].Rows[0]["CommSynchCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SynchFlag"].ToString()!="")
				{
					model.SynchFlag=int.Parse(ds.Tables[0].Rows[0]["SynchFlag"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ContractBeginDate"].ToString()!="")
				{
					model.ContractBeginDate=DateTime.Parse(ds.Tables[0].Rows[0]["ContractBeginDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ContractEndDate"].ToString()!="")
				{
					model.ContractEndDate=DateTime.Parse(ds.Tables[0].Rows[0]["ContractEndDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SysStartDate"].ToString()!="")
				{
					model.SysStartDate=DateTime.Parse(ds.Tables[0].Rows[0]["SysStartDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SysLogDate"].ToString()!="")
				{
					model.SysLogDate=DateTime.Parse(ds.Tables[0].Rows[0]["SysLogDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsFees"].ToString()!="")
				{
					model.IsFees=int.Parse(ds.Tables[0].Rows[0]["IsFees"].ToString());
				}
				model.Memo=ds.Tables[0].Rows[0]["Memo"].ToString();
				if(ds.Tables[0].Rows[0]["CommType"].ToString()!="")
				{
					model.CommType=int.Parse(ds.Tables[0].Rows[0]["CommType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Num"].ToString()!="")
				{
					model.Num=int.Parse(ds.Tables[0].Rows[0]["Num"].ToString());
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
			strSql.Append("select CommID,BranchID,CorpID,CommName,CommAddress,Province,City,Borough,Street,GateSign,CorpGroupCode,CorpRegionCode,CommSpell,RegDate,IsDelete,OrganCode,UseBeginDate,UseEndDate,CommKind,ManageTime,ManageKind,CommSynchCode,SynchFlag,ContractBeginDate,ContractEndDate,SysStartDate,SysLogDate,IsFees,Memo,CommType,Num,CommunityName,CommunityCode ");
			strSql.Append(" FROM Tb_HSPR_Community ");
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
			strSql.Append(" CommID,BranchID,CorpID,CommName,CommAddress,Province,City,Borough,Street,GateSign,CorpGroupCode,CorpRegionCode,CommSpell,RegDate,IsDelete,OrganCode,UseBeginDate,UseEndDate,CommKind,ManageTime,ManageKind,CommSynchCode,SynchFlag,ContractBeginDate,ContractEndDate,SysStartDate,SysLogDate,IsFees,Memo,CommType,Num,CommunityName,CommunityCode ");
			strSql.Append(" FROM Tb_HSPR_Community ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_Community WHERE 1=1 " + StrCondition;
			parameters[6].Value = "CommID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法


            public DataTable OrganGetEntryNodes(string UserCode)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@UserCode", SqlDbType.VarChar)
                                              };
                  parameters[0].Value = UserCode;

                  return DbHelperSQL.RunProcedure("Proc_Sys_Organ_GetEntryNodes", parameters, "RetDataSet").Tables[0];
            }
	}
}

