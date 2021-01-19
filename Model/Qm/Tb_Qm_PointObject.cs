using System;
namespace HM.Model.Qm
{
	/// <summary>
	/// ʵ����Tb_Qm_PointObject ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		/// ����Id
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// ��λid
		/// </summary>
		public string PointId
		{
			set{ _pointid=value;}
			get{return _pointid;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string ObjId
		{
			set{ _objid=value;}
			get{return _objid;}
		}
		/// <summary>
		/// ��ע
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

