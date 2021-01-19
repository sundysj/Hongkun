using System;
namespace HM.Model.Qm
{
	/// <summary>
	/// 实体类Tb_Interface_Record 。(属性说明自动提取数据库字段的描述信息)
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
        /// 类型
        /// </summary>
        public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 下载时间
		/// </summary>
		public DateTime? GetDate
		{
			set{ _getdate=value;}
			get{return _getdate;}
		}
		/// <summary>
		/// 下载人员
		/// </summary>
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		/// <summary>
		/// 项目Id
		/// </summary>
		public string ItemCode
		{
			set{ _itemcode=value;}
			get{return _itemcode;}
		}
		#endregion Model

	}
}

