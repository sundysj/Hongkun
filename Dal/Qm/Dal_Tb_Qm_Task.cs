using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;

namespace HM.DAL.Qm
{
    /// <summary>
    /// 数据访问类Dal_Tb_Qm_Task。
    /// </summary>
    public class Dal_Tb_Qm_Task
    {
        public Dal_Tb_Qm_Task()
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

            int result = DbHelperSQL.RunProcedure("Proc_Tb_Qm_Task_Exists", parameters, out rowsAffected);
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
        public void Add(HM.Model.Qm.Tb_Qm_Task model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,36),
                    new SqlParameter("@AddPId", SqlDbType.VarChar,36),
                    new SqlParameter("@AddDate", SqlDbType.DateTime),
                    new SqlParameter("@BeginDate", SqlDbType.DateTime),
                    new SqlParameter("@endDate", SqlDbType.DateTime),
                    new SqlParameter("@ItemCode", SqlDbType.VarChar,36),
                    new SqlParameter("@ItemName", SqlDbType.NVarChar,50),
                    new SqlParameter("@TaskType", SqlDbType.VarChar,36),
                    new SqlParameter("@TaskLevelId", SqlDbType.VarChar,36),
                    new SqlParameter("@TaskNO", SqlDbType.NVarChar,50),
                    new SqlParameter("@StanId", SqlDbType.VarChar,36),
                    new SqlParameter("@Professional", SqlDbType.VarChar,36),
                    new SqlParameter("@Type", SqlDbType.VarChar,36),
                    new SqlParameter("@TypeDescription", SqlDbType.NVarChar,3999),
                    new SqlParameter("@CheckStandard", SqlDbType.NVarChar,3999),
                    new SqlParameter("@CheckWay", SqlDbType.NVarChar,3999),
                    new SqlParameter("@Point", SqlDbType.Decimal,9),
                    new SqlParameter("@TaskPId", SqlDbType.VarChar,36),
                    new SqlParameter("@TaskDepCode", SqlDbType.VarChar,36),
                    new SqlParameter("@TaskRoleId", SqlDbType.VarChar,36),
                    new SqlParameter("@CheckRoleId", SqlDbType.VarChar,36),
                    new SqlParameter("@PointCoverage", SqlDbType.Decimal,9),
                    new SqlParameter("@PointCoverageDone", SqlDbType.Decimal,9),
                    new SqlParameter("@TaskStatus", SqlDbType.NVarChar,50),
                    new SqlParameter("@IsAbarbeitung", SqlDbType.Int,4),
                    new SqlParameter("@IsDelete", SqlDbType.Int,4),
                    new SqlParameter("@CheckResult", SqlDbType.NVarChar,999),
                    new SqlParameter("@ClosePId", SqlDbType.VarChar,36),
                    new SqlParameter("@CloseReason", SqlDbType.NVarChar,200),
                    new SqlParameter("@CloseTime", SqlDbType.DateTime)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.AddPId;
            parameters[2].Value = model.AddDate;
            parameters[3].Value = model.BeginDate;
            parameters[4].Value = model.endDate;
            parameters[5].Value = model.ItemCode;
            parameters[6].Value = model.ItemName;
            parameters[7].Value = model.TaskType;
            parameters[8].Value = model.TaskLevelId;
            parameters[9].Value = model.TaskNO;
            parameters[10].Value = model.StanId;
            parameters[11].Value = model.Professional;
            parameters[12].Value = model.Type;
            parameters[13].Value = model.TypeDescription;
            parameters[14].Value = model.CheckStandard;
            parameters[15].Value = model.CheckWay;
            parameters[16].Value = model.Point;
            parameters[17].Value = model.TaskPId;
            parameters[18].Value = model.TaskDepCode;
            parameters[19].Value = model.TaskRoleId;
            parameters[20].Value = model.CheckRoleId;
            parameters[21].Value = model.PointCoverage;
            parameters[22].Value = model.PointCoverageDone;
            parameters[23].Value = model.TaskStatus;
            parameters[24].Value = model.IsAbarbeitung;
            parameters[25].Value = model.IsDelete;
            parameters[26].Value = model.CheckResult;
            parameters[27].Value = model.ClosePId;
            parameters[28].Value = model.CloseReason;
            parameters[29].Value = model.CloseTime;

