using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.HSPR;
namespace MobileSoft.BLL.HSPR
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_HSPR_IncidentAccept 的摘要说明。
	/// </summary>
	public class Bll_Tb_HSPR_IncidentAccept
	{
		private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_IncidentAccept dal=new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_IncidentAccept();
		public Bll_Tb_HSPR_IncidentAccept()
		{}
		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_IncidentAccept model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_IncidentAccept model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			dal.Delete();
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_IncidentAccept GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel();
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_IncidentAccept GetModelByCache()
		{
			//该表无主键信息，请自定义主键/条件字段
			string CacheKey = "Tb_HSPR_IncidentAcceptModel-" ;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel();
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.HSPR.Tb_HSPR_IncidentAccept)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListCount(string strWhere)
        {
            return dal.GetListCount(strWhere);
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
		public List<MobileSoft.Model.HSPR.Tb_HSPR_IncidentAccept> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.HSPR.Tb_HSPR_IncidentAccept> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.HSPR.Tb_HSPR_IncidentAccept> modelList = new List<MobileSoft.Model.HSPR.Tb_HSPR_IncidentAccept>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.HSPR.Tb_HSPR_IncidentAccept model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.HSPR.Tb_HSPR_IncidentAccept();
					//model.IncidentID=dt.Rows[n]["IncidentID"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					//model.CustID=dt.Rows[n]["CustID"].ToString();
					//model.RoomID=dt.Rows[n]["RoomID"].ToString();
					model.TypeID=dt.Rows[n]["TypeID"].ToString();
					model.IncidentNum=dt.Rows[n]["IncidentNum"].ToString();
					model.IncidentPlace=dt.Rows[n]["IncidentPlace"].ToString();
					model.IncidentMan=dt.Rows[n]["IncidentMan"].ToString();
					if(dt.Rows[n]["IncidentDate"].ToString()!="")
					{
						model.IncidentDate=DateTime.Parse(dt.Rows[n]["IncidentDate"].ToString());
					}
					model.IncidentMode=dt.Rows[n]["IncidentMode"].ToString();
					if(dt.Rows[n]["DealLimit"].ToString()!="")
					{
						model.DealLimit=int.Parse(dt.Rows[n]["DealLimit"].ToString());
					}
					model.IncidentContent=dt.Rows[n]["IncidentContent"].ToString();
					if(dt.Rows[n]["ReserveDate"].ToString()!="")
					{
						model.ReserveDate=DateTime.Parse(dt.Rows[n]["ReserveDate"].ToString());
					}
					if(dt.Rows[n]["ReserveLimit"].ToString()!="")
					{
						model.ReserveLimit=int.Parse(dt.Rows[n]["ReserveLimit"].ToString());
					}
					model.Phone=dt.Rows[n]["Phone"].ToString();
					model.AdmiMan=dt.Rows[n]["AdmiMan"].ToString();
					if(dt.Rows[n]["AdmiDate"].ToString()!="")
					{
						model.AdmiDate=DateTime.Parse(dt.Rows[n]["AdmiDate"].ToString());
					}
					if(dt.Rows[n]["DispType"].ToString()!="")
					{
						model.DispType=int.Parse(dt.Rows[n]["DispType"].ToString());
					}
					model.DispMan=dt.Rows[n]["DispMan"].ToString();
					if(dt.Rows[n]["DispDate"].ToString()!="")
					{
						model.DispDate=DateTime.Parse(dt.Rows[n]["DispDate"].ToString());
					}
					model.DealMan=dt.Rows[n]["DealMan"].ToString();
					model.CoordinateNum=dt.Rows[n]["CoordinateNum"].ToString();
					if(dt.Rows[n]["EndDate"].ToString()!="")
					{
						model.EndDate=DateTime.Parse(dt.Rows[n]["EndDate"].ToString());
					}
					if(dt.Rows[n]["MainStartDate"].ToString()!="")
					{
						model.MainStartDate=DateTime.Parse(dt.Rows[n]["MainStartDate"].ToString());
					}
					if(dt.Rows[n]["MainEndDate"].ToString()!="")
					{
						model.MainEndDate=DateTime.Parse(dt.Rows[n]["MainEndDate"].ToString());
					}
					model.DealSituation=dt.Rows[n]["DealSituation"].ToString();
					model.CustComments=dt.Rows[n]["CustComments"].ToString();
					model.ServiceQuality=dt.Rows[n]["ServiceQuality"].ToString();
					model.ArticlesFacilities=dt.Rows[n]["ArticlesFacilities"].ToString();
					if(dt.Rows[n]["DealState"].ToString()!="")
					{
						model.DealState=int.Parse(dt.Rows[n]["DealState"].ToString());
					}
					model.IncidentMemo=dt.Rows[n]["IncidentMemo"].ToString();
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					model.Reasons=dt.Rows[n]["Reasons"].ToString();
					//model.RegionalID=dt.Rows[n]["RegionalID"].ToString();
					model.DeleteReasons=dt.Rows[n]["DeleteReasons"].ToString();
					if(dt.Rows[n]["DeleteDate"].ToString()!="")
					{
						model.DeleteDate=DateTime.Parse(dt.Rows[n]["DeleteDate"].ToString());
					}
					model.TypeCode=dt.Rows[n]["TypeCode"].ToString();
					model.Signatory=dt.Rows[n]["Signatory"].ToString();
					if(dt.Rows[n]["IsStatistics"].ToString()!="")
					{
						model.IsStatistics=int.Parse(dt.Rows[n]["IsStatistics"].ToString());
					}
					model.FinishUser=dt.Rows[n]["FinishUser"].ToString();
					if(dt.Rows[n]["DueAmount"].ToString()!="")
					{
						model.DueAmount=decimal.Parse(dt.Rows[n]["DueAmount"].ToString());
					}
					if(dt.Rows[n]["IsTell"].ToString()!="")
					{
						model.IsTell=int.Parse(dt.Rows[n]["IsTell"].ToString());
					}
					//model.DeviceID=dt.Rows[n]["DeviceID"].ToString();
					if(dt.Rows[n]["PrintTime"].ToString()!="")
					{
						model.PrintTime=DateTime.Parse(dt.Rows[n]["PrintTime"].ToString());
					}
					if(dt.Rows[n]["PrintCount"].ToString()!="")
					{
						model.PrintCount=int.Parse(dt.Rows[n]["PrintCount"].ToString());
					}
					model.PrintUserName=dt.Rows[n]["PrintUserName"].ToString();
					if(dt.Rows[n]["IsReceipt"].ToString()!="")
					{
						model.IsReceipt=int.Parse(dt.Rows[n]["IsReceipt"].ToString());
					}
					model.ReceiptUserName=dt.Rows[n]["ReceiptUserName"].ToString();
					//model.LocationID=dt.Rows[n]["LocationID"].ToString();
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

        public void IncidentAcceptPhoneInsert(int CommID, long CustID, long RoomID, string IncidentDate, string IncidentContent, string IncidentMan, string ReserveDate, string Phone, string IncidentImgs,string TypeID)
            {
                dal.IncidentAcceptPhoneInsert(CommID, CustID, RoomID, IncidentDate, IncidentContent, IncidentMan, ReserveDate, Phone, IncidentImgs,TypeID);
            }

        public void PubIncidentAcceptPhoneInsert(int CommID, long RegionalID, string IncidentDate, string IncidentMan, string IncidentContent, string ReserveDate, string Phone, string IncidentImgs,string TypeID,string CustID)
            {
                dal.PubIncidentAcceptPhoneInsert(CommID, RegionalID, IncidentDate, IncidentMan, IncidentContent, ReserveDate, Phone, IncidentImgs, TypeID, CustID);
            }

            public void PubIncidentPhoneUpdate(int CommID, long IncidentID, string ReceiptUserName, string DealContent, string CustIdea, int IsOVer)
            {
                  dal.PubIncidentPhoneUpdate(CommID, IncidentID, ReceiptUserName, DealContent, CustIdea, IsOVer);
            }
	}
}

