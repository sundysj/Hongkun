using System;
namespace MobileSoft.Model.Unified
{
	/// <summary>
	/// ʵ����Tb_Push_Config ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		/// ������������
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// ����
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

