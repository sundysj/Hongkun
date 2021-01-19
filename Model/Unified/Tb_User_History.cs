using System;
namespace MobileSoft.Model.Unified
{
	/// <summary>
	/// 实体类Tb_User_History 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_User_History
	{
		public Tb_User_History()
		{}
		#region Model
		private string _id;
		private string _userid;
		private string _logcontent;
		private DateTime _oprdate;
		/// <summary>
		/// 
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 用户ID
		/// </summary>
		public string UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 操作内容
		/// </summary>
		public string LogContent
		{
			set{ _logcontent=value;}
			get{return _logcontent;}
		}
		/// <summary>
		/// 操作时间
		/// </summary>
		public DateTime OprDate
		{
			set{ _oprdate=value;}
			get{return _oprdate;}
		}
		#endregion Model

	}
}

