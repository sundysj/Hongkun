using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.System;
namespace MobileSoft.BLL.System
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_System_Corp 的摘要说明。
	/// </summary>
	public class Bll_Tb_System_Corp
	{
		private readonly MobileSoft.DAL.System.Dal_Tb_System_Corp dal=new MobileSoft.DAL.System.Dal_Tb_System_Corp();
		public Bll_Tb_System_Corp()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long CorpSID)
		{
			return dal.Exists(CorpSID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.System.Tb_System_Corp model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Corp model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long CorpSID)
		{
			
			dal.Delete(CorpSID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Corp GetModel(long CorpSID)
		{
			
			return dal.GetModel(CorpSID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Corp GetModelByCache(long CorpSID)
		{
			
			string CacheKey = "Tb_System_CorpModel-" + CorpSID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CorpSID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.System.Tb_System_Corp)objModel;
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
		public List<MobileSoft.Model.System.Tb_System_Corp> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.System.Tb_System_Corp> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.System.Tb_System_Corp> modelList = new List<MobileSoft.Model.System.Tb_System_Corp>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.System.Tb_System_Corp model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.System.Tb_System_Corp();
					//model.CorpSID=dt.Rows[n]["CorpSID"].ToString();
					if(dt.Rows[n]["CorpID"].ToString()!="")
					{
						model.CorpID=int.Parse(dt.Rows[n]["CorpID"].ToString());
					}
					if(dt.Rows[n]["RegType"].ToString()!="")
					{
						model.RegType=int.Parse(dt.Rows[n]["RegType"].ToString());
					}
					if(dt.Rows[n]["RegMode"].ToString()!="")
					{
						model.RegMode=int.Parse(dt.Rows[n]["RegMode"].ToString());
					}
					model.CorpName=dt.Rows[n]["CorpName"].ToString();
					model.CorpShortName=dt.Rows[n]["CorpShortName"].ToString();
					model.CorpTypeName=dt.Rows[n]["CorpTypeName"].ToString();
					model.Province=dt.Rows[n]["Province"].ToString();
					model.City=dt.Rows[n]["City"].ToString();
					model.Borough=dt.Rows[n]["Borough"].ToString();
					model.Street=dt.Rows[n]["Street"].ToString();
					model.CommName=dt.Rows[n]["CommName"].ToString();
					model.LoginCode=dt.Rows[n]["LoginCode"].ToString();
					model.LoginPassWD=dt.Rows[n]["LoginPassWD"].ToString();
					model.PwdQuestion=dt.Rows[n]["PwdQuestion"].ToString();
					model.PwdAnswer=dt.Rows[n]["PwdAnswer"].ToString();
					model.CorpAddress=dt.Rows[n]["CorpAddress"].ToString();
					model.CorpPost=dt.Rows[n]["CorpPost"].ToString();
					model.CorpDeputy=dt.Rows[n]["CorpDeputy"].ToString();
					model.CorpLinkMan=dt.Rows[n]["CorpLinkMan"].ToString();
					model.CorpMobileTel=dt.Rows[n]["CorpMobileTel"].ToString();
					model.CorpWorkedTel=dt.Rows[n]["CorpWorkedTel"].ToString();
					model.CorpFax=dt.Rows[n]["CorpFax"].ToString();
					model.CorpEmail=dt.Rows[n]["CorpEmail"].ToString();
					model.CorpWeb=dt.Rows[n]["CorpWeb"].ToString();
					model.AdminUserName=dt.Rows[n]["AdminUserName"].ToString();
					model.AdminSex=dt.Rows[n]["AdminSex"].ToString();
					model.AdminUserTel=dt.Rows[n]["AdminUserTel"].ToString();
					model.AdminUserEmail=dt.Rows[n]["AdminUserEmail"].ToString();
					model.DBServer=dt.Rows[n]["DBServer"].ToString();
					model.DBName=dt.Rows[n]["DBName"].ToString();
					model.DBUser=dt.Rows[n]["DBUser"].ToString();
					model.DBPwd=dt.Rows[n]["DBPwd"].ToString();
					if(dt.Rows[n]["RegDate"].ToString()!="")
					{
						model.RegDate=DateTime.Parse(dt.Rows[n]["RegDate"].ToString());
					}
					if(dt.Rows[n]["BranchNum"].ToString()!="")
					{
						model.BranchNum=int.Parse(dt.Rows[n]["BranchNum"].ToString());
					}
					if(dt.Rows[n]["CommNum"].ToString()!="")
					{
						model.CommNum=int.Parse(dt.Rows[n]["CommNum"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					model.ImgLogo1=dt.Rows[n]["ImgLogo1"].ToString();
					model.ImgLogo2=dt.Rows[n]["ImgLogo2"].ToString();
					model.SysDir=dt.Rows[n]["SysDir"].ToString();
					model.SysVersion=dt.Rows[n]["SysVersion"].ToString();
					model.QQ=dt.Rows[n]["QQ"].ToString();
					model.StoreImage=dt.Rows[n]["StoreImage"].ToString();
					if(dt.Rows[n]["VSPID"].ToString()!="")
					{
						model.VSPID=int.Parse(dt.Rows[n]["VSPID"].ToString());
					}
					if(dt.Rows[n]["CorpSynchCode"].ToString()!="")
					{
						model.CorpSynchCode=new Guid(dt.Rows[n]["CorpSynchCode"].ToString());
					}
					if(dt.Rows[n]["SynchFlag"].ToString()!="")
					{
						model.SynchFlag=int.Parse(dt.Rows[n]["SynchFlag"].ToString());
					}
					model.LogoImgUrl=dt.Rows[n]["LogoImgUrl"].ToString();
					if(dt.Rows[n]["CorpSort"].ToString()!="")
					{
						model.CorpSort=int.Parse(dt.Rows[n]["CorpSort"].ToString());
					}
					if(dt.Rows[n]["IsRecommend"].ToString()!="")
					{
						model.IsRecommend=int.Parse(dt.Rows[n]["IsRecommend"].ToString());
					}
					model.RecommendIndex=dt.Rows[n]["RecommendIndex"].ToString();
					if(dt.Rows[n]["ActualCommSnum"].ToString()!="")
					{
						model.ActualCommSnum=int.Parse(dt.Rows[n]["ActualCommSnum"].ToString());
					}
					model.ContractMemo=dt.Rows[n]["ContractMemo"].ToString();
					if(dt.Rows[n]["MobileSnum"].ToString()!="")
					{
						model.MobileSnum=int.Parse(dt.Rows[n]["MobileSnum"].ToString());
					}
					if(dt.Rows[n]["ActualMobileSnum"].ToString()!="")
					{
						model.ActualMobileSnum=int.Parse(dt.Rows[n]["ActualMobileSnum"].ToString());
					}
					model.CallVersion=dt.Rows[n]["CallVersion"].ToString();
					if(dt.Rows[n]["IsValidCode"].ToString()!="")
					{
						model.IsValidCode=int.Parse(dt.Rows[n]["IsValidCode"].ToString());
					}
					if(dt.Rows[n]["IsFees"].ToString()!="")
					{
						model.IsFees=int.Parse(dt.Rows[n]["IsFees"].ToString());
					}
					if(dt.Rows[n]["ContractBeginDate"].ToString()!="")
					{
						model.ContractBeginDate=DateTime.Parse(dt.Rows[n]["ContractBeginDate"].ToString());
					}
					if(dt.Rows[n]["ContractEndDate"].ToString()!="")
					{
						model.ContractEndDate=DateTime.Parse(dt.Rows[n]["ContractEndDate"].ToString());
					}
					if(dt.Rows[n]["UseBeginDate"].ToString()!="")
					{
						model.UseBeginDate=DateTime.Parse(dt.Rows[n]["UseBeginDate"].ToString());
					}
					if(dt.Rows[n]["UseEndDate"].ToString()!="")
					{
						model.UseEndDate=DateTime.Parse(dt.Rows[n]["UseEndDate"].ToString());
					}
					if(dt.Rows[n]["AllowanceDate"].ToString()!="")
					{
						model.AllowanceDate=DateTime.Parse(dt.Rows[n]["AllowanceDate"].ToString());
					}
					model.YiZhiFuNum=dt.Rows[n]["YiZhiFuNum"].ToString();
					model.YiZhiFuKey=dt.Rows[n]["YiZhiFuKey"].ToString();
					model.YiJiFuHuoBanId=dt.Rows[n]["YiJiFuHuoBanId"].ToString();
					model.YiJiFuNum=dt.Rows[n]["YiJiFuNum"].ToString();
					model.YiJiFuKey=dt.Rows[n]["YiJiFuKey"].ToString();
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

        public DataTable System_Corp_GetVersion(int CorpID)
        {
            return dal.System_Corp_GetVersion(CorpID);
        }
	}
}

