using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Resources;
namespace MobileSoft.BLL.Resources
{
    /// <summary>
    /// 业务逻辑类Bll_Tb_Resources_Attr 的摘要说明。
    /// </summary>
    public class Bll_Tb_Resources_Attr
    {
        private readonly MobileSoft.DAL.Resources.Dal_Tb_Resources_Attr dal = new MobileSoft.DAL.Resources.Dal_Tb_Resources_Attr();
        public Bll_Tb_Resources_Attr()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long AttrID)
        {
            return dal.Exists(AttrID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(MobileSoft.Model.Resources.Tb_Resources_Attr model)
        {
           dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(MobileSoft.Model.Resources.Tb_Resources_Attr model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(long AttrID)
        {

            dal.Delete(AttrID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MobileSoft.Model.Resources.Tb_Resources_Attr GetModel(long AttrID)
        {

            return dal.GetModel(AttrID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public MobileSoft.Model.Resources.Tb_Resources_Attr GetModelByCache(long AttrID)
        {

            string CacheKey = "Tb_Resources_AttrModel-" + AttrID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(AttrID);
                    if (objModel != null)
                    {
                        int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (MobileSoft.Model.Resources.Tb_Resources_Attr)objModel;
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
        public List<MobileSoft.Model.Resources.Tb_Resources_Attr> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<MobileSoft.Model.Resources.Tb_Resources_Attr> DataTableToList(DataTable dt)
        {
            List<MobileSoft.Model.Resources.Tb_Resources_Attr> modelList = new List<MobileSoft.Model.Resources.Tb_Resources_Attr>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                MobileSoft.Model.Resources.Tb_Resources_Attr model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new MobileSoft.Model.Resources.Tb_Resources_Attr();
                    //model.AttrID=dt.Rows[n]["AttrID"].ToString();
                    model.AttrName = dt.Rows[n]["AttrName"].ToString();
                    if (dt.Rows[n]["AttrIndex"].ToString() != "")
                    {
                        model.AttrIndex = int.Parse(dt.Rows[n]["AttrIndex"].ToString());
                    }
                    model.AttrType = dt.Rows[n]["AttrType"].ToString();
                    model.AttrColor = dt.Rows[n]["AttrColor"].ToString();
                    //model.BussId=dt.Rows[n]["BussId"].ToString();
                    if (dt.Rows[n]["IsDelete"].ToString() != "")
                    {
                        model.IsDelete = int.Parse(dt.Rows[n]["IsDelete"].ToString());
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

