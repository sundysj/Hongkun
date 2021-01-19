using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
    /// <summary>
    /// 数据访问类Dal_Tb_HSPR_CommActivitiesMessage。
    /// </summary>
    public class Dal_Tb_HSPR_CommActivitiesMessage
    {
        public Dal_Tb_HSPR_CommActivitiesMessage()
        {
            DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
        }
        #region  成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string MsgCode)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@MsgCode", SqlDbType.VarChar,50)};
            parameters[0].Value = MsgCode;

            int result = DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommActivitiesMessage_Exists", parameters, out rowsAffected);
            if (result == 1)
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
        public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesMessage model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@MsgCode", SqlDbType.VarChar,36),
					new SqlParameter("@MsgType", SqlDbType.VarChar,36),
					new SqlParameter("@MsgTitle", SqlDbType.VarChar,3999),
					new SqlParameter("@MsgContent", SqlDbType.VarChar,3999),
					new SqlParameter("@MsgDate", SqlDbType.DateTime),
					new SqlParameter("@UserName", SqlDbType.VarChar,36),
					new SqlParameter("@MsgLinkCode", SqlDbType.VarChar,36),
					new SqlParameter("@AccCust", SqlDbType.VarChar,36),
					new SqlParameter("@SendCust", SqlDbType.VarChar,36),
					new SqlParameter("@IsRead", SqlDbType.Int,4),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@ReplayID", SqlDbType.BigInt,8),
                    new SqlParameter("@IsComplete", SqlDbType.VarChar,36),
                    new SqlParameter("@ImgList", SqlDbType.VarChar,3999)
                                        };
            parameters[0].Value = model.MsgCode;
            parameters[1].Value = model.MsgType;
            parameters[2].Value = model.MsgTitle;
            parameters[3].Value = model.MsgContent;
            parameters[4].Value = model.MsgDate;
            parameters[5].Value = model.UserName;
            parameters[6].Value = model.MsgLinkCode;
            parameters[7].Value = model.AccCust;
            parameters[8].Value = model.SendCust;
            parameters[9].Value = model.IsRead;
            parameters[10].Value = model.IsDelete;
            parameters[11].Value = model.ID;
            parameters[12].Value = model.ReplayID;
            parameters[13].Value = model.IsComplete;
            parameters[14].Value = model.ImgList;
            DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommActivitiesMessage_ADD", parameters, out rowsAffected);
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesMessage model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@MsgCode", SqlDbType.VarChar,36),
					new SqlParameter("@MsgType", SqlDbType.VarChar,36),
					new SqlParameter("@MsgTitle", SqlDbType.VarChar,3999),
					new SqlParameter("@MsgContent", SqlDbType.VarChar,3999),
					new SqlParameter("@MsgDate", SqlDbType.DateTime),
					new SqlParameter("@UserName", SqlDbType.VarChar,36),
					new SqlParameter("@MsgLinkCode", SqlDbType.VarChar,36),
					new SqlParameter("@AccCust", SqlDbType.VarChar,36),
					new SqlParameter("@SendCust", SqlDbType.VarChar,36),
					new SqlParameter("@IsRead", SqlDbType.Int,4),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@ReplayID", SqlDbType.BigInt,8),
                    new SqlParameter("@IsComplete", SqlDbType.VarChar,36),
                    new SqlParameter("@ImgList", SqlDbType.VarChar,3999)
                                        
                                        };
            parameters[0].Value = model.MsgCode;
            parameters[1].Value = model.MsgType;
            parameters[2].Value = model.MsgTitle;
            parameters[3].Value = model.MsgContent;
            parameters[4].Value = model.MsgDate;
            parameters[5].Value = model.UserName;
            parameters[6].Value = model.MsgLinkCode;
            parameters[7].Value = model.AccCust;
            parameters[8].Value = model.SendCust;
            parameters[9].Value = model.IsRead;
            parameters[10].Value = model.IsDelete;
            parameters[11].Value = model.ID;
            parameters[12].Value = model.ReplayID;
            parameters[13].Value = model.IsComplete;
            parameters[14].Value = model.ImgList;
            DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommActivitiesMessage_Update", parameters, out rowsAffected);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(string MsgCode)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@MsgCode", SqlDbType.VarChar,50)};
            parameters[0].Value = MsgCode;

            DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommActivitiesMessage_Delete", parameters, out rowsAffected);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesMessage GetModel(string MsgCode)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@MsgCode", SqlDbType.VarChar,50)};
            parameters[0].Value = MsgCode;

            MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesMessage model = new MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesMessage();
            DataSet ds = DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommActivitiesMessage_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.MsgCode = ds.Tables[0].Rows[0]["MsgCode"].ToString();
                model.MsgType = ds.Tables[0].Rows[0]["MsgType"].ToString();
                model.MsgTitle = ds.Tables[0].Rows[0]["MsgTitle"].ToString();
                model.MsgContent = ds.Tables[0].Rows[0]["MsgContent"].ToString();
                if (ds.Tables[0].Rows[0]["MsgDate"].ToString() != "")
                {
                    model.MsgDate = DateTime.Parse(ds.Tables[0].Rows[0]["MsgDate"].ToString());
                }
                model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                model.MsgLinkCode = ds.Tables[0].Rows[0]["MsgLinkCode"].ToString();
                model.AccCust = ds.Tables[0].Rows[0]["AccCust"].ToString();
                model.SendCust = ds.Tables[0].Rows[0]["SendCust"].ToString();
                if (ds.Tables[0].Rows[0]["IsRead"].ToString() != "")
                {
                    model.IsRead = int.Parse(ds.Tables[0].Rows[0]["IsRead"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsDelete"].ToString() != "")
                {
                    model.IsDelete = int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = long.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReplayID"].ToString() != "")
                {
                    model.ReplayID = long.Parse(ds.Tables[0].Rows[0]["ReplayID"].ToString());
                }

                model.IsComplete = ds.Tables[0].Rows[0]["IsComplete"].ToString();
                model.ImgList = ds.Tables[0].Rows[0]["ImgList"].ToString();
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM View_HSPR_CommActivitiesMessage_Filter ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string fieldOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM View_HSPR_CommActivitiesMessage_Filter ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + fieldOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort)
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
            parameters[5].Value = "SELECT * FROM View_HSPR_CommActivitiesMessage_Filter WHERE 1=1 " + StrCondition;
            parameters[6].Value = "MsgCode";
            DataSet Ds = DbHelperSQL.RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }

        #endregion  成员方法
    }
}

