using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Resources;
namespace MobileSoft.BLL.Resources
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Resources_Release ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Resources_Release
	{
		private readonly MobileSoft.DAL.Resources.Dal_Tb_Resources_Release dal=new MobileSoft.DAL.Resources.Dal_Tb_Resources_Release();
		public Bll_Tb_Resources_Release()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long ReleaseID)
		{
			return dal.Exists(ReleaseID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.Resources.Tb_Resources_Release model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_Release model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long ReleaseID)
		{
			
			dal.Delete(ReleaseID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_Release GetModel(long ReleaseID)
		{
			
			return dal.GetModel(ReleaseID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_Release GetModelByCache(long ReleaseID)
		{
			
			string CacheKey = "Tb_Resources_ReleaseModel-" + ReleaseID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ReleaseID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Resources.Tb_Resources_Release)objModel;
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
		public List<MobileSoft.Model.Resources.Tb_Resources_Release> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Resources.Tb_Resources_Release> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Resources.Tb_Resources_Release> modelList = new List<MobileSoft.Model.Resources.Tb_Resources_Release>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Resources.Tb_Resources_Release model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Resources.Tb_Resources_Release();
					//model.ReleaseID=dt.Rows[n]["ReleaseID"].ToString();
					//model.ResourcesID=dt.Rows[n]["ResourcesID"].ToString();
					model.ReleaseAdContent=dt.Rows[n]["ReleaseAdContent"].ToString();
					if(dt.Rows[n]["ReleaseCount"].ToString()!="")
					{
						model.ReleaseCount=decimal.Parse(dt.Rows[n]["ReleaseCount"].ToString());
					}
					if(dt.Rows[n]["IsGroupBuy"].ToString()!="")
					{
						if((dt.Rows[n]["IsGroupBuy"].ToString()=="1")||(dt.Rows[n]["IsGroupBuy"].ToString().ToLower()=="true"))
						{
						model.IsGroupBuy=true;
						}
						else
						{
							model.IsGroupBuy=false;
						}
					}
					if(dt.Rows[n]["GroupBuyPrice"].ToString()!="")
					{
						model.GroupBuyPrice=decimal.Parse(dt.Rows[n]["GroupBuyPrice"].ToString());
					}
					if(dt.Rows[n]["GroupBuyStartData"].ToString()!="")
					{
						model.GroupBuyStartData=DateTime.Parse(dt.Rows[n]["GroupBuyStartData"].ToString());
					}
					if(dt.Rows[n]["GroupBuyEndData"].ToString()!="")
					{
						model.GroupBuyEndData=DateTime.Parse(dt.Rows[n]["GroupBuyEndData"].ToString());
					}
					model.PaymentType=dt.Rows[n]["PaymentType"].ToString();
					//model.BussId=dt.Rows[n]["BussId"].ToString();
					if(dt.Rows[n]["IsStop"].ToString()!="")
					{
						if((dt.Rows[n]["IsStop"].ToString()=="1")||(dt.Rows[n]["IsStop"].ToString().ToLower()=="true"))
						{
						model.IsStop=true;
						}
						else
						{
							model.IsStop=false;
						}
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

        public DataSet GetFreeList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort)
        {
            return dal.GetFreeList(out PageCount, out Counts, StrCondition, PageIndex, PageSize, SortField, Sort);
        }
		#endregion  ��Ա����
	}
}

