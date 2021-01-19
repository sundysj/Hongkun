using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.System;
namespace MobileSoft.BLL.System
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_System_PowerNode 的摘要说明。
	/// </summary>
	public class Bll_Tb_System_PowerNode
	{
		private readonly MobileSoft.DAL.System.Dal_Tb_System_PowerNode dal=new MobileSoft.DAL.System.Dal_Tb_System_PowerNode();
		public Bll_Tb_System_PowerNode()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string PNodeCode)
		{
			return dal.Exists(PNodeCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.System.Tb_System_PowerNode model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_PowerNode model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string PNodeCode)
		{
			
			dal.Delete(PNodeCode);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_PowerNode GetModel(string PNodeCode)
		{
			
			return dal.GetModel(PNodeCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.System.Tb_System_PowerNode GetModelByCache(string PNodeCode)
		{
			
			string CacheKey = "Tb_System_PowerNodeModel-" + PNodeCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(PNodeCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.System.Tb_System_PowerNode)objModel;
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
		public List<MobileSoft.Model.System.Tb_System_PowerNode> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.System.Tb_System_PowerNode> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.System.Tb_System_PowerNode> modelList = new List<MobileSoft.Model.System.Tb_System_PowerNode>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.System.Tb_System_PowerNode model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.System.Tb_System_PowerNode();
					model.PNodeCode=dt.Rows[n]["PNodeCode"].ToString();
					model.PNodeName=dt.Rows[n]["PNodeName"].ToString();
					model.URLPage=dt.Rows[n]["URLPage"].ToString();
					model.URLTarget=dt.Rows[n]["URLTarget"].ToString();
					model.BackTitleImg=dt.Rows[n]["BackTitleImg"].ToString();
					model.Narrate=dt.Rows[n]["Narrate"].ToString();
					if(dt.Rows[n]["InPopedom"].ToString()!="")
					{
						model.InPopedom=int.Parse(dt.Rows[n]["InPopedom"].ToString());
					}
					model.Functions=dt.Rows[n]["Functions"].ToString();
					if(dt.Rows[n]["NodeType"].ToString()!="")
					{
						model.NodeType=int.Parse(dt.Rows[n]["NodeType"].ToString());
					}
					if(dt.Rows[n]["PNodeSort"].ToString()!="")
					{
						model.PNodeSort=int.Parse(dt.Rows[n]["PNodeSort"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
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
	}
}

