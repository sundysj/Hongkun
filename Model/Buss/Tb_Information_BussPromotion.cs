using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_Information_BussPromotion ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Information_BussPromotion
	{
		public Tb_Information_BussPromotion()
		{}
		#region Model
		private long _proid;
		private string _bussid;
		private string _title;
		private string _publisher;
		private DateTime? _publishdate;
		private string _reason;
		private string _proimage;
		private int? _isdelete;
		private int? _sort;
		private string _msgcontent;
		private string _proimageurl;
		private string _remark;
		[DisplayName("�̼Ҵ�����ϢID")]
		public long ProID
		{
			set{ _proid=value;}
			get{return _proid;}
		}
		[DisplayName("�̼�ID")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("��������")]
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		[DisplayName("������")]
		public string Publisher
		{
			set{ _publisher=value;}
			get{return _publisher;}
		}
		[DisplayName("��������")]
		public DateTime? PublishDate
		{
			set{ _publishdate=value;}
			get{return _publishdate;}
		}
		[DisplayName("ԭ��")]
		public string Reason
		{
			set{ _reason=value;}
			get{return _reason;}
		}
		[DisplayName("ͼƬ")]
		public string ProImage
		{
			set{ _proimage=value;}
			get{return _proimage;}
		}
		[DisplayName("�Ƿ�ɾ��")]
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		[DisplayName("�������")]
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		[DisplayName("����")]
		public string MsgContent
		{
			set{ _msgcontent=value;}
			get{return _msgcontent;}
		}
		[DisplayName("ͼƬת��·��")]
		public string ProImageUrl
		{
			set{ _proimageurl=value;}
			get{return _proimageurl;}
		}
		[DisplayName("��ע")]
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

