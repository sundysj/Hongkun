using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.WorkFlow;
namespace MobileSoft.BLL.WorkFlow
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_WorkFlow_GeneralDecompose ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_WorkFlow_GeneralDecompose
	{
		private readonly MobileSoft.DAL.WorkFlow.Dal_Tb_WorkFlow_GeneralDecompose dal=new MobileSoft.DAL.WorkFlow.Dal_Tb_WorkFlow_GeneralDecompose();
		public Bll_Tb_WorkFlow_GeneralDecompose()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long CutID)
		{
			return dal.Exists(CutID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralDecompose model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralDecompose model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long CutID)
		{
			
			dal.Delete(CutID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralDecompose GetModel(long CutID)
		{
			
			return dal.GetModel(CutID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralDecompose GetModelByCache(long CutID)
		{
			
			string CacheKey = "Tb_WorkFlow_GeneralDecomposeModel-" + CutID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CutID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralDecompose)objModel;
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
		public List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralDecompose> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralDecompose> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralDecompose> modelList = new List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralDecompose>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralDecompose model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralDecompose();
					if(dt.Rows[n]["GeneralDecomposeCode"].ToString()!="")
					{
						model.GeneralDecomposeCode=new Guid(dt.Rows[n]["GeneralDecomposeCode"].ToString());
					}
					if(dt.Rows[n]["GeneralMainCode"].ToString()!="")
					{
						model.GeneralMainCode=new Guid(dt.Rows[n]["GeneralMainCode"].ToString());
					}
					//model.CutID=dt.Rows[n]["CutID"].ToString();
					model.DecomposeCode=dt.Rows[n]["DecomposeCode"].ToString();
					model.DisposeMan=dt.Rows[n]["DisposeMan"].ToString();
					if(dt.Rows[n]["DisposePope"].ToString()!="")
					{
						model.DisposePope=int.Parse(dt.Rows[n]["DisposePope"].ToString());
					}
					if(dt.Rows[n]["IsFinishPope"].ToString()!="")
					{
						model.IsFinishPope=int.Parse(dt.Rows[n]["IsFinishPope"].ToString());
					}
					if(dt.Rows[n]["IsRemind"].ToString()!="")
					{
						model.IsRemind=int.Parse(dt.Rows[n]["IsRemind"].ToString());
					}
					if(dt.Rows[n]["RemindTime"].ToString()!="")
					{
						model.RemindTime=DateTime.Parse(dt.Rows[n]["RemindTime"].ToString());
					}
					model.RemindMode=dt.Rows[n]["RemindMode"].ToString();
					if(dt.Rows[n]["InstTime"].ToString()!="")
					{
						model.InstTime=DateTime.Parse(dt.Rows[n]["InstTime"].ToString());
					}
					if(dt.Rows[n]["SignTime"].ToString()!="")
					{
						model.SignTime=DateTime.Parse(dt.Rows[n]["SignTime"].ToString());
					}
					model.Memo=dt.Rows[n]["Memo"].ToString();
					if(dt.Rows[n]["RemindState"].ToString()!="")
					{
						model.RemindState=int.Parse(dt.Rows[n]["RemindState"].ToString());
					}
					if(dt.Rows[n]["DisposeResult"].ToString()!="")
					{
						model.DisposeResult=int.Parse(dt.Rows[n]["DisposeResult"].ToString());
					}
					if(dt.Rows[n]["DisposeTime"].ToString()!="")
					{
						model.DisposeTime=DateTime.Parse(dt.Rows[n]["DisposeTime"].ToString());
					}
					if(dt.Rows[n]["DisposeState"].ToString()!="")
					{
						model.DisposeState=int.Parse(dt.Rows[n]["DisposeState"].ToString());
					}
					if(dt.Rows[n]["msrepl_tran_version"].ToString()!="")
					{
						model.msrepl_tran_version=new Guid(dt.Rows[n]["msrepl_tran_version"].ToString());
					}
					model.TYPE=dt.Rows[n]["TYPE"].ToString();
					model.DisposeDepCode=dt.Rows[n]["DisposeDepCode"].ToString();
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

