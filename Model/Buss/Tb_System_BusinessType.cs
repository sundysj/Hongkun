using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_System_BusinessType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_System_BusinessType
	{
		public Tb_System_BusinessType()
		{}
		#region Model
		private string _businesstypecode;
		private string _businesscategory;
		private string _businesstypename;
		[DisplayName("��Ʒ�����ID")]
		public string BusinessTypeCode
		{
			set{ _businesstypecode=value;}
			get{return _businesstypecode;}
		}
		[DisplayName("��ƷĿ¼����ʶ")]
		public string BusinessCategory
		{
			set{ _businesscategory=value;}
			get{return _businesscategory;}
		}
		[DisplayName("��Ʒ�����������")]
		public string BusinessTypeName
		{
			set{ _businesstypename=value;}
			get{return _businesstypename;}
		}
		#endregion Model

	}
}

