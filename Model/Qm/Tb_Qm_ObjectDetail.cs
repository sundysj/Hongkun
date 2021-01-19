using System;
namespace HM.Model.Qm
{
	/// <summary>
	/// 实体类Tb_Qm_ObjectDetail 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Qm_ObjectDetail
	{
		public Tb_Qm_ObjectDetail()
		{}
		#region Model
		private string _id;
		private string _objid;
		private string _standardid;
		private string _remark;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 对象编号
		/// </summary>
		public string ObjId
		{
			set{ _objid=value;}
			get{return _objid;}
		}
		/// <summary>
		/// 标准编号
		/// </summary>
		public string StandardId
		{
			set{ _standardid=value;}
			get{return _standardid;}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

