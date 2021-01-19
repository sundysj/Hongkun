using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Order;
namespace MobileSoft.BLL.Order
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Order_RegistWare ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Order_RegistWare
	{
        private readonly MobileSoft.DAL.Order.Dal_Tb_Order_RegistWare dal = new MobileSoft.DAL.Order.Dal_Tb_Order_RegistWare();
		public Bll_Tb_Order_RegistWare()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(Guid RegistWareID)
		{
			return dal.Exists(RegistWareID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.Order.Tb_Order_RegistWare model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Order.Tb_Order_RegistWare model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(Guid RegistWareID)
		{
			
			dal.Delete(RegistWareID);
		}
        public void DeleteNotIn(string Where)
        {
            dal.DeleteNotIn(Where);
        }
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Order.Tb_Order_RegistWare GetModel(Guid RegistWareID)
		{
			
			return dal.GetModel(RegistWareID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Order.Tb_Order_RegistWare GetModelByCache(Guid RegistWareID)
		{
			
			string CacheKey = "Tb_Order_RegistWareModel-" + RegistWareID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(RegistWareID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Order.Tb_Order_RegistWare)objModel;
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
		public List<MobileSoft.Model.Order.Tb_Order_RegistWare> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Order.Tb_Order_RegistWare> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Order.Tb_Order_RegistWare> modelList = new List<MobileSoft.Model.Order.Tb_Order_RegistWare>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Order.Tb_Order_RegistWare model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Order.Tb_Order_RegistWare();
					if(dt.Rows[n]["RegistWareID"].ToString()!="")
					{
						model.RegistWareID=new Guid(dt.Rows[n]["RegistWareID"].ToString());
					}
					//model.ReleaseID=dt.Rows[n]["ReleaseID"].ToString();
					if(dt.Rows[n]["Count"].ToString()!="")
					{
						model.Count=decimal.Parse(dt.Rows[n]["Count"].ToString());
					}
					model.RegistID=dt.Rows[n]["RegistID"].ToString();
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

