using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Resources;
namespace MobileSoft.BLL.Resources
{
    /// <summary>
    /// ҵ���߼���Bll_Tb_Resources_Attr ��ժҪ˵����
    /// </summary>
    public class Bll_Tb_Resources_Attr
    {
        private readonly MobileSoft.DAL.Resources.Dal_Tb_Resources_Attr dal = new MobileSoft.DAL.Resources.Dal_Tb_Resources_Attr();
        public Bll_Tb_Resources_Attr()
        { }
        #region  ��Ա����
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(long AttrID)
        {
            return dal.Exists(AttrID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(MobileSoft.Model.Resources.Tb_Resources_Attr model)
        {
           dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(MobileSoft.Model.Resources.Tb_Resources_Attr model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(long AttrID)
        {

            dal.Delete(AttrID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public MobileSoft.Model.Resources.Tb_Resources_Attr GetModel(long AttrID)
        {

            return dal.GetModel(AttrID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
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
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string fieldOrder)
        {
            return dal.GetList(Top, strWhere, fieldOrder);
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<MobileSoft.Model.Resources.Tb_Resources_Attr> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// ��������б�
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
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort)
        {
            return dal.GetList(out PageCount, out Counts, StrCondition, PageIndex, PageSize, SortField, Sort);
        }

        #endregion  ��Ա����
    }
}

