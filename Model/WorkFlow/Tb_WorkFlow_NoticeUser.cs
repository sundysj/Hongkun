using System;
namespace MobileSoft.Model.WorkFlow
{
	/// <summary>
	/// ʵ����Tb_WorkFlow_NoticeUser ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_WorkFlow_NoticeUser
	{
		public Tb_WorkFlow_NoticeUser()
		{}
		#region Model
		private int _infoid;
		private string _tb_sys_user_usercode;
		private string _tb_dictionary_noticemethod_dictionarycode;
		private string _noticecontent;
		private string _instanceid;
		private string _tb_dictionary_instancetype_dictionarycode;
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
		public string Tb_Sys_User_UserCode
		{
			set{ _tb_sys_user_usercode=value;}
			get{return _tb_sys_user_usercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tb_Dictionary_NoticeMethod_DictionaryCode
		{
			set{ _tb_dictionary_noticemethod_dictionarycode=value;}
			get{return _tb_dictionary_noticemethod_dictionarycode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NoticeContent
		{
			set{ _noticecontent=value;}
			get{return _noticecontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string InstanceId
		{
			set{ _instanceid=value;}
			get{return _instanceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tb_Dictionary_InstanceType_DictionaryCode
		{
			set{ _tb_dictionary_instancetype_dictionarycode=value;}
			get{return _tb_dictionary_instancetype_dictionarycode;}
		}
		#endregion Model

	}
}

