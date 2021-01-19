using System;
namespace MobileSoft.Model.Unified
{
	/// <summary>
	/// 实体类Tb_Push_Config 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Push_Config
	{
		public Tb_Push_Config()
		{}
		#region Model
		private string _id;
		private string _package;
		private string _devkey;
		private string _devsecret;
		private string _appkey;
		private string _mastersecret;
		/// <summary>
		/// 极光推送主键
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 包名
		/// </summary>
		public string Package
		{
			set{ _package=value;}
			get{return _package;}
		}
		/// <summary>
		/// DevKey
		/// </summary>
		public string DevKey
		{
			set{ _devkey=value;}
			get{return _devkey;}
		}
		/// <summary>
		/// DevSecret
		/// </summary>
		public string DevSecret
		{
			set{ _devsecret=value;}
			get{return _devsecret;}
		}
		/// <summary>
		/// AppKey
		/// </summary>
		public string AppKey
		{
			set{ _appkey=value;}
			get{return _appkey;}
		}
		/// <summary>
		/// MasterSecret
		/// </summary>
		public string MasterSecret
		{
			set{ _mastersecret=value;}
			get{return _mastersecret;}
		}
		#endregion Model

	}
}

