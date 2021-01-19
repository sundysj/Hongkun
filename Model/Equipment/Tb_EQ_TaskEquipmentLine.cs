using System;
namespace HM.Model.Eq
{
	/// <summary>
	/// 实体类Tb_EQ_TaskEquipmentLine 。(属性说明自动提取数据库字段的描述信息)
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
		/// 设备巡检工艺路线Id
		/// </summary>
		public string TaskLineId
		{
			set{ _tasklineid=value;}
			get{return _tasklineid;}
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
		/// 设备标准主键Id
		/// </summary>
		public string StanId
		{
			set{ _stanid=value;}
			get{return _stanid;}
		}
		/// <summary>
		/// 工艺路线ID
		/// </summary>
		public string DetailId
		{
			set{ _detailid=value;}
			get{return _detailid;}
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
		/// 巡检项目、内容、方法、标准
		/// </summary>
		public string PollingNote
		{
			set{ _pollingnote=value;}
			get{return _pollingnote;}
		}
		/// <summary>
		/// 选项实际值
		/// </summary>
		public string ChooseValue
		{
			set{ _choosevalue=value;}
			get{return _choosevalue;}
		}
		/// <summary>
		/// 数字实际值
		/// </summary>
		public decimal? NumValue
		{
			set{ _numvalue=value;}
			get{return _numvalue;}
		}
		/// <summary>
		/// 文字实际值
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
		/// 参考值
		/// </summary>
		public string ReferenceValue
		{
			set{ _referencevalue=value;}
			get{return _referencevalue;}
		}
		#endregion Model

	}
}

