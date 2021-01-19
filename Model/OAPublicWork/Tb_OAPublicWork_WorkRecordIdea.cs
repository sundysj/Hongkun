using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// 实体类Tb_OAPublicWork_WorkRecordIdea 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_WorkRecordIdea
	{
		public Tb_OAPublicWork_WorkRecordIdea()
		{}
		#region Model
		private int _infoid;
		private int? _workrecord_infoid;
		private string _usercode;
		private string _ideacontent;
		private DateTime? _writedate;
		/// <summary>
		/// 
		/// </summary>
		public int InfoId
		{
			set{ _infoid=value;}
			get{return _infoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? WorkRecord_InfoId
		{
			set{ _workrecord_infoid=value;}
			get{return _workrecord_infoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IdeaContent
		{
			set{ _ideacontent=value;}
			get{return _ideacontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? WriteDate
		{
			set{ _writedate=value;}
			get{return _writedate;}
		}
		#endregion Model

	}
}

