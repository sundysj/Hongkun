using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Unify;
namespace MobileSoft.BLL.Unify
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Unify_Customer 的摘要说明。
	/// </summary>
	public class Bll_Tb_Unify_Customer
	{
		private readonly MobileSoft.DAL.Unify.Dal_Tb_Unify_Customer dal=new MobileSoft.DAL.Unify.Dal_Tb_Unify_Customer();
		public Bll_Tb_Unify_Customer()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid CustSynchCode)
		{
			return dal.Exists(CustSynchCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.Unify.Tb_Unify_Customer model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Unify.Tb_Unify_Customer model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(Guid CustSynchCode)
		{
			
			dal.Delete(CustSynchCode);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Unify.Tb_Unify_Customer GetModel(Guid CustSynchCode)
		{
			
			return dal.GetModel(CustSynchCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.Unify.Tb_Unify_Customer GetModelByCache(Guid CustSynchCode)
		{
			
			string CacheKey = "Tb_Unify_CustomerModel-" + CustSynchCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CustSynchCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Unify.Tb_Unify_Customer)objModel;
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
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(string Fields,int Top, string strWhere, string fieldOrder)
        {
            return dal.GetList(Fields,Top, strWhere, fieldOrder);
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Unify.Tb_Unify_Customer> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.Unify.Tb_Unify_Customer> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Unify.Tb_Unify_Customer> modelList = new List<MobileSoft.Model.Unify.Tb_Unify_Customer>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Unify.Tb_Unify_Customer model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Unify.Tb_Unify_Customer();
					if(dt.Rows[n]["CustSynchCode"].ToString()!="")
					{
						model.CustSynchCode=new Guid(dt.Rows[n]["CustSynchCode"].ToString());
					}
					if(dt.Rows[n]["CommSynchCode"].ToString()!="")
					{
						model.CommSynchCode=new Guid(dt.Rows[n]["CommSynchCode"].ToString());
					}
					//model.UnCustID=dt.Rows[n]["UnCustID"].ToString();
					model.CustTypeName=dt.Rows[n]["CustTypeName"].ToString();
					model.CustName=dt.Rows[n]["CustName"].ToString();
					model.NickName=dt.Rows[n]["NickName"].ToString();
					model.FixedTel=dt.Rows[n]["FixedTel"].ToString();
					model.MobilePhone=dt.Rows[n]["MobilePhone"].ToString();
					model.FaxTel=dt.Rows[n]["FaxTel"].ToString();
					model.EMail=dt.Rows[n]["EMail"].ToString();
					model.LoginPwd=dt.Rows[n]["LoginPwd"].ToString();
					model.Surname=dt.Rows[n]["Surname"].ToString();
					model.Name=dt.Rows[n]["Name"].ToString();
					model.Sex=dt.Rows[n]["Sex"].ToString();
					if(dt.Rows[n]["Birthday"].ToString()!="")
					{
						model.Birthday=DateTime.Parse(dt.Rows[n]["Birthday"].ToString());
					}
					model.Nationality=dt.Rows[n]["Nationality"].ToString();
					model.PaperName=dt.Rows[n]["PaperName"].ToString();
					model.PaperCode=dt.Rows[n]["PaperCode"].ToString();
					model.PassSign=dt.Rows[n]["PassSign"].ToString();
					model.Address=dt.Rows[n]["Address"].ToString();
					model.PostCode=dt.Rows[n]["PostCode"].ToString();
					model.MGradeSign=dt.Rows[n]["MGradeSign"].ToString();
					model.Recipient=dt.Rows[n]["Recipient"].ToString();
					model.Interests=dt.Rows[n]["Interests"].ToString();
					model.Hobbies=dt.Rows[n]["Hobbies"].ToString();
					model.WorkUnit=dt.Rows[n]["WorkUnit"].ToString();
					model.Job=dt.Rows[n]["Job"].ToString();
					model.Linkman=dt.Rows[n]["Linkman"].ToString();
					model.LinkmanTel=dt.Rows[n]["LinkmanTel"].ToString();
					model.SmsTel=dt.Rows[n]["SmsTel"].ToString();
					if(dt.Rows[n]["IsUnit"].ToString()!="")
					{
						model.IsUnit=int.Parse(dt.Rows[n]["IsUnit"].ToString());
					}
					model.LegalRepr=dt.Rows[n]["LegalRepr"].ToString();
					model.LegalReprTel=dt.Rows[n]["LegalReprTel"].ToString();
					model.Charge=dt.Rows[n]["Charge"].ToString();
					model.ChargeTel=dt.Rows[n]["ChargeTel"].ToString();
					if(dt.Rows[n]["ProvinceID"].ToString()!="")
					{
						model.ProvinceID=int.Parse(dt.Rows[n]["ProvinceID"].ToString());
					}
					if(dt.Rows[n]["CityID"].ToString()!="")
					{
						model.CityID=int.Parse(dt.Rows[n]["CityID"].ToString());
					}
					if(dt.Rows[n]["BoroughID"].ToString()!="")
					{
						model.BoroughID=int.Parse(dt.Rows[n]["BoroughID"].ToString());
					}
					if(dt.Rows[n]["RoomCount"].ToString()!="")
					{
						model.RoomCount=int.Parse(dt.Rows[n]["RoomCount"].ToString());
					}
					model.RoomSigns=dt.Rows[n]["RoomSigns"].ToString();
					model.Memo=dt.Rows[n]["Memo"].ToString();
					if(dt.Rows[n]["LiveType1"].ToString()!="")
					{
						model.LiveType1=int.Parse(dt.Rows[n]["LiveType1"].ToString());
					}
					if(dt.Rows[n]["LiveType2"].ToString()!="")
					{
						model.LiveType2=int.Parse(dt.Rows[n]["LiveType2"].ToString());
					}
					if(dt.Rows[n]["LiveType3"].ToString()!="")
					{
						model.LiveType3=int.Parse(dt.Rows[n]["LiveType3"].ToString());
					}
					if(dt.Rows[n]["IsDebts"].ToString()!="")
					{
						model.IsDebts=int.Parse(dt.Rows[n]["IsDebts"].ToString());
					}
					if(dt.Rows[n]["IsUsual"].ToString()!="")
					{
						model.IsUsual=int.Parse(dt.Rows[n]["IsUsual"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["SynchFlag"].ToString()!="")
					{
						model.SynchFlag=int.Parse(dt.Rows[n]["SynchFlag"].ToString());
					}
					model.HeadImgURL=dt.Rows[n]["HeadImgURL"].ToString();
					model.Introduce=dt.Rows[n]["Introduce"].ToString();
					model.Hometown=dt.Rows[n]["Hometown"].ToString();
					model.BirAnimal=dt.Rows[n]["BirAnimal"].ToString();
					model.Constellation=dt.Rows[n]["Constellation"].ToString();
					model.BloodGroup=dt.Rows[n]["BloodGroup"].ToString();
					model.Stature=dt.Rows[n]["Stature"].ToString();
					model.BodilyForm=dt.Rows[n]["BodilyForm"].ToString();
					model.MaritalStatus=dt.Rows[n]["MaritalStatus"].ToString();
					model.Qualification=dt.Rows[n]["Qualification"].ToString();
					model.GraduateSchool=dt.Rows[n]["GraduateSchool"].ToString();
					model.Occupation=dt.Rows[n]["Occupation"].ToString();
					model.Telphone=dt.Rows[n]["Telphone"].ToString();
					model.MobileTel=dt.Rows[n]["MobileTel"].ToString();
					model.EmailSign=dt.Rows[n]["EmailSign"].ToString();
					model.HomePage=dt.Rows[n]["HomePage"].ToString();
					model.QQSign=dt.Rows[n]["QQSign"].ToString();
					model.MSNSign=dt.Rows[n]["MSNSign"].ToString();
					model.BroadbandNum=dt.Rows[n]["BroadbandNum"].ToString();
					model.TeleFixedTel=dt.Rows[n]["TeleFixedTel"].ToString();
					model.TeleMobile=dt.Rows[n]["TeleMobile"].ToString();
					model.IPTVAccount=dt.Rows[n]["IPTVAccount"].ToString();
					if(dt.Rows[n]["TotalPosts"].ToString()!="")
					{
						model.TotalPosts=int.Parse(dt.Rows[n]["TotalPosts"].ToString());
					}
					if(dt.Rows[n]["Experience"].ToString()!="")
					{
						model.Experience=int.Parse(dt.Rows[n]["Experience"].ToString());
					}
					model.CustCardSign=dt.Rows[n]["CustCardSign"].ToString();
					if(dt.Rows[n]["CardSendDate"].ToString()!="")
					{
						model.CardSendDate=DateTime.Parse(dt.Rows[n]["CardSendDate"].ToString());
					}
					if(dt.Rows[n]["CardActiveDate"].ToString()!="")
					{
						model.CardActiveDate=DateTime.Parse(dt.Rows[n]["CardActiveDate"].ToString());
					}
					if(dt.Rows[n]["CardLossDates"].ToString()!="")
					{
						model.CardLossDates=DateTime.Parse(dt.Rows[n]["CardLossDates"].ToString());
					}
					if(dt.Rows[n]["IsCardSend"].ToString()!="")
					{
						model.IsCardSend=int.Parse(dt.Rows[n]["IsCardSend"].ToString());
					}
					if(dt.Rows[n]["IsCardActive"].ToString()!="")
					{
						model.IsCardActive=int.Parse(dt.Rows[n]["IsCardActive"].ToString());
					}
					if(dt.Rows[n]["IsCardLoss"].ToString()!="")
					{
						model.IsCardLoss=int.Parse(dt.Rows[n]["IsCardLoss"].ToString());
					}
					//model.OrdSNum=dt.Rows[n]["OrdSNum"].ToString();
					model.WanQinId=dt.Rows[n]["WanQinId"].ToString();
					model.WanQinPass=dt.Rows[n]["WanQinPass"].ToString();
					model.ShiPingId=dt.Rows[n]["ShiPingId"].ToString();
					model.ShiPingPass=dt.Rows[n]["ShiPingPass"].ToString();
					model.ZhiLenId=dt.Rows[n]["ZhiLenId"].ToString();
					model.ZhiLenPass=dt.Rows[n]["ZhiLenPass"].ToString();
					model.WaterCardNum=dt.Rows[n]["WaterCardNum"].ToString();
					model.WaterCardPass=dt.Rows[n]["WaterCardPass"].ToString();
					model.ElectricityNum=dt.Rows[n]["ElectricityNum"].ToString();
					model.ElectricityPass=dt.Rows[n]["ElectricityPass"].ToString();
					model.GasNum=dt.Rows[n]["GasNum"].ToString();
					model.GasPass=dt.Rows[n]["GasPass"].ToString();
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

