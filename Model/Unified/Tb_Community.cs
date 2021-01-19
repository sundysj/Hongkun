using System;
namespace MobileSoft.Model.Unified
{
	/// <summary>
	/// ʵ����Tb_Community ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		/// ʡ
		/// </summary>
		public string Province
		{
			set{ _province=value;}
			get{return _province;}
		}
		/// <summary>
		/// ��
		/// </summary>
		public string Area
		{
			set{ _area=value;}
			get{return _area;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string City
		{
			set{ _city=value;}
			get{return _city;}
		}
		/// <summary>
		/// ����������IP
		/// </summary>
		public string DBServer
		{
			set{ _dbserver=value;}
			get{return _dbserver;}
		}
		/// <summary>
		/// ��˾ID
		/// </summary>
		public int CorpID
		{
			set{ _corpid=value;}
			get{return _corpid;}
		}
		/// <summary>
		/// ���ݿ�����
		/// </summary>
		public string DBName
		{
			set{ _dbname=value;}
			get{return _dbname;}
		}
		/// <summary>
		/// �û���
		/// </summary>
		public string DBUser
		{
			set{ _dbuser=value;}
			get{return _dbuser;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string DBPwd
		{
			set{ _dbpwd=value;}
			get{return _dbpwd;}
		}
		/// <summary>
		/// С��ID
		/// </summary>
		public string CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// ��˾����
		/// </summary>
		public string CorpName
		{
			set{ _corpname=value;}
			get{return _corpname;}
		}
		/// <summary>
		/// С������
		/// </summary>
		public string CommName
		{
			set{ _commname=value;}
			get{return _commname;}
		}
		/// <summary>
		/// С��ģ��Ȩ��
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

