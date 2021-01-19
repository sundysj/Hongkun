using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.SQMSys;
namespace MobileSoft.BLL.SQMSys
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Sys_Field ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Sys_Field
	{
		private readonly MobileSoft.DAL.SQMSys.Dal_Tb_Sys_Field dal=new MobileSoft.DAL.SQMSys.Dal_Tb_Sys_Field();
		public Bll_Tb_Sys_Field()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(Guid FieldCode)
		{
			return dal.Exists(FieldCode);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.SQMSys.Tb_Sys_Field model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_Field model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(Guid FieldCode)
		{
			
			dal.Delete(FieldCode);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_Field GetModel(Guid FieldCode)
		{
			
			return dal.GetModel(FieldCode);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_Field GetModelByCache(Guid FieldCode)
		{
			
			string CacheKey = "Tb_Sys_FieldModel-" + FieldCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(FieldCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.SQMSys.Tb_Sys_Field)objModel;
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
		public List<MobileSoft.Model.SQMSys.Tb_Sys_Field> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.SQMSys.Tb_Sys_Field> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.SQMSys.Tb_Sys_Field> modelList = new List<MobileSoft.Model.SQMSys.Tb_Sys_Field>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.SQMSys.Tb_Sys_Field model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.SQMSys.Tb_Sys_Field();
					if(dt.Rows[n]["FieldCode"].ToString()!="")
					{
						model.FieldCode=new Guid(dt.Rows[n]["FieldCode"].ToString());
					}
					model.TemplateCode=dt.Rows[n]["TemplateCode"].ToString();
					model.FieldSign=dt.Rows[n]["FieldSign"].ToString();
					model.FieldName=dt.Rows[n]["FieldName"].ToString();
					model.DefaultName=dt.Rows[n]["DefaultName"].ToString();
					if(dt.Rows[n]["FieldOrderID"].ToString()!="")
					{
						model.FieldOrderID=int.Parse(dt.Rows[n]["FieldOrderID"].ToString());
					}
					if(dt.Rows[n]["FieldType"].ToString()!="")
					{
						model.FieldType=int.Parse(dt.Rows[n]["FieldType"].ToString());
					}
					if(dt.Rows[n]["FieldLength"].ToString()!="")
					{
						model.FieldLength=int.Parse(dt.Rows[n]["FieldLength"].ToString());
					}
					if(dt.Rows[n]["IsUsed"].ToString()!="")
					{
						model.IsUsed=int.Parse(dt.Rows[n]["IsUsed"].ToString());
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

