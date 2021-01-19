using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.WorkFlow;
namespace MobileSoft.BLL.WorkFlow
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_WorkFlow_InstanceTemp ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_WorkFlow_InstanceTemp
	{
		private readonly MobileSoft.DAL.WorkFlow.Dal_Tb_WorkFlow_InstanceTemp dal=new MobileSoft.DAL.WorkFlow.Dal_Tb_WorkFlow_InstanceTemp();
		public Bll_Tb_WorkFlow_InstanceTemp()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int InfoId)
		{
			return dal.Exists(InfoId);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_InstanceTemp model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_InstanceTemp model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int InfoId)
		{
			
			dal.Delete(InfoId);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_InstanceTemp GetModel(int InfoId)
		{
			
			return dal.GetModel(InfoId);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_InstanceTemp GetModelByCache(int InfoId)
		{
			
			string CacheKey = "Tb_WorkFlow_InstanceTempModel-" + InfoId;
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
			return (MobileSoft.Model.WorkFlow.Tb_WorkFlow_InstanceTemp)objModel;
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
		public List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_InstanceTemp> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_InstanceTemp> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_InstanceTemp> modelList = new List<MobileSoft.Model.WorkFlow.Tb_WorkFlow_InstanceTemp>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.WorkFlow.Tb_WorkFlow_InstanceTemp model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.WorkFlow.Tb_WorkFlow_InstanceTemp();
					if(dt.Rows[n]["InfoId"].ToString()!="")
					{
						model.InfoId=int.Parse(dt.Rows[n]["InfoId"].ToString());
					}
					if(dt.Rows[n]["InstanceId"].ToString()!="")
					{
						model.InstanceId=int.Parse(dt.Rows[n]["InstanceId"].ToString());
					}
					model.Tb_Dictionary_InstanceType_DictionaryCode=dt.Rows[n]["Tb_Dictionary_InstanceType_DictionaryCode"].ToString();
					model.InstanceMark=dt.Rows[n]["InstanceMark"].ToString();
					if(dt.Rows[n]["NoticeMsg"].ToString()!="")
					{
						model.NoticeMsg=int.Parse(dt.Rows[n]["NoticeMsg"].ToString());
					}
					if(dt.Rows[n]["NoticeMail"].ToString()!="")
					{
						model.NoticeMail=int.Parse(dt.Rows[n]["NoticeMail"].ToString());
					}
					if(dt.Rows[n]["NoticePhone"].ToString()!="")
					{
						model.NoticePhone=int.Parse(dt.Rows[n]["NoticePhone"].ToString());
					}
					if(dt.Rows[n]["NoticeHaveDeal"].ToString()!="")
					{
						model.NoticeHaveDeal=int.Parse(dt.Rows[n]["NoticeHaveDeal"].ToString());
					}
					if(dt.Rows[n]["NoticeStartDeal"].ToString()!="")
					{
						model.NoticeStartDeal=int.Parse(dt.Rows[n]["NoticeStartDeal"].ToString());
					}
					if(dt.Rows[n]["NoticeOtherUser"].ToString()!="")
					{
						model.NoticeOtherUser=int.Parse(dt.Rows[n]["NoticeOtherUser"].ToString());
					}
					if(dt.Rows[n]["IsComplete"].ToString()!="")
					{
						model.IsComplete=int.Parse(dt.Rows[n]["IsComplete"].ToString());
					}
					if(dt.Rows[n]["IsSuccess"].ToString()!="")
					{
						model.IsSuccess=int.Parse(dt.Rows[n]["IsSuccess"].ToString());
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

