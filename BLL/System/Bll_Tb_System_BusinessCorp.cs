using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.System;
namespace MobileSoft.BLL.System
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_System_BusinessCorp 的摘要说明。
	/// </summary>
	public class Bll_Tb_System_BusinessCorp
	{
		private readonly MobileSoft.DAL.System.Dal_Tb_System_BusinessCorp dal=new MobileSoft.DAL.System.Dal_Tb_System_BusinessCorp();
		public Bll_Tb_System_BusinessCorp()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long BussId)
		{
			return dal.Exists(BussId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.System.Tb_System_BusinessCorp model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_BusinessCorp model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long BussId)
		{
			
			dal.Delete(BussId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_BusinessCorp GetModel(long BussId)
		{
			
			return dal.GetModel(BussId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.System.Tb_System_BusinessCorp GetModelByCache(long BussId)
		{
			
			string CacheKey = "Tb_System_BusinessCorpModel-" + BussId;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(BussId);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.System.Tb_System_BusinessCorp)objModel;
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
		public List<MobileSoft.Model.System.Tb_System_BusinessCorp> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.System.Tb_System_BusinessCorp> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.System.Tb_System_BusinessCorp> modelList = new List<MobileSoft.Model.System.Tb_System_BusinessCorp>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.System.Tb_System_BusinessCorp model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.System.Tb_System_BusinessCorp();
					//model.BussId=dt.Rows[n]["BussId"].ToString();
					model.RegBigType=dt.Rows[n]["RegBigType"].ToString();
					model.RegSmallType=dt.Rows[n]["RegSmallType"].ToString();
					model.BussName=dt.Rows[n]["BussName"].ToString();
					model.BussShortName=dt.Rows[n]["BussShortName"].ToString();
					model.Province=dt.Rows[n]["Province"].ToString();
					model.City=dt.Rows[n]["City"].ToString();
					model.Borough=dt.Rows[n]["Borough"].ToString();
					model.LoginCode=dt.Rows[n]["LoginCode"].ToString();
					model.LoginPassWD=dt.Rows[n]["LoginPassWD"].ToString();
					model.BussAddress=dt.Rows[n]["BussAddress"].ToString();
					model.BussZipCode=dt.Rows[n]["BussZipCode"].ToString();
					model.BussLinkMan=dt.Rows[n]["BussLinkMan"].ToString();
					model.BussMobileTel=dt.Rows[n]["BussMobileTel"].ToString();
					model.BussWorkedTel=dt.Rows[n]["BussWorkedTel"].ToString();
					model.BussEmail=dt.Rows[n]["BussEmail"].ToString();
					model.BussWebName=dt.Rows[n]["BussWebName"].ToString();
					model.BussQQ=dt.Rows[n]["BussQQ"].ToString();
					model.BussWeiXin=dt.Rows[n]["BussWeiXin"].ToString();
					model.LogoImgUrl=dt.Rows[n]["LogoImgUrl"].ToString();
					model.MapImgUrl=dt.Rows[n]["MapImgUrl"].ToString();
					model.SysDir=dt.Rows[n]["SysDir"].ToString();
					model.SysVersion=dt.Rows[n]["SysVersion"].ToString();
					if(dt.Rows[n]["RegDate"].ToString()!="")
					{
						model.RegDate=DateTime.Parse(dt.Rows[n]["RegDate"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["BussSNum"].ToString()!="")
					{
						model.BussSNum=int.Parse(dt.Rows[n]["BussSNum"].ToString());
					}
					if(dt.Rows[n]["BussSynchCode"].ToString()!="")
					{
						model.BussSynchCode=new Guid(dt.Rows[n]["BussSynchCode"].ToString());
					}
					if(dt.Rows[n]["SynchFlag"].ToString()!="")
					{
						model.SynchFlag=int.Parse(dt.Rows[n]["SynchFlag"].ToString());
					}
					if(dt.Rows[n]["IsRecommend"].ToString()!="")
					{
						model.IsRecommend=int.Parse(dt.Rows[n]["IsRecommend"].ToString());
					}
					model.RecommendIndex=dt.Rows[n]["RecommendIndex"].ToString();
					model.RecommendTitle=dt.Rows[n]["RecommendTitle"].ToString();
					model.RecommendContent=dt.Rows[n]["RecommendContent"].ToString();
					model.ImgLogo1=dt.Rows[n]["ImgLogo1"].ToString();
					model.ImgLogo2=dt.Rows[n]["ImgLogo2"].ToString();
					model.ImgLogo3=dt.Rows[n]["ImgLogo3"].ToString();
					model.ImgLogo4=dt.Rows[n]["ImgLogo4"].ToString();
					model.ImgLogo5=dt.Rows[n]["ImgLogo5"].ToString();
					model.ImgLogo6=dt.Rows[n]["ImgLogo6"].ToString();
					model.ImgLogo7=dt.Rows[n]["ImgLogo7"].ToString();
					model.ImgLogo8=dt.Rows[n]["ImgLogo8"].ToString();
					model.ImgLogo9=dt.Rows[n]["ImgLogo9"].ToString();
					model.ImgLogo10=dt.Rows[n]["ImgLogo10"].ToString();
					if(dt.Rows[n]["VisitCount"].ToString()!="")
					{
						model.VisitCount=int.Parse(dt.Rows[n]["VisitCount"].ToString());
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
        public DataSet GetFreeList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort)
        {
            return dal.GetFreeList(out PageCount, out Counts, StrCondition, PageIndex, PageSize, SortField, Sort);
        }
		#endregion  成员方法
	}
}

