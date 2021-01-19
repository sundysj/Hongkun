using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using HM.Model.Qm;
namespace HM.BLL.Qm
{
    /// <summary>
    /// 业务逻辑类Bll_Tb_Qm_Task 的摘要说明。
    /// </summary>
    public class Bll_Tb_Qm_Task
    {
        private readonly HM.DAL.Qm.Dal_Tb_Qm_Task dal = new HM.DAL.Qm.Dal_Tb_Qm_Task();
        public Bll_Tb_Qm_Task()
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
        public void Add(HM.Model.Qm.Tb_Qm_Task model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(HM.Model.Qm.Tb_Qm_Task model)
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
        public HM.Model.Qm.Tb_Qm_Task GetModel(string Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public HM.Model.Qm.Tb_Qm_Task GetModelByCache(string Id)
        {

            string CacheKey = "Tb_Qm_TaskModel-" + Id;
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
            return (HM.Model.Qm.Tb_Qm_Task)objModel;
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
        public List<HM.Model.Qm.Tb_Qm_Task> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<HM.Model.Qm.Tb_Qm_Task> DataTableToList(DataTable dt)
        {
            List<HM.Model.Qm.Tb_Qm_Task> modelList = new List<HM.Model.Qm.Tb_Qm_Task>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                HM.Model.Qm.Tb_Qm_Task model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new HM.Model.Qm.Tb_Qm_Task();
                    model.Id = dt.Rows[n]["Id"].ToString();
                    model.AddPId = dt.Rows[n]["AddPId"].ToString();
                    if (dt.Rows[n]["AddDate"].ToString() != "")
                    {
                        model.AddDate = DateTime.Parse(dt.Rows[n]["AddDate"].ToString());
                    }
                    if (dt.Rows[n]["BeginDate"].ToString() != "")
                    {
                        model.BeginDate = DateTime.Parse(dt.Rows[n]["BeginDate"].ToString());
                    }
                    if (dt.Rows[n]["endDate"].ToString() != "")
                    {
                        model.endDate = DateTime.Parse(dt.Rows[n]["endDate"].ToString());
                    }
                    model.ItemCode = dt.Rows[n]["ItemCode"].ToString();
                    model.ItemName = dt.Rows[n]["ItemName"].ToString();
                    model.TaskType = dt.Rows[n]["TaskType"].ToString();
                    model.TaskLevelId = dt.Rows[n]["TaskLevelId"].ToString();
                    model.TaskNO = dt.Rows[n]["TaskNO"].ToString();
                    model.StanId = dt.Rows[n]["StanId"].ToString();
                    model.Professional = dt.Rows[n]["Professional"].ToString();
                    model.Type = dt.Rows[n]["Type"].ToString();
                    model.TypeDescription = dt.Rows[n]["TypeDescription"].ToString();
                    model.CheckStandard = dt.Rows[n]["CheckStandard"].ToString();
                    model.CheckWay = dt.Rows[n]["CheckWay"].ToString();
                    if (dt.Rows[n]["Point"].ToString() != "")
                    {
                        model.Point = decimal.Parse(dt.Rows[n]["Point"].ToString());
                    }
                    model.TaskPId = dt.Rows[n]["TaskPId"].ToString();
                    model.TaskDepCode = dt.Rows[n]["TaskDepCode"].ToString();
                    model.TaskRoleId = dt.Rows[n]["TaskRoleId"].ToString();
                    model.CheckRoleId = dt.Rows[n]["CheckRoleId"].ToString();
                    if (dt.Rows[n]["PointCoverage"].ToString() != "")
                    {
                        model.PointCoverage = decimal.Parse(dt.Rows[n]["PointCoverage"].ToString());
                    }
                    if (dt.Rows[n]["PointCoverageDone"].ToString() != "")
                    {
                        model.PointCoverageDone = decimal.Parse(dt.Rows[n]["PointCoverageDone"].ToString());
                    }
                    model.TaskStatus = dt.Rows[n]["TaskStatus"].ToString();
                    if (dt.Rows[n]["IsAbarbeitung"].ToString() != "")
                    {
                        model.IsAbarbeitung = int.Parse(dt.Rows[n]["IsAbarbeitung"].ToString());
                    }
                    if (dt.Rows[n]["IsDelete"].ToString() != "")
                    {
                        model.IsDelete = int.Parse(dt.Rows[n]["IsDelete"].ToString());
                    }
                    model.CheckResult = dt.Rows[n]["CheckResult"].ToString();
                    model.ClosePId = dt.Rows[n]["ClosePId"].ToString();
                    model.CloseReason = dt.Rows[n]["CloseReason"].ToString();
                    if (dt.Rows[n]["CloseTime"].ToString() != "")
                    {
                        model.CloseTime = DateTime.Parse(dt.Rows[n]["CloseTime"].ToString());
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

