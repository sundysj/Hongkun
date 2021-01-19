using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Common;
namespace MobileSoft.BLL.Common
{
    /// <summary>
    /// 业务逻辑类Bll_Tb_SendMessageRecord 的摘要说明。
    /// </summary>
    public class Bll_Tb_SendMessageRecord
    {
        private readonly MobileSoft.DAL.Common.Dal_Tb_SendMessageRecord dal = new MobileSoft.DAL.Common.Dal_Tb_SendMessageRecord();
        public Bll_Tb_SendMessageRecord()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 新增短信记录
        /// </summary>
        /// <param name="Mobile">手机号</param>
        /// <param name="SendContent">内容</param>
        /// <param name="MacCode">MacCode</param>
        /// <param name="SendType">短信类型</param>
        /// <param name="SendState">发送状态</param>
        public Tb_SendMessageRecord Add( string Mobile, string SendContent, string MacCode, string SendType,string SendState)
        {
            MobileSoft.Model.Common.Tb_SendMessageRecord model = new Tb_SendMessageRecord();
            model.Id = Guid.NewGuid().ToString();
            model.Mobile = Mobile;
            model.SendContent = SendContent;
            model.MacCode = MacCode;
            model.SendTime = DateTime.Now;
            model.SendType = SendType;
            model.SendState = SendState;
            Add(model);
            return model;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        private void Add(MobileSoft.Model.Common.Tb_SendMessageRecord model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(MobileSoft.Model.Common.Tb_SendMessageRecord model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(string Id)
        {

            dal.Delete(Id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MobileSoft.Model.Common.Tb_SendMessageRecord GetModel(string Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public MobileSoft.Model.Common.Tb_SendMessageRecord GetModelByCache(string Id)
        {

            string CacheKey = "Tb_SendMessageRecordModel-" + Id;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(Id);
                    if (objModel != null)
                    {
                        int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (MobileSoft.Model.Common.Tb_SendMessageRecord)objModel;
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
        public List<MobileSoft.Model.Common.Tb_SendMessageRecord> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<MobileSoft.Model.Common.Tb_SendMessageRecord> DataTableToList(DataTable dt)
        {
            List<MobileSoft.Model.Common.Tb_SendMessageRecord> modelList = new List<MobileSoft.Model.Common.Tb_SendMessageRecord>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                MobileSoft.Model.Common.Tb_SendMessageRecord model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new MobileSoft.Model.Common.Tb_SendMessageRecord();
                    model.Id = dt.Rows[n]["Id"].ToString();
                    model.Mobile = dt.Rows[n]["Mobile"].ToString();
                    model.SendContent = dt.Rows[n]["SendContent"].ToString();
                    if (dt.Rows[n]["SendTime"].ToString() != "")
                    {
                        model.SendTime = DateTime.Parse(dt.Rows[n]["SendTime"].ToString());
                    }
                    model.MacCode = dt.Rows[n]["MacCode"].ToString();
                    model.SendType = dt.Rows[n]["SendType"].ToString();
                    model.SendState = dt.Rows[n]["SendState"].ToString();
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

