using System;
namespace HM.Model.Qm
{
	/// <summary>
	/// ʵ����Tb_Qm_ObjectDetail ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		/// ������
		/// </summary>
		public string ObjId
		{
			set{ _objid=value;}
			get{return _objid;}
		}
		/// <summary>
		/// ��׼���
		/// </summary>
		public string StandardId
		{
			set{ _standardid=value;}
			get{return _standardid;}
		}
		/// <summary>
		/// ����
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

