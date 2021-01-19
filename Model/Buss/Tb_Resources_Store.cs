using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_Resources_Store ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Resources_Store
	{
		public Tb_Resources_Store()
		{}
		#region Model
		private string _id;
		private string _bussid;
		private string _resourcesid;
		private decimal _quantity;
		private DateTime _issuedate;
		[DisplayName("��Ʒ���")]
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		[DisplayName("�̼�ID")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("��ԴID")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public string ResourcesID
		{
			set{ _resourcesid=value;}
			get{return _resourcesid;}
		}
		[DisplayName("����")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public decimal Quantity
		{
			set{ _quantity=value;}
			get{return _quantity;}
		}
		[DisplayName("��������")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public DateTime IssueDate
		{
			set{ _issuedate=value;}
			get{return _issuedate;}
		}
		#endregion Model

	}
}

