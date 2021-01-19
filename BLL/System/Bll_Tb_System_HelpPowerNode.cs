using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.System;
namespace MobileSoft.BLL.System
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_System_HelpPowerNode ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_System_HelpPowerNode
	{
		private readonly MobileSoft.DAL.System.Dal_Tb_System_HelpPowerNode dal=new MobileSoft.DAL.System.Dal_Tb_System_HelpPowerNode();
		public Bll_Tb_System_HelpPowerNode()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int CorpID,string PNodeCode,long IID)
		{
			return dal.Exists(CorpID,PNodeCode,IID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(MobileSoft.Model.System.Tb_System_HelpPowerNode model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_HelpPowerNode model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int CorpID,string PNodeCode,long IID)
		{
			
			dal.Delete(CorpID,PNodeCode,IID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.System.Tb_System_HelpPowerNode GetModel(int CorpID,string PNodeCode,long IID)
		{
			
			return dal.GetModel(CorpID,PNodeCode,IID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.System.Tb_System_HelpPowerNode GetModelByCache(int CorpID,string PNodeCode,long IID)
		{
			
			string CacheKey = "Tb_System_HelpPowerNodeModel-" + CorpID+PNodeCode+IID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CorpID,PNodeCode,IID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.System.Tb_System_HelpPowerNode)objModel;
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
		public DataSet GetList(int Top,string strWhere,string fieldOrder)
		{
			return dal.GetList(Top,strWhere,fieldOrder);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.System.Tb_System_HelpPowerNode> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.System.Tb_System_HelpPowerNode> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.System.Tb_System_HelpPowerNode> modelList = new List<MobileSoft.Model.System.Tb_System_HelpPowerNode>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.System.Tb_System_HelpPowerNode model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.System.Tb_System_HelpPowerNode();
					//model.IID=dt.Rows[n]["IID"].ToString();
					if(dt.Rows[n]["CorpID"].ToString()!="")
					{
						model.CorpID=int.Parse(dt.Rows[n]["CorpID"].ToString());
					}
					model.PNodeCode=dt.Rows[n]["PNodeCode"].ToString();
					model.PNodeName=dt.Rows[n]["PNodeName"].ToString();
					if(dt.Rows[n]["InPopedom"].ToString()!="")
					{
						model.InPopedom=int.Parse(dt.Rows[n]["InPopedom"].ToString());
					}
					if(dt.Rows[n]["NodeType"].ToString()!="")
					{
						model.NodeType=int.Parse(dt.Rows[n]["NodeType"].ToString());
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
		/// ��������б�
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize,string SortField,int Sort)
		{
			return dal.GetList(out PageCount, out Counts, StrCondition, PageIndex, PageSize,SortField,Sort);
		}

		#endregion  ��Ա����
	}
}

