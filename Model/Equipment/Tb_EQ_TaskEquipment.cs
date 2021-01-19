using System;
namespace HM.Model.Eq
{
	/// <summary>
	/// 实体类Tb_EQ_TaskEquipment 。(属性说明自动提取数据库字段的描述信息)
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
		/// 巡检设备列表Id
		/// </summary>
		public string TaskEqId
		{
			set{ _taskeqid=value;}
			get{return _taskeqid;}
		}
		/// <summary>
		/// 任务生成Id
		/// </summary>
		public string TaskId
		{
			set{ _taskid=value;}
			get{return _taskid;}
		}
		/// <summary>
		/// 设备档案表主键Id
		/// </summary>
		public string EquiId
		{
			set{ _equiid=value;}
			get{return _equiid;}
		}
		/// <summary>
		/// 巡检项目、内容、方法、标准、描述
		/// </summary>
		public string PollingNote
		{
			set{ _pollingnote=value;}
			get{return _pollingnote;}
		}
		/// <summary>
		/// 巡检结果 正常/不正常
		/// </summary>
		public string PollingResult
		{
			set{ _pollingresult=value;}
			get{return _pollingresult;}
		}
		/// <summary>
		/// 是否报修
		/// </summary>
		public string IsMend
		{
			set{ _ismend=value;}
			get{return _ismend;}
		}
		/// <summary>
		/// 报事编号
		/// </summary>
		public string BSBH
		{
			set{ _bsbh=value;}
			get{return _bsbh;}
		}
		/// <summary>
		/// 是否处理
		/// </summary>
		public string IsHandle
		{
			set{ _ishandle=value;}
			get{return _ishandle;}
		}
		/// <summary>
		/// 处理人
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

