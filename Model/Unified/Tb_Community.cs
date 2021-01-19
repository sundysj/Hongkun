using System;
namespace MobileSoft.Model.Unified
{
	/// <summary>
	/// 实体类Tb_Community 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Community
	{
		public Tb_Community()
		{}
		#region Model
		private string _id;
		private string _province;
		private string _area;
		private string _city;
		private string _dbserver;
		private int _corpid;
		private string _dbname;
		private string _dbuser;
		private string _dbpwd;
		private string _commid;
		private string _corpname;
		private string _commname;
		private string _modulerights;
        private bool _isMultiDoorControlServer;
        
        /// <summary>
        /// 
        /// </summary>
        public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 省
		/// </summary>
		public string Province
		{
			set{ _province=value;}
			get{return _province;}
		}
		/// <summary>
		/// 市
		/// </summary>
		public string Area
		{
			set{ _area=value;}
			get{return _area;}
		}
		/// <summary>
		/// 城市
		/// </summary>
		public string City
		{
			set{ _city=value;}
			get{return _city;}
		}
		/// <summary>
		/// 服务器所在IP
		/// </summary>
		public string DBServer
		{
			set{ _dbserver=value;}
			get{return _dbserver;}
		}
		/// <summary>
		/// 公司ID
		/// </summary>
		public int CorpID
		{
			set{ _corpid=value;}
			get{return _corpid;}
		}
		/// <summary>
		/// 数据库名称
		/// </summary>
		public string DBName
		{
			set{ _dbname=value;}
			get{return _dbname;}
		}
		/// <summary>
		/// 用户名
		/// </summary>
		public string DBUser
		{
			set{ _dbuser=value;}
			get{return _dbuser;}
		}
		/// <summary>
		/// 密码
		/// </summary>
		public string DBPwd
		{
			set{ _dbpwd=value;}
			get{return _dbpwd;}
		}
		/// <summary>
		/// 小区ID
		/// </summary>
		public string CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 公司名称
		/// </summary>
		public string CorpName
		{
			set{ _corpname=value;}
			get{return _corpname;}
		}
		/// <summary>
		/// 小区名称
		/// </summary>
		public string CommName
		{
			set{ _commname=value;}
			get{return _commname;}
		}
		/// <summary>
		/// 小区模块权限
		/// </summary>
		public string ModuleRights
		{
			set{ _modulerights=value;}
			get{return _modulerights;}
		}

        public string Tel { get; set; }

        public bool IsMultiDoorControlServer
        {
            set { _isMultiDoorControlServer = value; }
            get { return _isMultiDoorControlServer; }
        }

        #endregion Model

    }
}

