using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// 实体类Tb_OAPublicWork_OfficialDocumentBrowseUser 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_OfficialDocumentBrowseUser
	{
		public Tb_OAPublicWork_OfficialDocumentBrowseUser()
		{}
		#region Model
		private int _infoid;
		private int? _officialdocumentid;
		private string _usercode;
		/// <summary>
		/// 
		/// </summary>
		public int InfoID
		{
			set{ _infoid=value;}
			get{return _infoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? OfficialDocumentID
		{
			set{ _officialdocumentid=value;}
			get{return _officialdocumentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		#endregion Model

	}
}

