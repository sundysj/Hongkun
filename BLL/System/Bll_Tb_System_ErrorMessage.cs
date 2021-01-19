using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using Erp.Model.System;
namespace Erp.BLL.System
{
    /// <summary>
    /// ҵ���߼���Bll_Tb_System_ErrorMessage ��ժҪ˵����
    /// </summary>
    public class Bll_Tb_System_ErrorMessage
    {
        private readonly Erp.DAL.System.Dal_Tb_System_ErrorMessage dal = new Erp.DAL.System.Dal_Tb_System_ErrorMessage();
        public Bll_Tb_System_ErrorMessage()
        { }
        #region  ��Ա����
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int fdi_ErrorId)
        {
            return dal.Exists(fdi_ErrorId);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Erp.Model.System.Tb_System_ErrorMessage model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(Erp.Model.System.Tb_System_ErrorMessage model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int fdi_ErrorId)
        {

            dal.Delete(fdi_ErrorId);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Erp.Model.System.Tb_System_ErrorMessage GetModel(int fdi_ErrorId)
        {

            return dal.GetModel(fdi_ErrorId);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public Erp.Model.System.Tb_System_ErrorMessage GetModelByCache(int fdi_ErrorId)
        {

            string CacheKey = "Tb_System_ErrorMessageModel-" + fdi_ErrorId;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(fdi_ErrorId);
                    if (objModel != null)
                    {
                        int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Erp.Model.System.Tb_System_ErrorMessage)objModel;
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<Erp.Model.System.Tb_System_ErrorMessage> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<Erp.Model.System.Tb_System_ErrorMessage> DataTableToList(DataTable dt)
        {
            List<Erp.Model.System.Tb_System_ErrorMessage> modelList = new List<Erp.Model.System.Tb_System_ErrorMessage>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Erp.Model.System.Tb_System_ErrorMessage model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Erp.Model.System.Tb_System_ErrorMessage();
                    if (dt.Rows[n]["fdi_ErrorId"].ToString() != "")
                    {
                        model.fdi_ErrorId = int.Parse(dt.Rows[n]["fdi_ErrorId"].ToString());
                    }
                    if (dt.Rows[n]["fdi_CorpId"].ToString() != "")
                    {
                        model.fdi_CorpId = int.Parse(dt.Rows[n]["fdi_CorpId"].ToString());
                    }
                    model.fdv_OprUserName = dt.Rows[n]["fdv_OprUserName"].ToString();
                    model.fdv_ErrorPage = dt.Rows[n]["fdv_ErrorPage"].ToString();
                    model.fdv_ErrorMessage = dt.Rows[n]["fdv_ErrorMessage"].ToString();
                    if (dt.Rows[n]["fdd_ErrorDate"].ToString() != "")
                    {
                        model.fdd_ErrorDate = DateTime.Parse(dt.Rows[n]["fdd_ErrorDate"].ToString());
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

