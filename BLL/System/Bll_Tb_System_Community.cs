using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.System;
namespace MobileSoft.BLL.System
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_System_Community ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_System_Community
	{
		private readonly MobileSoft.DAL.System.Dal_Tb_System_Community dal=new MobileSoft.DAL.System.Dal_Tb_System_Community();
		public Bll_Tb_System_Community()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(Guid CommCode,long CommID)
		{
			return dal.Exists(CommCode,CommID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.System.Tb_System_Community model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Community model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(Guid CommCode,long CommID)
		{
			
			dal.Delete(CommCode,CommID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Community GetModel(Guid CommCode,long CommID)
		{
			
			return dal.GetModel(CommCode,CommID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Community GetModelByCache(Guid CommCode,long CommID)
		{
			
			string CacheKey = "Tb_System_CommunityModel-" + CommCode+CommID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CommCode,CommID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.System.Tb_System_Community)objModel;
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
		public List<MobileSoft.Model.System.Tb_System_Community> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.System.Tb_System_Community> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.System.Tb_System_Community> modelList = new List<MobileSoft.Model.System.Tb_System_Community>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.System.Tb_System_Community model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.System.Tb_System_Community();
					if(dt.Rows[n]["CommCode"].ToString()!="")
					{
						model.CommCode=new Guid(dt.Rows[n]["CommCode"].ToString());
					}
					//model.CommID=dt.Rows[n]["CommID"].ToString();
					if(dt.Rows[n]["RegType"].ToString()!="")
					{
						model.RegType=int.Parse(dt.Rows[n]["RegType"].ToString());
					}
					if(dt.Rows[n]["RegMode"].ToString()!="")
					{
						model.RegMode=int.Parse(dt.Rows[n]["RegMode"].ToString());
					}
					model.CommName=dt.Rows[n]["CommName"].ToString();
					model.CommShortName=dt.Rows[n]["CommShortName"].ToString();
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
					if(dt.Rows[n]["StreetID"].ToString()!="")
					{
						model.StreetID=int.Parse(dt.Rows[n]["StreetID"].ToString());
					}
					model.GateSign=dt.Rows[n]["GateSign"].ToString();
					model.CommSpell=dt.Rows[n]["CommSpell"].ToString();
					model.LoginCode=dt.Rows[n]["LoginCode"].ToString();
					model.LoginPassWD=dt.Rows[n]["LoginPassWD"].ToString();
					model.PwdQuestion=dt.Rows[n]["PwdQuestion"].ToString();
					model.PwdAnswer=dt.Rows[n]["PwdAnswer"].ToString();
					model.CommAddress=dt.Rows[n]["CommAddress"].ToString();
					model.CommPost=dt.Rows[n]["CommPost"].ToString();
					model.CommDeputy=dt.Rows[n]["CommDeputy"].ToString();
					model.CommLinkMan=dt.Rows[n]["CommLinkMan"].ToString();
					model.CommMobileTel=dt.Rows[n]["CommMobileTel"].ToString();
					model.CommWorkedTel=dt.Rows[n]["CommWorkedTel"].ToString();
					model.CommFax=dt.Rows[n]["CommFax"].ToString();
					model.CommEmail=dt.Rows[n]["CommEmail"].ToString();
					model.CommWeb=dt.Rows[n]["CommWeb"].ToString();
					model.SysVersion=dt.Rows[n]["SysVersion"].ToString();
					if(dt.Rows[n]["RegDate"].ToString()!="")
					{
						model.RegDate=DateTime.Parse(dt.Rows[n]["RegDate"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["VSPID"].ToString()!="")
					{
						model.VSPID=int.Parse(dt.Rows[n]["VSPID"].ToString());
					}
					if(dt.Rows[n]["SynchFlag"].ToString()!="")
					{
						model.SynchFlag=int.Parse(dt.Rows[n]["SynchFlag"].ToString());
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

