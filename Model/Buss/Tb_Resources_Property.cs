using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_Resources_Property ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Resources_Property
	{
		public Tb_Resources_Property()
		{}
		#region Model
		private string _id;
		private long _bussid;
		private string _propertyname;
		private int _sort;
		[DisplayName("��Ʒ���Ա�ID")]
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		[DisplayName("�̼�ID")]
		public long BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("��Ʒ��������")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public string PropertyName
		{
			set{ _propertyname=value;}
			get{return _propertyname;}
		}
		[DisplayName("�������")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		#endregion Model

	}
}

