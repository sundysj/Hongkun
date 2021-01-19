using System;
namespace HM.Model.Eq
{
	/// <summary>
	/// ʵ����Tb_EQ_TaskEquipment ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_EQ_TaskEquipment
	{
		public Tb_EQ_TaskEquipment()
		{}
		#region Model
		private string _taskeqid;
		private string _taskid;
		private string _equiid;
		private string _pollingnote;
		private string _pollingresult;
		private string _ismend;
		private string _bsbh;
		private string _ishandle;
		private string _handlepid;
		private int? _isdelete;
		private DateTime? _adddate;
		/// <summary>
		/// Ѳ���豸�б�Id
		/// </summary>
		public string TaskEqId
		{
			set{ _taskeqid=value;}
			get{return _taskeqid;}
		}
		/// <summary>
		/// ��������Id
		/// </summary>
		public string TaskId
		{
			set{ _taskid=value;}
			get{return _taskid;}
		}
		/// <summary>
		/// �豸����������Id
		/// </summary>
		public string EquiId
		{
			set{ _equiid=value;}
			get{return _equiid;}
		}
		/// <summary>
		/// Ѳ����Ŀ�����ݡ���������׼������
		/// </summary>
		public string PollingNote
		{
			set{ _pollingnote=value;}
			get{return _pollingnote;}
		}
		/// <summary>
		/// Ѳ���� ����/������
		/// </summary>
		public string PollingResult
		{
			set{ _pollingresult=value;}
			get{return _pollingresult;}
		}
		/// <summary>
		/// �Ƿ���
		/// </summary>
		public string IsMend
		{
			set{ _ismend=value;}
			get{return _ismend;}
		}
		/// <summary>
		/// ���±��
		/// </summary>
		public string BSBH
		{
			set{ _bsbh=value;}
			get{return _bsbh;}
		}
		/// <summary>
		/// �Ƿ���
		/// </summary>
		public string IsHandle
		{
			set{ _ishandle=value;}
			get{return _ishandle;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string HandlePId
		{
			set{ _handlepid=value;}
			get{return _handlepid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AddDate
		{
			set{ _adddate=value;}
			get{return _adddate;}
		}
		#endregion Model

	}
}

