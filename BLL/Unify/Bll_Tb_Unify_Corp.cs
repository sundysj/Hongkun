using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.Unify;
namespace MobileSoft.BLL.Unify
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Unify_Corp ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Unify_Corp
	{
		private readonly MobileSoft.DAL.Unify.Dal_Tb_Unify_Corp dal=new MobileSoft.DAL.Unify.Dal_Tb_Unify_Corp();
		public Bll_Tb_Unify_Corp()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(Guid CorpSynchCode)
		{
			return dal.Exists(CorpSynchCode);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.Unify.Tb_Unify_Corp model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Unify.Tb_Unify_Corp model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(Guid CorpSynchCode)
		{
			
			dal.Delete(CorpSynchCode);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Unify.Tb_Unify_Corp GetModel(Guid CorpSynchCode)
		{
			
			return dal.GetModel(CorpSynchCode);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.Unify.Tb_Unify_Corp GetModelByCache(Guid CorpSynchCode)
		{
			
			string CacheKey = "Tb_Unify_CorpModel-" + CorpSynchCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CorpSynchCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.Unify.Tb_Unify_Corp)objModel;
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Unify.Tb_Unify_Corp> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.Unify.Tb_Unify_Corp> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.Unify.Tb_Unify_Corp> modelList = new List<MobileSoft.Model.Unify.Tb_Unify_Corp>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.Unify.Tb_Unify_Corp model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.Unify.Tb_Unify_Corp();
					if(dt.Rows[n]["CorpSynchCode"].ToString()!="")
					{
						model.CorpSynchCode=new Guid(dt.Rows[n]["CorpSynchCode"].ToString());
					}
					//model.UnCorpID=dt.Rows[n]["UnCorpID"].ToString();
					if(dt.Rows[n]["VSPID"].ToString()!="")
					{
						model.VSPID=int.Parse(dt.Rows[n]["VSPID"].ToString());
					}
					if(dt.Rows[n]["CorpID"].ToString()!="")
					{
						model.CorpID=int.Parse(dt.Rows[n]["CorpID"].ToString());
					}
					model.CorpName=dt.Rows[n]["CorpName"].ToString();
					model.Province=dt.Rows[n]["Province"].ToString();
					model.City=dt.Rows[n]["City"].ToString();
					model.Borough=dt.Rows[n]["Borough"].ToString();
					if(dt.Rows[n]["RegMode"].ToString()!="")
					{
						model.RegMode=int.Parse(dt.Rows[n]["RegMode"].ToString());
					}
					model.CorpTypeName=dt.Rows[n]["CorpTypeName"].ToString();
					model.CorpAddress=dt.Rows[n]["CorpAddress"].ToString();
					model.CorpPost=dt.Rows[n]["CorpPost"].ToString();
					model.CorpDeputy=dt.Rows[n]["CorpDeputy"].ToString();
					model.CorpLinkMan=dt.Rows[n]["CorpLinkMan"].ToString();
					model.CorpMobileTel=dt.Rows[n]["CorpMobileTel"].ToString();
					model.CorpWorkedTel=dt.Rows[n]["CorpWorkedTel"].ToString();
					model.CorpFax=dt.Rows[n]["CorpFax"].ToString();
					model.CorpEmail=dt.Rows[n]["CorpEmail"].ToString();
					model.CorpWeb=dt.Rows[n]["CorpWeb"].ToString();
					if(dt.Rows[n]["RegDate"].ToString()!="")
					{
						model.RegDate=DateTime.Parse(dt.Rows[n]["RegDate"].ToString());
					}
					model.SysVersion=dt.Rows[n]["SysVersion"].ToString();
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					if(dt.Rows[n]["SynchFlag"].ToString()!="")
					{
						model.SynchFlag=int.Parse(dt.Rows[n]["SynchFlag"].ToString());
					}
					model.ServerIP=dt.Rows[n]["ServerIP"].ToString();
					model.SysDir=dt.Rows[n]["SysDir"].ToString();
					if(dt.Rows[n]["CorpSort"].ToString()!="")
					{
						model.CorpSort=int.Parse(dt.Rows[n]["CorpSort"].ToString());
					}
					if(dt.Rows[n]["IsRecommend"].ToString()!="")
					{
						model.IsRecommend=int.Parse(dt.Rows[n]["IsRecommend"].ToString());
					}
					model.RecommendIndex=dt.Rows[n]["RecommendIndex"].ToString();
					model.LogoImgUrl=dt.Rows[n]["LogoImgUrl"].ToString();
					model.CorpShortName=dt.Rows[n]["CorpShortName"].ToString();
					if(dt.Rows[n]["CorpSNum"].ToString()!="")
					{
						model.CorpSNum=int.Parse(dt.Rows[n]["CorpSNum"].ToString());
					}
					model.Street=dt.Rows[n]["Street"].ToString();
					model.CommunityName=dt.Rows[n]["CommunityName"].ToString();
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

