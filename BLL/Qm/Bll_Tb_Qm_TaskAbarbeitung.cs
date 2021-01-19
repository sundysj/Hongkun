using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using HM.Model.Qm;
namespace HM.BLL.Qm
{
    /// <summary>
    /// 业务逻辑类Bll_Tb_Qm_TaskAbarbeitung 的摘要说明。
    /// </summary>
    public class Bll_Tb_Qm_TaskAbarbeitung
    {
        private readonly HM.DAL.Qm.Dal_Tb_Qm_TaskAbarbeitung dal = new HM.DAL.Qm.Dal_Tb_Qm_TaskAbarbeitung();
        public Bll_Tb_Qm_TaskAbarbeitung()
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
        /// 增加一条数据
        /// </summary>
        public void Add(HM.Model.Qm.Tb_Qm_TaskAbarbeitung model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(HM.Model.Qm.Tb_Qm_TaskAbarbeitung model)
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
        public HM.Model.Qm.Tb_Qm_TaskAbarbeitung GetModel(string Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public HM.Model.Qm.Tb_Qm_TaskAbarbeitung GetModelByCache(string Id)
        {

            string CacheKey = "Tb_Qm_TaskAbarbeitungModel-" + Id;
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
            return (HM.Model.Qm.Tb_Qm_TaskAbarbeitung)objModel;
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
        public List<HM.Model.Qm.Tb_Qm_TaskAbarbeitung> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<HM.Model.Qm.Tb_Qm_TaskAbarbeitung> DataTableToList(DataTable dt)
        {
            List<HM.Model.Qm.Tb_Qm_TaskAbarbeitung> modelList = new List<HM.Model.Qm.Tb_Qm_TaskAbarbeitung>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                HM.Model.Qm.Tb_Qm_TaskAbarbeitung model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new HM.Model.Qm.Tb_Qm_TaskAbarbeitung();
                    model.Id = dt.Rows[n]["Id"].ToString();
                    model.TaskId = dt.Rows[n]["TaskId"].ToString();
                    model.PointIds = dt.Rows[n]["PointIds"].ToString();
                    model.AddPId = dt.Rows[n]["AddPId"].ToString();
                    model.Pictures = dt.Rows[n]["Pictures"].ToString();
                    if (dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    }
                    if (dt.Rows[n]["CheckTime"].ToString() != "")
                    {
                        model.CheckTime = DateTime.Parse(dt.Rows[n]["CheckTime"].ToString());
                    }
                    model.CheckNote = dt.Rows[n]["CheckNote"].ToString();
                    model.CheckRemark = dt.Rows[n]["CheckRemark"].ToString();
                    model.CheckResult = dt.Rows[n]["CheckResult"].ToString();
                    model.CheckResult1 = dt.Rows[n]["CheckResult1"].ToString();
                    model.ProblemType = dt.Rows[n]["ProblemType"].ToString();
                    model.RectificationPeriod = dt.Rows[n]["RectificationPeriod"].ToString();
                    model.RectificationNote = dt.Rows[n]["RectificationNote"].ToString();
                    model.AbarPId = dt.Rows[n]["AbarPId"].ToString();
                    if (dt.Rows[n]["ReducePoint"].ToString() != "")
                    {
                        model.ReducePoint = decimal.Parse(dt.Rows[n]["ReducePoint"].ToString());
                    }
                    model.ReducePId = dt.Rows[n]["ReducePId"].ToString();
                    model.AbarCheck = dt.Rows[n]["AbarCheck"].ToString();
                    model.AbarCheckPId = dt.Rows[n]["AbarCheckPId"].ToString();
                    if (dt.Rows[n]["AbarCheckTime"].ToString() != "")
                    {
                        model.AbarCheckTime = DateTime.Parse(dt.Rows[n]["AbarCheckTime"].ToString());
                    }
                    model.Major = dt.Rows[n]["Major"].ToString();
                    model.ReduceResult = dt.Rows[n]["ReduceResult"].ToString();
                    if (dt.Rows[n]["ReduceTime"].ToString() != "")
                    {
                        model.ReduceTime = DateTime.Parse(dt.Rows[n]["ReduceTime"].ToString());
                    }
                    model.ReduceCheckResult = dt.Rows[n]["ReduceCheckResult"].ToString();
                    if (dt.Rows[n]["ReduceCheckTime"].ToString() != "")
                    {
                        model.ReduceCheckTime = DateTime.Parse(dt.Rows[n]["ReduceCheckTime"].ToString());
                    }
                    if (dt.Rows[n]["IsInTime"].ToString() != "")
                    {
                        model.IsInTime = int.Parse(dt.Rows[n]["IsInTime"].ToString());
                    }
                    if (dt.Rows[n]["IsOk"].ToString() != "")
                    {
                        model.IsOk = int.Parse(dt.Rows[n]["IsOk"].ToString());
                    }
                    model.CheckStatus = dt.Rows[n]["CheckStatus"].ToString();
                    model.Files = dt.Rows[n]["Files"].ToString();
                    model.Coordinate = dt.Rows[n]["Coordinate"].ToString();
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

