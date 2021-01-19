using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.System
{
	/// <summary>
	/// 数据访问类Dal_Tb_System_BusinessCorp。
	/// </summary>
	public class Dal_Tb_System_BusinessCorp
	{
		public Dal_Tb_System_BusinessCorp()
		{
			DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("BussId", "Tb_System_BusinessCorp"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long BussId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@BussId", SqlDbType.BigInt)};
			parameters[0].Value = BussId;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_System_BusinessCorp_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.System.Tb_System_BusinessCorp model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@RegBigType", SqlDbType.NVarChar,20),
					new SqlParameter("@RegSmallType", SqlDbType.NVarChar,20),
					new SqlParameter("@BussName", SqlDbType.NVarChar,100),
					new SqlParameter("@BussShortName", SqlDbType.NVarChar,20),
					new SqlParameter("@Province", SqlDbType.NVarChar,20),
					new SqlParameter("@City", SqlDbType.NVarChar,20),
					new SqlParameter("@Borough", SqlDbType.NVarChar,20),
					new SqlParameter("@LoginCode", SqlDbType.NVarChar,50),
					new SqlParameter("@LoginPassWD", SqlDbType.NVarChar,30),
					new SqlParameter("@BussAddress", SqlDbType.NVarChar,100),
					new SqlParameter("@BussZipCode", SqlDbType.NVarChar,10),
					new SqlParameter("@BussLinkMan", SqlDbType.NVarChar,20),
					new SqlParameter("@BussMobileTel", SqlDbType.NVarChar,50),
					new SqlParameter("@BussWorkedTel", SqlDbType.NVarChar,50),
					new SqlParameter("@BussEmail", SqlDbType.NVarChar,50),
					new SqlParameter("@BussWebName", SqlDbType.NVarChar,100),
					new SqlParameter("@BussQQ", SqlDbType.NVarChar,20),
					new SqlParameter("@BussWeiXin", SqlDbType.NVarChar,50),
					new SqlParameter("@LogoImgUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@MapImgUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@SysDir", SqlDbType.NVarChar,10),
					new SqlParameter("@SysVersion", SqlDbType.NVarChar,20),
					new SqlParameter("@RegDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@BussSNum", SqlDbType.Int,4),
					new SqlParameter("@BussSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@IsRecommend", SqlDbType.SmallInt,2),
					new SqlParameter("@RecommendIndex", SqlDbType.NVarChar,10),
					new SqlParameter("@RecommendTitle", SqlDbType.NVarChar,50),
					new SqlParameter("@RecommendContent", SqlDbType.NVarChar,500),
					new SqlParameter("@ImgLogo1", SqlDbType.NVarChar,200),
					new SqlParameter("@ImgLogo2", SqlDbType.NVarChar,200),
					new SqlParameter("@ImgLogo3", SqlDbType.NVarChar,200),
					new SqlParameter("@ImgLogo4", SqlDbType.NVarChar,200),
					new SqlParameter("@ImgLogo5", SqlDbType.NVarChar,200),
					new SqlParameter("@ImgLogo6", SqlDbType.NVarChar,200),
					new SqlParameter("@ImgLogo7", SqlDbType.NVarChar,200),
					new SqlParameter("@ImgLogo8", SqlDbType.NVarChar,200),
					new SqlParameter("@ImgLogo9", SqlDbType.NVarChar,200),
					new SqlParameter("@ImgLogo10", SqlDbType.NVarChar,200),
					new SqlParameter("@VisitCount", SqlDbType.Int,4)};
			parameters[0].Value = model.BussId;
			parameters[1].Value = model.RegBigType;
			parameters[2].Value = model.RegSmallType;
			parameters[3].Value = model.BussName;
			parameters[4].Value = model.BussShortName;
			parameters[5].Value = model.Province;
			parameters[6].Value = model.City;
			parameters[7].Value = model.Borough;
			parameters[8].Value = model.LoginCode;
			parameters[9].Value = model.LoginPassWD;
			parameters[10].Value = model.BussAddress;
			parameters[11].Value = model.BussZipCode;
			parameters[12].Value = model.BussLinkMan;
			parameters[13].Value = model.BussMobileTel;
			parameters[14].Value = model.BussWorkedTel;
			parameters[15].Value = model.BussEmail;
			parameters[16].Value = model.BussWebName;
			parameters[17].Value = model.BussQQ;
			parameters[18].Value = model.BussWeiXin;
			parameters[19].Value = model.LogoImgUrl;
			parameters[20].Value = model.MapImgUrl;
			parameters[21].Value = model.SysDir;
			parameters[22].Value = model.SysVersion;
			parameters[23].Value = model.RegDate;
			parameters[24].Value = model.IsDelete;
			parameters[25].Value = model.BussSNum;
			parameters[26].Value = model.BussSynchCode;
			parameters[27].Value = model.SynchFlag;
			parameters[28].Value = model.IsRecommend;
			parameters[29].Value = model.RecommendIndex;
			parameters[30].Value = model.RecommendTitle;
			parameters[31].Value = model.RecommendContent;
			parameters[32].Value = model.ImgLogo1;
			parameters[33].Value = model.ImgLogo2;
			parameters[34].Value = model.ImgLogo3;
			parameters[35].Value = model.ImgLogo4;
			parameters[36].Value = model.ImgLogo5;
			parameters[37].Value = model.ImgLogo6;
			parameters[38].Value = model.ImgLogo7;
			parameters[39].Value = model.ImgLogo8;
			parameters[40].Value = model.ImgLogo9;
			parameters[41].Value = model.ImgLogo10;
			parameters[42].Value = model.VisitCount;

			DbHelperSQL.RunProcedure("Proc_Tb_System_BusinessCorp_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_BusinessCorp model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@RegBigType", SqlDbType.NVarChar,20),
					new SqlParameter("@RegSmallType", SqlDbType.NVarChar,20),
					new SqlParameter("@BussName", SqlDbType.NVarChar,100),
					new SqlParameter("@BussShortName", SqlDbType.NVarChar,20),
					new SqlParameter("@Province", SqlDbType.NVarChar,20),
					new SqlParameter("@City", SqlDbType.NVarChar,20),
					new SqlParameter("@Borough", SqlDbType.NVarChar,20),
					new SqlParameter("@LoginCode", SqlDbType.NVarChar,50),
					new SqlParameter("@LoginPassWD", SqlDbType.NVarChar,30),
					new SqlParameter("@BussAddress", SqlDbType.NVarChar,100),
					new SqlParameter("@BussZipCode", SqlDbType.NVarChar,10),
					new SqlParameter("@BussLinkMan", SqlDbType.NVarChar,20),
					new SqlParameter("@BussMobileTel", SqlDbType.NVarChar,50),
					new SqlParameter("@BussWorkedTel", SqlDbType.NVarChar,50),
					new SqlParameter("@BussEmail", SqlDbType.NVarChar,50),
					new SqlParameter("@BussWebName", SqlDbType.NVarChar,100),
					new SqlParameter("@BussQQ", SqlDbType.NVarChar,20),
					new SqlParameter("@BussWeiXin", SqlDbType.NVarChar,50),
					new SqlParameter("@LogoImgUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@MapImgUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@SysDir", SqlDbType.NVarChar,10),
					new SqlParameter("@SysVersion", SqlDbType.NVarChar,20),
					new SqlParameter("@RegDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@BussSNum", SqlDbType.Int,4),
					new SqlParameter("@BussSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@IsRecommend", SqlDbType.SmallInt,2),
					new SqlParameter("@RecommendIndex", SqlDbType.NVarChar,10),
					new SqlParameter("@RecommendTitle", SqlDbType.NVarChar,50),
					new SqlParameter("@RecommendContent", SqlDbType.NVarChar,500),
					new SqlParameter("@ImgLogo1", SqlDbType.NVarChar,200),
					new SqlParameter("@ImgLogo2", SqlDbType.NVarChar,200),
					new SqlParameter("@ImgLogo3", SqlDbType.NVarChar,200),
					new SqlParameter("@ImgLogo4", SqlDbType.NVarChar,200),
					new SqlParameter("@ImgLogo5", SqlDbType.NVarChar,200),
					new SqlParameter("@ImgLogo6", SqlDbType.NVarChar,200),
					new SqlParameter("@ImgLogo7", SqlDbType.NVarChar,200),
					new SqlParameter("@ImgLogo8", SqlDbType.NVarChar,200),
					new SqlParameter("@ImgLogo9", SqlDbType.NVarChar,200),
					new SqlParameter("@ImgLogo10", SqlDbType.NVarChar,200),
					new SqlParameter("@VisitCount", SqlDbType.Int,4)};
			parameters[0].Value = model.BussId;
			parameters[1].Value = model.RegBigType;
			parameters[2].Value = model.RegSmallType;
			parameters[3].Value = model.BussName;
			parameters[4].Value = model.BussShortName;
			parameters[5].Value = model.Province;
			parameters[6].Value = model.City;
			parameters[7].Value = model.Borough;
			parameters[8].Value = model.LoginCode;
			parameters[9].Value = model.LoginPassWD;
			parameters[10].Value = model.BussAddress;
			parameters[11].Value = model.BussZipCode;
			parameters[12].Value = model.BussLinkMan;
			parameters[13].Value = model.BussMobileTel;
			parameters[14].Value = model.BussWorkedTel;
			parameters[15].Value = model.BussEmail;
			parameters[16].Value = model.BussWebName;
			parameters[17].Value = model.BussQQ;
			parameters[18].Value = model.BussWeiXin;
			parameters[19].Value = model.LogoImgUrl;
			parameters[20].Value = model.MapImgUrl;
			parameters[21].Value = model.SysDir;
			parameters[22].Value = model.SysVersion;
			parameters[23].Value = model.RegDate;
			parameters[24].Value = model.IsDelete;
			parameters[25].Value = model.BussSNum;
			parameters[26].Value = model.BussSynchCode;
			parameters[27].Value = model.SynchFlag;
			parameters[28].Value = model.IsRecommend;
			parameters[29].Value = model.RecommendIndex;
			parameters[30].Value = model.RecommendTitle;
			parameters[31].Value = model.RecommendContent;
			parameters[32].Value = model.ImgLogo1;
			parameters[33].Value = model.ImgLogo2;
			parameters[34].Value = model.ImgLogo3;
			parameters[35].Value = model.ImgLogo4;
			parameters[36].Value = model.ImgLogo5;
			parameters[37].Value = model.ImgLogo6;
			parameters[38].Value = model.ImgLogo7;
			parameters[39].Value = model.ImgLogo8;
			parameters[40].Value = model.ImgLogo9;
			parameters[41].Value = model.ImgLogo10;
			parameters[42].Value = model.VisitCount;

			DbHelperSQL.RunProcedure("Proc_Tb_System_BusinessCorp_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long BussId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@BussId", SqlDbType.BigInt)};
			parameters[0].Value = BussId;

			DbHelperSQL.RunProcedure("Proc_Tb_System_BusinessCorp_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_BusinessCorp GetModel(long BussId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@BussId", SqlDbType.BigInt)};
			parameters[0].Value = BussId;

			MobileSoft.Model.System.Tb_System_BusinessCorp model=new MobileSoft.Model.System.Tb_System_BusinessCorp();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_System_BusinessCorp_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["BussId"].ToString()!="")
				{
					model.BussId=long.Parse(ds.Tables[0].Rows[0]["BussId"].ToString());
				}
				model.RegBigType=ds.Tables[0].Rows[0]["RegBigType"].ToString();
				model.RegSmallType=ds.Tables[0].Rows[0]["RegSmallType"].ToString();
				model.BussName=ds.Tables[0].Rows[0]["BussName"].ToString();
				model.BussShortName=ds.Tables[0].Rows[0]["BussShortName"].ToString();
				model.Province=ds.Tables[0].Rows[0]["Province"].ToString();
				model.City=ds.Tables[0].Rows[0]["City"].ToString();
				model.Borough=ds.Tables[0].Rows[0]["Borough"].ToString();
				model.LoginCode=ds.Tables[0].Rows[0]["LoginCode"].ToString();
				model.LoginPassWD=ds.Tables[0].Rows[0]["LoginPassWD"].ToString();
				model.BussAddress=ds.Tables[0].Rows[0]["BussAddress"].ToString();
				model.BussZipCode=ds.Tables[0].Rows[0]["BussZipCode"].ToString();
				model.BussLinkMan=ds.Tables[0].Rows[0]["BussLinkMan"].ToString();
				model.BussMobileTel=ds.Tables[0].Rows[0]["BussMobileTel"].ToString();
				model.BussWorkedTel=ds.Tables[0].Rows[0]["BussWorkedTel"].ToString();
				model.BussEmail=ds.Tables[0].Rows[0]["BussEmail"].ToString();
				model.BussWebName=ds.Tables[0].Rows[0]["BussWebName"].ToString();
				model.BussQQ=ds.Tables[0].Rows[0]["BussQQ"].ToString();
				model.BussWeiXin=ds.Tables[0].Rows[0]["BussWeiXin"].ToString();
				model.LogoImgUrl=ds.Tables[0].Rows[0]["LogoImgUrl"].ToString();
				model.MapImgUrl=ds.Tables[0].Rows[0]["MapImgUrl"].ToString();
				model.SysDir=ds.Tables[0].Rows[0]["SysDir"].ToString();
				model.SysVersion=ds.Tables[0].Rows[0]["SysVersion"].ToString();
				if(ds.Tables[0].Rows[0]["RegDate"].ToString()!="")
				{
					model.RegDate=DateTime.Parse(ds.Tables[0].Rows[0]["RegDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BussSNum"].ToString()!="")
				{
					model.BussSNum=int.Parse(ds.Tables[0].Rows[0]["BussSNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BussSynchCode"].ToString()!="")
				{
					model.BussSynchCode=new Guid(ds.Tables[0].Rows[0]["BussSynchCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SynchFlag"].ToString()!="")
				{
					model.SynchFlag=int.Parse(ds.Tables[0].Rows[0]["SynchFlag"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsRecommend"].ToString()!="")
				{
					model.IsRecommend=int.Parse(ds.Tables[0].Rows[0]["IsRecommend"].ToString());
				}
				model.RecommendIndex=ds.Tables[0].Rows[0]["RecommendIndex"].ToString();
				model.RecommendTitle=ds.Tables[0].Rows[0]["RecommendTitle"].ToString();
				model.RecommendContent=ds.Tables[0].Rows[0]["RecommendContent"].ToString();
				model.ImgLogo1=ds.Tables[0].Rows[0]["ImgLogo1"].ToString();
				model.ImgLogo2=ds.Tables[0].Rows[0]["ImgLogo2"].ToString();
				model.ImgLogo3=ds.Tables[0].Rows[0]["ImgLogo3"].ToString();
				model.ImgLogo4=ds.Tables[0].Rows[0]["ImgLogo4"].ToString();
				model.ImgLogo5=ds.Tables[0].Rows[0]["ImgLogo5"].ToString();
				model.ImgLogo6=ds.Tables[0].Rows[0]["ImgLogo6"].ToString();
				model.ImgLogo7=ds.Tables[0].Rows[0]["ImgLogo7"].ToString();
				model.ImgLogo8=ds.Tables[0].Rows[0]["ImgLogo8"].ToString();
				model.ImgLogo9=ds.Tables[0].Rows[0]["ImgLogo9"].ToString();
				model.ImgLogo10=ds.Tables[0].Rows[0]["ImgLogo10"].ToString();
				if(ds.Tables[0].Rows[0]["VisitCount"].ToString()!="")
				{
					model.VisitCount=int.Parse(ds.Tables[0].Rows[0]["VisitCount"].ToString());
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
            strSql.Append(" FROM View_System_BusinessCorp_Filter ");
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
            strSql.Append(" FROM View_System_BusinessCorp_Filter ");
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
            parameters[5].Value = "SELECT * FROM View_System_BusinessCorp_Filter WHERE 1=1 " + StrCondition;
			parameters[6].Value = "BussId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

        public DataSet GetFreeList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort)
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
            parameters[5].Value = StrCondition;
            parameters[6].Value = "BussId";
            DataSet Ds = DbHelperSQL.RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }

		#endregion  成员方法
	}
}

