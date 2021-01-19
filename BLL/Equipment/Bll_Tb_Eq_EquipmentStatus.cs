using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using HM.Model.Eq;
namespace HM.BLL.Eq
{
    /// <summary>
    /// ҵ���߼���Bll_Tb_Eq_EquipmentStatus ��ժҪ˵����
    /// </summary>
    public class Bll_Tb_Eq_EquipmentStatus
    {
        private readonly MobileSoft.DAL.Eq.Dal_Tb_Eq_EquipmentStatus dal = new MobileSoft.DAL.Eq.Dal_Tb_Eq_EquipmentStatus();
        public Bll_Tb_Eq_EquipmentStatus()
        { }
        #region  ��Ա����
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(string Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(HM.Model.Eq.Tb_Eq_EquipmentStatus model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(HM.Model.Eq.Tb_Eq_EquipmentStatus model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(string Id)
        {

            dal.Delete(Id);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public HM.Model.Eq.Tb_Eq_EquipmentStatus GetModel(string Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public HM.Model.Eq.Tb_Eq_EquipmentStatus GetModelByCache(string Id)
        {

            string CacheKey = "Tb_Eq_EquipmentStatusModel-" + Id;
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
            return (HM.Model.Eq.Tb_Eq_EquipmentStatus)objModel;
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
        public List<HM.Model.Eq.Tb_Eq_EquipmentStatus> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<HM.Model.Eq.Tb_Eq_EquipmentStatus> DataTableToList(DataTable dt)
        {
            List<HM.Model.Eq.Tb_Eq_EquipmentStatus> modelList = new List<HM.Model.Eq.Tb_Eq_EquipmentStatus>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                HM.Model.Eq.Tb_Eq_EquipmentStatus model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new HM.Model.Eq.Tb_Eq_EquipmentStatus();
                    model.Id = dt.Rows[n]["Id"].ToString();
                    if (dt.Rows[n]["BeginTime"].ToString() != "")
                    {
                        model.BeginTime = DateTime.Parse(dt.Rows[n]["BeginTime"].ToString());
                    }
                    if (dt.Rows[n]["EndTime"].ToString() != "")
                    {
                        model.EndTime = DateTime.Parse(dt.Rows[n]["EndTime"].ToString());
                    }
                    model.EquipmentStatus = dt.Rows[n]["EquipmentStatus"].ToString();
                    model.Remark = dt.Rows[n]["Remark"].ToString();
                    model.EquipmentId = dt.Rows[n]["EquipmentId"].ToString();
                    model.AddPid = dt.Rows[n]["AddPid"].ToString();
                    if (dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    }
                    model.OperationPid = dt.Rows[n]["OperationPid"].ToString();
                    if (dt.Rows[n]["OperationTime"].ToString() != "")
                    {
                        model.OperationTime = DateTime.Parse(dt.Rows[n]["OperationTime"].ToString());
                    }
                    if (dt.Rows[n]["IsDelete"].ToString() != "")
                    {
                        model.IsDelete = int.Parse(dt.Rows[n]["IsDelete"].ToString());
                    }
                    model.Express = dt.Rows[n]["Express"].ToString();
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

