using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.SQMSys;
namespace MobileSoft.BLL.SQMSys
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Sys_TakePersonPic 的摘要说明。
	/// </summary>
	public class Bll_Tb_Sys_TakePersonPic
	{
		private readonly MobileSoft.DAL.SQMSys.Dal_Tb_Sys_TakePersonPic dal=new MobileSoft.DAL.SQMSys.Dal_Tb_Sys_TakePersonPic();
		public Bll_Tb_Sys_TakePersonPic()
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
		public int  Add(MobileSoft.Model.SQMSys.Tb_Sys_TakePersonPic model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_TakePersonPic model)
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
		public MobileSoft.Model.SQMSys.Tb_Sys_TakePersonPic GetModel(long StatID)
		{
			
			return dal.GetModel(StatID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_TakePersonPic GetModelByCache(long StatID)
		{
			
			string CacheKey = "Tb_Sys_TakePersonPicModel-" + StatID;
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
			return (MobileSoft.Model.SQMSys.Tb_Sys_TakePersonPic)objModel;
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
		public List<MobileSoft.Model.SQMSys.Tb_Sys_TakePersonPic> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.SQMSys.Tb_Sys_TakePersonPic> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.SQMSys.Tb_Sys_TakePersonPic> modelList = new List<MobileSoft.Model.SQMSys.Tb_Sys_TakePersonPic>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.SQMSys.Tb_Sys_TakePersonPic model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.SQMSys.Tb_Sys_TakePersonPic();
					//model.StatID=dt.Rows[n]["StatID"].ToString();
					if(dt.Rows[n]["StatType"].ToString()!="")
					{
						model.StatType=int.Parse(dt.Rows[n]["StatType"].ToString());
					}
					if(dt.Rows[n]["StreetCode"].ToString()!="")
					{
						model.StreetCode=new Guid(dt.Rows[n]["StreetCode"].ToString());
					}
					if(dt.Rows[n]["CommCode"].ToString()!="")
					{
						model.CommCode=new Guid(dt.Rows[n]["CommCode"].ToString());
					}
					if(dt.Rows[n]["StatDate"].ToString()!="")
					{
						model.StatDate=DateTime.Parse(dt.Rows[n]["StatDate"].ToString());
					}
					if(dt.Rows[n]["TotalHNum"].ToString()!="")
					{
						model.TotalHNum=int.Parse(dt.Rows[n]["TotalHNum"].ToString());
					}
					if(dt.Rows[n]["TotalHNum1"].ToString()!="")
					{
						model.TotalHNum1=int.Parse(dt.Rows[n]["TotalHNum1"].ToString());
					}
					if(dt.Rows[n]["TotalHNum2"].ToString()!="")
					{
						model.TotalHNum2=int.Parse(dt.Rows[n]["TotalHNum2"].ToString());
					}
					if(dt.Rows[n]["TotalHNum3"].ToString()!="")
					{
						model.TotalHNum3=int.Parse(dt.Rows[n]["TotalHNum3"].ToString());
					}
					if(dt.Rows[n]["TotalHNum4"].ToString()!="")
					{
						model.TotalHNum4=int.Parse(dt.Rows[n]["TotalHNum4"].ToString());
					}
					if(dt.Rows[n]["TotalNum"].ToString()!="")
					{
						model.TotalNum=int.Parse(dt.Rows[n]["TotalNum"].ToString());
					}
					if(dt.Rows[n]["TotalNum1"].ToString()!="")
					{
						model.TotalNum1=int.Parse(dt.Rows[n]["TotalNum1"].ToString());
					}
					if(dt.Rows[n]["TotalNum2"].ToString()!="")
					{
						model.TotalNum2=int.Parse(dt.Rows[n]["TotalNum2"].ToString());
					}
					if(dt.Rows[n]["TotalNum3"].ToString()!="")
					{
						model.TotalNum3=int.Parse(dt.Rows[n]["TotalNum3"].ToString());
					}
					if(dt.Rows[n]["TotalPHNum1"].ToString()!="")
					{
						model.TotalPHNum1=int.Parse(dt.Rows[n]["TotalPHNum1"].ToString());
					}
					if(dt.Rows[n]["TotalPHNum2"].ToString()!="")
					{
						model.TotalPHNum2=int.Parse(dt.Rows[n]["TotalPHNum2"].ToString());
					}
					if(dt.Rows[n]["TotalPHNum3"].ToString()!="")
					{
						model.TotalPHNum3=int.Parse(dt.Rows[n]["TotalPHNum3"].ToString());
					}
					if(dt.Rows[n]["TotalPHNum4"].ToString()!="")
					{
						model.TotalPHNum4=int.Parse(dt.Rows[n]["TotalPHNum4"].ToString());
					}
					if(dt.Rows[n]["TotalPHNum5"].ToString()!="")
					{
						model.TotalPHNum5=int.Parse(dt.Rows[n]["TotalPHNum5"].ToString());
					}
					if(dt.Rows[n]["TotalPHNum6"].ToString()!="")
					{
						model.TotalPHNum6=int.Parse(dt.Rows[n]["TotalPHNum6"].ToString());
					}
					if(dt.Rows[n]["TotalPHNum7"].ToString()!="")
					{
						model.TotalPHNum7=int.Parse(dt.Rows[n]["TotalPHNum7"].ToString());
					}
					if(dt.Rows[n]["TotalPNum1"].ToString()!="")
					{
						model.TotalPNum1=int.Parse(dt.Rows[n]["TotalPNum1"].ToString());
					}
					if(dt.Rows[n]["TotalPNum2"].ToString()!="")
					{
						model.TotalPNum2=int.Parse(dt.Rows[n]["TotalPNum2"].ToString());
					}
					if(dt.Rows[n]["TotalPNum3"].ToString()!="")
					{
						model.TotalPNum3=int.Parse(dt.Rows[n]["TotalPNum3"].ToString());
					}
					if(dt.Rows[n]["TotalPNum4"].ToString()!="")
					{
						model.TotalPNum4=int.Parse(dt.Rows[n]["TotalPNum4"].ToString());
					}
					if(dt.Rows[n]["TotalPNum5"].ToString()!="")
					{
						model.TotalPNum5=int.Parse(dt.Rows[n]["TotalPNum5"].ToString());
					}
					if(dt.Rows[n]["TotalPNum6"].ToString()!="")
					{
						model.TotalPNum6=int.Parse(dt.Rows[n]["TotalPNum6"].ToString());
					}
					if(dt.Rows[n]["EmploymentNum"].ToString()!="")
					{
						model.EmploymentNum=int.Parse(dt.Rows[n]["EmploymentNum"].ToString());
					}
					if(dt.Rows[n]["EmploymentNum1"].ToString()!="")
					{
						model.EmploymentNum1=int.Parse(dt.Rows[n]["EmploymentNum1"].ToString());
					}
					if(dt.Rows[n]["EmploymentNum2"].ToString()!="")
					{
						model.EmploymentNum2=int.Parse(dt.Rows[n]["EmploymentNum2"].ToString());
					}
					if(dt.Rows[n]["EmploymentRate"].ToString()!="")
					{
						model.EmploymentRate=decimal.Parse(dt.Rows[n]["EmploymentRate"].ToString());
					}
					if(dt.Rows[n]["TrainYearTimes"].ToString()!="")
					{
						model.TrainYearTimes=int.Parse(dt.Rows[n]["TrainYearTimes"].ToString());
					}
					if(dt.Rows[n]["TrainYearNum"].ToString()!="")
					{
						model.TrainYearNum=int.Parse(dt.Rows[n]["TrainYearNum"].ToString());
					}
					if(dt.Rows[n]["RecommendYearTimes"].ToString()!="")
					{
						model.RecommendYearTimes=int.Parse(dt.Rows[n]["RecommendYearTimes"].ToString());
					}
					if(dt.Rows[n]["RecommendYearNum"].ToString()!="")
					{
						model.RecommendYearNum=int.Parse(dt.Rows[n]["RecommendYearNum"].ToString());
					}
					if(dt.Rows[n]["SecurityNum1"].ToString()!="")
					{
						model.SecurityNum1=int.Parse(dt.Rows[n]["SecurityNum1"].ToString());
					}
					if(dt.Rows[n]["SecurityNum2"].ToString()!="")
					{
						model.SecurityNum2=int.Parse(dt.Rows[n]["SecurityNum2"].ToString());
					}
					if(dt.Rows[n]["SecurityNum3"].ToString()!="")
					{
						model.SecurityNum3=int.Parse(dt.Rows[n]["SecurityNum3"].ToString());
					}
					if(dt.Rows[n]["SecurityNum4"].ToString()!="")
					{
						model.SecurityNum4=int.Parse(dt.Rows[n]["SecurityNum4"].ToString());
					}
					if(dt.Rows[n]["SuspectNum"].ToString()!="")
					{
						model.SuspectNum=int.Parse(dt.Rows[n]["SuspectNum"].ToString());
					}
					if(dt.Rows[n]["EmphasisNum"].ToString()!="")
					{
						model.EmphasisNum=int.Parse(dt.Rows[n]["EmphasisNum"].ToString());
					}
					if(dt.Rows[n]["FertilityNum"].ToString()!="")
					{
						model.FertilityNum=int.Parse(dt.Rows[n]["FertilityNum"].ToString());
					}
					if(dt.Rows[n]["PreschoolNum"].ToString()!="")
					{
						model.PreschoolNum=int.Parse(dt.Rows[n]["PreschoolNum"].ToString());
					}
					if(dt.Rows[n]["BabyNum"].ToString()!="")
					{
						model.BabyNum=int.Parse(dt.Rows[n]["BabyNum"].ToString());
					}
					if(dt.Rows[n]["BabyNum1"].ToString()!="")
					{
						model.BabyNum1=int.Parse(dt.Rows[n]["BabyNum1"].ToString());
					}
					if(dt.Rows[n]["BabyNum2"].ToString()!="")
					{
						model.BabyNum2=int.Parse(dt.Rows[n]["BabyNum2"].ToString());
					}
					if(dt.Rows[n]["PartyBranchNum"].ToString()!="")
					{
						model.PartyBranchNum=int.Parse(dt.Rows[n]["PartyBranchNum"].ToString());
					}
					if(dt.Rows[n]["PartyNum"].ToString()!="")
					{
						model.PartyNum=int.Parse(dt.Rows[n]["PartyNum"].ToString());
					}
					if(dt.Rows[n]["PartyNum1"].ToString()!="")
					{
						model.PartyNum1=int.Parse(dt.Rows[n]["PartyNum1"].ToString());
					}
					if(dt.Rows[n]["PartyNum2"].ToString()!="")
					{
						model.PartyNum2=int.Parse(dt.Rows[n]["PartyNum2"].ToString());
					}
					if(dt.Rows[n]["PartyNum3"].ToString()!="")
					{
						model.PartyNum3=int.Parse(dt.Rows[n]["PartyNum3"].ToString());
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

