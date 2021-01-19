using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.SQMSys;
namespace MobileSoft.BLL.SQMSys
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_Sys_PowerNodeTemp ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_Sys_PowerNodeTemp
	{
		private readonly MobileSoft.DAL.SQMSys.Dal_Tb_Sys_PowerNodeTemp dal=new MobileSoft.DAL.SQMSys.Dal_Tb_Sys_PowerNodeTemp();
		public Bll_Tb_Sys_PowerNodeTemp()
		{}
		#region  ��Ա����

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeTemp model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeTemp model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete()
		{
			//�ñ���������Ϣ�����Զ�������/�����ֶ�
			dal.Delete();
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeTemp GetModel()
		{
			//�ñ���������Ϣ�����Զ�������/�����ֶ�
			return dal.GetModel();
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeTemp GetModelByCache()
		{
			//�ñ���������Ϣ�����Զ�������/�����ֶ�
			string CacheKey = "Tb_Sys_PowerNodeTempModel-" ;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel();
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeTemp)objModel;
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
		public List<MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeTemp> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeTemp> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeTemp> modelList = new List<MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeTemp>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeTemp model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeTemp();
					model.UserRoles=dt.Rows[n]["UserRoles"].ToString();
					model.PNodeCode=dt.Rows[n]["PNodeCode"].ToString();
					model.PNodeName=dt.Rows[n]["PNodeName"].ToString();
					model.URLPage=dt.Rows[n]["URLPage"].ToString();
					model.URLTarget=dt.Rows[n]["URLTarget"].ToString();
					model.BackTitleImg=dt.Rows[n]["BackTitleImg"].ToString();
					model.Narrate=dt.Rows[n]["Narrate"].ToString();
					if(dt.Rows[n]["InPopedom"].ToString()!="")
					{
						model.InPopedom=int.Parse(dt.Rows[n]["InPopedom"].ToString());
					}
					model.Functions=dt.Rows[n]["Functions"].ToString();
					if(dt.Rows[n]["NodeType"].ToString()!="")
					{
						model.NodeType=int.Parse(dt.Rows[n]["NodeType"].ToString());
					}
					if(dt.Rows[n]["PNodeSort"].ToString()!="")
					{
						model.PNodeSort=int.Parse(dt.Rows[n]["PNodeSort"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
					}
					model.PrentPNodeCode=dt.Rows[n]["PrentPNodeCode"].ToString();
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

