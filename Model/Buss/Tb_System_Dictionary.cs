using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_System_Dictionary ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_System_Dictionary
	{
		public Tb_System_Dictionary()
		{}
		#region Model
		private string _dictionarycode;
		private string _bussid;
		private string _dictionarytype;
		private string _dictionaryname;
		private int? _sort;
		private int? _isdelete;
		[DisplayName("�ֵ��ID")]
		public string DictionaryCode
		{
			set{ _dictionarycode=value;}
			get{return _dictionarycode;}
		}
		[DisplayName("�̼�ID")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("�ֵ����")]
		public string DictionaryType
		{
			set{ _dictionarytype=value;}
			get{return _dictionarytype;}
		}
		[DisplayName("�ֵ�����")]
		public string DictionaryName
		{
			set{ _dictionaryname=value;}
			get{return _dictionaryname;}
		}
		[DisplayName("���")]
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		[DisplayName("�Ƿ�ɾ��")]
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

