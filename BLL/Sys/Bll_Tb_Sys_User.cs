using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Sys;
namespace MobileSoft.BLL.Sys
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Sys_User 的摘要说明。
	/// </summary>
	public class Bll_Tb_Sys_User
	{
		private readonly MobileSoft.DAL.Sys.Dal_Tb_Sys_User dal=new MobileSoft.DAL.Sys.Dal_Tb_Sys_User();
		public Bll_Tb_Sys_User()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string UserCode)
		{
			return dal.Exists(UserCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.Sys.Tb_Sys_User model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Sys.Tb_Sys_User model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string UserCode)
		{
			
			dal.Delete(UserCode);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_User GetModel(string UserCode)
		{
			
			return dal.GetModel(UserCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_User GetModelByCache(string UserCode)
		{
			
			string CacheKey = "Tb_Sys_UserModel-" + UserCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(UserCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Sys.Tb_Sys_User)objModel;
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
		public List<MobileSoft.Model.Sys.Tb_Sys_User> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Sys.Tb_Sys_User> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Sys.Tb_Sys_User> modelList = new List<MobileSoft.Model.Sys.Tb_Sys_User>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Sys.Tb_Sys_User model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Sys.Tb_Sys_User();
					model.UserCode=dt.Rows[n]["UserCode"].ToString();
					if(dt.Rows[n]["CommID"].ToString()!="")
					{
						model.CommID=int.Parse(dt.Rows[n]["CommID"].ToString());
					}
					model.UserName=dt.Rows[n]["UserName"].ToString();
					model.DepCode=dt.Rows[n]["DepCode"].ToString();
					model.LoginCode=dt.Rows[n]["LoginCode"].ToString();
					model.PassWord=dt.Rows[n]["PassWord"].ToString();
					model.Sex=dt.Rows[n]["Sex"].ToString();
					model.MobileTel=dt.Rows[n]["MobileTel"].ToString();
					model.WorkedTel=dt.Rows[n]["WorkedTel"].ToString();
					model.PersonalTel=dt.Rows[n]["PersonalTel"].ToString();
					model.IDCardCode=dt.Rows[n]["IDCardCode"].ToString();
					model.LinkmanName=dt.Rows[n]["LinkmanName"].ToString();
					model.LinkmanTel=dt.Rows[n]["LinkmanTel"].ToString();
					model.Email=dt.Rows[n]["Email"].ToString();
					if(dt.Rows[n]["IsPublicMail"].ToString()!="")
					{
						model.IsPublicMail=int.Parse(dt.Rows[n]["IsPublicMail"].ToString());
					}
					model.EmployeeCode=dt.Rows[n]["EmployeeCode"].ToString();
					model.WorkerSign=dt.Rows[n]["WorkerSign"].ToString();
					model.Duty=dt.Rows[n]["Duty"].ToString();
					model.BankAccounts=dt.Rows[n]["BankAccounts"].ToString();
					model.Qualification=dt.Rows[n]["Qualification"].ToString();
					model.NativePlace=dt.Rows[n]["NativePlace"].ToString();
					model.Speciality=dt.Rows[n]["Speciality"].ToString();
					model.MaritalStatus=dt.Rows[n]["MaritalStatus"].ToString();
					model.Stature=dt.Rows[n]["Stature"].ToString();
					if(dt.Rows[n]["Birthday"].ToString()!="")
					{
						model.Birthday=DateTime.Parse(dt.Rows[n]["Birthday"].ToString());
					}
					model.Avoirdupois=dt.Rows[n]["Avoirdupois"].ToString();
					model.Nation=dt.Rows[n]["Nation"].ToString();
					model.Politics=dt.Rows[n]["Politics"].ToString();
					model.TechnicTitle=dt.Rows[n]["TechnicTitle"].ToString();
					model.Skills=dt.Rows[n]["Skills"].ToString();
					model.GuardCard=dt.Rows[n]["GuardCard"].ToString();
					model.Computer=dt.Rows[n]["Computer"].ToString();
					if(dt.Rows[n]["WorkingTime"].ToString()!="")
					{
						model.WorkingTime=DateTime.Parse(dt.Rows[n]["WorkingTime"].ToString());
					}
					if(dt.Rows[n]["JoinTime"].ToString()!="")
					{
						model.JoinTime=DateTime.Parse(dt.Rows[n]["JoinTime"].ToString());
					}
					model.Residence=dt.Rows[n]["Residence"].ToString();
					model.Address=dt.Rows[n]["Address"].ToString();
					model.Post=dt.Rows[n]["Post"].ToString();
					model.ArchivesAddr=dt.Rows[n]["ArchivesAddr"].ToString();
					model.PreInsuranceCode=dt.Rows[n]["PreInsuranceCode"].ToString();
					model.InsuranceCode=dt.Rows[n]["InsuranceCode"].ToString();
					model.ResidenceType=dt.Rows[n]["ResidenceType"].ToString();
					if(dt.Rows[n]["IsBuyInsurance"].ToString()!="")
					{
						model.IsBuyInsurance=int.Parse(dt.Rows[n]["IsBuyInsurance"].ToString());
					}
					if(dt.Rows[n]["IsAgreeBuy"].ToString()!="")
					{
						model.IsAgreeBuy=int.Parse(dt.Rows[n]["IsAgreeBuy"].ToString());
					}
					model.PayLevel=dt.Rows[n]["PayLevel"].ToString();
					if(dt.Rows[n]["WorkingDate"].ToString()!="")
					{
						model.WorkingDate=DateTime.Parse(dt.Rows[n]["WorkingDate"].ToString());
					}
					if(dt.Rows[n]["JionDate"].ToString()!="")
					{
						model.JionDate=DateTime.Parse(dt.Rows[n]["JionDate"].ToString());
					}
					if(dt.Rows[n]["ProbationDate"].ToString()!="")
					{
						model.ProbationDate=DateTime.Parse(dt.Rows[n]["ProbationDate"].ToString());
					}
					if(dt.Rows[n]["FormalDate"].ToString()!="")
					{
						model.FormalDate=DateTime.Parse(dt.Rows[n]["FormalDate"].ToString());
					}
					if(dt.Rows[n]["DimissionDate"].ToString()!="")
					{
						model.DimissionDate=DateTime.Parse(dt.Rows[n]["DimissionDate"].ToString());
					}
					if(dt.Rows[n]["InureDate"].ToString()!="")
					{
						model.InureDate=DateTime.Parse(dt.Rows[n]["InureDate"].ToString());
					}
					if(dt.Rows[n]["PraxisDate"].ToString()!="")
					{
						model.PraxisDate=DateTime.Parse(dt.Rows[n]["PraxisDate"].ToString());
					}
					if(dt.Rows[n]["JoinPartyDate"].ToString()!="")
					{
						model.JoinPartyDate=DateTime.Parse(dt.Rows[n]["JoinPartyDate"].ToString());
					}
					model.HealthState=dt.Rows[n]["HealthState"].ToString();
					model.OldName=dt.Rows[n]["OldName"].ToString();
					model.EyeLeft=dt.Rows[n]["EyeLeft"].ToString();
					model.EyeRight=dt.Rows[n]["EyeRight"].ToString();
					model.FamilyBirth=dt.Rows[n]["FamilyBirth"].ToString();
					model.BloodGroup=dt.Rows[n]["BloodGroup"].ToString();
					model.School=dt.Rows[n]["School"].ToString();
					model.Approach=dt.Rows[n]["Approach"].ToString();
					model.OldUnit=dt.Rows[n]["OldUnit"].ToString();
					model.ForRegional=dt.Rows[n]["ForRegional"].ToString();
					if(dt.Rows[n]["ArchiveState"].ToString()!="")
					{
						model.ArchiveState=int.Parse(dt.Rows[n]["ArchiveState"].ToString());
					}
					model.Memo=dt.Rows[n]["Memo"].ToString();
					if(dt.Rows[n]["IsShow"].ToString()!="")
					{
						model.IsShow=int.Parse(dt.Rows[n]["IsShow"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["DeleteDate"].ToString()!="")
					{
						model.DeleteDate=DateTime.Parse(dt.Rows[n]["DeleteDate"].ToString());
					}
					model.WorkAchievement=dt.Rows[n]["WorkAchievement"].ToString();
					model.FamilyContext=dt.Rows[n]["FamilyContext"].ToString();
					model.InterestLove=dt.Rows[n]["InterestLove"].ToString();
					model.StudySuffer=dt.Rows[n]["StudySuffer"].ToString();
					model.JobSuffer=dt.Rows[n]["JobSuffer"].ToString();
					model.ChangeLog=dt.Rows[n]["ChangeLog"].ToString();
					if(dt.Rows[n]["IsLogin"].ToString()!="")
					{
						model.IsLogin=int.Parse(dt.Rows[n]["IsLogin"].ToString());
					}
					if(dt.Rows[n]["IsDimission"].ToString()!="")
					{
						model.IsDimission=int.Parse(dt.Rows[n]["IsDimission"].ToString());
					}
					if(dt.Rows[n]["IsHostelSearch"].ToString()!="")
					{
						model.IsHostelSearch=int.Parse(dt.Rows[n]["IsHostelSearch"].ToString());
					}
					model.OrganizeCode=dt.Rows[n]["OrganizeCode"].ToString();
					if(dt.Rows[n]["SearchCommID"].ToString()!="")
					{
						model.SearchCommID=int.Parse(dt.Rows[n]["SearchCommID"].ToString());
					}
					if(dt.Rows[n]["IsMobile"].ToString()!="")
					{
						model.IsMobile=int.Parse(dt.Rows[n]["IsMobile"].ToString());
					}
					model.MachineCodes=dt.Rows[n]["MachineCodes"].ToString();
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

            /// <summary>
            /// 获得数据列表
            /// </summary>
            public DataTable FilterRoles(string UserCode, string OrganCode, int CommID)
            {
                  return dal.FilterRoles(UserCode, OrganCode, CommID);
            }

            public string Sys_User_FilterRoles(string UserCode, string OrganCode, int CommID)
            {
                return dal.Sys_User_FilterRoles(UserCode, OrganCode, CommID);
            }
		#endregion  成员方法
	}
}

