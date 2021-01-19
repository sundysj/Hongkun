using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Unify;
namespace MobileSoft.BLL.Unify
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Unify_Community ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Unify_Community
	{
        private readonly MobileSoft.DAL.Unify.Dal_Tb_Unify_Community dal = new MobileSoft.DAL.Unify.Dal_Tb_Unify_Community();
		public Bll_Tb_Unify_Community()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(Guid CommSynchCode)
		{
			return dal.Exists(CommSynchCode);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(ehome.Model.Unify.Tb_Unify_Community model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ehome.Model.Unify.Tb_Unify_Community model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(Guid CommSynchCode)
		{
			
			dal.Delete(CommSynchCode);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ehome.Model.Unify.Tb_Unify_Community GetModel(Guid CommSynchCode)
		{
			
			return dal.GetModel(CommSynchCode);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public ehome.Model.Unify.Tb_Unify_Community GetModelByCache(Guid CommSynchCode)
		{
			
			string CacheKey = "Tb_Unify_CommunityModel-" + CommSynchCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CommSynchCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ehome.Model.Unify.Tb_Unify_Community)objModel;
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
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string fieldOrder, string fieldList)
        {
            return dal.GetList(Top, strWhere, fieldOrder, fieldList);
        }
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<ehome.Model.Unify.Tb_Unify_Community> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<ehome.Model.Unify.Tb_Unify_Community> DataTableToList(DataTable dt)
		{
			List<ehome.Model.Unify.Tb_Unify_Community> modelList = new List<ehome.Model.Unify.Tb_Unify_Community>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ehome.Model.Unify.Tb_Unify_Community model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ehome.Model.Unify.Tb_Unify_Community();
					if(dt.Rows[n]["CommSynchCode"].ToString()!="")
					{
						model.CommSynchCode=new Guid(dt.Rows[n]["CommSynchCode"].ToString());
					}
					if(dt.Rows[n]["CorpSynchCode"].ToString()!="")
					{
						model.CorpSynchCode=new Guid(dt.Rows[n]["CorpSynchCode"].ToString());
					}
					//model.UnCommID=dt.Rows[n]["UnCommID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					model.CommName=dt.Rows[n]["CommName"].ToString();
					model.CommKind=dt.Rows[n]["CommKind"].ToString();
					if(dt.Rows[n]["ManageTime"].ToString()!="")
					{
						model.ManageTime=DateTime.Parse(dt.Rows[n]["ManageTime"].ToString());
					}
					model.ManageKind=dt.Rows[n]["ManageKind"].ToString();
					if(dt.Rows[n]["ProvinceID"].ToString()!="")
					{
						model.ProvinceID=int.Parse(dt.Rows[n]["ProvinceID"].ToString());
					}
					if(dt.Rows[n]["CityID"].ToString()!="")
					{
						model.CityID=int.Parse(dt.Rows[n]["CityID"].ToString());
					}
					if(dt.Rows[n]["BoroughID"].ToString()!="")
					{
						model.BoroughID=int.Parse(dt.Rows[n]["BoroughID"].ToString());
					}
					model.Street=dt.Rows[n]["Street"].ToString();
					model.GateSign=dt.Rows[n]["GateSign"].ToString();
					model.CommAddress=dt.Rows[n]["CommAddress"].ToString();
					model.CommPost=dt.Rows[n]["CommPost"].ToString();
					if(dt.Rows[n]["RegDate"].ToString()!="")
					{
						model.RegDate=DateTime.Parse(dt.Rows[n]["RegDate"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["SynchFlag"].ToString()!="")
					{
						model.SynchFlag=int.Parse(dt.Rows[n]["SynchFlag"].ToString());
					}
					if(dt.Rows[n]["CommSNum"].ToString()!="")
					{
						model.CommSNum=int.Parse(dt.Rows[n]["CommSNum"].ToString());
					}
					model.CommunityName=dt.Rows[n]["CommunityName"].ToString();
					model.CommunityCode=dt.Rows[n]["CommunityCode"].ToString();
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

