using System;
namespace HM.Model.Qm
{
	/// <summary>
	/// 实体类Tb_Qm_TaskPoint 。(属性说明自动提取数据库字段的描述信息)
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
		/// 扫描记录Id
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 任务编号
		/// </summary>
		public string TaskId
		{
			set{ _taskid=value;}
			get{return _taskid;}
		}
		/// <summary>
		/// 点位编号
		/// </summary>
		public string PointIds
		{
			set{ _pointids=value;}
			get{return _pointids;}
		}
		/// <summary>
		/// 照片
		/// </summary>
		public string Pictures
		{
			set{ _pictures=value;}
			get{return _pictures;}
		}
		/// <summary>
		/// 添加人员
		/// </summary>
		public string AddPId
		{
			set{ _addpid=value;}
			get{return _addpid;}
		}
		/// <summary>
		/// 添加时间
		/// </summary>
		public DateTime? AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
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

