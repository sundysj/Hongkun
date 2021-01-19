using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Information;
namespace MobileSoft.BLL.Information
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Information_ConsumerGuid ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Information_ConsumerGuid
	{
		private readonly MobileSoft.DAL.Information.Dal_Tb_Information_ConsumerGuid dal=new MobileSoft.DAL.Information.Dal_Tb_Information_ConsumerGuid();
		public Bll_Tb_Information_ConsumerGuid()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long GuideId)
		{
			return dal.Exists(GuideId);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public long Add(MobileSoft.Model.Information.Tb_Information_ConsumerGuid model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Information.Tb_Information_ConsumerGuid model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long GuideId)
		{
			
			dal.Delete(GuideId);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Information.Tb_Information_ConsumerGuid GetModel(long GuideId)
		{
			
			return dal.GetModel(GuideId);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Information.Tb_Information_ConsumerGuid GetModelByCache(long GuideId)
		{
			
			string CacheKey = "Tb_Information_ConsumerGuidModel-" + GuideId;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(GuideId);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Information.Tb_Information_ConsumerGuid)objModel;
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
		public List<MobileSoft.Model.Information.Tb_Information_ConsumerGuid> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Information.Tb_Information_ConsumerGuid> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Information.Tb_Information_ConsumerGuid> modelList = new List<MobileSoft.Model.Information.Tb_Information_ConsumerGuid>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Information.Tb_Information_ConsumerGuid model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Information.Tb_Information_ConsumerGuid();
					//model.GuideId=dt.Rows[n]["GuideId"].ToString();
					//model.BussId=dt.Rows[n]["BussId"].ToString();
					model.Title=dt.Rows[n]["Title"].ToString();
					model.GudPublisher=dt.Rows[n]["GudPublisher"].ToString();
					if(dt.Rows[n]["PubulishDate"].ToString()!="")
					{
						model.PubulishDate=DateTime.Parse(dt.Rows[n]["PubulishDate"].ToString());
					}
					model.GudContent=dt.Rows[n]["GudContent"].ToString();
					model.GudImage=dt.Rows[n]["GudImage"].ToString();
					//model.NumID=dt.Rows[n]["NumID"].ToString();
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

