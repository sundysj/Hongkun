using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_Resources_Specifications ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Resources_Specifications
	{
		public Tb_Resources_Specifications()
		{}
		#region Model
		private string _id;
		private long _bussid;
		private string _propertyid;
		private string _specname;
		private int _sort;
		[DisplayName("��Ʒ���Թ��ID")]
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		[DisplayName("�̼�ID")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public long BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("��Ʒ����ID")]
		public string PropertyId
		{
			set{ _propertyid=value;}
			get{return _propertyid;}
		}
		[DisplayName("��Ʒ���Թ������")]
		public string SpecName
		{
			set{ _specname=value;}
			get{return _specname;}
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

