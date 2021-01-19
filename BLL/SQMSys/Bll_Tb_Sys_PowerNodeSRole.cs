using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using MobileSoft.Model.SQMSys;
namespace MobileSoft.BLL.SQMSys
{
	/// <summary>
	/// 业务逻辑类Bll_Tb_Sys_PowerNodeSRole 的摘要说明。
	/// </summary>
	public class Bll_Tb_Sys_PowerNodeSRole
	{
		private readonly MobileSoft.DAL.SQMSys.Dal_Tb_Sys_PowerNodeSRole dal=new MobileSoft.DAL.SQMSys.Dal_Tb_Sys_PowerNodeSRole();
		public Bll_Tb_Sys_PowerNodeSRole()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long IID)
		{
			return dal.Exists(IID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeSRole model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeSRole model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long IID)
		{
			
			dal.Delete(IID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeSRole GetModel(long IID)
		{
			
			return dal.GetModel(IID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeSRole GetModelByCache(long IID)
		{
			
			string CacheKey = "Tb_Sys_PowerNodeSRoleModel-" + IID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(IID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeSRole)objModel;
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
		public List<MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeSRole> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeSRole> DataTableToList(DataTable dt)
		{
			List<MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeSRole> modelList = new List<MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeSRole>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeSRole model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeSRole();
					//model.IID=dt.Rows[n]["IID"].ToString();
					model.UserCode=dt.Rows[n]["UserCode"].ToString();
					model.OrganCode=dt.Rows[n]["OrganCode"].ToString();
					model.OrganName=dt.Rows[n]["OrganName"].ToString();
					if(dt.Rows[n]["IsComp"].ToString()!="")
					{
						model.IsComp=int.Parse(dt.Rows[n]["IsComp"].ToString());
					}
					//model.CommID=dt.Rows[n]["CommID"].ToString();
					if(dt.Rows[n]["CommCode"].ToString()!="")
					{
						model.CommCode=new Guid(dt.Rows[n]["CommCode"].ToString());
					}
					if(dt.Rows[n]["CkAll"].ToString()!="")
					{
						model.CkAll=int.Parse(dt.Rows[n]["CkAll"].ToString());
					}
					if(dt.Rows[n]["Ck1"].ToString()!="")
					{
						model.Ck1=int.Parse(dt.Rows[n]["Ck1"].ToString());
					}
					if(dt.Rows[n]["Ck2"].ToString()!="")
					{
						model.Ck2=int.Parse(dt.Rows[n]["Ck2"].ToString());
					}
					if(dt.Rows[n]["Ck3"].ToString()!="")
					{
						model.Ck3=int.Parse(dt.Rows[n]["Ck3"].ToString());
					}
					if(dt.Rows[n]["Ck4"].ToString()!="")
					{
						model.Ck4=int.Parse(dt.Rows[n]["Ck4"].ToString());
					}
					if(dt.Rows[n]["Ck5"].ToString()!="")
					{
						model.Ck5=int.Parse(dt.Rows[n]["Ck5"].ToString());
					}
					if(dt.Rows[n]["Ck6"].ToString()!="")
					{
						model.Ck6=int.Parse(dt.Rows[n]["Ck6"].ToString());
					}
					if(dt.Rows[n]["Ck7"].ToString()!="")
					{
						model.Ck7=int.Parse(dt.Rows[n]["Ck7"].ToString());
					}
					if(dt.Rows[n]["Ck8"].ToString()!="")
					{
						model.Ck8=int.Parse(dt.Rows[n]["Ck8"].ToString());
					}
					if(dt.Rows[n]["Ck9"].ToString()!="")
					{
						model.Ck9=int.Parse(dt.Rows[n]["Ck9"].ToString());
					}
					if(dt.Rows[n]["Ck10"].ToString()!="")
					{
						model.Ck10=int.Parse(dt.Rows[n]["Ck10"].ToString());
					}
					if(dt.Rows[n]["Ck11"].ToString()!="")
					{
						model.Ck11=int.Parse(dt.Rows[n]["Ck11"].ToString());
					}
					if(dt.Rows[n]["Ck12"].ToString()!="")
					{
						model.Ck12=int.Parse(dt.Rows[n]["Ck12"].ToString());
					}
					if(dt.Rows[n]["Ck13"].ToString()!="")
					{
						model.Ck13=int.Parse(dt.Rows[n]["Ck13"].ToString());
					}
					if(dt.Rows[n]["Ck14"].ToString()!="")
					{
						model.Ck14=int.Parse(dt.Rows[n]["Ck14"].ToString());
					}
					if(dt.Rows[n]["Ck15"].ToString()!="")
					{
						model.Ck15=int.Parse(dt.Rows[n]["Ck15"].ToString());
					}
					if(dt.Rows[n]["Ck16"].ToString()!="")
					{
						model.Ck16=int.Parse(dt.Rows[n]["Ck16"].ToString());
					}
					if(dt.Rows[n]["Ck17"].ToString()!="")
					{
						model.Ck17=int.Parse(dt.Rows[n]["Ck17"].ToString());
					}
					if(dt.Rows[n]["Ck18"].ToString()!="")
					{
						model.Ck18=int.Parse(dt.Rows[n]["Ck18"].ToString());
					}
					if(dt.Rows[n]["Ck19"].ToString()!="")
					{
						model.Ck19=int.Parse(dt.Rows[n]["Ck19"].ToString());
					}
					if(dt.Rows[n]["Ck20"].ToString()!="")
					{
						model.Ck20=int.Parse(dt.Rows[n]["Ck20"].ToString());
					}
					if(dt.Rows[n]["Ck21"].ToString()!="")
					{
						model.Ck21=int.Parse(dt.Rows[n]["Ck21"].ToString());
					}
					if(dt.Rows[n]["Ck22"].ToString()!="")
					{
						model.Ck22=int.Parse(dt.Rows[n]["Ck22"].ToString());
					}
					if(dt.Rows[n]["Ck23"].ToString()!="")
					{
						model.Ck23=int.Parse(dt.Rows[n]["Ck23"].ToString());
					}
					if(dt.Rows[n]["Ck24"].ToString()!="")
					{
						model.Ck24=int.Parse(dt.Rows[n]["Ck24"].ToString());
					}
					if(dt.Rows[n]["Ck25"].ToString()!="")
					{
						model.Ck25=int.Parse(dt.Rows[n]["Ck25"].ToString());
					}
					if(dt.Rows[n]["Ck26"].ToString()!="")
					{
						model.Ck26=int.Parse(dt.Rows[n]["Ck26"].ToString());
					}
					if(dt.Rows[n]["Ck27"].ToString()!="")
					{
						model.Ck27=int.Parse(dt.Rows[n]["Ck27"].ToString());
					}
					if(dt.Rows[n]["Ck28"].ToString()!="")
					{
						model.Ck28=int.Parse(dt.Rows[n]["Ck28"].ToString());
					}
					if(dt.Rows[n]["Ck29"].ToString()!="")
					{
						model.Ck29=int.Parse(dt.Rows[n]["Ck29"].ToString());
					}
					if(dt.Rows[n]["Ck30"].ToString()!="")
					{
						model.Ck30=int.Parse(dt.Rows[n]["Ck30"].ToString());
					}
					if(dt.Rows[n]["Ck31"].ToString()!="")
					{
						model.Ck31=int.Parse(dt.Rows[n]["Ck31"].ToString());
					}
					if(dt.Rows[n]["Ck32"].ToString()!="")
					{
						model.Ck32=int.Parse(dt.Rows[n]["Ck32"].ToString());
					}
					if(dt.Rows[n]["Ck33"].ToString()!="")
					{
						model.Ck33=int.Parse(dt.Rows[n]["Ck33"].ToString());
					}
					if(dt.Rows[n]["Ck34"].ToString()!="")
					{
						model.Ck34=int.Parse(dt.Rows[n]["Ck34"].ToString());
					}
					if(dt.Rows[n]["Ck35"].ToString()!="")
					{
						model.Ck35=int.Parse(dt.Rows[n]["Ck35"].ToString());
					}
					if(dt.Rows[n]["Ck36"].ToString()!="")
					{
						model.Ck36=int.Parse(dt.Rows[n]["Ck36"].ToString());
					}
					if(dt.Rows[n]["Ck37"].ToString()!="")
					{
						model.Ck37=int.Parse(dt.Rows[n]["Ck37"].ToString());
					}
					if(dt.Rows[n]["Ck38"].ToString()!="")
					{
						model.Ck38=int.Parse(dt.Rows[n]["Ck38"].ToString());
					}
					if(dt.Rows[n]["Ck39"].ToString()!="")
					{
						model.Ck39=int.Parse(dt.Rows[n]["Ck39"].ToString());
					}
					if(dt.Rows[n]["Ck40"].ToString()!="")
					{
						model.Ck40=int.Parse(dt.Rows[n]["Ck40"].ToString());
					}
					if(dt.Rows[n]["Ck41"].ToString()!="")
					{
						model.Ck41=int.Parse(dt.Rows[n]["Ck41"].ToString());
					}
					if(dt.Rows[n]["Ck42"].ToString()!="")
					{
						model.Ck42=int.Parse(dt.Rows[n]["Ck42"].ToString());
					}
					if(dt.Rows[n]["Ck43"].ToString()!="")
					{
						model.Ck43=int.Parse(dt.Rows[n]["Ck43"].ToString());
					}
					if(dt.Rows[n]["Ck44"].ToString()!="")
					{
						model.Ck44=int.Parse(dt.Rows[n]["Ck44"].ToString());
					}
					if(dt.Rows[n]["Ck45"].ToString()!="")
					{
						model.Ck45=int.Parse(dt.Rows[n]["Ck45"].ToString());
					}
					if(dt.Rows[n]["Ck46"].ToString()!="")
					{
						model.Ck46=int.Parse(dt.Rows[n]["Ck46"].ToString());
					}
					if(dt.Rows[n]["Ck47"].ToString()!="")
					{
						model.Ck47=int.Parse(dt.Rows[n]["Ck47"].ToString());
					}
					if(dt.Rows[n]["Ck48"].ToString()!="")
					{
						model.Ck48=int.Parse(dt.Rows[n]["Ck48"].ToString());
					}
					if(dt.Rows[n]["Ck49"].ToString()!="")
					{
						model.Ck49=int.Parse(dt.Rows[n]["Ck49"].ToString());
					}
					if(dt.Rows[n]["Ck50"].ToString()!="")
					{
						model.Ck50=int.Parse(dt.Rows[n]["Ck50"].ToString());
					}
					if(dt.Rows[n]["Ck51"].ToString()!="")
					{
						model.Ck51=int.Parse(dt.Rows[n]["Ck51"].ToString());
					}
					if(dt.Rows[n]["Ck52"].ToString()!="")
					{
						model.Ck52=int.Parse(dt.Rows[n]["Ck52"].ToString());
					}
					if(dt.Rows[n]["Ck53"].ToString()!="")
					{
						model.Ck53=int.Parse(dt.Rows[n]["Ck53"].ToString());
					}
					if(dt.Rows[n]["Ck54"].ToString()!="")
					{
						model.Ck54=int.Parse(dt.Rows[n]["Ck54"].ToString());
					}
					if(dt.Rows[n]["Ck55"].ToString()!="")
					{
						model.Ck55=int.Parse(dt.Rows[n]["Ck55"].ToString());
					}
					if(dt.Rows[n]["Ck56"].ToString()!="")
					{
						model.Ck56=int.Parse(dt.Rows[n]["Ck56"].ToString());
					}
					if(dt.Rows[n]["Ck57"].ToString()!="")
					{
						model.Ck57=int.Parse(dt.Rows[n]["Ck57"].ToString());
					}
					if(dt.Rows[n]["Ck58"].ToString()!="")
					{
						model.Ck58=int.Parse(dt.Rows[n]["Ck58"].ToString());
					}
					if(dt.Rows[n]["Ck59"].ToString()!="")
					{
						model.Ck59=int.Parse(dt.Rows[n]["Ck59"].ToString());
					}
					if(dt.Rows[n]["Ck60"].ToString()!="")
					{
						model.Ck60=int.Parse(dt.Rows[n]["Ck60"].ToString());
					}
					if(dt.Rows[n]["Ck61"].ToString()!="")
					{
						model.Ck61=int.Parse(dt.Rows[n]["Ck61"].ToString());
					}
					if(dt.Rows[n]["Ck62"].ToString()!="")
					{
						model.Ck62=int.Parse(dt.Rows[n]["Ck62"].ToString());
					}
					if(dt.Rows[n]["Ck63"].ToString()!="")
					{
						model.Ck63=int.Parse(dt.Rows[n]["Ck63"].ToString());
					}
					if(dt.Rows[n]["Ck64"].ToString()!="")
					{
						model.Ck64=int.Parse(dt.Rows[n]["Ck64"].ToString());
					}
					if(dt.Rows[n]["Ck65"].ToString()!="")
					{
						model.Ck65=int.Parse(dt.Rows[n]["Ck65"].ToString());
					}
					if(dt.Rows[n]["Ck66"].ToString()!="")
					{
						model.Ck66=int.Parse(dt.Rows[n]["Ck66"].ToString());
					}
					if(dt.Rows[n]["Ck67"].ToString()!="")
					{
						model.Ck67=int.Parse(dt.Rows[n]["Ck67"].ToString());
					}
					if(dt.Rows[n]["Ck68"].ToString()!="")
					{
						model.Ck68=int.Parse(dt.Rows[n]["Ck68"].ToString());
					}
					if(dt.Rows[n]["Ck69"].ToString()!="")
					{
						model.Ck69=int.Parse(dt.Rows[n]["Ck69"].ToString());
					}
					if(dt.Rows[n]["Ck70"].ToString()!="")
					{
						model.Ck70=int.Parse(dt.Rows[n]["Ck70"].ToString());
					}
					if(dt.Rows[n]["Ck71"].ToString()!="")
					{
						model.Ck71=int.Parse(dt.Rows[n]["Ck71"].ToString());
					}
					if(dt.Rows[n]["Ck72"].ToString()!="")
					{
						model.Ck72=int.Parse(dt.Rows[n]["Ck72"].ToString());
					}
					if(dt.Rows[n]["Ck73"].ToString()!="")
					{
						model.Ck73=int.Parse(dt.Rows[n]["Ck73"].ToString());
					}
					if(dt.Rows[n]["Ck74"].ToString()!="")
					{
						model.Ck74=int.Parse(dt.Rows[n]["Ck74"].ToString());
					}
					if(dt.Rows[n]["Ck75"].ToString()!="")
					{
						model.Ck75=int.Parse(dt.Rows[n]["Ck75"].ToString());
					}
					if(dt.Rows[n]["Ck76"].ToString()!="")
					{
						model.Ck76=int.Parse(dt.Rows[n]["Ck76"].ToString());
					}
					if(dt.Rows[n]["Ck77"].ToString()!="")
					{
						model.Ck77=int.Parse(dt.Rows[n]["Ck77"].ToString());
					}
					if(dt.Rows[n]["Ck78"].ToString()!="")
					{
						model.Ck78=int.Parse(dt.Rows[n]["Ck78"].ToString());
					}
					if(dt.Rows[n]["Ck79"].ToString()!="")
					{
						model.Ck79=int.Parse(dt.Rows[n]["Ck79"].ToString());
					}
					if(dt.Rows[n]["Ck80"].ToString()!="")
					{
						model.Ck80=int.Parse(dt.Rows[n]["Ck80"].ToString());
					}
					if(dt.Rows[n]["Ck81"].ToString()!="")
					{
						model.Ck81=int.Parse(dt.Rows[n]["Ck81"].ToString());
					}
					if(dt.Rows[n]["Ck82"].ToString()!="")
					{
						model.Ck82=int.Parse(dt.Rows[n]["Ck82"].ToString());
					}
					if(dt.Rows[n]["Ck83"].ToString()!="")
					{
						model.Ck83=int.Parse(dt.Rows[n]["Ck83"].ToString());
					}
					if(dt.Rows[n]["Ck84"].ToString()!="")
					{
						model.Ck84=int.Parse(dt.Rows[n]["Ck84"].ToString());
					}
					if(dt.Rows[n]["Ck85"].ToString()!="")
					{
						model.Ck85=int.Parse(dt.Rows[n]["Ck85"].ToString());
					}
					if(dt.Rows[n]["Ck86"].ToString()!="")
					{
						model.Ck86=int.Parse(dt.Rows[n]["Ck86"].ToString());
					}
					if(dt.Rows[n]["Ck87"].ToString()!="")
					{
						model.Ck87=int.Parse(dt.Rows[n]["Ck87"].ToString());
					}
					if(dt.Rows[n]["Ck88"].ToString()!="")
					{
						model.Ck88=int.Parse(dt.Rows[n]["Ck88"].ToString());
					}
					if(dt.Rows[n]["Ck89"].ToString()!="")
					{
						model.Ck89=int.Parse(dt.Rows[n]["Ck89"].ToString());
					}
					if(dt.Rows[n]["Ck90"].ToString()!="")
					{
						model.Ck90=int.Parse(dt.Rows[n]["Ck90"].ToString());
					}
					if(dt.Rows[n]["Ck91"].ToString()!="")
					{
						model.Ck91=int.Parse(dt.Rows[n]["Ck91"].ToString());
					}
					if(dt.Rows[n]["Ck92"].ToString()!="")
					{
						model.Ck92=int.Parse(dt.Rows[n]["Ck92"].ToString());
					}
					if(dt.Rows[n]["Ck93"].ToString()!="")
					{
						model.Ck93=int.Parse(dt.Rows[n]["Ck93"].ToString());
					}
					if(dt.Rows[n]["Ck94"].ToString()!="")
					{
						model.Ck94=int.Parse(dt.Rows[n]["Ck94"].ToString());
					}
					if(dt.Rows[n]["Ck95"].ToString()!="")
					{
						model.Ck95=int.Parse(dt.Rows[n]["Ck95"].ToString());
					}
					if(dt.Rows[n]["Ck96"].ToString()!="")
					{
						model.Ck96=int.Parse(dt.Rows[n]["Ck96"].ToString());
					}
					if(dt.Rows[n]["Ck97"].ToString()!="")
					{
						model.Ck97=int.Parse(dt.Rows[n]["Ck97"].ToString());
					}
					if(dt.Rows[n]["Ck98"].ToString()!="")
					{
						model.Ck98=int.Parse(dt.Rows[n]["Ck98"].ToString());
					}
					if(dt.Rows[n]["Ck99"].ToString()!="")
					{
						model.Ck99=int.Parse(dt.Rows[n]["Ck99"].ToString());
					}
					if(dt.Rows[n]["Ck100"].ToString()!="")
					{
						model.Ck100=int.Parse(dt.Rows[n]["Ck100"].ToString());
					}
					if(dt.Rows[n]["Ck101"].ToString()!="")
					{
						model.Ck101=int.Parse(dt.Rows[n]["Ck101"].ToString());
					}
					if(dt.Rows[n]["Ck102"].ToString()!="")
					{
						model.Ck102=int.Parse(dt.Rows[n]["Ck102"].ToString());
					}
					if(dt.Rows[n]["Ck103"].ToString()!="")
					{
						model.Ck103=int.Parse(dt.Rows[n]["Ck103"].ToString());
					}
					if(dt.Rows[n]["Ck104"].ToString()!="")
					{
						model.Ck104=int.Parse(dt.Rows[n]["Ck104"].ToString());
					}
					if(dt.Rows[n]["Ck105"].ToString()!="")
					{
						model.Ck105=int.Parse(dt.Rows[n]["Ck105"].ToString());
					}
					if(dt.Rows[n]["Ck106"].ToString()!="")
					{
						model.Ck106=int.Parse(dt.Rows[n]["Ck106"].ToString());
					}
					if(dt.Rows[n]["Ck107"].ToString()!="")
					{
						model.Ck107=int.Parse(dt.Rows[n]["Ck107"].ToString());
					}
					if(dt.Rows[n]["Ck108"].ToString()!="")
					{
						model.Ck108=int.Parse(dt.Rows[n]["Ck108"].ToString());
					}
					if(dt.Rows[n]["Ck109"].ToString()!="")
					{
						model.Ck109=int.Parse(dt.Rows[n]["Ck109"].ToString());
					}
					if(dt.Rows[n]["Ck110"].ToString()!="")
					{
						model.Ck110=int.Parse(dt.Rows[n]["Ck110"].ToString());
					}
					if(dt.Rows[n]["Ck111"].ToString()!="")
					{
						model.Ck111=int.Parse(dt.Rows[n]["Ck111"].ToString());
					}
					if(dt.Rows[n]["Ck112"].ToString()!="")
					{
						model.Ck112=int.Parse(dt.Rows[n]["Ck112"].ToString());
					}
					if(dt.Rows[n]["Ck113"].ToString()!="")
					{
						model.Ck113=int.Parse(dt.Rows[n]["Ck113"].ToString());
					}
					if(dt.Rows[n]["Ck114"].ToString()!="")
					{
						model.Ck114=int.Parse(dt.Rows[n]["Ck114"].ToString());
					}
					if(dt.Rows[n]["Ck115"].ToString()!="")
					{
						model.Ck115=int.Parse(dt.Rows[n]["Ck115"].ToString());
					}
					if(dt.Rows[n]["Ck116"].ToString()!="")
					{
						model.Ck116=int.Parse(dt.Rows[n]["Ck116"].ToString());
					}
					if(dt.Rows[n]["Ck117"].ToString()!="")
					{
						model.Ck117=int.Parse(dt.Rows[n]["Ck117"].ToString());
					}
					if(dt.Rows[n]["Ck118"].ToString()!="")
					{
						model.Ck118=int.Parse(dt.Rows[n]["Ck118"].ToString());
					}
					if(dt.Rows[n]["Ck119"].ToString()!="")
					{
						model.Ck119=int.Parse(dt.Rows[n]["Ck119"].ToString());
					}
					if(dt.Rows[n]["Ck120"].ToString()!="")
					{
						model.Ck120=int.Parse(dt.Rows[n]["Ck120"].ToString());
					}
					if(dt.Rows[n]["Ck121"].ToString()!="")
					{
						model.Ck121=int.Parse(dt.Rows[n]["Ck121"].ToString());
					}
					if(dt.Rows[n]["Ck122"].ToString()!="")
					{
						model.Ck122=int.Parse(dt.Rows[n]["Ck122"].ToString());
					}
					if(dt.Rows[n]["Ck123"].ToString()!="")
					{
						model.Ck123=int.Parse(dt.Rows[n]["Ck123"].ToString());
					}
					if(dt.Rows[n]["Ck124"].ToString()!="")
					{
						model.Ck124=int.Parse(dt.Rows[n]["Ck124"].ToString());
					}
					if(dt.Rows[n]["Ck125"].ToString()!="")
					{
						model.Ck125=int.Parse(dt.Rows[n]["Ck125"].ToString());
					}
					if(dt.Rows[n]["Ck126"].ToString()!="")
					{
						model.Ck126=int.Parse(dt.Rows[n]["Ck126"].ToString());
					}
					if(dt.Rows[n]["Ck127"].ToString()!="")
					{
						model.Ck127=int.Parse(dt.Rows[n]["Ck127"].ToString());
					}
					if(dt.Rows[n]["Ck128"].ToString()!="")
					{
						model.Ck128=int.Parse(dt.Rows[n]["Ck128"].ToString());
					}
					if(dt.Rows[n]["Ck129"].ToString()!="")
					{
						model.Ck129=int.Parse(dt.Rows[n]["Ck129"].ToString());
					}
					if(dt.Rows[n]["Ck130"].ToString()!="")
					{
						model.Ck130=int.Parse(dt.Rows[n]["Ck130"].ToString());
					}
					if(dt.Rows[n]["Ck131"].ToString()!="")
					{
						model.Ck131=int.Parse(dt.Rows[n]["Ck131"].ToString());
					}
					if(dt.Rows[n]["Ck132"].ToString()!="")
					{
						model.Ck132=int.Parse(dt.Rows[n]["Ck132"].ToString());
					}
					if(dt.Rows[n]["Ck133"].ToString()!="")
					{
						model.Ck133=int.Parse(dt.Rows[n]["Ck133"].ToString());
					}
					if(dt.Rows[n]["Ck134"].ToString()!="")
					{
						model.Ck134=int.Parse(dt.Rows[n]["Ck134"].ToString());
					}
					if(dt.Rows[n]["Ck135"].ToString()!="")
					{
						model.Ck135=int.Parse(dt.Rows[n]["Ck135"].ToString());
					}
					if(dt.Rows[n]["Ck136"].ToString()!="")
					{
						model.Ck136=int.Parse(dt.Rows[n]["Ck136"].ToString());
					}
					if(dt.Rows[n]["Ck137"].ToString()!="")
					{
						model.Ck137=int.Parse(dt.Rows[n]["Ck137"].ToString());
					}
					if(dt.Rows[n]["Ck138"].ToString()!="")
					{
						model.Ck138=int.Parse(dt.Rows[n]["Ck138"].ToString());
					}
					if(dt.Rows[n]["Ck139"].ToString()!="")
					{
						model.Ck139=int.Parse(dt.Rows[n]["Ck139"].ToString());
					}
					if(dt.Rows[n]["Ck140"].ToString()!="")
					{
						model.Ck140=int.Parse(dt.Rows[n]["Ck140"].ToString());
					}
					if(dt.Rows[n]["Ck141"].ToString()!="")
					{
						model.Ck141=int.Parse(dt.Rows[n]["Ck141"].ToString());
					}
					if(dt.Rows[n]["Ck142"].ToString()!="")
					{
						model.Ck142=int.Parse(dt.Rows[n]["Ck142"].ToString());
					}
					if(dt.Rows[n]["Ck143"].ToString()!="")
					{
						model.Ck143=int.Parse(dt.Rows[n]["Ck143"].ToString());
					}
					if(dt.Rows[n]["Ck144"].ToString()!="")
					{
						model.Ck144=int.Parse(dt.Rows[n]["Ck144"].ToString());
					}
					if(dt.Rows[n]["Ck145"].ToString()!="")
					{
						model.Ck145=int.Parse(dt.Rows[n]["Ck145"].ToString());
					}
					if(dt.Rows[n]["Ck146"].ToString()!="")
					{
						model.Ck146=int.Parse(dt.Rows[n]["Ck146"].ToString());
					}
					if(dt.Rows[n]["Ck147"].ToString()!="")
					{
						model.Ck147=int.Parse(dt.Rows[n]["Ck147"].ToString());
					}
					if(dt.Rows[n]["Ck148"].ToString()!="")
					{
						model.Ck148=int.Parse(dt.Rows[n]["Ck148"].ToString());
					}
					if(dt.Rows[n]["Ck149"].ToString()!="")
					{
						model.Ck149=int.Parse(dt.Rows[n]["Ck149"].ToString());
					}
					if(dt.Rows[n]["Ck150"].ToString()!="")
					{
						model.Ck150=int.Parse(dt.Rows[n]["Ck150"].ToString());
					}
					if(dt.Rows[n]["Ck151"].ToString()!="")
					{
						model.Ck151=int.Parse(dt.Rows[n]["Ck151"].ToString());
					}
					if(dt.Rows[n]["Ck152"].ToString()!="")
					{
						model.Ck152=int.Parse(dt.Rows[n]["Ck152"].ToString());
					}
					if(dt.Rows[n]["Ck153"].ToString()!="")
					{
						model.Ck153=int.Parse(dt.Rows[n]["Ck153"].ToString());
					}
					if(dt.Rows[n]["Ck154"].ToString()!="")
					{
						model.Ck154=int.Parse(dt.Rows[n]["Ck154"].ToString());
					}
					if(dt.Rows[n]["Ck155"].ToString()!="")
					{
						model.Ck155=int.Parse(dt.Rows[n]["Ck155"].ToString());
					}
					if(dt.Rows[n]["Ck156"].ToString()!="")
					{
						model.Ck156=int.Parse(dt.Rows[n]["Ck156"].ToString());
					}
					if(dt.Rows[n]["Ck157"].ToString()!="")
					{
						model.Ck157=int.Parse(dt.Rows[n]["Ck157"].ToString());
					}
					if(dt.Rows[n]["Ck158"].ToString()!="")
					{
						model.Ck158=int.Parse(dt.Rows[n]["Ck158"].ToString());
					}
					if(dt.Rows[n]["Ck159"].ToString()!="")
					{
						model.Ck159=int.Parse(dt.Rows[n]["Ck159"].ToString());
					}
					if(dt.Rows[n]["Ck160"].ToString()!="")
					{
						model.Ck160=int.Parse(dt.Rows[n]["Ck160"].ToString());
					}
					if(dt.Rows[n]["Ck161"].ToString()!="")
					{
						model.Ck161=int.Parse(dt.Rows[n]["Ck161"].ToString());
					}
					if(dt.Rows[n]["Ck162"].ToString()!="")
					{
						model.Ck162=int.Parse(dt.Rows[n]["Ck162"].ToString());
					}
					if(dt.Rows[n]["Ck163"].ToString()!="")
					{
						model.Ck163=int.Parse(dt.Rows[n]["Ck163"].ToString());
					}
					if(dt.Rows[n]["Ck164"].ToString()!="")
					{
						model.Ck164=int.Parse(dt.Rows[n]["Ck164"].ToString());
					}
					if(dt.Rows[n]["Ck165"].ToString()!="")
					{
						model.Ck165=int.Parse(dt.Rows[n]["Ck165"].ToString());
					}
					if(dt.Rows[n]["Ck166"].ToString()!="")
					{
						model.Ck166=int.Parse(dt.Rows[n]["Ck166"].ToString());
					}
					if(dt.Rows[n]["Ck167"].ToString()!="")
					{
						model.Ck167=int.Parse(dt.Rows[n]["Ck167"].ToString());
					}
					if(dt.Rows[n]["Ck168"].ToString()!="")
					{
						model.Ck168=int.Parse(dt.Rows[n]["Ck168"].ToString());
					}
					if(dt.Rows[n]["Ck169"].ToString()!="")
					{
						model.Ck169=int.Parse(dt.Rows[n]["Ck169"].ToString());
					}
					if(dt.Rows[n]["Ck170"].ToString()!="")
					{
						model.Ck170=int.Parse(dt.Rows[n]["Ck170"].ToString());
					}
					if(dt.Rows[n]["Ck171"].ToString()!="")
					{
						model.Ck171=int.Parse(dt.Rows[n]["Ck171"].ToString());
					}
					if(dt.Rows[n]["Ck172"].ToString()!="")
					{
						model.Ck172=int.Parse(dt.Rows[n]["Ck172"].ToString());
					}
					if(dt.Rows[n]["Ck173"].ToString()!="")
					{
						model.Ck173=int.Parse(dt.Rows[n]["Ck173"].ToString());
					}
					if(dt.Rows[n]["Ck174"].ToString()!="")
					{
						model.Ck174=int.Parse(dt.Rows[n]["Ck174"].ToString());
					}
					if(dt.Rows[n]["Ck175"].ToString()!="")
					{
						model.Ck175=int.Parse(dt.Rows[n]["Ck175"].ToString());
					}
					if(dt.Rows[n]["Ck176"].ToString()!="")
					{
						model.Ck176=int.Parse(dt.Rows[n]["Ck176"].ToString());
					}
					if(dt.Rows[n]["Ck177"].ToString()!="")
					{
						model.Ck177=int.Parse(dt.Rows[n]["Ck177"].ToString());
					}
					if(dt.Rows[n]["Ck178"].ToString()!="")
					{
						model.Ck178=int.Parse(dt.Rows[n]["Ck178"].ToString());
					}
					if(dt.Rows[n]["Ck179"].ToString()!="")
					{
						model.Ck179=int.Parse(dt.Rows[n]["Ck179"].ToString());
					}
					if(dt.Rows[n]["Ck180"].ToString()!="")
					{
						model.Ck180=int.Parse(dt.Rows[n]["Ck180"].ToString());
					}
					if(dt.Rows[n]["Ck181"].ToString()!="")
					{
						model.Ck181=int.Parse(dt.Rows[n]["Ck181"].ToString());
					}
					if(dt.Rows[n]["Ck182"].ToString()!="")
					{
						model.Ck182=int.Parse(dt.Rows[n]["Ck182"].ToString());
					}
					if(dt.Rows[n]["Ck183"].ToString()!="")
					{
						model.Ck183=int.Parse(dt.Rows[n]["Ck183"].ToString());
					}
					if(dt.Rows[n]["Ck184"].ToString()!="")
					{
						model.Ck184=int.Parse(dt.Rows[n]["Ck184"].ToString());
					}
					if(dt.Rows[n]["Ck185"].ToString()!="")
					{
						model.Ck185=int.Parse(dt.Rows[n]["Ck185"].ToString());
					}
					if(dt.Rows[n]["Ck186"].ToString()!="")
					{
						model.Ck186=int.Parse(dt.Rows[n]["Ck186"].ToString());
					}
					if(dt.Rows[n]["Ck187"].ToString()!="")
					{
						model.Ck187=int.Parse(dt.Rows[n]["Ck187"].ToString());
					}
					if(dt.Rows[n]["Ck188"].ToString()!="")
					{
						model.Ck188=int.Parse(dt.Rows[n]["Ck188"].ToString());
					}
					if(dt.Rows[n]["Ck189"].ToString()!="")
					{
						model.Ck189=int.Parse(dt.Rows[n]["Ck189"].ToString());
					}
					if(dt.Rows[n]["Ck190"].ToString()!="")
					{
						model.Ck190=int.Parse(dt.Rows[n]["Ck190"].ToString());
					}
					if(dt.Rows[n]["Ck191"].ToString()!="")
					{
						model.Ck191=int.Parse(dt.Rows[n]["Ck191"].ToString());
					}
					if(dt.Rows[n]["Ck192"].ToString()!="")
					{
						model.Ck192=int.Parse(dt.Rows[n]["Ck192"].ToString());
					}
					if(dt.Rows[n]["Ck193"].ToString()!="")
					{
						model.Ck193=int.Parse(dt.Rows[n]["Ck193"].ToString());
					}
					if(dt.Rows[n]["Ck194"].ToString()!="")
					{
						model.Ck194=int.Parse(dt.Rows[n]["Ck194"].ToString());
					}
					if(dt.Rows[n]["Ck195"].ToString()!="")
					{
						model.Ck195=int.Parse(dt.Rows[n]["Ck195"].ToString());
					}
					if(dt.Rows[n]["Ck196"].ToString()!="")
					{
						model.Ck196=int.Parse(dt.Rows[n]["Ck196"].ToString());
					}
					if(dt.Rows[n]["Ck197"].ToString()!="")
					{
						model.Ck197=int.Parse(dt.Rows[n]["Ck197"].ToString());
					}
					if(dt.Rows[n]["Ck198"].ToString()!="")
					{
						model.Ck198=int.Parse(dt.Rows[n]["Ck198"].ToString());
					}
					if(dt.Rows[n]["Ck199"].ToString()!="")
					{
						model.Ck199=int.Parse(dt.Rows[n]["Ck199"].ToString());
					}
					if(dt.Rows[n]["Ck200"].ToString()!="")
					{
						model.Ck200=int.Parse(dt.Rows[n]["Ck200"].ToString());
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

		#endregion  成员方法
	}
}

