using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
    /// <summary>
    /// 业务逻辑类Bll_Tb_HSPR_CommActivitiesMessage 的摘要说明。
    /// </summary>
    public class Bll_Tb_HSPR_CommActivitiesMessage
    {
        private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CommActivitiesMessage dal = new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CommActivitiesMessage();
        public Bll_Tb_HSPR_CommActivitiesMessage()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string MsgCode)
        {
            return dal.Exists(MsgCode);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesMessage model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesMessage model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(string MsgCode)
        {

            dal.Delete(MsgCode);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesMessage GetModel(string MsgCode)
        {

            return dal.GetModel(MsgCode);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesMessage GetModelByCache(string MsgCode)
        {

            string CacheKey = "Tb_HSPR_CommActivitiesMessageModel-" + MsgCode;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(MsgCode);
                    if (objModel != null)
                    {
                        int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesMessage)objModel;
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
        public List<MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesMessage> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesMessage> DataTableToList(DataTable dt)
        {
            List<MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesMessage> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesMessage>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesMessage model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesMessage();
                    model.MsgCode = dt.Rows[n]["MsgCode"].ToString();
                    model.MsgType = dt.Rows[n]["MsgType"].ToString();
                    model.MsgTitle = dt.Rows[n]["MsgTitle"].ToString();
                    model.MsgContent = dt.Rows[n]["MsgContent"].ToString();
                    if (dt.Rows[n]["MsgDate"].ToString() != "")
                    {
                        model.MsgDate = DateTime.Parse(dt.Rows[n]["MsgDate"].ToString());
                    }
                    model.UserName = dt.Rows[n]["UserName"].ToString();
                    model.MsgLinkCode = dt.Rows[n]["MsgLinkCode"].ToString();
                    model.AccCust = dt.Rows[n]["AccCust"].ToString();
                    model.SendCust = dt.Rows[n]["SendCust"].ToString();
                    if (dt.Rows[n]["IsRead"].ToString() != "")
                    {
                        model.IsRead = int.Parse(dt.Rows[n]["IsRead"].ToString());
                    }
                    if (dt.Rows[n]["IsDelete"].ToString() != "")
                    {
                        model.IsDelete = int.Parse(dt.Rows[n]["IsDelete"].ToString());
                    }
                    //model.ID=dt.Rows[n]["ID"].ToString();
                    //model.ReplayID=dt.Rows[n]["ReplayID"].ToString();
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

