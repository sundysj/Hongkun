using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_Unify_RelationProperty ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Unify_RelationProperty
	{
		public Tb_Unify_RelationProperty()
		{}
		#region Model
		private long _id;
		private string _bussid;
		private long _corpid;
		private string _commid;
		private int? _sort;
		[DisplayName("С����Ӧ�̼�ID")]
		public long ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		[DisplayName("�̼�ID")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public string BussID
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("���ID")]
		public long CorpID
		{
			set{ _corpid=value;}
			get{return _corpid;}
		}
		[DisplayName("С��ID")]
		public string CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		[DisplayName("")]
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		#endregion Model

	}
}

