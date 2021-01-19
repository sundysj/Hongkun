using System;
namespace HM.Model.Eq
{
	/// <summary>
	/// ʵ����Tb_EQ_TaskEquipmentLine ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_EQ_TaskEquipmentLine
	{
		public Tb_EQ_TaskEquipmentLine()
		{}
		#region Model
		private string _tasklineid;
		private string _taskid;
		private string _stanid;
		private string _detailid;
		private string _equiid;
		private string _pollingnote;
		private string _choosevalue;
		private decimal? _numvalue;
		private string _textvalue;
		private int? _isdelete;
		private DateTime? _adddate;
		private string _addpid;
		private int? _sort;
		private string _referencevalue;
		/// <summary>
		/// �豸Ѳ�칤��·��Id
		/// </summary>
		public string TaskLineId
		{
			set{ _tasklineid=value;}
			get{return _tasklineid;}
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
		/// �豸��׼����Id
		/// </summary>
		public string StanId
		{
			set{ _stanid=value;}
			get{return _stanid;}
		}
		/// <summary>
		/// ����·��ID
		/// </summary>
		public string DetailId
		{
			set{ _detailid=value;}
			get{return _detailid;}
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
		/// Ѳ����Ŀ�����ݡ���������׼
		/// </summary>
		public string PollingNote
		{
			set{ _pollingnote=value;}
			get{return _pollingnote;}
		}
		/// <summary>
		/// ѡ��ʵ��ֵ
		/// </summary>
		public string ChooseValue
		{
			set{ _choosevalue=value;}
			get{return _choosevalue;}
		}
		/// <summary>
		/// ����ʵ��ֵ
		/// </summary>
		public decimal? NumValue
		{
			set{ _numvalue=value;}
			get{return _numvalue;}
		}
		/// <summary>
		/// ����ʵ��ֵ
		/// </summary>
		public string TextValue
		{
			set{ _textvalue=value;}
			get{return _textvalue;}
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
		/// <summary>
		/// 
		/// </summary>
		public string AddPId
		{
			set{ _addpid=value;}
			get{return _addpid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// �ο�ֵ
		/// </summary>
		public string ReferenceValue
		{
			set{ _referencevalue=value;}
			get{return _referencevalue;}
		}
		#endregion Model

	}
}

