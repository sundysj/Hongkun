using System;
namespace MobileSoft.Model.System
{
	/// <summary>
	/// ʵ����Tb_System_Config ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_System_Config
	{
		public Tb_System_Config()
		{}
		#region Model
		private string _configkey;
		private string _configname;
		private string _configvalue;
		private string _memo;
		/// <summary>
		/// 
		/// </summary>
		public string ConfigKey
		{
			set{ _configkey=value;}
			get{return _configkey;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ConfigName
		{
			set{ _configname=value;}
			get{return _configname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ConfigValue
		{
			set{ _configvalue=value;}
			get{return _configvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		#endregion Model

	}
}

