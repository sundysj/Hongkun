using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.WorkFlow;
namespace MobileSoft.BLL.WorkFlow
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_WorkFlow_FlowNode 的摘要说明。
	/// </summary>
	public class Bll_Tb_WorkFlow_FlowNode
	{
		private readonly MobileSoft.DAL.WorkFlow.Dal_Tb_WorkFlow_FlowNode dal=new MobileSoft.DAL.WorkFlow.Dal_Tb_WorkFlow_FlowNode();
		public Bll_Tb_WorkFlow_FlowNode()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int InfoId)
		{
			return dal.Exists(InfoId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowNode model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowNode model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int InfoId)
		{
			
			dal.Delete(InfoId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowNode GetModel(int InfoId)
		{
			
			return dal.GetModel(InfoId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowNode GetModelByCache(int InfoId)
		{
			
			string CacheKey = "Tb_WorkFlow_FlowNodeModel-" + InfoId;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(InfoId);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowNode)objModel;
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
		public DataSet GetList(int Top,string strWhere,string fieldOrder)
		{
			return dal.GetList(Top,strWhere,fieldOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowNode> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowNode> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowNode> modelList = new List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowNode>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowNode model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowNode();
					if(dt.Rows[n]["InfoId"].ToString()!="")
					{
						model.InfoId=int.Parse(dt.Rows[n]["InfoId"].ToString());
					}
					if(dt.Rows[n]["Tb_WorkFlow_Instance_InfoId"].ToString()!="")
					{
						model.Tb_WorkFlow_Instance_InfoId=int.Parse(dt.Rows[n]["Tb_WorkFlow_Instance_InfoId"].ToString());
					}
					model.FlowNodeName=dt.Rows[n]["FlowNodeName"].ToString();
					if(dt.Rows[n]["FlowSort"].ToString()!="")
					{
						model.FlowSort=int.Parse(dt.Rows[n]["FlowSort"].ToString());
					}
					if(dt.Rows[n]["TimeOutDay"].ToString()!="")
					{
						model.TimeOutDay=int.Parse(dt.Rows[n]["TimeOutDay"].ToString());
					}
					if(dt.Rows[n]["TimeOutDays"].ToString()!="")
					{
						model.TimeOutDays=int.Parse(dt.Rows[n]["TimeOutDays"].ToString());
					}
					model.Tb_Dictionary_NodeOprMethod_DictionaryCode=dt.Rows[n]["Tb_Dictionary_NodeOprMethod_DictionaryCode"].ToString();
					model.Tb_Dictionary_NodeOprType_DictionaryCode=dt.Rows[n]["Tb_Dictionary_NodeOprType_DictionaryCode"].ToString();
					if(dt.Rows[n]["JumpFlowSort"].ToString()!="")
					{
						model.JumpFlowSort=int.Parse(dt.Rows[n]["JumpFlowSort"].ToString());
					}
					if(dt.Rows[n]["IsUpdateFlow"].ToString()!="")
					{
						model.IsUpdateFlow=int.Parse(dt.Rows[n]["IsUpdateFlow"].ToString());
					}
					model.Tb_Dictionary_OprState_DictionaryCode=dt.Rows[n]["Tb_Dictionary_OprState_DictionaryCode"].ToString();
					if(dt.Rows[n]["IsPrint"].ToString()!="")
					{
						model.IsPrint=int.Parse(dt.Rows[n]["IsPrint"].ToString());
					}
					if(dt.Rows[n]["IsStartUser"].ToString()!="")
					{
						model.IsStartUser=int.Parse(dt.Rows[n]["IsStartUser"].ToString());
					}
					if(dt.Rows[n]["WorkFlowStartDate"].ToString()!="")
					{
						model.WorkFlowStartDate=DateTime.Parse(dt.Rows[n]["WorkFlowStartDate"].ToString());
					}
					if(dt.Rows[n]["ReturnNode"].ToString()!="")
					{
						model.ReturnNode=int.Parse(dt.Rows[n]["ReturnNode"].ToString());
					}
					if(dt.Rows[n]["CheckLevel"].ToString()!="")
					{
						model.CheckLevel=int.Parse(dt.Rows[n]["CheckLevel"].ToString());
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
		public DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize,string SortField,int Sort)
		{
			return dal.GetList(out PageCount, out Counts, StrCondition, PageIndex, PageSize,SortField,Sort);
		}

		#endregion  成员方法

            public DataTable GetWorkFlowNodeList(int InstanceId, string DictionaryCode)
            {
                  return dal.GetWorkFlowNodeList(InstanceId, DictionaryCode);
            }
	}
}

