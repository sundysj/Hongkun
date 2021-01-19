using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_HSPR_KeyDoorDeviceSetting ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_HSPR_KeyDoorDeviceSetting
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_KeyDoorDeviceSetting dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_KeyDoorDeviceSetting();
		public Bll_Tb_HSPR_KeyDoorDeviceSetting()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long DoorID)
		{
			return dal.Exists(DoorID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_KeyDoorDeviceSetting model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_KeyDoorDeviceSetting model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long DoorID)
		{
			
			dal.Delete(DoorID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_KeyDoorDeviceSetting GetModel(long DoorID)
		{
			
			return dal.GetModel(DoorID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_KeyDoorDeviceSetting GetModelByCache(long DoorID)
		{
			
			string CacheKey = "Tb_HSPR_KeyDoorDeviceSettingModel-" + DoorID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(DoorID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_KeyDoorDeviceSetting)objModel;
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_KeyDoorDeviceSetting> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_KeyDoorDeviceSetting> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_KeyDoorDeviceSetting> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_KeyDoorDeviceSetting>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_KeyDoorDeviceSetting model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_KeyDoorDeviceSetting();
					//model.DoorID=dt.Rows[n]["DoorID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					if(dt.Rows[n]["DoorType"].ToString()!="")
					{
						model.DoorType=int.Parse(dt.Rows[n]["DoorType"].ToString());
					}
					model.DoorNum=dt.Rows[n]["DoorNum"].ToString();
					model.DoorName=dt.Rows[n]["DoorName"].ToString();
					model.DeviceAddRess=dt.Rows[n]["DeviceAddRess"].ToString();
					if(dt.Rows[n]["BuildSNum"].ToString()!="")
					{
						model.BuildSNum=int.Parse(dt.Rows[n]["BuildSNum"].ToString());
					}
					if(dt.Rows[n]["UnitSNum"].ToString()!="")
					{
						model.UnitSNum=int.Parse(dt.Rows[n]["UnitSNum"].ToString());
					}
					model.Memo=dt.Rows[n]["Memo"].ToString();
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
