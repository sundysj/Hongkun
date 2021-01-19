using System;
namespace MobileSoft.Model.Unified
{
	/// <summary>
	/// ʵ����Tb_User ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_User
	{
		public Tb_User()
		{}
		#region Model
		private string _id;
		private string _name;
		private string _mobile;
		private string _email;
		private string _qq;
		private string _qqtoken;
		private string _wechatnum;
		private string _wechattoken;
		private string _nickname;
		private string _pwd;
        private string _userPic;
        private int _sex;
        private string _birthday;
        private DateTime _regDate;

        public string Birthday
        {
            set { _birthday = value; }
            get { return _birthday; }
        }

        public DateTime RegDate
        {
            set { _regDate = value; }
            get { return _regDate; }
        }

        public int Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }

        /// <summary>
        /// �û�ͷ��
        /// </summary>
        public string UserPic
        {
            set { _userPic = value; }
            get { return _userPic; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// �ֻ�����
		/// </summary>
		public string Mobile
        {
			set{ _mobile=value;}
			get{return _mobile;}
		}
		/// <summary>
		/// �����ַ
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// qq����
		/// </summary>
		public string QQ
		{
			set{ _qq=value;}
			get{return _qq;}
		}
		/// <summary>
		/// qq��½��ʶ
		/// </summary>
		public string QQToken
		{
			set{ _qqtoken=value;}
			get{return _qqtoken;}
		}
		/// <summary>
		/// ΢�ź���
		/// </summary>
		public string WeChatNum
		{
			set{ _wechatnum=value;}
			get{return _wechatnum;}
		}
		/// <summary>
		/// ΢�ŵ�¼��ʶ
		/// </summary>
		public string WeChatToken
		{
			set{ _wechattoken=value;}
			get{return _wechattoken;}
		}
		/// <summary>
		/// �ǳ�
		/// </summary>
		public string NickName
		{
			set{ _nickname=value;}
			get{return _nickname;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Pwd
		{
			set{ _pwd=value;}
			get{return _pwd;}
		}
		#endregion Model

	}
}

