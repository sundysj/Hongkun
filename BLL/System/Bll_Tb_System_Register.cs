using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.System;
namespace MobileSoft.BLL.System
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_System_Register ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_System_Register
	{
		private readonly MobileSoft.DAL.System.Dal_Tb_System_Register dal=new MobileSoft.DAL.System.Dal_Tb_System_Register();
		public Bll_Tb_System_Register()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long RegID,int RegType)
		{
			return dal.Exists(RegID,RegType);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.System.Tb_System_Register model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Register model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long RegID,int RegType)
		{
			
			dal.Delete(RegID,RegType);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Register GetModel(long RegID,int RegType)
		{
			
			return dal.GetModel(RegID,RegType);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Register GetModelByCache(long RegID,int RegType)
		{
			
			string CacheKey = "Tb_System_RegisterModel-" + RegID+RegType;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(RegID,RegType);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.System.Tb_System_Register)objModel;
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
		public List<MobileSoft.Model.System.Tb_System_Register> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.System.Tb_System_Register> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.System.Tb_System_Register> modelList = new List<MobileSoft.Model.System.Tb_System_Register>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.System.Tb_System_Register model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.System.Tb_System_Register();
					//model.RegID=dt.Rows[n]["RegID"].ToString();
					if(dt.Rows[n]["RegType"].ToString()!="")
					{
						model.RegType=int.Parse(dt.Rows[n]["RegType"].ToString());
					}
					if(dt.Rows[n]["RegMode"].ToString()!="")
					{
						model.RegMode=int.Parse(dt.Rows[n]["RegMode"].ToString());
					}
					model.RegUserName=dt.Rows[n]["RegUserName"].ToString();
					model.RegPassWord=dt.Rows[n]["RegPassWord"].ToString();
					model.RegQuestion=dt.Rows[n]["RegQuestion"].ToString();
					model.RegAnswer=dt.Rows[n]["RegAnswer"].ToString();
					model.RegCorpName=dt.Rows[n]["RegCorpName"].ToString();
					model.CorpAddress=dt.Rows[n]["CorpAddress"].ToString();
					model.ZipCode=dt.Rows[n]["ZipCode"].ToString();
					model.LinkMan=dt.Rows[n]["LinkMan"].ToString();
					model.Tel=dt.Rows[n]["Tel"].ToString();
					model.Fax=dt.Rows[n]["Fax"].ToString();
					model.Email=dt.Rows[n]["Email"].ToString();
					model.WebName=dt.Rows[n]["WebName"].ToString();
					if(dt.Rows[n]["RegDate"].ToString()!="")
					{
						model.RegDate=DateTime.Parse(dt.Rows[n]["RegDate"].ToString());
					}
					if(dt.Rows[n]["IsAuditing"].ToString()!="")
					{
						model.IsAuditing=int.Parse(dt.Rows[n]["IsAuditing"].ToString());
					}
					if(dt.Rows[n]["IsSucc"].ToString()!="")
					{
						model.IsSucc=int.Parse(dt.Rows[n]["IsSucc"].ToString());
					}
					model.ReturnMsg=dt.Rows[n]["ReturnMsg"].ToString();
					model.Province=dt.Rows[n]["Province"].ToString();
					model.City=dt.Rows[n]["City"].ToString();
					model.Borough=dt.Rows[n]["Borough"].ToString();
					model.CorpTypeName=dt.Rows[n]["CorpTypeName"].ToString();
					model.Street=dt.Rows[n]["Street"].ToString();
					model.CommName=dt.Rows[n]["CommName"].ToString();
					if(dt.Rows[n]["VSPID"].ToString()!="")
					{
						model.VSPID=int.Parse(dt.Rows[n]["VSPID"].ToString());
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

