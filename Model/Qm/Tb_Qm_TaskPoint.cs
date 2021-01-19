using System;
namespace HM.Model.Qm
{
	/// <summary>
	/// ʵ����Tb_Qm_TaskPoint ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Qm_TaskPoint
	{
		public Tb_Qm_TaskPoint()
		{}
		#region Model
		private string _id;
		private string _taskid;
		private string _pointids;
		private string _pictures;
		private string _addpid;
		private DateTime? _addtime;
		private string _remark;
		private int? _isdelete;
		/// <summary>
		/// ɨ���¼Id
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string TaskId
		{
			set{ _taskid=value;}
			get{return _taskid;}
		}
		/// <summary>
		/// ��λ���
		/// </summary>
		public string PointIds
		{
			set{ _pointids=value;}
			get{return _pointids;}
		}
		/// <summary>
		/// ��Ƭ
		/// </summary>
		public string Pictures
		{
			set{ _pictures=value;}
			get{return _pictures;}
		}
		/// <summary>
		/// �����Ա
		/// </summary>
		public string AddPId
		{
			set{ _addpid=value;}
			get{return _addpid;}
		}
		/// <summary>
		/// ���ʱ��
		/// </summary>
		public DateTime? AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
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

