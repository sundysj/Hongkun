using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_HSPR_CommunitySuggestions ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_HSPR_CommunitySuggestions
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CommunitySuggestions dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_CommunitySuggestions();
		public Bll_Tb_HSPR_CommunitySuggestions()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string SuggestionsID)
		{
			return dal.Exists(SuggestionsID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CommunitySuggestions model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CommunitySuggestions model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string SuggestionsID)
		{
			
			dal.Delete(SuggestionsID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommunitySuggestions GetModel(string SuggestionsID)
		{
			
			return dal.GetModel(SuggestionsID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommunitySuggestions GetModelByCache(string SuggestionsID)
		{
			
			string CacheKey = "Tb_HSPR_CommunitySuggestionsModel-" + SuggestionsID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(SuggestionsID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_CommunitySuggestions)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CommunitySuggestions> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_CommunitySuggestions> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_CommunitySuggestions> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_CommunitySuggestions>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_CommunitySuggestions model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_CommunitySuggestions();
					model.SuggestionsID=dt.Rows[n]["SuggestionsID"].ToString();
					//model.CommID=dt.Rows[n]["CommID"].ToString();
					//model.CustID=dt.Rows[n]["CustID"].ToString();
					model.CustName=dt.Rows[n]["CustName"].ToString();
					//model.RoomID=dt.Rows[n]["RoomID"].ToString();
					model.RoomSign=dt.Rows[n]["RoomSign"].ToString();
					model.SuggestionsType=dt.Rows[n]["SuggestionsType"].ToString();
					model.SuggestionsTitle=dt.Rows[n]["SuggestionsTitle"].ToString();
					model.SuggestionsContent=dt.Rows[n]["SuggestionsContent"].ToString();
					if(dt.Rows[n]["IssueDate"].ToString()!="")
					{
						model.IssueDate=DateTime.Parse(dt.Rows[n]["IssueDate"].ToString());
					}
					model.LinkPhone=dt.Rows[n]["LinkPhone"].ToString();
					model.SuggestionsImages=dt.Rows[n]["SuggestionsImages"].ToString();
					model.ReplyStats=dt.Rows[n]["ReplyStats"].ToString();
					if(dt.Rows[n]["ReplyDate"].ToString()!="")
					{
						model.ReplyDate=DateTime.Parse(dt.Rows[n]["ReplyDate"].ToString());
					}
					model.ReplyContent=dt.Rows[n]["ReplyContent"].ToString();
					model.CustEvaluation=dt.Rows[n]["CustEvaluation"].ToString();
					if(dt.Rows[n]["EvaluationDate"].ToString()!="")
					{
						model.EvaluationDate=DateTime.Parse(dt.Rows[n]["EvaluationDate"].ToString());
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

