using System;
namespace MobileSoft.Model.WorkFlow
{
	/// <summary>
	/// ʵ����Tb_WorkFlow_DepartUserOprLevel ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_WorkFlow_DepartUserOprLevel
	{
		public Tb_WorkFlow_DepartUserOprLevel()
		{}
		#region Model
		private int _infoid;
		private string _depcode;
		private string _oprlevelcode;
		private string _usercode;
		/// <summary>
		/// 
		/// </summary>
		public int InfoId
		{
			set{ _infoid=value;}
			get{return _infoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DepCode
		{
			set{ _depcode=value;}
			get{return _depcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OprLevelCode
		{
			set{ _oprlevelcode=value;}
			get{return _oprlevelcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		#endregion Model

	}
}

