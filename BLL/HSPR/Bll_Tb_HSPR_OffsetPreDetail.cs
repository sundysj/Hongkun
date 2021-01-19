using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_HSPR_OffsetPreDetail ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_HSPR_OffsetPreDetail
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_OffsetPreDetail dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_OffsetPreDetail();
		public Bll_Tb_HSPR_OffsetPreDetail()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long IID)
		{
			return dal.Exists(IID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(MobileSoft.Model.HSPR.Tb_HSPR_OffsetPreDetail model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_OffsetPreDetail model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long IID)
		{
			
			dal.Delete(IID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_OffsetPreDetail GetModel(long IID)
		{
			
			return dal.GetModel(IID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_OffsetPreDetail GetModelByCache(long IID)
		{
			
			string CacheKey = "Tb_HSPR_OffsetPreDetailModel-" + IID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(IID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_OffsetPreDetail)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_OffsetPreDetail> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_OffsetPreDetail> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_OffsetPreDetail> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_OffsetPreDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_OffsetPreDetail model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_OffsetPreDetail();
					//model.IID=dt.Rows[n]["IID"].ToString();
					//model.OffsetID=dt.Rows[n]["OffsetID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					//model.CostID=dt.Rows[n]["CostID"].ToString();
					//model.CustID=dt.Rows[n]["CustID"].ToString();
					//model.RoomID=dt.Rows[n]["RoomID"].ToString();
					if(dt.Rows[n]["FeesDueDate"].ToString()!="")
					{
						model.FeesDueDate=DateTime.Parse(dt.Rows[n]["FeesDueDate"].ToString());
					}
					if(dt.Rows[n]["DebtsAmount"].ToString()!="")
					{
						model.DebtsAmount=decimal.Parse(dt.Rows[n]["DebtsAmount"].ToString());
					}
					if(dt.Rows[n]["OldPrecAmount"].ToString()!="")
					{
						model.OldPrecAmount=decimal.Parse(dt.Rows[n]["OldPrecAmount"].ToString());
					}
					if(dt.Rows[n]["NewPrecAmount"].ToString()!="")
					{
						model.NewPrecAmount=decimal.Parse(dt.Rows[n]["NewPrecAmount"].ToString());
					}
					if(dt.Rows[n]["OffsetAmount"].ToString()!="")
					{
						model.OffsetAmount=decimal.Parse(dt.Rows[n]["OffsetAmount"].ToString());
					}
					//model.FeesID=dt.Rows[n]["FeesID"].ToString();
					//model.PrecID=dt.Rows[n]["PrecID"].ToString();
					if(dt.Rows[n]["TakeWise"].ToString()!="")
					{
						model.TakeWise=int.Parse(dt.Rows[n]["TakeWise"].ToString());
					}
					if(dt.Rows[n]["IsAudit"].ToString()!="")
					{
						model.IsAudit=int.Parse(dt.Rows[n]["IsAudit"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["OffsetLateFeeAmount"].ToString()!="")
					{
						model.OffsetLateFeeAmount=decimal.Parse(dt.Rows[n]["OffsetLateFeeAmount"].ToString());
					}
					//model.ReceID=dt.Rows[n]["ReceID"].ToString();
					if(dt.Rows[n]["ReceCode"].ToString()!="")
					{
						model.ReceCode=new Guid(dt.Rows[n]["ReceCode"].ToString());
					}
					model.DelUserCode=dt.Rows[n]["DelUserCode"].ToString();
					if(dt.Rows[n]["DelDate"].ToString()!="")
					{
						model.DelDate=DateTime.Parse(dt.Rows[n]["DelDate"].ToString());
					}
					model.ChangeMemo=dt.Rows[n]["ChangeMemo"].ToString();
					if(dt.Rows[n]["IsProperty"].ToString()!="")
					{
						model.IsProperty=int.Parse(dt.Rows[n]["IsProperty"].ToString());
					}
					if(dt.Rows[n]["IsAutoPrec"].ToString()!="")
					{
						model.IsAutoPrec=int.Parse(dt.Rows[n]["IsAutoPrec"].ToString());
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

            public void HSPROffsetPreDetailDelete(long IID, string UserCode, string ChangeMemo)
            {
                  dal.HSPROffsetPreDetailDelete(IID, UserCode, ChangeMemo);
            }
	}
}

