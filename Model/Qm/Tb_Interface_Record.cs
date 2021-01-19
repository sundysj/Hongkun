using System;
namespace HM.Model.Qm
{
	/// <summary>
	/// ʵ����Tb_Interface_Record ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Interface_Record
	{
		public Tb_Interface_Record()
		{}
		#region Model
		private string _id;
		private string _type;
		private DateTime? _getdate;
		private string _usercode;
		private string _itemcode;
        private string _TaskId;
        /// <summary>
        /// 
        /// </summary>
        public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
        public string TaskId
        {
            set { _TaskId = value; }
            get { return _TaskId; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? GetDate
		{
			set{ _getdate=value;}
			get{return _getdate;}
		}
		/// <summary>
		/// ������Ա
		/// </summary>
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		/// <summary>
		/// ��ĿId
		/// </summary>
		public string ItemCode
		{
			set{ _itemcode=value;}
			get{return _itemcode;}
		}
		#endregion Model

	}
}

