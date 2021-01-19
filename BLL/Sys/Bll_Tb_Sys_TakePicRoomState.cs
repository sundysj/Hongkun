using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Sys;
namespace MobileSoft.BLL.Sys
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Sys_TakePicRoomState ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Sys_TakePicRoomState
	{
		private readonly MobileSoft.DAL.Sys.Dal_Tb_Sys_TakePicRoomState dal=new MobileSoft.DAL.Sys.Dal_Tb_Sys_TakePicRoomState();
		public Bll_Tb_Sys_TakePicRoomState()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long StatID)
		{
			return dal.Exists(StatID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(MobileSoft.Model.Sys.Tb_Sys_TakePicRoomState model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Sys.Tb_Sys_TakePicRoomState model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long StatID)
		{
			
			dal.Delete(StatID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_TakePicRoomState GetModel(long StatID)
		{
			
			return dal.GetModel(StatID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_TakePicRoomState GetModelByCache(long StatID)
		{
			
			string CacheKey = "Tb_Sys_TakePicRoomStateModel-" + StatID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(StatID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Sys.Tb_Sys_TakePicRoomState)objModel;
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
		public List<MobileSoft.Model.Sys.Tb_Sys_TakePicRoomState> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Sys.Tb_Sys_TakePicRoomState> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Sys.Tb_Sys_TakePicRoomState> modelList = new List<MobileSoft.Model.Sys.Tb_Sys_TakePicRoomState>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Sys.Tb_Sys_TakePicRoomState model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Sys.Tb_Sys_TakePicRoomState();
					//model.StatID=dt.Rows[n]["StatID"].ToString();
					if(dt.Rows[n]["StatType"].ToString()!="")
					{
						model.StatType=int.Parse(dt.Rows[n]["StatType"].ToString());
					}
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					model.OrganCode=dt.Rows[n]["OrganCode"].ToString();
					if(dt.Rows[n]["StatDate"].ToString()!="")
					{
						model.StatDate=DateTime.Parse(dt.Rows[n]["StatDate"].ToString());
					}
					if(dt.Rows[n]["RoomState"].ToString()!="")
					{
						model.RoomState=int.Parse(dt.Rows[n]["RoomState"].ToString());
					}
					if(dt.Rows[n]["Counts"].ToString()!="")
					{
						model.Counts=int.Parse(dt.Rows[n]["Counts"].ToString());
					}
					if(dt.Rows[n]["Counts0"].ToString()!="")
					{
						model.Counts0=int.Parse(dt.Rows[n]["Counts0"].ToString());
					}
					if(dt.Rows[n]["Counts1"].ToString()!="")
					{
						model.Counts1=int.Parse(dt.Rows[n]["Counts1"].ToString());
					}
					if(dt.Rows[n]["Counts2"].ToString()!="")
					{
						model.Counts2=int.Parse(dt.Rows[n]["Counts2"].ToString());
					}
					if(dt.Rows[n]["Counts3"].ToString()!="")
					{
						model.Counts3=int.Parse(dt.Rows[n]["Counts3"].ToString());
					}
					if(dt.Rows[n]["Counts4"].ToString()!="")
					{
						model.Counts4=int.Parse(dt.Rows[n]["Counts4"].ToString());
					}
					if(dt.Rows[n]["Counts5"].ToString()!="")
					{
						model.Counts5=int.Parse(dt.Rows[n]["Counts5"].ToString());
					}
					if(dt.Rows[n]["Counts6"].ToString()!="")
					{
						model.Counts6=int.Parse(dt.Rows[n]["Counts6"].ToString());
					}
					if(dt.Rows[n]["Counts7"].ToString()!="")
					{
						model.Counts7=int.Parse(dt.Rows[n]["Counts7"].ToString());
					}
					if(dt.Rows[n]["Counts8"].ToString()!="")
					{
						model.Counts8=int.Parse(dt.Rows[n]["Counts8"].ToString());
					}
					if(dt.Rows[n]["Area"].ToString()!="")
					{
						model.Area=decimal.Parse(dt.Rows[n]["Area"].ToString());
					}
					if(dt.Rows[n]["Area0"].ToString()!="")
					{
						model.Area0=decimal.Parse(dt.Rows[n]["Area0"].ToString());
					}
					if(dt.Rows[n]["Area1"].ToString()!="")
					{
						model.Area1=decimal.Parse(dt.Rows[n]["Area1"].ToString());
					}
					if(dt.Rows[n]["Area2"].ToString()!="")
					{
						model.Area2=decimal.Parse(dt.Rows[n]["Area2"].ToString());
					}
					if(dt.Rows[n]["Area3"].ToString()!="")
					{
						model.Area3=decimal.Parse(dt.Rows[n]["Area3"].ToString());
					}
					if(dt.Rows[n]["Area4"].ToString()!="")
					{
						model.Area4=decimal.Parse(dt.Rows[n]["Area4"].ToString());
					}
					if(dt.Rows[n]["Area5"].ToString()!="")
					{
						model.Area5=decimal.Parse(dt.Rows[n]["Area5"].ToString());
					}
					if(dt.Rows[n]["Area6"].ToString()!="")
					{
						model.Area6=decimal.Parse(dt.Rows[n]["Area6"].ToString());
					}
					if(dt.Rows[n]["Area7"].ToString()!="")
					{
						model.Area7=decimal.Parse(dt.Rows[n]["Area7"].ToString());
					}
					if(dt.Rows[n]["Area8"].ToString()!="")
					{
						model.Area8=decimal.Parse(dt.Rows[n]["Area8"].ToString());
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

