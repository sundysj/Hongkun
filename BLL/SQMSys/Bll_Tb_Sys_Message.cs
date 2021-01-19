using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.SQMSys;
namespace MobileSoft.BLL.SQMSys
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Sys_Message 的摘要说明。
	/// </summary>
	public class Bll_Tb_Sys_Message
	{
		private readonly MobileSoft.DAL.SQMSys.Dal_Tb_Sys_Message dal=new MobileSoft.DAL.SQMSys.Dal_Tb_Sys_Message();
		public Bll_Tb_Sys_Message()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid MessageCode,long CutID)
		{
			return dal.Exists(MessageCode,CutID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(MobileSoft.Model.SQMSys.Tb_Sys_Message model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_Message model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(Guid MessageCode,long CutID)
		{
			
			dal.Delete(MessageCode,CutID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_Message GetModel(Guid MessageCode,long CutID)
		{
			
			return dal.GetModel(MessageCode,CutID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
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
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string fieldOrder)
		{
			return dal.GetList(Top,strWhere,fieldOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.SQMSys.Tb_Sys_Message> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
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
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize,string SortField,int Sort)
		{
			return dal.GetList(out PageCount, out Counts, StrCondition, PageIndex, PageSize,SortField,Sort);
		}

		#endregion  成员方法
	}
}

