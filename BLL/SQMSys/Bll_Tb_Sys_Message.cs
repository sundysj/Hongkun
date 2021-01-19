using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.SQMSys;
namespace MobileSoft.BLL.SQMSys
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Sys_Message ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Sys_Message
	{
		private readonly MobileSoft.DAL.SQMSys.Dal_Tb_Sys_Message dal=new MobileSoft.DAL.SQMSys.Dal_Tb_Sys_Message();
		public Bll_Tb_Sys_Message()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(Guid MessageCode,long CutID)
		{
			return dal.Exists(MessageCode,CutID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(MobileSoft.Model.SQMSys.Tb_Sys_Message model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_Message model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(Guid MessageCode,long CutID)
		{
			
			dal.Delete(MessageCode,CutID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_Message GetModel(Guid MessageCode,long CutID)
		{
			
			return dal.GetModel(MessageCode,CutID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_Message GetModelByCache(Guid MessageCode,long CutID)
		{
			
			string CacheKey = "Tb_Sys_MessageModel-" + MessageCode+CutID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(MessageCode,CutID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.SQMSys.Tb_Sys_Message)objModel;
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
		public List<MobileSoft.Model.SQMSys.Tb_Sys_Message> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.SQMSys.Tb_Sys_Message> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.SQMSys.Tb_Sys_Message> modelList = new List<MobileSoft.Model.SQMSys.Tb_Sys_Message>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.SQMSys.Tb_Sys_Message model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.SQMSys.Tb_Sys_Message();
					if(dt.Rows[n]["MessageCode"].ToString()!="")
					{
						model.MessageCode=new Guid(dt.Rows[n]["MessageCode"].ToString());
					}
					//model.CutID=dt.Rows[n]["CutID"].ToString();
					model.UserCode=dt.Rows[n]["UserCode"].ToString();
					model.MsgTitle=dt.Rows[n]["MsgTitle"].ToString();
					model.Content=dt.Rows[n]["Content"].ToString();
					if(dt.Rows[n]["SendTime"].ToString()!="")
					{
						model.SendTime=DateTime.Parse(dt.Rows[n]["SendTime"].ToString());
					}
					if(dt.Rows[n]["MsgType"].ToString()!="")
					{
						model.MsgType=int.Parse(dt.Rows[n]["MsgType"].ToString());
					}
					model.SendMan=dt.Rows[n]["SendMan"].ToString();
					if(dt.Rows[n]["MsgState"].ToString()!="")
					{
						model.MsgState=int.Parse(dt.Rows[n]["MsgState"].ToString());
					}
					if(dt.Rows[n]["IsDeleteSend"].ToString()!="")
					{
						model.IsDeleteSend=int.Parse(dt.Rows[n]["IsDeleteSend"].ToString());
					}
					if(dt.Rows[n]["IsDeleteRead"].ToString()!="")
					{
						model.IsDeleteRead=int.Parse(dt.Rows[n]["IsDeleteRead"].ToString());
					}
					model.URL=dt.Rows[n]["URL"].ToString();
					model.HaveSendUsers=dt.Rows[n]["HaveSendUsers"].ToString();
					if(dt.Rows[n]["IsRemind"].ToString()!="")
					{
						model.IsRemind=int.Parse(dt.Rows[n]["IsRemind"].ToString());
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

