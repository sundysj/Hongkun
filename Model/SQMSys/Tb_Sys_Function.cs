using System;
namespace MobileSoft.Model.SQMSys
{
	/// <summary>
	/// ʵ����Tb_Sys_Function ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Sys_Function
	{
		public Tb_Sys_Function()
		{}
		#region Model
		private string _functioncode;
		private string _functionname;
		private string _memo;
		/// <summary>
		/// 
		/// </summary>
		public string FunctionCode
		{
			set{ _functioncode=value;}
			get{return _functioncode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FunctionName
		{
			set{ _functionname=value;}
			get{return _functionname;}
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

