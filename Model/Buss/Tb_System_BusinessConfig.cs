using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_System_BusinessConfig ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_System_BusinessConfig
	{
		public Tb_System_BusinessConfig()
		{}
		#region Model
		private string _id;
		private string _communityid;
		private string _bussid;
		private string _value;
		[DisplayName("")]
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		[DisplayName("��ӪϵͳС��ID")]
		public string CommunityId
		{
			set{ _communityid=value;}
			get{return _communityid;}
		}
		[DisplayName("�̼�Id")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("")]
		public string Value
		{
			set{ _value=value;}
			get{return _value;}
		}
		#endregion Model

	}
}