            DbHelperSQL.RunProcedure("Proc_Tb_Qm_Task_ADD", parameters, out rowsAffected);
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public void Update(HM.Model.Qm.Tb_Qm_Task model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,36),
                    new SqlParameter("@AddPId", SqlDbType.VarChar,36),
                    new SqlParameter("@AddDate", SqlDbType.DateTime),
                    new SqlParameter("@BeginDate", SqlDbType.DateTime),
                    new SqlParameter("@endDate", SqlDbType.DateTime),
                    new SqlParameter("@ItemCode", SqlDbType.VarChar,36),
                    new SqlParameter("@ItemName", SqlDbType.NVarChar,50),
                    new SqlParameter("@TaskType", SqlDbType.VarChar,36),
                    new SqlParameter("@TaskLevelId", SqlDbType.VarChar,36),
                    new SqlParameter("@TaskNO", SqlDbType.NVarChar,50),
                    new SqlParameter("@StanId", SqlDbType.VarChar,36),
                    new SqlParameter("@Professional", SqlDbType.VarChar,36),
                    new SqlParameter("@Type", SqlDbType.VarChar,36),
                    new SqlParameter("@TypeDescription", SqlDbType.NVarChar,3999),
                    new SqlParameter("@CheckStandard", SqlDbType.NVarChar,3999),
                    new SqlParameter("@CheckWay", SqlDbType.NVarChar,3999),
                    new SqlParameter("@Point", SqlDbType.Decimal,9),
                    new SqlParameter("@TaskPId", SqlDbType.VarChar,36),
                    new SqlParameter("@TaskDepCode", SqlDbType.VarChar,36),
                    new SqlParameter("@TaskRoleId", SqlDbType.VarChar,36),
                    new SqlParameter("@CheckRoleId", SqlDbType.VarChar,36),
                    new SqlParameter("@PointCoverage", SqlDbType.Decimal,9),
                    new SqlParameter("@PointCoverageDone", SqlDbType.Decimal,9),
                    new SqlParameter("@TaskStatus", SqlDbType.NVarChar,50),
                    new SqlParameter("@IsAbarbeitung", SqlDbType.Int,4),
                    new SqlParameter("@IsDelete", SqlDbType.Int,4),
                    new SqlParameter("@CheckResult", SqlDbType.NVarChar,999),
                    new SqlParameter("@ClosePId", SqlDbType.VarChar,36),
                    new SqlParameter("@CloseReason", SqlDbType.NVarChar,200),
                    new SqlParameter("@CloseTime", SqlDbType.DateTime)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.AddPId;
            parameters[2].Value = model.AddDate;
            parameters[3].Value = model.BeginDate;
            parameters[4].Value = model.endDate;
            parameters[5].Value = model.ItemCode;
            parameters[6].Value = model.ItemName;
            parameters[7].Value = model.TaskType;
            parameters[8].Value = model.TaskLevelId;
            parameters[9].Value = model.TaskNO;
            parameters[10].Value = model.StanId;
            parameters[11].Value = model.Professional;
            parameters[12].Value = model.Type;
            parameters[13].Value = model.TypeDescription;
            parameters[14].Value = model.CheckStandard;
            parameters[15].Value = model.CheckWay;
            parameters[16].Value = model.Point;
            parameters[17].Value = model.TaskPId;
            parameters[18].Value = model.TaskDepCode;
            parameters[19].Value = model.TaskRoleId;
            parameters[20].Value = model.CheckRoleId;
            parameters[21].Value = model.PointCoverage;
            parameters[22].Value = model.PointCoverageDone;
            parameters[23].Value = model.TaskStatus;
            parameters[24].Value = model.IsAbarbeitung;
            parameters[25].Value = model.IsDelete;
            parameters[26].Value = model.CheckResult;
            parameters[27].Value = model.ClosePId;
            parameters[28].Value = model.CloseReason;
            parameters[29].Value = model.CloseTime;

            DbHelperSQL.RunProcedure("Proc_Tb_Qm_Task_Update", parameters, out rowsAffected);
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

