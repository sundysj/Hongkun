using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Sys;
namespace MobileSoft.BLL.Sys
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Sys_TakePicSec 的摘要说明。
	/// </summary>
	public class Bll_Tb_Sys_TakePicSec
	{
		private readonly MobileSoft.DAL.Sys.Dal_Tb_Sys_TakePicSec dal=new MobileSoft.DAL.Sys.Dal_Tb_Sys_TakePicSec();
		public Bll_Tb_Sys_TakePicSec()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long StatID)
		{
			return dal.Exists(StatID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(MobileSoft.Model.Sys.Tb_Sys_TakePicSec model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Sys.Tb_Sys_TakePicSec model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long StatID)
		{
			
			dal.Delete(StatID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_TakePicSec GetModel(long StatID)
		{
			
			return dal.GetModel(StatID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_TakePicSec GetModelByCache(long StatID)
		{
			
			string CacheKey = "Tb_Sys_TakePicSecModel-" + StatID;
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
			return (MobileSoft.Model.Sys.Tb_Sys_TakePicSec)objModel;
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
		public List<MobileSoft.Model.Sys.Tb_Sys_TakePicSec> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Sys.Tb_Sys_TakePicSec> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Sys.Tb_Sys_TakePicSec> modelList = new List<MobileSoft.Model.Sys.Tb_Sys_TakePicSec>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Sys.Tb_Sys_TakePicSec model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Sys.Tb_Sys_TakePicSec();
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
					if(dt.Rows[n]["ManageCount"].ToString()!="")
					{
						model.ManageCount=int.Parse(dt.Rows[n]["ManageCount"].ToString());
					}
					if(dt.Rows[n]["ManageArea"].ToString()!="")
					{
						model.ManageArea=decimal.Parse(dt.Rows[n]["ManageArea"].ToString());
					}
					if(dt.Rows[n]["ManageCount1"].ToString()!="")
					{
						model.ManageCount1=int.Parse(dt.Rows[n]["ManageCount1"].ToString());
					}
					if(dt.Rows[n]["ManageArea1"].ToString()!="")
					{
						model.ManageArea1=decimal.Parse(dt.Rows[n]["ManageArea1"].ToString());
					}
					if(dt.Rows[n]["ManageCount2"].ToString()!="")
					{
						model.ManageCount2=int.Parse(dt.Rows[n]["ManageCount2"].ToString());
					}
					if(dt.Rows[n]["ManageArea2"].ToString()!="")
					{
						model.ManageArea2=decimal.Parse(dt.Rows[n]["ManageArea2"].ToString());
					}
					if(dt.Rows[n]["ManageCount3"].ToString()!="")
					{
						model.ManageCount3=int.Parse(dt.Rows[n]["ManageCount3"].ToString());
					}
					if(dt.Rows[n]["ManageArea3"].ToString()!="")
					{
						model.ManageArea3=decimal.Parse(dt.Rows[n]["ManageArea3"].ToString());
					}
					if(dt.Rows[n]["ManageCountNew"].ToString()!="")
					{
						model.ManageCountNew=int.Parse(dt.Rows[n]["ManageCountNew"].ToString());
					}
					if(dt.Rows[n]["ManageAreaNew"].ToString()!="")
					{
						model.ManageAreaNew=decimal.Parse(dt.Rows[n]["ManageAreaNew"].ToString());
					}
					if(dt.Rows[n]["AllArea"].ToString()!="")
					{
						model.AllArea=decimal.Parse(dt.Rows[n]["AllArea"].ToString());
					}
					if(dt.Rows[n]["ProUseArea1"].ToString()!="")
					{
						model.ProUseArea1=decimal.Parse(dt.Rows[n]["ProUseArea1"].ToString());
					}
					if(dt.Rows[n]["ProUseArea2"].ToString()!="")
					{
						model.ProUseArea2=decimal.Parse(dt.Rows[n]["ProUseArea2"].ToString());
					}
					if(dt.Rows[n]["ProUseArea3"].ToString()!="")
					{
						model.ProUseArea3=decimal.Parse(dt.Rows[n]["ProUseArea3"].ToString());
					}
					if(dt.Rows[n]["ProUseRoomNum1"].ToString()!="")
					{
						model.ProUseRoomNum1=int.Parse(dt.Rows[n]["ProUseRoomNum1"].ToString());
					}
					if(dt.Rows[n]["ProUseRoomNum2"].ToString()!="")
					{
						model.ProUseRoomNum2=int.Parse(dt.Rows[n]["ProUseRoomNum2"].ToString());
					}
					if(dt.Rows[n]["ProUseRoomNum3"].ToString()!="")
					{
						model.ProUseRoomNum3=int.Parse(dt.Rows[n]["ProUseRoomNum3"].ToString());
					}
					if(dt.Rows[n]["ParkAllNum"].ToString()!="")
					{
						model.ParkAllNum=int.Parse(dt.Rows[n]["ParkAllNum"].ToString());
					}
					if(dt.Rows[n]["ParkSaleNum"].ToString()!="")
					{
						model.ParkSaleNum=int.Parse(dt.Rows[n]["ParkSaleNum"].ToString());
					}
					if(dt.Rows[n]["ParkLeaseNum"].ToString()!="")
					{
						model.ParkLeaseNum=int.Parse(dt.Rows[n]["ParkLeaseNum"].ToString());
					}
					if(dt.Rows[n]["ParkEmptyNum"].ToString()!="")
					{
						model.ParkEmptyNum=int.Parse(dt.Rows[n]["ParkEmptyNum"].ToString());
					}
					if(dt.Rows[n]["CommKindRoomNum"].ToString()!="")
					{
						model.CommKindRoomNum=int.Parse(dt.Rows[n]["CommKindRoomNum"].ToString());
					}
					if(dt.Rows[n]["CommKindArea1"].ToString()!="")
					{
						model.CommKindArea1=decimal.Parse(dt.Rows[n]["CommKindArea1"].ToString());
					}
					if(dt.Rows[n]["CommKindCount1"].ToString()!="")
					{
						model.CommKindCount1=int.Parse(dt.Rows[n]["CommKindCount1"].ToString());
					}
					if(dt.Rows[n]["CommKindRoomNum1"].ToString()!="")
					{
						model.CommKindRoomNum1=int.Parse(dt.Rows[n]["CommKindRoomNum1"].ToString());
					}
					if(dt.Rows[n]["CommKindArea2"].ToString()!="")
					{
						model.CommKindArea2=decimal.Parse(dt.Rows[n]["CommKindArea2"].ToString());
					}
					if(dt.Rows[n]["CommKindCount2"].ToString()!="")
					{
						model.CommKindCount2=int.Parse(dt.Rows[n]["CommKindCount2"].ToString());
					}
					if(dt.Rows[n]["CommKindRoomNum2"].ToString()!="")
					{
						model.CommKindRoomNum2=int.Parse(dt.Rows[n]["CommKindRoomNum2"].ToString());
					}
					if(dt.Rows[n]["ChargeRateTheMonth"].ToString()!="")
					{
						model.ChargeRateTheMonth=decimal.Parse(dt.Rows[n]["ChargeRateTheMonth"].ToString());
					}
					if(dt.Rows[n]["ChargeRateTheYear"].ToString()!="")
					{
						model.ChargeRateTheYear=decimal.Parse(dt.Rows[n]["ChargeRateTheYear"].ToString());
					}
					if(dt.Rows[n]["ChargeRateLastYear"].ToString()!="")
					{
						model.ChargeRateLastYear=decimal.Parse(dt.Rows[n]["ChargeRateLastYear"].ToString());
					}
					if(dt.Rows[n]["ChargeRateTheMonth1"].ToString()!="")
					{
						model.ChargeRateTheMonth1=decimal.Parse(dt.Rows[n]["ChargeRateTheMonth1"].ToString());
					}
					if(dt.Rows[n]["ChargeRateTheYear1"].ToString()!="")
					{
						model.ChargeRateTheYear1=decimal.Parse(dt.Rows[n]["ChargeRateTheYear1"].ToString());
					}
					if(dt.Rows[n]["ChargeRateLastYear1"].ToString()!="")
					{
						model.ChargeRateLastYear1=decimal.Parse(dt.Rows[n]["ChargeRateLastYear1"].ToString());
					}
					if(dt.Rows[n]["ChargeRateTheMonth2"].ToString()!="")
					{
						model.ChargeRateTheMonth2=decimal.Parse(dt.Rows[n]["ChargeRateTheMonth2"].ToString());
					}
					if(dt.Rows[n]["ChargeRateTheYear2"].ToString()!="")
					{
						model.ChargeRateTheYear2=decimal.Parse(dt.Rows[n]["ChargeRateTheYear2"].ToString());
					}
					if(dt.Rows[n]["ChargeRateLastYear2"].ToString()!="")
					{
						model.ChargeRateLastYear2=decimal.Parse(dt.Rows[n]["ChargeRateLastYear2"].ToString());
					}
					if(dt.Rows[n]["IncidCustHasNum"].ToString()!="")
					{
						model.IncidCustHasNum=int.Parse(dt.Rows[n]["IncidCustHasNum"].ToString());
					}
					if(dt.Rows[n]["IncidCustNoNum"].ToString()!="")
					{
						model.IncidCustNoNum=int.Parse(dt.Rows[n]["IncidCustNoNum"].ToString());
					}
					if(dt.Rows[n]["IncidCustDayRate"].ToString()!="")
					{
						model.IncidCustDayRate=int.Parse(dt.Rows[n]["IncidCustDayRate"].ToString());
					}
					if(dt.Rows[n]["IncidCustMonthRate"].ToString()!="")
					{
						model.IncidCustMonthRate=int.Parse(dt.Rows[n]["IncidCustMonthRate"].ToString());
					}
					if(dt.Rows[n]["IncidCustYearRate"].ToString()!="")
					{
						model.IncidCustYearRate=int.Parse(dt.Rows[n]["IncidCustYearRate"].ToString());
					}
					if(dt.Rows[n]["IncidCustAllNoNum"].ToString()!="")
					{
						model.IncidCustAllNoNum=int.Parse(dt.Rows[n]["IncidCustAllNoNum"].ToString());
					}
					if(dt.Rows[n]["IncidCustDayNoNum"].ToString()!="")
					{
						model.IncidCustDayNoNum=int.Parse(dt.Rows[n]["IncidCustDayNoNum"].ToString());
					}
					if(dt.Rows[n]["IncidCustWeekNoNum"].ToString()!="")
					{
						model.IncidCustWeekNoNum=int.Parse(dt.Rows[n]["IncidCustWeekNoNum"].ToString());
					}
					if(dt.Rows[n]["IncidCustMonthNoNum"].ToString()!="")
					{
						model.IncidCustMonthNoNum=int.Parse(dt.Rows[n]["IncidCustMonthNoNum"].ToString());
					}
					if(dt.Rows[n]["IncidCompDayNum"].ToString()!="")
					{
						model.IncidCompDayNum=int.Parse(dt.Rows[n]["IncidCompDayNum"].ToString());
					}
					if(dt.Rows[n]["IncidCompMonthNum"].ToString()!="")
					{
						model.IncidCompMonthNum=int.Parse(dt.Rows[n]["IncidCompMonthNum"].ToString());
					}
					if(dt.Rows[n]["IncidCompYearNum"].ToString()!="")
					{
						model.IncidCompYearNum=int.Parse(dt.Rows[n]["IncidCompYearNum"].ToString());
					}
					if(dt.Rows[n]["CurFeesRate_Deno"].ToString()!="")
					{
						model.CurFeesRate_Deno=decimal.Parse(dt.Rows[n]["CurFeesRate_Deno"].ToString());
					}
					if(dt.Rows[n]["CurFeesRate_Mole"].ToString()!="")
					{
						model.CurFeesRate_Mole=decimal.Parse(dt.Rows[n]["CurFeesRate_Mole"].ToString());
					}
					if(dt.Rows[n]["CurYearFeesRate_Deno"].ToString()!="")
					{
						model.CurYearFeesRate_Deno=decimal.Parse(dt.Rows[n]["CurYearFeesRate_Deno"].ToString());
					}
					if(dt.Rows[n]["CurYearFeesRate_Mole"].ToString()!="")
					{
						model.CurYearFeesRate_Mole=decimal.Parse(dt.Rows[n]["CurYearFeesRate_Mole"].ToString());
					}
					if(dt.Rows[n]["BefLastFeesRate_Deno"].ToString()!="")
					{
						model.BefLastFeesRate_Deno=decimal.Parse(dt.Rows[n]["BefLastFeesRate_Deno"].ToString());
					}
					if(dt.Rows[n]["BefLastFeesRate_Mole"].ToString()!="")
					{
						model.BefLastFeesRate_Mole=decimal.Parse(dt.Rows[n]["BefLastFeesRate_Mole"].ToString());
					}
					if(dt.Rows[n]["IncidFinlishDayNum"].ToString()!="")
					{
						model.IncidFinlishDayNum=int.Parse(dt.Rows[n]["IncidFinlishDayNum"].ToString());
					}
					if(dt.Rows[n]["IncidHappenDayNum"].ToString()!="")
					{
						model.IncidHappenDayNum=int.Parse(dt.Rows[n]["IncidHappenDayNum"].ToString());
					}
					if(dt.Rows[n]["IncidFinlishMonthNum"].ToString()!="")
					{
						model.IncidFinlishMonthNum=int.Parse(dt.Rows[n]["IncidFinlishMonthNum"].ToString());
					}
					if(dt.Rows[n]["IncidHappenMonthNum"].ToString()!="")
					{
						model.IncidHappenMonthNum=int.Parse(dt.Rows[n]["IncidHappenMonthNum"].ToString());
					}
					if(dt.Rows[n]["IncidFinlishYearNum"].ToString()!="")
					{
						model.IncidFinlishYearNum=int.Parse(dt.Rows[n]["IncidFinlishYearNum"].ToString());
					}
					if(dt.Rows[n]["IncidHappenYearNum"].ToString()!="")
					{
						model.IncidHappenYearNum=int.Parse(dt.Rows[n]["IncidHappenYearNum"].ToString());
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

