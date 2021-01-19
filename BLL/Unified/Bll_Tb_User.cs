using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Unified;
namespace MobileSoft.BLL.Unified
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_User ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_User
	{
		private readonly MobileSoft.DAL.Unified.Dal_Tb_User dal=new MobileSoft.DAL.Unified.Dal_Tb_User();
		public Bll_Tb_User()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.Unified.Tb_User model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Unified.Tb_User model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string Id)
		{
			
			dal.Delete(Id);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Unified.Tb_User GetModel(string Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Unified.Tb_User GetModelByCache(string Id)
		{
			
			string CacheKey = "Tb_UserModel-" + Id;
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
				catch{}
			}
			return (MobileSoft.Model.Unified.Tb_User)objModel;
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Unified.Tb_User> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Unified.Tb_User> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Unified.Tb_User> modelList = new List<MobileSoft.Model.Unified.Tb_User>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Unified.Tb_User model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Unified.Tb_User();
					model.Id=dt.Rows[n]["Id"].ToString();
					model.Name=dt.Rows[n]["Name"].ToString();
					model.Mobile=dt.Rows[n]["Mobile"].ToString();
					model.Email=dt.Rows[n]["Email"].ToString();
					model.QQ=dt.Rows[n]["QQ"].ToString();
					model.QQToken=dt.Rows[n]["QQToken"].ToString();
					model.WeChatNum=dt.Rows[n]["WeChatNum"].ToString();
					model.WeChatToken=dt.Rows[n]["WeChatToken"].ToString();
					model.NickName=dt.Rows[n]["NickName"].ToString();
					model.Pwd=dt.Rows[n]["Pwd"].ToString();
                    model.UserPic = dt.Rows[n]["UserPic"].ToString();
                    model.Sex =Convert.ToInt32( dt.Rows[n]["Sex"]);
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

