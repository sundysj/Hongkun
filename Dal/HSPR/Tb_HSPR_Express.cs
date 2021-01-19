using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
    /// <summary>
    /// 数据访问类Tb_HSPR_Express。
    /// </summary>
    public class Tb_HSPR_Express
    {
        public Tb_HSPR_Express()
        {
            DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
        }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int EMSId)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@EMSId", SqlDbType.Int,4)};
            parameters[0].Value = EMSId;

            int result = DbHelperSQL.RunProcedure("UP_Tb_HSPR_Express_Exists", parameters, out rowsAffected);
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
        public int Add(MobileSoft.Model.HSPR.Tb_HSPR_Express model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@EMSId", SqlDbType.Int,4),
					new SqlParameter("@EMSNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@MailMan", SqlDbType.NVarChar,50),
					new SqlParameter("@MailManPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@Addressee", SqlDbType.NVarChar,50),
					new SqlParameter("@AddresseePhone", SqlDbType.NVarChar,50),
					new SqlParameter("@MailCompany", SqlDbType.NVarChar,200),
					new SqlParameter("@MailContent", SqlDbType.NVarChar,500),
					new SqlParameter("@Courier", SqlDbType.NVarChar,50),
					new SqlParameter("@CourierPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@PmGetDate", SqlDbType.DateTime),
					new SqlParameter("@PmOperator", SqlDbType.NVarChar,50),
					new SqlParameter("@PmPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@IsNotice", SqlDbType.Int,4),
					new SqlParameter("@CustIsGet", SqlDbType.Int,4),
					new SqlParameter("@GetPerson", SqlDbType.NVarChar,50),
					new SqlParameter("@GetDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.EMSNumber;
            parameters[2].Value = model.MailMan;
            parameters[3].Value = model.MailManPhone;
            parameters[4].Value = model.Addressee;
            parameters[5].Value = model.AddresseePhone;
            parameters[6].Value = model.MailCompany;
            parameters[7].Value = model.MailContent;
            parameters[8].Value = model.Courier;
            parameters[9].Value = model.CourierPhone;
            parameters[10].Value = model.PmGetDate;
            parameters[11].Value = model.PmOperator;
            parameters[12].Value = model.PmPhone;
            parameters[13].Value = model.Remark;
            parameters[14].Value = model.IsNotice;
            parameters[15].Value = model.CustIsGet;
            parameters[16].Value = model.GetPerson;
            parameters[17].Value = model.GetDate;
            parameters[18].Value = model.IsDelete;
            parameters[19].Value = model.CreateDate;
            parameters[20].Value = model.CreateUser;
            parameters[21].Value = model.CustID;
            parameters[22].Value = model.RoomID;
            parameters[23].Value = model.CommID;

            DbHelperSQL.RunProcedure("UP_Tb_HSPR_Express_ADD", parameters, out rowsAffected);
            return (int)parameters[0].Value;
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public void Update(MobileSoft.Model.HSPR.Tb_HSPR_Express model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@EMSId", SqlDbType.Int,4),
					new SqlParameter("@EMSNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@MailMan", SqlDbType.NVarChar,50),
					new SqlParameter("@MailManPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@Addressee", SqlDbType.NVarChar,50),
					new SqlParameter("@AddresseePhone", SqlDbType.NVarChar,50),
					new SqlParameter("@MailCompany", SqlDbType.NVarChar,200),
					new SqlParameter("@MailContent", SqlDbType.NVarChar,500),
					new SqlParameter("@Courier", SqlDbType.NVarChar,50),
					new SqlParameter("@CourierPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@PmGetDate", SqlDbType.DateTime),
					new SqlParameter("@PmOperator", SqlDbType.NVarChar,50),
					new SqlParameter("@PmPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@IsNotice", SqlDbType.Int,4),
					new SqlParameter("@CustIsGet", SqlDbType.Int,4),
					new SqlParameter("@GetPerson", SqlDbType.NVarChar,50),
					new SqlParameter("@GetDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4)};
            parameters[0].Value = model.EMSId;
            parameters[1].Value = model.EMSNumber;
            parameters[2].Value = model.MailMan;
            parameters[3].Value = model.MailManPhone;
            parameters[4].Value = model.Addressee;
            parameters[5].Value = model.AddresseePhone;
            parameters[6].Value = model.MailCompany;
            parameters[7].Value = model.MailContent;
            parameters[8].Value = model.Courier;
            parameters[9].Value = model.CourierPhone;
            parameters[10].Value = model.PmGetDate;
            parameters[11].Value = model.PmOperator;
            parameters[12].Value = model.PmPhone;
            parameters[13].Value = model.Remark;
            parameters[14].Value = model.IsNotice;
            parameters[15].Value = model.CustIsGet;
            parameters[16].Value = model.GetPerson;
            parameters[17].Value = model.GetDate;
            parameters[18].Value = model.IsDelete;
            parameters[19].Value = model.CreateDate;
            parameters[20].Value = model.CreateUser;
            parameters[21].Value = model.CustID;
            parameters[22].Value = model.RoomID;
            parameters[23].Value = model.CommID;

            DbHelperSQL.RunProcedure("UP_Tb_HSPR_Express_Update", parameters, out rowsAffected);
        }

        public void Update(int num)
        {
            string strSQL = "UPDATE Tb_HSPR_Express SET CustIsGet=1 WHERE EMSId=" + num;
            DbHelperSQL.ExecuteSql(strSQL);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int EMSId)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@EMSId", SqlDbType.Int,4)};
            parameters[0].Value = EMSId;

            DbHelperSQL.RunProcedure("UP_Tb_HSPR_Express_Delete", parameters, out rowsAffected);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MobileSoft.Model.HSPR.Tb_HSPR_Express GetModel(int EMSId)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@EMSId", SqlDbType.Int,4)};
            parameters[0].Value = EMSId;

            MobileSoft.Model.HSPR.Tb_HSPR_Express model = new MobileSoft.Model.HSPR.Tb_HSPR_Express();
            DataSet ds = DbHelperSQL.RunProcedure("UP_Tb_HSPR_Express_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["EMSId"].ToString() != "")
                {
                    model.EMSId = int.Parse(ds.Tables[0].Rows[0]["EMSId"].ToString());
                }
                model.EMSNumber = ds.Tables[0].Rows[0]["EMSNumber"].ToString();
                model.MailMan = ds.Tables[0].Rows[0]["MailMan"].ToString();
                model.MailManPhone = ds.Tables[0].Rows[0]["MailManPhone"].ToString();
                model.Addressee = ds.Tables[0].Rows[0]["Addressee"].ToString();
                model.AddresseePhone = ds.Tables[0].Rows[0]["AddresseePhone"].ToString();
                model.MailCompany = ds.Tables[0].Rows[0]["MailCompany"].ToString();
                model.MailContent = ds.Tables[0].Rows[0]["MailContent"].ToString();
                model.Courier = ds.Tables[0].Rows[0]["Courier"].ToString();
                model.CourierPhone = ds.Tables[0].Rows[0]["CourierPhone"].ToString();
                if (ds.Tables[0].Rows[0]["PmGetDate"].ToString() != "")
                {
                    model.PmGetDate = DateTime.Parse(ds.Tables[0].Rows[0]["PmGetDate"].ToString());
                }
                model.PmOperator = ds.Tables[0].Rows[0]["PmOperator"].ToString();
                model.PmPhone = ds.Tables[0].Rows[0]["PmPhone"].ToString();
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                if (ds.Tables[0].Rows[0]["IsNotice"].ToString() != "")
                {
                    model.IsNotice = int.Parse(ds.Tables[0].Rows[0]["IsNotice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CustIsGet"].ToString() != "")
                {
                    model.CustIsGet = int.Parse(ds.Tables[0].Rows[0]["CustIsGet"].ToString());
                }
                model.GetPerson = ds.Tables[0].Rows[0]["GetPerson"].ToString();
                if (ds.Tables[0].Rows[0]["GetDate"].ToString() != "")
                {
                    model.GetDate = DateTime.Parse(ds.Tables[0].Rows[0]["GetDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsDelete"].ToString() != "")
                {
                    model.IsDelete = int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                model.CreateUser = ds.Tables[0].Rows[0]["CreateUser"].ToString();
                if (ds.Tables[0].Rows[0]["CustID"].ToString() != "")
                {
                    model.CustID = long.Parse(ds.Tables[0].Rows[0]["CustID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RoomID"].ToString() != "")
                {
                    model.RoomID = long.Parse(ds.Tables[0].Rows[0]["RoomID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CommID"].ToString() != "")
                {
                    model.CommID = int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
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
            strSql.Append("select EMSId,EMSNumber,MailMan,MailManPhone,Addressee,AddresseePhone,MailCompany,MailContent,Courier,CourierPhone,PmGetDate,PmOperator,PmPhone,Remark,IsNotice,CustIsGet,GetPerson,GetDate,IsDelete,CreateDate,CreateUser,CustID,RoomID,CommID ");
            strSql.Append(" FROM Tb_HSPR_Express ");
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
            strSql.Append(" EMSId,EMSNumber,MailMan,MailManPhone,Addressee,AddresseePhone,MailCompany,MailContent,Courier,CourierPhone,PmGetDate,PmOperator,PmPhone,Remark,IsNotice,CustIsGet,GetPerson,GetDate,IsDelete,CreateDate,CreateUser,CustID,RoomID,CommID ");
            strSql.Append(" FROM Tb_HSPR_Express ");
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
            parameters[5].Value = "SELECT * FROM Tb_HSPR_Express WHERE 1=1 " + StrCondition;
            parameters[6].Value = "EMSId";
            DataSet Ds = DbHelperSQL.RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }

        #endregion  成员方法
    }
}

