using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// ʵ����Tb_OAPublicWork_WorkRecordNoticeUser ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_WorkRecordNoticeUser
	{
		public Tb_OAPublicWork_WorkRecordNoticeUser()
		{}
		#region Model
		private int _infoid;
		private string _myusercode;
		private string _tousercode;
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
		public string MyUserCode
		{
			set{ _myusercode=value;}
			get{return _myusercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ToUserCode
		{
			set{ _tousercode=value;}
			get{return _tousercode;}
		}
		#endregion Model

	}
}

