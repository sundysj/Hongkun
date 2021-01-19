using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_Information_Activities ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Information_Activities
	{
		public Tb_Information_Activities()
		{}
		#region Model
		private long _actid;
		private string _bussid;
		private string _title;
		private string _actpublisher;
		private DateTime? _publishdate;
		private string _actcontent;
		private string _actimage;
		private int? _isdelete;
		[DisplayName("�̼һID")]
		public long ActId
		{
			set{ _actid=value;}
			get{return _actid;}
		}
		[DisplayName("�̼�ID")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("����")]
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		[DisplayName("������")]
		public string ActPublisher
		{
			set{ _actpublisher=value;}
			get{return _actpublisher;}
		}
		[DisplayName("��������")]
		public DateTime? PublishDate
		{
			set{ _publishdate=value;}
			get{return _publishdate;}
		}
		[DisplayName("��������")]
		public string ActContent
		{
			set{ _actcontent=value;}
			get{return _actcontent;}
		}
		[DisplayName("ͼƬ")]
		public string ActImage
		{
			set{ _actimage=value;}
			get{return _actimage;}
		}
		[DisplayName("")]
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