            DbHelperSQL.RunProcedure("Proc_Tb_Qm_Task_Delete", parameters, out rowsAffected);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HM.Model.Qm.Tb_Qm_Task GetModel(string Id)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,50)};
            parameters[0].Value = Id;

            HM.Model.Qm.Tb_Qm_Task model = new HM.Model.Qm.Tb_Qm_Task();
            DataSet ds = DbHelperSQL.RunProcedure("Proc_Tb_Qm_Task_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.Id = ds.Tables[0].Rows[0]["Id"].ToString();
                model.AddPId = ds.Tables[0].Rows[0]["AddPId"].ToString();
                if (ds.Tables[0].Rows[0]["AddDate"].ToString() != "")
                {
                    model.AddDate = DateTime.Parse(ds.Tables[0].Rows[0]["AddDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BeginDate"].ToString() != "")
                {
                    model.BeginDate = DateTime.Parse(ds.Tables[0].Rows[0]["BeginDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["endDate"].ToString() != "")
                {
                    model.endDate = DateTime.Parse(ds.Tables[0].Rows[0]["endDate"].ToString());
                }
                model.ItemCode = ds.Tables[0].Rows[0]["ItemCode"].ToString();
                model.ItemName = ds.Tables[0].Rows[0]["ItemName"].ToString();
                model.TaskType = ds.Tables[0].Rows[0]["TaskType"].ToString();
                model.TaskLevelId = ds.Tables[0].Rows[0]["TaskLevelId"].ToString();
                model.TaskNO = ds.Tables[0].Rows[0]["TaskNO"].ToString();
                model.StanId = ds.Tables[0].Rows[0]["StanId"].ToString();
                model.Professional = ds.Tables[0].Rows[0]["Professional"].ToString();
                model.Type = ds.Tables[0].Rows[0]["Type"].ToString();
                model.TypeDescription = ds.Tables[0].Rows[0]["TypeDescription"].ToString();
                model.CheckStandard = ds.Tables[0].Rows[0]["CheckStandard"].ToString();
                model.CheckWay = ds.Tables[0].Rows[0]["CheckWay"].ToString();
                if (ds.Tables[0].Rows[0]["Point"].ToString() != "")
                {
                    model.Point = decimal.Parse(ds.Tables[0].Rows[0]["Point"].ToString());
                }
                model.TaskPId = ds.Tables[0].Rows[0]["TaskPId"].ToString();
                model.TaskDepCode = ds.Tables[0].Rows[0]["TaskDepCode"].ToString();
                model.TaskRoleId = ds.Tables[0].Rows[0]["TaskRoleId"].ToString();
                model.CheckRoleId = ds.Tables[0].Rows[0]["CheckRoleId"].ToString();
                if (ds.Tables[0].Rows[0]["PointCoverage"].ToString() != "")
                {
                    model.PointCoverage = decimal.Parse(ds.Tables[0].Rows[0]["PointCoverage"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PointCoverageDone"].ToString() != "")
                {
                    model.PointCoverageDone = decimal.Parse(ds.Tables[0].Rows[0]["PointCoverageDone"].ToString());
                }
                model.TaskStatus = ds.Tables[0].Rows[0]["TaskStatus"].ToString();
                if (ds.Tables[0].Rows[0]["IsAbarbeitung"].ToString() != "")
                {
                    model.IsAbarbeitung = int.Parse(ds.Tables[0].Rows[0]["IsAbarbeitung"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsDelete"].ToString() != "")
                {
                    model.IsDelete = int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
                }
                model.CheckResult = ds.Tables[0].Rows[0]["CheckResult"].ToString();
                model.ClosePId = ds.Tables[0].Rows[0]["ClosePId"].ToString();
                model.CloseReason = ds.Tables[0].Rows[0]["CloseReason"].ToString();
                if (ds.Tables[0].Rows[0]["CloseTime"].ToString() != "")
                {
                    model.CloseTime = DateTime.Parse(ds.Tables[0].Rows[0]["CloseTime"].ToString());
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,AddPId,AddDate,BeginDate,endDate,ItemCode,ItemName,TaskType,TaskLevelId,TaskNO,StanId,Professional,Type,TypeDescription,CheckStandard,CheckWay,Point,TaskPId,TaskDepCode,TaskRoleId,CheckRoleId,PointCoverage,PointCoverageDone,TaskStatus,IsAbarbeitung,IsDelete,CheckResult,ClosePId,CloseReason,CloseTime ");
            strSql.Append(" FROM Tb_Qm_Task ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id,AddPId,AddDate,BeginDate,endDate,ItemCode,ItemName,TaskType,TaskLevelId,TaskNO,StanId,Professional,Type,TypeDescription,CheckStandard,CheckWay,Point,TaskPId,TaskDepCode,TaskRoleId,CheckRoleId,PointCoverage,PointCoverageDone,TaskStatus,IsAbarbeitung,IsDelete,CheckResult,ClosePId,CloseReason,CloseTime ");
            strSql.Append(" FROM Tb_Qm_Task ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
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
            parameters[5].Value = "SELECT * FROM Tb_Qm_Task WHERE 1=1 " + StrCondition;
            parameters[6].Value = "Id";
            DataSet Ds = DbHelperSQL.RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }

        #endregion  成员方法
    }
}

