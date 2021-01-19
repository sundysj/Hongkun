using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Order;
namespace MobileSoft.BLL.Order
{
    /// <summary>
    /// 业务逻辑类Bll_Tb_Order_BusCar 的摘要说明。
    /// </summary>
    public class Bll_Tb_Order_BusCar
    {
        private readonly MobileSoft.DAL.Order.Dal_Tb_Order_BusCar dal = new MobileSoft.DAL.Order.Dal_Tb_Order_BusCar();
        public Bll_Tb_Order_BusCar()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string BusCarID)
        {
            return dal.Exists(BusCarID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(MobileSoft.Model.Order.Tb_Order_BusCar model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(MobileSoft.Model.Order.Tb_Order_BusCar model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(string BusCarID)
        {

            dal.Delete(BusCarID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MobileSoft.Model.Order.Tb_Order_BusCar GetModel(string BusCarID)
        {

            return dal.GetModel(BusCarID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public MobileSoft.Model.Order.Tb_Order_BusCar GetModelByCache(string BusCarID)
        {

            string CacheKey = "Tb_Order_BusCarModel-" + BusCarID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(BusCarID);
                    if (objModel != null)
                    {
                        int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (MobileSoft.Model.Order.Tb_Order_BusCar)objModel;
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<MobileSoft.Model.Order.Tb_Order_BusCar> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<MobileSoft.Model.Order.Tb_Order_BusCar> DataTableToList(DataTable dt)
        {
            List<MobileSoft.Model.Order.Tb_Order_BusCar> modelList = new List<MobileSoft.Model.Order.Tb_Order_BusCar>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                MobileSoft.Model.Order.Tb_Order_BusCar model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new MobileSoft.Model.Order.Tb_Order_BusCar();
                    model.BusCarID = dt.Rows[n]["BusCarID"].ToString();
                    //model.BusCustID=dt.Rows[n]["BusCustID"].ToString();
                    //model.BusReleaseID=dt.Rows[n]["BusReleaseID"].ToString();
                    //model.BusBussId=dt.Rows[n]["BusBussId"].ToString();
                    if (dt.Rows[n]["Created"].ToString() != "")
                    {
                        model.Created = DateTime.Parse(dt.Rows[n]["Created"].ToString());
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

