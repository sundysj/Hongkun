using System;
namespace MobileSoft.Model.Unified
{
	/// <summary>
	/// 实体类Tb_User 。(属性说明自动提取数据库字段的描述信息)
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
        /// 用户头像
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
		/// 姓名
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 手机号码
		/// </summary>
		public string Mobile
        {
			set{ _mobile=value;}
			get{return _mobile;}
		}
		/// <summary>
		/// 邮箱地址
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// qq号码
		/// </summary>
		public string QQ
		{
			set{ _qq=value;}
			get{return _qq;}
		}
		/// <summary>
		/// qq登陆标识
		/// </summary>
		public string QQToken
		{
			set{ _qqtoken=value;}
			get{return _qqtoken;}
		}
		/// <summary>
		/// 微信号码
		/// </summary>
		public string WeChatNum
		{
			set{ _wechatnum=value;}
			get{return _wechatnum;}
		}
		/// <summary>
		/// 微信登录标识
		/// </summary>
		public string WeChatToken
		{
			set{ _wechattoken=value;}
			get{return _wechattoken;}
		}
		/// <summary>
		/// 昵称
		/// </summary>
		public string NickName
		{
			set{ _nickname=value;}
			get{return _nickname;}
		}
		/// <summary>
		/// 密码
		/// </summary>
		public string Pwd
		{
			set{ _pwd=value;}
			get{return _pwd;}
		}
		#endregion Model

	}
}

