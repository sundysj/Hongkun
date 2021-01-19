using System;
namespace MobileSoft.Model.SQMSys
{
	/// <summary>
	/// ʵ����Tb_Sys_Group ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Sys_Group
	{
		public Tb_Sys_Group()
		{}
		#region Model
		private string _groupcode;
		private Guid _streetcode;
		private string _groupname;
		private string _groupdescribe;
		private string _grouplead;
		/// <summary>
		/// 
		/// </summary>
		public string GroupCode
		{
			set{ _groupcode=value;}
			get{return _groupcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid StreetCode
		{
			set{ _streetcode=value;}
			get{return _streetcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GroupName
		{
			set{ _groupname=value;}
			get{return _groupname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GroupDescribe
		{
			set{ _groupdescribe=value;}
			get{return _groupdescribe;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GroupLead
		{
			set{ _grouplead=value;}
			get{return _grouplead;}
		}
		#endregion Model

	}
}

