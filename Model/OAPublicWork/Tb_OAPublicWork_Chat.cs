using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// 实体类Tb_OAPublicWork_Chat 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_Chat
	{
		public Tb_OAPublicWork_Chat()
		{}
		#region Model
		private long _infoid;
		private string _sendman;
		private string _receman;
		private string _content;
		private DateTime? _sendtime;
		private int? _isread;
		/// <summary>
		/// 
		/// </summary>
		public long InfoId
		{
			set{ _infoid=value;}
			get{return _infoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SendMan
		{
			set{ _sendman=value;}
			get{return _sendman;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReceMan
		{
			set{ _receman=value;}
			get{return _receman;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? SendTime
		{
			set{ _sendtime=value;}
			get{return _sendtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsRead
		{
			set{ _isread=value;}
			get{return _isread;}
		}
		#endregion Model

	}
}

