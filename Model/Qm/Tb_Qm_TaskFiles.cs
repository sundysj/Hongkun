using System;
namespace HM.Model.Qm
{
	/// <summary>
	/// 实体类Tb_Qm_TaskFiles 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Qm_TaskFiles
	{
		public Tb_Qm_TaskFiles()
		{}
		#region Model
		private string _id;
		private string _taskid;
		private string _filename;
		private string _fix;
		private int? _isdelete;
		private string _filepath;
		private string _phonename;
		private DateTime? _phototime;
		private string _photopid;
		/// <summary>
		/// 附件表Id
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 任务表ID
		/// </summary>
		public string TaskId
		{
			set{ _taskid=value;}
			get{return _taskid;}
		}
		/// <summary>
		/// 文件名字
		/// </summary>
		public string FileName
		{
			set{ _filename=value;}
			get{return _filename;}
		}
		/// <summary>
		/// 后缀名
		/// </summary>
		public string Fix
		{
			set{ _fix=value;}
			get{return _fix;}
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
		/// 文件路径
		/// </summary>
		public string FilePath
		{
			set{ _filepath=value;}
			get{return _filepath;}
		}
		/// <summary>
		/// 机器名
		/// </summary>
		public string PhoneName
		{
			set{ _phonename=value;}
			get{return _phonename;}
		}
		/// <summary>
		/// 拍摄时间
		/// </summary>
		public DateTime? PhotoTime
		{
			set{ _phototime=value;}
			get{return _phototime;}
		}
		/// <summary>
		/// 登录帐号
		/// </summary>
		public string PhotoPId
		{
			set{ _photopid=value;}
			get{return _photopid;}
		}
		#endregion Model

	}
}

