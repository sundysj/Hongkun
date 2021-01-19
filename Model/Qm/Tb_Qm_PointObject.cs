using System;
namespace HM.Model.Qm
{
	/// <summary>
	/// 实体类Tb_Qm_PointObject 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Qm_PointObject
	{
		public Tb_Qm_PointObject()
		{}
		#region Model
		private string _id;
		private string _pointid;
		private string _objid;
		private string _remark;
		private int? _isdelete;
		/// <summary>
		/// 主键Id
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 点位id
		/// </summary>
		public string PointId
		{
			set{ _pointid=value;}
			get{return _pointid;}
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
		/// 备注
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

