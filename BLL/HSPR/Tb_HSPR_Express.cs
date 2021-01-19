using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;

namespace MobileSoft.BLL.HSPR
{
    /// <summary>
    /// 业务逻辑类Tb_HSPR_Express 的摘要说明。
    /// </summary>
    public class Tb_HSPR_Express
    {
        private readonly MobileSoft.DAL.HSPR.Tb_HSPR_Express dal = new MobileSoft.DAL.HSPR.Tb_HSPR_Express();
        public Tb_HSPR_Express()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int EMSId)
        {
            return dal.Exists(EMSId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(MobileSoft.Model.HSPR.Tb_HSPR_Express model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(MobileSoft.Model.HSPR.Tb_HSPR_Express model)
        {
            dal.Update(model);
        }

        public void Update(int EMSId)
        {
            dal.Update(EMSId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int EMSId)
        {

            dal.Delete(EMSId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MobileSoft.Model.HSPR.Tb_HSPR_Express GetModel(int EMSId)
        {

            return dal.GetModel(EMSId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public MobileSoft.Model.HSPR.Tb_HSPR_Express GetModelByCache(int EMSId)
        {

            string CacheKey = "Tb_HSPR_ExpressModel-" + EMSId;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(EMSId);
                    if (objModel != null)
                    {
                        int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (MobileSoft.Model.HSPR.Tb_HSPR_Express)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string fieldOrder)
        {
            return dal.GetList(Top, strWhere, fieldOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<MobileSoft.Model.HSPR.Tb_HSPR_Express> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<MobileSoft.Model.HSPR.Tb_HSPR_Express> DataTableToList(DataTable dt)
        {
            List<MobileSoft.Model.HSPR.Tb_HSPR_Express> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_Express>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                MobileSoft.Model.HSPR.Tb_HSPR_Express model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new MobileSoft.Model.HSPR.Tb_HSPR_Express();
                    if (dt.Rows[n]["EMSId"].ToString() != "")
                    {
                        model.EMSId = int.Parse(dt.Rows[n]["EMSId"].ToString());
                    }
                    model.EMSNumber = dt.Rows[n]["EMSNumber"].ToString();
                    model.MailMan = dt.Rows[n]["MailMan"].ToString();
                    model.MailManPhone = dt.Rows[n]["MailManPhone"].ToString();
                    model.Addressee = dt.Rows[n]["Addressee"].ToString();
                    model.AddresseePhone = dt.Rows[n]["AddresseePhone"].ToString();
                    model.MailCompany = dt.Rows[n]["MailCompany"].ToString();
                    model.MailContent = dt.Rows[n]["MailContent"].ToString();
                    model.Courier = dt.Rows[n]["Courier"].ToString();
                    model.CourierPhone = dt.Rows[n]["CourierPhone"].ToString();
                    if (dt.Rows[n]["PmGetDate"].ToString() != "")
                    {
                        model.PmGetDate = DateTime.Parse(dt.Rows[n]["PmGetDate"].ToString());
                    }
                    model.PmOperator = dt.Rows[n]["PmOperator"].ToString();
                    model.PmPhone = dt.Rows[n]["PmPhone"].ToString();
                    model.Remark = dt.Rows[n]["Remark"].ToString();
                    if (dt.Rows[n]["IsNotice"].ToString() != "")
                    {
                        model.IsNotice = int.Parse(dt.Rows[n]["IsNotice"].ToString());
                    }
                    if (dt.Rows[n]["CustIsGet"].ToString() != "")
                    {
                        model.CustIsGet = int.Parse(dt.Rows[n]["CustIsGet"].ToString());
                    }
                    model.GetPerson = dt.Rows[n]["GetPerson"].ToString();
                    if (dt.Rows[n]["GetDate"].ToString() != "")
                    {
                        model.GetDate = DateTime.Parse(dt.Rows[n]["GetDate"].ToString());
                    }
                    if (dt.Rows[n]["IsDelete"].ToString() != "")
                    {
                        model.IsDelete = int.Parse(dt.Rows[n]["IsDelete"].ToString());
                    }
                    if (dt.Rows[n]["CreateDate"].ToString() != "")
                    {
                        model.CreateDate = DateTime.Parse(dt.Rows[n]["CreateDate"].ToString());
                    }
                    model.CreateUser = dt.Rows[n]["CreateUser"].ToString();
                    //model.CustID=dt.Rows[n]["CustID"].ToString();
                    //model.RoomID=dt.Rows[n]["RoomID"].ToString();
                    if (dt.Rows[n]["CommID"].ToString() != "")
                    {
                        model.CommID = int.Parse(dt.Rows[n]["CommID"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort)
        {
            return dal.GetList(out PageCount, out Counts, StrCondition, PageIndex, PageSize, SortField, Sort);
        }

        #endregion  成员方法
    }
}

