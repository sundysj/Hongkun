using System;
namespace MobileSoft.Model.Unified
{
	/// <summary>
	/// ʵ����Tb_User_History ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		/// �û�ID
		/// </summary>
		public string UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string LogContent
		{
			set{ _logcontent=value;}
			get{return _logcontent;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OprDate
		{
			set{ _oprdate=value;}
			get{return _oprdate;}
		}
		#endregion Model

	}
}

