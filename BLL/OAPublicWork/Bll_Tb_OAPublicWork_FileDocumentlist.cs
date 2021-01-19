using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.OAPublicWork;
namespace MobileSoft.BLL.OAPublicWork
{
	/// <summary>
	/// ҵ���߼���Bll_Tb_OAPublicWork_FileDocumentlist ��ժҪ˵����
	/// </summary>
	public class Bll_Tb_OAPublicWork_FileDocumentlist
	{
		private readonly MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_FileDocumentlist dal=new MobileSoft.DAL.OAPublicWork.Dal_Tb_OAPublicWork_FileDocumentlist();
		public Bll_Tb_OAPublicWork_FileDocumentlist()
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
		public bool Exists(int InfoId)
		{
			return dal.Exists(InfoId);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FileDocumentlist model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FileDocumentlist model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int InfoId)
		{
			
			dal.Delete(InfoId);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FileDocumentlist GetModel(int InfoId)
		{
			
			return dal.GetModel(InfoId);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FileDocumentlist GetModelByCache(int InfoId)
		{
			
			string CacheKey = "Tb_OAPublicWork_FileDocumentlistModel-" + InfoId;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(InfoId);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FileDocumentlist)objModel;
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
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FileDocumentlist> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FileDocumentlist> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FileDocumentlist> modelList = new List<MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FileDocumentlist>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FileDocumentlist model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FileDocumentlist();
					if(dt.Rows[n]["InfoId"].ToString()!="")
					{
						model.InfoId=int.Parse(dt.Rows[n]["InfoId"].ToString());
					}
					model.WorkFlowTypeName=dt.Rows[n]["WorkFlowTypeName"].ToString();
					model.StartUserName=dt.Rows[n]["StartUserName"].ToString();
					model.FName=dt.Rows[n]["FName"].ToString();
					model.FileCode=dt.Rows[n]["FileCode"].ToString();
					model.SendCompany=dt.Rows[n]["SendCompany"].ToString();
					model.DraftUserName=dt.Rows[n]["DraftUserName"].ToString();
					if(dt.Rows[n]["DraftDate"].ToString()!="")
					{
						model.DraftDate=DateTime.Parse(dt.Rows[n]["DraftDate"].ToString());
					}
					model.ThemeWord=dt.Rows[n]["ThemeWord"].ToString();
					if(dt.Rows[n]["ThemeDate"].ToString()!="")
					{
						model.ThemeDate=DateTime.Parse(dt.Rows[n]["ThemeDate"].ToString());
					}
					model.FileSecretary=dt.Rows[n]["FileSecretary"].ToString();
					model.Urgency=dt.Rows[n]["Urgency"].ToString();
					model.ComeFileType=dt.Rows[n]["ComeFileType"].ToString();
					model.TwoFileType=dt.Rows[n]["TwoFileType"].ToString();
					model.FileTypeName=dt.Rows[n]["FileTypeName"].ToString();
					if(dt.Rows[n]["FilePageSize"].ToString()!="")
					{
						model.FilePageSize=int.Parse(dt.Rows[n]["FilePageSize"].ToString());
					}
					model.ResponsibilityUserName=dt.Rows[n]["ResponsibilityUserName"].ToString();
					model.RetentionDate=dt.Rows[n]["RetentionDate"].ToString();
					model.ReceCompany=dt.Rows[n]["ReceCompany"].ToString();
					model.FileRemark=dt.Rows[n]["FileRemark"].ToString();
					model.FileComRemark=dt.Rows[n]["FileComRemark"].ToString();
					model.FileCheckInfo=dt.Rows[n]["FileCheckInfo"].ToString();
					model.FileAddress=dt.Rows[n]["FileAddress"].ToString();
					model.UserCode=dt.Rows[n]["UserCode"].ToString();
					if(dt.Rows[n]["IsRead"].ToString()!="")
					{
						model.IsRead=int.Parse(dt.Rows[n]["IsRead"].ToString());
					}
					if(dt.Rows[n]["IsDelete"].ToString()!="")
					{
						model.IsDelete=int.Parse(dt.Rows[n]["IsDelete"].ToString());
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

