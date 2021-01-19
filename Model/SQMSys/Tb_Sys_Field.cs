using System;
namespace MobileSoft.Model.SQMSys
{
	/// <summary>
	/// 实体类Tb_Sys_Field 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Sys_Field
	{
		public Tb_Sys_Field()
		{}
		#region Model
		private Guid _fieldcode;
		private string _templatecode;
		private string _fieldsign;
		private string _fieldname;
		private string _defaultname;
		private int? _fieldorderid;
		private int? _fieldtype;
		private int? _fieldlength;
		private int? _isused;
		/// <summary>
		/// 
		/// </summary>
		public Guid FieldCode
		{
			set{ _fieldcode=value;}
			get{return _fieldcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TemplateCode
		{
			set{ _templatecode=value;}
			get{return _templatecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FieldSign
		{
			set{ _fieldsign=value;}
			get{return _fieldsign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FieldName
		{
			set{ _fieldname=value;}
			get{return _fieldname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DefaultName
		{
			set{ _defaultname=value;}
			get{return _defaultname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FieldOrderID
		{
			set{ _fieldorderid=value;}
			get{return _fieldorderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FieldType
		{
			set{ _fieldtype=value;}
			get{return _fieldtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FieldLength
		{
			set{ _fieldlength=value;}
			get{return _fieldlength;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsUsed
		{
			set{ _isused=value;}
			get{return _isused;}
		}
		#endregion Model

	}
}

